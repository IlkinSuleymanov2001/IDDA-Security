using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class postinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    confirmtoken = table.Column<string>(type: "text", maxLength: 400, nullable: true),
                    otpcode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    otpcreateddate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    isverify = table.Column<bool>(type: "boolean", maxLength: 20, nullable: false),
                    passwordsalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    passwordhash = table.Column<byte[]>(type: "bytea", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false)
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
                    LoginRetryCount = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    isBlock = table.Column<bool>(type: "boolean", maxLength: 5, nullable: false),
                    AccountBlockedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccountUnBlockedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                values: new object[] { 1, null, "ilkinsuleymanov200@gmail.com", "Ilkin", true, "Suleymanov", null, null, new byte[] { 82, 210, 182, 200, 215, 234, 151, 186, 237, 69, 246, 136, 205, 145, 8, 148, 78, 251, 249, 101, 254, 55, 126, 229, 207, 128, 248, 179, 64, 156, 142, 120, 186, 53, 85, 165, 172, 11, 76, 226, 235, 250, 229, 231, 48, 71, 82, 174, 89, 196, 108, 173, 221, 238, 92, 116, 201, 143, 230, 136, 173, 165, 23, 152 }, new byte[] { 207, 138, 77, 63, 17, 241, 132, 211, 124, 12, 19, 239, 131, 227, 79, 138, 232, 17, 24, 147, 215, 8, 15, 222, 49, 65, 91, 20, 34, 135, 45, 174, 157, 129, 154, 5, 242, 207, 60, 250, 121, 203, 162, 75, 48, 11, 3, 113, 149, 231, 176, 234, 119, 130, 26, 241, 226, 153, 144, 192, 85, 111, 118, 206, 78, 240, 37, 216, 245, 56, 53, 93, 164, 252, 107, 239, 62, 243, 173, 144, 164, 88, 166, 34, 111, 160, 70, 130, 23, 17, 245, 168, 41, 49, 88, 178, 125, 62, 41, 120, 61, 133, 23, 91, 51, 196, 142, 159, 75, 137, 106, 169, 182, 195, 195, 142, 184, 121, 145, 192, 196, 71, 91, 92, 172, 143, 248, 206 }, false });

            migrationBuilder.InsertData(
                table: "UserLoginSecurities",
                columns: new[] { "userid", "AccountBlockedTime", "AccountUnBlockedTime", "isBlock", "LoginRetryCount" },
                values: new object[] { 1, null, null, false, 0 });

            migrationBuilder.InsertData(
                table: "userroles",
                columns: new[] { "roleid", "userid" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

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
