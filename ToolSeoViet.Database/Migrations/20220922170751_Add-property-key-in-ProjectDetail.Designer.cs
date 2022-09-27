﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToolSeoViet.Database;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    [DbContext(typeof(ToolSeoVietContext))]
    [Migration("20220922170751_Add-property-key-in-ProjectDetail")]
    partial class AddpropertykeyinProjectDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ToolSeoViet.Database.Models.Heading", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Href")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("SearchContentId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("SearchContentId");

                    b.ToTable("Heading", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("ClaimName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Default")
                        .HasColumnType("bit");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ParentId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permission", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ec0f270b424249438540a16e9157c0c8",
                            ClaimName = "SEO",
                            Default = true,
                            DisplayName = "Quản lý",
                            IsActive = true,
                            ParentId = "",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "dc1c2ce584d74428b4e5241a5502787d",
                            ClaimName = "SEO.Setting",
                            Default = false,
                            DisplayName = "Cài đặt",
                            IsActive = true,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "296285809bac481890a454ea8aed6af4",
                            ClaimName = "SEO.Setting.User",
                            Default = false,
                            DisplayName = "Người dùng",
                            IsActive = true,
                            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "74e2235cc48d47529e080b62dc699b02",
                            ClaimName = "SEO.Setting.User.Save",
                            Default = false,
                            DisplayName = "Thêm mới và sửa",
                            IsActive = true,
                            ParentId = "296285809bac481890a454ea8aed6af4",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "98873832ebcb4d9fb12e9b21a187f12c",
                            ClaimName = "SEO.Setting.User.Reset",
                            Default = false,
                            DisplayName = "Đặt lại mật khẩu",
                            IsActive = true,
                            ParentId = "296285809bac481890a454ea8aed6af4",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "a8845d8773f345d9b572ef4ee04136cf",
                            ClaimName = "SEO.Project",
                            Default = true,
                            DisplayName = "Project",
                            IsActive = true,
                            ParentId = "296285809bac481890a454ea8aed6af4",
                            Type = "Web"
                        },
                        new
                        {
                            Id = "d6ee70dc6c7c468f8f35206085b1880f",
                            ClaimName = "SEO.Project.Save",
                            Default = false,
                            DisplayName = "Thêm mới và sửa",
                            IsActive = true,
                            ParentId = "a8845d8773f345d9b572ef4ee04136cf",
                            Type = "Web"
                        });
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.ProjectDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("BestPosition")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPosition")
                        .HasColumnType("int");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Url")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectDetail", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "469b14225a79448c93e4e780aa08f0cc",
                            Code = "admin",
                            Name = "Quản trị viên"
                        },
                        new
                        {
                            Id = "6ffa9fa20755486d9e317d447b652bd8",
                            Code = "user",
                            Name = "Người dùng"
                        });
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.RolePermission", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PermissionId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = "469b14225a79448c93e4e780aa08f0cc",
                            PermissionId = "ec0f270b424249438540a16e9157c0c8",
                            IsEnable = true
                        },
                        new
                        {
                            RoleId = "6ffa9fa20755486d9e317d447b652bd8",
                            PermissionId = "dc1c2ce584d74428b4e5241a5502787d",
                            IsEnable = true
                        });
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SearchContent", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SearchContent", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SLI", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SearchContentId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("SearchContentId");

                    b.ToTable("SLI", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SubTitle", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("HeadingId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeadingId");

                    b.ToTable("SubTitle", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Title", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("HeadingId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeadingId");

                    b.ToTable("Title", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "92dcba9b0bdd4f32a6170a1322472ead",
                            Avatar = "",
                            IsActive = true,
                            IsAdmin = true,
                            Name = "Hồ Văn Toàn",
                            Password = "CcW16ZwR+2SFn8AnpaN+dNakxXvQTI3btbcwpiugge2xYM4H2NfaAD0ZAnOcC4k8HnQLQBGLCpgCtggVfyopgg==",
                            RoleId = "469b14225a79448c93e4e780aa08f0cc",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.ViDictionary", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsMeaning")
                        .HasColumnType("bit");

                    b.Property<string>("Word")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ViDictionary", (string)null);
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Heading", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.SearchContent", "SearchContent")
                        .WithMany("Headings")
                        .HasForeignKey("SearchContentId");

                    b.Navigation("SearchContent");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Project", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.ProjectDetail", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.Project", "Project")
                        .WithMany("ProjectDetails")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.RolePermission", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolSeoViet.Database.Models.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SearchContent", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.User", "User")
                        .WithMany("SearchContents")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SLI", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.SearchContent", "SearchContent")
                        .WithMany("SLIs")
                        .HasForeignKey("SearchContentId");

                    b.Navigation("SearchContent");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SubTitle", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.Heading", "Heading")
                        .WithMany("SubTitles")
                        .HasForeignKey("HeadingId");

                    b.Navigation("Heading");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Title", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.Heading", "Heading")
                        .WithMany("Titles")
                        .HasForeignKey("HeadingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heading");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.User", b =>
                {
                    b.HasOne("ToolSeoViet.Database.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Heading", b =>
                {
                    b.Navigation("SubTitles");

                    b.Navigation("Titles");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Project", b =>
                {
                    b.Navigation("ProjectDetails");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.SearchContent", b =>
                {
                    b.Navigation("Headings");

                    b.Navigation("SLIs");
                });

            modelBuilder.Entity("ToolSeoViet.Database.Models.User", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("SearchContents");
                });
#pragma warning restore 612, 618
        }
    }
}
