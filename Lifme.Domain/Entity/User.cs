using Lifme.Domain.Entity.Relation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Avatar { get; set; }

        public string Description { get; set; }

        public List<UserPending> Pendings { get; set; }

        public List<UserFriend> Friends { get; set; }

        public List<UserBadge> Badges { get; set; }

    }
}
