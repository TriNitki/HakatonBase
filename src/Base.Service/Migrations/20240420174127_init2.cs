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
                name: "fk_categories_events_event_id",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_users_user_id",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_events_users_creator_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_users_events_event_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_event_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_events_creator_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_categories_event_id",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "ix_categories_user_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_event_id",
                table: "users",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_creator_id",
                table: "events",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_event_id",
                table: "categories",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_user_id",
                table: "categories",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_events_event_id",
                table: "categories",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_users_user_id",
                table: "categories",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_users_creator_id",
                table: "events",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_events_event_id",
                table: "users",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id");
        }
    }
}
