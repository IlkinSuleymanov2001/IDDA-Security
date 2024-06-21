using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class add_idtoken_name_config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDTokenExpireDate",
                table: "users",
                newName: "idtokenexpiredate");

            migrationBuilder.RenameColumn(
                name: "IDToken",
                table: "users",
                newName: "idtoken");

            migrationBuilder.AlterColumn<string>(
                name: "idtoken",
                table: "users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 39, 146, 41, 172, 151, 202, 82, 73, 207, 95, 135, 206, 66, 231, 186, 17, 132, 233, 185, 136, 54, 182, 204, 223, 163, 157, 216, 160, 46, 119, 117, 245, 192, 21, 127, 74, 92, 200, 232, 5, 192, 17, 246, 26, 52, 222, 85, 245, 153, 67, 235, 179, 149, 249, 68, 59, 109, 156, 223, 82, 220, 45, 156, 64 }, new byte[] { 54, 62, 145, 43, 198, 210, 194, 58, 133, 213, 110, 132, 192, 150, 79, 38, 2, 186, 47, 152, 222, 111, 90, 190, 96, 124, 168, 169, 93, 248, 124, 16, 5, 120, 24, 45, 91, 30, 176, 135, 215, 164, 134, 6, 153, 170, 22, 47, 213, 128, 58, 254, 222, 87, 229, 74, 47, 62, 94, 96, 179, 50, 99, 211, 28, 216, 100, 60, 4, 80, 37, 179, 60, 195, 42, 71, 16, 165, 115, 104, 245, 20, 77, 186, 159, 77, 96, 244, 109, 88, 3, 216, 191, 103, 93, 88, 39, 229, 61, 221, 55, 134, 73, 207, 117, 253, 168, 38, 170, 188, 7, 24, 216, 192, 58, 223, 14, 135, 188, 93, 123, 176, 47, 225, 230, 142, 95, 62 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idtokenexpiredate",
                table: "users",
                newName: "IDTokenExpireDate");

            migrationBuilder.RenameColumn(
                name: "idtoken",
                table: "users",
                newName: "IDToken");

            migrationBuilder.AlterColumn<string>(
                name: "IDToken",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 219, 171, 201, 254, 133, 169, 38, 113, 91, 100, 170, 162, 200, 179, 205, 70, 159, 74, 166, 234, 152, 96, 179, 245, 214, 235, 38, 229, 31, 190, 124, 255, 66, 223, 77, 100, 18, 203, 178, 199, 150, 86, 124, 67, 197, 132, 247, 246, 216, 122, 72, 54, 45, 12, 56, 30, 116, 253, 161, 236, 41, 24, 31, 44 }, new byte[] { 57, 45, 150, 76, 84, 46, 1, 93, 15, 57, 119, 140, 21, 148, 190, 171, 41, 27, 181, 254, 145, 240, 204, 175, 199, 154, 132, 151, 128, 135, 7, 23, 250, 37, 138, 26, 69, 91, 86, 131, 176, 140, 97, 73, 62, 102, 226, 136, 11, 157, 125, 107, 209, 4, 243, 175, 176, 15, 126, 221, 241, 134, 154, 144, 17, 17, 46, 162, 169, 94, 102, 237, 31, 188, 166, 20, 70, 210, 184, 90, 189, 88, 57, 69, 179, 94, 103, 218, 170, 40, 226, 250, 35, 195, 96, 34, 183, 231, 92, 103, 222, 92, 44, 20, 9, 232, 195, 253, 152, 207, 47, 248, 48, 161, 60, 196, 181, 1, 132, 188, 3, 158, 64, 82, 19, 195, 21, 164 } });
        }
    }
}
