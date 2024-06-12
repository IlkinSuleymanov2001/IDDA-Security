using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    otpcode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    otpcreateddate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    isverify = table.Column<bool>(type: "boolean", maxLength: 20, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
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
                columns: new[] { "Id", "email", "firstname", "isverify", "lastname", "otpcreateddate", "otpcode", "passwordhash", "PasswordSalt", "status" },
                values: new object[] { 1, "ilkinsuleymanov200@gmail.com", "Ilkin", true, "Suleymanov", null, null, new byte[] { 154, 170, 24, 18, 167, 144, 94, 0, 16, 122, 187, 172, 64, 73, 4, 96, 79, 251, 69, 98, 17, 123, 43, 31, 30, 83, 167, 37, 143, 50, 38, 70, 225, 24, 24, 112, 26, 232, 17, 218, 100, 122, 199, 154, 96, 98, 156, 9, 203, 243, 111, 143, 70, 135, 161, 6, 187, 252, 188, 211, 206, 36, 112, 20 }, new byte[] { 136, 116, 148, 176, 20, 46, 149, 223, 165, 183, 115, 109, 137, 56, 193, 250, 73, 21, 124, 120, 87, 200, 94, 36, 48, 156, 81, 99, 209, 82, 223, 145, 57, 213, 127, 144, 83, 40, 196, 166, 174, 53, 68, 120, 245, 108, 54, 12, 51, 205, 116, 58, 75, 42, 226, 140, 250, 114, 106, 100, 118, 204, 133, 68, 131, 245, 138, 141, 3, 147, 160, 114, 173, 17, 230, 236, 18, 11, 146, 169, 44, 151, 99, 22, 88, 112, 74, 232, 52, 161, 233, 247, 210, 24, 120, 45, 126, 25, 233, 123, 6, 54, 101, 187, 35, 106, 23, 131, 76, 55, 208, 168, 212, 34, 7, 22, 81, 35, 240, 181, 64, 121, 1, 178, 99, 87, 228, 93 }, false });

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
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userroles_roleid",
                table: "userroles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
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
