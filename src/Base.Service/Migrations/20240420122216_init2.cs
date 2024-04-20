using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Service.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_event_guests_events_event_id1",
                table: "event_guests");

            migrationBuilder.DropIndex(
                name: "ix_event_guests_event_id1",
                table: "event_guests");

            migrationBuilder.DropColumn(
                name: "event_id1",
                table: "event_guests");

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_event_id",
                table: "users",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_events_event_id",
                table: "users",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_events_event_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_event_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "event_id1",
                table: "event_guests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_event_guests_event_id1",
                table: "event_guests",
                column: "event_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_event_guests_events_event_id1",
                table: "event_guests",
                column: "event_id1",
                principalTable: "events",
                principalColumn: "id");
        }
    }
}
