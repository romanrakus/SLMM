using System.Threading.Tasks;
using System.Web.Http;
using SLMM.Core;

namespace SLMM.Server.Controllers
{
    public class RotationController : ApiController
    {
        [HttpPut]
        public async Task<IHttpActionResult> Rotate([FromBody]Rotation direction)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRotation()
        {
            return Ok();
        }
    }
}
