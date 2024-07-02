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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsVerify = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
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
                    modifiedtime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    deletedtime = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", maxLength: 50, nullable: false)
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
                name: "UserOtpSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tryotpcount = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    UserId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtpSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserOtpSecurities_users_UserId1",
                        column: x => x.UserId1,
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
                columns: new[] { "Id", "createdtime", "deletedtime", "email", "firstname", "idtoken", "idtokenexpiredate", "isdelete", "isverify", "modifiedtime", "otpcreateddate", "otpcode", "passwordhash", "PasswordSalt", "status" },
                values: new object[] { 1, null, null, "ilkinsuleymanov200@gmail.com", "Ilkin  Suleymanov", null, null, false, true, null, null, null, new byte[] { 143, 201, 137, 145, 212, 183, 124, 157, 135, 158, 26, 246, 106, 23, 57, 208, 189, 90, 50, 182, 218, 157, 126, 53, 149, 13, 79, 80, 7, 108, 189, 170, 148, 62, 20, 194, 139, 250, 73, 115, 27, 240, 9, 21, 80, 96, 51, 212, 220, 100, 134, 89, 7, 252, 21, 200, 198, 240, 199, 37, 144, 13, 134, 57 }, new byte[] { 249, 205, 91, 201, 197, 148, 176, 253, 165, 43, 233, 165, 103, 141, 102, 190, 52, 43, 190, 194, 57, 1, 31, 85, 135, 182, 189, 62, 239, 111, 239, 209, 116, 96, 106, 146, 93, 102, 128, 12, 11, 239, 16, 32, 13, 9, 175, 58, 137, 35, 224, 253, 234, 150, 141, 227, 77, 205, 40, 253, 107, 16, 250, 87, 52, 202, 32, 221, 72, 31, 252, 230, 236, 255, 199, 171, 136, 93, 202, 151, 99, 42, 199, 39, 98, 130, 219, 20, 203, 78, 188, 40, 120, 225, 244, 10, 215, 85, 160, 34, 186, 213, 155, 250, 95, 108, 151, 207, 83, 204, 213, 72, 77, 237, 253, 219, 239, 195, 226, 186, 215, 114, 237, 108, 0, 61, 181, 16 }, false });

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
                name: "IX_UserOtpSecurities_UserId1",
                table: "UserOtpSecurities",
                column: "UserId1");

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
                name: "UserAudits");

            migrationBuilder.DropTable(
                name: "UserLoginSecurities");

            migrationBuilder.DropTable(
                name: "UserOtpSecurities");

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
