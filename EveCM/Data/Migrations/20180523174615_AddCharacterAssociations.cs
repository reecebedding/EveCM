using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EveCM.Data.Migrations
{
    public partial class AddCharacterAssociations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterDetails",
                columns: table => new
                {
                    CharacterID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountID = table.Column<string>(nullable: true),
                    CharacterName = table.Column<string>(nullable: true),
                    CharacterOwnerHash = table.Column<string>(nullable: true),
                    ExpiresOn = table.Column<DateTime>(nullable: false),
                    TokenType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterDetails", x => x.CharacterID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterDetails");
        }
    }
}
