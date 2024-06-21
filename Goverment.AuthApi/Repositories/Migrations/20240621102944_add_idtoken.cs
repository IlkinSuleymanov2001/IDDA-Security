using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class add_idtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDToken",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IDTokenExpireDate",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IDToken", "IDTokenExpireDate", "passwordhash", "PasswordSalt" },
                values: new object[] { null, null, new byte[] { 219, 171, 201, 254, 133, 169, 38, 113, 91, 100, 170, 162, 200, 179, 205, 70, 159, 74, 166, 234, 152, 96, 179, 245, 214, 235, 38, 229, 31, 190, 124, 255, 66, 223, 77, 100, 18, 203, 178, 199, 150, 86, 124, 67, 197, 132, 247, 246, 216, 122, 72, 54, 45, 12, 56, 30, 116, 253, 161, 236, 41, 24, 31, 44 }, new byte[] { 57, 45, 150, 76, 84, 46, 1, 93, 15, 57, 119, 140, 21, 148, 190, 171, 41, 27, 181, 254, 145, 240, 204, 175, 199, 154, 132, 151, 128, 135, 7, 23, 250, 37, 138, 26, 69, 91, 86, 131, 176, 140, 97, 73, 62, 102, 226, 136, 11, 157, 125, 107, 209, 4, 243, 175, 176, 15, 126, 221, 241, 134, 154, 144, 17, 17, 46, 162, 169, 94, 102, 237, 31, 188, 166, 20, 70, 210, 184, 90, 189, 88, 57, 69, 179, 94, 103, 218, 170, 40, 226, 250, 35, 195, 96, 34, 183, 231, 92, 103, 222, 92, 44, 20, 9, 232, 195, 253, 152, 207, 47, 248, 48, 161, 60, 196, 181, 1, 132, 188, 3, 158, 64, 82, 19, 195, 21, 164 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IDTokenExpireDate",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 178, 190, 79, 149, 211, 250, 241, 105, 65, 163, 54, 23, 122, 10, 112, 247, 71, 21, 91, 122, 69, 153, 144, 123, 213, 54, 5, 4, 190, 125, 127, 188, 78, 180, 17, 149, 232, 45, 103, 201, 111, 42, 224, 233, 106, 99, 63, 245, 24, 252, 211, 28, 84, 243, 188, 2, 76, 116, 236, 11, 221, 192, 119, 191 }, new byte[] { 241, 116, 209, 203, 154, 194, 24, 162, 230, 132, 240, 137, 167, 246, 51, 238, 34, 52, 11, 88, 236, 144, 68, 196, 97, 131, 197, 252, 117, 100, 82, 251, 218, 11, 91, 223, 240, 254, 131, 70, 107, 82, 197, 98, 46, 222, 101, 9, 222, 12, 239, 98, 85, 61, 139, 99, 250, 35, 43, 31, 200, 131, 150, 234, 83, 246, 223, 100, 35, 15, 238, 34, 206, 7, 228, 189, 5, 225, 213, 50, 15, 231, 87, 138, 225, 125, 241, 226, 54, 211, 65, 254, 75, 175, 221, 149, 236, 52, 170, 186, 182, 162, 13, 176, 61, 96, 12, 54, 27, 81, 24, 25, 184, 46, 2, 36, 210, 97, 119, 92, 136, 66, 27, 16, 219, 11, 81, 46 } });
        }
    }
}
