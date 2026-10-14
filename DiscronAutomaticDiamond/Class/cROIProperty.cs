using DevExpress.Data.Linq.Helpers;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Emgu.CV.Ccm.ColorCorrectionModel;
using System.Xml.Linq;

namespace AutoBrutingDiamond.Class
{
    public class cROIProperty
    {
        public Guid ID;
        public bool Selected;
        public long IDLink;
        public bool Active;
        // public EActionTypes ActionType;
        // tool Blob
        public string DetectType { get; set; }
        public string GreyLevelThresholdType { get; set; }
        public bool FillHoles { get; set; }
        public List<double> ThresholdRange { get; set; }
        public bool EnableAreaFilter { get; set; }
        public List<double> AreaRange { get; set; }

        public int ActionType { get; set; }
        // tool FindLine
        public string EdgeTransition { get; set; }
        public string EdgeType { get; set; }
        public bool EnableAngleRangeCheck { get; set; }
        public List<double> GapLengthRange { get; set; }
        public List<double> LineLengthTolerance { get; set; }
        public List<double> LineAngleTolerance { get; set; }
        public List<double> OutlierDistanceThreshold { get; set; }
        public List<double> MaxPercentageOfOutliers { get; set; }
        public double EdgeThreshold { get; set; }
        public int MinEdgePointNumber { get; set; }
        public int SamplingPercent { get; set; }
        public string Name { get; set; }
        //public cROIProperty(Guid _ID, bool _IsSelected = false, int ActionType =0)
        //{
        //    ID = _ID;
        //    IDLink = -1;
        //    Active = true;
        //    Selected = _IsSelected;
        //    IDLink = -1;
        //    if (ActionType == 3)
        //    {
        //        ThresholdRange = new List<double>() { 0, 255 };
        //        FillHoles = false;
        //        DetectType = "Detect In Range Blobs";
        //        Name = "New ROI";

        //        EdgeTransition = "Dark to Light";
        //        EdgeType = "First Edge";
        //        EdgeThreshold = 20;
        //        SamplingPercent = 50;
        //    }
        //    else if (ActionType == 1)
        //    {
        //        EdgeTransition = "Dark to Light";
        //        EdgeType = "First Edge";
        //        EdgeThreshold = 20;
        //        SamplingPercent = 50;

        //        ThresholdRange = new List<double>() { 0, 255 };
        //        FillHoles = false;
        //        DetectType = "Detect In Range Blobs";
        //        Name = "New ROI";
        //    }
        //    //Train = new HTuple();
        //    //ColorSpace = new HTuple();
        //}
        public cROIProperty(Guid _ID, bool _IsSelected = false, int actionType = 0)
        {
            ID = _ID;
            IDLink = -1;
            Active = true;
            Selected = _IsSelected;
            IDLink = -1;
            ActionType = actionType;

            //if (ActionType == 3)
            //{
            //    ThresholdRange = new List<double>() { 0, 255 };
            //    FillHoles = false;
            //    DetectType = "Detect In Range Blobs";
            //    Name = "New ROI";

            //    EdgeTransition = "Dark to Light";
            //    EdgeType = "First Edge";
            //    EdgeThreshold = 20;
            //    SamplingPercent = 50;
            //}
            //else if (ActionType == 1)
            //{
            //    EdgeTransition = "Dark to Light";
            //    EdgeType = "First Edge";
            //    EdgeThreshold = 20;
            //    SamplingPercent = 50;

            //    ThresholdRange = new List<double>() { 0, 255 };
            //    FillHoles = false;
            //    DetectType = "Detect In Range Blobs";
            //    Name = "New ROI";
            //}
            //    Train = new HTuple();
            //    ColorSpace = new HTuple();
        }
    }

}
