using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOtpSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    tryotpcount = table.Column<int>(type: "integer", maxLength: 2, nullable: true),
                    unblockdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    islock = table.Column<bool>(type: "boolean", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtpSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserOtpSecurities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserOtpSecurities",
                columns: new[] { "userid", "islock", "tryotpcount", "unblockdate" },
                values: new object[] { 1, false, null, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 114, 203, 123, 201, 116, 6, 211, 220, 251, 236, 164, 137, 79, 72, 219, 136, 25, 169, 18, 70, 124, 157, 82, 151, 157, 43, 225, 199, 22, 39, 28, 5, 67, 250, 119, 158, 176, 252, 42, 75, 192, 45, 120, 46, 239, 10, 193, 117, 91, 217, 143, 210, 61, 248, 189, 18, 27, 132, 7, 72, 197, 57, 1, 51 }, new byte[] { 201, 59, 178, 154, 64, 217, 75, 61, 14, 99, 182, 91, 172, 184, 114, 161, 165, 139, 4, 141, 205, 192, 88, 51, 151, 130, 87, 8, 37, 93, 142, 8, 129, 8, 180, 242, 85, 53, 158, 220, 164, 117, 245, 16, 205, 150, 161, 35, 99, 211, 143, 197, 10, 134, 112, 9, 110, 205, 220, 235, 59, 234, 111, 109, 112, 20, 141, 213, 207, 236, 205, 13, 142, 71, 227, 170, 146, 132, 247, 250, 9, 48, 203, 22, 67, 219, 150, 120, 89, 105, 117, 242, 137, 183, 23, 85, 132, 45, 162, 199, 244, 18, 243, 170, 248, 188, 14, 141, 86, 114, 56, 41, 173, 137, 143, 19, 210, 249, 50, 64, 37, 229, 117, 133, 84, 182, 68, 253 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOtpSecurities");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 154, 170, 24, 18, 167, 144, 94, 0, 16, 122, 187, 172, 64, 73, 4, 96, 79, 251, 69, 98, 17, 123, 43, 31, 30, 83, 167, 37, 143, 50, 38, 70, 225, 24, 24, 112, 26, 232, 17, 218, 100, 122, 199, 154, 96, 98, 156, 9, 203, 243, 111, 143, 70, 135, 161, 6, 187, 252, 188, 211, 206, 36, 112, 20 }, new byte[] { 136, 116, 148, 176, 20, 46, 149, 223, 165, 183, 115, 109, 137, 56, 193, 250, 73, 21, 124, 120, 87, 200, 94, 36, 48, 156, 81, 99, 209, 82, 223, 145, 57, 213, 127, 144, 83, 40, 196, 166, 174, 53, 68, 120, 245, 108, 54, 12, 51, 205, 116, 58, 75, 42, 226, 140, 250, 114, 106, 100, 118, 204, 133, 68, 131, 245, 138, 141, 3, 147, 160, 114, 173, 17, 230, 236, 18, 11, 146, 169, 44, 151, 99, 22, 88, 112, 74, 232, 52, 161, 233, 247, 210, 24, 120, 45, 126, 25, 233, 123, 6, 54, 101, 187, 35, 106, 23, 131, 76, 55, 208, 168, 212, 34, 7, 22, 81, 35, 240, 181, 64, 121, 1, 178, 99, 87, 228, 93 } });
        }
    }
}
