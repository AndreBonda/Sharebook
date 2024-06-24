using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerFkToBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserDataId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDataId",
                table: "Users",
                column: "UserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_OwnerId",
                table: "Books",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_OwnerId",
                table: "Books",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserDataId",
                table: "Users",
                column: "UserDataId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_OwnerId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserDataId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserDataId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Books_OwnerId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Books");
        }
    }
}
