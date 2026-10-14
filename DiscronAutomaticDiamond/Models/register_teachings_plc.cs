using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Models
{
    public class register_teachings_plc
    {
        public int ID { get; set; }
        public string JogPlusAxis1 { get; set; }
        public string JogPlusAxis1Lamp { get; set; }
        public string JogMinusAxis1 { get; set; }
        public string JogMinusAxis1Lamp { get; set; }
        public string JogSpeedAxis1 { get; set; }

        public string JogPlusAxis2 { get; set; }
        public string JogPlusAxis2Lamp { get; set; }
        public string JogMinusAxis2 { get; set; }
        public string JogMinusAxis2Lamp { get; set; }
        public string JogSpeedAxis2 { get; set; }

        public string JogPlusAxis3 { get; set; }
        public string JogPlusAxis3Lamp { get; set; }
        public string JogMinusAxis3 { get; set; }
        public string JogMinusAxis3Lamp { get; set; }
        public string JogSpeedAxis3 { get; set; }

        public string JogPlusAxis4 { get; set; }
        public string JogPlusAxis4Lamp { get; set; }
        public string JogMinusAxis4 { get; set; }
        public string JogMinusAxis4Lamp { get; set; }
        public string JogSpeedAxis4 { get; set; }

        //MA
        public string MARunPositionAxis1 { get; set; }
        public string MARunPositionAxis1Lamp { get; set; }
        public string MAStopPositionAxis1 { get; set; }
        public string MAStopPositionAxis1Lamp { get; set; }
        public string PositionRunAxis1 { get; set; }
        public string PositionCurentAxis1 { get; set; }

        public string MARunPositionAxis2 { get; set; }
        public string MARunPositionAxis2Lamp { get; set; }
        public string MAStopPositionAxis2 { get; set; }
        public string MAStopPositionAxis2Lamp { get; set; }
        public string PositionRunAxis2 { get; set; }
        public string PositionCurentAxis2 { get; set; }

        public string MARunPositionAxis3 { get; set; }
        public string MARunPositionAxis3Lamp { get; set; }
        public string MAStopPositionAxis3 { get; set; }
        public string MAStopPositionAxis3Lamp { get; set; }
        public string PositionRunAxis3 { get; set; }
        public string PositionCurentAxis3 { get; set; }

        public string MARunPositionAxis4 { get; set; }
        public string MARunPositionAxis4Lamp { get; set; }
        public string MAStopPositionAxis4 { get; set; }
        public string MAStopPositionAxis4Lamp { get; set; }
        public string PositionRunAxis4 { get; set; }
        public string PositionCurentAxis4 { get; set; }

        //Origin
        public string OriginAxis2 { get; set; }
        public string OriginAxis2Lamp { get; set; }
        public string OriginCompleteAxis2 { get; set; }
        public string OriginCompleteAxis2Lamp { get; set; }

        public string OriginAxis3 { get; set; }
        public string OriginAxis3Lamp { get; set; }
        public string OriginCompleteAxis3 { get; set; }
        public string OriginCompleteAxis3Lamp { get; set; }

        public string OriginAxis4 { get; set; }
        public string OriginAxis4Lamp { get; set; }
        public string OriginCompleteAxis4 { get; set; }
        public string OriginCompleteAxis4Lamp { get; set; }

        //Manual Lamp
        public string InventorOn { get; set; }
        public string InventorOff { get; set; }
        public string AirFlowStartOn { get; set; }
        public string AirFlowStartOff { get; set; }
        public string AirFlowEndOn { get; set; }
        public string AirFlowEndOff { get; set; }
        public string SetInventorOn { get; set; }
        public string SetInventorOff { get; set; }
        public string SetAirFlowStartOn { get; set; }
        public string SetAirFlowStartOff { get; set; }
        public string SetAirFlowEndOn { get; set; }
        public string SetAirFlowEndOff { get; set; }

        //
        public string OffsetCheckVisionSpeedAxis1 { get; set; }
        public string Rotate90SpeedAxis1 { get; set; }
        public string RotateMinus90SpeedAxis1 { get; set; }
        public string RotateGrindingPositionAxis1 { get; set; }
        public string RotateGrindingSpeedAxis1 { get; set; }

        public string ReadyPositionAxis2 { get; set; }
        public string ReadySpeedAxis2 { get; set; }
        public string StartConcentricPositionAxis2 { get; set; }
        public string StartConcentricSpeedAxis2 { get; set; }
        public string ConcentricAdjustmentPositionAxis2 { get; set; }
        public string ConcentricAdjustmentSpeedAxis2 { get; set; }

        public string ReadyPositionAxis3 { get; set; }
        public string ReadySpeedAxis3 { get; set; }
        public string StartGrindingPositionAxis3 { get; set; }
        public string StartGrindingSpeedAxis3 { get; set; }
        public string OffsetGrindingPositionAxis3 { get; set; }
        public string OffsetGrindingSpeedAxis3 { get; set; }

        public string GrindingPositionAxis4 { get; set; }
        public string GrindingSpeedAxis4 { get; set; }
        public string ManualGrindingRightPositionAxis4 { get; set; }
        public string ManualGrindingRightSpeedAxis4 { get; set; }
        public string ManualGrindingLeftPositionAxis4 { get; set; }
        public string ManualGrindingLeftSpeedAxis4 { get; set; }
    }
}
