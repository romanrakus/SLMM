using System.Web.Http;
using SLMM.Communication;
using SLMM.Core;
using SLMM.Server.Models;

namespace SLMM.Server.Controllers
{
    public class LawnController : ApiController
    {
        private readonly ILownManager _lownManager;

        public LawnController(ILownManager lownManager)
        {
            _lownManager = lownManager;
        }

        [HttpPost]
        public IHttpActionResult Create(CreateLawnBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _lownManager.Init(new LawnMowingMachine(model.SizeX, model.SizeY, model.StartX, model.StartY));
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_lownManager.GetLocation());
        }
    }
}
