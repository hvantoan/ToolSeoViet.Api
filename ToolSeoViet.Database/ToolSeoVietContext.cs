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
        public DbSet<SearchContent> SearchContents { get; set; }
        public DbSet<SLI> SLIs { get; set; }
        public DbSet<ViDictionary> ViDictionaries { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectDetail> ProjectDetails { get; set; }

        public ToolSeoVietContext() {
        }

        public ToolSeoVietContext(DbContextOptions<ToolSeoVietContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("Server=ec2-44-209-57-4.compute-1.amazonaws.com;Port=5432;Database=d6fbbhmk7vpd6t;User Id=leemwdukezeali;Password=780351960e6d24ef88842fad33ed7f3486f213b60dae01aeed022547c53029fc;sslmode=Require;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
