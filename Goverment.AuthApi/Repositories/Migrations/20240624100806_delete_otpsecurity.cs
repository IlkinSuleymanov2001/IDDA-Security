using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class delete_otpsecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOtpSecurities_users_userid",
                table: "UserOtpSecurities");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "UserOtpSecurities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserOtpSecurities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 115, 158, 237, 30, 13, 244, 54, 12, 208, 48, 81, 139, 163, 188, 124, 110, 5, 69, 177, 132, 122, 234, 80, 132, 159, 116, 146, 233, 58, 131, 240, 73, 210, 167, 176, 32, 205, 43, 242, 29, 244, 51, 42, 25, 54, 241, 228, 253, 45, 196, 35, 218, 67, 34, 134, 4, 214, 192, 114, 21, 245, 179, 63, 183 }, new byte[] { 127, 54, 19, 63, 101, 131, 159, 243, 248, 1, 166, 169, 5, 100, 73, 143, 226, 111, 229, 72, 176, 116, 76, 195, 81, 208, 124, 248, 75, 193, 74, 74, 97, 106, 106, 216, 148, 155, 200, 146, 70, 213, 238, 41, 201, 102, 162, 238, 125, 88, 250, 4, 146, 73, 3, 78, 118, 176, 129, 201, 170, 227, 23, 165, 126, 57, 166, 210, 33, 39, 5, 68, 129, 243, 137, 253, 203, 254, 126, 222, 221, 58, 2, 10, 250, 85, 160, 186, 121, 64, 72, 234, 147, 148, 186, 214, 211, 248, 25, 112, 115, 100, 171, 86, 109, 176, 236, 57, 68, 154, 155, 139, 72, 188, 228, 211, 12, 128, 244, 246, 147, 32, 66, 162, 48, 193, 95, 215 } });

            migrationBuilder.CreateIndex(
                name: "IX_UserOtpSecurities_UserId1",
                table: "UserOtpSecurities",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOtpSecurities_users_UserId1",
                table: "UserOtpSecurities",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOtpSecurities_users_UserId1",
                table: "UserOtpSecurities");

            migrationBuilder.DropIndex(
                name: "IX_UserOtpSecurities_UserId1",
                table: "UserOtpSecurities");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserOtpSecurities");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "UserOtpSecurities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 51, 112, 80, 111, 92, 112, 168, 192, 108, 88, 71, 140, 229, 225, 52, 22, 41, 206, 194, 52, 213, 250, 70, 188, 189, 96, 59, 95, 127, 125, 52, 211, 33, 31, 147, 24, 59, 120, 231, 156, 83, 37, 16, 178, 39, 167, 21, 18, 113, 179, 199, 76, 109, 171, 112, 16, 47, 176, 122, 12, 20, 218, 34, 162 }, new byte[] { 59, 115, 192, 27, 117, 241, 46, 9, 136, 226, 15, 235, 66, 132, 209, 43, 246, 32, 227, 31, 39, 42, 112, 83, 106, 117, 20, 90, 91, 146, 116, 237, 22, 253, 226, 144, 166, 207, 200, 195, 217, 66, 57, 229, 97, 111, 233, 88, 160, 75, 199, 233, 50, 184, 216, 115, 105, 22, 146, 5, 186, 16, 184, 24, 225, 63, 11, 153, 86, 72, 225, 194, 167, 125, 175, 155, 230, 123, 153, 144, 114, 213, 226, 44, 132, 223, 220, 13, 99, 7, 127, 158, 207, 58, 89, 201, 31, 95, 89, 2, 33, 12, 27, 4, 213, 179, 195, 69, 174, 249, 234, 143, 170, 5, 246, 159, 194, 162, 64, 176, 227, 79, 125, 167, 14, 20, 158, 188 } });

            migrationBuilder.AddForeignKey(
                name: "FK_UserOtpSecurities_users_userid",
                table: "UserOtpSecurities",
                column: "userid",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
