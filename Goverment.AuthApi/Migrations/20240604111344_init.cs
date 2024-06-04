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
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                name: "UserLoginSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    LoginRetryCount = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    isBlock = table.Column<bool>(type: "bit", maxLength: 5, nullable: false),
                    AccountBlockedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountUnBlockedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserLoginSecurities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "confirmtoken", "email", "firstname", "isverify", "lastname", "otpcreateddate", "otpcode", "passwordhash", "passwordsalt", "status" },
                values: new object[] { 1, null, "ilkinsuleymanov200@gmail.com", "Ilkin", true, "Suleymanov", null, null, new byte[] { 14, 22, 72, 151, 84, 222, 95, 41, 250, 254, 227, 160, 11, 41, 52, 124, 224, 224, 140, 84, 158, 122, 51, 5, 198, 2, 75, 163, 239, 138, 13, 30, 33, 131, 230, 100, 179, 109, 245, 182, 125, 140, 43, 182, 57, 29, 12, 25, 132, 143, 25, 184, 70, 75, 213, 135, 213, 65, 37, 57, 192, 178, 15, 238 }, new byte[] { 81, 109, 140, 11, 78, 58, 58, 225, 11, 191, 76, 231, 237, 54, 27, 123, 255, 127, 57, 24, 54, 80, 68, 248, 50, 227, 236, 184, 178, 43, 208, 156, 246, 174, 62, 209, 116, 15, 137, 242, 111, 175, 155, 33, 120, 216, 52, 90, 134, 172, 211, 55, 30, 86, 75, 70, 197, 118, 96, 146, 229, 255, 251, 190, 69, 203, 140, 57, 173, 129, 53, 135, 190, 234, 208, 62, 189, 201, 238, 153, 171, 236, 242, 74, 181, 12, 226, 181, 80, 128, 19, 19, 65, 88, 14, 157, 125, 245, 58, 202, 232, 188, 226, 211, 216, 218, 31, 192, 183, 107, 68, 235, 20, 145, 93, 212, 80, 88, 105, 134, 60, 179, 94, 213, 148, 220, 81, 56 }, false });

            migrationBuilder.InsertData(
                table: "UserLoginSecurities",
                columns: new[] { "userid", "AccountBlockedTime", "AccountUnBlockedTime", "isBlock", "LoginRetryCount" },
                values: new object[] { 1, null, null, false, 0 });

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
                name: "UserLoginSecurities");

            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
