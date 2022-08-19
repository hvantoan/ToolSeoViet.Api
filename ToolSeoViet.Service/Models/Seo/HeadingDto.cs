using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class HeadingDto {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string Href { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public List<TitleDto> Titles { get; set; }
        public List<SubTitleDto> SubTitles { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TotalSLI { get; set; }
    }

    public partial class HeadingDto {

        public static HeadingDto FromEntity(ToolSeoViet.Database.Models.Heading entity,
            List<ToolSeoViet.Database.Models.Title> titles = null,
            List<ToolSeoViet.Database.Models.SubTitle> subTitles = null) {
            if (entity == null) return default;

            return new HeadingDto {
                Id = entity.Id,
                Name = entity.Name,
                Href = entity.Href,
                Position = entity.Position,
                Titles = titles?.Select(o=> TitleDto.FromEntity(o)).ToList(),
                SubTitles = subTitles?.Select(o=> SubTitleDto.FromEntity(o)).ToList(),
            };
        }
    }
}
