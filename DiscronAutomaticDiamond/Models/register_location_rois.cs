using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class register_location_rois
    {
		public int ID { get; set; }
		public string IDGuid { get; set; }
		public int IDAction { get; set; }
		public int CenterX { get; set; }
		public int CenterY { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Phi { get; set; }
		public int DescriptionCode { get; set; }
		public string DescriptionName { get; set; }
	}
}
