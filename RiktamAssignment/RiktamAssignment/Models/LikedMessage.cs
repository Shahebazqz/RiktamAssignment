using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Models
{
    public class LikedMessage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public int MessageId { get; set; }
        [Required]
        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}
