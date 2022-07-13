using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Exceptions;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;

namespace ToolSeoViet.Service.Implements
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor)
        {
        }

        public async Task<ListProjectResponse> All(GetProjectRequest request)
        {
            var data = await db.Projects.Where(o => o.UserId == (request.Id)).ToListAsync();

            if (data == null)
            {
                throw new ProjectException(Messages.Project.CheckProject.Project_NotFound);
            }

            var dataResponse = data.Select(o => new ProjectDto()
            {
                Id = o.Id,
                Name = o.Name,
                Domain = o.Domain

            }).ToList();


            return new()
            {
                Count = dataResponse.Count,
                Items = dataResponse
            };
        }
        public async Task<Project> Get(GetProjectRequest request)
        {
            var data = await db.Projects.Where(o => o.UserId == (request.Id)).ToListAsync();

            if (data == null)
            {
                throw new ProjectException(Messages.Project.CheckProject.Project_NotFound);
            }

            var dataResponse = data.Select(o => new GetProjectDto()
            {
                Id = o.Id,
                Key = o.Key,
                Index = o.Index,
                Url = o.Url,


            });


            return new()
            {
                Count = dataResponse.Count,
                Items = dataResponse
            };
        }

        public Task<SaveProjact> Save(PostProjectModel request)
        {
            throw new NotImplementedException();
        }
    }
}
