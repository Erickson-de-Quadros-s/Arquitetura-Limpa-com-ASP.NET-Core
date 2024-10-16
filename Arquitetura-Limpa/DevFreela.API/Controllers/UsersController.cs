using DevFreela.API.Models.Users;
using DevFreela.Application.Models.Users;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public UsersController(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _dbContext.Users
                          .Include(u => u.Skills)
                            .ThenInclude(u => u.Skill)
                          .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }


        // POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var NewUser = new User(model.Name, model.Email, model.BirthDate);

            _dbContext.Users.Add(NewUser);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"FIle: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}