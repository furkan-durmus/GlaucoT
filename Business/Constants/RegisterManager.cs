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
    public class RegisterManager : IRegisterService
    {
        IPatientDal _patientDal;

        public RegisterManager(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public bool CheckEmailIsExist(string userEmail)
        {
            return _patientDal.Get(p=>p.PatientEmail==userEmail) != null ? true : false;
        }
      

        public bool CheckKeyIsValid(RegisterUser user)
        {
            string expectedSecretKey = "";
            foreach (char character in user.UserEmail)
            {
                expectedSecretKey = expectedSecretKey + System.Convert.ToInt32(character);
            }
            foreach (char character in user.UserPassword)
            {
                expectedSecretKey = expectedSecretKey + System.Convert.ToInt32(character);
            }
            string mykeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(expectedSecretKey));

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(mykeyBase64);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                expectedSecretKey = Convert.ToHexString(hashBytes); // .NET 5 +
            }
            return expectedSecretKey == user.SecretKey ? true : false;
        }

    }
}
