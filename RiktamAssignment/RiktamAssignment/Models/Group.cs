﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public ICollection<GroupUser> Members { get; set; }
        [NotMapped]
        public ICollection<GroupMessage> Messages { get; set; }
    }
}
