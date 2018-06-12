using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lifme.Repository.Migrations
{
    public partial class Ajustandoalgumascolunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_Post_WinnerId",
                schema: "Lifme",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "Lifme",
                table: "Tournament");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                schema: "Lifme",
                table: "UserChallenge",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                schema: "Lifme",
                table: "Tournament",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_User_WinnerId",
                schema: "Lifme",
                table: "Tournament",
                column: "WinnerId",
                principalSchema: "Lifme",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_User_WinnerId",
                schema: "Lifme",
                table: "Tournament");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                schema: "Lifme",
                table: "UserChallenge",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                schema: "Lifme",
                table: "Tournament",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                schema: "Lifme",
                table: "Tournament",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_Post_WinnerId",
                schema: "Lifme",
                table: "Tournament",
                column: "WinnerId",
                principalSchema: "Lifme",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
