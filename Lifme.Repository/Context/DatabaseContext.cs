using Lifme.Domain.Entity;
using Lifme.Domain.Entity.Log;
using Lifme.Domain.Entity.Relation;
using Lifme.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BadgeMapping());
            modelBuilder.ApplyConfiguration(new ChallengeLogMapping());
            modelBuilder.ApplyConfiguration(new ChallengeMapping());
            modelBuilder.ApplyConfiguration(new GroupMapping());
            modelBuilder.ApplyConfiguration(new GroupUserMapping());
            modelBuilder.ApplyConfiguration(new PostMapping());
            modelBuilder.ApplyConfiguration(new PostLikeMapping());
            modelBuilder.ApplyConfiguration(new TournamentMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new UserBadgeMapping());
            modelBuilder.ApplyConfiguration(new UserChallengeMapping());
            modelBuilder.ApplyConfiguration(new UserFriendMapping());
            modelBuilder.ApplyConfiguration(new UserPendingMapping());
        }

        public DbSet<Badge> Badges { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<ChallengeLog> ChallengeLogs { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupUser> GroupUsers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserBadge> UserBadges { get; set;  }

        public DbSet<UserChallenge> UserChallenges { get; set; }

        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<UserPending> UserPendings { get; set; }
    }
}
