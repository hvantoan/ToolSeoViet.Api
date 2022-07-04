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

        public virtual ICollection<Heading> Headings { get; set; }
        public virtual ICollection<SearchContentOnUser> SearchContentOnUsers { get; set; }
    }

    public class SearchContentConfig : IEntityTypeConfiguration<SearchContent> {

        public void Configure(EntityTypeBuilder<SearchContent> builder) {
            builder.ToTable(nameof(SearchContent));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();

            // fk
            builder.HasMany(o => o.Headings).WithOne(o => o.SearchContent).HasForeignKey(o => o.SearchContentId);
            builder.HasMany(o => o.SearchContentOnUsers).WithOne(o => o.SearchContent).HasForeignKey(o => o.SearchContentId);
        }
    }
}
