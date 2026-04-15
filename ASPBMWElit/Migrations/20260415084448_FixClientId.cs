using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPBMWElit.Migrations
{
    /// <inheritdoc />
    public partial class FixClientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquirings_AspNetUsers_ClientId",
                table: "Inquirings");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Inquirings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquirings_AspNetUsers_ClientId",
                table: "Inquirings",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquirings_AspNetUsers_ClientId",
                table: "Inquirings");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Inquirings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquirings_AspNetUsers_ClientId",
                table: "Inquirings",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
