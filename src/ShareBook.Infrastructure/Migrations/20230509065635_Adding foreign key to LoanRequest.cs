using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingforeignkeytoLoanRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loanRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "loanRequest_CreatedAt",
                table: "books",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "loanRequest_Id",
                table: "books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loanRequest_RequestingUser",
                table: "books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "loanRequest_Status",
                table: "books",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loanRequest_CreatedAt",
                table: "books");

            migrationBuilder.DropColumn(
                name: "loanRequest_Id",
                table: "books");

            migrationBuilder.DropColumn(
                name: "loanRequest_RequestingUser",
                table: "books");

            migrationBuilder.DropColumn(
                name: "loanRequest_Status",
                table: "books");

            migrationBuilder.CreateTable(
                name: "loanRequests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    requestingUser = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_loanRequests", x => x.id);
                });
        }
    }
}
