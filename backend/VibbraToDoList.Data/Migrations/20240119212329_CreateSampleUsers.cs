using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibbraToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSampleUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Users",
                new[] { "Id", "UserName" },
                new[] { Guid.NewGuid().ToString(), "jane" },
                new[] { Guid.NewGuid().ToString(), "john" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
