using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class shape_diamond_infor
    {
		public int ID { get; set; }
		public decimal TOP { get; set; }
		public decimal Girdle { get; set; }
		public decimal BOT { get; set; }
		public decimal Crow { get; set; }
		public decimal StoneSize { get; set; }
		public decimal Ratio { get; set; }
		public decimal CuletX { get; set; }
		public decimal CuletY { get; set; }
		public decimal LowerGirdleX { get; set; }
		public decimal LowerGirdleY { get; set; }
		public decimal TableX { get; set; }
		public decimal TableY { get; set; }
		public int MeasurementType_Concentric { get; set; }
		public string MeasurementType_Concentric_Text { get; set; }
		public int MeasurementType_Grinding { get; set; }
		public string MeasurementType_Grinding_Text { get; set; }
		public int MeasurementType_Inspect { get; set; }
		public string MeasurementType_Inspect_Text { get; set; }
	}
}
