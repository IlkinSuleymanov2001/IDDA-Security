using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Method = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    otpcode = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    idtoken = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    otpcreateddate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    idtokenexpiredate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    isverify = table.Column<bool>(type: "boolean", maxLength: 20, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    passwordhash = table.Column<byte[]>(type: "bytea", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    createdtime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    modifiedtime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true)
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
                name: "UserResendOtpSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    tryotpcount = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    unblockdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    islock = table.Column<bool>(type: "boolean", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResendOtpSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserResendOtpSecurities_users_userid",
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
                columns: new[] { "Id", "createdtime", "email", "firstname", "idtoken", "idtokenexpiredate", "isverify", "modifiedtime", "otpcreateddate", "otpcode", "passwordhash", "PasswordSalt", "status" },
                values: new object[] { 1, null, "ilkinsuleymanov200@gmail.com", "Ilkin  Suleymanov", null, null, true, null, null, null, new byte[] { 19, 160, 45, 51, 160, 107, 180, 226, 4, 81, 66, 99, 85, 136, 250, 119, 80, 189, 41, 91, 33, 213, 39, 166, 174, 122, 129, 40, 76, 97, 157, 29, 1, 13, 166, 237, 39, 15, 164, 236, 183, 14, 59, 79, 102, 39, 160, 90, 114, 89, 225, 210, 193, 125, 175, 187, 82, 250, 231, 94, 146, 158, 13, 19 }, new byte[] { 239, 107, 69, 14, 141, 13, 2, 104, 189, 193, 155, 5, 21, 71, 133, 6, 41, 73, 56, 146, 212, 120, 170, 44, 132, 176, 105, 133, 6, 139, 103, 145, 12, 255, 127, 136, 232, 108, 130, 144, 48, 22, 141, 38, 237, 121, 82, 125, 114, 19, 92, 142, 180, 117, 94, 89, 180, 29, 29, 204, 123, 20, 158, 53, 230, 255, 143, 129, 127, 146, 220, 119, 163, 150, 240, 86, 120, 169, 193, 35, 75, 186, 11, 36, 51, 194, 15, 208, 96, 204, 3, 93, 23, 148, 14, 163, 21, 212, 91, 56, 83, 106, 152, 123, 87, 242, 210, 145, 151, 105, 67, 97, 203, 146, 91, 192, 113, 167, 132, 182, 95, 173, 123, 236, 213, 57, 156, 199 }, false });

            migrationBuilder.InsertData(
                table: "UserLoginSecurities",
                columns: new[] { "userid", "AccountBlockedTime", "AccountUnBlockedTime", "isBlock", "LoginRetryCount" },
                values: new object[] { 1, null, null, false, 0 });

            migrationBuilder.InsertData(
                table: "UserResendOtpSecurities",
                columns: new[] { "userid", "islock", "tryotpcount", "unblockdate" },
                values: new object[] { 1, false, 0, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_users_idtoken",
                table: "users",
                column: "idtoken",
                unique: true,
                filter: "\"idtoken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_otpcode",
                table: "users",
                column: "otpcode",
                unique: true,
                filter: "\"otpcode\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAudits");

            migrationBuilder.DropTable(
                name: "UserLoginSecurities");

            migrationBuilder.DropTable(
                name: "UserResendOtpSecurities");

            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
