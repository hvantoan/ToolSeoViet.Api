using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Models.Project;

namespace ToolSeoViet.Service.Interfaces
{
    public interface IProjectService
    {
        Task<ListProjectResponse> All(GetProjectRequest request);
        Task<Project> Get(GetProjectRequest request);
        Task<SaveProjact>Save(PostProjectModel request);

    }
}
