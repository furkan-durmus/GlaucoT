using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Contrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class MedicineRecordManager : IMedicineRecordService
    {
        IMedicineRecordDal _medicineRecordDal;

        public MedicineRecordManager(IMedicineRecordDal medicineRecordDal)
        {
            _medicineRecordDal = medicineRecordDal;
        }

        public void Add(MedicineRecord medicineRecord)
        {
            _medicineRecordDal.Add(medicineRecord);
        }

        public void Delete(MedicineRecord medicineRecord)
        {
            _medicineRecordDal.Delete(medicineRecord);
        }

        public MedicineRecord Get(int id)
        {
            return _medicineRecordDal.Get(m => m.Id == id);
        }

        public List<MedicineRecord> GetAll(Guid patientId)
        {
            return _medicineRecordDal.GetAll(m => m.PatientId == patientId);
        }

        public void Update(MedicineRecord medicineRecord)
        {
            _medicineRecordDal.Update(medicineRecord);
        }
    }
}
