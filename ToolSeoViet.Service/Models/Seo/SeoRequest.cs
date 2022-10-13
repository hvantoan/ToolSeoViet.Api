using ToolSeoViet.Service.Models.Project;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class SearchContentRequest {
        public string KeyWord { get; set; }
        public string Num { get; set; }
    }

    public partial class SearchPositionRequest {
        public ProjectDetailDto ProjectDetail { get; set; }
        public string Domain { get; set; }
    }

    public partial class SearchIndexRequest {
        public string Href { get; set; }
    }
}
