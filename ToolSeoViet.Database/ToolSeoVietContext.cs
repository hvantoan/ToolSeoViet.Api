using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToolSeoViet.Database.Models;

namespace ToolSeoViet.Database {
    public partial class ToolSeoVietContext : DbContext {
        public DbSet<Permission> Permisstions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<User> Users { get; set; }

        //Content
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<SubTitle> SubTitles { get; set; }
        public DbSet<SearchContent> SearchContent { get; set; }
        public DbSet<SearchContentOnUser> SearchContentOnUsers { get; set; }

        public ToolSeoVietContext() {
        }

        public ToolSeoVietContext(DbContextOptions<ToolSeoVietContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server=.;Database=Test; Trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
