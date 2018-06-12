using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lifme.Repository.Migrations
{
    public partial class Migrationinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lifme");

            migrationBuilder.CreateTable(
                name: "Badge",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(maxLength: 1024, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Challenge",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Image = table.Column<string>(maxLength: 1024, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Email = table.Column<string>(maxLength: 1024, nullable: false),
                    Image = table.Column<string>(maxLength: 1024, nullable: true),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Nickname = table.Column<string>(maxLength: 255, nullable: true),
                    Password = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeLog",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Accept = table.Column<bool>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeLog_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "Lifme",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministratorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Image = table.Column<string>(maxLength: 1024, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_User_AdministratorId",
                        column: x => x.AdministratorId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(maxLength: 1024, nullable: true),
                    Message = table.Column<string>(maxLength: 1024, nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBadge",
                schema: "Lifme",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    BadgeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadge", x => new { x.UserId, x.BadgeId });
                    table.ForeignKey(
                        name: "FK_UserBadge_Badge_BadgeId",
                        column: x => x.BadgeId,
                        principalSchema: "Lifme",
                        principalTable: "Badge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBadge_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserChallenge",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Accept = table.Column<bool>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    DayChallenge = table.Column<DateTime>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallenge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChallenge_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "Lifme",
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserChallenge_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFriend",
                schema: "Lifme",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriend", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_UserFriend_User_FriendId",
                        column: x => x.FriendId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFriend_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPending",
                schema: "Lifme",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    PendingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPending", x => new { x.UserId, x.PendingId });
                    table.ForeignKey(
                        name: "FK_UserPending_User_PendingId",
                        column: x => x.PendingId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPending_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "Lifme",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Lifme",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostLike",
                schema: "Lifme",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLike", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostLike_Post_PostId",
                        column: x => x.PostId,
                        principalSchema: "Lifme",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostLike_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Lifme",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                schema: "Lifme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    Image = table.Column<string>(maxLength: 1024, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    WinnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournament_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Lifme",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournament_Post_WinnerId",
                        column: x => x.WinnerId,
                        principalSchema: "Lifme",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeLog_ChallengeId",
                schema: "Lifme",
                table: "ChallengeLog",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_AdministratorId",
                schema: "Lifme",
                table: "Group",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                schema: "Lifme",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_UserId",
                schema: "Lifme",
                table: "PostLike",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_GroupId",
                schema: "Lifme",
                table: "Tournament",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_WinnerId",
                schema: "Lifme",
                table: "Tournament",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadge_BadgeId",
                schema: "Lifme",
                table: "UserBadge",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenge_ChallengeId",
                schema: "Lifme",
                table: "UserChallenge",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenge_UserId",
                schema: "Lifme",
                table: "UserChallenge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_FriendId",
                schema: "Lifme",
                table: "UserFriend",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                schema: "Lifme",
                table: "UserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPending_PendingId",
                schema: "Lifme",
                table: "UserPending",
                column: "PendingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeLog",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "PostLike",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "Tournament",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "UserBadge",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "UserChallenge",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "UserFriend",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "UserPending",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "Badge",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "Challenge",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Lifme");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Lifme");
        }
    }
}
