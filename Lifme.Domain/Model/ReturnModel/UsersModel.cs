using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class UsersModel
    {
        public SimpleUserModel User { get; set; }

        public bool IsUser { get; set; }

        public bool IsPending { get; set; }

        public bool IsFriend { get; set; }

        public bool IsInvite { get; set;  }

        public static UsersModel ConvertToUsersModel(User user, User loggedUser)
        {
            return new UsersModel
            {
                User = SimpleUserModel.ConvertToSimpleUserModel(user),
                IsUser = user.Id == loggedUser.Id,
                IsPending = loggedUser.Pendings.Any(x => x.Pending.Id == user.Id),
                IsFriend = user.Friends.Any(x => x.Friend.Id == loggedUser.Id),
                IsInvite = user.Pendings.Any(x => x.Pending.Id == loggedUser.Id)
            };
        }
    }
}
