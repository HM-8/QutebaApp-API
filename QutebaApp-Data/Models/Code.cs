using System;
using System.Collections.Generic;
using System.Text;

namespace QutebaApp_Data.Models
{
    public class Code
    {
        public int UserId { get; set; }
        public int CodeDigit { get; set; }

        public virtual User User { get; set; }
    }
}
