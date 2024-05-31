using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    confirmtoken = table.Column<string>(type: "text", maxLength: 400, nullable: true),
                    otpcode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    isresetpassword = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    otpcreateddate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    isverify = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    passwordsalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordhash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    roleid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userroles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "FK_userroles_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userroles_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "USER" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "confirmtoken", "email", "firstname", "isresetpassword", "isverify", "lastname", "otpcreateddate", "otpcode", "passwordhash", "passwordsalt", "status" },
                values: new object[] { 1, null, "ilkinsuleymanov200@gmail.com", "Ilkin", false, true, "Suleymanov", null, null, new byte[] { 157, 15, 250, 172, 26, 4, 34, 171, 163, 18, 10, 115, 179, 238, 186, 0, 127, 188, 145, 35, 67, 88, 155, 183, 126, 215, 233, 174, 164, 110, 41, 24, 156, 71, 4, 110, 0, 29, 75, 191, 62, 129, 18, 65, 166, 184, 6, 176, 92, 5, 0, 51, 75, 47, 237, 2, 143, 49, 74, 197, 76, 200, 115, 232 }, new byte[] { 61, 1, 227, 172, 100, 125, 89, 13, 76, 92, 21, 247, 144, 49, 163, 244, 63, 84, 167, 236, 217, 172, 154, 74, 75, 60, 85, 221, 12, 229, 233, 146, 197, 147, 28, 179, 73, 113, 73, 218, 185, 65, 102, 71, 134, 224, 194, 131, 91, 184, 193, 91, 122, 221, 26, 96, 55, 170, 246, 124, 99, 253, 11, 87, 31, 197, 210, 27, 214, 87, 113, 81, 203, 52, 238, 186, 214, 84, 41, 183, 100, 88, 76, 162, 106, 94, 204, 212, 4, 31, 90, 181, 149, 61, 231, 69, 245, 18, 7, 34, 18, 126, 55, 21, 3, 245, 57, 229, 62, 71, 97, 206, 48, 12, 238, 79, 14, 120, 161, 222, 65, 39, 254, 106, 69, 55, 172, 63 }, false });

            migrationBuilder.InsertData(
                table: "userroles",
                columns: new[] { "roleid", "userid" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_userroles_roleid",
                table: "userroles",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
