using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileLib;
using MostWantedRest;

namespace MostWantedRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IProfileList _profileRepo;

        public GameController(IGameService gameService, IProfileList profileRepo)
        {
            _gameService = gameService;
            _profileRepo = profileRepo;
        }

        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult StartGame([FromBody] StartGameRequest request)
        {
            var currentUser = _gameService.StartGame(request.ProfileId);
            if (currentUser == null)
            {
                return NotFound($"Profile with ID {request.ProfileId} not found");
            }

            var profile = _profileRepo.GetById(request.ProfileId);
            return Ok(new
            {
                message = "Game started",
                currentUser = currentUser,
                profile = profile
            });
        }

        [HttpPost("end")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult EndGame()
        {
            var currentUser = _gameService.EndGame();
            if (currentUser == null)
            {
                return NotFound("No active game found");
            }

            return Ok(new
            {
                message = "Game ended",
                currentUser = currentUser
            });
        }

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCurrentGame()
        {
            var currentUser = _gameService.GetCurrentGame();
            if (currentUser == null)
            {
                return NotFound("No active game");
            }

            var profile = _profileRepo.GetById(currentUser.ProfileId);
            return Ok(new
            {
                currentUser = currentUser,
                profile = profile
            });
        }

        [HttpPost("addscore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddScore([FromBody] int points)
        {
            if (points <= 0)
            {
                return BadRequest("Points must be positive");
            }

            var profile = _gameService.AddScore(points);
            if (profile == null)
            {
                return NotFound("No active game or profile not found");
            }

            return Ok(new
            {
                message = $"Added {points} points",
                profile = profile
            });
        }
    }
}
