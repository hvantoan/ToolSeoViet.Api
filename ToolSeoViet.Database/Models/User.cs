﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
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
        public ICollection<SearchContent> SearchContents { get; set; }
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
            builder.HasMany(o => o.SearchContents).WithOne(o => o.User).HasForeignKey(o => o.UserId);

            builder.HasData(new User() {
                Avatar = "",
                Id = "92dcba9b0bdd4f32a6170a1322472ead",
                IsActive = true,
                IsAdmin = true,
                Name = "Hồ Văn Toàn",
                Password = "CcW16ZwR+2SFn8AnpaN+dNakxXvQTI3btbcwpiugge2xYM4H2NfaAD0ZAnOcC4k8HnQLQBGLCpgCtggVfyopgg==",
                RoleId = "469b14225a79448c93e4e780aa08f0cc",
                Username = "admin"
            });

            builder.HasMany(o=>o.Projects).WithOne(o=>o.User).HasForeignKey(o => o.UserId);
        }
    }
}