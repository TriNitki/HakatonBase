using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Service.Migrations
{
    /// <inheritdoc />
    public partial class new_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_to_merch",
                table: "user_to_merch");

            migrationBuilder.AlterColumn<double>(
                name: "points",
                table: "users",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_to_merch",
                table: "user_to_merch",
                columns: new[] { "user_id", "merch_id", "purchased_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_to_merch",
                table: "user_to_merch");

            migrationBuilder.AlterColumn<int>(
                name: "points",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_to_merch",
                table: "user_to_merch",
                columns: new[] { "user_id", "merch_id" });
        }
    }
}
