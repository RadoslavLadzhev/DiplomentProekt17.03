using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPBMWElit.Migrations
{
    /// <inheritdoc />
    public partial class CarTypeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descpription",
                table: "Cars",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cars",
                newName: "Descpription");
        }
    }
}
