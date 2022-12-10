using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientService _patientService;
        IRegisterService _registerService;
        ILoginService _loginService;
        IMobileHomeService _mobileHomeService;
        public PatientController(IPatientService patientService, IRegisterService registerService, ILoginService loginService, IMobileHomeService mobileHomeService)
        {
            _patientService = patientService;
            _registerService = registerService;
            _loginService = loginService;
            _mobileHomeService = mobileHomeService;
        }

        [HttpPost("register")]

        public IActionResult RegisterPatient(RegisterPatient registerPatient)
        {

            if (!_registerService.CheckKeyIsValid(registerPatient))
            {
                return Ok(new { message = $"Yetkisiz İşlem!", data = -1 });
            }

            if (_registerService.CheckPhoneIsExist(registerPatient.PatientPhoneNumber))
            {
                return Ok(new { message = $"Bu telefon numarası ile kayıtlı bir hesabınız bulunuyor.", data = 0 });
            }

            Patient patient = new();
            patient.PatientId = new Guid();
            patient.PatientName = registerPatient.PatientName;
            patient.PatientLastName = registerPatient.PatientLastName;
            patient.PatientAge = 0;
            patient.PatientGender = 0;
            patient.PatientPhoneNumber = registerPatient.PatientPhoneNumber;
            patient.PatientPassword = registerPatient.PatientPassword;
            patient.PatientPhotoPath = "null";
            patient.IsActive = true;

            _patientService.Add(patient);
            return Ok(new { message = $"Kayıt işlemi başarılı.", data = patient.PatientId });
        }


        [HttpPost("login")]
        public IActionResult Login(LoginPatient loginPatient)
        {

            if (!_loginService.CheckKeyIsValid(loginPatient))
            {
                return Ok(new { message = $"Yetkisiz İşlem!", data = -1 });
            }

            if (!_loginService.CheckLoginIsValid(loginPatient))
            {
                return Ok(new { message = $"Telefon numarası veya Şifre hatalı.", data = 0 });
            }           
            return Ok(new {data = _loginService.ResponsePatientId(loginPatient).PatientId });
        }

        [HttpPost("home")]
        public IActionResult PatientHomeData(MobileHomeRequest patient)
        {

            if (!_mobileHomeService.CheckKeyIsValid(patient.PatientId, patient.SecretKey))
            {
                return Ok(new { message = $"Yetkisiz İşlem!", data = -1 });
            }

                   
            return Ok(new {UserProfileData = _mobileHomeService.GetUserProfileData(patient.PatientId), UserDrugsData = _mobileHomeService.GetUserDrugsData(patient.PatientId) });
        }


    }
}