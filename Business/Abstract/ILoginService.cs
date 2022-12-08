using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILoginService
    {
        bool CheckKeyIsValid(LoginUser user);
        bool CheckLoginIsValid(LoginUser user);
        Patient ResponsePatientId(LoginUser user);
    }
}
