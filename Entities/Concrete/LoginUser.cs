using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LoginUser
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string SecretKey { get; set; }
    }
}
