using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using SharpTestsEx;

namespace MavenThought.SharpTestsEx.Mvc
{
    class ViewRenderConstraints : IViewRenderConstraints
    {
        private readonly ViewResultBase _viewResult;
        private readonly string _msg;

        public ViewRenderConstraints(ViewResultBase viewResult, string msg = null)
        {
            _viewResult = viewResult;
            _msg = msg;
        }

        public void View(string viewName)
        {
            var msg = string.IsNullOrEmpty(_msg) ? "The controller should render view called {0}" : _msg;

            this._viewResult.ViewName
                .Should(string.Format(msg, viewName))
                .Be(viewName);
        }

        public void Partial(string viewName)
        {
            ((object) this._viewResult).Should().Be.InstanceOf<PartialViewResult>();

            View(viewName);
        }

        public void View<TController>(Expression<Func<TController, ActionResult>> action) where TController : ControllerBase
        {
            var viewName = ExpressionHelper.GetExpressionText(action);

            var msg = string.IsNullOrEmpty(_msg) ? "The controller should render view called {0} or be empty" : _msg;

            var actual = this._viewResult.ViewName;

            (actual == string.Empty || actual == viewName)
                .Should(string.Format(msg, viewName))
                .Be.True();
                
        }

        public void Partial<TController>(Expression<Func<TController, ActionResult>> action) where TController : ControllerBase
        {
            var viewName = ExpressionHelper.GetExpressionText(action);

            var msg = string.IsNullOrEmpty(_msg) ? "The controller should render a partial view called {0} or be empty" : _msg;

            var actual = this._viewResult.ViewName;

            ((object)this._viewResult).Should().Be.InstanceOf<PartialViewResult>();

            (actual == string.Empty || actual == viewName)
                .Should(string.Format(msg, viewName))
                .Be.True();
        }
    }
}