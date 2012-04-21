require 'rubygems'    

puts "Chequing bundled dependencies, please wait...."

system "bundle install --system --quiet"
Gem.clear_paths

require 'albacore'
require 'git'
require 'rake/clean'

include FileUtils

solution_file = FileList["*.sln"].first
project_name = "MavenThought.SharpTestsEx.Mvc"
commit = Git.open(".").log.first.sha[0..10] rescue 'na'
version = IO.readlines('VERSION')[0] rescue "0.0.0.0"
deploy_folder = "c:/temp/build/#{project_name}.#{version}_#{commit}"
merged_folder = "#{deploy_folder}/merged"

CLEAN.include("main/**/bin", "main/**/obj", "*.xml", "*.gemspec", "*.vsmdi", "test/**/obj", "test/**/bin", "*.testsettings")

CLOBBER.include("**/_*", "**/.svn", "Packages/*", "**/*.user", "**/*.cache", "**/*.suo")

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep"]

desc "Updates build version, generate zip, merged version and the gem in #{deploy_folder}"
task :deploy => ["deploy:all"]

desc "Run all tests"
task :test => ["test:all"]

namespace :setup do
	desc "Setup dependencies for nuget packages"
	task :dep do
		FileList["**/packages.config"].each do |file|
			sh "nuget install #{file} /OutputDirectory Packages"
		end
	end
end

namespace :build do

	desc "Build the project"
	msbuild :all, [:config] => [:setup] do |msb, args|
		msb.properties :configuration => args[:config] || :Debug
		msb.targets :Build
		msb.solution = solution_file
	end

	desc "Rebuild the project"
	task :re => ["clean", "build:all"]
end

namespace :test do
	
	desc 'Run all tests'
	task :all => [:default] do 
		tests = FileList["test/**/bin/debug/**/*.Tests.dll"].join " "
		system "./tools/gallio/bin/gallio.echo.exe #{tests}"
	end
	
end

namespace :deploy do

	desc "Publish nuspec package"
	task :publish  => ["util:build_release"] do
		nuget_lib = "nuget/lib/NET35"
		clean_folder = Rake::Task["util:clean_folder"]
		package = Rake::Task["deploy:package"]
		["", "nunit", "mstest", "xunit"].each do |ext|
			clean_folder.invoke("nuget")
			mkdir_p nuget_lib
			cp FileList["main/**/bin/release/MavenT*#{ext}.dll"][0], nuget_lib
			nuget_package = "maventhought.testing#{ext.empty? ? "" : "." + ext}"
			package.invoke(nuget_package)
			clean_folder.reenable
			package.reenable
			sh "nuget push nuget/#{nuget_package}.#{version}.nupkg" 
		end
	end 
	
	desc 'Deletes the last published packages'
	task :unpublish do
		["", "nunit", "mstest", "xunit"].each do |ext|
			nuget_package = "maventhought.testing#{ext.empty? ? "" : "." + ext}"
			sh "nuget delete #{nuget_package} #{version}"
		end
	end

	nuspec :spec, :package_id  do |nuspec, args|
	   nuspec.id = args.package_id
	   nuspec.version = version
	   nuspec.authors = "Amir Barylko"
	   nuspec.owners = "Amir Barylko"
	   nuspec.description = "Extensions to SharpTestsEx for ASP.NET MVC"
	   nuspec.summary = "Assertions for action result, json responses, and more"
	   nuspec.language = "en-US"
	   nuspec.licenseUrl = "https://github.com/amirci/mt_sharptestsex_mvc/LICENSE"
	   nuspec.projectUrl = "https://github.com/amirci/mt_sharptestsex_mvc"
	   nuspec.working_directory = "nuget"
	   nuspec.output_file = "#{args.package_id}.#{version}.nuspec"
	   nuspec.tags = "testing mvc sharptestsex"
	   nuspec.dependency "SharpTestsEx"
	end
	
	nugetpack :package, :package_id do |p, args|
		spec = Rake::Task["deploy:spec"]
		spec.invoke(args.package_id)
		spec.reenable
		p.nuspec = "nuget/#{args.package_id}.#{version}.nuspec"
		p.output = "nuget"
	end
end

namespace :util do
	task :clean_folder, :folder do |t, args|
		rm_rf(args.folder)
		Dir.mkdir(args.folder) unless File.directory? args.folder
	end
	
	assemblyinfo :update_version, :file do |asm, args|
		asm.version = version
		asm.company_name = "MavenThought Inc."
		asm.product_name = "MavenThought Testing (sha #{commit})"
		asm.copyright = "MavenThought Inc. 2006 - #{DateTime.now.year}"
		asm.output_file = "GlobalAssemblyInfo.cs"
	end	

	task :build_release => [:update_version] do 
		Rake::Task["build:all"].invoke(:Release)
	end
end