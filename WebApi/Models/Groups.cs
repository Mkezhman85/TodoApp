using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("group")]
    public class Groups
    {
        public Groups()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }

        public static implicit operator long(Groups v)
        {
            throw new NotImplementedException();
        }
    }
}