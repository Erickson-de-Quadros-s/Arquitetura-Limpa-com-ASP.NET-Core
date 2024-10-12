using DevFreela.Application.Models.ProjectModel;
using DevFreela.Application.Models.ProjetctModel;
using DevFreela.Application.Models.Result;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3);
        ResultViewModel<ProjectViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateProjectInputModel model);
        ResultViewModel Update(UpdateProjectInputModel model);
        ResultViewModel Delete(int id);
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model);
    }

    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ResultViewModel Complete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                          .Include(p => p.Client)
                          .Include(p => p.Freelancer)
                          .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                          .Skip(page * size)
                          .Take(size)
                          .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel Start(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel Update(UpdateProjectInputModel model)
        {
            throw new NotImplementedException();
        }



    }
}