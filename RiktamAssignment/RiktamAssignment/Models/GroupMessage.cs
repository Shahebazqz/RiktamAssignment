using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Models
{
    public class GroupMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }

        public Group Group { get; set; }

        [Required]
        public int SenderId { get; set; }

        public User Sender { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
