using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class UserFriend
    {
        public int UserId { get; set; }

        public int FriendId { get; set; }

        public User User { get; set; }

        public User Friend { get; set; }
    }
}
