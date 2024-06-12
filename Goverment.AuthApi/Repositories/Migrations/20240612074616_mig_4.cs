using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tryotpcount",
                table: "UserOtpSecurities",
                type: "integer",
                maxLength: 2,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserOtpSecurities",
                keyColumn: "userid",
                keyValue: 1,
                column: "tryotpcount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 166, 69, 146, 238, 183, 221, 112, 225, 149, 44, 240, 173, 56, 236, 192, 174, 84, 28, 144, 246, 98, 41, 201, 249, 242, 70, 248, 96, 224, 174, 24, 31, 79, 235, 13, 18, 40, 39, 116, 128, 177, 202, 5, 2, 161, 13, 185, 67, 173, 30, 117, 192, 54, 17, 80, 62, 108, 236, 254, 193, 17, 101, 76, 220 }, new byte[] { 197, 170, 195, 161, 149, 164, 153, 142, 149, 223, 34, 243, 213, 27, 71, 140, 98, 99, 145, 77, 136, 31, 170, 244, 2, 193, 39, 136, 166, 196, 14, 63, 135, 183, 216, 68, 140, 0, 48, 139, 165, 178, 19, 105, 233, 85, 61, 173, 23, 89, 163, 101, 208, 92, 50, 247, 61, 23, 61, 246, 121, 99, 35, 94, 56, 175, 58, 151, 167, 229, 91, 148, 158, 169, 189, 45, 118, 53, 151, 210, 71, 0, 205, 186, 232, 242, 225, 162, 32, 210, 40, 110, 94, 59, 133, 125, 148, 11, 8, 88, 143, 160, 156, 104, 110, 170, 87, 127, 123, 236, 35, 240, 205, 15, 54, 187, 117, 58, 241, 82, 83, 186, 110, 174, 131, 83, 70, 111 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tryotpcount",
                table: "UserOtpSecurities",
                type: "integer",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 2);

            migrationBuilder.UpdateData(
                table: "UserOtpSecurities",
                keyColumn: "userid",
                keyValue: 1,
                column: "tryotpcount",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 128, 95, 30, 6, 225, 192, 234, 37, 211, 70, 102, 137, 220, 66, 198, 178, 15, 33, 41, 131, 152, 52, 66, 79, 132, 254, 6, 164, 174, 177, 188, 103, 71, 145, 142, 17, 189, 78, 108, 183, 222, 163, 84, 205, 240, 68, 12, 179, 9, 44, 182, 182, 84, 27, 10, 151, 151, 109, 54, 251, 100, 1, 139, 248 }, new byte[] { 143, 214, 3, 20, 8, 29, 103, 25, 226, 160, 165, 230, 247, 101, 191, 93, 25, 15, 191, 137, 175, 22, 186, 249, 151, 69, 200, 158, 43, 92, 127, 69, 141, 89, 177, 174, 153, 4, 222, 112, 149, 249, 34, 66, 224, 214, 160, 159, 23, 203, 163, 135, 111, 54, 142, 139, 229, 220, 194, 113, 130, 178, 193, 218, 193, 220, 43, 135, 84, 245, 212, 33, 113, 98, 160, 212, 69, 207, 72, 207, 165, 116, 196, 138, 123, 47, 34, 74, 217, 254, 104, 23, 148, 121, 224, 10, 33, 245, 86, 102, 189, 247, 62, 193, 9, 130, 187, 205, 229, 89, 65, 61, 195, 135, 243, 238, 212, 119, 204, 175, 72, 227, 186, 98, 247, 130, 106, 130 } });
        }
    }
}
