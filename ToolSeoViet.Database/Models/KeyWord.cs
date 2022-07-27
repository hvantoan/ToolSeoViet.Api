using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToolSeoViet.Database.Models
{
    public partial class KeyWord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentPosition { get; set; }
        public int BestPosition { get; set; }
        public string Url { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
    public class KeyWordConfig : IEntityTypeConfiguration<KeyWord>
    {
        public void Configure(EntityTypeBuilder<KeyWord> builder)
        {
            builder.ToTable(nameof(KeyWord));
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255);
            builder.Property(o => o.CurrentPosition);
            builder.Property(o => o.BestPosition);
            builder.Property(o => o.Url).HasMaxLength(Int32.MaxValue);
            builder.Property(o => o.ProjectId).HasMaxLength(32).IsRequired();
            //fk
            builder.HasOne(o => o.Project).WithMany(o => o.KeyWords).HasForeignKey(o => o.ProjectId);
        }
    }
}
