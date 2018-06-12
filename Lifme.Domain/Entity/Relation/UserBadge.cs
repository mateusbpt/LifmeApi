using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class UserBadge
    {
        public int UserId { get; set; }

        public int BadgeId { get; set; }

        public User User { get; set; }

        public Badge Badge { get; set; }
    }
}
