using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public User Winner { get; set; }

        public DateTime StartDate { get; set; }

        public bool Finished { get; set; }

        public string Feedback { get; set; }

        public Group Group { get; set; }
    }
}
