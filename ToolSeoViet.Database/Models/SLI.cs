using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class SLI {
        public string Id { get; set; }

        public string SearchContentId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public SearchContent SearchContent { get; set; }
    }

    public class SLIConfig : IEntityTypeConfiguration<SLI> {

        public void Configure(EntityTypeBuilder<SLI> builder) {
            builder.ToTable(nameof(SLI));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.SearchContentId).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();

            // fk
            builder.HasOne(o => o.SearchContent).WithMany(o => o.SLIs).HasForeignKey(o => o.SearchContentId);
        }
    }
}
