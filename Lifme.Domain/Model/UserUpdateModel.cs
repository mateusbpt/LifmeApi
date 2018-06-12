using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class UserUpdateModel
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Description { get; set; }

        public void Validation()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Name == null || LastName == null || Nickname == null)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }

        }

        public User UpdatedUser(User user)
        {
            user.Name = Name;
            user.LastName = LastName;
            user.Nickname = Nickname;
            user.Description = Description;

            return user;
        }

    }
}
