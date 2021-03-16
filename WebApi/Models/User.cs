using System;
namespace WebApi.Models
{
    public class User
    {
        public User()
        {
            IsBlocked = false;
            RequiredPasswordChange = false;
            BeginDate = DateTime.Now;
        }

        public long Id { get; set; }
        public string Login { get; set; } = "someUser";
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsBlocked { get; set; }
        public bool RequiredPasswordChange { get; set; }
        public int WrongLoginCount { get; set; }
        public DateTime LastLoginDate { get; set; }

        //public int id { get; set; }
        //public string login { get; set; } = "someUser";
        //public DateTime begin_date { get; set; }
        //public DateTime end_date { get; set; }
        //public bool is_blocked { get; set; }
        //public bool required_password_change { get; set; }
        //public int wrong_login_count { get; set; }
        //public DateTime last_login_date { get; set; }

    }
}
