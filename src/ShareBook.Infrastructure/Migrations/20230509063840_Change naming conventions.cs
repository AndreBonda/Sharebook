using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changenamingconventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SharedByOwner",
                table: "books",
                newName: "sharedByOwner");

            migrationBuilder.RenameColumn(
                name: "Pages",
                table: "books",
                newName: "pages");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "books",
                newName: "owner");

            migrationBuilder.RenameColumn(
                name: "Labels",
                table: "books",
                newName: "labels");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "books",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "books",
                newName: "author");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "books",
                newName: "id");

            migrationBuilder.AddColumn<DateTime>(
                name: "currentLoanRequest_CreatedAt",
                table: "books",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "currentLoanRequest_Id",
                table: "books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "currentLoanRequest_RequestingUser",
                table: "books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "currentLoanRequest_Status",
                table: "books",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pK_books",
                table: "books",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pK_books",
                table: "books");

            migrationBuilder.DropColumn(
                name: "currentLoanRequest_CreatedAt",
                table: "books");

            migrationBuilder.DropColumn(
                name: "currentLoanRequest_Id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "currentLoanRequest_RequestingUser",
                table: "books");

            migrationBuilder.DropColumn(
                name: "currentLoanRequest_Status",
                table: "books");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sharedByOwner",
                table: "Books",
                newName: "SharedByOwner");

            migrationBuilder.RenameColumn(
                name: "pages",
                table: "Books",
                newName: "Pages");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "Books",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "labels",
                table: "Books",
                newName: "Labels");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Books",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LoanRequests",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestingUser = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequests", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_LoanRequests_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
