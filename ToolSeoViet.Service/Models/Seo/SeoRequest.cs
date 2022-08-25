namespace ToolSeoViet.Service.Models.Seo {
    public partial class SearchContentRequest {
        public string KeyWord { get; set; }
        public string Num { get; set; }
    }

    public partial class SearchIndexRequest {
        public string Key { get; set; }
        public string Domain { get; set; }
    }
}
