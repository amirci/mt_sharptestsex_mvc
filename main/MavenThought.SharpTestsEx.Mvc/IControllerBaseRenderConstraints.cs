using System.Net;

namespace MavenThought.SharpTestsEx.Mvc
{
    public interface IControllerBaseRenderConstraints
    {
        void RespondWith(HttpStatusCode statusCode);
    }
}