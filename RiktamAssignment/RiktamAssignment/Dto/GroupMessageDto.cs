using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiktamAssignment.Dto
{
    public class GroupMessageDto
    {
        public int GroupMessageId { get; set; }
        public int GroupId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
