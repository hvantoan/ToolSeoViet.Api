using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class SearchContentOnUser {
        public string SearchContentId { get; set; }
        public string UserId { get; set; }

        public SearchContent SearchContent { get; set; }
        public User User { get; set; }
    }

    public class SearchContentOnUserConfig : IEntityTypeConfiguration<SearchContentOnUser> {

        public void Configure(EntityTypeBuilder<SearchContentOnUser> builder) {
            builder.ToTable(nameof(SearchContentOnUser));
            builder.HasKey(o => new { o.SearchContentId, o.UserId });
            builder.Property(o => o.SearchContentId).HasMaxLength(32);
            builder.Property(o => o.UserId).HasMaxLength(32);
            // fk
            builder.HasOne(o => o.User).WithMany(o => o.SearchContentOnUsers).HasForeignKey(o => o.UserId);
            builder.HasOne(o => o.SearchContent).WithMany(o => o.SearchContentOnUsers).HasForeignKey(o => o.SearchContentId);
        }
    }



}
