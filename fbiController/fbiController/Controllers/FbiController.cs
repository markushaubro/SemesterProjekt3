using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using fbiController;          
using fbiController.DTO;      
using fbiController.Models;   

namespace fbiController.Controllers
{
    [ApiController]
    [Route("api/fbi")]
    public class FbiController : ControllerBase
    {
        private readonly WantedDbContext _db;
        private readonly IHttpClientFactory _httpClientFactory;

        public FbiController(WantedDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync(CancellationToken ct)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("fbi");

                int page = 1;
                int updated = 0;
                const int maxPages = 100; 

                while (!ct.IsCancellationRequested && page <= maxPages)
                {
                    var url = $"wanted/v1/list?page={page}";
                    var httpResponse = await client.GetAsync(url, ct);

                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        return StatusCode(503, $"FBI API rate limit hit on page {page}.");
                    }

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        return StatusCode((int)httpResponse.StatusCode,
                            $"FBI API error on page {page}: {httpResponse.StatusCode}");
                    }

                    var response = await httpResponse.Content
                        .ReadFromJsonAsync<FbiListResponse>(cancellationToken: ct);

                    if (response?.Items == null || response.Items.Count == 0)
                        break;

                    foreach (var item in response.Items)
                    {
                        var hasReward =
                            (item.RewardMin.HasValue && item.RewardMin.Value > 0) ||
                            (item.RewardMax.HasValue && item.RewardMax.Value > 0);

                        if (!hasReward)
                        {
                            continue;
                        }

                        var entity = await _db.WantedPeople
                            .FirstOrDefaultAsync(x => x.FbiUid == item.Uid, ct);

                        if (entity == null)
                        {
                            entity = new WantedPerson
                            {
                                FbiUid = item.Uid
                            };
                            _db.WantedPeople.Add(entity);
                        }

                        entity.Title = item.Title;
                        entity.Description = item.Description;
                        entity.Sex = item.Sex;
                        entity.Nationality = item.Nationality;
                        entity.Status = item.Status;
                        entity.Url = item.Url;
                        entity.Publication = item.Publication;
                        entity.ImageUrl = item.Images?.FirstOrDefault()?.Thumb
                                              ?? item.Images?.FirstOrDefault()?.Large;

                        entity.RewardMin = item.RewardMin;
                        entity.RewardMax = item.RewardMax;

                        entity.LastSyncedUtc = DateTime.UtcNow;

                        updated++;
                    }

                    await _db.SaveChangesAsync(ct);

                    int pageSize = response.Items.Count;
                    if (response.Total <= page * pageSize)
                        break;

                    page++;
                    await Task.Delay(250, ct);
                }

                return Ok(new { updated });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error during sync: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var query = _db.WantedPeople.AsQueryable();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(x => x.Publication)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return Ok(new { total, page, pageSize, items });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var item = await _db.WantedPeople.FindAsync(new object[] { id }, ct);
            return item is null ? NotFound() : Ok(item);
        }
    }
}
