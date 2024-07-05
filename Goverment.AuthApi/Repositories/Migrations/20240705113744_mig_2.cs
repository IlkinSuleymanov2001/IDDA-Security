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
            migrationBuilder.DropColumn(
                name: "deletedtime",
                table: "users");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 102, 64, 139, 87, 45, 137, 206, 127, 248, 102, 6, 75, 143, 37, 102, 6, 166, 5, 101, 71, 226, 17, 44, 182, 214, 227, 215, 99, 89, 243, 26, 144, 184, 229, 72, 9, 4, 95, 245, 77, 231, 87, 220, 105, 47, 100, 102, 243, 67, 186, 33, 227, 210, 212, 80, 233, 31, 59, 14, 218, 17, 61, 196, 231 }, new byte[] { 77, 6, 138, 193, 242, 38, 236, 207, 255, 107, 120, 26, 158, 125, 172, 171, 144, 138, 243, 247, 38, 78, 67, 162, 196, 204, 46, 123, 168, 195, 74, 162, 38, 5, 135, 17, 153, 250, 8, 184, 44, 149, 77, 151, 243, 27, 153, 39, 51, 218, 62, 255, 93, 57, 198, 6, 219, 66, 209, 245, 7, 91, 172, 106, 138, 221, 240, 44, 82, 87, 153, 80, 251, 198, 252, 232, 44, 37, 90, 145, 33, 170, 98, 232, 45, 4, 64, 199, 206, 124, 107, 5, 29, 12, 189, 206, 241, 74, 118, 129, 85, 222, 11, 118, 244, 31, 60, 41, 13, 1, 6, 22, 16, 142, 52, 130, 239, 135, 225, 178, 56, 217, 174, 68, 44, 12, 241, 119 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deletedtime",
                table: "users",
                type: "timestamp with time zone",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "users",
                type: "boolean",
                maxLength: 50,
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "deletedtime", "isdelete", "passwordhash", "PasswordSalt" },
                values: new object[] { null, false, new byte[] { 9, 50, 196, 11, 103, 131, 83, 0, 213, 54, 122, 185, 93, 138, 31, 156, 62, 238, 222, 241, 49, 103, 108, 66, 67, 249, 113, 37, 53, 38, 55, 131, 72, 97, 157, 138, 176, 22, 166, 35, 90, 163, 197, 149, 254, 180, 26, 164, 67, 121, 12, 189, 134, 141, 147, 175, 90, 4, 27, 232, 116, 111, 158, 106 }, new byte[] { 167, 5, 166, 218, 223, 163, 57, 77, 71, 79, 250, 22, 143, 207, 179, 135, 153, 246, 173, 73, 100, 101, 247, 193, 43, 79, 11, 174, 11, 12, 142, 89, 76, 97, 57, 13, 253, 229, 210, 230, 201, 146, 217, 111, 210, 6, 30, 37, 92, 167, 195, 168, 169, 195, 130, 149, 193, 231, 185, 212, 229, 39, 67, 64, 57, 109, 84, 235, 215, 77, 151, 132, 154, 183, 16, 18, 82, 154, 202, 78, 23, 219, 98, 174, 75, 62, 78, 188, 63, 211, 177, 252, 219, 240, 234, 65, 239, 99, 127, 161, 160, 236, 121, 97, 56, 136, 78, 234, 183, 199, 64, 163, 248, 102, 225, 31, 49, 154, 128, 46, 55, 186, 80, 5, 162, 2, 152, 213 } });
        }
    }
}
