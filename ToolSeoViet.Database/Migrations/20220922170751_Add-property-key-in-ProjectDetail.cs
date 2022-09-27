using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class AddpropertykeyinProjectDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "ProjectDetail",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "ProjectDetail");
        }
    }
}
