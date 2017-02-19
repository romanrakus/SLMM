using System.Web.Http;
using SLMM.Communication;

namespace SLMM.Server.Controllers
{
    public class MowerController : ApiController
    {
        private readonly ILownManager _lownManager;

        public MowerController(ILownManager lownManager)
        {
            _lownManager = lownManager;
        }

        [HttpPut]
        public IHttpActionResult Switch([FromBody]bool on)
        {
            _lownManager.Mow(on);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetStatus()
        {
            return Ok(_lownManager.IsMowing());
        }
    }
}
