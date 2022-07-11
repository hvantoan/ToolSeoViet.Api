using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ClaimName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchContent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViDictionary",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Word = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsMeaning = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViDictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Heading",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Href = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    SearchContentId = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heading_SearchContent_SearchContentId",
                        column: x => x.SearchContentId,
                        principalTable: "SearchContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SLI",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SearchContentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLI_SearchContent_SearchContentId",
                        column: x => x.SearchContentId,
                        principalTable: "SearchContent",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "SubTitle",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HeadingId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTitle_Heading_HeadingId",
                        column: x => x.HeadingId,
                        principalTable: "Heading",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    HeadingId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Title_Heading_HeadingId",
                        column: x => x.HeadingId,
                        principalTable: "Heading",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "ClaimName", "Default", "DisplayName", "IsActive", "ParentId", "Type" },
                values: new object[,]
                {
                    { "296285809bac481890a454ea8aed6af4", "SEO.Setting.User", false, "Người dùng", true, "dc1c2ce584d74428b4e5241a5502787d", "Web" },
                    { "74e2235cc48d47529e080b62dc699b02", "SEO.Setting.User.Save", false, "Thêm mới và sửa", true, "296285809bac481890a454ea8aed6af4", "Web" },
                    { "98873832ebcb4d9fb12e9b21a187f12c", "SEO.Setting.User.Reset", false, "Đặt lại mật khẩu", true, "296285809bac481890a454ea8aed6af4", "Web" },
                    { "a8845d8773f345d9b572ef4ee04136cf", "SEO.Project", true, "Project", true, "296285809bac481890a454ea8aed6af4", "Web" },
                    { "d6ee70dc6c7c468f8f35206085b1880f", "SEO.Project.Save", false, "Thêm mới và sửa", true, "a8845d8773f345d9b572ef4ee04136cf", "Web" },
                    { "dc1c2ce584d74428b4e5241a5502787d", "SEO.Setting", false, "Cài đặt", true, "ec0f270b424249438540a16e9157c0c8", "Web" },
                    { "ec0f270b424249438540a16e9157c0c8", "SEO", true, "Quản lý", true, "", "Web" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { "469b14225a79448c93e4e780aa08f0cc", "admin", "Quản trị viên" },
                    { "6ffa9fa20755486d9e317d447b652bd8", "user", "Người dùng" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "IsEnable" },
                values: new object[] { "ec0f270b424249438540a16e9157c0c8", "469b14225a79448c93e4e780aa08f0cc", true });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "IsEnable" },
                values: new object[] { "dc1c2ce584d74428b4e5241a5502787d", "6ffa9fa20755486d9e317d447b652bd8", true });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "IsActive", "IsAdmin", "Name", "Password", "RoleId", "Username" },
                values: new object[] { "92dcba9b0bdd4f32a6170a1322472ead", "", true, true, "Hồ Văn Toàn", "CcW16ZwR+2SFn8AnpaN+dNakxXvQTI3btbcwpiugge2xYM4H2NfaAD0ZAnOcC4k8HnQLQBGLCpgCtggVfyopgg==", "469b14225a79448c93e4e780aa08f0cc", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Heading_SearchContentId",
                table: "Heading",
                column: "SearchContentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchContentOnUser_UserId",
                table: "SearchContentOnUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SLI_SearchContentId",
                table: "SLI",
                column: "SearchContentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTitle_HeadingId",
                table: "SubTitle",
                column: "HeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_Title_HeadingId",
                table: "Title",
                column: "HeadingId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SearchContentOnUser");

            migrationBuilder.DropTable(
                name: "SLI");

            migrationBuilder.DropTable(
                name: "SubTitle");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropTable(
                name: "ViDictionary");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Heading");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "SearchContent");
        }
    }
}
