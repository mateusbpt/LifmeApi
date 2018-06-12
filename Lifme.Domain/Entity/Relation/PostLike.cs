using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity.Relation
{
    public class PostLike
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public Post Post { get; set; }

        public User User { get; set; }    
    }
}
