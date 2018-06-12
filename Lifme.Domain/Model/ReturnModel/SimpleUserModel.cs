using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class SimpleUserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Avatar { get; set; }

        public static SimpleUserModel ConvertToSimpleUserModel(User user)
        {
            return new SimpleUserModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Nickname = user.Nickname,
                Avatar = user.Avatar
            };
        }

    }
}
