using Lifme.Domain.Entity.Relation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Entity
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public User Administrator { get; set; }

        public List<GroupUser> Users { get; set; }
    }
}
