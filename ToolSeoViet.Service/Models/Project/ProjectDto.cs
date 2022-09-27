using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ToolSeoViet.Service.Models.Project {
    public partial class ProjectDto {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        public string Domain { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProjectDetailDto> ProjectDetails { get; set; }
    }

    public partial class ProjectDto {
        public static ProjectDto FromEntity(ToolSeoViet.Database.Models.Project entity) {
            if (entity == null) return default;

            return new ProjectDto {
                Id = entity.Id,
                Domain = entity.Domain,
                Name = entity.Name,
                ProjectDetails = entity.ProjectDetails?.Select(o => ProjectDetailDto.FromEntity(o)).ToList()
            };
        }
    }
}


