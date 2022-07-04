using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolSeoViet.Database.Migrations
{
    public partial class AddBaseRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { "469b14225a79448c93e4e780aa08f0cc", "admin", "Quản trị viên" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { "6ffa9fa20755486d9e317d447b652bd8", "user", "Người dùng" });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "IsEnable" },
                values: new object[] { "ec0f270b424249438540a16e9157c0c8", "469b14225a79448c93e4e780aa08f0cc", true });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId", "IsEnable" },
                values: new object[] { "dc1c2ce584d74428b4e5241a5502787d", "6ffa9fa20755486d9e317d447b652bd8", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "ec0f270b424249438540a16e9157c0c8", "469b14225a79448c93e4e780aa08f0cc" });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "dc1c2ce584d74428b4e5241a5502787d", "6ffa9fa20755486d9e317d447b652bd8" });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "469b14225a79448c93e4e780aa08f0cc");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "6ffa9fa20755486d9e317d447b652bd8");
        }
    }
}
