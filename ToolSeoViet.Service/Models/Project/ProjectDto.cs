using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Models.KeyWord;

namespace ToolSeoViet.Service.Models.Project
{
    public partial class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Domain { get; set; }
        public List<KeyWordDto> KeyWords { get; set; }
    }

    public partial class ProjectDto
    {
        public static ProjectDto FromEntity(ToolSeoViet.Database.Models.Project entity,
            List<ToolSeoViet.Database.Models.ProjectDetail> keyWords = null)
        {
            if (entity == null) return default;

            return new ProjectDto
            {
                Id = entity.Id,
                Domain = entity.Domain,
                Name = entity.Name,
                KeyWords = keyWords?.Select(o=> KeyWordDto.FromEntity(o)).ToList() ?? new()
            };
        }
    }
}


