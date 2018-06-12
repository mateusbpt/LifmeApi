using Lifme.Domain.Entity;
using Lifme.Domain.Entity.Relation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class SimpleUserChallengeModel
    {
        public int Id { get; set; }

        public DateTime DayChallenge { get; set; }

        public bool Accept { get; set; }

        public bool Completed { get; set; }

        public bool Finished { get; set; }

        public string Feedback { get; set; }

        public Challenge Challenge { get; set; }

        public static SimpleUserChallengeModel ConvertToSimpleUserChallengeModel(UserChallenge userChallenge)
        {
            return new SimpleUserChallengeModel
            {
                Id = userChallenge.Id,
                Accept = userChallenge.Accept,
                DayChallenge = userChallenge.DayChallenge,
                Completed = userChallenge.Completed,
                Finished = userChallenge.Finished,
                Feedback = userChallenge.Feedback,
                Challenge = userChallenge.Challenge
            };
        }

    }
}
