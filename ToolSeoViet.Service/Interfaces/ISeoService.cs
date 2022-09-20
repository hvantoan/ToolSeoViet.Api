using System.Threading.Tasks;
using ToolSeoViet.Service.Models.Seo;

namespace ToolSeoViet.Service.Interfaces {
    public interface ISeoService {
        Task<SearchContentDto> GetContennt(SearchContentRequest request);
        Task<SearchPosition> Position(SearchPositionRequest request);
        Task<SearchIndex> Index(SearchIndexRequest request);
    }
}
