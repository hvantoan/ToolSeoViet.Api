using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ToolSeoViet.Database.Models {

    public partial class User {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public Role Role { get; set; }
        public virtual ICollection<SearchContentOnUser> SearchContentOnUsers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class UserConfig : IEntityTypeConfiguration<User> {

        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable(nameof(User));

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasMaxLength(32);
            builder.Property(o => o.RoleId).HasMaxLength(32);

            builder.Property(o => o.Name).HasMaxLength(32);
            builder.Property(o => o.Avatar);

            builder.Property(o => o.Username).IsRequired();
            builder.Property(o => o.Password).IsRequired();

            // fk
            builder.HasOne(o => o.Role).WithMany(o => o.Users).HasForeignKey(o => o.RoleId);
            builder.HasMany(o => o.SearchContentOnUsers).WithOne(o => o.User).HasForeignKey(o => o.UserId);
            builder.HasMany(o=>o.Projects).WithOne(o=>o.User).HasForeignKey(o => o.UserId);
        }
    }
}