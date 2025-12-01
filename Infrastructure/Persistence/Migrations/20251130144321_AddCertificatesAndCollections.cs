using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificatesAndCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jewelry_certificates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    certificate_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    issued_by = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    jewelry_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jewelry_certificates", x => x.id);
                    table.ForeignKey(
                        name: "fk_jewelry_certificates_jewelries_jewelry_id",
                        column: x => x.jewelry_id,
                        principalTable: "jewelries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jewelry_collections",
                columns: table => new
                {
                    collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    jewelry_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jewelry_collections", x => new { x.collection_id, x.jewelry_id });
                    table.ForeignKey(
                        name: "fk_jewelry_collections_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_jewelry_collections_jewelries_jewelry_id",
                        column: x => x.jewelry_id,
                        principalTable: "jewelries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_certificates_certificate_number",
                table: "jewelry_certificates",
                column: "certificate_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_certificates_jewelry_id",
                table: "jewelry_certificates",
                column: "jewelry_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_collections_jewelry_id",
                table: "jewelry_collections",
                column: "jewelry_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jewelry_certificates");

            migrationBuilder.DropTable(
                name: "jewelry_collections");

            migrationBuilder.DropTable(
                name: "collections");
        }
    }
}
