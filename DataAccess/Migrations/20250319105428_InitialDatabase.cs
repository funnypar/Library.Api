using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBookTag_BookTags_BookTagsId",
                table: "BookBookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBookTag_Books_BooksId",
                table: "BookBookTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBookTag",
                table: "BookBookTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook");

            migrationBuilder.RenameTable(
                name: "BookBookTag",
                newName: "BookTagsRelation");

            migrationBuilder.RenameTable(
                name: "AuthorBook",
                newName: "AuthorsRelation");

            migrationBuilder.RenameIndex(
                name: "IX_BookBookTag_BooksId",
                table: "BookTagsRelation",
                newName: "IX_BookTagsRelation_BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorsRelation",
                newName: "IX_AuthorsRelation_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTagsRelation",
                table: "BookTagsRelation",
                columns: new[] { "BookTagsId", "BooksId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorsRelation",
                table: "AuthorsRelation",
                columns: new[] { "AuthorsId", "BooksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsRelation_Authors_AuthorsId",
                table: "AuthorsRelation",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsRelation_Books_BooksId",
                table: "AuthorsRelation",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTagsRelation_BookTags_BookTagsId",
                table: "BookTagsRelation",
                column: "BookTagsId",
                principalTable: "BookTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTagsRelation_Books_BooksId",
                table: "BookTagsRelation",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsRelation_Authors_AuthorsId",
                table: "AuthorsRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsRelation_Books_BooksId",
                table: "AuthorsRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTagsRelation_BookTags_BookTagsId",
                table: "BookTagsRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTagsRelation_Books_BooksId",
                table: "BookTagsRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTagsRelation",
                table: "BookTagsRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorsRelation",
                table: "AuthorsRelation");

            migrationBuilder.RenameTable(
                name: "BookTagsRelation",
                newName: "BookBookTag");

            migrationBuilder.RenameTable(
                name: "AuthorsRelation",
                newName: "AuthorBook");

            migrationBuilder.RenameIndex(
                name: "IX_BookTagsRelation_BooksId",
                table: "BookBookTag",
                newName: "IX_BookBookTag_BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorsRelation_BooksId",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBookTag",
                table: "BookBookTag",
                columns: new[] { "BookTagsId", "BooksId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookTag_BookTags_BookTagsId",
                table: "BookBookTag",
                column: "BookTagsId",
                principalTable: "BookTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookTag_Books_BooksId",
                table: "BookBookTag",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
