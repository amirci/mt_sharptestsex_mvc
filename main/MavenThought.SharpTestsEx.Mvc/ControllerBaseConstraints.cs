using System.Web.Mvc;

namespace MavenThought.SharpTestsEx.Mvc
{
    public static class ControllerBaseConstraints
    {
        public static IControllerBaseRenderConstraints Should(this Controller controller)
        {
            return new ControllerBaseRenderConstraints(controller);
        }
    }
}