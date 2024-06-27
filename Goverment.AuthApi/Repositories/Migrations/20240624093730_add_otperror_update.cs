using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class add_otperror_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "islock",
                table: "UserOtpSecurities");

            migrationBuilder.DropColumn(
                name: "unblockdate",
                table: "UserOtpSecurities");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 51, 112, 80, 111, 92, 112, 168, 192, 108, 88, 71, 140, 229, 225, 52, 22, 41, 206, 194, 52, 213, 250, 70, 188, 189, 96, 59, 95, 127, 125, 52, 211, 33, 31, 147, 24, 59, 120, 231, 156, 83, 37, 16, 178, 39, 167, 21, 18, 113, 179, 199, 76, 109, 171, 112, 16, 47, 176, 122, 12, 20, 218, 34, 162 }, new byte[] { 59, 115, 192, 27, 117, 241, 46, 9, 136, 226, 15, 235, 66, 132, 209, 43, 246, 32, 227, 31, 39, 42, 112, 83, 106, 117, 20, 90, 91, 146, 116, 237, 22, 253, 226, 144, 166, 207, 200, 195, 217, 66, 57, 229, 97, 111, 233, 88, 160, 75, 199, 233, 50, 184, 216, 115, 105, 22, 146, 5, 186, 16, 184, 24, 225, 63, 11, 153, 86, 72, 225, 194, 167, 125, 175, 155, 230, 123, 153, 144, 114, 213, 226, 44, 132, 223, 220, 13, 99, 7, 127, 158, 207, 58, 89, 201, 31, 95, 89, 2, 33, 12, 27, 4, 213, 179, 195, 69, 174, 249, 234, 143, 170, 5, 246, 159, 194, 162, 64, 176, 227, 79, 125, 167, 14, 20, 158, 188 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "islock",
                table: "UserOtpSecurities",
                type: "boolean",
                maxLength: 5,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "unblockdate",
                table: "UserOtpSecurities",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 162, 53, 209, 168, 123, 242, 16, 239, 38, 172, 218, 20, 222, 88, 226, 32, 251, 99, 92, 161, 92, 79, 168, 130, 204, 13, 37, 93, 133, 5, 102, 66, 146, 227, 71, 239, 168, 20, 129, 170, 37, 55, 248, 94, 94, 165, 240, 247, 248, 214, 171, 55, 46, 134, 160, 143, 116, 64, 198, 62, 251, 107, 147, 240 }, new byte[] { 128, 56, 238, 53, 131, 206, 90, 227, 154, 128, 7, 74, 105, 22, 102, 209, 183, 186, 57, 174, 20, 11, 138, 56, 124, 88, 203, 32, 139, 121, 111, 18, 20, 89, 23, 131, 211, 17, 30, 7, 30, 42, 102, 34, 114, 45, 119, 75, 82, 82, 253, 133, 35, 0, 253, 130, 2, 154, 93, 201, 215, 209, 126, 89, 2, 231, 128, 177, 83, 6, 57, 123, 179, 231, 54, 228, 11, 175, 217, 172, 194, 110, 188, 5, 244, 188, 224, 136, 174, 56, 16, 225, 160, 176, 165, 82, 74, 20, 188, 152, 216, 110, 179, 244, 23, 50, 208, 163, 253, 14, 147, 0, 163, 198, 181, 255, 249, 90, 165, 158, 147, 187, 122, 88, 121, 220, 41, 231 } });
        }
    }
}
