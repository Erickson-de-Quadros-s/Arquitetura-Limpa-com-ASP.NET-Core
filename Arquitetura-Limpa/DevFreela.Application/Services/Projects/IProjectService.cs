﻿using DevFreela.Application.Models.ProjectModel;
using DevFreela.Application.Models.Result;
using DevFreela.Application.Models.Users;

namespace DevFreela.Application.Services.Projects
{
    public interface IProjectService
    {
        ResultViewModel<ProjectItemViewModel> GetById(int id);
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3);
        ResultViewModel<int> Insert(CreateProjectInputModel model);
        ResultViewModel Update(int id, UpdateProjectInputModel model);
        ResultViewModel Delete(int id);
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model);
    }

}