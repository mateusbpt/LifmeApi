using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class TournamentWinnerModel
    {
        public int IdWinner { get; set; }

        public string Feedback { get; set; }

        public Tournament UpdatedTournament(User user, Tournament tournament)
        {
            tournament.Feedback = Feedback;
            tournament.Winner = user;
            tournament.Finished = true;

            return tournament;
        }

        public void Validate()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Feedback == null || IdWinner == 0)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }
        }
    }
}
