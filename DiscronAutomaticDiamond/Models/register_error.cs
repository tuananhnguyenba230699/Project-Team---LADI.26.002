using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class register_error
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string BitNumber { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorName { get; set; }
        public string ErrorWarning { get; set; }
        public string RegisterAddress { get; set; }
    }
}
