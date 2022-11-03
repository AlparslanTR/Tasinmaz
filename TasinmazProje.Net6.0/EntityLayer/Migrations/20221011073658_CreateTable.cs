using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreLayer.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlAd = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Durum = table.Column<string>(type: "text", nullable: false),
                    IslemTipi = table.Column<string>(type: "text", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<string>(type: "text", nullable: false),
                    UserIp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ilces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlceName = table.Column<string>(type: "text", nullable: false),
                    IlId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ilces_Ils_IlId",
                        column: x => x.IlId,
                        principalTable: "Ils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mahalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MahalleAdi = table.Column<string>(type: "text", nullable: false),
                    IlceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mahalles_Ilces_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasinmazs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlId = table.Column<int>(type: "integer", nullable: false),
                    IlceId = table.Column<int>(type: "integer", nullable: false),
                    MahalleId = table.Column<int>(type: "integer", nullable: false),
                    Parsel = table.Column<int>(type: "integer", nullable: false),
                    Ada = table.Column<int>(type: "integer", nullable: false),
                    Nitelik = table.Column<string>(type: "text", nullable: false),
                    XCoordinate = table.Column<string>(type: "text", nullable: false),
                    YCoordinate = table.Column<string>(type: "text", nullable: false),
                    ParselCoordinate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasinmazs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasinmazs_Ilces_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasinmazs_Ils_IlId",
                        column: x => x.IlId,
                        principalTable: "Ils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasinmazs_Mahalles_MahalleId",
                        column: x => x.MahalleId,
                        principalTable: "Mahalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ilces_IlId",
                table: "Ilces",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalles_IlceId",
                table: "Mahalles",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmazs_IlceId",
                table: "Tasinmazs",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmazs_IlId",
                table: "Tasinmazs",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmazs_MahalleId",
                table: "Tasinmazs",
                column: "MahalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolId",
                table: "Users",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Tasinmazs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Mahalles");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "Ilces");

            migrationBuilder.DropTable(
                name: "Ils");
        }
    }
}
