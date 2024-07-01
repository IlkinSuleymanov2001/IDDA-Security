using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdtime",
                table: "users",
                type: "datetime2",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedtime",
                table: "users",
                type: "datetime2",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "users",
                type: "bit",
                maxLength: 50,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "modifiedtime",
                table: "users",
                type: "datetime2",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "createdtime", "deletedtime", "isdelete", "modifiedtime", "passwordhash", "PasswordSalt" },
                values: new object[] { null, null, false, null, new byte[] { 139, 43, 58, 220, 29, 237, 168, 56, 2, 143, 53, 221, 98, 255, 189, 46, 145, 104, 79, 37, 172, 19, 67, 172, 211, 97, 143, 69, 98, 4, 17, 182, 49, 171, 182, 240, 167, 230, 153, 72, 99, 111, 108, 71, 238, 66, 237, 242, 255, 148, 4, 186, 131, 65, 115, 183, 59, 237, 22, 122, 46, 109, 225, 193 }, new byte[] { 113, 181, 11, 69, 249, 134, 75, 191, 136, 49, 110, 15, 3, 251, 254, 16, 104, 148, 123, 24, 124, 214, 90, 183, 244, 54, 226, 46, 222, 46, 129, 206, 7, 0, 97, 155, 85, 108, 128, 84, 205, 179, 29, 141, 1, 176, 68, 104, 7, 92, 53, 123, 3, 87, 106, 108, 161, 45, 80, 202, 4, 213, 18, 231, 7, 53, 218, 67, 43, 239, 40, 91, 1, 101, 123, 127, 133, 122, 172, 121, 210, 87, 255, 122, 98, 62, 86, 195, 58, 22, 95, 213, 11, 204, 38, 154, 60, 87, 67, 106, 242, 73, 90, 212, 237, 138, 94, 185, 22, 248, 234, 63, 37, 111, 31, 119, 196, 84, 25, 99, 165, 50, 160, 48, 209, 59, 96, 134 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdtime",
                table: "users");

            migrationBuilder.DropColumn(
                name: "deletedtime",
                table: "users");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "users");

            migrationBuilder.DropColumn(
                name: "modifiedtime",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 94, 118, 240, 62, 250, 97, 104, 91, 212, 90, 127, 83, 142, 1, 180, 159, 31, 80, 223, 60, 115, 247, 166, 157, 166, 165, 205, 145, 151, 143, 20, 43, 57, 120, 152, 34, 44, 251, 75, 96, 205, 135, 235, 56, 254, 103, 212, 99, 243, 252, 17, 68, 238, 139, 244, 228, 194, 109, 246, 172, 200, 53, 147, 106 }, new byte[] { 178, 129, 14, 198, 30, 152, 76, 227, 136, 147, 56, 16, 186, 207, 78, 211, 254, 245, 78, 104, 98, 39, 207, 99, 78, 181, 223, 72, 154, 42, 57, 243, 38, 122, 160, 152, 7, 94, 77, 91, 216, 214, 155, 188, 123, 246, 176, 114, 4, 58, 247, 203, 243, 6, 107, 126, 224, 130, 237, 64, 55, 152, 197, 232, 164, 55, 6, 195, 10, 88, 114, 26, 43, 170, 233, 60, 93, 145, 120, 210, 180, 144, 180, 58, 108, 166, 85, 27, 145, 152, 117, 114, 147, 119, 1, 17, 170, 65, 150, 7, 137, 255, 79, 120, 26, 20, 105, 197, 119, 67, 246, 249, 70, 59, 45, 115, 223, 218, 183, 105, 161, 87, 227, 64, 225, 250, 118, 23 } });
        }
    }
}
