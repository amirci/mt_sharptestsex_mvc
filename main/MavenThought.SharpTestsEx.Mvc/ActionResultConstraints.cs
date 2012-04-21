using System.Web.Mvc;

namespace MavenThought.SharpTestsEx.Mvc
{
    public class ActionResultConstraints : IActionResultConstraints
    {
        private readonly ActionResult _viewResult;
        private readonly string _msg;

        public ActionResultConstraints(ActionResult viewResult, string msg = null)
        {
            _viewResult = viewResult;
            _msg = msg;
        }

        public IViewRenderConstraints Render
        {
            get { return new ViewRenderConstraints((ViewResultBase) _viewResult, _msg); }
        }

        public IJsonRenderConstraints Return
        {
            get { return new JsonRenderConstraints((JsonResult)_viewResult, _msg); }
        }

        public IViewDataConstraints Set(string key)
        {
            return new ViewDataConstraints((ViewResult) this._viewResult, key, _msg);
        }
    }
}