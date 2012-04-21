using System.Net;
using System.Web.Mvc;
using SharpTestsEx;

namespace MavenThought.SharpTestsEx.Mvc
{
    public class ControllerBaseRenderConstraints : IControllerBaseRenderConstraints
    {
        private readonly Controller _controller;

        public ControllerBaseRenderConstraints(Controller controller)
        {
            _controller = controller;
        }

        public void RespondWith(HttpStatusCode statusCode)
        {
            this._controller.Response.StatusCode.Should().Be(statusCode);
        }
    }
}