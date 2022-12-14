using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class DoctorManager : IDoctorService
    {
        IDoctorDal _doctorDal;

        public DoctorManager(IDoctorDal doctorDal)
        {
            _doctorDal = doctorDal;
        }

        public void Add(Doctor doctor)
        {
            _doctorDal.Add(doctor);
        }

        public void Delete(Doctor doctor)
        {
            _doctorDal.Delete(doctor);
        }

        public Doctor Get(Guid doctorId)
        {
            return _doctorDal.Get(d => d.DoctorId == doctorId);
        }

        public List<Doctor> GetAll()
        {
            return _doctorDal.GetAll();
        }

        public Doctor Login(string email, string password)
        {
            return _doctorDal.Get(q => q.DoctorEmail == email && q.DoctorPassword == password);
        }

        public void Update(Doctor doctor)
        {
            _doctorDal.Update(doctor);
        }
    }
}
