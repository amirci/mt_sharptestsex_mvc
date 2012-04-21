using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace MavenThought.SharpTestsEx.Mvc
{
    public abstract class ControllerSpec
    {
        protected NameValueCollection Headers { get; set; }

        protected NameValueCollection RequestParams { get; set; }

        protected ActionResult ActionResult { get; set; }

        protected ViewResultBase ViewResult
        {
            get { return (ViewResultBase) this.ActionResult; }
        }

        protected HttpContextBase Context { get; set; }

        protected HttpRequestBase Request { get; set; }

        protected HttpResponseBase Response { get; set; }
    }
}
