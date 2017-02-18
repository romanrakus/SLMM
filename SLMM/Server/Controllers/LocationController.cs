using System.Threading.Tasks;
using System.Web.Http;

namespace SLMM.Server.Controllers
{
    public class LoctionController : ApiController
    {
        [HttpPut]
        public async Task<IHttpActionResult> Move([FromBody]int moveBy)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentLocation()
        {
            return Ok();
        }
    }
}
