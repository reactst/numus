using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Numus.Server.Migrations
{
    /// <inheritdoc />
    public partial class added_CompanyId_to_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Companies_CompanyId",
                table: "Categories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Companies_CompanyId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Categories");
        }
    }
}
