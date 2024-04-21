using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Service.Migrations
{
    /// <inheritdoc />
    public partial class merch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "points",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "reward",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "merch",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    stock = table.Column<long>(type: "bigint", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merch", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_to_merch",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    merch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    purchased_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_to_merch", x => new { x.user_id, x.merch_id });
                    table.ForeignKey(
                        name: "fk_user_to_merch_merch_merch_id",
                        column: x => x.merch_id,
                        principalTable: "merch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_to_merch_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_to_merch_merch_id",
                table: "user_to_merch",
                column: "merch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_to_merch");

            migrationBuilder.DropTable(
                name: "merch");

            migrationBuilder.DropColumn(
                name: "points",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reward",
                table: "events");
        }
    }
}
