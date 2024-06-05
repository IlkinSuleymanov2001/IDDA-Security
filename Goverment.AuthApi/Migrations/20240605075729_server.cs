using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class server : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "userroles",
                columns: new[] { "roleid", "userid" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 15, 209, 155, 50, 58, 102, 242, 134, 157, 176, 105, 230, 251, 95, 162, 204, 49, 37, 14, 7, 63, 3, 174, 232, 106, 50, 245, 150, 206, 143, 5, 190, 149, 8, 175, 57, 103, 236, 76, 148, 161, 204, 90, 4, 197, 109, 181, 106, 205, 22, 183, 120, 12, 31, 148, 95, 10, 65, 215, 101, 26, 219, 199, 125 }, new byte[] { 145, 92, 28, 164, 53, 248, 216, 235, 69, 9, 25, 57, 213, 168, 219, 206, 218, 117, 98, 44, 128, 68, 70, 247, 59, 15, 46, 244, 254, 179, 125, 165, 185, 63, 129, 230, 169, 243, 112, 181, 98, 141, 199, 27, 124, 221, 19, 49, 120, 110, 196, 105, 230, 252, 96, 129, 161, 115, 41, 78, 175, 96, 187, 230, 54, 109, 78, 122, 191, 223, 112, 79, 119, 106, 244, 40, 117, 117, 154, 209, 118, 41, 212, 8, 214, 246, 25, 155, 170, 21, 40, 31, 137, 209, 23, 250, 54, 121, 39, 70, 23, 202, 150, 118, 47, 40, 114, 215, 121, 243, 60, 231, 43, 213, 109, 200, 2, 127, 186, 136, 82, 217, 59, 193, 231, 248, 147, 139 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "userroles",
                keyColumns: new[] { "roleid", "userid" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 14, 22, 72, 151, 84, 222, 95, 41, 250, 254, 227, 160, 11, 41, 52, 124, 224, 224, 140, 84, 158, 122, 51, 5, 198, 2, 75, 163, 239, 138, 13, 30, 33, 131, 230, 100, 179, 109, 245, 182, 125, 140, 43, 182, 57, 29, 12, 25, 132, 143, 25, 184, 70, 75, 213, 135, 213, 65, 37, 57, 192, 178, 15, 238 }, new byte[] { 81, 109, 140, 11, 78, 58, 58, 225, 11, 191, 76, 231, 237, 54, 27, 123, 255, 127, 57, 24, 54, 80, 68, 248, 50, 227, 236, 184, 178, 43, 208, 156, 246, 174, 62, 209, 116, 15, 137, 242, 111, 175, 155, 33, 120, 216, 52, 90, 134, 172, 211, 55, 30, 86, 75, 70, 197, 118, 96, 146, 229, 255, 251, 190, 69, 203, 140, 57, 173, 129, 53, 135, 190, 234, 208, 62, 189, 201, 238, 153, 171, 236, 242, 74, 181, 12, 226, 181, 80, 128, 19, 19, 65, 88, 14, 157, 125, 245, 58, 202, 232, 188, 226, 211, 216, 218, 31, 192, 183, 107, 68, 235, 20, 145, 93, 212, 80, 88, 105, 134, 60, 179, 94, 213, 148, 220, 81, 56 } });
        }
    }
}
