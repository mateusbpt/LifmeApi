using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class GroupAddModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public Group ConvertToGroup(User user)
        {
            return new Group
            {
                Name = Name,
                Description = Description,
                Image = Image,
                Administrator = user
            };
        }

        public void Validate()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Name == null || Image == null || Description == null)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }
        }

    }
}
