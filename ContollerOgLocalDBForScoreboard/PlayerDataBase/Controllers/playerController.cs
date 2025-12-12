using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ScoreboardController : ControllerBase
{
    private readonly GameDbContext _db;

    public ScoreboardController(GameDbContext db)
    {
        _db = db;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlayerScoreboard>> Get(int id)
    {
        var player = await _db.PlayerScoreboards.FindAsync(id);
        if (player == null) return NotFound();
        return Ok(player);
    }
}
