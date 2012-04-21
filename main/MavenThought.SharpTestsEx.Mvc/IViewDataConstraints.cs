namespace MavenThought.SharpTestsEx.Mvc
{
    public interface IViewDataConstraints
    {
        void To(string value);
        void To<T>(T value);
    }
}