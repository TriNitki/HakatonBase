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
            migrationBuilder.DropForeignKey(
                name: "fk_events_users_user_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_user_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "events");

            migrationBuilder.CreateIndex(
                name: "ix_events_creator_id",
                table: "events",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_users_user_id",
                table: "events",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_users_user_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_creator_id",
                table: "events");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "events",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_events_user_id",
                table: "events",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_users_user_id",
                table: "events",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
