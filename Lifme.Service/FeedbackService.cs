using Lifme.Domain.Entity;
using Lifme.Domain.Model.ReturnModel;
using Lifme.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifme.Service
{
    public class FeedbackService
    {
        private readonly DatabaseContext _context;

        public FeedbackService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<FeedbackModel> GetFeedbackUser(User user)
        {
            var totalBadges = await _context.Badges.CountAsync();
            var totalChallenges = await _context.UserChallenges.CountAsync(x => x.User.Id == user.Id);

            var userChallengesCompleted = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .Where(x => x.Completed && x.User.Id == user.Id)
                .Select(x => SimpleUserChallengeModel.ConvertToSimpleUserChallengeModel(x))
                .ToListAsync();
            var userBadges = await _context.UserBadges
                 .Include(x => x.User)
                 .Include(x => x.Badge)
                 .Where(x => x.User.Id == user.Id)
                 .Select(x => x.Badge)
                 .ToListAsync();

            return new FeedbackModel
            {
                Badges = userBadges,
                CompletedChallenges = userChallengesCompleted,
                TotalBadges = totalBadges,
                TotalChallenges = totalChallenges
            };
        }
    }
}
