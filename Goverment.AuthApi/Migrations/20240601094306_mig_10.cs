using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserLoginSecurities",
                columns: new[] { "userid", "AccountBlockedTime", "AccountUnBlockedTime", "isBlock", "LoginRetryCount" },
                values: new object[] { 1, null, null, false, 0 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 156, 28, 92, 160, 57, 101, 96, 151, 104, 115, 84, 255, 181, 107, 203, 100, 78, 137, 35, 70, 39, 50, 0, 24, 82, 246, 78, 254, 98, 151, 121, 123, 24, 131, 77, 165, 166, 19, 187, 94, 31, 13, 132, 71, 138, 196, 3, 49, 41, 94, 2, 2, 37, 231, 173, 35, 41, 57, 175, 33, 224, 57, 137, 193 }, new byte[] { 244, 219, 254, 209, 138, 120, 237, 147, 114, 74, 136, 100, 159, 12, 32, 196, 19, 78, 167, 21, 180, 192, 100, 163, 92, 217, 117, 253, 149, 36, 196, 204, 49, 51, 105, 135, 0, 167, 186, 210, 152, 113, 179, 65, 180, 110, 122, 34, 238, 87, 204, 90, 3, 54, 7, 232, 81, 169, 208, 141, 148, 205, 44, 22, 157, 143, 106, 44, 113, 181, 198, 58, 174, 9, 144, 121, 26, 40, 132, 176, 43, 205, 13, 121, 230, 135, 223, 229, 15, 136, 118, 147, 220, 55, 142, 181, 57, 1, 117, 109, 1, 27, 188, 43, 218, 106, 96, 53, 32, 141, 243, 44, 66, 183, 164, 113, 40, 44, 88, 91, 94, 166, 1, 206, 205, 13, 83, 128 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserLoginSecurities",
                keyColumn: "userid",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 98, 11, 119, 46, 145, 170, 31, 180, 245, 112, 223, 107, 96, 110, 246, 102, 122, 48, 249, 55, 110, 208, 182, 116, 170, 171, 255, 166, 28, 243, 191, 3, 249, 54, 36, 70, 10, 231, 198, 168, 152, 122, 236, 174, 150, 90, 117, 75, 177, 252, 101, 30, 116, 153, 218, 154, 34, 164, 151, 124, 93, 162, 184, 1 }, new byte[] { 145, 143, 169, 219, 36, 215, 126, 169, 63, 177, 191, 117, 80, 147, 0, 119, 202, 223, 167, 147, 46, 123, 172, 232, 68, 150, 53, 218, 10, 119, 94, 227, 204, 51, 211, 0, 172, 195, 233, 111, 166, 68, 14, 35, 15, 57, 46, 230, 33, 16, 245, 227, 214, 2, 236, 238, 98, 12, 26, 250, 237, 233, 157, 179, 105, 114, 193, 21, 100, 246, 174, 174, 203, 219, 160, 7, 248, 148, 64, 25, 105, 180, 117, 177, 206, 225, 152, 99, 4, 77, 85, 31, 169, 49, 112, 132, 160, 156, 240, 127, 65, 144, 2, 225, 194, 232, 235, 10, 19, 203, 108, 114, 38, 112, 159, 150, 112, 162, 212, 161, 46, 138, 246, 45, 195, 186, 162, 182 } });
        }
    }
}
