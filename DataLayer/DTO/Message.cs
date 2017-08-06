using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string AdTitle { get; set; }
        public int AdId { get; set; }
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
        public DateTime MessageDate { get; set; }
        public string To { get; set; }
        public string From { get; set; }
    }
}
