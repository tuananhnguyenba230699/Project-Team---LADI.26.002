using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class history_data
    {
        public DateTime DateTimeStart { get; set; }
        public string DiamondCode { get; set; }
        public decimal DistanceOriginalMin { get; set; }
        public decimal DistanceOriginalMax { get; set; }
        public decimal DiameterAverage { get; set; }
        public decimal DistanceGrindingRequest { get; set; }
        public decimal DistanceGrindingReal { get; set; }
        public string CycleConcentric { get; set; }
        public string CycleGrinding { get; set; }
        public decimal OvalMax { get; set; }
        public decimal OvalMin { get; set; }
        public decimal OvalAverage { get; set; }
        public string CrowHeight { get; set; }
        public string TotalDepth { get; set; }
        public decimal Cutting_Value { get; set; }
        public string IsResize_Target { get; set; }
        public DateTime DateTimeEnd { get; set; }
    }
}
