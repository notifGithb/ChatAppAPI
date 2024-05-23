using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KullaniciSifresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesajs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GonderilmeZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GorulmeDurumu = table.Column<bool>(type: "bit", nullable: false),
                    GonderenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AliciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesajs_Kullanicis_AliciId",
                        column: x => x.AliciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mesajs_Kullanicis_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicis_KullaniciAdi",
                table: "Kullanicis",
                column: "KullaniciAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mesajs_AliciId",
                table: "Mesajs",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajs_GonderenId",
                table: "Mesajs",
                column: "GonderenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesajs");

            migrationBuilder.DropTable(
                name: "Kullanicis");
        }
    }
}
