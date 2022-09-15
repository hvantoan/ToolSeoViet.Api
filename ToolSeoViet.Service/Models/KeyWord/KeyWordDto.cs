using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.KeyWord
{
    public partial class KeyWordDto
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public int BestPosition { get; set; }
        public string Url { get; set; }
        public string ProjectId { get; set; }
    }

    public partial class KeyWordDto
    {
        public static KeyWordDto FromEntity(ToolSeoViet.Database.Models.ProjectDetail entity)
        {
            if (entity == null) return default;

            return new KeyWordDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CurrentPosition = entity.CurrentPosition,
                BestPosition = entity.BestPosition,
                Url = entity.Url,
                ProjectId = entity.ProjectId,
            };
        }
    }
}
