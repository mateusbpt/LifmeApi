using Lifme.Domain.Entity;
using Lifme.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class PostModel
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public Post ConvertToPost(User user)
        {
            return new Post
            {
                Title = Title,
                Message = Message,
                User = user
            };
        }

        public void Validate()
        {
            if (this == null)
            {
                throw new NotFoundException("Não foram encontrados dados para serem cadastrados.");
            }

            if (Title == null || Message == null)
            {
                throw new BadRequestException("Um ou mais campos obrigatórios não estão preenchidos.");
            }
        }

    }
}

