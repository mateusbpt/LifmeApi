using Lifme.Domain.Entity;
using Lifme.Domain.Entity.Relation;
using Lifme.Domain.Exceptions;
using Lifme.Domain.Model;
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
    public class ChallengeService
    {
        private readonly DatabaseContext _context;

        private const int CHALLENGES_FOR_DAY = 5;

        public ChallengeService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UserChallenge> GetUserChallengeById(User user, int id)
        {
            var userChallenge = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .FirstOrDefaultAsync(x => x.User.Id == user.Id && x.Id == id);

            if (userChallenge == null)
            {
               throw new NotFoundException("Desafio não existente.");
            }

            return userChallenge;
        }

        public async Task<ChallengeModel> GetChallengeProfile(User user)
        {
            var countChallenges = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .CountAsync(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id);

            if (countChallenges == 0)
            {
                var challenge = await GetNextChallenge();
                await _context.UserChallenges.AddAsync(new UserChallenge { User = user, Challenge = challenge, DayChallenge = DateTime.Now });
                await _context.SaveChangesAsync();
            }

            var challengesOfDay = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .Where(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id)
                .ToListAsync();

            return new ChallengeModel
            {
                Challenge = SimpleUserChallengeModel.ConvertToSimpleUserChallengeModel(challengesOfDay.LastOrDefault()),
                ChallengesForDay = challengesOfDay.Select(x => SimpleUserChallengeModel.ConvertToSimpleUserChallengeModel(x)).ToList(),
                ChallengesRemaining = CHALLENGES_FOR_DAY - challengesOfDay.Count
            };

        }

        public async Task AcceptChallenge(User user, UserChallenge userChallenge)
        {
            if (userChallenge == null)
            {
                throw new NotFoundException("Desafio não existente.");
            }

            var challengesOfDay = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .Where(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id)
                .ToListAsync();

            if (userChallenge.Id != challengesOfDay.LastOrDefault().Id)
            {
                throw new BadRequestException("Esse desafio não é o atual.");
            }

            userChallenge.Accept = true;
       
            await _context.SaveChangesAsync();   
        }

        public async Task RejectChallenge(User user, UserChallenge userChallenge)
        {
            if (userChallenge == null)
            {
                throw new NotFoundException("Desafio não existente.");
            }

            if (userChallenge.Accept)
            {
                throw new BadRequestException("Esse desafio foi aceito anteriormente.");
            }

            var challengesOfDay = await _context.UserChallenges
               .Include(x => x.User)
               .Include(x => x.Challenge)
               .Where(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id)
               .ToListAsync();

            if (userChallenge.Id != challengesOfDay.LastOrDefault().Id)
            {
                throw new BadRequestException("Esse desafio não é o atual.");
            }

            var countChallenges = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .CountAsync(x => x.DayChallenge.Date == DateTime.Now.Date);

            if (countChallenges < CHALLENGES_FOR_DAY)
            {
                var challenge = await GetNextChallenge();
                await _context.UserChallenges.AddAsync(new UserChallenge { User = user, Challenge = challenge, DayChallenge = DateTime.Now });
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteChallenge(CompleteChallengeModel completeChallengeModel, User user, UserChallenge userChallenge)
        {
            completeChallengeModel.Validate();

            if (userChallenge == null)
            {
                throw new NotFoundException("Desafio não existente.");
            }

            if (userChallenge.Finished)
            {
                throw new BadRequestException("Esse desafio já foi finalizado.");
            }

            var challengesOfDay = await _context.UserChallenges
               .Include(x => x.User)
               .Include(x => x.Challenge)
               .Where(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id)
               .ToListAsync();

            if (userChallenge.Id != challengesOfDay.LastOrDefault().Id)
            {
                throw new BadRequestException("Esse desafio não é o atual.");
            }

            userChallenge.Completed = completeChallengeModel.Completed;
            userChallenge.Feedback = completeChallengeModel.Feedback;
            userChallenge.Finished = true;

            var countChallenges = await _context.UserChallenges
                .Include(x => x.User)
                .Include(x => x.Challenge)
                .CountAsync(x => x.DayChallenge.Date == DateTime.Now.Date && user.Id == x.User.Id);

            if (countChallenges < CHALLENGES_FOR_DAY)
            {
                var challenge = await GetNextChallenge();
                await _context.UserChallenges.AddAsync(new UserChallenge { User = user, Challenge = challenge, DayChallenge = DateTime.Now });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Challenge> GetNextChallenge()
        {
            var challenges = await _context.Challenges.ToListAsync();
            var randomNumber = new Random();
            var maxChallenge = challenges.Count + 1;
            var randomChallenge = randomNumber.Next(1, maxChallenge);

            return challenges.FirstOrDefault(x => x.Id == randomChallenge);

        }
    }
}
