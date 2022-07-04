using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class Title {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HeadingId { get; set; }
        public Heading Heading { get; set; }

        public virtual ICollection<SubTitle> SubTitles { get; set; }
    }
    public class TitleConfig : IEntityTypeConfiguration<Title> {

        public void Configure(EntityTypeBuilder<Title> builder) {
            builder.ToTable(nameof(User));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.HeadingId).HasMaxLength(32).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(32);


            // fk
            builder.HasOne(o => o.Heading).WithMany(o => o.Titles).HasForeignKey(o => o.HeadingId);

        }
    }
}
