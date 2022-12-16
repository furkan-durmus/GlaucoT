using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DeletePatientMedicineRecord
    {
        public int MedicineRecordId { get; set; }
        public Guid PatientId { get; set; }
        public int MedicineId { get; set; }
        public string SecretKey { get; set; }
    }
}
