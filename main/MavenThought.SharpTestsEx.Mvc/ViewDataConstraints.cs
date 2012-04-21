using System.Web.Mvc;
using SharpTestsEx;

namespace MavenThought.SharpTestsEx.Mvc
{
    class ViewDataConstraints : IViewDataConstraints
    {
        private readonly ViewResult _viewResult;
        private readonly string _key;
        private readonly string _msg;

        public ViewDataConstraints(ViewResult viewResult, string key, string msg)
        {
            _viewResult = viewResult;
            _key = key;
            _msg = msg;
        }

        public void To(string value)
        {
            var msg = _msg ?? "The viewdata dictionary with key {0} should match the expected value";

            this._viewResult.ViewData[this._key].ToString().Should(msg).Be(value);
        }

        public void To<T>(T value)
        {
            var msg = _msg ?? "The viewdata dictionary with key {0} should match the expected value";

            ((T)this._viewResult.ViewData[this._key]).Should(msg).Be(value);
        }
    }
}