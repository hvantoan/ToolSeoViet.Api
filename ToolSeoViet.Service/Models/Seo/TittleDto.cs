using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class TitleDto {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
    }

    public partial class TitleDto {
        public static TitleDto FromEntity(ToolSeoViet.Database.Models.Title entity) {
            if (entity == null) return default;

            return new TitleDto {
                Id = entity.Id,
                Name = entity.Name,
                Position = entity.Position,
            };
        }
    }
}
