using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Setupcurrentloanrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loanRequest_Status",
                table: "books",
                newName: "currentLoanRequest_Status");

            migrationBuilder.RenameColumn(
                name: "loanRequest_RequestingUser",
                table: "books",
                newName: "currentLoanRequest_RequestingUser");

            migrationBuilder.RenameColumn(
                name: "loanRequest_Id",
                table: "books",
                newName: "currentLoanRequest_Id");

            migrationBuilder.RenameColumn(
                name: "loanRequest_CreatedAt",
                table: "books",
                newName: "currentLoanRequest_CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_Status",
                table: "books",
                newName: "loanRequest_Status");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_RequestingUser",
                table: "books",
                newName: "loanRequest_RequestingUser");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_Id",
                table: "books",
                newName: "loanRequest_Id");

            migrationBuilder.RenameColumn(
                name: "currentLoanRequest_CreatedAt",
                table: "books",
                newName: "loanRequest_CreatedAt");
        }
    }
}
