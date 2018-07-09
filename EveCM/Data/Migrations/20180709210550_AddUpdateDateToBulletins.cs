using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EveCM.Data.Migrations
{
    public partial class AddUpdateDateToBulletins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Bulletins",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Bulletins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Bulletins");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Bulletins",
                newName: "Date");
        }
    }
}
