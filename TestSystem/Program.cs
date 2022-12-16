// See https://aka.ms/new-console-template for more information





using Business.Constants;
using DataAccess.Contrete.EntityFramework;
using Web.Jobs;


// Test klasını gerekli parametrelerle buraya web'i çalıştırmadan servisi test edebilirsin.

var myTest = new SendMedicineNotification(new MedicineRecordManager(new EFMedicineRecordDal()));
myTest.SendNotificationWithOneSignal();
