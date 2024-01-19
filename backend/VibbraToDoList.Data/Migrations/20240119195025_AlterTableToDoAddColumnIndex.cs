using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibbraToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableToDoAddColumnIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "ToDos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "ToDos");
        }
    }
}
