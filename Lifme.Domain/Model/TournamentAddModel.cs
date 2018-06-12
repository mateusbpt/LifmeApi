using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class TournamentAddModel
    {
        public string Name { get; set; }

        public string Image { get; set; }


        public Tournament ConvertToTournament(Group group)
        {
            return new Tournament
            {
                Name = Name,
                Image = Image,
                StartDate = DateTime.Now,
                Finished = false,
                Group = group
            };
        }

        public void Validate()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Name == null || Image == null)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }
        }

    }
}
