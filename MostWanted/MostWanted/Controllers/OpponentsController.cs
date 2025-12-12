using Microsoft.AspNetCore.Mvc;
using MostWanted.Api.Data;
using MostWanted.Api.Models;
using System.Collections.Generic;

namespace MostWanted.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpponentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Opponent>> Get()
        {
            var opponents = OpponentRepository.GetOpponents();
            return Ok(opponents);
        }
    }
    namespace MyNamespace
    {
        
    }
}
