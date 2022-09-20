using Newtonsoft.Json;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class SubTitleDto {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
    }

    public partial class SubTitleDto {

        public static SubTitleDto FromEntity(ToolSeoViet.Database.Models.SubTitle entity) {
            if (entity == null) return default;

            return new SubTitleDto {
                Id = entity.Id,
                Name = entity.Name,
                Position = entity.Position,
            };
        }
    }
}
