using DevFreela.Application.Models.ProjectModel;
using DevFreela.Application.Models.Result;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3);
        ResultViewModel<ProjectItemViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateProjectInputModel model);
        ResultViewModel Update(int id, UpdateProjectInputModel model);
        ResultViewModel Delete(int id);
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model);
    }

}