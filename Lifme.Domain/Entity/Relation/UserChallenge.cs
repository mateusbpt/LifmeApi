using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class UserChallenge
    {
        public int Id { get; set; }

        public DateTime DayChallenge { get; set; } 

        public bool Accept { get; set; }

        public bool Completed { get; set; }

        public bool Finished { get; set; }

        public string Feedback { get; set; }

        public Challenge Challenge { get; set; }

        public User User { get; set; }
    }
}
