using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VibbraToDoList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableToDoDropColumnParentToDoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentToDoId",
                table: "ToDos");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ToDoListId",
                table: "ToDos",
                column: "ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_ToDoLists_ToDoListId",
                table: "ToDos",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_ToDoLists_ToDoListId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ToDoListId",
                table: "ToDos");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentToDoId",
                table: "ToDos",
                type: "TEXT",
                nullable: true);
        }
    }
}
