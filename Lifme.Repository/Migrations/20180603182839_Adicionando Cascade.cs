using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lifme.Repository.Migrations
{
    public partial class AdicionandoCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Post_PostId",
                schema: "Lifme",
                table: "PostLike");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Post_PostId",
                schema: "Lifme",
                table: "PostLike",
                column: "PostId",
                principalSchema: "Lifme",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Post_PostId",
                schema: "Lifme",
                table: "PostLike");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Post_PostId",
                schema: "Lifme",
                table: "PostLike",
                column: "PostId",
                principalSchema: "Lifme",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
