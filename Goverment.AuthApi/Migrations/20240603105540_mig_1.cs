using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
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
                columns: new[] { "Id", "confirmtoken", "email", "firstname", "isresetpassword", "isverify", "lastname", "otpcreateddate", "otpcode", "passwordhash", "passwordsalt", "status" },
                values: new object[] { 1, null, "ilkinsuleymanov200@gmail.com", "Ilkin", false, true, "Suleymanov", null, null, new byte[] { 46, 106, 195, 255, 100, 166, 20, 131, 237, 62, 109, 123, 73, 73, 175, 103, 191, 26, 233, 155, 218, 74, 140, 212, 246, 8, 166, 203, 150, 25, 47, 216, 68, 191, 188, 202, 217, 53, 235, 41, 101, 235, 142, 49, 66, 164, 245, 81, 69, 252, 43, 61, 56, 215, 139, 66, 98, 209, 189, 171, 217, 154, 9, 212 }, new byte[] { 14, 137, 52, 113, 72, 45, 229, 139, 184, 37, 210, 128, 100, 173, 132, 108, 57, 124, 106, 36, 140, 62, 158, 171, 212, 121, 217, 14, 64, 40, 5, 172, 156, 134, 196, 134, 51, 37, 93, 23, 253, 249, 144, 224, 140, 160, 244, 253, 74, 103, 121, 120, 242, 159, 219, 217, 63, 78, 137, 192, 2, 117, 33, 170, 81, 34, 185, 13, 141, 108, 108, 199, 43, 35, 89, 1, 165, 20, 93, 247, 113, 248, 133, 111, 120, 145, 2, 109, 111, 191, 20, 100, 108, 198, 25, 228, 156, 5, 185, 77, 143, 219, 193, 217, 92, 161, 60, 161, 70, 251, 105, 184, 57, 17, 150, 227, 232, 164, 76, 136, 87, 20, 128, 182, 9, 88, 174, 153 }, false });

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
