using Lifme.Domain.Entity;
using Lifme.Domain.Entity.Relation;
using Lifme.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifme.Service
{
    public class BadgeService
    {
        private readonly DatabaseContext _context;

        public BadgeService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task VerifyNewBadges(User user)
        {
            //Todas as Badges
            var badges = await _context.Badges
                .ToListAsync();

            // Badges do Usuário
            var userBadges = await _context.UserBadges
                .Include(x => x.User)
                .Include(x => x.Badge)
                .Where(x => x.User.Id == user.Id)
                .ToListAsync();

            // Recebeu 5, 20, 50 curtidas nos posts.
            var likeCount = await _context.PostLikes
                .Include(x => x.Post)
                .ThenInclude(x => x.User)
                .CountAsync(x => x.Post.User.Id == user.Id);

            // Curtiu 5, 10, 20 posts.
            var myLikeCount = await _context.PostLikes
                .Include(x => x.User)
                .CountAsync(x => x.User.Id == user.Id);

            // Compartilhou 3, 5, 10 experiências/dicas.
            var postCount = await _context.Posts
                .Include(x => x.User)
                .Where(x => x.User.Id == user.Id)
                .CountAsync();

            // Participou de um grupo de amigos.
            var hasGroup = await _context.GroupUsers
                .Include(x => x.User)
                .AnyAsync(x => x.User.Id == user.Id);

            // Fez 5, 10, 50 amigos.
            var friendsCount = await _context.UserFriends
                .Include(x => x.User)
                .CountAsync(x => x.User.Id == user.Id);

            // Venceu 1, 5, 10 torneios.
            var winnerTournamentCount = await _context.Tournaments
                .Include(x => x.Winner)
                .CountAsync(x => x.Winner.Id == user.Id);

            // Completou 5, 10, 20 desafios.
            var challengesUserFinishCount = await _context.UserChallenges
                .Include(x => x.User)
                .CountAsync(x => x.Completed && x.User.Id == user.Id);

            // Concluiu o cadastro.    
            var userRegisterFinish = user.Description != null;

            // Entrou pela primeira vez. 
            var firstLogin = !userBadges.Any(x => x.Badge.Id == 21);

            // Badges de desafio
            if (challengesUserFinishCount >= 5 && !userBadges.Any(x => x.Badge.Id == 1))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(0) });
            }
            else if (challengesUserFinishCount >= 10 && !userBadges.Any(x => x.Badge.Id == 2))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(1) });
            }
            else if (challengesUserFinishCount >= 20 && !userBadges.Any(x => x.Badge.Id == 3))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(2) });
            }

            // Badges de Amigos
            if (friendsCount >= 5 && !userBadges.Any(x => x.Badge.Id == 4))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(3) });
            }
            else if (friendsCount >= 10 && !userBadges.Any(x => x.Badge.Id == 5))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(4) });
            }
            else if (friendsCount >= 50 && !userBadges.Any(x => x.Badge.Id == 6))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(5) });
            }

            // Badges de posts
            if (postCount >= 3 && !userBadges.Any(x => x.Badge.Id == 7))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(6) });
            }
            else if (postCount >= 5 && !userBadges.Any(x => x.Badge.Id == 8))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(7) });
            }
            else if (postCount >= 10 && !userBadges.Any(x => x.Badge.Id == 9))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(8) });
            }

            // Badge de entrar em um grupo
            if (hasGroup && !userBadges.Any(x => x.Badge.Id == 10))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(9) });
            }

            //Badge de concluir o cadastro
            if (userRegisterFinish && !userBadges.Any(x => x.Badge.Id == 11))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(10) });
            }

            // Badges de torneios
            if (winnerTournamentCount >= 1 && !userBadges.Any(x => x.Badge.Id == 12))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(11) });
            }
            else if (winnerTournamentCount >= 5 && !userBadges.Any(x => x.Badge.Id == 13))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(12) });
            }
            else if (winnerTournamentCount >= 10 && !userBadges.Any(x => x.Badge.Id == 14))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(13) });
            }

            // Badges de curtidas
            if (myLikeCount >= 5 && !userBadges.Any(x => x.Badge.Id == 15))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(14) });
            }
            else if (myLikeCount >= 10 && !userBadges.Any(x => x.Badge.Id == 16))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(15) });
            }
            else if (myLikeCount >= 20 && !userBadges.Any(x => x.Badge.Id == 17))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(16) });
            }

            // Badges de curtidas recebidas
            if (likeCount >= 5 && !userBadges.Any(x => x.Badge.Id == 18))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(17) });
            }
            else if (likeCount >= 20 && !userBadges.Any(x => x.Badge.Id == 19))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(18) });
            }
            else if (likeCount >= 50 && !userBadges.Any(x => x.Badge.Id == 20))
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(19) });
            }

            //Badge de primeiro login
            if (firstLogin)
            {
                await _context.UserBadges.AddAsync(new UserBadge { User = user, Badge = badges.ElementAt(20) });
            }

            await _context.SaveChangesAsync();
        }

    }
}
