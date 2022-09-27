using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToolSeoViet.Service.Models.Project {
    public class GetProjectRequest {
        public string Id { get; set; }
    }
    public class SaveProjectRequest{
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        public string Domain { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProjectDetailDto> ProjectDetails { get; set; }
    }
    public class DeleteProjectRequest {
        public string Id { get; set; }
    }
}
