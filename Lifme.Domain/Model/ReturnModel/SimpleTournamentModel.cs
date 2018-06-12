using Lifme.Domain.Entity;
using System;

namespace Lifme.Domain.Model.ReturnModel
{
    public class SimpleTournamentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public bool Finished { get; set;  }

        public SimpleUserModel Winner { get; set; }

        public DateTime StartDate { get; set; }

        public string Feedback { get; set; }

        public static SimpleTournamentModel ConvertToSimpleTournamentModel(Tournament tournament)
        {
            var winner = tournament.Winner != null ? SimpleUserModel.ConvertToSimpleUserModel(tournament.Winner): null;

            return new SimpleTournamentModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Image = tournament.Image,
                Finished = tournament.Finished,
                Winner = winner,
                StartDate =  tournament.StartDate,
                Feedback = tournament.Feedback
            };
        }

    }
}