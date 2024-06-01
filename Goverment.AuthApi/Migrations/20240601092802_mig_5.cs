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
            migrationBuilder.AlterColumn<bool>(
                name: "isBlock",
                table: "UserLoginSecurities",
                type: "bit",
                maxLength: 5,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 158, 89, 186, 226, 20, 21, 234, 169, 162, 217, 120, 20, 12, 63, 32, 147, 229, 102, 221, 88, 29, 140, 63, 228, 10, 233, 245, 230, 30, 47, 186, 191, 232, 23, 238, 239, 250, 154, 172, 223, 164, 62, 14, 175, 219, 213, 13, 133, 199, 55, 231, 205, 197, 23, 142, 172, 192, 57, 5, 51, 1, 95, 62, 142 }, new byte[] { 43, 40, 52, 39, 204, 63, 239, 204, 180, 218, 210, 237, 222, 154, 147, 232, 162, 36, 108, 100, 121, 127, 129, 40, 117, 33, 254, 131, 137, 15, 149, 112, 236, 61, 249, 83, 193, 161, 102, 144, 0, 236, 137, 193, 165, 203, 124, 17, 224, 9, 128, 163, 61, 90, 64, 187, 71, 163, 183, 47, 247, 96, 136, 216, 88, 136, 63, 18, 79, 33, 232, 185, 126, 82, 127, 140, 211, 21, 119, 44, 205, 17, 18, 207, 226, 190, 98, 45, 113, 232, 184, 233, 62, 91, 60, 3, 54, 125, 40, 67, 45, 93, 166, 71, 199, 44, 40, 101, 85, 183, 159, 143, 215, 109, 187, 207, 183, 100, 175, 99, 85, 45, 71, 238, 77, 180, 2, 127 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlock",
                table: "UserLoginSecurities",
                type: "bit",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 5);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 219, 167, 25, 20, 189, 15, 117, 86, 66, 239, 189, 173, 17, 106, 177, 13, 43, 193, 52, 114, 187, 31, 149, 195, 158, 249, 206, 16, 200, 113, 168, 31, 50, 106, 218, 19, 86, 198, 10, 27, 71, 37, 86, 219, 84, 53, 194, 67, 95, 111, 46, 179, 129, 150, 32, 222, 70, 4, 69, 202, 225, 228, 22, 123 }, new byte[] { 98, 117, 71, 127, 192, 22, 244, 204, 196, 220, 140, 103, 229, 161, 57, 84, 68, 171, 44, 51, 219, 135, 66, 107, 203, 108, 211, 101, 227, 154, 82, 57, 32, 247, 143, 164, 120, 72, 195, 57, 206, 206, 100, 48, 135, 91, 91, 97, 155, 24, 204, 165, 248, 245, 112, 67, 161, 99, 71, 23, 54, 239, 67, 205, 36, 55, 209, 119, 124, 45, 8, 141, 240, 0, 77, 57, 7, 180, 33, 8, 115, 28, 110, 1, 222, 98, 42, 108, 58, 28, 105, 249, 236, 182, 231, 214, 169, 50, 124, 52, 64, 235, 89, 115, 216, 28, 251, 5, 62, 97, 74, 135, 60, 190, 250, 47, 142, 71, 192, 113, 48, 4, 6, 102, 85, 241, 157, 135 } });
        }
    }
}
