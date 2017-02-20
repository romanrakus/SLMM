using System.Web.Http;
using SLMM.Common;
using SLMM.Communication;

namespace SLMM.Server.Controllers
{
    public class RotationController : ApiController
    {
        private readonly ILownManager _lownManager;

        public RotationController(ILownManager lownManager)
        {
            _lownManager = lownManager;
        }

        [HttpPut]
        public IHttpActionResult Rotate([FromBody]Rotation direction)
        {
            _lownManager.Rotate(direction);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetRotation()
        {
            return Ok(_lownManager.GetCurrentPosition());
        }
    }
}
