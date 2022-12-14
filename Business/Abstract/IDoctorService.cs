using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDoctorService
    {
        List<Doctor> GetAll();
        Doctor Get(Guid doctorId);
        void Add(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        Doctor Login(string email, string password);
    }
}
