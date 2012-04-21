namespace MavenThought.SharpTestsEx.Mvc
{
    public interface IJsonRenderConstraints
    {
        void Json(object match);
        void Json();
    }
}