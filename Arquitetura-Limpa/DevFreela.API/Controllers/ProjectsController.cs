using DevFreela.Application.Models;
using DevFreela.Application.Models.ProjectModel;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly DevFreelaDbContext _dbContext;
        public ProjectsController(
            IOptions<FreelanceTotalCostConfig> options,
            DevFreelaDbContext dbContext

            )
        {
            _config = options.Value;
            _dbContext = dbContext;

        }

        // GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        // GET api/projects/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);
            var model = ProjectItemViewModel.FromEntity(project);

            return Ok(model);
        }

        // POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {


            if (model.TotalCost < _config.Minimum || model.TotalCost > _config.Maximum)
            {
                return BadRequest("Numero fora dos limites.");
            }

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        // PUT api/projects/1234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _dbContext.Update(project);
            _dbContext.SaveChanges();


            return NoContent();
        }

        //  DELETE api/projects/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            project.SetAsDeleted();
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Start();
            _dbContext.Update(project);
            _dbContext.SaveChanges();
            return NoContent();
        }

        // PUT api/projects/1234/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Complete();
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}