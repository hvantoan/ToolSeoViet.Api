using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class SearchContent {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public virtual ICollection<Heading> Headings { get; set; }
        public virtual ICollection<SLI> SLIs { get; set; }

    }

    public class SearchContentConfig : IEntityTypeConfiguration<SearchContent> {

        public void Configure(EntityTypeBuilder<SearchContent> builder) {
            builder.ToTable(nameof(SearchContent));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(Int32.MaxValue).IsRequired();
            builder.Property(o => o.UserId).HasMaxLength(32);
            builder.Property(o => o.DateCreated);

            // fk
            builder.HasMany(o => o.Headings).WithOne(o => o.SearchContent).HasForeignKey(o => o.SearchContentId);
            builder.HasMany(o => o.SLIs).WithOne(o => o.SearchContent).HasForeignKey(o => o.SearchContentId);
            builder.HasOne(o => o.User).WithMany(o => o.SearchContents).HasForeignKey(o => o.UserId);
        }
    }
}
