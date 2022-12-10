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
        IOTPService _otpService;
        public PatientController(IPatientService patientService, IRegisterService registerService, ILoginService loginService, IMobileHomeService mobileHomeService, IOTPService otpService)
        {
            _patientService = patientService;
            _registerService = registerService;
            _loginService = loginService;
            _mobileHomeService = mobileHomeService;
            _otpService = otpService;
        }

        [HttpPost("register")]

        public IActionResult RegisterPatient(RegisterPatient registerPatient)
        {

            if (!_registerService.CheckKeyIsValid(registerPatient))
            {
                return Ok(new { status = -99,message = $"Yetkisiz İşlem!" });
            }

            if (!_otpService.CheckOTP(registerPatient))
            {
                return Ok(new { status = 0, message = $"Geçersiz OTP" });
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
            return Ok(new { message = patient.PatientId , status = 1 });
        }


        [HttpPost("login")]
        public IActionResult Login(LoginPatient loginPatient)
        {

            if (!_loginService.CheckKeyIsValid(loginPatient))
            {
                return Ok(new { status = -99, message = $"Yetkisiz İşlem!" });
            }

            if (!_loginService.CheckLoginIsValid(loginPatient))
            {
                return Ok(new { status = 0, message = $"Telefon numarası veya Şifre hatalı." });
            }           
            return Ok(new { status = 1 , message = _loginService.ResponsePatientId(loginPatient).PatientId });
        }

        [HttpPost("home")]
        public IActionResult PatientHomeData(MobileHomeRequest patient)
        {

            if (!_mobileHomeService.CheckKeyIsValid(patient.PatientId, patient.SecretKey))
            {
                return Ok(new { status = -99 , message = $"Yetkisiz İşlem!"});
            }

                   
            return Ok(new { status = 1,message =new { UserProfileData = _mobileHomeService.GetUserProfileData(patient.PatientId), UserDrugsData = _mobileHomeService.GetUserDrugsData(patient.PatientId)} });
        }
        

        [HttpPost("sendregistersms")]
        public IActionResult PatientSendOTP(RegisterPatient registerPatient)
        {

            if (!_registerService.CheckKeyIsValid(registerPatient))
            {
                return Ok(new { status = -99, message = $"Yetkisiz İşlem!" });
            }

            if (!_otpService.CheckAcceptableSmsLimit(registerPatient))
            {
                return Ok(new { status = -1, message = $"Too many request" });
            }

            if (_registerService.CheckPhoneIsExist(registerPatient.PatientPhoneNumber))
            {
                return Ok(new { status = 0, message = $"Bu telefon numarası ile kayıtlı bir hesabınız bulunuyor." });
            }
            OTP userOTPData = new();
            userOTPData.PatientPhoneNumber = registerPatient.PatientPhoneNumber;
            userOTPData.OTPCode = "12345";
            userOTPData.CreateDate = DateTime.Now;
            userOTPData.ExpireDate = DateTime.Now.AddMinutes(3);
            _otpService.Create(userOTPData);

            return Ok(new { status = 1,message = $"OTP başarıyla oluşturuldu." });
        }


    }
}