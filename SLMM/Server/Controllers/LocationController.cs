using System.Web.Http;
using SLMM.Communication;

namespace SLMM.Server.Controllers
{
    public class LocationController : ApiController
    {
        private readonly ILownManager _lownManager;

        public LocationController(ILownManager lownManager)
        {
            _lownManager = lownManager;
        }

        [HttpPut]
        public IHttpActionResult Move([FromBody]int moveBy)
        {
            _lownManager.MoveBy(moveBy);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetCurrentLocation()
        {
            return Ok(_lownManager.GetLocation());
        }
    }
}
