using Lifme.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model.ReturnModel
{
    public class SimpleGroupModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public SimpleUserModel Adminstrator { get; set; }

        public static SimpleGroupModel ConvertToSimpleGroupModel(Group group)
        {
            return new SimpleGroupModel
            {
                Id = group.Id,
                Name = group.Name,
                Image = group.Image,
                Description = group.Description,
                Adminstrator = SimpleUserModel.ConvertToSimpleUserModel(group.Administrator)
            };
        }

    }
}
