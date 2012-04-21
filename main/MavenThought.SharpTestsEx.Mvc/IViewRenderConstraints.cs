using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MavenThought.SharpTestsEx.Mvc
{
    public interface IViewRenderConstraints
    {
        void View(string viewName);
        void Partial(string viewName);
        void View<TController>(Expression<Func<TController, ActionResult>> action) where TController : ControllerBase;
        void Partial<TController>(Expression<Func<TController, ActionResult>> action) where TController : ControllerBase;
    }
}