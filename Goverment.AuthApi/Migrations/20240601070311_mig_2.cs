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
            migrationBuilder.DeleteData(
                table: "userroles",
                keyColumns: new[] { "roleid", "userid" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 132, 170, 50, 121, 54, 131, 120, 98, 129, 112, 96, 126, 73, 21, 212, 245, 15, 78, 80, 22, 58, 225, 11, 122, 6, 166, 58, 136, 202, 30, 106, 141, 239, 184, 177, 199, 18, 99, 83, 163, 123, 150, 24, 78, 232, 101, 191, 93, 136, 172, 11, 62, 230, 186, 223, 108, 14, 176, 60, 229, 96, 100, 227, 129 }, new byte[] { 32, 60, 125, 255, 137, 41, 202, 82, 108, 82, 89, 107, 48, 196, 5, 151, 223, 193, 168, 253, 172, 150, 73, 91, 123, 214, 193, 44, 247, 97, 16, 81, 242, 170, 148, 160, 50, 251, 113, 51, 14, 152, 167, 98, 8, 30, 11, 109, 56, 156, 154, 195, 197, 31, 51, 83, 109, 22, 188, 219, 41, 53, 242, 92, 0, 234, 63, 122, 44, 201, 19, 6, 146, 120, 119, 231, 104, 107, 154, 193, 176, 203, 178, 161, 89, 150, 107, 20, 51, 161, 147, 48, 27, 238, 114, 136, 156, 132, 54, 15, 247, 77, 7, 206, 145, 189, 36, 10, 102, 12, 54, 235, 143, 131, 173, 27, 187, 21, 105, 166, 203, 103, 67, 60, 20, 106, 149, 225 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new byte[] { 254, 175, 209, 74, 108, 48, 163, 92, 252, 188, 205, 69, 165, 138, 81, 90, 24, 126, 215, 190, 102, 84, 217, 217, 179, 13, 104, 55, 174, 199, 208, 221, 64, 83, 101, 227, 71, 77, 103, 70, 129, 137, 3, 134, 62, 154, 227, 165, 52, 152, 12, 64, 6, 127, 123, 203, 6, 171, 129, 99, 170, 54, 23, 47 }, new byte[] { 139, 254, 189, 118, 175, 53, 192, 64, 163, 154, 209, 56, 134, 32, 5, 84, 132, 96, 134, 187, 221, 218, 83, 4, 30, 225, 250, 20, 210, 139, 39, 226, 236, 33, 145, 24, 237, 45, 130, 11, 57, 34, 249, 107, 63, 237, 38, 236, 18, 197, 33, 200, 160, 112, 37, 224, 23, 155, 131, 243, 217, 162, 19, 77, 250, 69, 19, 106, 159, 72, 112, 183, 35, 26, 211, 110, 100, 212, 45, 129, 23, 194, 115, 87, 175, 49, 89, 255, 19, 95, 130, 216, 116, 51, 174, 190, 154, 105, 231, 95, 129, 7, 84, 203, 206, 79, 24, 118, 92, 215, 238, 217, 162, 6, 29, 156, 117, 44, 25, 96, 48, 109, 115, 100, 48, 247, 171, 178 } });
        }
    }
}
