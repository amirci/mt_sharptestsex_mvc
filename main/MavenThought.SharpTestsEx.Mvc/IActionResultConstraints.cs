namespace MavenThought.SharpTestsEx.Mvc
{
    public interface IActionResultConstraints 
    {
        IViewRenderConstraints Render { get; }

        IJsonRenderConstraints Return { get; }

        IViewDataConstraints Set(string key);
    }
}