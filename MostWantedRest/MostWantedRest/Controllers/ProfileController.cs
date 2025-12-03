using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MostWantedRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileList _profileRepo;
        public ProfileController(IProfileList repo) 
        {
            _profileRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Profile> Get()
        {
            return _profileRepo.GetAll();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Profile> Get(int id)
        {
            Profile? profile = _profileRepo.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(profile);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Profile> Post([FromBody] Profile createProfile)
        {
            Profile? profile = _profileRepo.Add(createProfile);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + profile.ID,
                profile);
        }

        // PUT api/<ProfileController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Profile> Put(int id, [FromBody] Profile value)
        {
            Profile? profileToEdit = _profileRepo.Update(id, value);
            if(profileToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(profileToEdit);
            }
            
        }

        // DELETE api/<ProfileController>/5
        [HttpDelete("{id}")]
        public ActionResult<Profile> Delete(int id)
        {
            Profile? deleteProfile = _profileRepo.GetById(id);
            if (deleteProfile == null)
            {
                return NotFound();
            }
            else
            {
                _profileRepo.Remove(id);
                return Ok(deleteProfile);
            }
        }
    }
}
