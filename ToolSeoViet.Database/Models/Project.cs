using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ToolSeoViet.Database.Models
{
    public partial class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<KeyWord> KeyWords { get; set; }
    }
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
            builder.Property(o => o.Domain).HasMaxLength(255).IsRequired();
            builder.Property(o => o.UserId).HasMaxLength(32).IsRequired();

            //fk
            builder.HasMany(o => o.KeyWords).WithOne(o => o.Project).HasForeignKey(o => o.ProjectId);
            builder.HasOne(o=>o.User).WithMany(o=>o.Projects).HasForeignKey(o => o.UserId);
        }
    }
}
