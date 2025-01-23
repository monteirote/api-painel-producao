using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_painel_producao.Migrations
{
    /// <inheritdoc />
    public partial class CreatingRelationsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_BackgroundId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_FrameId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_GlassId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_PaperId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Orders_OrderId",
                table: "FramedArtworks");

            migrationBuilder.AlterColumn<int>(
                name: "PaperId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GlassId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "FrameId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BackgroundId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_BackgroundId",
                table: "FramedArtworks",
                column: "BackgroundId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_FrameId",
                table: "FramedArtworks",
                column: "FrameId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_GlassId",
                table: "FramedArtworks",
                column: "GlassId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_PaperId",
                table: "FramedArtworks",
                column: "PaperId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Orders_OrderId",
                table: "FramedArtworks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_BackgroundId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_FrameId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_GlassId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Materials_PaperId",
                table: "FramedArtworks");

            migrationBuilder.DropForeignKey(
                name: "FK_FramedArtworks_Orders_OrderId",
                table: "FramedArtworks");

            migrationBuilder.AlterColumn<int>(
                name: "PaperId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GlassId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FrameId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BackgroundId",
                table: "FramedArtworks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_BackgroundId",
                table: "FramedArtworks",
                column: "BackgroundId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_FrameId",
                table: "FramedArtworks",
                column: "FrameId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_GlassId",
                table: "FramedArtworks",
                column: "GlassId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Materials_PaperId",
                table: "FramedArtworks",
                column: "PaperId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FramedArtworks_Orders_OrderId",
                table: "FramedArtworks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
