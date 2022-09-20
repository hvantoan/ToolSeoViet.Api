using System.Threading.Tasks;
using ToolSeoViet.Service.Models.SearchContent;
using ToolSeoViet.Service.Models.Seo;

namespace ToolSeoViet.Service.Interfaces {
    public interface ISearchContentService {
        Task<ListSearchContentResponse> All();
        Task Save(SearchContentDto searchContent);
        Task<SearchContentDto> Get(GetSearchContent request);

    }
}
