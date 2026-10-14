using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class register_roi_parameters
    {
        public int ID { get; set; }
        public string IDROI { get; set; }
        public int IDAction { get; set; }
        public string DetectType { get; set; }
        public string FillHole { get; set; }
        public decimal ThresholdMin { get; set; }
        public decimal ThresholdMax { get; set; }
        public string EdgeTransition { get; set; }
        public string EdgeType { get; set; }
        public decimal EdgeThreshold { get; set; }
        public decimal SamplingPercent { get; set; }
    }
}
