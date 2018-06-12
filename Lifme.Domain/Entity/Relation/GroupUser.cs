using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class GroupUser
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }

        public Group Group { get; set; }

        public User User { get; set; }

    }
}
