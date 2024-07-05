using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserAudits");

            migrationBuilder.DropColumn(
                name: "IsVerify",
                table: "UserAudits");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserAudits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAudits");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 9, 50, 196, 11, 103, 131, 83, 0, 213, 54, 122, 185, 93, 138, 31, 156, 62, 238, 222, 241, 49, 103, 108, 66, 67, 249, 113, 37, 53, 38, 55, 131, 72, 97, 157, 138, 176, 22, 166, 35, 90, 163, 197, 149, 254, 180, 26, 164, 67, 121, 12, 189, 134, 141, 147, 175, 90, 4, 27, 232, 116, 111, 158, 106 }, new byte[] { 167, 5, 166, 218, 223, 163, 57, 77, 71, 79, 250, 22, 143, 207, 179, 135, 153, 246, 173, 73, 100, 101, 247, 193, 43, 79, 11, 174, 11, 12, 142, 89, 76, 97, 57, 13, 253, 229, 210, 230, 201, 146, 217, 111, 210, 6, 30, 37, 92, 167, 195, 168, 169, 195, 130, 149, 193, 231, 185, 212, 229, 39, 67, 64, 57, 109, 84, 235, 215, 77, 151, 132, 154, 183, 16, 18, 82, 154, 202, 78, 23, 219, 98, 174, 75, 62, 78, 188, 63, 211, 177, 252, 219, 240, 234, 65, 239, 99, 127, 161, 160, 236, 121, 97, 56, 136, 78, 234, 183, 199, 64, 163, 248, 102, 225, 31, 49, 154, 128, 46, 55, 186, 80, 5, 162, 2, 152, 213 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserAudits",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerify",
                table: "UserAudits",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UserAudits",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserAudits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 143, 201, 137, 145, 212, 183, 124, 157, 135, 158, 26, 246, 106, 23, 57, 208, 189, 90, 50, 182, 218, 157, 126, 53, 149, 13, 79, 80, 7, 108, 189, 170, 148, 62, 20, 194, 139, 250, 73, 115, 27, 240, 9, 21, 80, 96, 51, 212, 220, 100, 134, 89, 7, 252, 21, 200, 198, 240, 199, 37, 144, 13, 134, 57 }, new byte[] { 249, 205, 91, 201, 197, 148, 176, 253, 165, 43, 233, 165, 103, 141, 102, 190, 52, 43, 190, 194, 57, 1, 31, 85, 135, 182, 189, 62, 239, 111, 239, 209, 116, 96, 106, 146, 93, 102, 128, 12, 11, 239, 16, 32, 13, 9, 175, 58, 137, 35, 224, 253, 234, 150, 141, 227, 77, 205, 40, 253, 107, 16, 250, 87, 52, 202, 32, 221, 72, 31, 252, 230, 236, 255, 199, 171, 136, 93, 202, 151, 99, 42, 199, 39, 98, 130, 219, 20, 203, 78, 188, 40, 120, 225, 244, 10, 215, 85, 160, 34, 186, 213, 155, 250, 95, 108, 151, 207, 83, 204, 213, 72, 77, 237, 253, 219, 239, 195, 226, 186, 215, 114, 237, 108, 0, 61, 181, 16 } });
        }
    }
}
