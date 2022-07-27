using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Service.Models.SearchContent;
using ToolSeoViet.Service.Models.Seo;

namespace ToolSeoViet.Service.Interfaces {
    public interface ISearchContentService {
        Task<ListSearchContentResponse> All();
        Task Save(SearchContentDto searchContent);

    }
}
