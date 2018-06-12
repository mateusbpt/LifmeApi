using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lifme.Repository.Migrations
{
    public partial class AdicionandocolunaFinished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                schema: "Lifme",
                table: "UserChallenge",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                schema: "Lifme",
                table: "Tournament",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                schema: "Lifme",
                table: "UserChallenge");

            migrationBuilder.DropColumn(
                name: "Finished",
                schema: "Lifme",
                table: "Tournament");
        }
    }
}
