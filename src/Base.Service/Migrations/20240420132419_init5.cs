using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Service.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start_dt",
                table: "events",
                newName: "start_at");

            migrationBuilder.AddColumn<DateTime>(
                name: "published_at",
                table: "events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "published_at",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "start_at",
                table: "events",
                newName: "start_dt");
        }
    }
}
