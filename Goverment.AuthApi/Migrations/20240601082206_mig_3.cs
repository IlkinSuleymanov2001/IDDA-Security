using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goverment.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginSecurities",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false),
                    LoginRetryCount = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    isBlock = table.Column<bool>(type: "bit", maxLength: 5, nullable: true),
                    AccountBlockedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountUnBlockedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginSecurities", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserLoginSecurities_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 203, 149, 56, 184, 51, 20, 91, 247, 215, 120, 17, 45, 236, 76, 92, 5, 103, 162, 27, 63, 50, 67, 200, 146, 57, 104, 31, 237, 54, 146, 58, 136, 1, 35, 14, 180, 83, 169, 211, 25, 224, 247, 228, 233, 59, 247, 24, 253, 108, 32, 47, 61, 58, 17, 161, 234, 235, 80, 119, 172, 60, 50, 158, 171 }, new byte[] { 50, 83, 181, 104, 165, 26, 123, 131, 35, 22, 117, 16, 186, 232, 99, 62, 152, 223, 5, 121, 36, 18, 179, 72, 207, 148, 33, 54, 204, 113, 30, 37, 138, 47, 14, 110, 106, 43, 78, 89, 236, 85, 189, 211, 62, 223, 26, 53, 2, 45, 165, 146, 255, 222, 48, 77, 98, 121, 30, 194, 74, 25, 37, 105, 200, 185, 151, 35, 86, 175, 34, 22, 204, 104, 160, 155, 10, 85, 236, 117, 123, 113, 47, 133, 229, 245, 162, 101, 148, 225, 82, 252, 109, 173, 141, 115, 161, 188, 60, 96, 67, 223, 183, 87, 212, 188, 61, 231, 19, 80, 15, 123, 26, 56, 152, 85, 133, 87, 245, 71, 251, 108, 158, 178, 25, 95, 56, 218 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoginSecurities");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "passwordhash", "passwordsalt" },
                values: new object[] { new byte[] { 132, 170, 50, 121, 54, 131, 120, 98, 129, 112, 96, 126, 73, 21, 212, 245, 15, 78, 80, 22, 58, 225, 11, 122, 6, 166, 58, 136, 202, 30, 106, 141, 239, 184, 177, 199, 18, 99, 83, 163, 123, 150, 24, 78, 232, 101, 191, 93, 136, 172, 11, 62, 230, 186, 223, 108, 14, 176, 60, 229, 96, 100, 227, 129 }, new byte[] { 32, 60, 125, 255, 137, 41, 202, 82, 108, 82, 89, 107, 48, 196, 5, 151, 223, 193, 168, 253, 172, 150, 73, 91, 123, 214, 193, 44, 247, 97, 16, 81, 242, 170, 148, 160, 50, 251, 113, 51, 14, 152, 167, 98, 8, 30, 11, 109, 56, 156, 154, 195, 197, 31, 51, 83, 109, 22, 188, 219, 41, 53, 242, 92, 0, 234, 63, 122, 44, 201, 19, 6, 146, 120, 119, 231, 104, 107, 154, 193, 176, 203, 178, 161, 89, 150, 107, 20, 51, 161, 147, 48, 27, 238, 114, 136, 156, 132, 54, 15, 247, 77, 7, 206, 145, 189, 36, 10, 102, 12, 54, 235, 143, 131, 173, 27, 187, 21, 105, 166, 203, 103, 67, 60, 20, 106, 149, 225 } });
        }
    }
}
