using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OriginalUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ShortUrl = table.Column<string>(type: "TEXT", nullable: true),
                    FullUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Browser = table.Column<string>(type: "TEXT", nullable: true),
                    Platform = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UrlId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlMetrics_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlMetrics_UrlId",
                table: "UrlMetrics",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlMetrics");

            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
