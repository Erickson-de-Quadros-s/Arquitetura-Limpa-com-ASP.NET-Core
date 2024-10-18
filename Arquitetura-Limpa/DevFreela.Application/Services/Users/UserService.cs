using DevFreela.API.Models.Users;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Models.Users;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _dbContext.Users
                         .Include(u => u.Skills)
                           .ThenInclude(u => u.Skill)
                         .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("Not Found");
            }
            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Sucess(model);

        }
        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var NewUser = new User(model.Name, model.Email, model.BirthDate);


            _dbContext.Users.Add(NewUser);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Sucess(NewUser.Id);
        }

        //public ResultViewModel<int> InsertProfilePicture(int id, IFormFile file)
        //{
        //    var description = $"FIle: {file.FileName}, Size: {file.Length}";

        //    // Processar a imagem

        //    return ResultViewModel<int>.Sucess(NewUser.Id);
        //}

        public ResultViewModel<int> InsertSkills(int id, UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Sucess(model.Id);
        }

    }
}