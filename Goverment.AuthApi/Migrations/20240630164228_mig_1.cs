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
                name: "UserAudits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    otpcode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    idtoken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    otpcreateddate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    idtokenexpiredate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    isverify = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
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
                name: "UserOtpSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tryotpcount = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: false)
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
                    tryotpcount = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    unblockdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    islock = table.Column<bool>(type: "bit", maxLength: 5, nullable: false)
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
                columns: new[] { "Id", "email", "firstname", "idtoken", "idtokenexpiredate", "isverify", "otpcreateddate", "otpcode", "passwordhash", "PasswordSalt", "status" },
                values: new object[] { 1, "ilkinsuleymanov200@gmail.com", "Ilkin  Suleymanov", null, null, true, null, null, new byte[] { 94, 118, 240, 62, 250, 97, 104, 91, 212, 90, 127, 83, 142, 1, 180, 159, 31, 80, 223, 60, 115, 247, 166, 157, 166, 165, 205, 145, 151, 143, 20, 43, 57, 120, 152, 34, 44, 251, 75, 96, 205, 135, 235, 56, 254, 103, 212, 99, 243, 252, 17, 68, 238, 139, 244, 228, 194, 109, 246, 172, 200, 53, 147, 106 }, new byte[] { 178, 129, 14, 198, 30, 152, 76, 227, 136, 147, 56, 16, 186, 207, 78, 211, 254, 245, 78, 104, 98, 39, 207, 99, 78, 181, 223, 72, 154, 42, 57, 243, 38, 122, 160, 152, 7, 94, 77, 91, 216, 214, 155, 188, 123, 246, 176, 114, 4, 58, 247, 203, 243, 6, 107, 126, 224, 130, 237, 64, 55, 152, 197, 232, 164, 55, 6, 195, 10, 88, 114, 26, 43, 170, 233, 60, 93, 145, 120, 210, 180, 144, 180, 58, 108, 166, 85, 27, 145, 152, 117, 114, 147, 119, 1, 17, 170, 65, 150, 7, 137, 255, 79, 120, 26, 20, 105, 197, 119, 67, 246, 249, 70, 59, 45, 115, 223, 218, 183, 105, 161, 87, 227, 64, 225, 250, 118, 23 }, false });

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
