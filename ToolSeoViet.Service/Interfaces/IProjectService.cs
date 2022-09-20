using System.Threading.Tasks;
using ToolSeoViet.Service.Models.Project;

namespace ToolSeoViet.Service.Interfaces {
    public interface IProjectService
    {
        Task<GetProjectResponse> Get(string id);
        Task<ListProjectResponse> All();
        Task CreateOrUpdate(SaveProjectRequest request);

        Task Delete(string Id);
    }
}
