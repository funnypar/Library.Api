using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditAuthorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthorDetails_AuthorId",
                table: "AuthorDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "AuthorDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AuthorDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDetails_AuthorId",
                table: "AuthorDetails",
                column: "AuthorId",
                unique: true,
                filter: "[AuthorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthorDetails_AuthorId",
                table: "AuthorDetails");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AuthorDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "AuthorDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDetails_AuthorId",
                table: "AuthorDetails",
                column: "AuthorId",
                unique: true);
        }
    }
}
