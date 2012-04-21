using System.Web.Mvc;

namespace MavenThought.SharpTestsEx.Mvc
{
    public static class ClassConstraintsActionResultExtensions
    {
        public static IActionResultConstraints Should(this ActionResult viewResult)
        {
            return new ActionResultConstraints(viewResult);
        }

        public static IActionResultConstraints Should(this ActionResult viewResult, string msg)
        {
            return new ActionResultConstraints(viewResult, msg);
        }

    }
}