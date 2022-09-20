using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using ToolSeoViet.Service.Models.KeyWord;

namespace ToolSeoViet.Service.Models.Project {
    public partial class ProjectDto {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        public string Domain { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<KeyWordDto> KeyWords { get; set; }
    }

    public partial class ProjectDto {
        public static ProjectDto FromEntity(ToolSeoViet.Database.Models.Project entity) {
            if (entity == null) return default;

            return new ProjectDto {
                Id = entity.Id,
                Domain = entity.Domain,
                Name = entity.Name,
                KeyWords = entity.ProjectDetails?.Select(o => KeyWordDto.FromEntity(o)).ToList()
            };
        }
    }
}


