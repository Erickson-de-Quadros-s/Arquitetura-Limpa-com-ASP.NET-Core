using DevFreela.API.Models.Users;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Models.Users;
using DevFreela.Application.Services.Users;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ResultViewModel<UserViewModel> Get(int id)
        {
           return _userService.GetById(id);
        }

        [HttpPost]
        public ResultViewModel<int> Post(CreateUserInputModel model)
        {
           return _userService.Insert(model);
        }

        //[HttpPut("{id}/profile-picture")]
        //public ResultViewModel<int> PostProfilePicture(int id, IFormFile file)
        //{
        //    var description = $"FIle: {file.FileName}, Size: {file.Length}";

        //    // Processar a imagem

        //    return Ok(description);
        //}
        [HttpPost("{id}/skills")]
        public ResultViewModel<int> PostSkills(int id, UserSkillsInputModel model)
        {
           return _userService.InsertSkills(id, model);
        }
    }
}