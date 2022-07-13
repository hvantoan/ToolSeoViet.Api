using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;


namespace ToolSeoViet.Database.Models {
    public partial class Heading {
        public string Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string SearchContentId { get; set; }
        public SearchContent SearchContent { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
        public virtual ICollection<SubTitle> SubTitles { get; set; }
    }

    public class HeadingConfig : IEntityTypeConfiguration<Heading> {

        public void Configure(EntityTypeBuilder<Heading> builder) {
            builder.ToTable(nameof(Heading));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Name).HasMaxLength(255);
            builder.Property(o => o.Href).HasMaxLength(Int32.MaxValue);

            // fk
            builder.HasMany(o => o.Titles).WithOne(o => o.Heading).HasForeignKey(o => o.HeadingId);
            builder.HasMany(o => o.SubTitles).WithOne(o => o.Heading).HasForeignKey(o => o.HeadingId);
            builder.HasOne(o => o.SearchContent).WithMany(o => o.Headings).HasForeignKey(o => o.SearchContentId);
        }
    }
}
