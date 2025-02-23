using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LRNNo",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_IsDeleted",
                table: "Venues",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_IsDeleted",
                table: "Teams",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_IsDeleted",
                table: "Sports",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SportCategories_IsDeleted",
                table: "SportCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CreatedAt",
                table: "Players",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Players_IsDeleted",
                table: "Players",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Players_LRNNo",
                table: "Players",
                column: "LRNNo");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IsDeleted",
                table: "Events",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Venues_IsDeleted",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Teams_IsDeleted",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Sports_IsDeleted",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_SportCategories_IsDeleted",
                table: "SportCategories");

            migrationBuilder.DropIndex(
                name: "IX_Players_CreatedAt",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_IsDeleted",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_LRNNo",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Events_IsDeleted",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "LRNNo",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
