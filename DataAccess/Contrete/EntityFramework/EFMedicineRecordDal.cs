using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contrete.EntityFramework
{
    public class EFMedicineRecordDal : EFEntityRepositoryBase<MedicineRecord, GlaucotContext>, IMedicineRecordDal
    {
        public List<UserMedicinesData> GetAllMedicineDataOfPatient(Guid patientId)
        {
            using (GlaucotContext context = new GlaucotContext())
            {
                return (from medicineRecords in context.MedicineRecords
                        join medicines in context.Medicines on medicineRecords.MedicineId equals medicines.MedicineId
                        where medicineRecords.PatientId == patientId
                        select new UserMedicinesData
                        {
                            MedicineRecordId = medicineRecords.MedicineRecordId,
                            MedicineName = medicines.MedicineName,
                            MedicineFrequency = medicineRecords.MedicineFrequency,
                            MedicineSideEffect = medicineRecords.MedicineSideEffect,
                            MedicineUsageRange = medicineRecords.MedicineUsageRange,
                            MedicineUsegeTimeList =medicineRecords.MedicineUsegeTimeList
                        }).ToList();
            }
        }
    }
}
