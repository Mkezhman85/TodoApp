using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("group")]
    public class Group
    {
        public Group()
        {
        }

        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }


    }
}
