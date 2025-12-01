using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jewelries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    jewelry_type = table.Column<string>(type: "text", nullable: false),
                    material = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jewelries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jewelry_care_schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    jewelry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    next_service_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    interval = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jewelry_care_schedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jewelry_orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    jewelry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    priority = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    scheduled_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jewelry_orders", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_jewelries_jewelry_type",
                table: "jewelries",
                column: "jewelry_type");

            migrationBuilder.CreateIndex(
                name: "ix_jewelries_status",
                table: "jewelries",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_care_schedules_is_active",
                table: "jewelry_care_schedules",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_care_schedules_jewelry_id",
                table: "jewelry_care_schedules",
                column: "jewelry_id");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_care_schedules_next_service_date",
                table: "jewelry_care_schedules",
                column: "next_service_date");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_orders_jewelry_id",
                table: "jewelry_orders",
                column: "jewelry_id");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_orders_order_number",
                table: "jewelry_orders",
                column: "order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_orders_scheduled_date",
                table: "jewelry_orders",
                column: "scheduled_date");

            migrationBuilder.CreateIndex(
                name: "ix_jewelry_orders_status",
                table: "jewelry_orders",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jewelries");

            migrationBuilder.DropTable(
                name: "jewelry_care_schedules");

            migrationBuilder.DropTable(
                name: "jewelry_orders");
        }
    }
}
