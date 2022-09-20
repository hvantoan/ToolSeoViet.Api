namespace ToolSeoViet.Service.Models.Project {
    public class GetProjectRequest {
        public string Id { get; set; }
    }
    public class SaveProjectRequest{
        public ProjectDto Project { get; set; }
    }
    public class DeleteProjectRequest {
        public string Id { get; set; }
    }
}
