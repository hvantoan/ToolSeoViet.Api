using System.Threading.Tasks;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Service.Models.Seo;

namespace ToolSeoViet.Service.Interfaces {
    public interface ISeoService {
        Task<SearchContentDto> GetContennt(SearchContentRequest request);
        Task<ProjectDetailDto> Position(SearchPositionRequest request);
        Task<SearchIndex> Index(SearchIndexRequest request);
    }
}
