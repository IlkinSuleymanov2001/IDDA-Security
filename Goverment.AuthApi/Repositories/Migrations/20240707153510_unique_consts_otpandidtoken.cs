using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class unique_consts_otpandidtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOtpSecurities");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 235, 0, 239, 153, 97, 4, 169, 108, 70, 84, 210, 57, 226, 139, 212, 46, 139, 140, 233, 180, 250, 128, 192, 243, 197, 101, 55, 99, 178, 234, 97, 114, 217, 243, 112, 14, 92, 34, 9, 170, 4, 243, 37, 207, 221, 170, 216, 143, 140, 164, 35, 225, 0, 217, 206, 158, 155, 184, 135, 8, 83, 11, 138, 55 }, new byte[] { 140, 148, 60, 194, 238, 249, 110, 0, 128, 131, 19, 200, 7, 23, 10, 109, 185, 95, 230, 68, 72, 57, 47, 227, 243, 18, 117, 180, 9, 236, 69, 201, 69, 2, 81, 162, 213, 113, 55, 183, 167, 185, 176, 59, 174, 8, 203, 250, 79, 37, 150, 64, 12, 196, 16, 226, 148, 200, 54, 61, 149, 29, 62, 86, 16, 106, 141, 215, 116, 179, 124, 8, 192, 16, 194, 102, 108, 188, 184, 230, 47, 109, 208, 81, 90, 233, 120, 122, 53, 81, 2, 235, 159, 251, 235, 139, 96, 22, 222, 97, 231, 223, 219, 107, 120, 129, 143, 184, 139, 207, 192, 150, 104, 32, 241, 156, 39, 233, 225, 76, 160, 235, 166, 228, 180, 110, 180, 182 } });

            migrationBuilder.CreateIndex(
                name: "IX_users_idtoken",
                table: "users",
                column: "idtoken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_otpcode",
                table: "users",
                column: "otpcode",
                unique: true);
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

            migrationBuilder.CreateTable(
                name: "UserOtpSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId1 = table.Column<int>(type: "integer", nullable: false),
                    tryotpcount = table.Column<int>(type: "integer", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtpSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserOtpSecurities_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "PasswordSalt" },
                values: new object[] { new byte[] { 102, 64, 139, 87, 45, 137, 206, 127, 248, 102, 6, 75, 143, 37, 102, 6, 166, 5, 101, 71, 226, 17, 44, 182, 214, 227, 215, 99, 89, 243, 26, 144, 184, 229, 72, 9, 4, 95, 245, 77, 231, 87, 220, 105, 47, 100, 102, 243, 67, 186, 33, 227, 210, 212, 80, 233, 31, 59, 14, 218, 17, 61, 196, 231 }, new byte[] { 77, 6, 138, 193, 242, 38, 236, 207, 255, 107, 120, 26, 158, 125, 172, 171, 144, 138, 243, 247, 38, 78, 67, 162, 196, 204, 46, 123, 168, 195, 74, 162, 38, 5, 135, 17, 153, 250, 8, 184, 44, 149, 77, 151, 243, 27, 153, 39, 51, 218, 62, 255, 93, 57, 198, 6, 219, 66, 209, 245, 7, 91, 172, 106, 138, 221, 240, 44, 82, 87, 153, 80, 251, 198, 252, 232, 44, 37, 90, 145, 33, 170, 98, 232, 45, 4, 64, 199, 206, 124, 107, 5, 29, 12, 189, 206, 241, 74, 118, 129, 85, 222, 11, 118, 244, 31, 60, 41, 13, 1, 6, 22, 16, 142, 52, 130, 239, 135, 225, 178, 56, 217, 174, 68, 44, 12, 241, 119 } });

            migrationBuilder.CreateIndex(
                name: "IX_UserOtpSecurities_UserId1",
                table: "UserOtpSecurities",
                column: "UserId1");
        }
    }
}
