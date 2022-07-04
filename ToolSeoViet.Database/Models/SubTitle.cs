using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class SubTitle {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TitleId { get; set; }
        public Title Title { get; set; }
    }

    public class SubTitleConfig : IEntityTypeConfiguration<SubTitle> {

        public void Configure(EntityTypeBuilder<SubTitle> builder) {
            builder.ToTable(nameof(SubTitle));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.TitleId).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
            // fk
            builder.HasOne(o => o.Title).WithMany(o => o.SubTitles).HasForeignKey(o => o.TitleId);

        }
    }
}
