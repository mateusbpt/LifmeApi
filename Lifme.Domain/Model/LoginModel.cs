using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public void Validation()
        {
            if (Email == null || Password == null)
            {
                throw new BadRequestException("Os campos E-mail e Senha são obrigatórios");
            }
        }
    }
}
