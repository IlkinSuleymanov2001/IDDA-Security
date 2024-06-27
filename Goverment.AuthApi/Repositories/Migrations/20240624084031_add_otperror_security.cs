using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class add_otperror_security : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOtpSecurities",
                keyColumn: "userid",
                keyValue: 1);

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

            migrationBuilder.InsertData(
                table: "UserResendOtpSecurities",
                columns: new[] { "userid", "islock", "tryotpcount", "unblockdate" },
                values: new object[] { 1, false, 0, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 162, 53, 209, 168, 123, 242, 16, 239, 38, 172, 218, 20, 222, 88, 226, 32, 251, 99, 92, 161, 92, 79, 168, 130, 204, 13, 37, 93, 133, 5, 102, 66, 146, 227, 71, 239, 168, 20, 129, 170, 37, 55, 248, 94, 94, 165, 240, 247, 248, 214, 171, 55, 46, 134, 160, 143, 116, 64, 198, 62, 251, 107, 147, 240 }, new byte[] { 128, 56, 238, 53, 131, 206, 90, 227, 154, 128, 7, 74, 105, 22, 102, 209, 183, 186, 57, 174, 20, 11, 138, 56, 124, 88, 203, 32, 139, 121, 111, 18, 20, 89, 23, 131, 211, 17, 30, 7, 30, 42, 102, 34, 114, 45, 119, 75, 82, 82, 253, 133, 35, 0, 253, 130, 2, 154, 93, 201, 215, 209, 126, 89, 2, 231, 128, 177, 83, 6, 57, 123, 179, 231, 54, 228, 11, 175, 217, 172, 194, 110, 188, 5, 244, 188, 224, 136, 174, 56, 16, 225, 160, 176, 165, 82, 74, 20, 188, 152, 216, 110, 179, 244, 23, 50, 208, 163, 253, 14, 147, 0, 163, 198, 181, 255, 249, 90, 165, 158, 147, 187, 122, 88, 121, 220, 41, 231 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserResendOtpSecurities");

            migrationBuilder.InsertData(
                table: "UserOtpSecurities",
                columns: new[] { "userid", "islock", "tryotpcount", "unblockdate" },
                values: new object[] { 1, false, 0, null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 39, 146, 41, 172, 151, 202, 82, 73, 207, 95, 135, 206, 66, 231, 186, 17, 132, 233, 185, 136, 54, 182, 204, 223, 163, 157, 216, 160, 46, 119, 117, 245, 192, 21, 127, 74, 92, 200, 232, 5, 192, 17, 246, 26, 52, 222, 85, 245, 153, 67, 235, 179, 149, 249, 68, 59, 109, 156, 223, 82, 220, 45, 156, 64 }, new byte[] { 54, 62, 145, 43, 198, 210, 194, 58, 133, 213, 110, 132, 192, 150, 79, 38, 2, 186, 47, 152, 222, 111, 90, 190, 96, 124, 168, 169, 93, 248, 124, 16, 5, 120, 24, 45, 91, 30, 176, 135, 215, 164, 134, 6, 153, 170, 22, 47, 213, 128, 58, 254, 222, 87, 229, 74, 47, 62, 94, 96, 179, 50, 99, 211, 28, 216, 100, 60, 4, 80, 37, 179, 60, 195, 42, 71, 16, 165, 115, 104, 245, 20, 77, 186, 159, 77, 96, 244, 109, 88, 3, 216, 191, 103, 93, 88, 39, 229, 61, 221, 55, 134, 73, 207, 117, 253, 168, 38, 170, 188, 7, 24, 216, 192, 58, 223, 14, 135, 188, 93, 123, 176, 47, 225, 230, 142, 95, 62 } });
        }
    }
}
