using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class UpdateViDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ViDictionary_Word",
                table: "ViDictionary",
                column: "Word",
                unique: true,
                filter: "[Word] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ViDictionary_Word",
                table: "ViDictionary");
        }
    }
}
