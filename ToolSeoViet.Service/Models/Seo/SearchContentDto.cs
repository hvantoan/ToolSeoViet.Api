using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class SearchContentDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SearchSLIKey> SLI { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public List<HeadingDto> Headings { get; set; }
    }

    public partial class SearchContentDto {
        public static SearchContentDto FromEntity(ToolSeoViet.Database.Models.SearchContent entity,
            List<ToolSeoViet.Database.Models.Heading> headings = null) {
            if (entity == null) return default;

            return new SearchContentDto {
                Id = entity.Id,
                Name = entity.Name,
                DateCreated = entity.DateCreated,
                Headings = headings?.Select(o => HeadingDto.FromEntity(o)).ToList(),
            };
        }
    }
}
