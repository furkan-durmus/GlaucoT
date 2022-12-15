using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Contrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IDoctorService, DoctorManager>();
builder.Services.AddSingleton<IDoctorDal, EFDoctorDal>();

builder.Services.AddSingleton<IGlassRecordService, GlassRecordManager>();
builder.Services.AddSingleton<IGlassRecordDal, EFGlassRecordDal>();

builder.Services.AddSingleton<IMedicineRecordService, MedicineRecordManager>();
builder.Services.AddSingleton<IMedicineRecordDal, EFMedicineRecordDal>();

builder.Services.AddSingleton<IMedicineService, MedicineManager>();
builder.Services.AddSingleton<IMedicineDal, EFMedicineDal>();

builder.Services.AddSingleton<IPatientService, PatientManager>();
builder.Services.AddSingleton<IPatientDal, EFPatientDal>();

builder.Services.AddSingleton<IOTPService, OTPManager>();
builder.Services.AddSingleton<IOTPDal, EFOTPDal>();

builder.Services.AddSingleton<IGlassRecordService, GlassRecordManager>();
builder.Services.AddSingleton<IGlassRecordDal, EFGlassRecordDal>();

builder.Services.AddSingleton<IMedicineRecordService, MedicineRecordManager>();
builder.Services.AddSingleton<IMedicineRecordDal, EFMedicineRecordDal>();

builder.Services.AddSingleton<IMedicineService, MedicineManager>();
builder.Services.AddSingleton<IMedicineDal, EFMedicineDal>();

builder.Services.AddSingleton<IRegisterService, RegisterManager>();
builder.Services.AddSingleton<ILoginService, LoginManager>();
builder.Services.AddSingleton<IMobileHomeService, MobileHomeManager>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.Cookie.Name = "GlaucotCookie";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
