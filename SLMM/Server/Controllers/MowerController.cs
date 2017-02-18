using System.Threading.Tasks;
using System.Web.Http;

namespace SLMM.Server.Controllers
{
    public class MowerController : ApiController
    {
        [HttpPut]
        public async Task<IHttpActionResult> Switch([FromBody]bool on)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetStatus()
        {
            return Ok();
        }
    }
}
