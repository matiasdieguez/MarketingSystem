using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingSystem.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Contacts");
        }
    }
}
