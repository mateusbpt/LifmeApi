using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class SimplePostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; } 

        public SimpleUserModel User { get; set; }

        public List<SimpleUserModel> Likes { get; set; }

        public static SimplePostModel ConvertToSimplePostModel(Post post)
        {
            return new SimplePostModel
            {
                Id = post.Id,
                Title = post.Title,
                Message = post.Message,
                Image = post.Image,
                Date = post.Date,
                User = SimpleUserModel.ConvertToSimpleUserModel(post.User),
                Likes = post.Likes.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x.User)).ToList()
            };
        }
    }
}
