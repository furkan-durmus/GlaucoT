using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Contrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Web.Identity;

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


#region Identity

builder.Services.AddScoped<IUserStore<DoctorUser>, DoctorUserStore>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, DoctorUserRoleStore>();
builder.Services.AddScoped<UserManager<DoctorUser>, DoctorUserManager>();
builder.Services.AddIdentity<DoctorUser, IdentityRole>(v =>
{
    v.SignIn.RequireConfirmedAccount = true;
    v.SignIn.RequireConfirmedEmail = true;
    v.Password.RequiredLength = 5;
    v.User.RequireUniqueEmail = true;
    v.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    v.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
}).AddDefaultTokenProviders();
builder.Services.AddAuthentication();
builder.Services.ConfigureApplicationCookie(v =>
{
    v.Cookie.Name = "GlaucoT.Cookie";
    v.LoginPath = "/Home/Index";
    v.ExpireTimeSpan = TimeSpan.FromDays(30);
    v.Cookie.SameSite = SameSiteMode.Strict;
    v.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    //v.AccessDeniedPath = "/Yardimci/Index";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(v =>
{
    v.Cookie.Name = "GlaucoT.Session";
    v.IdleTimeout = TimeSpan.FromMinutes(30);
    v.Cookie.IsEssential = true;
    v.Cookie.SameSite = SameSiteMode.Strict;
    v.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

#endregion


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Home/Index";
//        options.Cookie.Name = "GlaucotCookie";
//    });


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
