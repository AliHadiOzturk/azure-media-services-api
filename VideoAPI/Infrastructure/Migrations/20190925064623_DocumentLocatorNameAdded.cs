using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoAPI.Infrastructure.Migrations
{
    public partial class DocumentLocatorNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "locatorname",
                schema: "novus",
                table: "document",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "locatorname",
                schema: "novus",
                table: "document");
        }
    }
}
