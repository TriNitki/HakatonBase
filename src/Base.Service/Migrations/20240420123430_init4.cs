using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Service.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_moderated",
                table: "events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_event_id",
                table: "categories",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_events_event_id",
                table: "categories",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_events_event_id",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "ix_categories_event_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "is_moderated",
                table: "events");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "categories");
        }
    }
}
