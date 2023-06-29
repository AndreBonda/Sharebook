using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_loan_request_requesting_user",
                table: "books");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "books");

            migrationBuilder.AddColumn<Guid>(
                name: "current_loan_request_requesting_user_id",
                table: "books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_books_owner_id",
                table: "books",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_users_user_id",
                table: "books",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_users_user_id",
                table: "books");

            migrationBuilder.DropIndex(
                name: "ix_books_owner_id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "current_loan_request_requesting_user_id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "books");

            migrationBuilder.AddColumn<string>(
                name: "current_loan_request_requesting_user",
                table: "books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "books",
                type: "text",
                nullable: true);
        }
    }
}
