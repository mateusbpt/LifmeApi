using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class ChallengeModel
    {
        public SimpleUserChallengeModel Challenge { get; set; }

        public List<SimpleUserChallengeModel> ChallengesForDay { get; set; }

        public int ChallengesRemaining { get; set; }

    }
}
