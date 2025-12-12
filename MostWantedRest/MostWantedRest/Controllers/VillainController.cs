using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileLib.RepoVillains;

namespace MostWantedRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillainController : ControllerBase
    {
        private readonly IVillianRepo _villainRepo;

        public VillainController(IVillianRepo villainRepo)
        {
            _villainRepo = villainRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Villain>> GetAll()
        {
            var villains = _villainRepo.GetAll();
            return Ok(villains);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Villain> GetById(int id)
        {
            var villain = _villainRepo.GetById(id);
            if (villain == null)
            {
                return NotFound($"Villain with ID {id} not found");
            }
            return Ok(villain);
        }

        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Villain>> GetActive()
        {
            var activeVillains = _villainRepo.GetActiveVillains();
            return Ok(activeVillains);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Villain> Post([FromBody] CreateVillainDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var villain = new Villain
            {
                Title = request.Title,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                MaxReward = request.MaxReward,
                IsActive = true
            };

            var created = _villainRepo.Add(villain);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeleteAll()
        {
            _villainRepo.DeleteAll();
            return NoContent();
        }


    }
}
