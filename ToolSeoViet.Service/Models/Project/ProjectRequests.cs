using Newtonsoft.Json;
using System.Collections.Generic;
using ToolSeoViet.Service.Models.KeyWord;
using ToolSeoViet.Services.Models;

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
        public List<KeyWordDto> KeyWords { get; set; }
    }
    public class DeleteProjectRequest {
        public string Id { get; set; }
    }
}
