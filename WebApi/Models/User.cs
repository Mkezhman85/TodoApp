using System;
namespace WebApi.Models
{
    public class User
    {
        public User()
        {
            is_blocked = false;
            required_password_change = false;
            begin_date = DateTime.Now;
        }

        public int id { get; set; }
        public string login { get; set; } = "someUser";
        public DateTime begin_date { get; set; }
        public DateTime end_date { get; set; }
        public bool is_blocked { get; set; }
        public bool required_password_change { get; set; }
        public int wrong_login_count { get; set; }
        public DateTime last_login_date { get; set; }

    }
}
