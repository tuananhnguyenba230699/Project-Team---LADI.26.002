using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
	public class register_device
	{
		public int ID { get; set; }
		public string Type { get; set; }
		public string TypeName { get; set; }
		public string DeviceName { get; set; }
		public string IpAddress { get; set; }
		public int Port { get; set; }
	}
}
