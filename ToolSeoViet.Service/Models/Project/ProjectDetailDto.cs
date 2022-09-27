namespace ToolSeoViet.Service.Models.Project {
    public partial class ProjectDetailDto {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; } = 0;
        public int BestPosition { get; set; } = 0;
        public string Url { get; set; } = "";
        public string ProjectId { get; set; }
    }

    public partial class ProjectDetailDto {
        public static ProjectDetailDto FromEntity(ToolSeoViet.Database.Models.ProjectDetail entity) {
            if (entity == null) return default;

            return new ProjectDetailDto {
                Id = entity.Id,
                Key = entity.Key,
                Name = entity.Name,
                CurrentPosition = entity.CurrentPosition,
                BestPosition = entity.BestPosition,
                Url = entity.Url,
                ProjectId = entity.ProjectId,
            };
        }
    }
}
