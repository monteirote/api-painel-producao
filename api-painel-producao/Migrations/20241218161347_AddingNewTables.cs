using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_painel_producao.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Users",
                newName: "StatusLastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeactivatedAt",
                table: "Users",
                newName: "DataLastModifiedAt");

            migrationBuilder.AddColumn<int>(
                name: "DataLastModifiedById",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusLastModifiedById",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedById = table.Column<int>(type: "INTEGER", nullable: false),
                    DeactivatedById = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customers_Users_DeactivatedById",
                        column: x => x.DeactivatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customers_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasurementUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    ValueByUnit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedForId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedById = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanceledAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CanceledById = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CreatedForId",
                        column: x => x.CreatedForId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CanceledById",
                        column: x => x.CanceledById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FramedArtworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Height = table.Column<float>(type: "REAL", nullable: false),
                    Width = table.Column<float>(type: "REAL", nullable: false),
                    ImageFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ImageFilePath = table.Column<string>(type: "TEXT", nullable: false),
                    ImageDescription = table.Column<string>(type: "TEXT", nullable: false),
                    GlassId = table.Column<int>(type: "INTEGER", nullable: false),
                    FrameId = table.Column<int>(type: "INTEGER", nullable: false),
                    BackgroundId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaperId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FramedArtworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FramedArtworks_Materials_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FramedArtworks_Materials_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FramedArtworks_Materials_GlassId",
                        column: x => x.GlassId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FramedArtworks_Materials_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FramedArtworks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DataLastModifiedById",
                table: "Users",
                column: "DataLastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusLastModifiedById",
                table: "Users",
                column: "StatusLastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedById",
                table: "Customers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeactivatedById",
                table: "Customers",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastModifiedById",
                table: "Customers",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FramedArtworks_BackgroundId",
                table: "FramedArtworks",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_FramedArtworks_FrameId",
                table: "FramedArtworks",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_FramedArtworks_GlassId",
                table: "FramedArtworks",
                column: "GlassId");

            migrationBuilder.CreateIndex(
                name: "IX_FramedArtworks_OrderId",
                table: "FramedArtworks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FramedArtworks_PaperId",
                table: "FramedArtworks",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CanceledById",
                table: "Orders",
                column: "CanceledById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedById",
                table: "Orders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedForId",
                table: "Orders",
                column: "CreatedForId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LastModifiedById",
                table: "Orders",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Reference",
                table: "Orders",
                column: "Reference",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_DataLastModifiedById",
                table: "Users",
                column: "DataLastModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_StatusLastModifiedById",
                table: "Users",
                column: "StatusLastModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_DataLastModifiedById",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_StatusLastModifiedById",
                table: "Users");

            migrationBuilder.DropTable(
                name: "FramedArtworks");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Users_DataLastModifiedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatusLastModifiedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DataLastModifiedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatusLastModifiedById",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "StatusLastModifiedAt",
                table: "Users",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DataLastModifiedAt",
                table: "Users",
                newName: "DeactivatedAt");
        }
    }
}
