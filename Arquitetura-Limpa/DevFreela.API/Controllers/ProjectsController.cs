using DevFreela.Application.Models.ProjectModel;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Services.Projects;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(
            IProjectService projectService
            )
        {
            _projectService = projectService;
        }

        [HttpGet]
        public ResultViewModel<List<ProjectItemViewModel>> Get(string search = "", int page = 0, int size = 3)
        {
            return _projectService.GetAll(search, page, size);
        }

        [HttpGet("{id}")]
        public ResultViewModel<ProjectItemViewModel> GetById(int id)
        {
            return _projectService.GetById(id);
        }

        [HttpPost]
        public ResultViewModel<int> Post(CreateProjectInputModel model)
        {
            return _projectService.Insert(model);
        }

        [HttpPut("{id}")]
        public ResultViewModel Put(int id, [FromBody] UpdateProjectInputModel model)
        {
            return _projectService.Update(id, model);
        }

        [HttpDelete("{id}")]
        public ResultViewModel Delete(int id)
        {
            return _projectService.Delete(id);
        }

        [HttpPut("{id}/start")]
        public ResultViewModel Start(int id)
        {
            return _projectService.Start(id);
        }

        [HttpPut("{id}/complete")]
        public ResultViewModel Complete(int id)
        {
            return _projectService.Complete(id);
        }

        [HttpPost("{id}/comments")]
        public ResultViewModel PostComment(int id, CreateProjectCommentInputModel model)
        {
            return _projectService.InsertComment(id, model);
        }

    }
}