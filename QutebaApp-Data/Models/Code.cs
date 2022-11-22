using System;

namespace QutebaApp_Data.Models
{
    public class Code
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int CodeDigit { get; set; }
        public DateTime TimeCreated { get; set; }

        public virtual User User { get; set; }
    }
}
