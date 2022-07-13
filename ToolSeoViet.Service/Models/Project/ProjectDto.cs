using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Project
{
    public partial class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
    }

    public partial class ProjectDto
    {

        public static ProjectDto FromEntity(ToolSeoViet.Database.Models.Project entity)
        {
            if (entity == null) return default;

            return new ProjectDto
            {
                Id = entity.Id, Domain=entity.Domain, Name= entity.Name,
            };
        }
    }
}


