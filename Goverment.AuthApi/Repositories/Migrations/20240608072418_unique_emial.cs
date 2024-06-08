using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class unique_emial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordsalt",
                table: "users",
                newName: "PasswordSalt");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 74, 121, 229, 251, 33, 87, 179, 52, 87, 169, 48, 158, 245, 123, 35, 183, 198, 5, 153, 222, 166, 106, 25, 49, 37, 203, 51, 227, 167, 81, 151, 224, 239, 5, 25, 193, 242, 214, 217, 76, 222, 214, 195, 120, 1, 120, 172, 120, 141, 183, 25, 155, 60, 192, 136, 80, 69, 151, 103, 247, 21, 2, 7, 87 }, new byte[] { 102, 99, 196, 181, 133, 129, 167, 73, 17, 64, 167, 66, 45, 209, 101, 221, 198, 49, 30, 53, 189, 171, 125, 211, 235, 56, 93, 8, 19, 224, 111, 199, 88, 130, 172, 188, 61, 252, 242, 174, 202, 223, 185, 122, 135, 152, 87, 117, 219, 57, 218, 73, 128, 219, 195, 117, 50, 156, 228, 37, 14, 237, 187, 60, 84, 202, 138, 238, 17, 3, 154, 149, 75, 54, 16, 112, 118, 143, 68, 142, 255, 23, 56, 166, 0, 177, 250, 247, 203, 31, 162, 142, 125, 181, 202, 0, 172, 74, 214, 68, 171, 103, 113, 95, 165, 220, 103, 132, 167, 216, 106, 189, 152, 158, 173, 221, 110, 37, 191, 142, 72, 132, 104, 254, 127, 92, 208, 222 } });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "users",
                newName: "passwordsalt");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 82, 210, 182, 200, 215, 234, 151, 186, 237, 69, 246, 136, 205, 145, 8, 148, 78, 251, 249, 101, 254, 55, 126, 229, 207, 128, 248, 179, 64, 156, 142, 120, 186, 53, 85, 165, 172, 11, 76, 226, 235, 250, 229, 231, 48, 71, 82, 174, 89, 196, 108, 173, 221, 238, 92, 116, 201, 143, 230, 136, 173, 165, 23, 152 }, new byte[] { 207, 138, 77, 63, 17, 241, 132, 211, 124, 12, 19, 239, 131, 227, 79, 138, 232, 17, 24, 147, 215, 8, 15, 222, 49, 65, 91, 20, 34, 135, 45, 174, 157, 129, 154, 5, 242, 207, 60, 250, 121, 203, 162, 75, 48, 11, 3, 113, 149, 231, 176, 234, 119, 130, 26, 241, 226, 153, 144, 192, 85, 111, 118, 206, 78, 240, 37, 216, 245, 56, 53, 93, 164, 252, 107, 239, 62, 243, 173, 144, 164, 88, 166, 34, 111, 160, 70, 130, 23, 17, 245, 168, 41, 49, 88, 178, 125, 62, 41, 120, 61, 133, 23, 91, 51, 196, 142, 159, 75, 137, 106, 169, 182, 195, 195, 142, 184, 121, 145, 192, 196, 71, 91, 92, 172, 143, 248, 206 } });
        }
    }
}
