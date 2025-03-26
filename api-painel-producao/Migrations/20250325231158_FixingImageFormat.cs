using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_painel_producao.Migrations
{
    /// <inheritdoc />
    public partial class FixingImageFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FramedArtworks",
                keyColumn: "ImageFile",
                keyValue: null,
                column: "ImageFile",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageFile",
                table: "FramedArtworks",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageFile",
                table: "FramedArtworks",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
