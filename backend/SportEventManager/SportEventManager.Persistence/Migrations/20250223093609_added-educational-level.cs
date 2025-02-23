using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportEventManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addededucationallevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Players",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Players",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "EducationalLevelID");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Players",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LRNNo",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationalLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalLevels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EducationalLevels",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), "System", false, "Elementary", null, "System" },
                    { 2, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), "System", false, "High School", null, "System" },
                    { 3, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), "System", false, "Paralympics", null, "System" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_EducationalLevelID",
                table: "Players",
                column: "EducationalLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalLevels_Name",
                table: "EducationalLevels",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_EducationalLevels_EducationalLevelID",
                table: "Players",
                column: "EducationalLevelID",
                principalTable: "EducationalLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_EducationalLevels_EducationalLevelID",
                table: "Players");

            migrationBuilder.DropTable(
                name: "EducationalLevels");

            migrationBuilder.DropIndex(
                name: "IX_Players_EducationalLevelID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LRNNo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Players",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "EducationalLevelID",
                table: "Players",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Players",
                newName: "RegistrationDate");
        }
    }
}
