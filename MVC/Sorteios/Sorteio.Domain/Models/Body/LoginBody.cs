using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorteio.Domain.Models.Body
{
    public class LoginBody
    {
        public string email { get; set; }
        public string senha { get; set; }
    }
}
