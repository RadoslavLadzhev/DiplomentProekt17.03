using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPBMWElit.Migrations
{
    /// <inheritdoc />
    public partial class FixCarInquiringRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Inquirings_InquiringsId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_InquiringsId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InquiringsId",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Inquirings_CarId",
                table: "Inquirings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquirings_Cars_CarId",
                table: "Inquirings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquirings_Cars_CarId",
                table: "Inquirings");

            migrationBuilder.DropIndex(
                name: "IX_Inquirings_CarId",
                table: "Inquirings");

            migrationBuilder.AddColumn<int>(
                name: "InquiringsId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_InquiringsId",
                table: "Cars",
                column: "InquiringsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Inquirings_InquiringsId",
                table: "Cars",
                column: "InquiringsId",
                principalTable: "Inquirings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
