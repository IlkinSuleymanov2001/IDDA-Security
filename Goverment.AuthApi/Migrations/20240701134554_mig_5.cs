using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserAudits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "UserAudits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 155, 211, 85, 32, 7, 244, 98, 135, 206, 171, 47, 61, 98, 89, 38, 93, 56, 37, 146, 80, 120, 49, 196, 189, 234, 206, 108, 13, 174, 215, 107, 210, 232, 53, 211, 134, 255, 20, 221, 122, 122, 43, 9, 22, 189, 45, 22, 71, 28, 95, 80, 97, 31, 245, 175, 32, 70, 13, 207, 102, 194, 195, 92, 92 }, new byte[] { 255, 55, 24, 44, 41, 153, 90, 127, 164, 205, 12, 9, 138, 228, 204, 239, 205, 7, 189, 7, 72, 238, 22, 136, 202, 250, 254, 224, 170, 255, 227, 104, 147, 237, 22, 142, 72, 35, 134, 158, 179, 212, 246, 135, 20, 206, 237, 101, 29, 139, 100, 147, 118, 227, 56, 134, 64, 234, 46, 45, 70, 25, 41, 180, 47, 243, 165, 92, 53, 93, 211, 181, 0, 14, 103, 240, 30, 186, 180, 182, 194, 3, 135, 117, 170, 98, 134, 113, 233, 145, 70, 122, 201, 136, 49, 223, 216, 237, 186, 130, 141, 12, 122, 169, 137, 205, 121, 108, 18, 124, 220, 251, 39, 13, 81, 67, 109, 233, 80, 114, 180, 147, 178, 127, 108, 218, 125, 216 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserAudits");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "UserAudits");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 43, 58, 220, 29, 237, 168, 56, 2, 143, 53, 221, 98, 255, 189, 46, 145, 104, 79, 37, 172, 19, 67, 172, 211, 97, 143, 69, 98, 4, 17, 182, 49, 171, 182, 240, 167, 230, 153, 72, 99, 111, 108, 71, 238, 66, 237, 242, 255, 148, 4, 186, 131, 65, 115, 183, 59, 237, 22, 122, 46, 109, 225, 193 }, new byte[] { 113, 181, 11, 69, 249, 134, 75, 191, 136, 49, 110, 15, 3, 251, 254, 16, 104, 148, 123, 24, 124, 214, 90, 183, 244, 54, 226, 46, 222, 46, 129, 206, 7, 0, 97, 155, 85, 108, 128, 84, 205, 179, 29, 141, 1, 176, 68, 104, 7, 92, 53, 123, 3, 87, 106, 108, 161, 45, 80, 202, 4, 213, 18, 231, 7, 53, 218, 67, 43, 239, 40, 91, 1, 101, 123, 127, 133, 122, 172, 121, 210, 87, 255, 122, 98, 62, 86, 195, 58, 22, 95, 213, 11, 204, 38, 154, 60, 87, 67, 106, 242, 73, 90, 212, 237, 138, 94, 185, 22, 248, 234, 63, 37, 111, 31, 119, 196, 84, 25, 99, 165, 50, 160, 48, 209, 59, 96, 134 } });
        }
    }
}
