using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class line_find_infor
    {
		public int ID { get; set; }
		public int IDModel { get; set; }
		public int IDROI { get; set; }
		public string EdgeTransition { get; set; }
		public string EdgeType { get; set; }
		public int EdgeThreshold { get; set; }
		public int SamplingPercent { get; set; }
	}
}
