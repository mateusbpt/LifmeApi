using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class CompleteChallengeModel
    {
        public bool Completed { get; set; }

        public string Feedback { get; set; }

        public void Validate()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Feedback == null && Completed)
            {
                throw new BadRequestException("Feedback não foi preenchido.");
            }
        }
    }
}
