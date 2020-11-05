using System;
using System.Collections.Generic;
using System.Text;

namespace QutebaApp_Data.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public DateTime CategoryCreationTime { get; set; }
    }
}
