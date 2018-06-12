using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class FriendsAndPendingsModel
    {
        public List<SimpleUserModel> Pendings { get; set; }
        public List<SimpleUserModel> Friends { get; set; }

        public static FriendsAndPendingsModel ConvertToFriendsAndPendingsModel(List<User> pendings, List<User> friends)
        {
            return new FriendsAndPendingsModel
            {
                Pendings = pendings.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x)).ToList(),
                Friends = friends.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x)).ToList()
            };

        }
    }
}
