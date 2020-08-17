using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VideoAPI.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "novus");

            migrationBuilder.CreateTable(
                name: "document",
                schema: "novus",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filename = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    assetname = table.Column<string>(nullable: true),
                    encodedassetname = table.Column<string>(nullable: true),
                    encodejobname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "streamingurl",
                schema: "novus",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(nullable: true),
                    streamingprotocol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_streamingurl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "document_streamingurl",
                schema: "novus",
                columns: table => new
                {
                    documentid = table.Column<long>(nullable: false),
                    streamingurlid = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_streamingurl", x => new { x.documentid, x.streamingurlid });
                    table.ForeignKey(
                        name: "document_id",
                        column: x => x.documentid,
                        principalSchema: "novus",
                        principalTable: "document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "streamingurl_id",
                        column: x => x.streamingurlid,
                        principalSchema: "novus",
                        principalTable: "streamingurl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_streamingurl_streamingurlid",
                schema: "novus",
                table: "document_streamingurl",
                column: "streamingurlid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_streamingurl",
                schema: "novus");

            migrationBuilder.DropTable(
                name: "document",
                schema: "novus");

            migrationBuilder.DropTable(
                name: "streamingurl",
                schema: "novus");
        }
    }
}
