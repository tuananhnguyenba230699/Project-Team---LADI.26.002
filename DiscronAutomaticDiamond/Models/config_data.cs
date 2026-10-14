using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class config_data
    {
        public int ID { get; set; }
        public string Ratio_Pixel_MM { get; set; }
        public string AllowableDeviation_MM { get; set; }
        public string ValueConvert { get; set; }
        public string OffsetGrindingStone { get; set; }
        public string InspectNumber { get; set; }
        public string CountSet_Min_Max { get; set; }
        public string GreaterHeight { get; set; }
        public string AverageHeight { get; set; }
        public string LessHeight { get; set; }
        public string DistanceGrind { get; set; }
        public string DistanceMove_Preview { get; set; }
        public int IsTotalDepth_TAB { get; set; }
        public string Grinding_L_Position { get; set; }
        public string Grinding_R_Position { get; set; }

    }
}
