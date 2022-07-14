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
        Task<ProjectDto> Get(GetProjectRequest request);
        Task CreateOrUpdate(ProjectDto request);

    }
}
