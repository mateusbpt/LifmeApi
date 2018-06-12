using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class GroupProfileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public SimpleUserModel Administrator { get; set; }

        public List<SimpleUserModel> Users { get; set; }

        public List<SimpleTournamentModel> TournamentsNotFinished { get; set; }

        public List<SimpleTournamentModel> TournamentsFinished { get; set; }

        public static GroupProfileModel ConvertToSimpleUserModel(Group group, List<Tournament> tournaments)
        {
            return new GroupProfileModel
            {
                Id = group.Id,
                Name = group.Name,
                Image = group.Image,
                Description = group.Description,
                Administrator = SimpleUserModel.ConvertToSimpleUserModel(group.Administrator),
                Users = group.Users.Select(x => SimpleUserModel.ConvertToSimpleUserModel(x.User)).ToList(),
                TournamentsNotFinished = tournaments.Where(x => !x.Finished).Select(x => SimpleTournamentModel.ConvertToSimpleTournamentModel(x)).ToList(),
                TournamentsFinished = tournaments.Where(x => x.Finished).Select(x => SimpleTournamentModel.ConvertToSimpleTournamentModel(x)).ToList()
            };
        }
    }
}
