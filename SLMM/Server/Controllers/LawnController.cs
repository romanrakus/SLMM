using System.Threading.Tasks;
using System.Web.Http;
using SLMM.Server.Models;

namespace SLMM.Server.Controllers
{
    public class LawnController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateLawnBindingModel model)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }
    }
}
