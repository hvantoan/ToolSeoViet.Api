using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Project
{
    public partial class GetProjectDto
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public int Index { get; set; }
        public string Url { get; set; }
        //public string ProjectId { get; set; }
    }
    public partial class GetProjectDto
    {
        public static GetProjectDto FromEntity(ToolSeoViet.Database.Models.KeyWord entity)
        {
            if (entity == null) return default;

            return new GetProjectDto
            {
                Id = entity.Id,
                Key = entity.Key,
                Index = entity.Index,
                Url = entity.Url,
            };
        }
    }
}
