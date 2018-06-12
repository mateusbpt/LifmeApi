using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
