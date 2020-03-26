using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace RedisWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public JsonResult<string[]> Get()
        {
            return Json(new string[] { "value1", "value2" });
        }
    }
}
