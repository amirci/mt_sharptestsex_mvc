using System.Web.Mvc;
using Newtonsoft.Json;
using SharpTestsEx;

namespace MavenThought.SharpTestsEx.Mvc
{
    class JsonRenderConstraints : IJsonRenderConstraints
    {
        private readonly JsonResult _jsonResult;
        private readonly string _msg;

        public JsonRenderConstraints(JsonResult jsonResult, string msg)
        {
            _jsonResult = jsonResult;
            _msg = msg;
        }

        public void Json(object match)
        {
            var data = this._jsonResult.Data;

            JsonConvert.SerializeObject(match)
                .Should(_msg ?? "Both objects should match JSON serialized!")
                .Be(JsonConvert.SerializeObject(data));
        }

        public void Json()
        {
            
        }
    }
}