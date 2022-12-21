using System;
namespace Web.Identity
{
    public class DoctorUser
    {
        public int Id { get; set; }
        public Guid DoctorUd { get; set; }
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }
        public string SecurityStamp { get; set; }
    }
}

