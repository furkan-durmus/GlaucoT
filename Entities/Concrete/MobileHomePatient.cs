using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MobileHomeRequest
    {
        public Guid PatientId { get; set; }
        public string SecretKey { get; set; }
    }
}
