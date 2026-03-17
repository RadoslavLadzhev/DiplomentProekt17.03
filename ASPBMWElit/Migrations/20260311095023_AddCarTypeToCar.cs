using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPBMWElit.Migrations
{
    /// <inheritdoc />
    public partial class AddCarTypeToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Cars");
        }
    }
}
