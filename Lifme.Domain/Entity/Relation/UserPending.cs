using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class UserPending
    {
        public int UserId { get; set; }

        public int PendingId { get; set; }

        public User User { get; set; }

        public User Pending { get; set; }
    }
}
