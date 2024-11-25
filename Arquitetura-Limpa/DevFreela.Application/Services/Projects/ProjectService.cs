﻿using DevFreela.Application.Models.ProjectModel;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Models.Users;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return ResultViewModel.Error("Não foi possível concluir a operação");
            }

            project.Complete();
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return ResultViewModel.Error($"Não foi possivel excluir o registro {id}");
            }
            project.SetAsDeleted();
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
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

        public ResultViewModel<ProjectItemViewModel> GetById(int id)
        {
            var project = _dbContext.Projects
                         .Include(p => p.Client)
                         .Include(p => p.Freelancer)
                         .Include(p => p.Comments)
                         .SingleOrDefault(p => p.Id == id);

            if (project == null)
            {
                return ResultViewModel<ProjectItemViewModel>.Error("Projeto não encontrado.");
            }

            var model = ProjectItemViewModel.FromEntity(project);

            return ResultViewModel<ProjectItemViewModel>.Sucess(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Sucess(project.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return ResultViewModel.Error($"Projeto com a ID {id} nao encontrado!");
            }

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return ResultViewModel.Error($"Projeto não encontrado com a {id}");
            }

            project.Start();
            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Update(int id, UpdateProjectInputModel model)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            project.Update(model.Title, model.Description, model.TotalCost);

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Sucess();
        }

    }
}