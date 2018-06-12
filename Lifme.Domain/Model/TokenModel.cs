using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Domain.Model
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
