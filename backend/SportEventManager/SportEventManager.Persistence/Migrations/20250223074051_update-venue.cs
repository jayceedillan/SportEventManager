using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatevenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Venues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
