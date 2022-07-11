using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToolSeoViet.Database.Models {
    public partial class ViDictionary {
        public string Id { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }
        public bool IsMeaning { get; set; }
    }

    public class ViDictionaryConfig : IEntityTypeConfiguration<ViDictionary> {

        public void Configure(EntityTypeBuilder<ViDictionary> builder) {
            builder.ToTable(nameof(ViDictionary));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.Word).HasMaxLength(255);
            builder.Property(o => o.Description).HasMaxLength(255);

            // fk
        }
    }


}
