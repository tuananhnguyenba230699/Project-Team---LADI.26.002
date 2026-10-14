using AutoBrutingDiamond.Class;
using RTCHikSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond
{
    class GlobVar
    {
        public static decimal Roi_Top;
        public static decimal Roi_Girdle;
        public static decimal Roi_Bot;
        public static decimal Roi_Crow;
        public static decimal Ratio;
        public Dictionary<Guid, cRectangType> ROIS = new Dictionary<Guid, cRectangType>();
        public Dictionary<Guid, cROIProperty> ROISProperties = new Dictionary<Guid, cROIProperty>();
    }
 
}
