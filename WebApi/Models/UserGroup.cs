using System;
namespace WebApi.Models
{
    public class UserGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }
    }
}
