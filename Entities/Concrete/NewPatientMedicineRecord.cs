﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class NewPatientMedicineRecord
    {
        public Guid PatientId { get; set; }
        public int MedicineId { get; set; }
        public int MedicineUsageRange { get; set; }
        public int MedicineFrequency { get; set; }
        public string MedicineUsegeTimeList { get; set; }
        public string SecretKey { get; set; }
    }
}
