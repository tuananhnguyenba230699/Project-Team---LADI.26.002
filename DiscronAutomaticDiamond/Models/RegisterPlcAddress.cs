using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class RegisterPlcAddress
    {
        public int ID { get; set; }
        public string AddressCode { get; set; }
        public string RegisterAddress { get; set; }
        public string AddressName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
