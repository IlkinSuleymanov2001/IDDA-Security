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
            migrationBuilder.DropColumn(
                name: "lastname",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "firstname", "passwordhash", "PasswordSalt" },
                values: new object[] { "Ilkin  Suleymanov", new byte[] { 178, 190, 79, 149, 211, 250, 241, 105, 65, 163, 54, 23, 122, 10, 112, 247, 71, 21, 91, 122, 69, 153, 144, 123, 213, 54, 5, 4, 190, 125, 127, 188, 78, 180, 17, 149, 232, 45, 103, 201, 111, 42, 224, 233, 106, 99, 63, 245, 24, 252, 211, 28, 84, 243, 188, 2, 76, 116, 236, 11, 221, 192, 119, 191 }, new byte[] { 241, 116, 209, 203, 154, 194, 24, 162, 230, 132, 240, 137, 167, 246, 51, 238, 34, 52, 11, 88, 236, 144, 68, 196, 97, 131, 197, 252, 117, 100, 82, 251, 218, 11, 91, 223, 240, 254, 131, 70, 107, 82, 197, 98, 46, 222, 101, 9, 222, 12, 239, 98, 85, 61, 139, 99, 250, 35, 43, 31, 200, 131, 150, 234, 83, 246, 223, 100, 35, 15, 238, 34, 206, 7, 228, 189, 5, 225, 213, 50, 15, 231, 87, 138, 225, 125, 241, 226, 54, 211, 65, 254, 75, 175, 221, 149, 236, 52, 170, 186, 182, 162, 13, 176, 61, 96, 12, 54, 27, 81, 24, 25, 184, 46, 2, 36, 210, 97, 119, 92, 136, 66, 27, 16, 219, 11, 81, 46 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "firstname", "lastname", "passwordhash", "PasswordSalt" },
                values: new object[] { "Ilkin", "Suleymanov", new byte[] { 166, 69, 146, 238, 183, 221, 112, 225, 149, 44, 240, 173, 56, 236, 192, 174, 84, 28, 144, 246, 98, 41, 201, 249, 242, 70, 248, 96, 224, 174, 24, 31, 79, 235, 13, 18, 40, 39, 116, 128, 177, 202, 5, 2, 161, 13, 185, 67, 173, 30, 117, 192, 54, 17, 80, 62, 108, 236, 254, 193, 17, 101, 76, 220 }, new byte[] { 197, 170, 195, 161, 149, 164, 153, 142, 149, 223, 34, 243, 213, 27, 71, 140, 98, 99, 145, 77, 136, 31, 170, 244, 2, 193, 39, 136, 166, 196, 14, 63, 135, 183, 216, 68, 140, 0, 48, 139, 165, 178, 19, 105, 233, 85, 61, 173, 23, 89, 163, 101, 208, 92, 50, 247, 61, 23, 61, 246, 121, 99, 35, 94, 56, 175, 58, 151, 167, 229, 91, 148, 158, 169, 189, 45, 118, 53, 151, 210, 71, 0, 205, 186, 232, 242, 225, 162, 32, 210, 40, 110, 94, 59, 133, 125, 148, 11, 8, 88, 143, 160, 156, 104, 110, 170, 87, 127, 123, 236, 35, 240, 205, 15, 54, 187, 117, 58, 241, 82, 83, 186, 110, 174, 131, 83, 70, 111 } });
        }
    }
}
