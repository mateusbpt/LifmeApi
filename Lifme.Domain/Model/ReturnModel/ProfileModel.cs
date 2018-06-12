using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class ProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Avatar { get; set; }

        public List<SimpleUserModel> Pendings { get; set; }

        public List<SimpleUserModel> Friends { get; set; }

        public List<SimpleGroupModel> Groups { get; set; }

        public bool IsUser { get; set; }

        public bool IsPending { get; set; }

        public bool IsFriend { get; set; }

        public bool IsInvite { get; set; }

        public static ProfileModel ConvertToProfileModel(User user, User loggedUser, List<Group> groups)
        {
            return new ProfileModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Nickname = user.Nickname,
                Avatar = user.Avatar,
                Pendings = user.Pendings.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x.Pending)).ToList(),
                Friends = user.Friends.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x.Friend)).ToList(),
                Groups = groups.Select(x => SimpleGroupModel.ConvertToSimpleGroupModel(x)).ToList(),
                IsUser = user.Id == loggedUser.Id,
                IsPending = loggedUser.Pendings.Any(x => x.Pending.Id == user.Id),
                IsFriend = user.Friends.Any(x => x.Friend.Id == loggedUser.Id),
                IsInvite = user.Pendings.Any(x => x.Pending.Id == loggedUser.Id)
            };
        }


    }
}
