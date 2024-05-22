using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig_055 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MesajOutboxes");

            migrationBuilder.DropColumn(
                name: "GonderilmeDurumu",
                table: "Mesajs");

            migrationBuilder.AlterColumn<string>(
                name: "KullaniciAdi",
                table: "Kullanicis",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicis_KullaniciAdi",
                table: "Kullanicis",
                column: "KullaniciAdi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kullanicis_KullaniciAdi",
                table: "Kullanicis");

            migrationBuilder.AddColumn<bool>(
                name: "GonderilmeDurumu",
                table: "Mesajs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "KullaniciAdi",
                table: "Kullanicis",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "MesajOutboxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesajId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GonderenId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesajOutboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesajOutboxes_Mesajs_MesajId",
                        column: x => x.MesajId,
                        principalTable: "Mesajs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MesajOutboxes_MesajId",
                table: "MesajOutboxes",
                column: "MesajId");
        }
    }
}
