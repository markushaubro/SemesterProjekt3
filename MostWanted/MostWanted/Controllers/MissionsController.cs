using Microsoft.AspNetCore.Mvc;
using MostWanted.Api.Data;
using MostWanted.Api.Models;
using System.Collections.Generic;

namespace MostWanted.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Mission>> Get()
        {
            var missions = MissionRepository.GetMissions();
            return Ok(missions);
        }
    }
}
