using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibbraToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableToDos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    ToDoListId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParentToDoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ToDoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDos_ToDos_ToDoId",
                        column: x => x.ToDoId,
                        principalTable: "ToDos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoListsUsers_ToDoListId",
                table: "ToDoListsUsers",
                column: "ToDoListId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ToDoId",
                table: "ToDos",
                column: "ToDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoListsUsers_ToDoLists_ToDoListId",
                table: "ToDoListsUsers",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoListsUsers_ToDoLists_ToDoListId",
                table: "ToDoListsUsers");

            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDoListsUsers_ToDoListId",
                table: "ToDoListsUsers");
        }
    }
}
