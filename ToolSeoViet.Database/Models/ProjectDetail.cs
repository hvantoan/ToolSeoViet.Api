using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ToolSeoViet.Database.Models {
    public partial class ProjectDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; } = 0;
        public int BestPosition { get; set; } = 0;
        public string Url { get; set; } = "";
        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
    public class KeyWordConfig : IEntityTypeConfiguration<ProjectDetail>
    {
        public void Configure(EntityTypeBuilder<ProjectDetail> builder)
        {
            builder.ToTable(nameof(ProjectDetail));
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(255);
            builder.Property(o => o.CurrentPosition);
            builder.Property(o => o.BestPosition);
            builder.Property(o => o.Url).HasMaxLength(Int32.MaxValue);
            builder.Property(o => o.ProjectId).HasMaxLength(32).IsRequired();
            //fk
            builder.HasOne(o => o.Project).WithMany(o => o.ProjectDetails).HasForeignKey(o => o.ProjectId);
        }
    }
}
