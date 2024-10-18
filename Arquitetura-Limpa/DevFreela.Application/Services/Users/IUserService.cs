using DevFreela.API.Models.Users;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Models.Users;

namespace DevFreela.Application.Services.Users
{
    public interface IUserService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateUserInputModel model);
        //ResultViewModel<int> InsertProfilePicture(int id, IFormFile file);
        ResultViewModel<int> InsertSkills(int id, UserSkillsInputModel model);

    }
}