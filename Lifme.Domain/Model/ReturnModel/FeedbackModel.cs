using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class FeedbackModel
    {
        public int TotalChallenges { get; set; }

        public int TotalBadges { get; set; }

        public List<Badge> Badges { get; set; }

        public List<SimpleUserChallengeModel> CompletedChallenges { get; set; }

    }
}
