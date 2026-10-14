using AccessIni_Scada;
using AutoBrutingDiamond.Class;
using AutoBrutingDiamond.Forms;
using AutoBrutingDiamond.Models;
using Blob_Muilti_ROI;
using DevExpress.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using LineFindTool;
using MySQLLib;
using RTCBase.Drawing;
using RTCConst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GraphicsWindow.GraphicsWindow;

namespace AutoBrutingDiamond
{
    public partial class FrmSetting_Vision : Form
    {
        public FrmSetting_Vision()
        {
            InitializeComponent();
            SmartWindow.FitImageDoubleClick = true;
        }

        #region Variable
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        bool isCloseThread = false;
        Thread _mappingManualPLC;
        public Image smartWindowImg = null;
        DataTable dt = new DataTable();
        //CAMERA HIKROBOT
        Bitmap bitmap_Camera = null;
        private Image _Image = null;
        public Image Image
        {
            get => _Image;
            set
            {
                GlobFuncs.DisposeImage(_Image);
                _Image = value;
                SmartWindow.ClearImage();
                if (_Image != null)
                    GlobFuncs.SmartSetPart(_Image, SmartWindow);
            }
        }

        #region Get & Set Value to Shape Diamond
        private PointF Culet
        {
            get { return new PointF(SmartWindow.m_pCulet.X, SmartWindow.m_pCulet.Y); }
            set { SmartWindow.m_pCulet = value; }
        }
        private PointF LowerGridle
        {
            get
            {
                return new PointF(SmartWindow.m_pLowerGirdle.X, SmartWindow.m_pLowerGirdle.Y);
            }
            set
            {
                SmartWindow.m_pLowerGirdle = value;
            }
        }
        private PointF mTable
        {
            get
            {
                return new PointF(SmartWindow.m_pTable.X, SmartWindow.m_pTable.Y);
            }
            set
            {
                SmartWindow.m_pTable = value;
            }
        }
        private PointF CenterRadius
        {
            get { return new PointF(SmartWindow.m_pTable.X, SmartWindow.m_pCulet.Y); }
        }
        private PointF CenterCrow
        {
            get { return new PointF(LowerGridle.X, Culet.Y); }
        }
        #endregion

        LineFindTool.LineFind LineFind = new LineFindTool.LineFind();
        private Image InputImage = null;
        SetOrigin.SetOrigin SetOrigin = new SetOrigin.SetOrigin();
        BlobTool BlobTool = new BlobTool();
        DistanceMeasurementTool.Measurement DistanceMeasure = new DistanceMeasurementTool.Measurement();
        private Image<Gray, byte> GrayImage = null;
        public static Image<Bgr, byte> DisplayImage = null;
        public Dictionary<Guid, cRectangType> ROIS = new Dictionary<Guid, cRectangType>();
        public Dictionary<Guid, cROIProperty> ROIProperties = new Dictionary<Guid, cROIProperty>();
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjectsCircle = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();

        private GraphicsWindow.GraphicsWindow.DataRoi CurrentDrawings = new GraphicsWindow.GraphicsWindow.DataRoi();
        private GraphicsWindow.GraphicsWindow.DataRoi CurrentDrawingsCircle = new GraphicsWindow.GraphicsWindow.DataRoi();
        // TODO: 141224
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects_Crow_TotalDept = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
        double Distance;
        List<double> DistancePoint = new List<double>();
        int ColB, RowB;
        List<double> Line1 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line2 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line3 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line4 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line5 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line6 = new List<double>() { 0, 0, 0, 0 };
        List<double> Line7 = new List<double>() { 0, 0, 0, 0 }; //Line mép trục bên trái
        List<double> Line8 = new List<double>() { 0, 0, 0, 0 }; //Line mép trục bên trái
        List<double> Line9 = new List<double>() { 0, 0, 0, 0 }; //Line tâm trục bên trái
        PointF point1 = new PointF(0, 0);
        PointF point2 = new PointF(0, 0);
        PointF point3 = new PointF(0, 0);
        PointF point4 = new PointF(0, 0);
        List<Guid> lstRemoveROI = new List<Guid>();
        public double concentric = 0;
        public double deviation = 0;
        public double distance = 0;
        public double crowHeight = 0;
        public double totalDeep = 0;

        // TODO: 311025 - NB  
        config_data _config_data = null;
        double PIXEL_TO_MM = 0;

        // TODO: 010726 - NB  
        private Config_Rois_Diamond _config_Rois_Diamond;
        #endregion

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang load thông tin");
            wait.Show();
            tabControl1.TabPages.Remove(tabPage5);
            SmartWindow.percentLineCheck = Properties.Settings.Default.PercentLineCheck;
            Lib.UpdateTextBoxText(txtPercentToTable, (Properties.Settings.Default.PercentLineCheck * 100).ToString("F1"));
            SmartWindow.ZoomIN = true;
            SmartWindow.ZoomOut = false;
            SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.2f, 1.2f);
            tabControl1.SelectedIndex = 1;
            // TODO: 230925 - NB
            tabVision_Camera.SelectedIndex = 1;

            #region Vision
            try
            {
                LoadShapeDiamond();
                LoadROIs_Diamond();
                LoadROIParameters();
                LoadSettingCamera();
                LoadROICircle_Calib();
                LoadROI_Crow_TotalDept();
                CircleWindow_MouseUp(null, null);
                LoadRootAxis();

                // TODO: 311025 - NB  
                _config_data = _mySQLHelper.FindAll<config_data>().FirstOrDefault();
                PIXEL_TO_MM = double.Parse(_config_data.Ratio_Pixel_MM, System.Globalization.CultureInfo.InvariantCulture);

                // TODO: 010726 - NB
                LoadConfig_Rois();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu : " + ex.ToString());
            }

            // TODO: 041224 - NB
            ConnectCamera();
            btnSnapCamera_Click(null, null);
            btnRunLineFind_VisionTools.Enabled = true;
            #endregion

            #region PLC 
            LoadInfor_PLC();
            if (cDeviceManager._plcMC == null) return;
            if (cDeviceManager._plcMC.Connected)
            {
                _mappingManualPLC = new Thread(MappingManualPLC);
                _mappingManualPLC.IsBackground = true;
                _mappingManualPLC.Start();
            }
            #endregion

            wait.Close();

            SmartWindow_DoubleClick(null, null);
        }

        #region SETTINGS VISION

        #region CONNECT CAMERA
        bool IsLive = false;
        public string DeviceName = string.Empty;
        Bitmap imageLiveCam = null;
        private void ConnectCamera()
        {
            string errMessage = string.Empty;
            bool IsConnectCamera = true;
            if (cDeviceManager.HikCamera == null)
                IsConnectCamera = cDeviceManager.ConnectCamera_Hikrobot(DeviceName, ref errMessage);
            if (!IsConnectCamera)
                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
            else
                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);
        }
        #endregion

        #region LOAD SETTINGS CAMERA, ROI,...
        // Thêm chức năng load thông tin setting camera live và camera snap
        private void LoadSettingCamera()
        {
            List<settings_camera> lstSettingCamera = _mySQLHelper.FindAll<settings_camera>();
            for (int i = 0; i < lstSettingCamera.Count; i++)
            {
                string paramName = lstSettingCamera[i].ParamName.ToString();
                switch (paramName)
                {
                    case "GainLive":
                        numGainLive.Value = Lib.ToInt(lstSettingCamera[i].ParamValue);
                        break;
                    case "ExposureLive":
                        num_ExposureLive.Value = Lib.ToDecimal(lstSettingCamera[i].ParamValue);
                        break;
                    case "GammaLive":
                        num_GammaLive.Value = Lib.ToDecimal(lstSettingCamera[i].ParamValue);
                        break;
                    case "GainSnap":
                        numGainSnap.Value = Lib.ToInt(lstSettingCamera[i].ParamValue);
                        break;
                    case "ExposureSnap":
                        num_ExposureSnap.Value = Lib.ToDecimal(lstSettingCamera[i].ParamValue);
                        break;
                    case "GammaSnap":
                        num_GammaSnap.Value = Lib.ToDecimal(lstSettingCamera[i].ParamValue);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Hàm lấy ra tất cả các giá trị setup của Shape Kim Cương và gán giá trị vào SmartWindow
        /// </summary>
        double diamondSizeOrigin = 0;
        public void LoadShapeDiamond()
        {
            List<shape_diamond_infor> lstShapeDiamond = _mySQLHelper.FindAll<shape_diamond_infor>();
            if (lstShapeDiamond.Count > 0)
            {   // gắn cách giá trị đã lưu vào Smart để vẽ shape Kim Cương
                SmartWindow.m_fTOP = Lib.ToDouble(lstShapeDiamond[0].TOP);
                SmartWindow.m_fBOT = Lib.ToDouble(lstShapeDiamond[0].BOT);
                SmartWindow.m_fCrow = Lib.ToDouble(lstShapeDiamond[0].Crow);
                SmartWindow.m_fGirdle = Lib.ToDouble(lstShapeDiamond[0].Girdle);

                Lib.UpdateNumericUpDownValue(numRoi_Top, Lib.ToDecimal(lstShapeDiamond[0].TOP));
                Lib.UpdateNumericUpDownValue(numRoi_Bot, Lib.ToDecimal(lstShapeDiamond[0].BOT));
                Lib.UpdateNumericUpDownValue(numRoi_Crow, Lib.ToDecimal(lstShapeDiamond[0].Crow));
                Lib.UpdateNumericUpDownValue(numRoi_Girdle, Lib.ToDecimal(lstShapeDiamond[0].Girdle));

                SmartWindow.m_pCulet = new PointF(Lib.ToInt(lstShapeDiamond[0].CuletX), Lib.ToInt(lstShapeDiamond[0].CuletY));
                SmartWindow.m_pTable = new PointF(Lib.ToInt(lstShapeDiamond[0].TableX), Lib.ToInt(lstShapeDiamond[0].TableY));
                SmartWindow.m_pLowerGirdle = new PointF(Lib.ToInt(lstShapeDiamond[0].LowerGirdleX), Lib.ToInt(lstShapeDiamond[0].LowerGirdleY));

                Lib.UpdateTextBoxText(txtCuletX, SmartWindow.m_pCulet.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtCuletY, SmartWindow.m_pCulet.Y.ToString("F1"));
                Lib.UpdateTextBoxText(txtTableX, SmartWindow.m_pTable.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtTableY, SmartWindow.m_pTable.Y.ToString("F1"));
                Lib.UpdateTextBoxText(txtGirldleX, SmartWindow.m_pLowerGirdle.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtGirldleY, SmartWindow.m_pLowerGirdle.Y.ToString("F1"));

                Lib.UpdateTextBoxText(txtCulletXView, SmartWindow.m_pCulet.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtCulletYView, SmartWindow.m_pCulet.Y.ToString("F1"));

                cbMeasurementType_Concentric.SelectedIndex = lstShapeDiamond[0].MeasurementType_Concentric;
                cbMeasurementType_Grinding.SelectedIndex = lstShapeDiamond[0].MeasurementType_Grinding;
                cbMeasurementType_Inspect.SelectedIndex = lstShapeDiamond[0].MeasurementType_Inspect;
            }
            diamondSizeOrigin = (SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y) * 2;
        }

        /// <summary>
        /// Hàm load và hiển thị giá trị của tâm trục PIN
        /// </summary>
        void LoadRootAxis()
        {
            register_root_axis rootAxis = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();
            if (rootAxis == null || rootAxis.ID <= 0) return;
            Lib.UpdateTextBoxText(txtCenterRotateAxisX1, rootAxis.CenterRotateAxisX1.ToString("F1"));
            Lib.UpdateTextBoxText(txtCenterRotateAxisY1, rootAxis.CenterRotateAxisY1.ToString("F1"));
            Lib.UpdateTextBoxText(txtCenterRotateAxisX2, rootAxis.CenterRotateAxisX2.ToString("F1"));
            Lib.UpdateTextBoxText(txtCenterRotateAxisY2, rootAxis.CenterRotateAxisY2.ToString("F1"));
        }

        /// <summary>
        /// Hàm load tất cả giá trị của các ROI đã được vẽ
        /// </summary>
        public void LoadROIs_Diamond()
        {
            var exp1 = new Expression("IDAction", 1);
            var exp2 = new Expression("IDAction", 2);

            List<register_location_rois> lstAllROI = _mySQLHelper.FindByExpression<register_location_rois>(exp1.Or(exp2));
            for (int i = 0; i < lstAllROI.Count; i++)
            {
                // Không load các ROI của phần hình tròn lên smartwindow (ActionID = 10) 
                if (Lib.ToInt(lstAllROI[i].IDAction) != 1 && Lib.ToInt(lstAllROI[i].IDAction) != 2) continue;
                // Từ giá trị đã có gán vào đối tượng Rectangle
                Guid ID = Lib.ToGuid(lstAllROI[i].IDGuid);
                int IDAction = Lib.ToInt(lstAllROI[i].IDAction);
                int row = Lib.ToInt(lstAllROI[i].CenterX);
                int col = Lib.ToInt(lstAllROI[i].CenterY);
                double Phi = Lib.ToDouble(lstAllROI[i].Phi);
                int Width = Lib.ToInt(lstAllROI[i].Width);
                int Height = Lib.ToInt(lstAllROI[i].Height);
                int Position = Lib.ToInt(lstAllROI[i].Height);
                GraphicsWindow.GraphicsWindow.DataRoi ROI;
                cRectangType Rec = new cRectangType(Image);
                if (row != -1 && col != -1)
                {
                    Rec.Center.Row = row;
                    Rec.Center.Col = col;
                    Rec.Center.Z = Phi;
                    Rec.Phi = Phi;
                    Rec.ActionID = IDAction;
                    Rec.Width = Width;
                    Rec.Height = Height;
                    Rec.Position = Position;
                    Rec.ID = ID;

                    // TODO: 230925 - NB
                    Rec.DescriptionCode = Lib.ToInt(lstAllROI[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstAllROI[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }
                Color color;
                if (i < 2) color = Color.Orange; // TODO: 230925 - NB
                else color = Color.GreenYellow;

                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle,
                    (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                    GraphicsWindow.GraphicsWindow.EConnectTypes.None, color, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;
                ROIS.Add(Rec.ID, Rec);
                DrawingObjects.Add(ROI.Key, ROI);
            }
            // Sẽ luôn có 2 ROI ở 2 đỉnh của Shape Kim Cương nếu chưa có thì tạo sẵn 2 ROI này
            List<cRectangType> lstROI = ROIS.Values.ToList().FindAll(x => x.ActionID == 1 || x.ActionID == 2);
            if (lstROI.Count <= 1)
            {
                CreatROI((int)SmartWindow.m_pLowerGirdle.Y, (int)SmartWindow.m_pLowerGirdle.X, 180, 1, 0);
                CreatROI((int)(SmartWindow.m_pLowerGirdle.Y - 2 * (SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y)), (int)SmartWindow.m_pLowerGirdle.X, 0, 1, 0);
            }
            else
            {
                List<GraphicsWindow.GraphicsWindow.DataRoi> lst = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
                foreach (cRectangType roi in lstROI)
                {
                    lst.Add(DrawingObjects[roi.ID]);
                }
                SmartWindow.DrawROI(lst);
            }
        }

        List<register_location_rois> lstROICircle_Calib;
        /// <summary>
        /// Load các ROI của phần tính toán pixel to MM
        /// </summary>
        // TODO: Huy - update 11/12/2024
        private void LoadROICircle_Calib()
        {
            var exp1 = new Expression("IDAction", 10);
            lstROICircle_Calib = _mySQLHelper.FindByExpression<register_location_rois>(exp1);
            for (int i = 0; i < lstROICircle_Calib.Count; i++)
            {
                // Từ giá trị đã có gán vào đối tượng Rectangle
                Guid ID = Lib.ToGuid(lstROICircle_Calib[i].IDGuid);
                int IDAction = Lib.ToInt(lstROICircle_Calib[i].IDAction);
                int row = Lib.ToInt(lstROICircle_Calib[i].CenterX);
                int col = Lib.ToInt(lstROICircle_Calib[i].CenterY);
                double Phi = Lib.ToDouble(lstROICircle_Calib[i].Phi);
                int Width = Lib.ToInt(lstROICircle_Calib[i].Width);
                int Height = Lib.ToInt(lstROICircle_Calib[i].Height);
                int Position = Lib.ToInt(lstROICircle_Calib[i].Height);
                GraphicsWindow.GraphicsWindow.DataRoi ROI;
                cRectangType Rec = new cRectangType(Image);
                if (row != -1 && col != -1)
                {
                    Rec.Center.Row = row;
                    Rec.Center.Col = col;
                    Rec.Center.Z = Phi;
                    Rec.Phi = Phi;
                    Rec.ActionID = IDAction;
                    Rec.Width = Width;
                    Rec.Height = Height;
                    Rec.Position = Position;
                    Rec.ID = ID;

                    // TODO: 230925 - NB
                    Rec.DescriptionCode = Lib.ToInt(lstROICircle_Calib[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstROICircle_Calib[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = CircleWindow.Image.Width / 2;
                    Rec.Center.Col = CircleWindow.Image.Height / 2;
                }
                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Red, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;
                ROIS.Add(Rec.ID, Rec);
                DrawingObjectsCircle.Add(ROI.Key, ROI);
            }
            List<cRectangType> lstCircleRec = ROIS.Values.ToList().FindAll(x => x.ActionID == 10);
            if (lstCircleRec.Count < 1)
            {
                CreatROICircle(-1, -1, 180, 10, 0);
                CreatROICircle(400, 400, 0, 10, 0);
            }
            else
            {
                List<GraphicsWindow.GraphicsWindow.DataRoi> lst = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
                foreach (cRectangType roi in lstCircleRec)
                {
                    if (roi.ActionID != 10) continue;
                    lst.Add(DrawingObjectsCircle[roi.ID]);
                }
                CircleWindow.DrawROI(lst);
            }
        }

        private void LoadROI_Crow_TotalDept()
        {
            var exp1 = new Expression("IDAction", 11);
            List<register_location_rois> lstROI_Crow_TotalDept = _mySQLHelper.FindByExpression<register_location_rois>(exp1);
            for (int i = 0; i < lstROI_Crow_TotalDept.Count; i++)
            {
                // Từ giá trị đã có gán vào đối tượng Rectangle
                Guid ID = Lib.ToGuid(lstROI_Crow_TotalDept[i].IDGuid);
                int IDAction = Lib.ToInt(lstROI_Crow_TotalDept[i].IDAction);
                int row = Lib.ToInt(lstROI_Crow_TotalDept[i].CenterX);
                int col = Lib.ToInt(lstROI_Crow_TotalDept[i].CenterY);
                double Phi = Lib.ToDouble(lstROI_Crow_TotalDept[i].Phi);
                int Width = Lib.ToInt(lstROI_Crow_TotalDept[i].Width);
                int Height = Lib.ToInt(lstROI_Crow_TotalDept[i].Height);
                int Position = Lib.ToInt(lstROI_Crow_TotalDept[i].Height);
                GraphicsWindow.GraphicsWindow.DataRoi ROI;
                cRectangType Rec = new cRectangType(Image);
                if (row != -1 && col != -1)
                {
                    Rec.Center.Row = row;
                    Rec.Center.Col = col;
                    Rec.Center.Z = Phi;
                    Rec.Phi = Phi;
                    Rec.ActionID = IDAction;
                    Rec.Width = Width;
                    Rec.Height = Height;
                    Rec.Position = Position;
                    Rec.ID = ID;

                    // TODO: 230925 - NB
                    Rec.DescriptionCode = Lib.ToInt(lstROI_Crow_TotalDept[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstROI_Crow_TotalDept[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }
                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Blue, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;
                ROIS.Add(Rec.ID, Rec);
                DrawingObjects_Crow_TotalDept.Add(ROI.Key, ROI);
            }
            List<cRectangType> lstCircleRec = ROIS.Values.ToList().FindAll(x => x.ActionID == 11);
            if (lstCircleRec.Count <= 1)
            {
                CreatROICircle(-1, -1, 180, 10, 0);
                CreatROICircle(400, 400, 0, 10, 0);
            }
            else
            {
                List<GraphicsWindow.GraphicsWindow.DataRoi> lst = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
                foreach (cRectangType roi in lstCircleRec)
                {
                    if (roi.ActionID != 11) continue;
                    lst.Add(DrawingObjects_Crow_TotalDept[roi.ID]);
                }
                SmartWindow.DrawROI(lst);
            }
        }

        List<register_roi_parameters> lstAllRoiProp;
        private void LoadROIParameters()
        {
            lstAllRoiProp = _mySQLHelper.FindAll<register_roi_parameters>();
            for (int i = 0; i < lstAllRoiProp.Count; i++)
            {
                // Từ giá trị đã có gán vào đối tượng Rectangle
                Guid ID = Lib.ToGuid(lstAllRoiProp[i].IDROI);
                int IDAction = Lib.ToInt(lstAllRoiProp[i].IDAction);
                string DetectType = Lib.ToString(lstAllRoiProp[i].DetectType);
                bool FillHole = Lib.ToBoolean(lstAllRoiProp[i].FillHole);
                double ThresholdMin = Lib.ToInt(lstAllRoiProp[i].ThresholdMin);
                double ThresholdMax = Lib.ToDouble(lstAllRoiProp[i].ThresholdMax);
                string EdgeTransition = Lib.ToString(lstAllRoiProp[i].EdgeTransition);
                string EdgeType = Lib.ToString(lstAllRoiProp[i].EdgeType);
                double EdgeThreshold = Lib.ToInt(lstAllRoiProp[i].EdgeThreshold);
                int SamplingPercent = Lib.ToInt(lstAllRoiProp[i].SamplingPercent);
                GraphicsWindow.GraphicsWindow.DataRoi ROI;

                cROIProperty property = new cROIProperty(ID, false, IDAction);
                property.ID = ID;
                property.ActionType = IDAction;
                property.FillHoles = FillHole;
                property.DetectType = DetectType;
                property.ThresholdRange = new List<double> { ThresholdMin, ThresholdMax };
                property.EdgeTransition = EdgeTransition;
                property.EdgeType = EdgeType;
                property.EdgeThreshold = EdgeThreshold;
                property.SamplingPercent = SamplingPercent;
                ROIProperties.Add(ID, property);
            }
        }
        /// <summary>
        /// Hàm Tạo ROI Cho CircleWindow
        /// </summary>
         // TODO: Huy - update 11/12/2024
        private void CreatROICircle(int col, int row, int phi = 0, int IDAction = 10, int NumPosition = 1)
        {
            // if (Action == null) return null;
            object resultObject = null;
            GraphicsWindow.GraphicsWindow.DataRoi ROI;
            cRectangType Rec = new cRectangType(Image);
            if (row != -1 && col != -1)
            {
                Rec.Center.Row = row;
                Rec.Center.Col = col;
                Rec.Phi = phi;
            }
            else
            {
                Rec.Center.Row = CircleWindow.Image.Width / 2;
                Rec.Center.Col = CircleWindow.Image.Height / 2;
            }

            ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, 0,
               GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Red, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, Guid.NewGuid());
            Rec.ID = ROI.Key;
            Rec.ActionID = IDAction;
            Rec.Position = NumPosition;
            ROIS.Add(Rec.ID, Rec);
            DrawingObjectsCircle.Add(ROI.Key, ROI);
            AddROIProperties(ROI.Key, ROIS.Count <= 1, IDAction);
            CircleWindow.AddRoi(ROI);
        }
        public void AddROIProperties(Guid _ID, bool _IsSelected = false, int ActionID = 0)
        {
            if (_IsSelected)
                foreach (cROIProperty roiProperty in ROIProperties.Values)
                    roiProperty.Selected = false;
            cROIProperty crp = new cROIProperty(_ID, _IsSelected, ActionID);
            if (ROIProperties.Count <= 0)
                crp.Selected = true;
            ROIProperties.Add(_ID, crp);
        }

        // Khi thay đổi giá trị của các control thì thay đổi giá trị Properties của các ROI thông qua Dictionary ROIProperties
        private void numRoi_Top_ValueChanged(object sender, EventArgs e)
        {
            SmartWindow.m_fTOP = Lib.ToDouble(numRoi_Top.Value);
        }

        private void numRoi_Girdle_ValueChanged(object sender, EventArgs e)
        {
            SmartWindow.m_fGirdle = Lib.ToDouble(numRoi_Girdle.Value);
        }

        private void numRoi_Bot_ValueChanged(object sender, EventArgs e)
        {
            SmartWindow.m_fBOT = Lib.ToDouble(numRoi_Bot.Value);
        }

        private void numRoi_Crow_ValueChanged(object sender, EventArgs e)
        {
            SmartWindow.m_fCrow = Lib.ToDouble(numRoi_Crow.Value);
        }
        private void cbEdgeTransition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ROIProperties.ContainsKey(SmartWindow.KeySelect))
            {
                ROIProperties[SmartWindow.KeySelect].EdgeTransition = Lib.ToString(cbEdgeTransition.SelectedItem);
            }
        }

        private void cbEdgeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ROIProperties.ContainsKey(SmartWindow.KeySelect))
            {
                ROIProperties[SmartWindow.KeySelect].EdgeType = Lib.ToString(cbEdgeType.SelectedItem);
            }
        }

        private void txtEdgeDetectionThreshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ROIProperties.ContainsKey(SmartWindow.KeySelect))
                {
                    ROIProperties[SmartWindow.KeySelect].EdgeThreshold = Lib.ToDouble(txtEdgeDetectionThreshold.Text);
                }
            }
        }

        private void LoadROILineFind()
        {
            List<int> lstActionID = new List<int>() { 1, 2 };
            LoadROIByListAction(lstActionID);
            List<GraphicsWindow.GraphicsWindow.DataRoi> lst = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
            List<cRectangType> lstCircleRec = ROIS.Values.ToList().FindAll(x => x.ActionID == 11);
            foreach (cRectangType roi in lstCircleRec)
            {
                if (roi.ActionID != 11) continue;
                lst.Add(DrawingObjects_Crow_TotalDept[roi.ID]);
            }
            SmartWindow.DrawROI(lst);

        }


        /// <summary>
        /// lOAD LẠI CÁC ROI THEO CÁC ACTION (SELECTED TABINDEX)
        /// </summary>
        /// <param name="ActionID"></param>
        private void LoadROIByAction(int ActionID)
        {
            // LỌC CÁC ĐỐI TƯỢNG CÓ CÁC ACTIONID mong muốn
            List<cRectangType> Datas = ROIS.Values.ToList().FindAll(x => x.ActionID == ActionID);
            List<GraphicsWindow.GraphicsWindow.DataRoi> lstROI = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
            // Hàm clear các ROI đang có trêm SmartWindow
            SmartWindow.ClearAllRoi();
            foreach (cRectangType Data in Datas)
            {
                if (DrawingObjects.ContainsKey(Data.ID))
                {
                    lstROI.Add(DrawingObjects[Data.ID]);
                }
            }
            // Hiển thị các ROI đã lọc được bên trên
            SmartWindow.DrawROI(lstROI);
        }

        private void LoadROIByListAction(List<int> ActionID)
        {
            // LỌC CÁC ĐỐI TƯỢNG CÓ CÁC ACTIONID mong muốn
            List<cRectangType> Datas = ROIS.Values.ToList().FindAll(x => ActionID.Contains(x.ActionID));
            List<GraphicsWindow.GraphicsWindow.DataRoi> lstROI = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
            // Hàm clear các ROI đang có trêm SmartWindow
            SmartWindow.ClearAllRoi();
            foreach (cRectangType Data in Datas)
            {
                if (DrawingObjects.ContainsKey(Data.ID))
                {
                    lstROI.Add(DrawingObjects[Data.ID]);
                }
            }
            // Hiển thị các ROI đã lọc được bên trên
            SmartWindow.DrawROI(lstROI);
        }

        private void LoadROIBlob()
        {
            LoadROIByAction(3);
        }

        #endregion

        #region SET LIVE CAMERA
        private bool SetLiveCamera_Hikrobot(bool isLive = true)
        {
            try
            {
                if (cDeviceManager.HikCamera == null || cDeviceManager.HikCamera.GetCameraHandle() == IntPtr.Zero) return false;
                if (isLive)
                {
                    cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
                    imageLiveCam = cDeviceManager.HikCamera?.SingleShotGrabbing();
                    StartLiveBackgroundWorker(BackgroundWorkerLiveWebCam_Hikrobot_DoWork, LiveBackgroundWorkerCompleted);
                }
                else
                    StopLiveBackgroundWorker();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private BackgroundWorker liveBackgroundWorker = null;
        // CAMERA BASLER
        private void StartLiveBackgroundWorker(DoWorkEventHandler doWorkFunc, RunWorkerCompletedEventHandler runWorkerCompletedFunc)
        {
            StopLiveBackgroundWorker();
            liveBackgroundWorker = new BackgroundWorker();
            liveBackgroundWorker.WorkerSupportsCancellation = true;
            liveBackgroundWorker.DoWork -= doWorkFunc;
            liveBackgroundWorker.DoWork += doWorkFunc;
            liveBackgroundWorker.RunWorkerCompleted -= runWorkerCompletedFunc;
            liveBackgroundWorker.RunWorkerCompleted += runWorkerCompletedFunc;
            liveBackgroundWorker.RunWorkerAsync();
        }
        private void LiveBackgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //SnapImage_Camera_Hikrobot_New();
            //if (liveBackgroundWorker != null)
            //{
            //    liveBackgroundWorker.Dispose();
            //    liveBackgroundWorker = null;
            //}
        }

        private void BackgroundWorkerLiveWebCam_Hikrobot_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsLive && SmartWindow != null)
            {
                try
                {
                    if (cDeviceManager.HikCamera == null) return;
                    if (SmartWindow == null) return;
                    bitmap_Camera = cDeviceManager.HikCamera.SingleShotGrabbing();
                    if (bitmap_Camera != null)
                    {
                        if (SmartWindow.InvokeRequired)
                        {
                            SmartWindow.Invoke(new Action(() =>
                            {
                                SmartWindow.Image = bitmap_Camera;
                            }));
                        }
                        else
                            SmartWindow.Image = bitmap_Camera;
                    }
                    if (liveBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (!IsLive) return;
                }
            }
        }
        private void StopLiveBackgroundWorker()
        {
            try
            {
                Thread.Sleep(100);
                if (liveBackgroundWorker != null && liveBackgroundWorker.IsBusy)
                    liveBackgroundWorker.CancelAsync();
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                try
                {
                    if (liveBackgroundWorker != null && liveBackgroundWorker.IsBusy)
                        liveBackgroundWorker.CancelAsync();
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        #region THÊM MỚI / XÓA ROI
        private void btnAddROI_Click(object sender, EventArgs e)
        {
            CreatROI(ColB, RowB, 0, tabAction.SelectedIndex + 1);
        }

        /// <summary>
        /// Hàm tạo các ROI
        /// </summary>
        /// <param name="col"> giá trị col hiển thị trên ảnh</param>
        /// <param name="row">giá trị row hiển thị trên ảnh</param>
        /// <param name="phi">Góc của hình hiện thị trên ảnh</param>
        /// <param name="IDAction">Action chứa Rectangle</param>
        /// 
        private void CreatROI(int col, int row, int phi = 0, int IDAction = 1, int NumPosition = 1)
        {
            // if (Action == null) return null;
            object resultObject = null;
            GraphicsWindow.GraphicsWindow.DataRoi ROI;
            cRectangType Rec = new cRectangType(Image);
            if (row != -1 && col != -1)
            {
                Rec.Center.Row = row;
                Rec.Center.Col = col;
                Rec.Phi = phi;
            }
            else
            {
                Rec.Center.Row = SmartWindow.Image.Width / 2;
                Rec.Center.Col = SmartWindow.Image.Height / 2;

            }
            ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, 0,
               GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Red, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, Guid.NewGuid());
            Rec.ID = ROI.Key;
            Rec.ActionID = IDAction;
            Rec.Position = NumPosition;
            ROIS.Add(Rec.ID, Rec);
            DrawingObjects.Add(ROI.Key, ROI);
            AddROIProperties(ROI.Key, ROIS.Count <= 1, IDAction);
            SmartWindow.AddRoi(ROI);
        }

        /// <summary>
        ///  Hàm xoá ROI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteROI_Click(object sender, EventArgs e)
        {
            /// Remove khỏi tất cả các Dictionary chứ ROI và Properties của ROI
            lstRemoveROI.Add(SmartWindow.KeySelect);
            ROIS.Remove(SmartWindow.KeySelect);
            ROIProperties.Remove(SmartWindow.KeySelect);
            // Xoá khỏi màn hình Graphics
            SmartWindow.CleraRoi(SmartWindow.KeySelect);
            DrawingObjects.Remove(SmartWindow.KeySelect);
        }
        #endregion

        #region SỰ KIỆN SMARTWINDOW
        /// <summary>
        /// Hàm thay đổi vị trí Shaoe Kim Cương đồng thời cập vị trí của 2 ROI ở đỉnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SmartWindow_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void SmartWindow_MouseUp(object sender, MouseEventArgs e)
        {
            Lib.UpdateTextBoxText(txtCuletX, SmartWindow.m_pCulet.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtCuletY, SmartWindow.m_pCulet.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableX, SmartWindow.m_pTable.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableY, SmartWindow.m_pTable.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleX, SmartWindow.m_pLowerGirdle.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleY, SmartWindow.m_pLowerGirdle.Y.ToString("F1"));

            Lib.UpdateTextBoxText(txtCulletXView, SmartWindow.m_pCulet.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtCulletYView, SmartWindow.m_pCulet.Y.ToString("F1"));

            if (IsDoubleClick)
            {
                IsDoubleClick = false;
                return;
            }

            double _distanceRequest = ((SmartWindow.m_pUpperGirdle.Y - SmartWindow.m_pUpperGirldleUp.Y) * PIXEL_TO_MM);

            #region ĐƯỜNG CẠNH TRÊN / DƯỚI KIM CƯƠNG
            // TODO: 010726 - NB
            var roiList = ROIS.Values.Where(x => x.ActionID == 1).ToList();
            double h_DIAMETER = Lib.ToDouble(_config_Rois_Diamond.DIAMETER_DIAMOND.Height);
            double w_DIAMETER = Lib.ToDouble(_config_Rois_Diamond.DIAMETER_DIAMOND.Width);
            foreach (var roi in roiList)
            {
                Guid idRoi = roi.ID;
                if (roi.DescriptionCode == 1)
                {
                    SmartWindow.MoveROI(new PointF(LowerGridle.X, LowerGridle.Y), idRoi);
                    ROIS[idRoi].Center.Row = LowerGridle.X;
                    ROIS[idRoi].Center.Col = LowerGridle.Y;

                    GraphicsWindow.GraphicsWindow.DataRoi roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                    if (roiSelected != null)
                    {
                        roiSelected.ShapeHeight = (float)h_DIAMETER;
                        roiSelected.ShapeWidth = (float)w_DIAMETER;
                    }
                    ROIS[idRoi].Height = h_DIAMETER;
                    ROIS[idRoi].Width = w_DIAMETER;
                }
                else if (roi.DescriptionCode == 2)
                {
                    SmartWindow.MoveROI(new PointF(LowerGridle.X, LowerGridle.Y - 2 * (LowerGridle.Y - Culet.Y)), idRoi);
                    ROIS[idRoi].Center.Row = LowerGridle.X;
                    ROIS[idRoi].Center.Col = LowerGridle.Y - 2 * (LowerGridle.Y - Culet.Y);
                    GraphicsWindow.GraphicsWindow.DataRoi roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                    if (roiSelected != null)
                    {
                        roiSelected.ShapeHeight = (float)h_DIAMETER;
                        roiSelected.ShapeWidth = (float)w_DIAMETER;
                    }
                    ROIS[idRoi].Height = h_DIAMETER;
                    ROIS[idRoi].Width = w_DIAMETER;
                }
            }
            #endregion

            #region ĐƯỜNG TRÊN / DƯỚI FULL 4 ĐƯỜNG CHÉO KIM CƯƠNG
            // TODO: 010726 - NB
            var roiList2 = ROIS.Values.Where(x => x.ActionID == 11 && x.DescriptionCode != 5).ToList();
            foreach (var roi in roiList2)
            {
                Guid idRoi = roi.ID;
                int _dep = roi.DescriptionCode;
                float y = 0;
                float x = 0;

                float ratio = Convert.ToSingle(_config_Rois_Diamond.TAB_DIAMOND.Ratio);
                double h_BOT = Lib.ToDouble(_config_Rois_Diamond.TAB_DIAMOND.Height);
                double w_BOT = Lib.ToDouble(_config_Rois_Diamond.TAB_DIAMOND.Width);

                double h_TAB = Lib.ToDouble(_config_Rois_Diamond.BOT_DIAMOND.Height);
                double w_TAB = Lib.ToDouble(_config_Rois_Diamond.BOT_DIAMOND.Width);

                switch (_dep)
                {
                    case 1:
                        y = SmartWindow.m_pUpperGirdle.Y - ((SmartWindow.m_pUpperGirdle.Y - SmartWindow.m_pCulet.Y) * ratio) / 100;
                        x = SmartWindow.m_pUpperGirdle.X - ((SmartWindow.m_pUpperGirdle.X - SmartWindow.m_pCulet.X) * ratio) / 100;
                        SmartWindow.MoveROI(new PointF(x, y), idRoi);
                        ROIS[idRoi].Center.Row = x;
                        ROIS[idRoi].Center.Col = y;
                        GraphicsWindow.GraphicsWindow.DataRoi roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                        if (roiSelected != null)
                        {
                            roiSelected.ShapeHeight = (float)h_BOT;
                            roiSelected.ShapeWidth = (float)w_BOT;
                        }
                        ROIS[idRoi].Height = h_BOT;
                        ROIS[idRoi].Width = w_BOT;
                        break;
                    case 2:
                        y = SmartWindow.m_pUpperGirldleUp.Y - ((SmartWindow.m_pUpperGirldleUp.Y - SmartWindow.m_pCulet.Y) * ratio) / 100;
                        x = SmartWindow.m_pUpperGirldleUp.X - ((SmartWindow.m_pUpperGirldleUp.X - SmartWindow.m_pCulet.X) * ratio) / 100;
                        SmartWindow.MoveROI(new PointF(x, y), idRoi);
                        ROIS[idRoi].Center.Row = x;
                        ROIS[idRoi].Center.Col = y;
                        roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                        if (roiSelected != null)
                        {
                            roiSelected.ShapeHeight = (float)h_BOT;
                            roiSelected.ShapeWidth = (float)w_BOT;
                        }
                        ROIS[idRoi].Height = h_BOT;
                        ROIS[idRoi].Width = w_BOT;
                        break;
                    case 3:
                        x = SmartWindow.m_pTable.X - 2;
                        y = SmartWindow.m_pLowerGirdle.Y - (float)ROIS[idRoi].Height - 2;
                        ROIS[idRoi].Center.Row = x;
                        ROIS[idRoi].Center.Col = y;
                        SmartWindow.MoveROI(new PointF(x, y), idRoi);
                        roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                        if (roiSelected != null)
                        {
                            roiSelected.ShapeHeight = (float)h_TAB;
                            roiSelected.ShapeWidth = (float)w_TAB;
                        }
                        ROIS[idRoi].Height = h_TAB;
                        ROIS[idRoi].Width = w_TAB;
                        break;
                    case 4:
                        x = SmartWindow.m_pTable.X + 2;
                        y = SmartWindow.m_pLowerGirdleUp.Y + (float)ROIS[idRoi].Height + 2;
                        ROIS[idRoi].Center.Row = x;
                        ROIS[idRoi].Center.Col = y;
                        SmartWindow.MoveROI(new PointF(x, y), idRoi);
                        roiSelected = SmartWindow.ListDataRoiOutput.Where(m => m.Key == idRoi).FirstOrDefault();
                        if (roiSelected != null)
                        {
                            roiSelected.ShapeHeight = (float)h_TAB;
                            roiSelected.ShapeWidth = (float)w_TAB;
                        }
                        ROIS[idRoi].Height = h_TAB;
                        ROIS[idRoi].Width = w_TAB;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region Lấy ra ROI đang được chọn
            diamondSizeOrigin = (SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y) * 2;
            CurrentDrawings = DrawingObjects.FirstOrDefault(x => x.Key == SmartWindow.KeySelect).Value;
            cRectangType ROISelected;
            if (CurrentDrawings != null)
            {
                ROISelected = ((cRectangType)ROIS[SmartWindow.KeySelect]);
                var DataRoiSelected = SmartWindow.ListDataRoiOutput.Find(X => X.Key == SmartWindow.KeySelect);
                ROISelected.Height = DataRoiSelected.ShapeHeight;
                ROISelected.Width = DataRoiSelected.ShapeWidth;
                ROISelected.Phi = DataRoiSelected.Angle;
                ROISelected.Center.Row = DataRoiSelected.CenterPointX;
                ROISelected.Center.Col = DataRoiSelected.CenterPointY;
                ROISelected.ID = DataRoiSelected.Key;
                ROISelected.Color = DataRoiSelected.ColorRoi;
                ROIS[(SmartWindow.KeySelect)] = ROISelected;
                DrawingObjects[SmartWindow.KeySelect] = SmartWindow.ListDataRoiOutput.Find(X => X.Key == SmartWindow.KeySelect);
            }
            else
            {
                CurrentDrawings = DrawingObjects_Crow_TotalDept.FirstOrDefault(x => x.Key == SmartWindow.KeySelect).Value;
                if (CurrentDrawings != null)
                {
                    ROISelected = ((cRectangType)ROIS[SmartWindow.KeySelect]);
                    var DataRoiSelected = SmartWindow.ListDataRoiOutput.Find(X => X.Key == SmartWindow.KeySelect);
                    ROISelected.Height = DataRoiSelected.ShapeHeight;
                    ROISelected.Width = DataRoiSelected.ShapeWidth;
                    ROISelected.Phi = DataRoiSelected.Angle;
                    ROISelected.Center.Row = DataRoiSelected.CenterPointX;
                    ROISelected.Center.Col = DataRoiSelected.CenterPointY;
                    ROISelected.ID = DataRoiSelected.Key;
                    ROISelected.Color = DataRoiSelected.ColorRoi;
                    ROIS[(SmartWindow.KeySelect)] = ROISelected;
                    DrawingObjects_Crow_TotalDept[SmartWindow.KeySelect] = SmartWindow.ListDataRoiOutput.Find(X => X.Key == SmartWindow.KeySelect);
                }
            }
            if (tabAction.SelectedIndex == 1)
                contextMenuStrip1.Hide();

            if (ROIProperties.ContainsKey(SmartWindow.KeySelect))
                SwitchRoi();
            #endregion
        }
        private void SwitchRoi(bool onlyGetValue = false)
        {
            try
            {
                if (CurrentDrawings == null) return;
                var roiProperties = ROIProperties.Values.Where(x => x.Selected);
                cROIProperty crp = null;
                // chuyển ROI được chọn trước đó sang Selected = false
                if (roiProperties != null && roiProperties.Any())
                {
                    crp = (cROIProperty)roiProperties.First();
                    crp.Selected = false;
                    ROIProperties[crp.ID] = crp;
                }

                if (onlyGetValue) return;
                // Chuyển ROI được chọn hiện tại Selected = false;
                if (CurrentDrawings != null && ROIProperties.ContainsKey(CurrentDrawings.Key))
                {
                    crp = ROIProperties[CurrentDrawings.Key];
                    crp.Selected = true;
                    ROIProperties[crp.ID] = crp;
                    // Hiển thị dữ liệu  thuộc tính của ROI này lên các control
                    BindingDataToControls(crp);
                }

            }
            catch (Exception ex)
            {
                //Lib.saveToLog(ex.Message);
            }
        }

        /// <summary>
        ///  Hàm hiển thị lại các giá trị của ROI lên các controls
        /// </summary>
        /// <param name="ROI"></param>
        private void BindingDataToControls(cROIProperty ROI)
        {
            try
            {
                // Tool FineLine
                if (ROI.ActionType == 1)
                {
                    cbEdgeTransition.SelectedItem = ROI.EdgeTransition;
                    cbEdgeType.SelectedItem = ROI.EdgeType;
                    txtEdgeDetectionThreshold.Text = Lib.ToString(ROI.EdgeThreshold);
                    numSamplingPercent.Value = ROI.SamplingPercent;
                }
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region LƯU CÁC ROI, ROIPROPERTIES VÀ THÔNG SỐ SHAPE KIM CƯƠNG
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveDiamondShape();
                SaveROI();
                RemoveROI();

                // TODO: 041224 - NB
                GlobVar.Roi_Bot = numRoi_Bot.Value;
                GlobVar.Roi_Top = numRoi_Top.Value;
                GlobVar.Roi_Girdle = numRoi_Girdle.Value;
                GlobVar.Roi_Crow = numRoi_Crow.Value;
            }
        }

        /// <summary>
        /// Remove các ROI đã bị xóa khỏi DB
        /// </summary>
        private void RemoveROI()
        {
            if (lstRemoveROI.Count <= 0) return;
            foreach (var item in lstRemoveROI)
            {
                var exp1 = new Expression("IDGuid", item);
                _mySQLHelper.DeleteByExpression<register_location_rois>(exp1);
            }
        }

        /// <summary>
        /// Hàm lưu giá trị của Shape Kim Cương
        /// </summary>
        /// <returns></returns>
        public bool SaveDiamondShape()
        {
            // TODO: 270825 - NB
            if (Culet.X == 0 || Culet.Y == 0 || LowerGridle.X == 0 || LowerGridle.Y == 0 || mTable.X == 0 || mTable.Y == 0)
                return false;

            shape_diamond_infor shapeDiamond = _mySQLHelper.FindAll<shape_diamond_infor>().FirstOrDefault();
            shapeDiamond.TOP = numRoi_Top.Value;
            shapeDiamond.Girdle = numRoi_Girdle.Value;
            shapeDiamond.BOT = numRoi_Bot.Value;
            shapeDiamond.Crow = numRoi_Crow.Value;
            shapeDiamond.CuletX = Lib.ToDecimal(Culet.X);
            shapeDiamond.CuletY = Lib.ToDecimal(Culet.Y);
            shapeDiamond.LowerGirdleX = Lib.ToDecimal(LowerGridle.X);
            shapeDiamond.LowerGirdleY = Lib.ToDecimal(LowerGridle.Y);
            shapeDiamond.TableX = Lib.ToDecimal(mTable.X);
            shapeDiamond.TableY = Lib.ToDecimal(mTable.Y);
            shapeDiamond.MeasurementType_Concentric = cbMeasurementType_Concentric.SelectedIndex;
            shapeDiamond.MeasurementType_Concentric_Text = cbMeasurementType_Concentric.Text.Trim();
            shapeDiamond.MeasurementType_Grinding = cbMeasurementType_Grinding.SelectedIndex;
            shapeDiamond.MeasurementType_Grinding_Text = cbMeasurementType_Grinding.Text.Trim();
            shapeDiamond.MeasurementType_Inspect = cbMeasurementType_Inspect.SelectedIndex;
            shapeDiamond.MeasurementType_Inspect_Text = cbMeasurementType_Inspect.Text.Trim();
            try
            {
                if (shapeDiamond != null)
                    _mySQLHelper.Update<shape_diamond_infor>(shapeDiamond);
                else
                    _mySQLHelper.Insert<shape_diamond_infor>(shapeDiamond);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm lưu tất cả các ROI đã tạo cho các tool
        /// </summary>
        /// <returns></returns>
        public bool SaveROI()
        {
            foreach (cRectangType ROI in ROIS.Values)
            {
                register_location_rois roi = _mySQLHelper.FindByField<register_location_rois>("IDGUID", ROI.ID);

                try
                {
                    if (roi.ID != 0)
                    {
                        roi.CenterX = Lib.ToInt(ROI.Center.Row);
                        roi.CenterY = Lib.ToInt(ROI.Center.Col);
                        roi.Phi = Lib.ToInt(ROI.Phi);
                        roi.Width = Lib.ToInt(ROI.Width);
                        roi.Height = Lib.ToInt(ROI.Height);
                        _mySQLHelper.Update<register_location_rois>(roi);

                    }
                    else
                    {
                        roi.CenterX = Lib.ToInt(ROI.Center.Row);
                        roi.CenterY = Lib.ToInt(ROI.Center.Col);
                        roi.Phi = Lib.ToInt(ROI.Phi);
                        roi.Width = Lib.ToInt(ROI.Width);
                        roi.Height = Lib.ToInt(ROI.Height);
                        roi.IDGuid = ROI.ID.ToString();
                        roi.IDAction = ROI.ActionID;
                        _mySQLHelper.Insert<register_location_rois>(roi);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public void SaveROIProperties()
        {
            foreach (cROIProperty data in ROIProperties.Values)
            {
                register_roi_parameters selectedROI = _mySQLHelper.FindByField<register_roi_parameters>("IDROI", data.ID);
                selectedROI.IDAction = data.ActionType;
                selectedROI.DetectType = data.DetectType;
                selectedROI.FillHole = data.FillHoles.ToString();
                selectedROI.ThresholdMin = Lib.ToDecimal(data.ThresholdRange[0]);
                selectedROI.ThresholdMax = Lib.ToDecimal(data.ThresholdRange[1]);
                selectedROI.EdgeTransition = data.EdgeTransition;
                selectedROI.EdgeType = data.EdgeType;
                selectedROI.EdgeThreshold = Lib.ToDecimal(data.EdgeThreshold);
                selectedROI.SamplingPercent = data.SamplingPercent;
                try
                {
                    if (selectedROI != null)
                    {
                        _mySQLHelper.Update<register_roi_parameters>(selectedROI);
                    }
                    else
                    {
                        selectedROI.IDROI = data.ID.ToString();
                        _mySQLHelper.Insert<register_roi_parameters>(selectedROI);
                    }
                }
                catch (Exception ex)
                {
                    Lib.saveToErr(ex.ToString());
                }
            }
        }
        #endregion

        #region CHẠY CÁC TOOL VISION
        /// <summary>
        /// Load Ảnh để test các tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileImage = new OpenFileDialog();
            OpenFileImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (OpenFileImage.ShowDialog() == DialogResult.OK)
            {
                InputImage = new Bitmap(OpenFileImage.FileName);
                SmartWindow.Image = InputImage;
                GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)InputImage.Clone());
                DisplayImage = GrayImage.Convert<Bgr, byte>();
            }
        }


        List<double> LineTest1 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest2 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest3 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest4 = new List<double>() { 0, 0, 0, 0 };
        private void btnDistanceMeasure_Click(object sender, EventArgs e)
        {
            Bitmap bitmapImage = null;
            lock (_lockImage)
                bitmapImage = (Bitmap)InputImage.Clone();
            // Set các input cho tool 
            distance = CalcLineToLine(bitmapImage, Line1, Line2, true);
            lblDistance.Text = distance.ToString("F2");
            PointF intersectPoint = FindIntersection((Line1[1], Line1[0]), (Line1[3], Line1[2]), (Line11[1], Line11[0]), (Line11[3], Line11[2]));

            PointF inter1 = FindIntersection((Line11[1], Line11[0]), (Line11[3], Line11[2]), (Line1[1], Line1[0]), (Line1[3], Line1[2]));
            LineTest1 = new List<double>() { inter1.Y, inter1.X, Line1[0], Line1[1] };
            PointF intersectPoint1 = FindIntersection((LineTest1[1], LineTest1[0]), (LineTest1[3], LineTest1[2]), (Line13[1], Line13[0]), (Line13[3], Line13[2]));
            LineTest2 = new List<double>() { intersectPoint1.Y, intersectPoint1.X, LineTest1[0], LineTest1[1] };
            //DisplayGraphics(SmartWindow, LineTest2, DisplayImage, Color.Yellow);
            double crowHeight1 = Math.Sqrt((LineTest2[0] - LineTest2[2]) * (LineTest2[0] - LineTest2[2]) + (LineTest2[1] - LineTest2[3]) * (LineTest2[1] - LineTest2[3]));

            #region Điểm giao nhau của đường chéo trên / dưới để lấy ra tâm kim cương
            PointF inter_GiaoNhau = FindIntersection((Line12[1], Line12[0]), (Line12[3], Line12[2]), (Line2[1], Line2[0]), (Line2[3], Line2[2]));
            LineTest3 = new List<double>() { inter_GiaoNhau.Y, inter_GiaoNhau.X, Line2[0], Line2[1] };
            PointF intersectPoint2 = FindIntersection((LineTest3[1], LineTest3[0]), (LineTest3[3], LineTest3[2]), (Line14[1], Line14[0]), (Line14[3], Line14[2]));
            LineTest4 = new List<double>() { intersectPoint2.Y, intersectPoint2.X, LineTest3[0], LineTest3[1] };
            #endregion

            // DisplayGraphics(SmartWindow, LineTest4, DisplayImage, Color.Yellow);
            double crowHeight2 = Math.Sqrt((LineTest4[0] - LineTest4[2]) * (LineTest4[0] - LineTest4[2]) + (LineTest4[1] - LineTest4[3]) * (LineTest4[1] - LineTest4[3]));
            double average = (crowHeight1 + crowHeight2) / 2;
            lblCrowHeight.Text = average.ToString("F2");

            List<double> LineTest5 = new List<double>() { 0, 0, 0, 0 };
            LineTest5 = new List<double>() { LineTest2[0], LineTest2[1], LineTest4[0], LineTest4[1] };
            DisplayGraphics(SmartWindow, LineTest5, DisplayImage, Color.Green); // FromArgb(255, 128, 0)

            List<double> LineTest6 = new List<double>() { 0, 0, 0, 0 };
            PointF inter_S = new PointF((float)LineTest5[1], (float)LineTest5[0]);
            PointF inter_E = new PointF((float)LineTest5[3], (float)LineTest5[2]);
            // Lấy điểm vuông góc từ tâm kim cương đến cạnh bên
            PointF inter_Test = GetRightAnglePoint(inter_GiaoNhau, inter_E, inter_S);
            LineTest6 = new List<double>() { inter_GiaoNhau.Y, inter_GiaoNhau.X, inter_Test.Y, inter_Test.X };
            // Vẽ đường từ tâm đến cạnh bên màu trắng
            DisplayGraphics(SmartWindow, LineTest6, DisplayImage, Color.White);
            totalDeep = Math.Sqrt((LineTest6[0] - LineTest6[2]) * (LineTest6[0] - LineTest6[2]) + (LineTest6[1] - LineTest6[3]) * (LineTest6[1] - LineTest6[3]));
            lblTotalDeep.Text = totalDeep.ToString("F2");
        }

        // TODO: 210825 - NB
        public static PointF GetRightAnglePoint(PointF PointCenter, PointF StartPointLine, PointF EndPointLine)
        {
            float dx = EndPointLine.X - StartPointLine.X;
            float dy = EndPointLine.Y - StartPointLine.Y;

            // Nếu đường thẳng là một điểm
            if (dx == 0 && dy == 0)
                return StartPointLine;

            // Tính hệ số t cho hình chiếu vuông góc
            float t = ((PointCenter.X - StartPointLine.X) * dx + (PointCenter.Y - StartPointLine.Y) * dy) /
                      (dx * dx + dy * dy);

            // Tính điểm trên đường thẳng mà đường nối từ point đến đó vuông góc
            float hx = StartPointLine.X + t * dx;
            float hy = StartPointLine.Y + t * dy;

            return new PointF(hx, hy);
        }

        /// <summary>
        /// Tính khoảng cách giữa 2 điểm
        /// </summary>
        /// <param name="InputImage"> ẢNH INPUT</param>
        /// <param name="Point1">Điểm thứ nhất</param>
        /// <param name="Point2">Điểm thứ 2</param>
        /// <returns></returns>
        private double CalcPointToPoint(Bitmap InputImage, PointF Point1, PointF Point2)
        {
            DistanceMeasure = new DistanceMeasurementTool.Measurement();
            double Value = 0;
            DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_PointToPoint;
            DistanceMeasure.Point = new List<Point>() { new Point((int)Point1.X, (int)Point1.Y) };
            DistanceMeasure.PointTwo = new List<Point>() { new Point((int)Point2.X, (int)Point2.Y) };
            DistanceMeasure.InputImage = InputImage;
            DistanceMeasure.IsShowImageResult = true;
            DistanceMeasure.Tolerance = new Tuple<double, double, double>(0, 0, 0);
            DistanceMeasure.MeasurementType = cDistanceMeasurementTool.MeasurementType_Euclidean;
            bool Result = DistanceMeasure.Run();
            DistancePoint = new List<double>() { Point1.X, Point1.Y, Point2.X, Point2.Y };
            // Lấy output của các tool
            if (DistanceMeasure.Distance.Count > 0)
            {
                Value = DistanceMeasure.Distance[0];
            }
            DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Lime);
            return Value;
        }

        private double CalcLineToLine(Bitmap InputImage, List<double> Line1, List<double> Line2, bool isShowLine)
        {
            double Value = 0;
            DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_LineToLine;
            DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line1[0]), Lib.ToInt(Line1[1])), new Point(Lib.ToInt(Line1[2]), Lib.ToInt(Line1[3])));
            DistanceMeasure.LineTwo = new Tuple<Point, Point>(new Point(Lib.ToInt(Line2[0]), Lib.ToInt(Line2[1])), new Point(Lib.ToInt(Line2[2]), Lib.ToInt(Line2[3])));
            DistanceMeasure.InputImage = InputImage;
            DistanceMeasure.IsShowImageResult = true;
            DistanceMeasure.Tolerance = new Tuple<double, double, double>(0, 0, 0);
            DistanceMeasure.MeasurementType = cDistanceMeasurementTool.MeasurementType_MidPointToLine;
            bool Result = DistanceMeasure.Run();
            DistancePoint = new List<double>() { DistanceMeasure.OutPoint1.X, DistanceMeasure.OutPoint1.Y, DistanceMeasure.OutPoint2.X, DistanceMeasure.OutPoint2.Y };
            // Lấy output của các tool
            if (DistanceMeasure.Distance.Count > 0)
            {
                Distance = DistanceMeasure.Distance[0];
            }
            if (isShowLine)
                DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Red);
            return Distance;
        }

        private double CalcPointToLine(Bitmap InputImage, Point Point1, List<double> Line2, bool isShowLine)
        {
            double Value = 0;
            DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_PointToLine;
            DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line1[0]), Lib.ToInt(Line1[1])), new Point(Lib.ToInt(Line1[2]), Lib.ToInt(Line1[3])));
            DistanceMeasure.Point = new List<Point> { Point1 };
            DistanceMeasure.InputImage = InputImage;
            DistanceMeasure.IsShowImageResult = true;
            DistanceMeasure.Tolerance = new Tuple<double, double, double>(0, 0, 0);
            DistanceMeasure.MeasurementType = cDistanceMeasurementTool.MeasurementType_MidPointToMidPoint;
            bool Result = DistanceMeasure.Run();
            DistancePoint = new List<double>() { DistanceMeasure.OutPoint1.X, DistanceMeasure.OutPoint1.Y, DistanceMeasure.OutPoint2.X, DistanceMeasure.OutPoint2.Y };
            // Lấy output của các tool
            if (DistanceMeasure.Distance.Count > 0)
            {
                Distance = DistanceMeasure.Distance[0];
            }
            if (isShowLine)
                DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Yellow);
            return Distance;
        }


        /// <summary>
        ///  chạy tool Blob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunBlob_Click(object sender, EventArgs e)
        {
            BlobTool = new BlobTool();
            RTCRectangle Data = new RTCRectangle();
            List<RTCRectangle> ROIs = new List<RTCRectangle>();
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
            Tuple<Point, double> toolOriginF = Tuple.Create(new Point(0, 0), 0.0);
            SetOrigin = new SetOrigin.SetOrigin();
            BlobTool.ToolOrigin = toolOrigin;
            BlobTool.InputImage = GrayImage;
            Guid OldID = Guid.Empty;
            foreach (object ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 10))
            {
                cDrawingBaseType ROIB = (cDrawingBaseType)ROI;
                OldID = ROIB.ID;
                switch (ROIB.DrawingType)
                {
                    case EDrawingtypes.Rectang:
                        {
                            cRectangType Rec = (cRectangType)ROI;
                            Data = new RTCRectangle();
                            Data.Phi = (float)Rec.Phi * Math.PI / 180;
                            Data.Width = (float)Rec.Width * 2;
                            Data.Height = (float)Rec.Height * 2;
                            Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                            ROIs.Add(Data);
                            //SmartWindow.DrawROI(DataRoi);
                            break;
                        }
                }
            }
            // Giá trị input tool LineFind
            BlobTool.ROI = ROIs;
            BlobTool.ToolOrigin = toolOrigin;
            BlobTool.InSetOrigin = new List<PointF[]> { SetOrigin.Run(Data, toolOrigin) };
            BlobTool.DetectType = cBlobTool.DectectType_DetectInRangeBlobs;
            Blobs.GreyLevelThresholdType = cBlobTool.GreyLevelThresholdType_FixedThresholdRange;
            Blobs.ThresholdRange = Tuple.Create(int.Parse(txbThresMin.Text), int.Parse(txbThresMax.Text));
            BlobTool.FillHoles = true;
            BlobTool.IsShowImageResult = true;
            BlobTool.GreyLevelThresholdType = cBlobTool.GreyLevelThresholdType_FixedThresholdRange;
            BlobTool.RequireNumberOfBlobs = new Tuple<int, int>(0, 10);

            bool result = BlobTool.Run();
            // Giá trị output tool
            string err = LineFind.ErrMessage;
            //SmartWindow.Image = BlobTool.OutputImageShow;

            int Height = BlobTool.OutputHeightList.Max();
            double scale = double.Parse(txbActualSize.Text) / Height;
            textBox2.Text = Height.ToString();
            txbScale.Text = scale.ToString();
            textBox1.Text = ((scale * 1000) / 2).ToString();
            CircleWindow.Image = Blobs.OutputImage;
        }
        PointF inter = new PointF(0, 0);
        private void btnRunLineFind_VisionTools_Click(object sender, EventArgs e)
        {
            chkMove_ROI.Checked = false;
            if (DisplayImage == null) return;
            Image<Bgr, byte> cloneImage = new Image<Bgr, byte>(DisplayImage.Size);
            DisplayImage.CopyTo(cloneImage);
            //SmartWindow.Image = DisplayImage.ToBitmap();
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
            SetOrigin = new SetOrigin.SetOrigin();

            #region COMMENT LẠI?
            List<RTCRectangle> ROIs = new List<RTCRectangle>();
            int count = 0;
            RTCRectangle Data = new RTCRectangle();
            foreach (object ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 && x.Position == 1))
            {
                LineFind = new LineFind();
                LineFind.ToolOrigin = toolOrigin;
                cDrawingBaseType ROIB = (cDrawingBaseType)ROI;
                Guid OldID = ROIB.ID;
                switch (ROIB.DrawingType)
                {
                    case EDrawingtypes.Rectang:
                        {
                            cRectangType Rec = (cRectangType)ROI;
                            Data = new RTCRectangle();
                            Data.Phi = (float)Rec.Phi * Math.PI / 180;
                            Data.Width = (float)Rec.Width * 2;
                            Data.Height = (float)Rec.Height * 2;
                            Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                            ROIs.Add(Data);
                            //SmartWindow.DrawROI(DataRoi);
                            break;
                        }
                }
                // Giá trị input tool LineFind
                LineFind.ROI = Data;
                LineFind.ToolOrigin = toolOrigin;
                LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                LineFind.InputImage = GrayImage;
                LineFind.EdgeTransition = ROIProperties[OldID].EdgeTransition;
                LineFind.EdgeType = ROIProperties[OldID].EdgeType;
                LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties[OldID].EdgeThreshold);
                LineFind.SamplingPercent = ROIProperties[OldID].SamplingPercent;
                LineFind.OutlierDistanceThreshold = 1;
                LineFind.StepFindPoint = 1;
                LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties[OldID].MaxPercentageOfOutliers);
                LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties[OldID].MinEdgePointNumber);
                LineFind.IsShowImageResult = true;

                bool result = LineFind.Run();
                // Giá trị output tool
                string err = LineFind.ErrMessage;

                if (LineFind.LineSegment != null && count == 0)
                {
                    Line1 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    txtLine1X1.Text = Line1[0].ToString();
                    txtLine1Y1.Text = Line1[1].ToString();
                    txtLine1X2.Text = Line1[2].ToString();
                    txtL1Y2.Text = Line1[3].ToString();
                    DisplayGraphics(SmartWindow, Line1, cloneImage, Color.Green);
                }
                else if (LineFind.LineSegment != null && count == 1)
                {
                    Line2 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    txtLine2X1.Text = Line2[0].ToString();
                    txtLine2Y1.Text = Line2[1].ToString();
                    txtLine2X2.Text = Line2[2].ToString();
                    txtLine2Y2.Text = Line2[3].ToString();
                    DisplayGraphics(SmartWindow, Line2, cloneImage, Color.Green);

                }
                else if (LineFind.LineSegment != null && count == 2)
                {
                    Line3 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line3, cloneImage, Color.Green);
                }
                else if (LineFind.LineSegment != null && count == 3)
                {
                    Line4 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line4, cloneImage, Color.Green);
                }
                count++;
            }
            count = 0;
            foreach (object ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 && x.Position != 1))
            {
                LineFind = new LineFind();
                LineFind.ToolOrigin = toolOrigin;
                cDrawingBaseType ROIB = (cDrawingBaseType)ROI;
                Guid OldID = ROIB.ID;
                switch (ROIB.DrawingType)
                {
                    case EDrawingtypes.Rectang:
                        {
                            cRectangType Rec = (cRectangType)ROI;
                            Data = new RTCRectangle();
                            Data.Phi = (float)Rec.Phi * Math.PI / 180;
                            Data.Width = (float)Rec.Width * 2;
                            Data.Height = (float)Rec.Height * 2;
                            Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                            ROIs.Add(Data);
                            //SmartWindow.DrawROI(DataRoi);
                            break;
                        }
                }
                // Giá trị input tool LineFind
                LineFind.ROI = Data;
                LineFind.ToolOrigin = toolOrigin;
                LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                LineFind.InputImage = GrayImage;
                LineFind.EdgeTransition = ROIProperties[OldID].EdgeTransition;
                LineFind.EdgeType = ROIProperties[OldID].EdgeType;
                LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties[OldID].EdgeThreshold);
                LineFind.SamplingPercent = ROIProperties[OldID].SamplingPercent;
                LineFind.OutlierDistanceThreshold = 1;
                LineFind.StepFindPoint = 1;
                LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties[OldID].MaxPercentageOfOutliers);
                LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties[OldID].MinEdgePointNumber);
                LineFind.IsShowImageResult = true;

                bool result = LineFind.Run();
                // Giá trị output tool
                string err = LineFind.ErrMessage;
                if (err != null)
                {

                }
                //SmartWindow.Image = LineFind.OutputImageShow;
                //LineFind.OutputImageShow.Save($"D:\\test +{count}.bmp");
                // Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 0)
                {
                    Line1 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    txtLine1X1.Text = Line1[0].ToString();
                    txtLine1Y1.Text = Line1[1].ToString();
                    txtLine1X2.Text = Line1[2].ToString();
                    txtL1Y2.Text = Line1[3].ToString();
                    DisplayGraphics(SmartWindow, Line1, cloneImage, Color.Lime);
                }
                else if (LineFind.LineSegment != null && count == 1)
                {
                    Line2 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    txtLine2X1.Text = Line2[0].ToString();
                    txtLine2Y1.Text = Line2[1].ToString();
                    txtLine2X2.Text = Line2[2].ToString();
                    txtLine2Y2.Text = Line2[3].ToString();
                    DisplayGraphics(SmartWindow, Line2, cloneImage, Color.Lime);
                }
                else if (LineFind.LineSegment != null && count == 2)
                {
                    Line3 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line3, cloneImage, Color.Lime);
                }
                else if (LineFind.LineSegment != null && count == 3)
                {
                    Line4 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line4, cloneImage, Color.Lime);
                }
                count++;
            }
            count = 0;
            foreach (object ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 2))
            {
                LineFind = new LineFind();
                LineFind.ToolOrigin = toolOrigin;
                cDrawingBaseType ROIB = (cDrawingBaseType)ROI;
                Guid OldID = ROIB.ID;
                switch (ROIB.DrawingType)
                {
                    case EDrawingtypes.Rectang:
                        {
                            cRectangType Rec = (cRectangType)ROI;
                            Data = new RTCRectangle();
                            Data.Phi = (float)Rec.Phi * Math.PI / 180;
                            Data.Width = (float)Rec.Width * 2;
                            Data.Height = (float)Rec.Height * 2;
                            Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                            ROIs.Add(Data);
                            //SmartWindow.DrawROI(DataRoi);
                            break;
                        }
                }
                // Giá trị input tool LineFind
                LineFind.ROI = Data;
                LineFind.ToolOrigin = toolOrigin;
                LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                LineFind.InputImage = GrayImage;
                LineFind.EdgeTransition = ROIProperties[OldID].EdgeTransition;
                LineFind.EdgeType = ROIProperties[OldID].EdgeType;
                LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties[OldID].EdgeThreshold);
                LineFind.SamplingPercent = ROIProperties[OldID].SamplingPercent;
                LineFind.OutlierDistanceThreshold = 1;
                LineFind.StepFindPoint = 1;
                LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties[OldID].MaxPercentageOfOutliers);
                LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties[OldID].MinEdgePointNumber);
                LineFind.IsShowImageResult = true;

                bool result = LineFind.Run();
                // Giá trị output tool
                string err = LineFind.ErrMessage;
                //SmartWindow.Image = LineFind.OutputImageShow;
                //LineFind.OutputImageShow.Save($"D:\\test +{count}.bmp");
                // Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 0)
                {
                    Line7 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line7, cloneImage, Color.Lime);
                }
                else if (LineFind.LineSegment != null && count == 1)
                {
                    Line8 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line8, cloneImage, Color.Lime);
                }
                count++;
            }
            #endregion

            #region Line tâm đường kính kim cương
            Line5 = new List<double>()
            {
                 Line1[2]+100,
                 (Line1[1] + Line2[1])/2,  // Y1 trung bình
                 SmartWindow.m_pCulet.X,
                 (Line1[3] + Line2[3])/2   // Y2 trung bình
            };
            // DisplayGraphics(SmartWindow, Line5, cloneImage, Color.Lime);
            #endregion

            #region Line tâm trục phai (trục xoay)
            if (Line3 != null)
            {
                Line6 = new List<double>()
                {
                 Line3[2],
                 (Line3[1] + Line4[1])/2,  // Y1 trung bình
                 Line4[2],
                 (Line3[3] + Line4[3])/2   // Y2 trung bình
                };
                // TODO: 291124 - HUY
                DisplayGraphics(SmartWindow, Line6, cloneImage, Color.Yellow);

                txtCenterRotateAxisX1.Text = Line6[0].ToString();
                txtCenterRotateAxisY1.Text = Line6[1].ToString();
                txtCenterRotateAxisX2.Text = Line6[2].ToString();
                txtCenterRotateAxisY2.Text = Line6[3].ToString();

                point1 = new Point(Lib.ToInt(Line1[2]), Lib.ToInt((Line1[1] + Line2[1]) / 2));
                point2 = new Point(Lib.ToInt(Line1[2]), Lib.ToInt((Line3[3] + Line4[3]) / 2));
                point3 = new Point(Lib.ToInt(Line1[2] + 300), Lib.ToInt(Line6[1]));
                point4 = new Point(Lib.ToInt(Line1[2] + 300), Lib.ToInt(Line2[1]));
            }
            #endregion

            #region Line tâm trục bên trái
            if (Line7 != null && Line8 != null)
            {
                Line9 = new List<double>()
                {
                 Line7[2],
                 (Line7[1] + Line8[1])/2,  // Y1 trung bình
                 Line8[2],
                 (Line7[3] + Line8[3])/2   // Y2 trung bình
                };
                // TODO: 291124 - HUY
                DisplayGraphics(SmartWindow, Line9, cloneImage, Color.Yellow);
            }
            #endregion

            if (chkIntersect.Checked)
            {
                // TODO: 141224
                LineFindRun_Crow_TotalDept();

                inter = FindIntersection((Line11[1], Line11[0]), (Line11[3], Line11[2]), (Line12[1], Line12[0]), (Line12[3], Line12[2]));
                Line11_Intersect = new List<double>() { inter.Y, inter.X, Line11[2], Line11[3] };
                DisplayGraphics(SmartWindow, Line11_Intersect, DisplayImage, Color.DarkBlue);
                Line12_Intersect = new List<double>() { inter.Y, inter.X, Line12[0], Line12[1] };
                DisplayGraphics(SmartWindow, Line12_Intersect, DisplayImage, Color.DarkBlue);
            }
        }
        #endregion

        #region HIỂN THỊ LÊN SMARTWINDOW
        public static void DisplayGraphics(GraphicsWindow.GraphicsWindow smartWindow, object graphicsDisplay, Image<Bgr, byte> inputImage, Color color)
        {

            if (graphicsDisplay.GetType() == typeof(List<VectorOfVectorOfPoint>))
            {
                List<VectorOfVectorOfPoint> Region = (List<VectorOfVectorOfPoint>)graphicsDisplay;
                for (int i = 0; i < Region.Count; i++)
                {
                    //CvInvoke.DrawContours(imgShow, _outputBlobList[i], -1, new MCvScalar(0, 255, 0), -1);
                    CvInvoke.FillPoly(inputImage, Region[i], new MCvScalar(0, 255, 0));
                }
                DisplayImage = inputImage;
            }
            else if (graphicsDisplay.GetType() == typeof(List<double>))
            {
                List<double> Region = (List<double>)graphicsDisplay;
                if (Region.Count > 3)
                {
                    CvInvoke.ArrowedLine(inputImage, new Point(Lib.ToInt(Region[0]), Lib.ToInt(Region[1])), new Point(Lib.ToInt(Region[2]), Lib.ToInt(Region[3])), new MCvScalar(color.B, color.G, color.R), 5);
                }
                DisplayImage = inputImage;
            }
            var imgShow = DisplayImage.Convert<Bgr, byte>();
            smartWindow.Image = imgShow.ToBitmap();
        }
        #endregion

        #region SAVE THÔNG SỐ CAMERA
        private void btnSave_Camera_Live_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup camera live không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // TODO: 271124 - NB
                string dbName = "settings_camera";
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={numGainLive.Value} WHERE ParamName = 'GainLive'; ");
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={num_ExposureLive.Value} WHERE ParamName = 'ExposureLive'; ");
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={num_GammaLive.Value} WHERE ParamName = 'GammaLive'; ");
            }
        }

        private void btnSave_Camera_Snap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup camera live không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // TODO: 271124 - NB
                string dbName = "settings_camera";
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={numGainSnap.Value} WHERE ParamName = 'GainSnap'; ");
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={num_ExposureSnap.Value} WHERE ParamName = 'ExposureSnap'; ");
                _mySQLHelper.ExecuteScalar($"UPDATE {dbName} set ParamValue ={num_GammaSnap.Value} WHERE ParamName = 'GammaSnap'; ");
            }
        }

        // TODO: 08052026 - NB
        private void btnPlusCenterRotateAxisY_Click(object sender, EventArgs e)
        {
            decimal centerRotateAxisY1 = Lib.ToDecimal(txtCenterRotateAxisY1.Text) + 1;
            decimal centerRotateAxisY2 = Lib.ToDecimal(txtCenterRotateAxisY2.Text) + 1;
            txtCenterRotateAxisY1.Text = centerRotateAxisY1.ToString();
            txtCenterRotateAxisY2.Text = centerRotateAxisY2.ToString();
        }
        private void btnMinusCenterRotateAxisY_Click(object sender, EventArgs e)
        {
            decimal centerRotateAxisY1 = Lib.ToDecimal(txtCenterRotateAxisY1.Text) - 1;
            decimal centerRotateAxisY2 = Lib.ToDecimal(txtCenterRotateAxisY2.Text) - 1;
            txtCenterRotateAxisY1.Text = centerRotateAxisY1.ToString();
            txtCenterRotateAxisY2.Text = centerRotateAxisY2.ToString();
        }

        private void btnSaveRoot_Axis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                register_root_axis _model = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();
                if (_model == null)
                    _model = new register_root_axis();
                _model.CenterRotateAxisX1 = Lib.ToDecimal(txtCenterRotateAxisX1.Text);
                _model.CenterRotateAxisY1 = Lib.ToDecimal(txtCenterRotateAxisY1.Text);
                _model.CenterRotateAxisX2 = Lib.ToDecimal(txtCenterRotateAxisX2.Text);
                _model.CenterRotateAxisY2 = Lib.ToDecimal(txtCenterRotateAxisY2.Text);

                try
                {
                    if (_model.ID > 0)
                        _mySQLHelper.Update<register_root_axis>(_model);
                    else
                        _mySQLHelper.Insert<register_root_axis>(_model);
                    decimal distanceChange = (_model.CenterRotateAxisY1 + _model.CenterRotateAxisY2) / 2 - Lib.ToDecimal(SmartWindow.m_pCulet.Y);
                    SmartWindow.m_pCulet.Y = (float)(_model.CenterRotateAxisY1 + _model.CenterRotateAxisY2) / 2;
                    SmartWindow.m_pLowerGirdle.Y += (float)distanceChange;
                    SmartWindow.m_pTable.Y += (float)distanceChange;

                    DataDispText text = new DataDispText();
                    text.TextView = " ";
                    text.InPoint = new Point(1000, 1500);
                    text.InSize = 150;
                    text.InColor = "Red";
                    text.Key = Guid.NewGuid();
                    SmartWindow.DispText(text);
                    SmartWindow.ClearDispText();
                }
                catch (Exception ex)
                {
                    Lib.saveToErr(ex.ToString());
                }
            }
        }
        #endregion

        #region BUTTON CLICK
        private void btnSaveLineFind_Click(object sender, EventArgs e)
        {
            SaveROIProperties();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // TODO: 041224 - NB
            if (cbMeasurementType_Concentric.SelectedIndex <= 0)
            {
                MessageBox.Show("Xin vui lòng lựa chọn MeasurementType Concentric?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            if (cbMeasurementType_Grinding.SelectedIndex <= 0)
            {
                MessageBox.Show("Xin vui lòng lựa chọn MeasurementType Grinding?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            if (cbMeasurementType_Inspect.SelectedIndex <= 0)
            {
                MessageBox.Show("Xin vui lòng lựa chọn MeasurementType Inspect?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SmartWindow.m_pCulet.X = (float)Lib.ToDecimal(txtCuletX.Text);
                SmartWindow.m_pCulet.Y = (float)Lib.ToDecimal(txtCuletY.Text);
                SmartWindow.m_pTable.X = (float)Lib.ToDecimal(txtTableX.Text);
                SmartWindow.m_pTable.Y = (float)Lib.ToDecimal(txtTableY.Text);
                SmartWindow.m_pLowerGirdle.X = (float)Lib.ToDecimal(txtGirldleX.Text);
                SmartWindow.m_pLowerGirdle.Y = (float)Lib.ToDecimal(txtGirldleY.Text);

                SaveDiamondShape();
            }
        }

        /// <summary>
        /// Thay đổi vị trí cullet hình kim cương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCullet_Click(object sender, EventArgs e)
        {
            SmartWindow.m_pCulet.X = (float)Lib.ToDecimal(txtCulletXView.Text);
            SmartWindow.m_pCulet.Y = (float)(Lib.ToDecimal(txtCulletYView.Text));
            SmartWindow.m_pTable.X = (float)Lib.ToDecimal(txtTableX.Text);
            SmartWindow.m_pTable.Y = (float)Lib.ToDecimal(txtTableY.Text);
            SmartWindow.m_pLowerGirdle.X = (float)Lib.ToDecimal(txtGirldleX.Text);
            SmartWindow.m_pLowerGirdle.Y = (float)Lib.ToDecimal(txtGirldleY.Text);

            SmartWindow_DoubleClick(null, null);
            SmartWindow.m_bCheckMouseAll = true;
            SmartWindow_MouseMove(null, null);
            SmartWindow.m_bCheckMouseAll = false;
            IsDoubleClick = false;
            SmartWindow_MouseUp(null, null);
        }

        /// <summary>
        /// Reset kích thước kim cương và ROI khi kéo nhầm kim cương ngược hướng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetDiamond_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn reset vị trí và kích thước kim cương không không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetDiamondAndROI();
            }
        }

        void ResetDiamondAndROI()
        {
            SmartWindow.m_fTOP = 34;
            SmartWindow.m_fBOT = 41.8;
            SmartWindow.m_fCrow = 13.5;
            SmartWindow.m_fGirdle = 2.2;

            SmartWindow.m_pCulet = new PointF(1778, 2632);
            SmartWindow.m_pTable = new PointF(2912, 3196);
            SmartWindow.m_pLowerGirdle = new PointF((float)2659.05, (float)3572.36);

            DataDispText displayText = new DataDispText();
            displayText.TextView = string.Empty;
            displayText.InPoint = new Point(1000, 1500);
            displayText.InSize = 150;
            displayText.InColor = "Red";
            displayText.Key = Guid.NewGuid();
            SmartWindow.DispText(displayText);
            SmartWindow.ClearDispText();
            diamondSizeOrigin = SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pLowerGirdleUp.Y;

            #region ĐƯỜNG TRÊN / DƯỚI FULL 4 ĐƯỜNG CHÉO KIM CƯƠNG
            // TODO: 230925 - NB
            var roiList2 = ROIS.Values.Where(x => x.ActionID == 11 && x.DescriptionCode != 5).ToList();
            foreach (var roi in roiList2)
            {
                Guid idRoi = roi.ID;
                int _descriptionCode = roi.DescriptionCode;
                switch (_descriptionCode)
                {
                    case 1: // LINE CHÉO KIM CƯƠNG HƯỚNG LÊN TRÊN
                        GraphicsWindow.GraphicsWindow.DataRoi roiSelected7 = SmartWindow.ListDataRoiOutput.Where(x => x.Key == idRoi).FirstOrDefault();
                        if (roiSelected7 != null)
                        {
                            roiSelected7.ShapeHeight = 100;
                            roiSelected7.ShapeWidth = 300;
                            float y = SmartWindow.m_pUpperGirdle.Y - (SmartWindow.m_pUpperGirdle.Y - SmartWindow.m_pCulet.Y) / 4;
                            float x = SmartWindow.m_pUpperGirdle.X - (SmartWindow.m_pUpperGirdle.X - SmartWindow.m_pCulet.X) / 4 - 30;
                            SmartWindow.MoveROI(new PointF(x, y), idRoi);
                            ROIS[idRoi].Center.Row = x;
                            ROIS[idRoi].Center.Col = y;
                            ROIS[idRoi].Height = 100;
                            ROIS[idRoi].Width = 300;
                        }
                        break;
                    case 2: // LINE CHÉO KIM CƯƠNG HƯỚNG LÊN DƯỚI
                        GraphicsWindow.GraphicsWindow.DataRoi roiSelected8 = SmartWindow.ListDataRoiOutput.Where(x => x.Key == idRoi).FirstOrDefault();
                        if (roiSelected8 != null)
                        {
                            roiSelected8.ShapeHeight = 100;
                            roiSelected8.ShapeWidth = 300;
                            float y = SmartWindow.m_pUpperGirldleUp.Y - (SmartWindow.m_pUpperGirldleUp.Y - SmartWindow.m_pCulet.Y) / 4;
                            float x = SmartWindow.m_pUpperGirldleUp.X - (SmartWindow.m_pUpperGirldleUp.X - SmartWindow.m_pCulet.X) / 4 - 30;
                            SmartWindow.MoveROI(new PointF(x, y), idRoi);
                            ROIS[idRoi].Center.Row = x;
                            ROIS[idRoi].Center.Col = y;
                            ROIS[idRoi].Height = 100;
                            ROIS[idRoi].Width = 300;
                        }
                        break;
                    case 3: // LINE BẮT MÉP TRÊN KIM CƯƠNG FULL
                        GraphicsWindow.GraphicsWindow.DataRoi roiSelected5 = SmartWindow.ListDataRoiOutput.Where(x => x.Key == idRoi).FirstOrDefault();
                        if (roiSelected5 != null)
                        {
                            roiSelected5.ShapeHeight = 50;
                            roiSelected5.ShapeWidth = 300;
                            float x = SmartWindow.m_pLowerGirdle.X + (float)ROIS[idRoi].Width - 3;
                            float y = SmartWindow.m_pLowerGirdle.Y - (float)ROIS[idRoi].Height - 3;
                            SmartWindow.MoveROI(new PointF(x, y), idRoi);
                            ROIS[idRoi].Center.Row = x;
                            ROIS[idRoi].Center.Col = y;
                            ROIS[idRoi].Height = 50;
                            ROIS[idRoi].Width = 300;
                        }
                        break;
                    case 4: //LINE BẮT MÉP DƯỚI KIM CƯƠNG FULL
                        GraphicsWindow.GraphicsWindow.DataRoi roiSelected6 = SmartWindow.ListDataRoiOutput.Where(x => x.Key == idRoi).FirstOrDefault();
                        if (roiSelected6 != null)
                        {
                            roiSelected6.ShapeHeight = 50;
                            roiSelected6.ShapeWidth = 300;
                            float x = SmartWindow.m_pLowerGirdleUp.X + (float)ROIS[idRoi].Width + 3;
                            float y = SmartWindow.m_pLowerGirdleUp.Y + (float)ROIS[idRoi].Height + 3;
                            SmartWindow.MoveROI(new PointF(x, y), idRoi);
                            ROIS[idRoi].Center.Row = x;
                            ROIS[idRoi].Center.Col = y;
                            ROIS[idRoi].Height = 50;
                            ROIS[idRoi].Width = 300;
                        }
                        break;
                    default:
                        break;
                }
            }
            #endregion

            SmartWindow.m_bCheckMouseAll = true;
            IsDoubleClick = false;
            SmartWindow_MouseUp(null, null);
            SmartWindow.m_bCheckMouseAll = false;

            SaveDiamondShape();
            SaveROI();

        }
        private void txtPercentToTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự có phải là số hoặc dấu chấm hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }

        private void btnSavePercentLineCheck_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PercentLineCheck = Lib.ToDouble(txtPercentToTable.Text) / 100;
            Properties.Settings.Default.Save();
            SmartWindow.percentLineCheck = Properties.Settings.Default.PercentLineCheck;
        }
        #endregion

        #endregion

        #region ORTHER VISION
        private void tabAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabAction.SelectedIndex == 0)
            //    LoadROILineFind();
            //if (tabAction.SelectedIndex == 2)
            //    LoadROIBlob();
        }

        private object _lockImage = new object();
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;
            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];
            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);
            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }
            // Use our own font.
            Font _tabFont = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Pixel);
            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void chkMove_ROI_CheckedChanged(object sender, EventArgs e)
        {
            SmartWindow.isChangeYPosition = chkMove_ROI.Checked;
        }

        private void FrmSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: 041224 - NB
            isCloseThread = true;
            IsLive = false;
            Thread.Sleep(200);
            SetLiveCamera_Hikrobot(false);
            //DisconnectCamera_Hikrobot();
            this.DialogResult = DialogResult.OK;
        }

        bool IsDoubleClick = false;
        private void SmartWindow_DoubleClick(object sender, EventArgs e)
        {
            SmartWindow.FunctionFitImage();
            SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.2f, 1.2f);
            IsDoubleClick = true;
        }

        private void num_GammaSnap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetGamma_NET((float)num_GammaSnap.Value);
        }

        private void numGainSnap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetGainMode_NET((uint)numGainSnap.Value);
        }

        private void num_ExposureSnap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET((float)num_ExposureSnap.Value);
        }


        private void numGainLive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetGainMode_NET((uint)numGainLive.Value);
        }

        private void num_ExposureLive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET((float)num_ExposureLive.Value);
        }

        private void num_GammaLive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager.HikCamera != null)
                cDeviceManager.HikCamera.MV_CC_SetGamma_NET((float)num_GammaLive.Value);
        }

        #endregion

        #region SETTINGS PLC

        private void LoadInfor_PLC()
        {
            register_device plcDevice = _mySQLHelper.FindByField<register_device>("Type", 2);
            if (plcDevice != null)
            {
                txtPlcIpAddress.Text = plcDevice.IpAddress.ToString();
                txtPlcPort.Text = plcDevice.Port.ToString();
            }
        }

        #region MAPPING BUTTON COLOR
        int tabControl1_Index = 0;
        private void MappingManualPLC()
        {
            while (true)
            {
                Thread.Sleep(200);
                try
                {
                    if (isCloseThread) return;
                    if (tabControl1.InvokeRequired)
                    {
                        tabControl1.Invoke(new Action(() =>
                        {
                            tabControl1_Index = tabControl1.SelectedIndex;
                        }));
                    }
                    else
                        tabControl1_Index = tabControl1.SelectedIndex;
                    if (tabControl1_Index != 2) continue;
                }
                catch (Exception ex)
                {
                }

                #region Manual PLC
                try
                {
                    //R2020
                    SetButtonColorManual("R2021", btnInventorOn);
                    //R2021
                    SetButtonColorManual("R2022", btnInventorOff);
                    //R2022
                    SetButtonColorManual("R2023", btnAirFlowStart_On);
                    //R2023
                    SetButtonColorManual("R2024", btnAirFlowStart_Off);
                    //R2024
                    SetButtonColorManual("R2025", btnAirFlowEnd_On);
                    //R2025
                    SetButtonColorManual("R2026", btnAirFlowEnd_Off);

                    //SetButtonColorManual(cDeviceManager.plcTeaching.InventorOn, btnInventorOn);
                    ////R2021
                    //SetButtonColorManual(cDeviceManager.plcTeaching.InventorOff, btnInventorOff);
                    ////R2022
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowStartOn, btnAirFlowStart_On);
                    ////R2023
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowStartOff, btnAirFlowStart_Off);
                    ////R2024
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowEndOn, btnAirFlowEnd_On);
                    ////R2025
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowEndOff, btnAirFlowEnd_Off);
                }
                catch (Exception ex)
                {
                }
                #endregion

                #region Teachings PLC
                try
                {
                    ////R2100
                    //SetButtonColorManual("R2100", btnSet_JogPlusAxis1);
                    ////R2101
                    //SetButtonColorManual("R2101", btnSet_JogMinusAxis1);
                    ////R2103
                    //SetButtonColorManual("R2103", btnSet_JogPlusAxis2);
                    ////R2104
                    //SetButtonColorManual("R2104", btnSet_JogMinusAxis2);
                    ////R2106
                    //SetButtonColorManual("R2106", btnSet_JogPlusAxis3);
                    ////R2107
                    //SetButtonColorManual("R2107", btnSet_JogMinusAxis3);
                    ////R2109
                    //SetButtonColorManual("R2109", btnSet_JogPlusAxis4);
                    ////R2110
                    //SetButtonColorManual("R21010", btnSet_JogMinusAxis4);

                    //R2112
                    SetButtonColorManual("R2112", btnSet_ManualPositionRun1);
                    //R2113
                    SetButtonColorManual("R2113", btnSet_ManualPositionStop1);
                    //R2116
                    SetButtonColorManual("R2116", btnSet_ManualPositionRun2);
                    //R2117
                    SetButtonColorManual("R2117", btnSet_ManualPositionStop2);
                    //R2120
                    SetButtonColorManual("R2120", btnSet_ManualPositionRun3);
                    //R2121
                    SetButtonColorManual("R2121", btnSet_ManualPositionStop3);
                    //R2124
                    SetButtonColorManual("R2124", btnSet_ManualPositionRun4);
                    //R2125
                    SetButtonColorManual("R2125", btnSet_ManualPositionStop4);

                    //R2128
                    SetButtonColorManual("R2128", btnOriginExis2);
                    //R2129
                    SetButtonColorManual("R2129", btnOriginCompleteExis2);
                    //R2130
                    SetButtonColorManual("R2130", btnOriginExis3);
                    //R2131
                    SetButtonColorManual("R2131", btnOriginCompleteExis3);
                    //R2132
                    SetButtonColorManual("R2132", btnOriginExis4);
                    //R2133
                    SetButtonColorManual("R2133", btnOriginCompleteExis4);
                }
                catch (Exception ex)
                {
                }
                #endregion

                try
                {
                    // TODO: 170325 - NB
                    cDeviceManager._plcMC.GetSingleRegister_Int32("SD5500", out int value);
                    Lib.UpdateTextBoxText(txtJogSpeedAxis1, value.ToString());

                    cDeviceManager._plcMC.GetSingleRegister_Int32("SD5540", out int value1);
                    Lib.UpdateTextBoxText(txtJogSpeedAxis2, value1.ToString());

                    cDeviceManager._plcMC.GetSingleRegister_Int32("SD5580", out int value2);
                    Lib.UpdateTextBoxText(txtJogSpeedAxis3, value2.ToString());

                    cDeviceManager._plcMC.GetSingleRegister_Int32("SD5620", out int value3);
                    Lib.UpdateTextBoxText(txtJogSpeedAxis4, value3.ToString());
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        #region MANUAL DIVECE
        private void btnInventorOn_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.InventorOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnInventorOff_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.InventorOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnAirFlowStart_On_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowStartOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnAirFlowStart_Off_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowStartOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnAirFlowEnd_On_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowEndOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnAirFlowEnd_Off_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowEndOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }
        #endregion

        #region TEACHING SERVO

        #region JOG + / -
        private void btnSet_JogPlusAxis1_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis1, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogPlusAxis1;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogPlusAxis1_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogPlusAxis1;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis1, Color.White, Color.Black);
        }

        private void btnSet_JogMinusAxis1_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis1, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogMinusAxis1;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogMinusAxis1_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogMinusAxis1;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis1, Color.White, Color.Black);
        }

        private void btnSet_JogPlusAxis2_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis2, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogPlusAxis2;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogPlusAxis2_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogPlusAxis2;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis2, Color.White, Color.Black);
        }

        private void btnSet_JogMinusAxis2_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis2, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogMinusAxis2;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogMinusAxis2_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogMinusAxis2;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis2, Color.White, Color.Black);
        }

        private void btnSet_JogPlusAxis3_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis3, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogPlusAxis3;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogPlusAxis3_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogPlusAxis3;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis3, Color.White, Color.Black);
        }

        private void btnSet_JogMinusAxis3_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis3, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogMinusAxis3;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogMinusAxis3_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogMinusAxis3;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis3, Color.White, Color.Black);
        }

        private void btnSet_JogPlusAxis4_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis4, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogPlusAxis4;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogPlusAxis4_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogPlusAxis4;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogPlusAxis4, Color.White, Color.Black);
        }

        private void btnSet_JogMinusAxis4_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis4, Color.Green, Color.White);
            string address = cDeviceManager._teachings_plc.JogMinusAxis4;
            cDeviceManager.SetBitsOn(address);
        }

        private void btnSet_JogMinusAxis4_MouseUp(object sender, MouseEventArgs e)
        {
            // TODO: 170325 - NB
            string address = cDeviceManager._teachings_plc.JogMinusAxis4;
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btnSet_JogMinusAxis4, Color.White, Color.Black);
        }
        #endregion

        #region MANUAL POSITION
        private void btnSet_ManualPositionRun1_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionStop1_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionRun2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionStop2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionRun3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionStop3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionRun4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnSet_ManualPositionStop4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }
        #endregion

        #region GET SPEED
        private void btnJogSpeedAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis1, value.ToString());
        }

        private void btnJogSpeedAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis2, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis2, value.ToString());
        }

        private void btnJogSpeedAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis3, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis3, value.ToString());
        }

        private void btnJogSpeedAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis4, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis4, value.ToString());
        }

        private void btnPositionRunAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis1, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis1, value.ToString());
        }

        private void btnPositionCurrentAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis1, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis1, value.ToString());
        }

        private void btnPositionRunAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis2, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis2, value.ToString());
        }

        private void btnPositionCurrentAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis2, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis2, value.ToString());
        }

        private void btnPositionRunAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis3, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis3, value.ToString());
        }

        private void btnPositionCurrentAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis3, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis3, value.ToString());
        }

        private void btnPositionRunAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis4, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis4, value.ToString());
        }

        private void btnPositionCurrentAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis4, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis4, value.ToString());
        }
        #endregion

        #region SET SPEED
        private void btnJogSpeedAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis1, value);
        }

        private void btnJogSpeedAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis2, value);
        }

        private void btnJogSpeedAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis3, value);
        }

        private void btnJogSpeedAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis4, value);
        }

        private void btnPositionRunAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis1, value);
        }

        private void btnPositionCurrentAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis1, value);
        }

        private void btnPositionRunAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis2, value);
        }

        private void btnPositionCurrentAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis2, value);
        }

        private void btnPositionRunAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis3, value);
        }

        private void btnPositionCurrentAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis3, value);
        }

        private void btnPositionRunAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis4, value);
        }

        private void btnPositionCurrentAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis4, value);
        }
        #endregion

        #endregion

        #region TEACHING AXIS

        #region Axis #1
        private void btnGet_Offsetcheckvision_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetCheckVisionSpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtOffsetcheckvision_Speed_1, value.ToString());
        }

        private void btnSet_Offsetcheckvision_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffsetcheckvision_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetCheckVisionSpeedAxis1, value);
        }

        private void btnGet_RotatePlus_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.Rotate90SpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtRotatePlus_Speed_1, value.ToString());
        }

        private void btnSet_RotatePlus_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtRotatePlus_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.Rotate90SpeedAxis1, value);
        }

        private void btnGet_RotateMinus_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateMinus90SpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtRotateMinus_1, value.ToString());
        }

        private void btnSet_RotateMinus_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtRotateMinus_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateMinus90SpeedAxis1, value);
        }

        private void btnGet_Grinding_Position_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateGrindingPositionAxis1, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_1, value.ToString());
        }

        private void btnSet_Grinding_Position_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateGrindingPositionAxis1, value);
        }

        private void btnGet_Grinding_Speed_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateGrindingSpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_1, value.ToString());
        }

        private void btnSet_Grinding_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.RotateGrindingSpeedAxis1, value);
        }
        #endregion

        #region Exis #2
        private void btnGet_Ready_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadyPositionAxis2, out int value);
            Lib.UpdateTextBoxText(txtReady_Position_2, value.ToString());
        }

        private void btnSet_Ready_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadyPositionAxis2, value);
        }

        private void btnGet_Ready_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadySpeedAxis2, out int value);
            Lib.UpdateTextBoxText(txtReady_Speed_2, value.ToString());
        }

        private void btnSet_Ready_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadySpeedAxis2, value);
        }

        private void btnGet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricPositionAxis2, out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Position_2, value.ToString());
        }

        private void btnSet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricPositionAxis2, value);
        }

        private void btnGet_Concentrick_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricSpeedAxis2, out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Speed_2, value.ToString());
        }

        private void btnSet_Concentrick_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricSpeedAxis2, value);
        }

        private void btnGet_Concentrick_Adjustment_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ConcentricAdjustmentPositionAxis2, out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Adjustment_Position_2, value.ToString());
        }

        private void btnSet_Concentrick_Adjustment_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Adjustment_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ConcentricAdjustmentPositionAxis2, value);
        }

        private void btnGet_Concentrick_Adjustment_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ConcentricAdjustmentSpeedAxis2, out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Adjustment_Speed_2, value.ToString());
        }

        private void btnSet_Concentrick_Adjustment_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Adjustment_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ConcentricAdjustmentSpeedAxis2, value);
        }
        #endregion

        #region Axis #3
        private void btnGet_Ready_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadyPositionAxis3, out int value);
            Lib.UpdateTextBoxText(txtReady_Position_3, value.ToString());
        }

        private void btnSet_Ready_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadyPositionAxis3, value);
        }

        private void btnGet_Ready_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadySpeedAxis3, out int value);
            Lib.UpdateTextBoxText(txtReady_Speed_3, value.ToString());
        }

        private void btnSet_Ready_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ReadySpeedAxis3, value);
        }

        private void btnGet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingPositionAxis3, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_3, value.ToString());
        }

        private void btnSet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingPositionAxis3, value);
        }

        private void btnGet_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingSpeedAxis3, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_3, value.ToString());
        }

        private void btnSet_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingSpeedAxis3, value);
        }

        private void btnGet_Offset_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetGrindingPositionAxis3, out int value);
            Lib.UpdateTextBoxText(txtOffset_Grinding_Position_3, value.ToString());
        }

        private void btnSet_Offset_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffset_Grinding_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetGrindingPositionAxis3, value);
        }

        private void btnGet_Offset_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetGrindingSpeedAxis3, out int value);
            Lib.UpdateTextBoxText(txtOffset_Grinding_Speed_3, value.ToString());
        }

        private void btnSet_Offset_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffset_Grinding_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.OffsetGrindingSpeedAxis3, value);
        }
        #endregion

        #region Axis #4
        private void btnGet_Grinding_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32(cDeviceManager._teachings_plc.GrindingPositionAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_4, value.ToString());
        }

        private void btnSet_Grinding_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32(cDeviceManager._teachings_plc.GrindingPositionAxis4, value);
        }

        private void btnGet_Grinding_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.GrindingSpeedAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.GrindingSpeedAxis4, value);
        }

        private void btnGet_Grinding_R_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32(cDeviceManager._teachings_plc.ManualGrindingRightPositionAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_R_Position_4, value.ToString());
        }

        private void btnSet_Grinding_R_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_R_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32(cDeviceManager._teachings_plc.ManualGrindingRightPositionAxis4, value);

            // TODO: 311025 - NB  
            _config_data.Grinding_R_Position = txtGrinding_R_Position_4.Text.Trim();
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        private void btnGet_Grinding_R_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ManualGrindingRightSpeedAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_R_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_R_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_R_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ManualGrindingRightSpeedAxis4, value);
        }

        private void btnGet_Grinding_L_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32(cDeviceManager._teachings_plc.ManualGrindingLeftPositionAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_L_Position_4, value.ToString());
        }

        private void btnSet_Grinding_L_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_L_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32(cDeviceManager._teachings_plc.ManualGrindingLeftPositionAxis4, value);

            // TODO: 311025 - NB  
            _config_data.Grinding_L_Position = txtGrinding_L_Position_4.Text.Trim();
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        private void btnGet_Grinding_L_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.ManualGrindingLeftSpeedAxis4, out int value);
            Lib.UpdateTextBoxText(txtGrinding_L_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_L_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_L_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.ManualGrindingLeftSpeedAxis4, value);
        }
        #endregion

        #endregion

        #region ORIGIN
        private void btnOriginExis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnOriginCompleteExis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnOriginExis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnOriginCompleteExis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnOriginExis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }

        private void btnOriginCompleteExis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 500);
        }
        #endregion

        #region OTHER PLC
        private async void SetButtonColorManual(string address, Button btn)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    int valueCheck = cDeviceManager._plcMC.GetSingleRegister_Int16(address, out int value);
                    switch (value)
                    {
                        case 0:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Green, Color.White);
                            break;
                        case 1:
                            Lib.UpdateButton_BColor_FColor(btn, Color.FromArgb(251, 196, 191), Color.Black);
                            break;
                        case 2:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Lime, Color.Black);
                            break;
                        case 3:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Gray, Color.White);
                            break;
                        case 4:
                            Lib.UpdateButton_BColor_FColor(btn, Color.FromArgb(252, 242, 131), Color.Black);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            });
            await task;
        }

        private async void ResetOffPLC_ButtonClick_VisionTools(Button btn, string address, int delay)
        {
            await Task.Delay(delay);
            cDeviceManager.SetBitsOff(address);
            Lib.UpdateButton_BColor_FColor(btn, Color.White, Color.Black);
        }

        private async void ResetOffPLC_ButtonClick(string address, int delay)
        {
            await Task.Delay(delay);
            cDeviceManager.SetBitsOff(address);
        }

        private void btnRegister_Teaching_Click(object sender, EventArgs e)
        {
            FrmRegisterTeachingPLC frm = new FrmRegisterTeachingPLC();
            frm.ShowDialog();
        }

        private void btnRegisterError_Click(object sender, EventArgs e)
        {
            FrmRegisterPLCAddress frm = new FrmRegisterPLCAddress();
            frm.ShowDialog();
        }

        private void btnLiveCamera_Click(object sender, EventArgs e)
        {
            IsLive = true;
            Thread.Sleep(200);
            if (!SetLiveCamera_Hikrobot(IsLive))
            {
                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                return;
            }
            Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);
            cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(100);
            cDeviceManager.HikCamera.MV_CC_SetGain_NET(6);

            // TODO: 081224
            //cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();

            btnLiveCamera.Appearance.BackColor = Color.RoyalBlue;
            btnLiveCamera.Appearance.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
            btnLiveCamera.Appearance.ForeColor = Color.White; // Chữ trắng

            btnSnapCamera.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
            btnSnapCamera.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
            btnSnapCamera.Appearance.Options.UseBackColor = false;
            btnSnapCamera.Appearance.Options.UseForeColor = false;
        }

        private void btnSnapCamera_Click(object sender, EventArgs e)
        {
            btnSnapCamera.Appearance.BackColor = Color.RoyalBlue;
            btnSnapCamera.Appearance.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
            btnSnapCamera.Appearance.ForeColor = Color.White; // Chữ trắng

            if (!SetLiveCamera_Hikrobot(IsLive))
            {
                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                return;
            }
            Thread.Sleep(100);
            IsLive = false;
            Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);
            cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(100);
            cDeviceManager.HikCamera.MV_CC_SetGain_NET(6);

            // TODO: 081224
            cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();

            // TODO: 271124
            bitmap_Camera = cDeviceManager.HikCamera.SingleShotGrabbing();
            if (bitmap_Camera == null)
            {
                Thread.Sleep(200);
                bitmap_Camera = cDeviceManager.HikCamera.SingleShotGrabbing();
                if (bitmap_Camera == null)
                {
                    MessageBox.Show("Xin vui lòng Snap image thêm 1 lần nữa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
            }
            if (bitmap_Camera != null)
            {
                if (SmartWindow.InvokeRequired)
                {
                    SmartWindow.Invoke(new Action(() =>
                    {
                        SmartWindow.Image = bitmap_Camera;
                    }));
                }
                else
                    SmartWindow.Image = bitmap_Camera;
            }

            if (smartWindowImg != null)
                SmartWindow.Image = smartWindowImg;

            InputImage = SmartWindow.Image;
            GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)InputImage.Clone());
            DisplayImage = GrayImage.Convert<Bgr, byte>();
            lock (_lockImage)
            {
                SmartWindow_DoubleClick(null, null);
            }

            btnRunLineFind_VisionTools.Enabled = btnDistanceMeasure.Enabled = true;
            btnLiveCamera.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
            btnLiveCamera.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
            btnLiveCamera.Appearance.Options.UseBackColor = false;
            btnLiveCamera.Appearance.Options.UseForeColor = false;
        }

        private void btnSaveDeviceInfor_Click(object sender, EventArgs e)
        {
            register_device register_Device = _mySQLHelper.FindAll<register_device>().FirstOrDefault();
            register_Device.IpAddress = txtPlcIpAddress.Text.Trim();
            register_Device.Port = Lib.ToInt(txtPlcPort.Text.Trim());
            _mySQLHelper.Update<register_device>(register_Device);
        }

        private void btnRotate_Speed_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1420", out int value);
            Lib.UpdateTextBoxText(txtRotate_Speed, value.ToString());
        }

        private void btnRotate_Speed_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtRotate_Speed.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1420", value);
        }

        private void btnDiamondSpin_ON_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_ON, Color.LimeGreen, Color.White);
            string address = "D7005.1";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick_VisionTools(btnDiamondSpin_ON, address, 500);
        }

        private void btnDiamondSpin_OFF_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_OFF, Color.LimeGreen, Color.White);
            string address = "D7005.5";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick_VisionTools(btnDiamondSpin_OFF, address, 500);
        }
        private void btnRotaryGrinding_Setup_ON_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_ON, Color.LimeGreen, Color.White);
            string address = "R500.0";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick_VisionTools(btnRotaryGrinding_Setup_ON, address, 500);
        }

        private void btnRotaryGrinding_Setup_OFF_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_OFF, Color.LimeGreen, Color.White);
            string address = "R500.1";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick_VisionTools(btnRotaryGrinding_Setup_OFF, address, 500);
        }

        private void btnRotaryGrinding_JogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_JogPlus, Color.LimeGreen, Color.White);
            cDeviceManager.SetBitsOn("M1260");
        }

        private void btnRotaryGrinding_JogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            cDeviceManager.SetBitsOff("M1260");
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_JogPlus, Color.White, Color.Black);
        }

        private void btnRotaryGrinding_JogPlusMinus_MouseDown(object sender, MouseEventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_JogPlusMinus, Color.LimeGreen, Color.White);
            cDeviceManager.SetBitsOn("M1261");
        }
        private void btnRotaryGrinding_JogPlusMinus_MouseUp(object sender, MouseEventArgs e)
        {
            cDeviceManager.SetBitsOff("M1261");
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_JogPlusMinus, Color.White, Color.Black);
        }

        private void btnRotaryGrinding_Setup_UP_MouseDown(object sender, MouseEventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_UP, Color.LimeGreen, Color.White);
            cDeviceManager.SetBitsOn("M1240");
        }

        private void btnRotaryGrinding_Setup_UP_MouseUp(object sender, MouseEventArgs e)
        {
            cDeviceManager.SetBitsOff("M1240");
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_UP, Color.White, Color.Black);
        }

        private void btnRotaryGrinding_Setup_DOWN_MouseDown(object sender, MouseEventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_DOWN, Color.LimeGreen, Color.White);
            cDeviceManager.SetBitsOn("M1241");
        }

        private void btnRotaryGrinding_Setup_DOWN_MouseUp(object sender, MouseEventArgs e)
        {
            cDeviceManager.SetBitsOff("M1241");
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_DOWN, Color.White, Color.Black);
        }

        private void btnStart_Auto_Grinding_Position_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1830", out int value);
            Lib.UpdateTextBoxText(txtStart_Auto_Grinding_Position, value.ToString());
        }

        private void btnStart_Auto_Grinding_Position_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtStart_Auto_Grinding_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1830", value);
        }

        private void btnStart_Auto_Grinding_Speed_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1832", out int value);
            Lib.UpdateTextBoxText(txtStart_Auto_Grinding_Speed, value.ToString());
        }

        private void btnStart_Auto_Grinding_Speed_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtStart_Auto_Grinding_Speed.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1832", value);
        }

        private void btnAuto_Grinding_L_Position_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1836", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_L_Position, value.ToString());
        }

        private void btnAuto_Grinding_L_Position_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtAuto_Grinding_L_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1836", value);
        }

        private void btnAuto_Grinding_L_Speed_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1838", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_L_Speed, value.ToString());
        }

        private void btnAuto_Grinding_L_Speed_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtAuto_Grinding_L_Speed.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1838", value);
        }

        private void btnAuto_Grinding_R_Position_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1842", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_R_Position, value.ToString());
        }

        private void btnAuto_Grinding_R_Position_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtAuto_Grinding_R_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1842", value);
        }

        private void btnAuto_Grinding_R_Speed_Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1844", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_R_Speed, value.ToString());
        }

        private void btnAuto_Grinding_R_Speed_Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtAuto_Grinding_R_Speed.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1844", value);
        }
        private void btnRotaryGrinding_Setup_Reset_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_Reset, Color.LimeGreen, Color.White);
            string address = "M404";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick_VisionTools(btnRotaryGrinding_Setup_Reset, address, 1000);
        }




        #endregion

        #endregion

        #region SETTING CONVER PIXEL TO MM

        /// <summary>
        /// Lưu các ROI phần Vision tính pixel to MM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// // TODO: Huy - update 11/12/2024
        private void btnSaveCircleROI_Click(object sender, EventArgs e)
        {
            foreach (cRectangType ROI in ROIS.Values)
            {
                if (ROI.ActionID != 10) continue;
                register_location_rois roi = _mySQLHelper.FindByField<register_location_rois>("IDGUID", ROI.ID);
                roi.CenterX = Lib.ToInt(ROI.Center.Row);
                roi.CenterY = Lib.ToInt(ROI.Center.Col);
                roi.Phi = Lib.ToInt(ROI.Phi);
                roi.Width = Lib.ToInt(ROI.Width);
                roi.Height = Lib.ToInt(ROI.Height);
                try
                {
                    if (roi.IDGuid != null && roi.IDGuid != "0")
                        _mySQLHelper.Update<register_location_rois>(roi);
                    else
                    {
                        roi.IDGuid = ROI.ID.ToString();
                        roi.IDAction = ROI.ActionID;
                        _mySQLHelper.Insert<register_location_rois>(roi);
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }
        private void CircleWindow_MouseUp(object sender, MouseEventArgs e)
        {
            // Lấy ra ROI đang được chọn
            CurrentDrawingsCircle = DrawingObjectsCircle.FirstOrDefault(x => x.Key == CircleWindow.KeySelect).Value;
            cRectangType ROISelected;
            if (CurrentDrawingsCircle != null)
            {
                ROISelected = ((cRectangType)ROIS[CircleWindow.KeySelect]);
                var DataRoiSelected = CircleWindow.ListDataRoiOutput.Find(X => X.Key == CircleWindow.KeySelect);
                ROISelected.Height = DataRoiSelected.ShapeHeight;
                ROISelected.Width = DataRoiSelected.ShapeWidth;
                ROISelected.Phi = DataRoiSelected.Angle;
                ROISelected.Center.Row = DataRoiSelected.CenterPointX;
                ROISelected.Center.Col = DataRoiSelected.CenterPointY;
                ROISelected.ID = DataRoiSelected.Key;
                ROISelected.Color = DataRoiSelected.ColorRoi;
                ROIS[(CircleWindow.KeySelect)] = ROISelected;
                DrawingObjectsCircle[CircleWindow.KeySelect] = CircleWindow.ListDataRoiOutput.Find(X => X.Key == CircleWindow.KeySelect);
            }

            if (ROIProperties.ContainsKey(CircleWindow.KeySelect))
                SwitchRoiCircle();
        }

        private void btnSaveCircleROI_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thông Setting chuyển đổi Pixel to MM không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (cRectangType ROI in ROIS.Values)
                {
                    if (ROI.ActionID != 10) continue;
                    register_location_rois roi = _mySQLHelper.FindByField<register_location_rois>("IDAction", 10);
                    roi.CenterX = Lib.ToInt(ROI.Center.Row);
                    roi.CenterY = Lib.ToInt(ROI.Center.Col);
                    roi.Phi = Lib.ToInt(ROI.Phi);
                    roi.Width = Lib.ToInt(ROI.Width);
                    roi.Height = Lib.ToInt(ROI.Height);
                    try
                    {
                        if (roi.IDGuid != null && roi.IDGuid != "0")
                            _mySQLHelper.Update<register_location_rois>(roi);
                        else
                        {
                            roi.IDGuid = ROI.ID.ToString();
                            roi.IDAction = ROI.ActionID;
                            _mySQLHelper.Insert<register_location_rois>(roi);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        // TODO: NB - 111224
        Blob_Muilti_ROI.BlobTool Blobs = new Blob_Muilti_ROI.BlobTool();
        private void btnCalib_Click(object sender, EventArgs e)
        {
            SetOrigin.SetOrigin BlobROI = new SetOrigin.SetOrigin();
            List<RTCRectangle> listROI = new List<RTCRectangle>();
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), (double)0);

            var DataROI = CircleWindow.GetDataRoi();
            RTCRectangle ROIS = new RTCRectangle();
            ROIS.Center = new RTCPoint(DataROI.Item2, DataROI.Item3, 0);
            ROIS.Phi = (double)DataROI.Item6 * Math.PI / 180;
            ROIS.Width = (double)DataROI.Item4 * 2;
            ROIS.Height = (double)DataROI.Item5 * 2;
            listROI.Add(ROIS);

            Blobs.ROI = listROI;
            var InSetOrigin = BlobROI.Run(listROI, toolOrigin);
            Blobs.ToolOrigin = toolOrigin;
            Blobs.InSetOrigin = InSetOrigin;
            Blobs.InputImage = inputImage.Clone();
            Blobs.DetectType = cBlobTool.DectectType_DetectInRangeBlobs;
            Blobs.FillHoles = true;
            Blobs.GreyLevelThresholdType = cBlobTool.GreyLevelThresholdType_FixedThresholdRange;
            Blobs.ThresholdRange = Tuple.Create(int.Parse(txbThresMin.Text), int.Parse(txbThresMax.Text));
            Blobs.RequireNumberOfBlobs = Tuple.Create(0, 100);
            Blobs.EnableAreaFilter = true;
            Blobs.AreaRange = Tuple.Create(double.Parse(txbAreaMin.Text), double.Parse(txbAreaMax.Text));
            Blobs.IsShowImageResult = true;
            var result = Blobs.Run();
            int Height = Blobs.OutputHeightList.Max();
            double scale = double.Parse(txbActualSize.Text) / Height;
            textBox2.Text = Height.ToString();
            txbScale.Text = scale.ToString();
            textBox1.Text = ((scale * 1000) / 2).ToString();
            CircleWindow.Image = Blobs.OutputImage;
        }


        Image<Gray, byte> inputImage;
        // TODO: NB - 111224
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở ảnh
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inputImage = new Image<Gray, byte>(openFileDialog.FileName);
                    CircleWindow.Image = inputImage.ToBitmap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SwitchRoiCircle(bool onlyGetValue = false)
        {
            try
            {
                if (CurrentDrawingsCircle == null) return;
                var roiProperties = ROIProperties.Values.Where(x => x.Selected);
                cROIProperty crp = null;
                // chuyển ROI được chọn trước đó sang Selected = false
                if (roiProperties != null && roiProperties.Any())
                {
                    crp = (cROIProperty)roiProperties.First();
                    crp.Selected = false;
                    ROIProperties[crp.ID] = crp;
                }

                if (onlyGetValue) return;
                // Chuyển ROI được chọn hiện tại Selected = false;
                if (CurrentDrawingsCircle != null && ROIProperties.ContainsKey(CurrentDrawingsCircle.Key))
                {
                    crp = ROIProperties[CurrentDrawingsCircle.Key];
                    crp.Selected = true;
                    ROIProperties[crp.ID] = crp;
                    // Hiển thị dữ liệu  thuộc tính của ROI này lên các control
                }

            }
            catch (Exception ex)
            {
                //Lib.saveToLog(ex.Message);
            }
        }
        #endregion

        #region TÍNH TOÁN TÌM CÁC ĐƯỜNG GIAO NHAU
        List<double> Line11 = new List<double>() { 0, 0, 0, 0 }; // ĐƯỜNG CHÉO KIM CƯƠNG HƯỚNG LÊN TRÊN
        List<double> Line12 = new List<double>() { 0, 0, 0, 0 }; // ĐƯỜNG CHÉO KIM CƯƠNG HƯỚNG LÊN DƯỚI
        List<double> Line11_Intersect = new List<double>() { 0, 0, 0, 0 }; // GIAO NHAU
        List<double> Line12_Intersect = new List<double>() { 0, 0, 0, 0 }; // GIAO NHAU

        List<double> Line13 = new List<double>() { 0, 0, 0, 0 }; // ĐƯỜNG BẮT MÉP TRÊN KIM CƯƠNG FULL
        List<double> Line14 = new List<double>() { 0, 0, 0, 0 }; // ĐƯỜNG BẮT MÉP DƯỚI KIM CƯƠNG FULL
        private void LineFindRun_Crow_TotalDept()
        {
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);

            #region Cần comment lại?
            List<RTCRectangle> ROIs = new List<RTCRectangle>();
            int count = 0;
            RTCRectangle Data = new RTCRectangle();
            foreach (object ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 11))
            {
                LineFind = new LineFind();
                cDrawingBaseType ROIB = (cDrawingBaseType)ROI;
                Guid OldID = ROIB.ID;
                switch (ROIB.DrawingType)
                {
                    case EDrawingtypes.Rectang:
                        {
                            cRectangType Rec = (cRectangType)ROI;
                            Data = new RTCRectangle();
                            Data.Phi = (float)Rec.Phi * Math.PI / 180;
                            Data.Width = (float)Rec.Width * 2;
                            Data.Height = (float)Rec.Height * 2;
                            Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                            ROIs.Add(Data);
                            break;
                        }
                }
                LineFind.ROI = Data;
                LineFind.ToolOrigin = toolOrigin;
                LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                LineFind.InputImage = GrayImage;
                LineFind.EdgeTransition = ROIProperties[OldID].EdgeTransition;
                LineFind.EdgeType = ROIProperties[OldID].EdgeType;
                LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties[OldID].EdgeThreshold);
                LineFind.SamplingPercent = ROIProperties[OldID].SamplingPercent;
                LineFind.OutlierDistanceThreshold = 1;
                LineFind.StepFindPoint = 1;
                LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties[OldID].MaxPercentageOfOutliers);
                LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties[OldID].MinEdgePointNumber);
                LineFind.IsShowImageResult = true;

                bool result = LineFind.Run();
                // Giá trị output tool
                string err = LineFind.ErrMessage;
                CircleWindow.ClearAllRoi();
                // Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 0)
                {
                    Line11 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line11, DisplayImage, Color.DarkBlue);
                }
                else if (LineFind.LineSegment != null && count == 1)
                {
                    Line12 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line12, DisplayImage, Color.DarkBlue);
                }
                // Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 2)
                {
                    Line13 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line13, DisplayImage, Color.Red);
                }
                else if (LineFind.LineSegment != null && count == 3)
                {
                    Line14 = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    DisplayGraphics(SmartWindow, Line14, DisplayImage, Color.Red);
                }
                count++;
            }
            #endregion
        }

        public PointF FindIntersection((double X, double Y) line1Start, (double X, double Y) line1End, (double X, double Y) line2Start, (double X, double Y) line2End)
        {
            PointF OutInter = new PointF();
            double x1 = line1Start.X, y1 = line1Start.Y;
            double x2 = line1End.X, y2 = line1End.Y;
            double x3 = line2Start.X, y3 = line2Start.Y;
            double x4 = line2End.X, y4 = line2End.Y;
            double denom = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (denom == 0) return OutInter;
            double t1 = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denom;
            double t2 = ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) / denom;
            double px = x1 + t1 * (x2 - x1);
            double py = y1 + t1 * (y2 - y1);
            return OutInter = new PointF((float)(px), (float)(py));
        }
        #endregion

        #region CÀI ĐẶT OFFSET ĐÁ MÀI
        private void btnGetOffsetStone_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath;

                string fileName = path + "\\" + "OffsetGrindingStone" + ".txt";
                string offsetValue = txtOffsetGrindingStone.Text;
                if (!File.Exists(fileName))
                    return;
                else
                {
                    string[] result = File.ReadAllLines(fileName);
                    txtOffsetGrindingStone.Text = result[0];
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {

            SmartWindow.m_pCulet.Y -= 1;
            SmartWindow.m_pLowerGirdle.Y -= 1;
            SmartWindow.m_pTable.Y -= 1;



            DataDispText displayText = new DataDispText();
            displayText.TextView = " ";
            displayText.InPoint = new Point(1000, 1500);
            displayText.InSize = 150;
            displayText.InColor = "Red";
            displayText.Key = Guid.NewGuid();
            SmartWindow.DispText(displayText);
            SmartWindow.ClearDispText();
            Lib.UpdateTextBoxText(txtCulletYView, SmartWindow.m_pCulet.Y.ToString("F1"));

            // SmartWindow_MouseUp(null, null);
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            SmartWindow.m_pCulet.Y += 1;
            SmartWindow.m_pLowerGirdle.Y += 1;
            SmartWindow.m_pTable.Y += 1;

            DataDispText displayText = new DataDispText();
            displayText.TextView = " ";
            displayText.InPoint = new Point(1000, 1500);
            displayText.InSize = 150;
            displayText.InColor = "Red";
            displayText.Key = Guid.NewGuid();
            SmartWindow.DispText(displayText);
            SmartWindow.ClearDispText();
            Lib.UpdateTextBoxText(txtCulletYView, SmartWindow.m_pCulet.Y.ToString("F1"));
        }

        private void btnGet_PumpRunningTime_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D3225", out int value);
            Lib.UpdateTextBoxText(txtPumpRunningTime, value.ToString());
        }

        private void btnSet_PumpRunningTime_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPumpRunningTime.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D3225", value);
        }

        private void btnGet_TGian_ChoPhunNuoc_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D3230", out int value);
            Lib.UpdateTextBoxText(txtTGian_ChoPhunNuoc, value.ToString());
        }

        private void btnSet_TGian_ChoPhunNuoc_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtTGian_ChoPhunNuoc.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D3230", value);
        }


        private void btnSetOffsetStone_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath;

                string fileName = path + "\\" + "OffsetGrindingStone" + ".txt";
                string offsetValue = txtOffsetGrindingStone.Text;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, offsetValue);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, offsetValue);
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region KHỞI TẠO VÀ LOAD CONFIG .INI
        // TODO: 010726 - NB
        public class Config_Rois_Diamond
        {
            public Device TAB_DIAMOND { get; set; }
            public Device BOT_DIAMOND { get; set; }
            public Device DIAMETER_DIAMOND { get; set; }
        }

        public class Device
        {
            public string Height { get; set; }
            public string Width { get; set; }
            public string Ratio { get; set; }
        }

        private string ReadIni(string section, string key, string defaultValue = "")
        {
            StringBuilder sb = new StringBuilder(255);
            string path = Path.Combine(Application.StartupPath, "SettingsRois_Diamond.ini");
            AccessIni.GetPrivateProfileString(section, key, defaultValue, sb, 255, path);
            return sb.ToString().Trim();
        }
        private void WriteIni(string section, string key, string value)
        {
            string path = Path.Combine(Path.Combine(Application.StartupPath, "SettingsRois_Diamond.ini"));
            AccessIni.WritePrivateProfileString(section, key, value, path);
        }

        private void WriteInis(string section, Dictionary<string, string> data)
        {
            string path = Path.Combine(Application.StartupPath, "SettingsRois_Diamond.ini");

            foreach (var item in data)
            {
                AccessIni.WritePrivateProfileString(section, item.Key, item.Value, path);
            }
        }

        private void LoadConfig_Rois()
        {
            _config_Rois_Diamond = Get_Config_Rois_Diamond();

            txtHeight_R.Text = _config_Rois_Diamond.BOT_DIAMOND.Height;
            txtWidth_R.Text = _config_Rois_Diamond.BOT_DIAMOND.Width;

            txtRatio_L.Text = _config_Rois_Diamond.TAB_DIAMOND.Ratio;
            txtHeight_L.Text = _config_Rois_Diamond.TAB_DIAMOND.Height;
            txtWidth_L.Text = _config_Rois_Diamond.TAB_DIAMOND.Width;

            txtHeight_T_B.Text = _config_Rois_Diamond.DIAMETER_DIAMOND.Height;
            txtWidth_T_B.Text = _config_Rois_Diamond.DIAMETER_DIAMOND.Width;
        }

        private void btnSave_SettingsRois_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu các thông tin vừa setup không ?".ToUpper(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WriteInis("TAB", new Dictionary<string, string>
                {
                    { "HEIGHT", txtHeight_R.Text.Trim() },
                    { "WIDTH", txtWidth_R.Text.Trim()  }
                });

                WriteInis("BOT", new Dictionary<string, string>
                {
                    { "RATIO", txtRatio_L.Text.Trim() },
                    { "HEIGHT", txtHeight_L.Text.Trim() },
                    { "WIDTH", txtWidth_L.Text.Trim()  }
                });

                WriteInis("DIAMETER", new Dictionary<string, string>
                {
                    { "HEIGHT", txtHeight_T_B.Text.Trim() },
                    { "WIDTH", txtWidth_T_B.Text.Trim()  }
                });

                MessageBox.Show("Thành công! Xin vui lòng di chuyển kim cương để thấy sự thay đổi!".ToUpper(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);

                _config_Rois_Diamond = Get_Config_Rois_Diamond();
            }
        }

        private Config_Rois_Diamond Get_Config_Rois_Diamond()
        {
            return new Config_Rois_Diamond
            {
                BOT_DIAMOND = new Device
                {
                    Height = ReadIni("TAB", "HEIGHT"),
                    Width = ReadIni("TAB", "WIDTH")
                },

                TAB_DIAMOND = new Device
                {
                    Ratio = ReadIni("BOT", "RATIO"),
                    Height = ReadIni("BOT", "HEIGHT"),
                    Width = ReadIni("BOT", "WIDTH")
                },

                DIAMETER_DIAMOND = new Device
                {
                    Height = ReadIni("DIAMETER", "HEIGHT"),
                    Width = ReadIni("DIAMETER", "WIDTH")
                }
            };
        }
        #endregion
    }
}
