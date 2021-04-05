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

        //public long GroupId { get; set; }
        public Groups group { get; set; }


    }
}
