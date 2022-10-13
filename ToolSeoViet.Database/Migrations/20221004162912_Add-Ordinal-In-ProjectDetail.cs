using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class AddOrdinalInProjectDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordinal",
                table: "ProjectDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordinal",
                table: "ProjectDetail");
        }
    }
}
