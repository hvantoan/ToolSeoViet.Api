using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class FixSearchContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchContentOnUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SearchContent",
                type: "nvarchar(32)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SearchContent_UserId",
                table: "SearchContent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchContent_User_UserId",
                table: "SearchContent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchContent_User_UserId",
                table: "SearchContent");

            migrationBuilder.DropIndex(
                name: "IX_SearchContent_UserId",
                table: "SearchContent");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SearchContent");

            migrationBuilder.CreateTable(
                name: "SearchContentOnUser",
                columns: table => new
                {
                    SearchContentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchContentOnUser", x => new { x.SearchContentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SearchContentOnUser_SearchContent_SearchContentId",
                        column: x => x.SearchContentId,
                        principalTable: "SearchContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchContentOnUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchContentOnUser_UserId",
                table: "SearchContentOnUser",
                column: "UserId");
        }
    }
}
