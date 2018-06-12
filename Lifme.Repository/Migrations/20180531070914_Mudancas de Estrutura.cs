using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lifme.Repository.Migrations
{
    public partial class MudancasdeEstrutura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                schema: "Lifme",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Image",
                schema: "Lifme",
                table: "User",
                newName: "Background");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                schema: "Lifme",
                table: "User",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "Lifme",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "Lifme",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "Lifme",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Background",
                schema: "Lifme",
                table: "User",
                newName: "Image");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                schema: "Lifme",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
