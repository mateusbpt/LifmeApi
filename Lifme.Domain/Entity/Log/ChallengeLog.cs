using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Log
{
    public class ChallengeLog
    {
        public int Id { get; set; }

        public Challenge Challenge { get; set; }

        public bool Accept { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
