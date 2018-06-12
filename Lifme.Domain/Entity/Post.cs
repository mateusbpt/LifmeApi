using Lifme.Domain.Entity.Relation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public User User { get; set; }

        public List<PostLike> Likes { get; set; }
    }
}
