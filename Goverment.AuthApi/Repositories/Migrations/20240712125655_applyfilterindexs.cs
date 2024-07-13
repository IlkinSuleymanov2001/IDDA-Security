using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class applyfilterindexs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_idtoken",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_otpcode",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 179, 193, 107, 176, 37, 28, 25, 103, 135, 147, 51, 203, 226, 176, 35, 56, 115, 162, 239, 27, 77, 206, 91, 37, 12, 147, 14, 105, 120, 161, 90, 83, 34, 111, 223, 184, 206, 48, 26, 117, 178, 1, 248, 120, 12, 26, 173, 123, 172, 168, 68, 42, 23, 249, 83, 59, 98, 137, 234, 140, 226, 117, 101, 114 }, new byte[] { 238, 89, 183, 0, 176, 244, 133, 253, 102, 22, 242, 103, 250, 236, 244, 225, 185, 68, 212, 242, 137, 52, 243, 17, 41, 108, 228, 18, 208, 146, 234, 69, 100, 133, 252, 192, 139, 178, 92, 77, 166, 223, 9, 23, 53, 255, 157, 124, 225, 187, 248, 231, 124, 81, 172, 39, 200, 129, 19, 158, 22, 249, 27, 95, 84, 232, 75, 250, 183, 40, 200, 63, 43, 159, 54, 244, 222, 215, 189, 89, 136, 155, 131, 124, 210, 2, 125, 236, 87, 45, 255, 152, 153, 97, 24, 189, 152, 194, 100, 207, 231, 72, 207, 236, 163, 123, 126, 230, 217, 49, 84, 88, 121, 239, 135, 26, 54, 216, 62, 38, 47, 202, 166, 43, 185, 72, 105, 112 } });

            migrationBuilder.CreateIndex(
                name: "IX_users_idtoken",
                table: "users",
                column: "idtoken",
                unique: true,
                filter: "\"idtoken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_otpcode",
                table: "users",
                column: "otpcode",
                unique: true,
                filter: "\"otpcode\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_idtoken",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_otpcode",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 167, 57, 221, 119, 232, 32, 188, 88, 210, 202, 70, 152, 126, 19, 171, 84, 53, 0, 37, 228, 213, 127, 215, 31, 141, 252, 180, 121, 35, 8, 154, 17, 53, 167, 74, 222, 152, 87, 173, 89, 64, 208, 110, 39, 180, 145, 27, 29, 139, 21, 203, 208, 94, 207, 81, 150, 61, 120, 44, 129, 215, 55, 226, 227 }, new byte[] { 171, 14, 214, 222, 216, 32, 61, 123, 58, 211, 250, 214, 214, 154, 240, 135, 78, 73, 79, 52, 161, 11, 81, 152, 89, 219, 0, 137, 34, 82, 142, 215, 192, 12, 175, 26, 240, 14, 171, 239, 79, 226, 4, 92, 203, 128, 191, 121, 44, 155, 40, 126, 128, 123, 10, 104, 124, 221, 177, 64, 27, 107, 40, 162, 50, 66, 246, 167, 150, 2, 84, 27, 51, 164, 10, 158, 235, 136, 198, 148, 130, 17, 214, 65, 46, 74, 117, 88, 66, 22, 210, 94, 24, 107, 136, 128, 39, 30, 173, 237, 181, 198, 187, 41, 151, 9, 141, 101, 62, 162, 185, 102, 12, 149, 32, 113, 191, 111, 141, 114, 250, 137, 233, 213, 168, 125, 154, 223 } });

            migrationBuilder.CreateIndex(
                name: "IX_users_idtoken",
                table: "users",
                column: "idtoken",
                unique: true,
                filter: "\"IDToken\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_otpcode",
                table: "users",
                column: "otpcode",
                unique: true,
                filter: "\"OtpCode\" IS NOT NULL");
        }
    }
}
