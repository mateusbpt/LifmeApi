using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using Lifme.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class UserRegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Avatar { get; set; }

        public void Validation()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Email == null)
            {
                throw new BadRequestException("O e-mail é um campo obrigatório.");
            }

            if (Password == null || Password.Length < 8)
            {
                throw new BadRequestException("A senha é um campo obrigatório e deve possuir no mínimo 8 caracteres.");
            }

            if (Name == null || LastName == null || Nickname == null || Avatar == null)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }

        }

        public User ConvertToUser()
        {
            return new User
            {
                Email = Email,
                Password = CryptographyService.PasswordCryptograpy(Email, Password),
                Name = Name,
                LastName = LastName,
                Avatar = Avatar,
                Nickname = Nickname
            };
        }
    }
}
