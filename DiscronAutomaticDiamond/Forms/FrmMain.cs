using AccessIni_Scada;
using AutoBrutingDiamond.Class;
using AutoBrutingDiamond.Models;
using DevExpress.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using LineFindTool;
using MySQLLib;
using RTCBase.Drawing;
using RTCConst;
using RTCHikSdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GraphicsWindow.GraphicsWindow;

namespace AutoBrutingDiamond
{
    public partial class FrmMain : Form
    {
        #region KHAI BÁO VISION
        bool IsRun = false;
        bool IsLive = false;
        bool isSettingCameraLive = false;
        private BackgroundWorker liveBackgroundWorker = null;
        public bool IsTriggerOn { get; set; }
        public string DeviceName { get; set; }

        //point 1-2 dùng để tính độ lệch (đo khoảng cách từ tâm trục xoay đến tâm đường kính kim cương)
        PointF point1 = new PointF(0, 0); // Điểm tâm đường kính kim cương
        PointF point2 = new PointF(0, 0); // Điểm tâm trục xoay kim cương

        //point 3-4 dùng để tính độ đảo (đo khoảng cách từ tâm trục xoay đến mép trên kim cương)
        PointF point3 = new PointF(0, 0);
        PointF point4 = new PointF(0, 0);

        //point 5 là điểm nằm trên đường đỉnh kim cương
        PointF point5 = new PointF(0, 0);

        //point6 là điểm nằm trên tâm trục bên trái
        PointF point6 = new PointF(0, 0);

        double PIXEL_TO_MM = 1.6501650165016502;

        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects_Circle = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
        // TODO: 141224
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects_Crow_TotalDept = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects_LowestPoint_Diamond = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
        private object _lockImage = new object();

        // TODO: 181224
        List<double> Line_Diamond_Top = new List<double>() { 0, 0, 0, 0 };//Line mép trên kim cương
        List<double> Line_Diamond_Bottom = new List<double>() { 0, 0, 0, 0 };//Line mép dưới kim cương
        List<double> Line_Diamond_Center = new List<double>() { 0, 0, 0, 0 };//Line đường kính kính kim cương

        List<double> Line_Shaft_Right_Top = new List<double>() { 0, 0, 0, 0 };//Line mép trục xoay bên phải
        List<double> Line_Shaft_Right_Bottom = new List<double>() { 0, 0, 0, 0 };//Line mép trục xoay bên phải
        List<double> Line_Shaft_Right_Center = new List<double>() { 0, 0, 0, 0 };//Line tâm trục xoay bên phải

        List<double> Line_Shaft_Left_Top = new List<double>() { 0, 0, 0, 0 }; // Line mép trục bên trái
        List<double> Line_Shaft_Left_Bottom = new List<double>() { 0, 0, 0, 0 }; // Line mép trục bên trái
        List<double> Line_Shaft_Left_Center = new List<double>() { 0, 0, 0, 0 }; // Line tâm trục bên trái

        List<double> Line_Cross_Diamond_Up = new List<double>() { 0, 0, 0, 0 }; // LINE CHÉO KIM CƯƠNG HƯỚNG LÊN TRÊN
        List<double> Line_Cross_Diamond_Down = new List<double>() { 0, 0, 0, 0 }; // LINE CHÉO KIM CƯƠNG HƯỚNG LÊN DƯỚI
        List<double> Line_Cross_Diamond_Intersect_1 = new List<double>() { 0, 0, 0, 0 }; // GIAO NHAU
        List<double> Line_Cross_Diamond_Intersect_2 = new List<double>() { 0, 0, 0, 0 }; // GIAO NHAU

        List<double> Line_Diamond_Top_Full = new List<double>() { 0, 0, 0, 0 }; // LINE BẮT MÉP TRÊN KIM CƯƠNG FULL 
        List<double> Line_Diamond_Bottom_Full = new List<double>() { 0, 0, 0, 0 }; // LINE BẮT MÉP DƯỚI KIM CƯƠNG FULL

        // edge on grinding wheel
        List<double> Line_Diamond_Bottom_Lengthen = new List<double>() { 0, 0, 0, 0 }; // Line kéo dài từ mép dưới kim cương
        List<double> Line_GrindStone_Top = new List<double>() { 0, 0, 0, 0 }; // Line bắt mép trên đá mài
        List<double> Line_Diamond_Lowest = new List<double>() { 0, 0, 0, 0 }; // Line bắt điểm thấp nhất của kim cương
        List<double> Line_Shaft_Bottom_Lengthen = new List<double>() { 0, 0, 0, 0 }; // Line kéo dài từ mép dưới kim cương
        #endregion

        #region KHAI BÁO PHẦN MỀM
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        Thread _reconnectPLC_MC;
        Thread _findMin;
        Thread _process_ReadDataPLC;
        Thread _mapIO, _checkAlive, _checkAlarm_MainProcess, _checkTriggerConcentric;
        Thread _functionMeasure, _functionMeasureManual, _runFunctionInspect;

        PlcAddress _plcAddress = new PlcAddress();

        public class PlcAddress
        {
            public string BarlightAddress;
            public string ButtonSwitchAddress;
            public string Trigger1;
            public string Trigger2;
            public string Trigger3;
            public string Trigger4;
        }
        #endregion

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"MACHINE AUTO BRUTING VER [{File.GetLastWriteTime(Assembly.GetEntryAssembly().Location).ToString("dd/MM/yyyy HH:mm:ss")}]";
            Lib.UpdateButtonText(btnRunMode_Manual, "[MANUAL]\rMÀI TỰ ĐỘNG");

            IsShowROIs_FirtDiamond = true;
            chkZoom.Checked = true;

            btnRunMode_Auto_Click(null, null);
            DeviceListAcq();
            SmartWindow.FitImage = true;
            if (Properties.Settings.Default.PercentLineCheck == null)
            {
                Properties.Settings.Default.PercentLineCheck = 0.165;
                Properties.Settings.Default.Save();
            }
            SmartWindow.percentLineCheck = Properties.Settings.Default.PercentLineCheck;
            SmartWindow.isChangeYPosition = false;

            try
            {
                LoadShapeDiamond();
                LoadROIs_Diamond();
                LoadROICircle_Calib();
                LoadROI_Crow_TotalDept();
                LoadROI_LowestPoint_Diamond();
                LoadROIParameters();
                LoadPlcAddress();

                // TODO: 230925 - NB
                LoadConfig_Data();

                cDeviceManager.LoadPlcTeachingAddress();
                _register_root_axis = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();
                if (_register_root_axis != null && _register_root_axis.ID > 0)
                {
                    Lib.UpdateTextBoxText(txtRootAxisX1, _register_root_axis.CenterRotateAxisX1.ToString());
                    Lib.UpdateTextBoxText(txtRootAxisY1, _register_root_axis.CenterRotateAxisY1.ToString());
                    Lib.UpdateTextBoxText(txtRootAxisX2, _register_root_axis.CenterRotateAxisX2.ToString());
                    Lib.UpdateTextBoxText(txtRootAxisY2, _register_root_axis.CenterRotateAxisY2.ToString());
                }
                LoadSettingCamera();
                Lib.UpdateTextBoxText(txtCuletX, SmartWindow.m_pCulet.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtCuletY, SmartWindow.m_pCulet.Y.ToString("F1"));
                Lib.UpdateTextBoxText(txtTableX, SmartWindow.m_pTable.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtTableY, SmartWindow.m_pTable.Y.ToString("F1"));
                Lib.UpdateTextBoxText(txtGirldleX, SmartWindow.m_pLowerGirdle.X.ToString("F1"));
                Lib.UpdateTextBoxText(txtGirldleY, SmartWindow.m_pLowerGirdle.Y.ToString("F1"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.ToString());
            }

            SmartWindow.ZoomIN = true;
            SmartWindow.ZoomOut = false;
            SmartWindow.FunctionFitImage();
            SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.1f, 1.1f);

            SmartWindow.LockRoi = false;
            _distanceRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text.Trim());
            _allowableDeviation = Lib.ToInt(txtAllowableDeviation_Micromet.Text.Trim());
            Lib.UpdateComboboxSelectedIndex(cbStoneSize, 0);
            Lib.UpdateLabelText(lbGrindingStone_L, GrindingStone_L.Value.ToString());

            // LẤY RA CODE MỚI NHẤT 
            List<history_data> lastData = _mySQLHelper.SqlToList<history_data>("SELECT DiamondCode FROM history_data ORDER BY ID DESC LIMIT 1");
            Lib.UpdateTextBoxText(txtDiamondCode, lastData[0].DiamondCode.ToString());
        }

        #region HÀM CHUYỂN ĐỔI CONFIG CAMERA
        float gainCameraLive = 0;
        float exposureTimeLive = 0;
        float gammaCameraLive = 0;
        float gainCameraStone = 0;
        float exposureTimeStone = 0;
        float gammaCameraStone = 0;
        float preampGainStone = 0;
        private void LoadSettingCamera()
        {
            List<settings_camera> lstSettingCamera = _mySQLHelper.FindAll<settings_camera>();
            for (int i = 0; i < lstSettingCamera.Count; i++)
            {
                string paramName = lstSettingCamera[i].ParamName.ToString();
                switch (paramName)
                {
                    case "GainLive":
                        gainCameraLive = (float)lstSettingCamera[i].ParamValue;
                        break;
                    case "ExposureLive":
                        exposureTimeLive = (float)lstSettingCamera[i].ParamValue;
                        break;
                    case "GammaLive":
                        gammaCameraLive = (float)lstSettingCamera[i].ParamValue;
                        break;
                    case "GainSnap":
                        gainCameraStone = (float)lstSettingCamera[i].ParamValue;
                        break;
                    case "ExposureSnap":
                        exposureTimeStone = (float)lstSettingCamera[i].ParamValue;
                        break;
                    case "GammaSnap":
                        gammaCameraStone = (float)(lstSettingCamera[i].ParamValue);
                        break;
                    case "PreampGainSnap":
                        preampGainStone = (float)(lstSettingCamera[i].ParamValue);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region LOAD DATA
        config_data _config_data = null;
        private void LoadConfig_Data()
        {
            // TODO: 010726 - NB
            try
            {
                _config_Rois_Diamond = Get_Config_Rois_Diamond();
            }
            catch { }

            try
            {
                _config_data = _mySQLHelper.FindAll<config_data>().FirstOrDefault();
                Lib.UpdateTextBoxText(txtGreater, _config_data.GreaterHeight);
                Lib.UpdateTextBoxText(txtFromTo, _config_data.AverageHeight);
                Lib.UpdateTextBoxText(txtLess, _config_data.LessHeight);

                PIXEL_TO_MM = double.Parse(_config_data.Ratio_Pixel_MM, CultureInfo.InvariantCulture);
                Lib.UpdateTextBoxText(txtRatio_Pixel_To_Mm, _config_data.Ratio_Pixel_MM);

                Lib.UpdateTextBoxText(txtAllowableDeviation_Milimet, _config_data.AllowableDeviation_MM);
                _allowableDeviation = Lib.ToDouble(txtAllowableDeviation_Milimet.Text.Trim()) * 1000;
                Lib.UpdateTextBoxText(txtAllowableDeviation_Micromet, $"{_allowableDeviation}");

                valueConvert = Lib.ToDouble(_config_data.ValueConvert);
                Lib.UpdateTextBoxText(txtValueConvert, _config_data.ValueConvert);

                offsetGrind = Lib.ToDouble(_config_data.OffsetGrindingStone);
                Lib.UpdateTextBoxText(txtOffsetGrinding, _config_data.OffsetGrindingStone);

                num_Inspect.Value = Lib.ToInt(_config_data.InspectNumber);
                Lib.UpdateTextBoxText(txtInspectActual, _config_data.InspectNumber);

                numCountSet_Min_Max.Value = Lib.ToInt(_config_data.CountSet_Min_Max);
                Lib.UpdateTextBoxText(txtCutting_Value, _config_data.DistanceGrind);

                // TODO - 271025 - NB
                distanceMove_Preview = Lib.ToDouble(_config_data.DistanceMove_Preview);
                Lib.UpdateTextBoxText(txtDistanceMove_Preview, _config_data.DistanceMove_Preview);
                Is_Calculate_TotalDepth_TAB = chk_TotalDepth_TAB.Checked = Lib.ToBoolean(_config_data.IsTotalDepth_TAB);

                // TODO - 311025 - NB
                grinding_L_Position = Lib.ToInt(_config_data.Grinding_L_Position);
                grinding_R_Position = Lib.ToInt(_config_data.Grinding_R_Position);
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        private void LoadPlcAddress()
        {
            List<RegisterPlcAddress> lstAllAddress = _mySQLHelper.FindAll<RegisterPlcAddress>();
            if (lstAllAddress.Count > 0)
            {
                string addressCode = "";
                for (int i = 0; i < lstAllAddress.Count; i++)
                {
                    addressCode = lstAllAddress[i].AddressCode.ToString().Trim();
                    switch (addressCode)
                    {
                        case "Barlight":
                            _plcAddress.BarlightAddress = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        case "ButtonSwitch":
                            _plcAddress.ButtonSwitchAddress = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        case "Trigger1":
                            _plcAddress.Trigger1 = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        case "Trigger2":
                            _plcAddress.Trigger2 = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        case "Trigger3":
                            _plcAddress.Trigger3 = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        case "Trigger4":
                            _plcAddress.Trigger4 = lstAllAddress[i].RegisterAddress.ToString().Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void loadParameterPLC()
        {
            int value = 0;
            if (cDeviceManager._plcMC != null)
            {
                cDeviceManager._plcMC.GetSingleRegister_Int32("D1830", out value);
                Lib.UpdateTextBoxText(txtStart_Auto_Grinding_Position, value.ToString());

                // Rotate Speed Grinding
                cDeviceManager._plcMC.GetSingleRegister_Int32("D1420", out value);
                Lib.UpdateTextBoxText(txtRotate_Speed, value.ToString());

                // Auto Grinding Left
                if (grinding_L_Position <= 0)
                    cDeviceManager._plcMC.GetSingleRegister_Int32("D1836", out grinding_L_Position);
                Lib.UpdateTextBoxText(txtAuto_Grinding_L_Position, grinding_L_Position.ToString());

                // Auto Grinding Right
                if (grinding_R_Position <= 0)
                    cDeviceManager._plcMC.GetSingleRegister_Int32("D1842", out grinding_R_Position);
                Lib.UpdateTextBoxText(txtAuto_Grinding_R_Position, grinding_R_Position.ToString());

                // TODO: 271025 - NB
                cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricPositionAxis2, out value);
                Lib.UpdateTextBoxText(txtConcentrick_Position_2, value.ToString());

                cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingPositionAxis3, out value);
                Lib.UpdateTextBoxText(txtGrinding_Position_3, value.ToString());
            }
        }

        #region BUTTON GET / SET PARAMATER
        private void btnStart_Auto_Grinding_Position_Get_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int32("D1830", out int value);
            Lib.UpdateTextBoxText(txtStart_Auto_Grinding_Position, value.ToString());
        }

        private void btnStart_Auto_Grinding_Position_Set_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            // TODO: 071026 - NB
            int value = Lib.ToInt(txtStart_Auto_Grinding_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D3232", value);
        }

        private void btnAuto_Grinding_L_Position_Get_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int32("D1836", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_L_Position, value.ToString());
        }

        private void btnAuto_Grinding_L_Position_Set_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            grinding_L_Position = Lib.ToInt(txtAuto_Grinding_L_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1836", grinding_L_Position);

            // TODO: 311025 - NB  
            _config_data.Grinding_L_Position = Lib.ToString(grinding_L_Position);
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        private void btnAuto_Grinding_R_Position_Get_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int32("D1842", out int value);
            Lib.UpdateTextBoxText(txtAuto_Grinding_R_Position, value.ToString());
        }

        private void btnAuto_Grinding_R_Position_Set_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            grinding_R_Position = Lib.ToInt(txtAuto_Grinding_R_Position.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1842", grinding_R_Position);

            // TODO: 311025 - NB  
            _config_data.Grinding_R_Position = Lib.ToString(grinding_R_Position);
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        private void btnGet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricPositionAxis2, out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Position_2, value.ToString());
        }

        private void btnSet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            int value = Lib.ToInt(txtConcentrick_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartConcentricPositionAxis2, value);
        }

        private void btnGet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingPositionAxis3, out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_3, value.ToString());
        }

        private void btnSet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            int value = Lib.ToInt(txtGrinding_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.StartGrindingPositionAxis3, value);
        }

        private void btnRotate_Speed_Get_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            cDeviceManager._plcMC.GetSingleRegister_Int32("D1420", out int value);
            Lib.UpdateTextBoxText(txtRotate_Speed, value.ToString());
        }

        private void btnRotate_Speed_Set_Click(object sender, EventArgs e)
        {
            if (cDeviceManager._plcMC == null) return;
            Set_Button_Light((Button)sender);

            int value = Lib.ToInt(txtRotate_Speed.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1420", value);
        }

        private void Set_Button_Light(Button btn)
        {
            Lib.UpdateButton_BColor_FColor(btn, Color.LimeGreen, Color.White);
            Set_Button_Light_Async(btn);
        }

        private async void Set_Button_Light_Async(Button btn)
        {
            await Task.Delay(300);
            Lib.UpdateButton_BColor_FColor(btn, Color.White, Color.Black);
        }
        #endregion

        #endregion

        #region LOAD THÔNG SỐ ROI, CÀI ĐẶT ROI VÀ HIỂN THỊ DISPLAY GRAPHICS
        shape_diamond_infor shapeDiamond;
        public void LoadShapeDiamond()
        {
            shapeDiamond = _mySQLHelper.FindByID<shape_diamond_infor>(1);
            if (shapeDiamond == null) return;

            SmartWindow.m_fTOP = Lib.ToDouble(shapeDiamond.TOP);
            SmartWindow.m_fBOT = Lib.ToDouble(shapeDiamond.BOT);
            SmartWindow.m_fCrow = Lib.ToDouble(shapeDiamond.Crow);
            SmartWindow.m_fGirdle = Lib.ToDouble(shapeDiamond.Girdle);
            SmartWindow.m_pCulet = new PointF((float)shapeDiamond.CuletX, (float)shapeDiamond.CuletY);
            SmartWindow.m_pTable = new PointF((float)shapeDiamond.TableX, (float)shapeDiamond.TableY);
            SmartWindow.m_pLowerGirdle = new PointF((float)shapeDiamond.LowerGirdleX, (float)shapeDiamond.LowerGirdleY);

            if (!IsShowROIs_Full) return;
            Lib.UpdateNumericUpDownValue(numRoi_Top, Lib.ToDecimal(shapeDiamond.TOP));
            Lib.UpdateNumericUpDownValue(numRoi_Bot, Lib.ToDecimal(shapeDiamond.BOT));
            Lib.UpdateNumericUpDownValue(numRoi_Crow, Lib.ToDecimal(shapeDiamond.Crow));
            Lib.UpdateNumericUpDownValue(numRoi_Girdle, Lib.ToDecimal(shapeDiamond.Girdle));

            Lib.UpdateTextBoxText(txtCuletX, SmartWindow.m_pCulet.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtCuletY, SmartWindow.m_pCulet.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableX, SmartWindow.m_pTable.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableY, SmartWindow.m_pTable.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleX, SmartWindow.m_pLowerGirdle.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleY, SmartWindow.m_pLowerGirdle.Y.ToString("F1"));
            diamondSizeOrigin = (SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y) * 2;
        }

        #region Khởi tạo ROIS
        public Dictionary<Guid, cRectangType> ROIS = new Dictionary<Guid, cRectangType>();
        // TODO: 230925 - NB - ROIS riêng bắt đá mài
        public Dictionary<Guid, cRectangType> ROIS_Stone_TotalDept = new Dictionary<Guid, cRectangType>();
        public Dictionary<Guid, cRectangType> ROIS_LowestPoint_Diamond = new Dictionary<Guid, cRectangType>();
        private Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi> DrawingObjects = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
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
        #endregion

        List<GraphicsWindow.GraphicsWindow.DataRoi> lst_SmartWindow_Rois = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
        public void LoadROIs_Diamond()
        {
            var exp1 = new Expression("IDAction", 1);
            var exp2 = new Expression("IDAction", 2);
            List<register_location_rois> lstAllROI = _mySQLHelper.FindByExpression<register_location_rois>(exp1.Or(exp2));
            for (int i = 0; i < lstAllROI.Count; i++)
            {
                Guid ID = Lib.ToGuid(lstAllROI[i].IDGuid);
                int IDAction = Lib.ToInt(lstAllROI[i].IDAction);
                int row = Lib.ToInt(lstAllROI[i].CenterX);
                int col = Lib.ToInt(lstAllROI[i].CenterY);
                double Phi = Lib.ToDouble(lstAllROI[i].Phi);
                int Width = Lib.ToInt(lstAllROI[i].Width);
                int Height = Lib.ToInt(lstAllROI[i].Height);
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
                    Rec.DescriptionCode = Lib.ToInt(lstAllROI[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstAllROI[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }
                Color color = new Color();
                if (IsShowROIs_FirtDiamond) color = Color.Transparent;
                else color = Color.Orange;

                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, color, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;
                ROIS.Add(Rec.ID, Rec);
                DrawingObjects.Add(ROI.Key, ROI);
                if ((IsShowROIs_Full || IsShowROIs_FirtDiamond) && (i <= 1 || IDAction == 11))
                    lst_SmartWindow_Rois.Add(DrawingObjects[ROI.Key]);
            }
            if (IsShowROIs_Full || IsShowROIs_FirtDiamond)
                SmartWindow.DrawROI(lst_SmartWindow_Rois);
        }

        List<GraphicsWindow.GraphicsWindow.DataRoi> lst_SmartWindow_Crow_TotalDept = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
        private void LoadROI_Crow_TotalDept()
        {
            bool IsShow_Crow_TotalDept = false;
            var exp1 = new Expression("IDAction", 11);
            List<register_location_rois> lstROI_Crow_TotalDept = _mySQLHelper.FindByExpression<register_location_rois>(exp1);
            for (int i = 0; i < lstROI_Crow_TotalDept.Count; i++)
            {
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
                    Rec.DescriptionCode = Lib.ToInt(lstROI_Crow_TotalDept[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstROI_Crow_TotalDept[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }

                Color color = new Color();
                if (IsShowROIs_FirtDiamond) color = Color.Transparent;
                else color = Color.Blue;

                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, color, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;

                if (lstROI_Crow_TotalDept[i].DescriptionCode == 5) ROIS_Stone_TotalDept.Add(Rec.ID, Rec);
                else ROIS.Add(Rec.ID, Rec);
                DrawingObjects_Crow_TotalDept.Add(ROI.Key, ROI);

                if (IsShowROIs_Full || IsShowROIs_FirtDiamond)
                {
                    if (!lst_SmartWindow_Crow_TotalDept.Contains(DrawingObjects_Crow_TotalDept[ROI.Key]))
                    {
                        if (lstROI_Crow_TotalDept[i].DescriptionCode != 5)
                            lst_SmartWindow_Crow_TotalDept.Add(DrawingObjects_Crow_TotalDept[ROI.Key]);
                    }
                }
            }

            if (IsShow_Crow_TotalDept || IsShowROIs_Full || IsShowROIs_FirtDiamond)
            {
                SmartWindow.DrawROI(lst_SmartWindow_Crow_TotalDept);
                IsShow_Crow_TotalDept = false;
            }
        }
        List<GraphicsWindow.GraphicsWindow.DataRoi> lst_SmartWindow_LowestPoint_Diamond = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
        private void LoadROI_LowestPoint_Diamond()
        {
            var exp1 = new Expression("IDAction", 12);
            List<register_location_rois> lstROI_LowestPoint_Diamond = _mySQLHelper.FindByExpression<register_location_rois>(exp1);
            for (int i = 0; i < lstROI_LowestPoint_Diamond.Count; i++)
            {
                Guid ID = Lib.ToGuid(lstROI_LowestPoint_Diamond[i].IDGuid);
                int IDAction = Lib.ToInt(lstROI_LowestPoint_Diamond[i].IDAction);
                int row = Lib.ToInt(lstROI_LowestPoint_Diamond[i].CenterX);
                int col = Lib.ToInt(lstROI_LowestPoint_Diamond[i].CenterY);
                double Phi = Lib.ToDouble(lstROI_LowestPoint_Diamond[i].Phi);
                int Width = Lib.ToInt(lstROI_LowestPoint_Diamond[i].Width);
                int Height = Lib.ToInt(lstROI_LowestPoint_Diamond[i].Height);
                int Position = Lib.ToInt(lstROI_LowestPoint_Diamond[i].Height);
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
                    Rec.DescriptionCode = Lib.ToInt(lstROI_LowestPoint_Diamond[i].DescriptionCode);
                    Rec.DescriptionName = Lib.ToString(lstROI_LowestPoint_Diamond[i].DescriptionName);
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }
                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Blue, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;

                ROIS_LowestPoint_Diamond.Add(Rec.ID, Rec);
                DrawingObjects_LowestPoint_Diamond.Add(ROI.Key, ROI);

                if (IsShowROI_LowestPoint)
                {
                    if (!lst_SmartWindow_LowestPoint_Diamond.Contains(DrawingObjects_LowestPoint_Diamond[ROI.Key]))
                        lst_SmartWindow_LowestPoint_Diamond.Add(DrawingObjects_LowestPoint_Diamond[ROI.Key]);
                }
            }

            if (IsShowROI_LowestPoint)
                SmartWindow.DrawROI(lst_SmartWindow_LowestPoint_Diamond);
        }

        private void LoadROIParameters()
        {
            List<register_roi_parameters> lstRoiProperties = _mySQLHelper.FindAll<register_roi_parameters>();
            for (int i = 0; i < lstRoiProperties.Count; i++)
            {
                // Từ giá trị đã có gán vào đối tượng Rectangle
                Guid ID = Lib.ToGuid(lstRoiProperties[i].IDROI);
                int IDAction = Lib.ToInt(lstRoiProperties[i].IDAction);
                string DetectType = Lib.ToString(lstRoiProperties[i].DetectType);
                bool FillHole = Lib.ToBoolean(lstRoiProperties[i].FillHole);
                double ThresholdMin = Lib.ToInt(lstRoiProperties[i].ThresholdMin);
                double ThresholdMax = Lib.ToDouble(lstRoiProperties[i].ThresholdMax);
                string EdgeTransition = Lib.ToString(lstRoiProperties[i].EdgeTransition);
                string EdgeType = Lib.ToString(lstRoiProperties[i].EdgeType);
                double EdgeThreshold = Lib.ToInt(lstRoiProperties[i].EdgeThreshold);
                int SamplingPercent = Lib.ToInt(lstRoiProperties[i].SamplingPercent);
                //GraphicsWindow.GraphicsWindow.DataRoi ROI;

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
                ROIProperties_Parameters.Add(ID, property);
            }
        }

        #region Hiển thị Display Graphics trên SmartWindow
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

        #endregion

        #region BUTTON CONNECT
        register_root_axis _register_root_axis = null;
        private void btnConnect_Device_Click(object sender, EventArgs e)
        {
            // Connect thiết bị
            if (!IsRun)
            {
                WaitDialogForm waitDialog = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang kết nối thiết bị");
                waitDialog.Show();
                try
                {
                    #region Connect PLC
                    cDeviceManager.Connect_PLC();
                    if (cDeviceManager._plcMC.Connected)
                        Lib.UpdateButtonText_BColor_FColor(btnStatusPLC, "Connected", Color.Green, Color.White);
                    else
                        Lib.UpdateButtonText_BColor_FColor(btnStatusPLC, "Disconnected", Color.Red, Color.White);
                    #endregion

                    #region Connect Camera
                    string errMessage = string.Empty;
                    bool IsConnect = cDeviceManager.ConnectCamera_Hikrobot(cbDeviceList.Text, ref errMessage);
                    if (!IsConnect)
                        Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                    else
                        Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);
                    #endregion

                    // TODO: 070526 - NB
                    waitDialog.Close();
                    if (cDeviceManager._plcMC == null || !cDeviceManager._plcMC.Connected || cDeviceManager.HikCamera == null)
                    {
                        if (IsConnect) DisconnectCamera_Hikrobot();
                        return;
                    }

                    IsRun = true;
                    step_progress = 1;

                    if (btnConnect_Device.InvokeRequired)
                    {
                        btnConnect_Device.Invoke((MethodInvoker)delegate
                        {
                            btnConnect_Device.Appearance.BackColor = Color.FromArgb(0, 200, 83); // Xanh lá cây tươi
                            btnConnect_Device.Appearance.ForeColor = Color.White; // Chữ trắng
                            btnConnect_Device.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
                        });
                    }
                    else
                    {
                        btnConnect_Device.Appearance.BackColor = Color.FromArgb(0, 200, 83); // Xanh lá cây tươi
                        btnConnect_Device.Appearance.ForeColor = Color.White; // Chữ trắng
                        btnConnect_Device.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
                    }
                    Lib.UpdateMultipleButtonBackColor(Color.Orange, btnGrindAutoState, btnInspectState, btnConcentricState);
                    _register_root_axis = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();

                    loadParameterPLC();

                    #region Auto Reconnect
                    if (_reconnectPLC_MC == null)
                    {
                        _reconnectPLC_MC = new Thread(() => ReconnectPLC_MC());
                        _reconnectPLC_MC.IsBackground = true;
                        _reconnectPLC_MC.Start();
                    }
                    #endregion

                    #region Tìm đường kính nhỏ nhất 
                    if (_findMin == null)
                    {
                        _findMin = new Thread(FindMin);
                        _findMin.IsBackground = true;
                        _findMin.Start();
                    }
                    #endregion

                    #region Check alive PLC + Alarm MainProcess
                    if (_checkAlive == null)
                    {
                        _checkAlive = new Thread(() => CheckAlive());
                        _checkAlive.IsBackground = true;
                        _checkAlive.Start();
                    }

                    if (_checkAlarm_MainProcess == null)
                    {
                        _checkAlarm_MainProcess = new Thread(() => CheckAlarm_MainProcess());
                        _checkAlarm_MainProcess.IsBackground = true;
                        _checkAlarm_MainProcess.Start();
                    }
                    #endregion

                    #region Luồng mài tự động : check đồng tâm --> mài --> đo độ đảo
                    if (_checkTriggerConcentric == null || !_checkTriggerConcentric.IsAlive)
                    {
                        _checkTriggerConcentric = new Thread(() => CheckTriggerConcentric());
                        _checkTriggerConcentric.IsBackground = true;
                        _checkTriggerConcentric.Start();
                    }

                    // Luồng Mài Tự động
                    if (_functionMeasure == null || !_functionMeasure.IsAlive)
                    {
                        _functionMeasure = new Thread(() => GrindingAutoFunction());
                        _functionMeasure.IsBackground = true;
                        _functionMeasure.Start();
                    }

                    // Inspect Function
                    if (_runFunctionInspect == null || !_runFunctionInspect.IsAlive)
                    {
                        _runFunctionInspect = new Thread(() => InspectFunction());
                        _runFunctionInspect.IsBackground = true;
                        _runFunctionInspect.Start();
                    }
                    #endregion

                    #region Luồng mài Manual
                    _functionMeasureManual = new Thread(() => MeasureFunctionManual());
                    _functionMeasureManual.IsBackground = true;
                    _functionMeasureManual.Start();
                    #endregion

                    btnLiveCamera_Click(null, null);

                    // TODO: 210825 - NB
                    SmartWindow_MouseUp(null, null);

                    Lib.UpdateButtonEnable(btnChangeSettingCamera, true);
                    cDeviceManager._plcMC.SetSingleRegister_Int16("D3125", Lib.ToInt(num_Inspect.Value));
                    Lib.UpdateTextBoxText(txtInspectActual, $"{num_Inspect.Value}");
                    btnGetDiameter.Focus();

                    // TODO: 311025 - NB
                    // Auto Grinding Left
                    if (grinding_L_Position > 0)
                        cDeviceManager._plcMC.SetSingleRegister_Int32("D1836", grinding_L_Position);
                    // Auto Grinding Right
                    if (grinding_R_Position > 0)
                        cDeviceManager._plcMC.SetSingleRegister_Int32("D1842", grinding_R_Position);

                    // TODO: 130526 - NB
                    SetBitsOff("D254.0");
                    SetBitsOff("D252.1");
                    SetBitsOff("D252.0");
                }
                catch (Exception ex)
                {
                    waitDialog.Close();
                    Lib.saveToErr(ex.ToString());
                }
            }
            else
            {
                WaitDialogForm waitDialog = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang ngắt kết nối thiết bị");
                waitDialog.Show();
                try
                {
                    IsRun = false;
                    show_progress = 0;
                    // Khi khôi phục màu về mặc định
                    btnConnect_Device.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
                    btnConnect_Device.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
                    btnConnect_Device.Appearance.Options.UseBackColor = false;
                    btnConnect_Device.Appearance.Options.UseForeColor = false;
                    DisconnectCamera_Hikrobot();
                    AbortThread(_checkTriggerConcentric);
                    AbortThread(_functionMeasure);
                    AbortThread(_runFunctionInspect);
                    AbortThread(_functionMeasureManual);
                    AbortThread(_reconnectPLC_MC);
                    AbortThread(_findMin);
                    AbortThread(_process_ReadDataPLC);
                    AbortThread(_mapIO);

                    SetBitsOff("D202.0"); //ready
                    SetBitsOff("D202.1"); //busy
                    SetBitsOff("D202.2"); //OK
                    SetBitsOff("D202.3"); //NG
                    SetBitsOff("D202.4"); // Mode manual 

                    SetBitsOff("D206.0"); //ready
                    SetBitsOff("D206.1"); //busy
                    SetBitsOff("D206.2"); //OK
                    SetBitsOff("D206.3"); //NG

                    SetBitsOff("D210.0"); //ready
                    SetBitsOff("D210.1"); //busy
                    SetBitsOff("D210.2"); //OK
                    SetBitsOff("D210.3"); //NG

                    //TODO : HuyNT - 230525
                    SetBitsOff("D218.1"); //pm gửi tín hiệu get diameter
                    SetBitsOff("D218.2"); //complete get diameter

                    cDeviceManager.Disconnect_PLC();
                    Lib.UpdateButtonText_BColor_FColor(btnStatusPLC, "Connect", Color.Transparent, Color.Black);
                    Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connect", Color.Transparent, Color.Black);
                    Lib.UpdateButtonBackColor(btnAlivePLC, Color.Transparent);
                    Lib.UpdateMultipleButtonBackColor(Color.Transparent, btnGrindAutoState, btnInspectState, btnConcentricState);

                    if (IsStartProcess)
                        btnStartProcess_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                waitDialog.Close();
            }
            //btnSettingVision.Enabled = !IsRun;
        }
        #endregion

        #region BUTTONS START PROCESS
        bool IsStartProcess = false;
        int countStartProcessPLC = 0;
        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            double distanceUserRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text.Trim());

            if (distanceUserRequest <= 0)
            {
                MessageBox.Show("Please enter Diameter Request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtAllowableDeviation_Milimet.Text.Trim()))
            {
                MessageBox.Show("Please enter Allowable Deviation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(num_Inspect.Text.Trim()))
            {
                MessageBox.Show("Please enter number position check Inspect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtDiamondCode.Text.Trim()))
            {
                MessageBox.Show("Please enter Diamond Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // TODO: 240425
            if (!IsLive && IsStartProcess)
            {
                ShowPanel_WaitingForm("Bạn đang Snap Camerea\r\nXin vui lòng Live Camera (tab Settings bên phải)", 1);
                return;
            }

            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang xử lý Start / Stop Process");
            wait.Show();

            if (!IsStartProcess)
            {
                if (IsReset_PLC)
                    IsReset_PLC = false;

                Lib.UpdateSimpleButtonEnable(btnLock, false);
                Lib.UpdateSimpleButtonEnable(btnCalib, false);
                Lib.UpdateSimpleButtonEnable(btnSnapCamera, false);
                Lib.UpdateSimpleButtonEnable(btnSnapCalib, false);
                Lib.UpdateSimpleButtonEnable(btnLiveCamera, false);

                Is_ShowTightenForce = IsGrindDiamond_LineFindRun = SmartWindow.LockRoi = false;
                step_progress = 1;
                show_progress = 0;

                if (chkMove_ROI.InvokeRequired) chkMove_ROI.Invoke((MethodInvoker)delegate { chkMove_ROI.Checked = false; });
                else chkMove_ROI.Checked = false;
                SmartWindow.isChangeYPosition = false;

                #region CLEAR CÁC TÍN HIỆU PLC
                SetBitsOff("D202.0"); //ready
                SetBitsOff("D202.1"); //busy
                SetBitsOff("D202.2"); //OK
                SetBitsOff("D202.3"); //NG
                SetBitsOff("D202.4"); // Mode manual 


                SetBitsOff("D206.0"); //ready
                SetBitsOff("D206.1"); //busy
                SetBitsOff("D206.2"); //OK
                SetBitsOff("D206.3"); //NG

                SetBitsOff("D210.0"); //ready
                SetBitsOff("D210.1"); //busy
                SetBitsOff("D210.2"); //OK
                SetBitsOff("D210.3"); //NG

                //TODO : HuyNT - 230525
                SetBitsOff("D218.1"); //pm gửi tín hiệu get diameter
                SetBitsOff("D218.2"); //complete get diameter

                //TODO : HuyNT - 100625 - gửi bit xác nhận Start Process xuống PLC
                if (cDeviceManager._plcMC.Connected)
                {
                    countStartProcessPLC = 0;
                    while (countStartProcessPLC < 5)
                    {
                        countStartProcessPLC++;
                        SetBitsOn("D202.5");
                        Thread.Sleep(50);
                        if (GetBits("D202.5")) break;

                    }
                    if (countStartProcessPLC >= 4)
                    {
                        ShowPanel_WaitingForm("On bit Start Process thất bại", 1);
                        return;
                    }
                }

                // Cycletime búa gõ
                cDeviceManager._plcMC.SetSingleRegister_Int16("D4000", 0);
                // Cycletime mài
                cDeviceManager._plcMC.SetSingleRegister_Int16("D4100", 0);
                #endregion

                _register_root_axis = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();
                _distanceRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text.Trim());
                Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, $"{_distanceRequest}");

                if (SmartWindow.m_fTOP <= 0)
                    LoadShapeDiamond();

                IsStartProcess = true;
                btnSettingVision.Enabled = isReady_Concentric = isReady_Grinding = isReady_Inspect = !IsStartProcess;
                btnStartProcess.Appearance.BackColor = Color.FromArgb(0, 200, 83);
                btnStartProcess.Appearance.ForeColor = Color.White;

                cDeviceManager._plcMC.SetSingleRegister_Int16("D250", modeRunning);

                if (btnRunMode_Auto.InvokeRequired) btnRunMode_Auto.Invoke((MethodInvoker)delegate { btnRunMode_Auto.Enabled = false; });
                else btnRunMode_Auto.Enabled = false;

                if (btnRunMode_Manual.InvokeRequired) btnRunMode_Manual.Invoke((MethodInvoker)delegate { btnRunMode_Manual.Enabled = false; });
                else btnRunMode_Manual.Enabled = false;

                txtRatio_Pixel_To_Mm.ReadOnly = true;
                btnSaveRatio.Enabled = false;

                if (Is_Bypass_Concentric)
                {
                    Is_Bypass_Concentric = false;
                    Lib.UpdateButton_BColor_FColor(btnBypass_Concentric, Color.White, Color.Black);
                }
            }
            else
            {
                ClearProcess();

                //TODO : HuyNT - 10062025
                if (cDeviceManager._plcMC.Connected)
                {
                    countStartProcessPLC = 0;
                    while (countStartProcessPLC < 5)
                    {
                        countStartProcessPLC++;
                        SetBitsOff("D202.5");
                        Thread.Sleep(50);
                        if (!GetBits("D202.5")) break;

                    }
                    if (countStartProcessPLC >= 4)
                    {
                        ShowPanel_WaitingForm("Off bit Start Process thất bại", 1);
                        wait.Close();
                        return;
                    }
                }

            }
            ClosePanel();
            wait.Close();
        }

        private void ClearProcess()
        {
            // TODO : 200525
            count_GrindingAuto = 0;
            isFindMin = false;
            show_progress = step_progress = 0;
            modeRunning = 1;
            IsShowROI_LowestPoint = false;
            IsStartProcess = false;
            // TODO: 191125 - NB
            if (chkResizeTarget.Checked)
                Lib.UpdateTextboxReadOnly(txtDistance_Target_Milimet, false);
            Lib.UpdateTextboxReadOnly(txtAllowableDeviation_Milimet, false);
            Lib.UpdateTextboxReadOnly(txtCutting_Value, false);
            Lib.UpdateNumericReadOnly(num_Inspect, false);
            btnSettingVision.Enabled = !IsStartProcess;

            // TODO: 270825 - NB
            //SmartWindow.ClearAllRoi();

            //TODO :  HuyNT - 18062025
            isReady_Concentric = false;
            isReady_Grinding = false;
            isReady_Inspect = false;
            IsMode_ConcentricManual = false;
            IsConfirm_ConcentricManual = false;
            Is_ShowTightenForce = false;

            _valueAngle = 0;
            _height_Frinding_Diamond_Real = 0;
            _height_Frinding_Diamond_Max = 0;
            IsGrindDiamond_LineFindRun = false;
            get_Diameter_Manual = false;
            startProcess = false;
            lstDuongKinh = new List<double>();

            //TODO : HuyNT - 10062025 : Off bit start process PLC
            try
            {
                if (cDeviceManager._plcMC.Connected)
                {
                    countStartProcessPLC = 0;
                    while (countStartProcessPLC < 5)
                    {
                        countStartProcessPLC++;
                        SetBitsOff("D202.5");
                        Thread.Sleep(50);
                        if (!GetBits("D202.5")) break;

                    }
                    if (countStartProcessPLC >= 4)
                    {
                        ShowPanel_WaitingForm("Off bit Start Process thất bại", 1);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }

            btnStartProcess.Appearance.BackColor = Color.Empty;
            btnStartProcess.Appearance.ForeColor = Color.Empty;
            btnStartProcess.Appearance.Options.UseBackColor = false;
            btnStartProcess.Appearance.Options.UseForeColor = false;

            Lib.UpdateSimpleButtonEnable(btnLock, true);
            Lib.UpdateSimpleButtonEnable(btnCalib, true);
            Lib.UpdateSimpleButtonEnable(btnSnapCamera, true);
            Lib.UpdateSimpleButtonEnable(btnSnapCalib, true);
            Lib.UpdateSimpleButtonEnable(btnLiveCamera, true);

            if (btnRunMode_Auto.InvokeRequired)
                btnRunMode_Auto.Invoke((MethodInvoker)delegate { btnRunMode_Auto.Enabled = true; });
            else
                btnRunMode_Auto.Enabled = true;
            if (btnRunMode_Manual.InvokeRequired)
                btnRunMode_Manual.Invoke((MethodInvoker)delegate
                {
                    btnRunMode_Manual.Enabled = true;
                });
            else
            {
                btnRunMode_Manual.Enabled = true;
            }

            // TODO: 110526 - NB
            btnRunMode_Auto_Click(null, null);
            //ShowButtonRunning(1);
        }

        #endregion

        #region MAIN PROCESS
        double show_progress = 0;
        int step_progress = 1;

        #region STEP 1: KIỂM TRA ĐỒNG TÂM

        #region Khai báo
        bool IsMode_ConcentricManual = false;
        bool IsShowLiveCamera = false;
        bool isReady_Concentric = false;
        bool startProcess = false;
        DateTime dateTime_Start;
        bool Is_OpenFrontLight = false;

        // TODO: 060825 - NB
        Stopwatch sw_Concentric_Total = new Stopwatch();
        Stopwatch sw_Concentric = new Stopwatch();
        #endregion

        /// <summary>
        /// Hàm kiểm tra đồng tâm
        /// </summary>
        private void CheckTriggerConcentric()
        {
            //phần mềm on Bit Ready Chạy Function Đồng tâm
            SetBitsOn("D202.0"); //Ready
            isReady_Concentric = false;
            while (IsRun)
            {
                Thread.Sleep(100);
                if (!cDeviceManager._plcMC.Connected || step_progress != 1 || !IsStartProcess) continue;
                if (!isReady_Concentric && !GetBits("D202.0")) SetBitsOn("D202.0"); //Ready
                try
                {
                    #region PLC TRIGGER KIỂM TRA ĐỒNG TÂM
                    bool dataTrigger4 = GetBits("D102.0");
                    if (dataTrigger4 && IsModeOption)
                    {
                        SetBitsOff("D216.0");

                        if (sw_Concentric_Total != null && !sw_Concentric_Total.IsRunning)
                        {
                            sw_Concentric_Total.Restart();
                            sw_Concentric_Total.Start();
                        }

                        if (IsFist_Process == 0)
                        {
                            Lib.UpdateTextBoxText(txtLogProcess, string.Empty);
                            Lib.saveToLogConcentric(txtLogProcess, "START\r\n");
                            IsFist_Process = 1;
                            ClosePanel();
                        }

                        Lib.saveToLogConcentric(txtLogProcess, "1. Kiểm tra triger đồng tâm D102.0 ON");
                        SetBitsOff("D220.1"); //bit ok công đoạn tìm min
                        SetBitsOff("D218.2"); //bit hoàn thành công đoạn tìm min

                        show_progress = 1;
                        if (!startProcess)
                        {
                            startProcess = true;
                            dateTime_Start = DateTime.Now;
                        }
                        isReady_Concentric = true;
                        IsShowLiveCamera = true;

                        if (sw_Concentric != null) sw_Concentric.Restart();
                        sw_Concentric.Start();

                        #region TÍNH TOÁN ĐỒNG TÂM
                        try
                        {
                            Lib.saveToLogConcentric(txtLogProcess, "3. Run LineFind Tool để tìm ra các đoạn thẳng");

                            if (DisplayImage == null) continue;
                            if (_allowableDeviation == 0)
                                _allowableDeviation = Lib.ToInt(txtAllowableDeviation_Micromet.Text.Trim());
                            string textView = string.Empty;

                            // BYPASS việc gõ đồng tâm
                            if (Is_Bypass_Concentric)
                            {
                                SetBitsOff("D202.0"); //Ready
                                SetBitsOn("D202.1"); //Busy

                                Lib.saveToLogConcentric(txtLogProcess, $"6. Bypass gõ đồng tâm {DateTime.Now}");
                                cDeviceManager._plcMC.SetSingleRegister_Int32("D204", Lib.ToInt(_allowableDeviation));
                                cDeviceManager._plcMC.SetSingleRegister_Int32("D212", 0);

                                SetBitsOn("D202.2"); //OK
                                SetBitsOff("D202.3"); //NG

                                textView = "Deviation Concentric : BYPASS" + "\r\n" + "Deviation Shaft : BYPASS" + "\r\n" + "Angle Measurement : BYPASS";
                            }
                            else
                            {
                                // Run LineFind Tool để tìm ra các đoạn thẳng
                                //LineFindRun();
                                // TODO: 060825 - NB
                                LineFindRun_Concentric();

                                Lib.saveToLogConcentric(txtLogProcess, "4. Gửi giá trị độ lệch :" + (_deviationConcentric).ToString() + " và giá trị bit đồng tâm");

                                #region GỬI DỮ LIỆU XUỐNG PLC VÀ HIỂN THỊ MÀN HÌNH
                                SetBitsOff("D202.0"); //Ready
                                SetBitsOn("D202.1"); //Busy

                                // TODO: 211125 - NB
                                if (Lib.ToInt(_deviationConcentric) >= 100)
                                    _deviationConcentric_SendPLC = Lib.ToInt(_deviationConcentric) / 2;
                                else if (Lib.ToInt(_deviationConcentric) >= 50)
                                    _deviationConcentric_SendPLC = 50;
                                else
                                    _deviationConcentric_SendPLC = _deviationConcentric;

                                // TODO: 211125 - NB
                                cDeviceManager._plcMC.SetSingleRegister_Int32("D204", Lib.ToInt(_deviationConcentric_SendPLC));
                                cDeviceManager._plcMC.SetSingleRegister_Int32("D212", Lib.ToInt(_deviationBetweenTwoShaft));

                                //Kiểm tra nếu độ lệch nằm trong khoảng cho phép thì sẽ ON bit Complete đồng tâm, nếu không thì OFF bit
                                Lib.saveToLogConcentric(txtLogProcess, "5. Kiểm tra nếu độ lệch nằm trong khoảng cho phép thì sẽ ON bit Complete đồng tâm, nếu không thì OFF bit");

                                #region KIỂM TRA GÓC LỚN HƠN 15, SẼ CHẠY MODE MANUAL
                                if (_valueAngle >= 15)
                                {
                                    Lib.saveToLogConcentric(txtLogProcess, "6. Góc lớn hơn 15 nên gửi tín hiệu chạy mode manual");
                                    SetBitsOff("D202.2"); // OK
                                    SetBitsOff("D202.3"); // NG
                                    IsMode_ConcentricManual = true;
                                    IsShowLiveCamera = false;
                                    SetBitsOn("D202.4"); // Mode manual

                                    modeRunning = 2;
                                    ShowButtonRunning(modeRunning);
                                    if (btnRunMode_Auto.InvokeRequired)
                                        btnRunMode_Auto.Invoke((MethodInvoker)delegate { btnRunMode_Auto.Enabled = true; });
                                    else btnRunMode_Auto.Enabled = true;
                                    if (btnRunMode_Manual.InvokeRequired)
                                        btnRunMode_Manual.Invoke((MethodInvoker)delegate { btnRunMode_Manual.Enabled = true; });
                                    else btnRunMode_Manual.Enabled = true;

                                    cDeviceManager._plcMC.SetSingleRegister_Int16("D250", 2);
                                    ShowPanel_WaitingForm("HAI MÉP VIÊN KIM CƯƠNG BỊ VÁT CẠNH.\r\nXIN VUI LÒNG CHUYỂN MODE MANUAL VÀ GÕ ĐỒNG TÂM THỦ CÔNG!", 1);
                                    continue;
                                }
                                else
                                {
                                    Lib.saveToLogConcentric(txtLogProcess, "6. Góc nhỏ hơn 15");
                                    if (Math.Abs(_deviationConcentric) <= _allowableDeviation)
                                    {
                                        SetBitsOn("D202.2"); //OK
                                        SetBitsOff("D202.3"); //NG
                                    }
                                    else
                                    {
                                        SetBitsOn("D202.3"); //NG
                                        SetBitsOff("D202.2"); //OK
                                        SetBitsOn("D202.1"); //busy
                                        SetBitsOff("D202.0"); //ready
                                    }
                                }
                                #endregion

                                // TODO: 211125 - NB
                                textView = "Deviation Concentric: " + _deviationConcentric.ToString("F3")
                                + "\r\n" + "Deviation Concentric -> PLC: " + _deviationConcentric_SendPLC.ToString("F3")
                                + "\r\n" + "Deviation Shaft: " + _deviationBetweenTwoShaft.ToString("F3")
                                + "\r\n" + "Angle Measurement: " + _valueAngle.ToString("F2");
                                #endregion
                            }

                            #region Hiển thị giá trị độ lệch lên ảnh
                            Lib.saveToLogConcentric(txtLogProcess, "7. Hiển thị giá trị độ lệch lên ảnh");
                            SmartWindow.ClearDispText();
                            DataDispText displayText = new DataDispText();
                            displayText.TextView = textView;
                            displayText.InPoint = new Point(100, 1000);
                            displayText.InSize = 200;
                            displayText.InColor = "Red";
                            displayText.Key = Guid.NewGuid();
                            SmartWindow.DispText(displayText);
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Lib.saveToErr(ex.ToString());
                        }
                        #endregion

                        sw_Concentric.Stop();
                        Lib.saveToLogConcentric(txtLogProcess, "Cycletime Concentric : " + (sw_Concentric.ElapsedMilliseconds).ToString());
                        IsShowLiveCamera = false;
                    }
                    #endregion

                    #region ĐỌC TÍN HIỆU CONFIRM CỦA PLC KHI CÔNG NHÂN CHUYỂN CHẾ ĐỘ MANUAL
                    // TODO: 050525
                    if (GetBits("M5050") && !IsModeOption && IsMode_ConcentricManual) // tín hiệu confirm + chuyển nút vặn thực tế
                    {
                        Lib.saveToLogConcentric(txtLogProcess, "8. Kiểm tra tín hiệu CONFIRM CỦA PLC KHI CÔNG NHÂN CHUYỂN CHẾ ĐỘ MANUAL");
                        ClosePanel();
                        SetBitsOff("D202.4");
                        show_progress = 1.5;
                        step_progress = 2;
                        ShowPanel_WaitingForm("ĐANG TRONG QUY TRÌNH GÕ ĐỒNG TÂM THỦ CÔNG\r\nCHUẨN BỊ SẴN SÀNG MÀI TỰ ĐỘNG", 2, 2);
                        SetBitsOff("M5050");
                        Lib.saveToLogConcentric(txtLogProcess, "9. ĐỌC TÍN HIỆU CONFIRM CỦA PLC KHI CÔNG NHÂN CHUYỂN CHẾ ĐỘ MANUAL");
                    }
                    #endregion

                    #region Check tín hiệu bit trigger 4, nếu ON thì vision bắt đầu tính toán giá trị độ lệch
                    if (GetBits("D102.1") && IsModeOption)
                    {
                        Lib.saveToLogConcentric(txtLogProcess, "10. Check tín hiệu bit trigger 4, nếu ON thì vision bắt đầu tính toán giá trị độ lệch");
                        Lib.saveToLogConcentric(txtLogProcess, "11. Tín hiệu Bit 102.1 ON");
                        show_progress = 1;
                        isReady_Concentric = false;
                        SetBitsOff("D202.3"); //OK
                        SetBitsOff("D202.2"); //NG
                        SetBitsOff("D202.1"); //Busy
                        SetBitsOn("D202.0"); //Ready
                        Lib.saveToLogConcentric(txtLogProcess, "12. Check Complete công đoạn");
                    }
                    #endregion

                    #region Check Complete công đoạn
                    if (GetBits("M3020") && IsModeOption)
                    {
                        try
                        {
                            // TODO: 070526 - NB
                            if (!Is_OpenFrontLight && !ckb_isTightenForce.Checked) // nếu lần trước đó đã bấm tích chọn, thì ko chuyển đèn nữa
                            {
                                WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Front Light");
                                wait.Show();
                                Open_FrontLight();
                                Is_OpenFrontLight = true;
                                wait.Close();
                            }

                            // TODO: 130526 - NB
                            if (ckb_isTightenForce.Checked)
                                SetBitsOn("D254.0");

                            Lib.UpdateLabelText(lblValue_Average, "0");
                        }
                        catch (Exception)
                        {
                        }

                        Lib.saveToLogConcentric(txtLogProcess, "13. Bit M3020 ON, đã conplete");
                        show_progress = 1.5;
                        step_progress = 2;
                        SetBitsOff("M3020");
                        Lib.UpdateButtonBackColor(btnConcentricState, Color.Green);
                        count_GrindingAuto = _height_Frinding_Diamond_Real = _height_Frinding_Diamond_Real = 0;
                        Is_ShowTightenForce = IsGrindDiamond_LineFindRun = false;
                        SmartWindow.FunctionFitImage();
                        SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.1f, 1.1f);

                        // TODO: 060825 - NB
                        sw_Concentric_Total.Stop();
                        Lib.saveToLogGrinding(txtLogProcess, $"END - 14. Đọc bit từ PLC hiển thị cảnh báo siết lực\r\nCycletime: {sw_Concentric_Total.ElapsedMilliseconds}");
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Lib.saveToLog(ex.ToString());
                }
            }
        }
        #endregion

        #region STEP 2: MÀI AUTO

        #region Khai báo
        double _deviationConcentric = 0;
        // TODO: 211125 - NB
        double _deviationConcentric_SendPLC = 0;
        double _deviationBetweenTwoShaft = 0;
        double _distanceGrinding = 0;
        double _valueAngle = 0;
        bool isReady_Grinding = false;
        int _height_Frinding_Diamond_Real = 0;
        int _height_Frinding_Diamond_SendPLC = 0;
        int _height_Frinding_Diamond_Max = 0;
        int count_GrindingAuto = 0;
        bool IsGrindDiamond_LineFindRun = false;
        bool Is_ShowTightenForce = false;

        // TODO: 060825 - NB
        Stopwatch sw_Grinding_Total = new Stopwatch();
        Stopwatch sw_Grinding = new Stopwatch();
        #endregion

        private void GrindingAutoFunction()
        {
            if (_distanceRequest == 0)
                _distanceRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text.Trim());
            double distanceMoveOld = _distanceGrinding - _distanceRequest;
            double distanceMoveNew = _distanceGrinding - _distanceRequest;
            SetBitsOn("D206.0"); //ready
            isReady_Grinding = false;
            while (IsRun)
            {
                Thread.Sleep(100);
                if (!cDeviceManager._plcMC.Connected || !IsModeOption || !IsStartProcess) continue;

                #region CHUYỂN SANG MODE MANUAL
                try
                {
                    if (GetBits("M5058"))
                    {
                        Lib.saveToLogGrinding(txtLogProcess, "[CHUYỂN MODE] - Chuyển sang Mode Manual");
                        // TODO: 050525 
                        IsMode_ConcentricManual = false;
                        SetBitsOff("M5058");
                        SetBitsOn("D270.0");
                        step_progress = 2;
                    }
                }
                catch (Exception) { }
                #endregion

                if (step_progress != 2) continue;
                if (!isReady_Grinding && !GetBits("D206.0")) SetBitsOn("D206.0"); //Ready
                try
                {
                    #region CẢNH BÁO CÔNG NHÂN XIẾT THÊM 1 LỰC
                    // Đọc bit từ PLC hiển thị cảnh báo siết lực
                    bool is_Show_TightenForce = GetBits("M5032");
                    if (is_Show_TightenForce && !Is_ShowTightenForce && !ckb_isTightenForce.Checked)
                    {
                        Lib.saveToLogGrinding(txtLogProcess, "[XIẾT LỰC] - START\r\n1. Đọc bit từ PLC hiển thị cảnh báo siết lực");
                        Is_ShowTightenForce = true;
                        ShowPanel_WaitingForm("Xin vui lòng xiết thêm lực trục xoay sau khi căn chỉnh!", 2);

                        // TODO: 070526 - NB
                        if (ckb_isTightenForce.InvokeRequired)
                            ckb_isTightenForce.Invoke((MethodInvoker)delegate
                            {
                                ckb_isTightenForce.Visible = true;
                            });
                        else
                        {
                            ckb_isTightenForce.Visible = true;
                        }
                    }
                    if (!is_Show_TightenForce && Is_ShowTightenForce) // Công nhân bấm nút Start cứng để chạy tiếp tục (ko dùng chuột)
                    {
                        Is_ShowTightenForce = false;
                        ClosePanel();
                    }
                    #endregion

                    #region Cảnh báo yêu cầu công nhân xác nhận sau khi gõ thủ công -> chuyển sang mài tự động
                    if (GetBits("M5052") && IsMode_ConcentricManual && !ckb_isTightenForce.Checked)
                    {
                        Lib.saveToLogGrinding(txtLogProcess, "[XIẾT LỰC] 2. Cảnh báo yêu cầu công nhân xác nhận sau khi gõ thủ công-> chuyển sang mài tự động");
                        // TODO: 050525 
                        IsMode_ConcentricManual = false;
                        SetBitsOff("M5052");
                        ShowPanel_WaitingForm("SAU KHI GÕ ĐỒNG TÂM THỦ CÔNG\r\nBẠN CHẮC CHẮN MUỐN CHẠY CHU TRÌNH MÀI TỰ ĐỘNG KHÔNG?", 2);
                        IsConfirm_ConcentricManual = true;
                        continue;
                    }
                    #endregion

                    #region CHUYỂN CHẾ ĐỘ ĐÈN BACK LIGHT
                    // Thêm bit + cờ khi bắt tín hiệu bật đèn
                    if (GetBits("M3200") && Is_OpenFrontLight)
                    {
                        try
                        {
                            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Back Light");
                            wait.Show();
                            Open_BackLight();
                            Is_OpenFrontLight = false;
                            wait.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    #endregion

                    #region BẮT ĐẦU MÀI
                    if (GetBits("D106.0") && !IsMode_ConcentricManual && !IsConfirm_ConcentricManual)
                    {
                        // TODO: 060825 - NB
                        if (sw_Grinding_Total != null && !sw_Grinding_Total.IsRunning)
                        {
                            sw_Grinding_Total.Restart();
                            sw_Grinding_Total.Start();
                        }

                        if (IsFist_Process == 1)
                        {
                            Lib.saveToLogGrinding(txtLogProcess, "START");
                            IsFist_Process = 2;
                        }

                        Lib.saveToLogGrinding(txtLogProcess, "1. Nhận tín hiệu trigger chu trình mài [D106.0] ON");
                        // TODO: 191125 - NB
                        if (chkResizeTarget.Checked)
                            Lib.UpdateTextboxReadOnly(txtDistance_Target_Milimet, true);
                        Lib.UpdateTextboxReadOnly(txtAllowableDeviation_Milimet, true);
                        Lib.UpdateTextboxReadOnly(txtCutting_Value, true);
                        Lib.UpdateNumericReadOnly(num_Inspect, true);
                        show_progress = 2;
                        IsShowLiveCamera = isReady_Grinding = true;

                        SetBitsOff("D206.0"); //ready
                        SetBitsOn("D206.1"); //busy
                        Lib.saveToLogGrinding(txtLogProcess, "2. Reset bit ready [D206.0] OFF, [D206.1] ON");

                        try
                        {
                            Lib.saveToLogGrinding(txtLogProcess, "3. Tính toán kết quả bằng hàm LineFind");
                            count_GrindingAuto++;

                            #region LineFindRun - Tính toán lại giá trị đá mài cần đi lên để gửi PLC
                            // TODO: 060825 - NB
                            if (sw_Grinding != null) sw_Grinding.Restart();
                            sw_Grinding.Start();

                            // TODO: 060825 - NB
                            //LineFindRun();
                            LineFindRun_Grinding();

                            // TODO: 060825 - NB
                            sw_Grinding.Stop();
                            Lib.saveToLogGrinding(txtLogProcess, $"3.1. Cycletime LineFindRun = [{sw_Grinding.ElapsedMilliseconds}]!");

                            if (_distanceRequest == 0)
                                _distanceRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text.Trim());
                            distanceMoveNew = (_distanceGrinding - _distanceRequest);

                            #region So sánh đường đi lên thực tế và gửi xuống PLC
                            // Lấy dữ liệu fix cứng trong code
                            if (!chkGrindLayer.Checked)
                            {
                                if (distanceMoveNew < 8) _height_Frinding_Diamond_Real = 1;
                                else if (distanceMoveNew < 15) _height_Frinding_Diamond_Real = 3;
                                else if (distanceMoveNew < 20) _height_Frinding_Diamond_Real = 4;
                                else if (distanceMoveNew <= 30) _height_Frinding_Diamond_Real = 5;
                                else _height_Frinding_Diamond_Real = 7;
                            }
                            else if (count_GrindingAuto <= 2)
                            {
                                if (_height_Frinding_Diamond_Real < 0 || _height_Frinding_Diamond_Max < 0) _height_Frinding_Diamond_SendPLC = 1;
                                else if (_height_Frinding_Diamond_Real >= _height_Frinding_Diamond_Max) _height_Frinding_Diamond_SendPLC = 1;
                                else _height_Frinding_Diamond_SendPLC = _height_Frinding_Diamond_Real;
                            }
                            else
                            {
                                try
                                {
                                    if (distanceMoveNew > 30.0)
                                        _height_Frinding_Diamond_Real = Lib.ToInt(_config_data.GreaterHeight);
                                    if (distanceMoveNew <= 30.0 && distanceMoveNew >= 10.0)
                                        _height_Frinding_Diamond_Real = Lib.ToInt(_config_data.AverageHeight);
                                    if (distanceMoveNew < 10.0)
                                        _height_Frinding_Diamond_Real = Lib.ToInt(_config_data.LessHeight);
                                }
                                catch (Exception ex3)
                                {
                                    Lib.saveToErr(ex3.ToString());
                                    _height_Frinding_Diamond_Real = 1;
                                }
                            }

                            //Thêm ràng buộc tránh việc giá trị thực tế văng vào trục pin
                            if (_height_Frinding_Diamond_Real < 0 || _height_Frinding_Diamond_Max < 0 || _height_Frinding_Diamond_Real >= _height_Frinding_Diamond_Max)
                                _height_Frinding_Diamond_SendPLC = 1;
                            else
                                _height_Frinding_Diamond_SendPLC = _height_Frinding_Diamond_Real;
                            Lib.saveToLogGrinding(txtLogProcess, $"4. Khoảng cách đi lên của đá _height_Frinding_Diamond_SendPLC = {_height_Frinding_Diamond_SendPLC}!");
                            #endregion

                            #endregion

                            #region HIỂN THỊ ĐƯỜNG KÍNH ĐÁ HIỆN TẠI VÀ GỬI DỮ LIỆU XUỐNG PLC
                            Lib.saveToLogGrinding(txtLogProcess, $"5. Gửi giá trị đá mài đi lên cho PLC : {_height_Frinding_Diamond_SendPLC}");
                            cDeviceManager._plcMC.SetSingleRegister_Int16("D208", _height_Frinding_Diamond_SendPLC);
                            cDeviceManager._plcMC.SetSingleRegister_Int16("D7016", _height_Frinding_Diamond_SendPLC);

                            cDeviceManager._plcMC.GetSingleRegister_Int32("SD5580", out int valueHeight_Real);
                            SmartWindow.ClearDispText();
                            DataDispText displayText = new DataDispText();
                            displayText.TextView = "Current Dia: " + (_distanceGrinding / 1000).ToString("F3")
                                + "\r\n" + "Target Dia: " + Lib.ToDouble(_distanceRequest / 1000).ToString("F3")
                                + "\r\n" + "Cutting Remain: " + (distanceMoveNew / 1000).ToString("F3")
                                + $" ({Math.Round(_distanceRequest / _distanceGrinding * 100, 2)}%)"
                                + "\r\n" + $"Wheel Pos: {valueHeight_Real + _height_Frinding_Diamond_Real}";
                            displayText.InPoint = new Point(100, 1000);
                            displayText.InSize = 200;
                            displayText.InColor = "Red";
                            displayText.Key = Guid.NewGuid();
                            SmartWindow.DispText(displayText);

                            if (count_GrindingAuto <= 2)
                            {
                                displayText = new DataDispText();
                                displayText.TextView = "Height Frinding Real: " + _height_Frinding_Diamond_Real
                                    + "\r\nHeight Frinding Max: " + _height_Frinding_Diamond_Max
                                    + "\r\nHeight Frinding -> PLC: " + _height_Frinding_Diamond_SendPLC;
                                displayText.InPoint = new Point(100, 3300);
                                displayText.InSize = 180;
                                displayText.InColor = "Green";
                                displayText.Key = Guid.NewGuid();
                                SmartWindow.DispText(displayText);
                            }
                            else
                            {
                                displayText = new DataDispText();
                                displayText.TextView = "Height Frinding -> PLC: " + _height_Frinding_Diamond_SendPLC;
                                displayText.InPoint = new Point(100, 3300);
                                displayText.InSize = 180;
                                displayText.InColor = "Green";
                                displayText.Key = Guid.NewGuid();
                                SmartWindow.DispText(displayText);
                            }

                            double result = 0;
                            if (distanceMove_Preview <= 0 || distanceMove_Preview > 1) result = 0.003;
                            else result = distanceMove_Preview;
                            if ((distanceMoveNew / 1000) > result)
                            {
                                SetBitsOn("D206.3"); //NG
                                Lib.saveToLogGrinding(txtLogProcess, $"6, Tính toán giá trị cần mài còn lại {distanceMoveNew.ToString("F4")} : NG");
                            }
                            else if ((distanceMoveNew / 1000) <= result)
                            {
                                SetBitsOn("D206.2"); //OK
                                Lib.saveToLogGrinding(txtLogProcess, $"6. Tính toán giá trị cần mài còn lại {distanceMoveNew.ToString("F4")} : OK");
                            }
                            Lib.saveToLogGrinding(txtLogProcess, $"7. Tính toán giá trị cần mài còn lại hoàn thành");
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Lib.saveToErr(ex.ToString());
                        }
                        IsShowLiveCamera = false;
                        Lib.saveToLogGrinding(txtLogProcess, $"8. Kiểm tra giá trị bit Confirm PLC D106.1 ON?");
                    }
                    #endregion

                    #region Kiểm tra mài thành công
                    bool isPlcConfirm = GetBits("D106.1");
                    if (isPlcConfirm)
                    {
                        SetBitsOn("D206.0"); //ready
                        SetBitsOff("D206.1"); //busy
                        SetBitsOff("D206.2"); //OK
                        SetBitsOff("D206.3"); //NG
                        Lib.saveToLogGrinding(txtLogProcess, $"9. Hoàn thành mài, chờ lần mài tiếp theo");
                    }
                    #endregion

                    #region Xác nhận mài thành công
                    bool isGrindSuccess = GetBits("M3021");
                    if (isGrindSuccess)
                    {
                        Lib.saveToLogGrinding(txtLogProcess, $"10. Xác nhận hoàn thành chu trình mài");
                        SetBitsOff("M3021");
                        if (distanceMoveNew <= 3)
                        {
                            count_GrindingAuto = 0;
                            isReady_Grinding = false;
                            Lib.UpdateButtonBackColor(btnGrindAutoState, Color.Green);
                            lst_DistanceGrinding.Clear();
                            lst_CrowHeight = new List<double>();
                            lst_TotalDepth = new List<double>();

                            show_progress = 2.5;
                            step_progress = 3;
                        }

                        // TODO: 060825 - NB
                        sw_Grinding_Total.Stop();
                        Lib.saveToLogGrinding(txtLogProcess, $"11. Cycletime hoàn thành = [{sw_Grinding_Total.ElapsedMilliseconds}]!");
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Lib.saveToLog(ex.ToString());
                }
            }
        }
        #endregion

        #region STEP 3: INSPECT PHÂN LOẠI 

        #region Khai báo
        bool isReady_Inspect = false;
        List<double> lst_DistanceGrinding = new List<double>();
        List<double> lst_CrowHeight = new List<double>();
        List<double> lst_TotalDepth = new List<double>();
        // TODO: 060825 - NB
        Stopwatch sw_Inspect_Total = new Stopwatch();
        Stopwatch sw_Inspect = new Stopwatch();
        #endregion

        private void InspectFunction()
        {
            int checkInversionTimes = Lib.ToInt(num_Inspect.Text);
            int countCheckInversion = 0;
            SetBitsOn("D210.0");
            isReady_Inspect = false;
            while (IsRun)
            {
                Thread.Sleep(50);
                if (!cDeviceManager._plcMC.Connected || !IsModeOption || !IsStartProcess) continue;

                if (!isReady_Inspect && !GetBits("D210.0")) SetBitsOn("D210.0"); //Ready
                try
                {
                    #region TÍN HIỆU TRIGGER
                    if (GetBits("D110.0"))
                    {
                        if (sw_Inspect_Total != null && !sw_Inspect_Total.IsRunning)
                        {
                            sw_Inspect_Total.Restart();
                            sw_Inspect_Total.Start();
                        }

                        if (IsFist_Process == 2)
                        {
                            Lib.saveToLogInspect(txtLogProcess, "START");
                            IsFist_Process = 3;
                        }
                        Lib.saveToLogInspect(txtLogProcess, "1.Kiểm tra tín hiệu trigger [D110.0] ON");

                        step_progress = 3;
                        show_progress = 3;

                        IsShowLiveCamera = isReady_Inspect = true;
                        SetBitsOff("D210.0"); //Ready
                        SetBitsOn("D210.1"); //Busy
                        Lib.saveToLogInspect(txtLogProcess, "2. SET [D210.0] OFF, [D210.1] ON");

                        try
                        {
                            // TÍNH TOÁN ĐỘ ĐẢO, TOTAL DEPTH VÀ CROW HEIGHT
                            Lib.saveToLogInspect(txtLogProcess, "3.Tính toán độ đảo, total depth và crow height");

                            if (sw_Inspect != null) sw_Inspect.Restart();
                            sw_Inspect.Start();

                            InversionFunction_DistanceMeasureRun();

                            sw_Inspect.Stop();
                            Lib.saveToLogInspect(txtLogProcess, $"Cycletime: {sw_Inspect.ElapsedMilliseconds}");

                            #region Hiển thị Inversion
                            SmartWindow.ClearDispText();
                            DataDispText displayText = new DataDispText();
                            displayText.TextView = "Diameter: " + lst_DistanceGrinding[lst_DistanceGrinding.Count - 1].ToString("F3");
                            displayText.InPoint = new Point(100, 1200);
                            displayText.InSize = 200;
                            displayText.InColor = "Red";
                            displayText.Key = Guid.NewGuid();
                            SmartWindow.DispText(displayText);
                            #endregion

                            Lib.saveToLogInspect(txtLogProcess, "4.Hoàn thành tính toán, hiển thị các thông số");

                            SetBitsOn("D210.2"); //OK
                            Lib.saveToLogInspect(txtLogProcess, "5. On bit D210 xác nhận OKE");
                        }
                        catch (Exception ex)
                        {
                            Lib.saveToErr(ex.ToString());
                        }

                        IsShowLiveCamera = false;
                        Lib.saveToLogInspect(txtLogProcess, "6. Kiểm tra bit Confirm từ PLC D110.1");
                    }
                    #endregion

                    #region CONFIRM TỪ PLC
                    bool isConfirm = GetBits("D110.1");
                    if (isConfirm)
                    {
                        Lib.saveToLogInspect(txtLogProcess, "7. Xác nhận bit Confirm ON thành công");
                        isReady_Inspect = false;
                        SetBitsOn("D210.0"); //Ready
                        SetBitsOff("D210.1"); //Busy
                        SetBitsOff("D210.2"); //OK
                        SetBitsOff("D210.3"); //NG
                        countCheckInversion++;

                        checkInversionTimes = Lib.ToInt(num_Inspect.Value);
                        Lib.saveToLogInspect(txtLogProcess, $"8. Phân loại lần thứ {countCheckInversion}");

                        if (countCheckInversion == checkInversionTimes)
                        {
                            // TODO: 060825 - NB
                            sw_Inspect_Total.Stop();
                            Lib.saveToLogInspect(txtLogProcess, $"9. Phân loại đủ {checkInversionTimes} lần. Hiển thị thông số khi hoàn thành" +
                                $"\r\nCycltetime{sw_Inspect_Total.ElapsedMilliseconds}");

                            countCheckInversion = 0;
                            show_progress = -1;
                            step_progress = 1;
                            Lib.UpdateButtonBackColor(btnInspectState, Color.Green);

                            #region Hiển thị thông số sau khi hoàn thành, tính toán Crow Height và TotalDept
                            // TODO: 010726 - NB
                            double _ovalMax = lst_DistanceGrinding.Max();
                            double _ovalMin = lst_DistanceGrinding.Min();
                            double _oval_Average = (_ovalMax - _ovalMin);
                            double _average = lst_DistanceGrinding.Sum() / lst_DistanceGrinding.Count;

                            // tính theo %:  chuyển sang mm và chia cho đường kính thực tế 
                            double ratio_CrowHeight = 0;
                            double ratio_TotalDepth_Total = 0;

                            // TODO: 020726 - NB
                            if (!Is_Semi_Bypass_Inspect)
                            {
                                if (chkIntersect.Checked)
                                {
                                    Lib.saveToLogInspect(txtLogProcess, $"10. Tính toán Crow height và total Depth");
                                    LineFindRun_Crow_TotalDept();
                                    PointF inter = FindIntersection((Line_Cross_Diamond_Up[1], Line_Cross_Diamond_Up[0]), (Line_Cross_Diamond_Up[3], Line_Cross_Diamond_Up[2]), (Line_Cross_Diamond_Down[1], Line_Cross_Diamond_Down[0]), (Line_Cross_Diamond_Down[3], Line_Cross_Diamond_Down[2]));
                                    Line_Cross_Diamond_Intersect_1 = new List<double>() { inter.Y, inter.X, Line_Cross_Diamond_Up[2], Line_Cross_Diamond_Up[3] };
                                    DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_1, DisplayImage, Color.Blue);
                                    Line_Cross_Diamond_Intersect_2 = new List<double>() { inter.Y, inter.X, Line_Cross_Diamond_Down[0], Line_Cross_Diamond_Down[1] };
                                    DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_2, DisplayImage, Color.Blue);
                                }

                                ratio_CrowHeight = (lst_CrowHeight.Sum() / lst_CrowHeight.Count) / 1000 / _average;
                                ratio_TotalDepth_Total = (lst_TotalDepth.Sum() / lst_TotalDepth.Count) / 1000 / _average;
                            }
                            Lib.saveToLogInspect(txtLogProcess, $"11. Hiển thị Crow height và total Depth theo phần trăm");

                            #region Hiển thị thông số màn hình
                            SmartWindow.ClearDispText();
                            DataDispText displayText = new DataDispText();
                            displayText.TextView = "Roundness: " + _oval_Average.ToString("F3") + "\r\n" +
                            "Max: " + _ovalMax.ToString("F3") + ";" +
                            "Min: " + _ovalMin.ToString("F3") + "\r\n" +
                            "Average: " + _average.ToString("F3") + "\r\n" +
                            "Crow Height: " + Math.Round((ratio_CrowHeight * 100), 1).ToString("F1") + " (%)" + "\r\n" +
                            "Total Depth: " + Math.Round((ratio_TotalDepth_Total * 100), 1).ToString("F1") + " (%)";
                            displayText.InPoint = new Point(100, 1000);
                            displayText.InSize = 200;
                            displayText.InColor = "Red";
                            displayText.Key = Guid.NewGuid();
                            SmartWindow.DispText(displayText);

                            // TODO: 060826 - NB
                            DataDispText displayText_R = new DataDispText();
                            displayText_R.TextView = Math.Round(_average, 2).ToString("F2");
                            displayText_R.InPoint = new Point(4400, 500);
                            displayText_R.InSize = 200;
                            displayText_R.InColor = "White";
                            displayText_R.Key = Guid.NewGuid();
                            SmartWindow.DispText(displayText_R);
                            Lib.UpdateLabelText(lblValue_Average, Math.Round(_average, 2).ToString("F2"));
                            #endregion

                            if (!Is_Semi_Bypass_Inspect)
                            {
                                DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_2, DisplayImage, Color.Blue);
                                DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_1, DisplayImage, Color.Blue);
                            }

                            startProcess = false;
                            Lib.saveToLogInspect(txtLogProcess, $"12. Hiển thị thông số lên màn hình");

                            if (Is_Bypass_Concentric)
                            {
                                Is_Bypass_Concentric = false;
                                Lib.UpdateButton_BColor_FColor(btnBypass_Concentric, Color.White, Color.Black);
                            }
                            #endregion

                            #region CHUYỂN ĐÈN FRONT LIGHT
                            try
                            {
                                if (!Is_OpenFrontLight)
                                {
                                    WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Front Light");
                                    wait.Show();
                                    Open_FrontLight();
                                    Is_OpenFrontLight = true;
                                    wait.Close();
                                }
                            }
                            catch (Exception)
                            {
                            }
                            #endregion

                            SetBitsOn("D216.0");

                            #region SAVE DB
                            try
                            {
                                // Cycletime búa gõ
                                cDeviceManager._plcMC.GetSingleRegister_Int16("D4000", out int cycleConcentric);
                                // Cycletime mài
                                cDeviceManager._plcMC.GetSingleRegister_Int16("D4100", out int cycleGrinding);

                                history_data _history_data = new history_data();
                                _history_data.DateTimeStart = dateTime_Start;
                                _history_data.DiamondCode = txtDiamondCode.Text.Trim();
                                _history_data.DistanceOriginalMin = Lib.ToDecimal(DistanceOriginalMin);
                                _history_data.DistanceOriginalMax = Lib.ToDecimal(DistanceOriginalMax);
                                _history_data.DistanceGrindingRequest = Lib.ToDecimal(txtDistance_Target_Milimet.Text.Trim());
                                _history_data.DistanceGrindingReal = Lib.ToDecimal(_distanceGrinding / 1000);
                                _history_data.CycleConcentric = Lib.ToString(cycleConcentric / 10);
                                _history_data.CycleGrinding = Lib.ToString(cycleGrinding / 10);
                                _history_data.OvalMax = Lib.ToDecimal(_ovalMax.ToString("F3"));
                                _history_data.OvalMin = Lib.ToDecimal(_ovalMin.ToString("F3"));
                                _history_data.OvalAverage = Lib.ToDecimal(_oval_Average.ToString("F3"));
                                _history_data.CrowHeight = Math.Round((ratio_CrowHeight * 100), 1).ToString("F1");
                                _history_data.TotalDepth = Math.Round((ratio_TotalDepth_Total * 100), 1).ToString("F1");
                                _history_data.DateTimeEnd = DateTime.Now;
                                _history_data.DiameterAverage = Lib.ToDecimal(_average.ToString("F3"));
                                if (chkResizeTarget.Checked)
                                {
                                    _history_data.Cutting_Value = Lib.ToDecimal(txtMax.Text) - Lib.ToDecimal(txtDistance_Target_Milimet.Text);
                                    _history_data.IsResize_Target = "Resize Target";
                                }
                                else
                                {
                                    _history_data.Cutting_Value = Lib.ToDecimal(txtCutting_Value.Text);
                                    _history_data.IsResize_Target = "Null";
                                }
                                _mySQLHelper.Insert<history_data>(_history_data);

                                Lib.saveToLogInspect(txtLogProcess, $"END - 13. Lưu kết quả vào DB");
                            }
                            catch (Exception ex)
                            {
                                Lib.saveToErr(ex.ToString());
                            }
                            #endregion

                            #region Chuyển mode
                            lst_DistanceGrinding.Clear();
                            if (modeRunning == 4)
                            {
                                // TODO: 140726 - NB
                                Reload_FullRois();
                            }
                            else
                            {
                                // Chỉ mode auto mới sử dụng
                                modeRunning = 1;
                                ShowButtonRunning(modeRunning);
                            }
                            #endregion

                            SetBitsOff("D216.0");
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Lib.saveToErr(ex.ToString());
                }
            }
        }
        #endregion

        #region THREAD MÀI MANUAL
        public static object lockImage_Snap = new object();
        private void MeasureFunctionManual()
        {
            Lib.UpdateButtonBackColor(btnGrindAutoState, Color.Blue);
            double _distance = 0;
            double distanceMove = _distance - _distanceRequest;
            //Lib.AddTextBoxText(txtProcess, "10. Check trigger 1" + Environment.NewLine);
            //ready
            SetBitsOn("D206.0");
            bool isReady = false;
            while (true)
            {
                Thread.Sleep(50);
                if (!IsRun) return;

                //kiểm tra có phải chế độ manual hay không
                if (!_isManual || IsModeOption || !IsStartProcess) continue;
                if (!isReady && !GetBits("D206.0")) SetBitsOn("D206.0"); //ON Bit Ready

                try
                {
                    if (!IsLive)
                    {
                        SetLiveCamera_Hikrobot(true);
                        IsLive = true;
                    }
                    //continue; //thêm vào để test
                    isReady = true;
                    // nhận tín hiệu trigger1 từ PLC
                    bool dataTrigger1 = GetBits("D7005.3");
                    if (dataTrigger1)
                    {
                        //ready
                        SetBitsOff("D7005.3");

                        show_progress = 4;
                        if (!IsLive)
                        {
                            SetLiveCamera_Hikrobot(true);
                            IsLive = true;
                        }
                        Bitmap bitmap_FunctionManual = cDeviceManager.HikCamera.SingleShotGrabbing();
                        lock (lockImage_Snap)
                        {
                            if (SmartWindow.InvokeRequired)
                            {
                                SmartWindow.Invoke(new Action(() =>
                                {
                                    // TODO: 240425
                                    //SmartWindow.Image = bitmap_FunctionManual;
                                    InputImage = bitmap_FunctionManual;
                                }));
                            }
                            else
                            {
                                // TODO: 240425
                                //SmartWindow.Image = bitmap_FunctionManual;
                                InputImage = bitmap_FunctionManual;
                            }
                        }
                        if (SmartWindow.Image == null) continue;
                        //ready
                        SetBitsOff("D206.0");
                        //busy
                        SetBitsOn("D206.1");

                        // run tool line find và distance measure
                        // TODO: 060825 - NB 
                        //LineFindRun();
                        LineFindRun_Grinding();

                        //Hiển thị đường kính lên 
                        SmartWindow.ClearDispText();
                        DataDispText displayText = new DataDispText();
                        displayText.TextView = "Diameter: " + _distanceGrinding.ToString("F3");
                        displayText.InPoint = new Point(100, 1200);
                        displayText.InSize = 200;
                        displayText.InColor = "Red";
                        displayText.Key = Guid.NewGuid();
                        SmartWindow.DispText(displayText);

                        SetBitsOn("D7005.3");
                    }

                    bool isPlcConfirm = GetBits("D106.1");
                    if (isPlcConfirm)
                    {
                        SetBitsOn("D206.0"); //ready
                        SetBitsOff("D206.1"); //busy
                        SetBitsOff("D206.3"); //NG
                        SetBitsOff("D206.2"); //OK
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        #endregion

        #endregion

        #region XỬ LÝ VỀ VISION

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

        #region SNAP IMAGE HIKROBOT NEW
        private bool SetLiveCamera_Hikrobot(bool isLive = true)
        {
            try
            {
                if (cDeviceManager.HikCamera == null || cDeviceManager.HikCamera.GetCameraHandle() == IntPtr.Zero)
                    return false;
                if (isLive)
                {
                    cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
                    StartLiveBackgroundWorker(BackgroundWorkerLiveWebCam_Hikrobot_DoWork, LiveBackgroundWorkerCompleted);
                    SmartWindow_DoubleClick(null, null);
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

        private Bitmap SnapImage_Camera_Hikrobot()
        {
            if (cDeviceManager.HikCamera == null) return null;
            if (IsLive) SetLiveCamera_Hikrobot(false);
            var bitmap = cDeviceManager.HikCamera.SingleShotGrabbing();
            if (bitmap != null)
            {
                if (SmartWindow.InvokeRequired)
                {
                    SmartWindow.Invoke(new Action(() =>
                    {
                        // TODO: 240425
                        //SmartWindow.Image = bitmap;
                        InputImage = bitmap;
                    }));
                }
                else
                {
                    // TODO: 240425
                    //SmartWindow.Image = bitmap;
                    InputImage = bitmap;
                }
            }
            return bitmap;
        }
        #endregion

        #region TOOL LINE FIND RUN

        #region Khai báo 
        SetOrigin.SetOrigin SetOrigin = new SetOrigin.SetOrigin();
        LineFindTool.LineFind LineFind = new LineFindTool.LineFind();
        public Dictionary<Guid, cROIProperty> ROIProperties_Parameters = new Dictionary<Guid, cROIProperty>();
        public static Image<Bgr, byte> DisplayImage = null;
        private Image<Gray, byte> GrayImage = null;
        private Image InputImage = null;
        double offsetGrind = 0;
        double valueConvert = 1.6;
        double distanceMove_Preview = 0;
        int grinding_L_Position = 0;
        int grinding_R_Position = 0;
        bool Is_Calculate_TotalDepth_TAB = false;
        bool IsResult_LineFind = false;
        #endregion

        #region LineFindRun Concentric
        // TODO: 060825 - NB
        private void LineFindRun_Concentric()
        {
            try
            {
                #region Snap image
                // TODO: 010125 - NB
                if (bitmap_Live != null)
                {
                    if (SmartWindow.InvokeRequired) SmartWindow.Invoke(new Action(() => { InputImage = bitmap_Live; }));
                    else InputImage = bitmap_Live;
                }

                if (InputImage == null) return;
                Bitmap bitmapImage = (Bitmap)InputImage.Clone();
                if (step_progress <= 1) SmartWindow_DoubleClick(null, null);
                GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)bitmapImage.Clone());
                DisplayImage = GrayImage.Convert<Bgr, byte>();
                if (DisplayImage == null) return;
                Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
                SetOrigin = new SetOrigin.SetOrigin();
                #endregion

                #region LẤY RA THÔNG TIN LINE DIMAOND VÀ LINE TRỤC BÊN PHẢI / TRÁI
                List<RTCRectangle> ROIs = new List<RTCRectangle>();
                List<RTCRectangle> _ROIs_Stone = new List<RTCRectangle>();
                int count = 0;
                RTCRectangle Data = new RTCRectangle();
                var roiList = this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 || x.ActionID == 2);
                for (int i = 0; i < roiList.Count; i++)
                {
                    // TODO: 230925 - NB
                    int actionID = roiList[i].ActionID;
                    int descCode = roiList[i].DescriptionCode;
                    LineFind = new LineFind();

                    #region Hàm tính LineFind
                    cDrawingBaseType ROIB = (cDrawingBaseType)roiList[i];
                    Guid OldID = ROIB.ID;
                    switch (ROIB.DrawingType)
                    {
                        case EDrawingtypes.Rectang:
                            {
                                cRectangType Rec = (cRectangType)roiList[i];
                                Data = new RTCRectangle();
                                Data.Phi = (float)Rec.Phi * Math.PI / 180;
                                Data.Width = (float)Rec.Width * 2;
                                Data.Height = (float)Rec.Height * 2;
                                Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                                ROIs.Add(Data);
                                break;
                            }
                    }
                    // Giá trị input tool LineFind
                    LineFind.ROI = Data;
                    LineFind.ToolOrigin = toolOrigin;
                    LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                    LineFind.InputImage = GrayImage;
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;

                    var x = LineFind.LineSegment;
                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        SetBitsOn("D214.0");
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }
                    // Giá trị output tool
                    string err = LineFind.ErrMessage;
                    #endregion

                    // LINE TRỤC BÊN PHẢI 
                    if (LineFind.LineSegment != null && actionID == 1)
                    {
                        // TODO: 230925 - NB
                        switch (descCode)
                        {
                            case 1:
                                Line_Diamond_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Bottom, DisplayImage, Color.LimeGreen);
                                break;
                            case 2:
                                Line_Diamond_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Top, DisplayImage, Color.LimeGreen);
                                break;
                            case 3:
                                Line_Shaft_Right_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            case 4:
                                Line_Shaft_Right_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            default:
                                break;
                        }
                    }
                    // LINE TRỤC BÊN TRÁI
                    else if (LineFind.LineSegment != null && actionID == 2)
                    {
                        // TODO: 230925 - NB
                        switch (descCode)
                        {
                            case 1:
                                Line_Shaft_Left_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            case 2:
                                Line_Shaft_Left_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region LINE ĐƯỜNG KÍNH KIM CƯƠNG
                Line_Diamond_Center = new List<double>()
                {
                     Line_Diamond_Bottom[2],
                     (Line_Diamond_Bottom[1] + Line_Diamond_Top[1])/2,
                     Line_Diamond_Top[2],
                     (Line_Diamond_Bottom[3] + Line_Diamond_Top[3])/2
                };
                DisplayGraphics(SmartWindow, Line_Diamond_Center, DisplayImage, Color.Yellow);
                #endregion

                #region Chế độ Auto + Semi Normal (còn Semi bypass không sử dụng được)
                if (!Is_Semi_Bypass_Inspect) // Đối với kim cương thuận chiều rois
                {
                    #region LINE TÂM TRỤC XOAY
                    if (_register_root_axis != null)
                    {
                        Line_Shaft_Right_Center = new List<double>()
                        {
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisX1),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisY1),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisX2),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisY2)
                        };
                        if (!get_Diameter_Manual && step_progress == 1)
                            DisplayGraphics(SmartWindow, Line_Shaft_Right_Center, DisplayImage, Color.LimeGreen);
                    }
                    #endregion

                    #region LINE TÂM TRỤC BÊN TRÁI
                    if (Line_Shaft_Left_Top != null && Line_Shaft_Left_Bottom != null)
                    {
                        Line_Shaft_Left_Center = new List<double>()
                        {
                         Line_Shaft_Left_Top[2],
                         (Line_Shaft_Left_Top[1] + Line_Shaft_Left_Bottom[1])/2,  // Y1 trung bình
                         Line_Shaft_Left_Bottom[2],
                         (Line_Shaft_Left_Top[3] + Line_Shaft_Left_Bottom[3])/2   // Y2 trung bình
                        };
                        if (!get_Diameter_Manual && step_progress == 1)
                            DisplayGraphics(SmartWindow, Line_Shaft_Left_Center, DisplayImage, Color.LimeGreen);
                    }
                    #endregion

                    #region LẤY 3 ĐIỂM NẰM TRÊN ĐƯỜNG TÂM KIM CƯỜNG + TÂM TRỤC XOAY + TÂM TRỤC BÊN TRÁI
                    float point1_X = (float)(Line_Diamond_Top[1] + Line_Diamond_Top[3]) / 2;
                    float point1_Y = (float)(Line_Diamond_Bottom[1] + Line_Diamond_Bottom[3]) / 2;
                    point1 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(point1_X + point1_Y) / 2);
                    // Điểm nằm trên đường thẳng tâm trục xoay bên phải
                    float point2_X = (float)(Line_Shaft_Right_Top[1] + Line_Shaft_Right_Top[3]) / 2;
                    float point2_Y = (float)(Line_Shaft_Right_Bottom[1] + Line_Shaft_Right_Bottom[3]) / 2;
                    point2 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(point2_X + point2_Y) / 2);

                    //Điểm nằm trên đường thẳng tâm trục bên trái
                    point6 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(Line_Shaft_Left_Top[3] + Line_Shaft_Left_Bottom[3]) / 2);
                    #endregion
                }
                #endregion

                #region TÍNH GÓC GIỮA KIM CƯƠNG VÀ TRỤC
                if (get_Diameter_Manual)
                {
                    _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                    return;
                }

                // TÍNH ĐỘ LỆCH CỦA 2 TRỤC BÊN PHẢI
                _deviationConcentric = Math.Abs(CalcLineToLine(bitmapImage, Line_Diamond_Center, Line_Shaft_Right_Center, false) * PIXEL_TO_MM);
                if (point1.Y < point2.Y) _deviationConcentric = -_deviationConcentric;

                // TÍNH ĐỘ LỆCH CỦA 2 TRỤC BÊN TRÁI
                _deviationBetweenTwoShaft = Math.Abs(CalcLineToLine(bitmapImage, Line_Shaft_Left_Center, Line_Shaft_Right_Center, false, false) * PIXEL_TO_MM);
                if (point1.Y < point6.Y) _deviationBetweenTwoShaft = -_deviationBetweenTwoShaft;

                // TÍNH GÓC GIỮA KIM CƯƠNG VÀ TRỤC
                _valueAngle = CalcLineToLine_AngleMeasurement(bitmapImage, Line_Diamond_Center, Line_Shaft_Right_Center, false);
                #endregion
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region LineFindRun Grinding
        // TODO: 060825 - NB
        private void LineFindRun_Grinding(bool isCalculater_height_Frinding = false)
        {
            try
            {
                #region Snap image
                if (bitmap_Live != null)
                {
                    if (SmartWindow.InvokeRequired) SmartWindow.Invoke(new Action(() => { InputImage = bitmap_Live; }));
                    else InputImage = bitmap_Live;
                }
                if (InputImage == null) return;
                Bitmap bitmapImage = (Bitmap)InputImage.Clone();
                GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)bitmapImage.Clone());
                DisplayImage = GrayImage.Convert<Bgr, byte>();
                if (DisplayImage == null) return;
                Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
                SetOrigin = new SetOrigin.SetOrigin();
                #endregion

                #region LẤY RA THÔNG TIN LINE DIMAOND
                List<RTCRectangle> ROIs = new List<RTCRectangle>();
                List<RTCRectangle> _ROIs_Stone = new List<RTCRectangle>();
                RTCRectangle Data = new RTCRectangle();

                var roiList = this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 && x.DescriptionCode == 1 || x.ActionID == 1 && x.DescriptionCode == 2);
                for (int i = 0; i < roiList.Count; i++)
                {
                    int actionID = roiList[i].ActionID;
                    int descCode = roiList[i].DescriptionCode;
                    LineFind = new LineFind();

                    #region HÀM TÍNH LineFind
                    cDrawingBaseType ROIB = (cDrawingBaseType)roiList[i];
                    Guid OldID = ROIB.ID;
                    switch (ROIB.DrawingType)
                    {
                        case EDrawingtypes.Rectang:
                            {
                                cRectangType Rec = (cRectangType)roiList[i];
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
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;

                    var x = LineFind.LineSegment;
                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        SetBitsOn("D214.0");
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }
                    #endregion

                    #region LẤY RA LINE TRÊN / DƯỚI KIM CƯƠNG 
                    if (LineFind.LineSegment == null) return;

                    // TODO: 230925 - NB
                    switch (descCode)
                    {
                        case 1:
                            Line_Diamond_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_Diamond_Bottom, DisplayImage, Color.LimeGreen);
                            break;
                        case 2:
                            Line_Diamond_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_Diamond_Top, DisplayImage, Color.LimeGreen);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                #endregion

                #region LINE ĐƯỜNG KÍNH KIM CƯƠNG
                Line_Diamond_Center = new List<double>()
                {
                     Line_Diamond_Bottom[2],
                     (Line_Diamond_Bottom[1] + Line_Diamond_Top[1])/2,
                     Line_Diamond_Top[2],
                     (Line_Diamond_Bottom[3] + Line_Diamond_Top[3])/2
                };
                #endregion

                #region TÍNH ĐƯỜNG KÍNH KIM CƯƠNG
                if (get_Diameter_Manual)
                {
                    _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                    return;
                }
                else _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                #endregion

                #region TÍNH KHOẢNG CÁCH TỪ MÉP ĐÁ ĐẾN MÉP DƯỚI KIM CƯƠNG
                if (isCalculater_height_Frinding) return;
                foreach (object ROI in ROIS_Stone_TotalDept.Values.ToList().FindAll(x => x.ActionID == 11 && x.DescriptionCode == 5))
                {
                    LineFind = new LineFind();

                    #region HÀM TÍNH LineFind
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
                                // TODO: 230925 - NB
                                _ROIs_Stone.Add(Data);
                                break;
                            }
                    }
                    LineFind.ROI = Data;
                    LineFind.ToolOrigin = toolOrigin;
                    LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                    LineFind.InputImage = GrayImage;
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;

                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }
                    #endregion

                    if (LineFind.LineSegment != null)
                    {
                        #region MANUAL BYPASS PHÂN LOẠI 
                        if (modeRunning == 4)
                        {
                            var ROIB_Lowest = ROIS_LowestPoint_Diamond.Values.ToList().FindAll(x => x.ActionID == 12).FirstOrDefault();
                            if (ROIB_Lowest == null) return;

                            double offsetGrind_Max = 0;
                            if (step_progress == 2) offsetGrind_Max = offsetGrind;
                            else offsetGrind_Max = offsetGrind / 2;

                            // Mép trên đá mài
                            Line_GrindStone_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_GrindStone_Top, DisplayImage, Color.Yellow);

                            // Mép dưới kim cương
                            Line_Diamond_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Diamond_Bottom[1], LineFind.LineSegment.Item2.X, Line_Diamond_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Diamond_Bottom_Lengthen, DisplayImage, Color.Blue);

                            Line_Diamond_Lowest = new List<double>() { Line_GrindStone_Top[0], ROIB_Lowest.Center.Col - ROIB_Lowest.Height, Line_GrindStone_Top[2], ROIB_Lowest.Center.Col - ROIB_Lowest.Height };
                            DisplayGraphics(SmartWindow, Line_Diamond_Lowest, DisplayImage, Color.Orange);
                            double value = (CalcLineToLine(bitmapImage, Line_GrindStone_Top, Line_Diamond_Lowest, false) * PIXEL_TO_MM * valueConvert) - offsetGrind;
                            _height_Frinding_Diamond_Real = (int)Math.Ceiling(value);

                            Line_Shaft_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Shaft_Left_Bottom[1], LineFind.LineSegment.Item2.X, Line_Shaft_Left_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Shaft_Bottom_Lengthen, DisplayImage, Color.Yellow);

                            double valueMax = (CalcLineToLine(bitmapImage, Line_Shaft_Bottom_Lengthen, Line_GrindStone_Top, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Max = (int)Math.Ceiling(valueMax);
                            Lib.UpdateTextBoxText(txtLogProcess, "Offset Value : " + offsetGrind_Max.ToString() + "\r\nHeight Frinding Max : " + valueMax.ToString("F2") + "\r\nHeight Frinding Real : " + _height_Frinding_Diamond_Real.ToString("F2") + "\r\nConvert Value : " + valueConvert.ToString());

                            break;
                        }
                        #endregion

                        #region AUTO
                        else // AUTO
                        {
                            // TODO: 230925 - NB
                            Line_Diamond_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_Diamond_Bottom_Lengthen, DisplayImage, Color.Yellow);

                            Line_GrindStone_Top = new List<double>() { LineFind.LineSegment.Item1.X, Line_Diamond_Bottom[1], LineFind.LineSegment.Item2.X, Line_Diamond_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_GrindStone_Top, DisplayImage, Color.Blue);

                            // TODO: 230925 - NB
                            double offsetGrind_Max = 0;
                            if (step_progress == 2) offsetGrind_Max = offsetGrind;
                            else offsetGrind_Max = offsetGrind / 2;

                            double value = (CalcLineToLine(bitmapImage, Line_GrindStone_Top, Line_Diamond_Bottom_Lengthen, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Real = (int)Math.Ceiling(value);

                            Line_Shaft_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Shaft_Left_Bottom[1], LineFind.LineSegment.Item2.X, Line_Shaft_Left_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Shaft_Bottom_Lengthen, DisplayImage, Color.Orange);

                            double valueMax = (CalcLineToLine(bitmapImage, Line_Shaft_Bottom_Lengthen, Line_Diamond_Bottom_Lengthen, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Max = (int)Math.Ceiling(valueMax);
                            Lib.UpdateTextBoxText(txtLogProcess, "Offset Value : " + (offsetGrind).ToString() + " ; Height Value : " + valueMax.ToString("F2") + " ; Convert Value : " + valueConvert.ToString());

                            if (_height_Frinding_Diamond_Real > _height_Frinding_Diamond_Max && _height_Frinding_Diamond_Real > 0 && _height_Frinding_Diamond_Max > 0)
                            {
                                // TODO: 230925 - NB
                                string id = _mySQLHelper.FindByField<register_location_rois>("DescriptionCode", 5).IDGuid;
                                if (OldID != Guid.Parse(id))
                                    OldID = Guid.Parse(id);

                                string log = $"LineFind.ROI: [{Data}], ToolOrigin.ROI: [{toolOrigin}]";

                                string log1 = $"Line_Diamond_Bottom: IDGuid: [{id}] : [{LineFind.LineSegment.Item1.X}, {LineFind.LineSegment.Item1.Y},{LineFind.LineSegment.Item2.X}, {LineFind.LineSegment.Item2.Y}]";

                                string log2 = $"Line_GrindStone_Top: [{LineFind.LineSegment.Item1.X}, {Line_Diamond_Bottom[1]},{LineFind.LineSegment.Item2.X}, {Line_Diamond_Bottom[3]}]";

                                string log3 = $"Line_Shaft_Bottom: [{LineFind.LineSegment.Item1.X}, {Line_Shaft_Left_Bottom[1]},{LineFind.LineSegment.Item2.X}, {Line_Shaft_Left_Bottom[3]}]";

                                Lib.saveToLog_HeightDiamond($"{log} \r\n{log1} \r\n {log2} \r\n {log3}");
                            }
                            break;
                        }
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region LineFindRun_Grinding_InspectFunction
        // TODO: 060825 - NB
        private void LineFindRun_Grinding_InspectFunction()
        {
            try
            {
                #region Snap image
                if (bitmap_Live != null)
                {
                    if (SmartWindow.InvokeRequired) SmartWindow.Invoke(new Action(() => { InputImage = bitmap_Live; }));
                    else InputImage = bitmap_Live;
                }
                if (InputImage == null) return;
                Bitmap bitmapImage = (Bitmap)InputImage.Clone();
                GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)bitmapImage.Clone());
                DisplayImage = GrayImage.Convert<Bgr, byte>();
                if (DisplayImage == null) return;
                Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
                SetOrigin = new SetOrigin.SetOrigin();
                #endregion

                #region LẤY RA THÔNG TIN LINE DIMAOND
                List<RTCRectangle> ROIs = new List<RTCRectangle>();
                List<RTCRectangle> _ROIs_Stone = new List<RTCRectangle>();
                RTCRectangle Data = new RTCRectangle();
                var roiList = this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 && x.DescriptionCode == 1);
                for (int i = 0; i < roiList.Count; i++)
                {
                    // TODO: 230925 - NB
                    int actionID = roiList[i].ActionID;
                    int descCode = roiList[i].DescriptionCode;
                    LineFind = new LineFind();

                    #region HÀM TÍNH LineFind
                    cDrawingBaseType ROIB = (cDrawingBaseType)roiList[i];
                    Guid OldID = ROIB.ID;
                    switch (ROIB.DrawingType)
                    {
                        case EDrawingtypes.Rectang:
                            {
                                cRectangType Rec = (cRectangType)roiList[i];
                                Data = new RTCRectangle();
                                Data.Phi = (float)Rec.Phi * Math.PI / 180;
                                Data.Width = (float)Rec.Width * 2;
                                Data.Height = (float)Rec.Height * 2;
                                Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                                ROIs.Add(Data);
                                break;
                            }
                    }
                    // Giá trị input tool LineFind
                    LineFind.ROI = Data;
                    LineFind.ToolOrigin = toolOrigin;
                    LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                    LineFind.InputImage = GrayImage;
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;
                    var x = LineFind.LineSegment;
                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        SetBitsOn("D214.0");
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }
                    string err = LineFind.ErrMessage;
                    #endregion

                    #region LINE TRỤC BÊN PHẢI 
                    if (LineFind.LineSegment != null && actionID == 1)
                    {
                        // TODO: 230925 - NB
                        switch (descCode)
                        {
                            case 1:
                                Line_Diamond_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Bottom, DisplayImage, Color.LimeGreen);
                                break;
                            case 2:
                                Line_Diamond_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Top, DisplayImage, Color.LimeGreen);
                                break;
                        }
                    }
                    #endregion
                }
                #endregion

                #region LINE ĐƯỜNG KÍNH KIM CƯƠNG
                Line_Diamond_Center = new List<double>()
                {
                     Line_Diamond_Bottom[2],
                     (Line_Diamond_Bottom[1] + Line_Diamond_Top[1])/2,
                     Line_Diamond_Top[2],
                     (Line_Diamond_Bottom[3] + Line_Diamond_Top[3])/2
                };
                #endregion

                #region TÍNH ĐƯỜNG KÍNH KIM CƯƠNG
                if (get_Diameter_Manual)
                {
                    _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                    return;
                }

                _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                if (step_progress == 3 && show_progress == 3)
                    lst_DistanceGrinding.Add(_distanceGrinding / 1000);
                #endregion
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region LineFindRun OLD
        private void LineFindRun()
        {
            try
            {
                #region Snap image
                // TODO: 010125 - NB
                if (bitmap_Live != null)
                {
                    if (SmartWindow.InvokeRequired)
                    {
                        SmartWindow.Invoke(new Action(() =>
                        {
                            InputImage = bitmap_Live;
                        }));
                    }
                    else
                        InputImage = bitmap_Live;
                }

                if (InputImage == null) return;
                Bitmap bitmapImage = (Bitmap)InputImage.Clone();
                if (step_progress <= 1)
                    SmartWindow_DoubleClick(null, null);

                GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)bitmapImage.Clone());
                DisplayImage = GrayImage.Convert<Bgr, byte>();
                if (DisplayImage == null) return;
                Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);
                SetOrigin = new SetOrigin.SetOrigin();
                #endregion

                #region LẤY RA THÔNG TIN LINE DIMAOND VÀ LINE TRỤC BÊN PHẢI / TRÁI
                List<RTCRectangle> ROIs = new List<RTCRectangle>();
                List<RTCRectangle> _ROIs_Stone = new List<RTCRectangle>();
                int count = 0;
                RTCRectangle Data = new RTCRectangle();

                var roiList = this.ROIS.Values.ToList().FindAll(x => x.ActionID == 1 || x.ActionID == 2);
                for (int i = 0; i < roiList.Count; i++)
                {
                    // TODO: 230925 - NB
                    int actionID = roiList[i].ActionID;
                    int descCode = roiList[i].DescriptionCode;

                    LineFind = new LineFind();
                    cDrawingBaseType ROIB = (cDrawingBaseType)roiList[i];
                    Guid OldID = ROIB.ID;
                    switch (ROIB.DrawingType)
                    {
                        case EDrawingtypes.Rectang:
                            {
                                cRectangType Rec = (cRectangType)roiList[i];
                                Data = new RTCRectangle();
                                Data.Phi = (float)Rec.Phi * Math.PI / 180;
                                Data.Width = (float)Rec.Width * 2;
                                Data.Height = (float)Rec.Height * 2;
                                Data.Center = new RTCPoint(Rec.Center.Row, Rec.Center.Col, 0);

                                ROIs.Add(Data);
                                break;
                            }
                    }
                    // Giá trị input tool LineFind
                    LineFind.ROI = Data;
                    LineFind.ToolOrigin = toolOrigin;
                    LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                    LineFind.InputImage = GrayImage;
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;

                    var x = LineFind.LineSegment;
                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        SetBitsOn("D214.0");
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }
                    // Giá trị output tool
                    string err = LineFind.ErrMessage;

                    // LINE TRỤC BÊN PHẢI 
                    if (LineFind.LineSegment != null && actionID == 1)
                    {
                        // TODO: 230925 - NB
                        switch (descCode)
                        {
                            case 1:
                                Line_Diamond_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Bottom, DisplayImage, Color.LimeGreen);
                                break;
                            case 2:
                                Line_Diamond_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                if (step_progress != 3)
                                    DisplayGraphics(SmartWindow, Line_Diamond_Top, DisplayImage, Color.LimeGreen);
                                break;
                            case 3:
                                Line_Shaft_Right_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            case 4:
                                Line_Shaft_Right_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            default:
                                break;
                        }
                    }
                    // LINE TRỤC BÊN TRÁI
                    else if (LineFind.LineSegment != null && actionID == 2)
                    {
                        // TODO: 230925 - NB
                        switch (descCode)
                        {
                            case 1:
                                Line_Shaft_Left_Bottom = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            case 2:
                                Line_Shaft_Left_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                                break;
                            default:
                                break;
                        }
                    }
                }

                #endregion

                #region LINE ĐƯỜNG KÍNH KIM CƯƠNG
                Line_Diamond_Center = new List<double>()
                {
                     Line_Diamond_Bottom[2],
                     (Line_Diamond_Bottom[1] + Line_Diamond_Top[1])/2,
                     Line_Diamond_Top[2],
                     (Line_Diamond_Bottom[3] + Line_Diamond_Top[3])/2
                };
                if (!get_Diameter_Manual && step_progress == 1)
                    DisplayGraphics(SmartWindow, Line_Diamond_Center, DisplayImage, Color.Yellow);
                #endregion

                // TODO: 020726 - NB - Chế độ Auto + Semi Normal (còn Semi bypass không sử dụng được)
                if (!Is_Semi_Bypass_Inspect) // Đối với kim cương thuận chiều rois
                {
                    #region LINE TÂM TRỤC XOAY
                    if (_register_root_axis != null)
                    {
                        Line_Shaft_Right_Center = new List<double>()
                        {
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisX1),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisY1),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisX2),
                        Lib.ToDouble( _register_root_axis.CenterRotateAxisY2)
                        };
                        if (!get_Diameter_Manual && step_progress == 1)
                            DisplayGraphics(SmartWindow, Line_Shaft_Right_Center, DisplayImage, Color.LimeGreen);
                    }
                    #endregion

                    #region LINE TÂM TRỤC BÊN TRÁI
                    if (Line_Shaft_Left_Top != null && Line_Shaft_Left_Bottom != null)
                    {
                        Line_Shaft_Left_Center = new List<double>()
                        {
                         Line_Shaft_Left_Top[2],
                         (Line_Shaft_Left_Top[1] + Line_Shaft_Left_Bottom[1])/2,  // Y1 trung bình
                         Line_Shaft_Left_Bottom[2],
                         (Line_Shaft_Left_Top[3] + Line_Shaft_Left_Bottom[3])/2   // Y2 trung bình
                        };
                        if (!get_Diameter_Manual && step_progress == 1)
                            DisplayGraphics(SmartWindow, Line_Shaft_Left_Center, DisplayImage, Color.LimeGreen);
                    }
                    #endregion

                    #region LẤY 3 ĐIỂM NẰM TRÊN ĐƯỜNG TÂM KIM CƯỜNG + TÂM TRỤC XOAY + TÂM TRỤC BÊN TRÁI
                    float point1_X = (float)(Line_Diamond_Top[1] + Line_Diamond_Top[3]) / 2;
                    float point1_Y = (float)(Line_Diamond_Bottom[1] + Line_Diamond_Bottom[3]) / 2;
                    point1 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(point1_X + point1_Y) / 2);
                    // Điểm nằm trên đường thẳng tâm trục xoay bên phải
                    float point2_X = (float)(Line_Shaft_Right_Top[1] + Line_Shaft_Right_Top[3]) / 2;
                    float point2_Y = (float)(Line_Shaft_Right_Bottom[1] + Line_Shaft_Right_Bottom[3]) / 2;
                    point2 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(point2_X + point2_Y) / 2);

                    //Điểm nằm trên đường thẳng tâm trục bên trái
                    point6 = new PointF(Lib.ToInt(Line_Diamond_Bottom[2]), (float)(Line_Shaft_Left_Top[3] + Line_Shaft_Left_Bottom[3]) / 2);
                    #endregion
                }

                #region TÍNH ĐƯỜNG KÍNH KIM CƯƠNG
                if (get_Diameter_Manual)
                {
                    _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                    return;
                }

                if (step_progress == 1) // Check đồng tâm
                {
                    // TÍNH ĐỘ LỆCH CỦA 2 TRỤC BÊN PHẢI
                    _deviationConcentric = Math.Abs(CalcLineToLine(bitmapImage, Line_Diamond_Center, Line_Shaft_Right_Center, false) * PIXEL_TO_MM);
                    if (point1.Y < point2.Y) _deviationConcentric = -_deviationConcentric;

                    // TÍNH ĐỘ LỆCH CỦA 2 TRỤC BÊN TRÁI
                    _deviationBetweenTwoShaft = Math.Abs(CalcLineToLine(bitmapImage, Line_Shaft_Left_Center, Line_Shaft_Right_Center, false, false) * PIXEL_TO_MM);
                    if (point1.Y < point6.Y) _deviationBetweenTwoShaft = -_deviationBetweenTwoShaft;

                    // TÍNH GÓC GIỮA KIM CƯƠNG VÀ TRỤC
                    _valueAngle = CalcLineToLine_AngleMeasurement(bitmapImage, Line_Diamond_Center, Line_Shaft_Right_Center, false);
                }
                else
                {
                    _distanceGrinding = CalcLineToLine(bitmapImage, Line_Diamond_Bottom, Line_Diamond_Top, true) * PIXEL_TO_MM;
                    if (step_progress == 3 && show_progress == 3)
                        lst_DistanceGrinding.Add(_distanceGrinding / 1000);
                }
                #endregion

                #region TÍNH CHIỀU CAO TỪ MÉP ĐÁ ĐẾN MÉP DƯỚI KIM CƯƠNG
                if (step_progress != 2 || IsGrindDiamond_LineFindRun || (count_GrindingAuto != 2 && count_GrindingAuto != 3)) return;
                count = 0;

                // TODO: 230925 - NB
                foreach (object ROI in ROIS_Stone_TotalDept.Values.ToList().FindAll(x => x.ActionID == 11 && x.DescriptionCode == 5))
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
                                // TODO: 230925 - NB
                                _ROIs_Stone.Add(Data);
                                break;
                            }
                    }

                    LineFind.ROI = Data;
                    LineFind.ToolOrigin = toolOrigin;
                    LineFind.InSetOrigin = SetOrigin.Run(Data, toolOrigin);
                    LineFind.InputImage = GrayImage;
                    LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                    LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                    LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                    LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                    LineFind.OutlierDistanceThreshold = 1;
                    LineFind.StepFindPoint = 1;
                    LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                    LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                    LineFind.IsShowImageResult = true;

                    bool result = LineFind.Run();
                    if (!result)
                    {
                        _height_Frinding_Diamond_Real = 0;
                        // TODO: 070526 - NB
                        ShowPanel_WaitingForm("PHẦN MỀM ĐANG KHÔNG XÁC ĐỊNH ĐƯỢC ROIS, XIN VUI LÒNG KIỂM TRA LẠI", 1);
                        IsResult_LineFind = true;
                        return;
                    }
                    if (IsResult_LineFind)
                    {
                        ClosePanel();
                        IsResult_LineFind = false;
                    }

                    if (LineFind.LineSegment != null && count == 0)
                    {
                        #region MANUAL BYPASS PHÂN LOẠI 
                        if (modeRunning == 4)
                        {
                            var ROIB_Lowest = ROIS_LowestPoint_Diamond.Values.ToList().FindAll(x => x.ActionID == 12).FirstOrDefault();
                            if (ROIB_Lowest == null) return;

                            double offsetGrind_Max = 0;
                            if (step_progress == 2) offsetGrind_Max = offsetGrind;
                            else offsetGrind_Max = offsetGrind / 2;

                            // Mép trên đá mài
                            Line_GrindStone_Top = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_GrindStone_Top, DisplayImage, Color.Yellow);

                            // Mép dưới kim cương
                            Line_Diamond_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Diamond_Bottom[1], LineFind.LineSegment.Item2.X, Line_Diamond_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Diamond_Bottom_Lengthen, DisplayImage, Color.Blue);

                            Line_Diamond_Lowest = new List<double>() { Line_GrindStone_Top[0], ROIB_Lowest.Center.Col - ROIB_Lowest.Height, Line_GrindStone_Top[2], ROIB_Lowest.Center.Col - ROIB_Lowest.Height };
                            DisplayGraphics(SmartWindow, Line_Diamond_Lowest, DisplayImage, Color.Orange);
                            double value = (CalcLineToLine(bitmapImage, Line_GrindStone_Top, Line_Diamond_Lowest, false) * PIXEL_TO_MM * valueConvert) - offsetGrind;
                            _height_Frinding_Diamond_Real = (int)Math.Ceiling(value);

                            Line_Shaft_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Shaft_Left_Bottom[1], LineFind.LineSegment.Item2.X, Line_Shaft_Left_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Shaft_Bottom_Lengthen, DisplayImage, Color.Yellow);

                            double valueMax = (CalcLineToLine(bitmapImage, Line_Shaft_Bottom_Lengthen, Line_GrindStone_Top, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Max = (int)Math.Ceiling(valueMax);
                            Lib.UpdateTextBoxText(txtLogProcess, "Offset Value : " + offsetGrind_Max.ToString() + "\r\nHeight Frinding Max : " + valueMax.ToString("F2") + "\r\nHeight Frinding Real : " + _height_Frinding_Diamond_Real.ToString("F2") + "\r\nConvert Value : " + valueConvert.ToString());

                            break;
                        }
                        #endregion

                        #region AUTO
                        else // AUTO
                        {
                            // TODO: 230925 - NB
                            Line_Diamond_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                            DisplayGraphics(SmartWindow, Line_Diamond_Bottom_Lengthen, DisplayImage, Color.Yellow);

                            Line_GrindStone_Top = new List<double>() { LineFind.LineSegment.Item1.X, Line_Diamond_Bottom[1], LineFind.LineSegment.Item2.X, Line_Diamond_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_GrindStone_Top, DisplayImage, Color.Blue);

                            // TODO: 230925 - NB
                            double offsetGrind_Max = 0;
                            if (step_progress == 2) offsetGrind_Max = offsetGrind;
                            else offsetGrind_Max = offsetGrind / 2;

                            double value = (CalcLineToLine(bitmapImage, Line_GrindStone_Top, Line_Diamond_Bottom_Lengthen, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Real = (int)Math.Ceiling(value);

                            Line_Shaft_Bottom_Lengthen = new List<double>() { LineFind.LineSegment.Item1.X, Line_Shaft_Left_Bottom[1], LineFind.LineSegment.Item2.X, Line_Shaft_Left_Bottom[3] };
                            DisplayGraphics(SmartWindow, Line_Shaft_Bottom_Lengthen, DisplayImage, Color.Orange);

                            double valueMax = (CalcLineToLine(bitmapImage, Line_Shaft_Bottom_Lengthen, Line_Diamond_Bottom_Lengthen, true) * PIXEL_TO_MM * valueConvert) - offsetGrind_Max;
                            _height_Frinding_Diamond_Max = (int)Math.Ceiling(valueMax);
                            Lib.UpdateTextBoxText(txtLogProcess, "Offset Value : " + (offsetGrind).ToString() + " ; Height Value : " + valueMax.ToString("F2") + " ; Convert Value : " + valueConvert.ToString());

                            if (_height_Frinding_Diamond_Real > _height_Frinding_Diamond_Max && _height_Frinding_Diamond_Real > 0 && _height_Frinding_Diamond_Max > 0)
                            {
                                // TODO: 230925 - NB
                                string id = _mySQLHelper.FindByField<register_location_rois>("DescriptionCode", 5).IDGuid;
                                if (OldID != Guid.Parse(id))
                                    OldID = Guid.Parse(id);

                                string log = $"LineFind.ROI: [{Data}], ToolOrigin.ROI: [{toolOrigin}]";

                                string log1 = $"Line_Diamond_Bottom: IDGuid: [{id}] : [{LineFind.LineSegment.Item1.X}, {LineFind.LineSegment.Item1.Y},{LineFind.LineSegment.Item2.X}, {LineFind.LineSegment.Item2.Y}]";

                                string log2 = $"Line_GrindStone_Top: [{LineFind.LineSegment.Item1.X}, {Line_Diamond_Bottom[1]},{LineFind.LineSegment.Item2.X}, {Line_Diamond_Bottom[3]}]";

                                string log3 = $"Line_Shaft_Bottom: [{LineFind.LineSegment.Item1.X}, {Line_Shaft_Left_Bottom[1]},{LineFind.LineSegment.Item2.X}, {Line_Shaft_Left_Bottom[3]}]";

                                Lib.saveToLog_HeightDiamond($"{log} \r\n{log1} \r\n {log2} \r\n {log3}");
                            }

                            break;
                        }
                        #endregion
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #endregion

        #region CÁC HÀM TÍNH KHOẢNG CÁCH (CalcLineToLine)
        DistanceMeasurementTool.Measurement DistanceMeasure = new DistanceMeasurementTool.Measurement();
        List<double> DistancePoint = new List<double>();
        private double CalcLineToLine(Bitmap InputImage, List<double> Line1, List<double> Line2, bool isShowImageResult, bool IsShow = true)
        {
            double Value = 0;
            try
            {
                DistanceMeasure = new DistanceMeasurementTool.Measurement();
                DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_LineToLine;
                DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line1[0]), Lib.ToInt(Line1[1])), new Point(Lib.ToInt(Line1[2]), Lib.ToInt(Line1[3])));
                DistanceMeasure.LineTwo = new Tuple<Point, Point>(new Point(Lib.ToInt(Line2[0]), Lib.ToInt(Line2[1])), new Point(Lib.ToInt(Line2[2]), Lib.ToInt(Line2[3])));
                DistanceMeasure.InputImage = InputImage;
                DistanceMeasure.IsShowImageResult = isShowImageResult;
                DistanceMeasure.Tolerance = new Tuple<double, double, double>(0, 0, 0);

                // TODO: 041224 - NB
                if (!get_Diameter_Manual)
                {
                    switch (step_progress)
                    {
                        case 1:
                            DistanceMeasure.MeasurementType = shapeDiamond.MeasurementType_Concentric_Text;
                            break;
                        case 2:
                            DistanceMeasure.MeasurementType = shapeDiamond.MeasurementType_Grinding_Text;
                            break;
                        case 3:
                            DistanceMeasure.MeasurementType = shapeDiamond.MeasurementType_Inspect_Text;
                            break;
                        default:
                            break;
                    }
                }
                else
                    DistanceMeasure.MeasurementType = shapeDiamond.MeasurementType_Grinding_Text;

                // xử lý DistanceMeasure
                DistanceMeasure.Run();

                // Lấy output của các tool
                if (DistanceMeasure.Distance.Count > 0)
                {
                    double Distance = 0;
                    Distance = Math.Abs(DistanceMeasure.OutPoint1.Y - DistanceMeasure.OutPoint2.Y);
                    Value = Distance;
                }

                // TODO: 301124 - NB
                if (get_Diameter_Manual) return Value;
                DistancePoint = new List<double>() { DistanceMeasure.OutPoint1.X, DistanceMeasure.OutPoint1.Y, DistanceMeasure.OutPoint2.X, DistanceMeasure.OutPoint2.Y };
                // TODO: 230925 - NB
                if ((Math.Abs(Value) * PIXEL_TO_MM) <= _allowableDeviation)
                {
                    if (step_progress != 3 && IsShow)
                        DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Lime);
                }
                else
                {
                    if (step_progress == 1 && IsShow) // check đồng tâm
                        DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Red);
                    else if (step_progress == 2 && IsShow) // mài
                        DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.LimeGreen);
                }
            }
            catch (Exception ex)
            {
            }
            return Value;
        }
        #endregion

        #region CÁC HÀM TÍNH GÓC LỆNH GIỮA 2 LINE
        // TODO: 010425 - NB
        double Value = 0;
        private double CalcLineToLine_AngleMeasurement(Bitmap InputImage, List<double> Line1, List<double> Line2, bool isShowImageResult)
        {
            try
            {
                DistanceMeasure = new DistanceMeasurementTool.Measurement();
                DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_LineToLine;
                DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line1[0]), Lib.ToInt(Line1[1])), new Point(Lib.ToInt(Line1[2]), Lib.ToInt(Line1[3])));
                DistanceMeasure.LineTwo = new Tuple<Point, Point>(new Point(Lib.ToInt(Line2[0]), Lib.ToInt(Line2[1])), new Point(Lib.ToInt(Line2[2]), Lib.ToInt(Line2[3])));
                DistanceMeasure.InputImage = InputImage;
                DistanceMeasure.IsShowImageResult = isShowImageResult;
                DistanceMeasure.Tolerance = new Tuple<double, double, double>(0, 0, 0);
                DistanceMeasure.MeasurementType = shapeDiamond.MeasurementType_Concentric_Text;

                // Bổ sung thêm tính góc lệch giữa 2 đường line
                DistanceMeasure.AngleDirection = cDistanceMeasurementTool.AngleDirection_CWFromLine1ToLine2;
                DistanceMeasure.AngleEndPoint = cDistanceMeasurementTool.AngleEndPoint_FullAngleToEndingLine;
                DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_AngleOf2Lines;

                // xử lý DistanceMeasure
                DistanceMeasure.Run();
                Value = (DistanceMeasure.AngleActual * 180) / 3.1416;
                if (Value > 180) Value = 360 - Value;
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
            return Value;
        }
        #endregion

        #region HÀM TÍNH TOÁN ĐỘ ĐẢO
        private void InversionFunction_DistanceMeasureRun()
        {
            try
            {
                // TODO: 060825 - NB
                //LineFindRun();
                LineFindRun_Grinding_InspectFunction();

                // TODO: 020726 - NB
                if (Is_Semi_Bypass_Inspect) return;

                // Hiển thị line và tính toán total depth,crow height
                LineFindRun_Crow_TotalDept();
            }
            catch (Exception ex)
            {
                Lib.saveToLog(ex.ToString());
            }
        }

        List<double> LineTest1 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest2 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest3 = new List<double>() { 0, 0, 0, 0 };
        List<double> LineTest4 = new List<double>() { 0, 0, 0, 0 };
        private void Calc_Crowheight_TotalDepth()
        {
            // Lấy
            PointF inter = FindIntersection((Line_Cross_Diamond_Up[1], Line_Cross_Diamond_Up[0]), (Line_Cross_Diamond_Up[3], Line_Cross_Diamond_Up[2]), (Line_Diamond_Bottom[1], Line_Diamond_Bottom[0]), (Line_Diamond_Bottom[3], Line_Diamond_Bottom[2]));
            LineTest1 = new List<double>() { inter.Y, inter.X, Line_Diamond_Bottom[0], Line_Diamond_Bottom[1] };
            PointF intersectPoint = FindIntersection((LineTest1[1], LineTest1[0]), (LineTest1[3], LineTest1[2]), (Line_Diamond_Top_Full[1], Line_Diamond_Top_Full[0]), (Line_Diamond_Top_Full[3], Line_Diamond_Top_Full[2]));
            LineTest2 = new List<double>() { intersectPoint.Y, intersectPoint.X, LineTest1[0], LineTest1[1] };
            DisplayGraphics(SmartWindow, LineTest2, DisplayImage, Color.Yellow);
            double crowHeight1 = Math.Sqrt((LineTest2[0] - LineTest2[2]) * (LineTest2[0] - LineTest2[2]) + (LineTest2[1] - LineTest2[3]) * (LineTest2[1] - LineTest2[3]));

            PointF inter3 = FindIntersection((Line_Cross_Diamond_Down[1], Line_Cross_Diamond_Down[0]), (Line_Cross_Diamond_Down[3], Line_Cross_Diamond_Down[2]), (Line_Diamond_Top[1], Line_Diamond_Top[0]), (Line_Diamond_Top[3], Line_Diamond_Top[2]));
            LineTest3 = new List<double>() { inter3.Y, inter3.X, Line_Diamond_Top[0], Line_Diamond_Top[1] };
            PointF intersectPoint4 = FindIntersection((LineTest3[1], LineTest3[0]), (LineTest3[3], LineTest3[2]), (Line_Diamond_Bottom_Full[1], Line_Diamond_Bottom_Full[0]), (Line_Diamond_Bottom_Full[3], Line_Diamond_Bottom_Full[2]));
            LineTest4 = new List<double>() { intersectPoint4.Y, intersectPoint4.X, LineTest3[0], LineTest3[1] };
            DisplayGraphics(SmartWindow, LineTest4, DisplayImage, Color.Yellow); // FromArgb(255, 128, 0)
            double crowHeight2 = Math.Sqrt((LineTest4[0] - LineTest4[2]) * (LineTest4[0] - LineTest4[2]) + (LineTest4[1] - LineTest4[3]) * (LineTest4[1] - LineTest4[3]));
            double average = (crowHeight1 + crowHeight2) / 2 * PIXEL_TO_MM;
            lst_CrowHeight.Add(average);

            #region Điểm giao nhau của đường chéo trên / dưới để lấy ra tâm kim cương
            PointF inter_GiaoNhau = FindIntersection((Line_Cross_Diamond_Up[1], Line_Cross_Diamond_Up[0]), (Line_Cross_Diamond_Up[3], Line_Cross_Diamond_Up[2]), (Line_Cross_Diamond_Down[1], Line_Cross_Diamond_Down[0]), (Line_Cross_Diamond_Down[3], Line_Cross_Diamond_Down[2]));
            Line_Cross_Diamond_Intersect_1 = new List<double>() { inter_GiaoNhau.Y, inter_GiaoNhau.X, Line_Cross_Diamond_Up[2], Line_Cross_Diamond_Up[3] };
            DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_1, DisplayImage, Color.Blue);
            Line_Cross_Diamond_Intersect_2 = new List<double>() { inter_GiaoNhau.Y, inter_GiaoNhau.X, Line_Cross_Diamond_Down[0], Line_Cross_Diamond_Down[1] };
            DisplayGraphics(SmartWindow, Line_Cross_Diamond_Intersect_2, DisplayImage, Color.Blue);
            #endregion

            #region Lấy ra line thực tế kim cương theo 2 line full mép trên/dưới
            List<double> LineTest5 = new List<double>() { 0, 0, 0, 0 };
            LineTest5 = new List<double>() { LineTest2[0], LineTest2[1], LineTest4[0], LineTest4[1] };
            DisplayGraphics(SmartWindow, LineTest5, DisplayImage, Color.Green); // FromArgb(255, 128, 0)
            #endregion

            List<double> LineTest6 = new List<double>() { 0, 0, 0, 0 };
            PointF inter_S = new PointF();
            PointF inter_E = new PointF();
            // TODO: 271025 - NB
            if (Is_Calculate_TotalDepth_TAB)
            {
                //nửa mặt Table
                int temp = (int)((SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pLowerGirdleUp.Y) * (double)shapeDiamond.Crow / 100 * Math.Tan((90 - (double)shapeDiamond.TOP) * Math.PI / 180));
                inter_E = new PointF(SmartWindow.m_pLowerGirdleUp.Y + temp, SmartWindow.m_pTable.X);
                inter_S = new PointF(SmartWindow.m_pTable.Y, SmartWindow.m_pTable.X);
            }
            else
            {
                inter_S = new PointF((float)LineTest5[1], (float)LineTest5[0]);
                inter_E = new PointF((float)LineTest5[3], (float)LineTest5[2]);
            }

            // Lấy điểm vuông góc từ tâm kim cương đến cạnh bên
            PointF inter_Test = GetRightAnglePoint(inter_GiaoNhau, inter_S, inter_E);
            LineTest6 = new List<double>() { inter_GiaoNhau.Y, inter_GiaoNhau.X, inter_Test.Y, inter_Test.X };

            // Vẽ đường từ tâm đến cạnh bên màu trắng
            DisplayGraphics(SmartWindow, LineTest6, DisplayImage, Color.White);
            double totalDept = Math.Sqrt((LineTest6[0] - LineTest6[2]) * (LineTest6[0] - LineTest6[2]) + (LineTest6[1] - LineTest6[3]) * (LineTest6[1] - LineTest6[3])) * PIXEL_TO_MM;
            lst_TotalDepth.Add(totalDept);
        }

        public static PointF GetRightAnglePoint(PointF PointCenter, PointF StartPointLine, PointF EndPointLine)
        {
            // TODO: 210825 - NB
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
        #endregion

        #region HÀM TÍNH CROWHEIGHT, TOTALDEPTH
        private double CalcPointToLine_Crow(Bitmap InputImage, Point Point1, List<double> Line2)
        {
            double Value = 0;
            DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_PointToLine;
            DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line2[0]), Lib.ToInt(Line2[1])), new Point(Lib.ToInt(Line2[2]), Lib.ToInt(Line2[3])));
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
                Value = DistanceMeasure.Distance[0];
            }
            DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.OrangeRed);
            return Value;
        }

        private double CalcPointToLine_Crow_TotalDept(Bitmap InputImage, Point Point1, List<double> Line2)
        {
            double Value = 0;
            DistanceMeasure.DistanceType = cDistanceMeasurementTool.DistanceType_PointToLine;
            DistanceMeasure.Line = new Tuple<Point, Point>(new Point(Lib.ToInt(Line_Diamond_Bottom[0]), Lib.ToInt(Line_Diamond_Bottom[1])), new Point(Lib.ToInt(Line_Diamond_Bottom[2]), Lib.ToInt(Line_Diamond_Bottom[3])));
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
                Value = DistanceMeasure.Distance[0];
            }
            DisplayGraphics(SmartWindow, DistancePoint, DisplayImage, Color.Green);
            return Value;
        }
        #endregion

        #endregion

        #region RECONNECT, ALIVE VÀ HIỂN THỊ LỖI PLC

        #region RECONNECT PLC
        bool statusReconnect = false;
        private void ReconnectPLC_MC()
        {
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    if (!cDeviceManager._plcMC.Connected && !statusReconnect)
                    {
                        statusReconnect = true;
                        Lib.UpdateButtonText_BColor_FColor(btnStatusPLC, "Disconnected", Color.Red, Color.White);
                        Lib.saveToLog($"Thời gian mất kết nối PLC: {DateTime.Now}");
                        continue;
                    }
                    if (statusReconnect)
                    {
                        cDeviceManager._plcMC.Connect();
                        if (cDeviceManager._plcMC.Connected)
                        {
                            statusReconnect = false;
                            Lib.UpdateButtonText_BColor_FColor(btnStatusPLC, "Connected", Color.Green, Color.White);
                            Lib.saveToLog($"Thời gian kết nối PLC trở lại: {DateTime.Now}");
                        }
                    }
                    if (!statusReconnect)
                        Thread.Sleep(2000);
                    else
                        Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    Lib.saveToErr("ReconnectPLC()" + "\r\n" + ex.ToString());
                }
            }
        }
        #endregion

        #region ALIVE PLC
        bool is_LockRois = false;
        bool _isManual = false;
        private void CheckAlive()
        {
            int oData_Alive = 1;
            while (true)
            {
                Thread.Sleep(100);

                try
                {
                    #region ALIVE PLC
                    try
                    {
                        if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                        {
                            cDeviceManager._plcMC.GetSingleRegister_Int16("D200", out oData_Alive);
                            cDeviceManager._plcMC.GetSingleRegister_Int32("D1848", out int grindingStone_left);
                            cDeviceManager._plcMC.GetSingleRegister_Int32("D1842", out int grindingStone_right);
                            cDeviceManager._plcMC.GetSingleRegister_Int32("D1850", out int wheelSpeed_left);
                            cDeviceManager._plcMC.GetSingleRegister_Int32("D1844", out int wheelSpeed_right);
                            cDeviceManager._plcMC.GetSingleRegister_Int32("D3238", out int wheel_Speed_To_Left_Cutting);

                            Lib.UpdateLabelText(lbGrindingStone_L, grindingStone_left.ToString());
                            Lib.UpdateLabelText(lbGrindingStone_R, grindingStone_right.ToString());
                            Lib.UpdateLabelText(lbGrindingWheelSpeed_L, wheelSpeed_left.ToString());
                            Lib.UpdateLabelText(lbGrindingWheelSpeed_R, wheelSpeed_right.ToString());
                            // TODO: 060826 - NB
                            Lib.UpdateLabelText(lblWheel_Speed_To_Left_Cutting, wheel_Speed_To_Left_Cutting.ToString());

                            if (tabControl1.InvokeRequired)
                                tabControl1.Invoke((MethodInvoker)delegate
                                {
                                    GrindingStone_L.Value = grindingStone_left;
                                    GrindingStone_R.Value = grindingStone_right;
                                    GrindingWheelSpeed_L.Value = wheelSpeed_left;
                                    GrindingWheelSpeed_R.Value = wheelSpeed_right;
                                    zoom_Wheel_Speed_To_Left_Cutting.Value = wheel_Speed_To_Left_Cutting;
                                }
                                );
                            else
                            {
                                GrindingStone_L.Value = grindingStone_left;
                                GrindingStone_R.Value = grindingStone_right;
                                GrindingWheelSpeed_L.Value = wheelSpeed_left;
                                GrindingWheelSpeed_R.Value = wheelSpeed_right;
                                zoom_Wheel_Speed_To_Left_Cutting.Value = wheel_Speed_To_Left_Cutting;
                            }

                            if (oData_Alive == 0)
                                Lib.UpdateButtonBackColor(btnAlivePLC, Color.Green);
                            else
                                Lib.UpdateButtonBackColor(btnAlivePLC, Color.Transparent);
                            oData_Alive = oData_Alive == 0 ? 1 : 0;

                            if (cDeviceManager.GetBits("M415"))
                            {
                                IsModeOption = true;
                                Lib.UpdateLabelText_BColor_FColor(lblModeOption, "AUTO", Color.Transparent, Color.Green);
                            }
                            if (cDeviceManager.GetBits("M414"))
                            {
                                IsModeOption = false;
                                Lib.UpdateLabelText_BColor_FColor(lblModeOption, "MANUAL", Color.Transparent, Color.Red);
                            }

                            // Lấy trạng thái Mode chạy Auto / Manual
                            _isManual = GetBits("M3300");
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.saveToErr(ex.ToString());
                    }
                    #endregion

                    #region  ALIVE TRẠNG THÁI CAMERA
                    // TODO: 120825 - NB
                    if (IsRun)
                    {
                        try
                        {
                            if (cDeviceManager.HikCamera == null)
                                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                            else if (cDeviceManager.HikCamera.IsOpen)
                                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);
                            else
                                Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                        }
                        catch (Exception) { }
                    }
                    #endregion

                    #region LOCK / UNLOCK ROIS
                    // TODO: 020726 - NB - CHO PHÉP MANUAL CŨNG BỎ LOCK ROIS
                    if (IsStartProcess && step_progress > 1 && show_progress >= 2)  //  lock rois
                                                                                    //if (IsModeOption && IsStartProcess && step_progress > 1 && show_progress >= 2)  //  lock rois
                    {
                        if (!is_LockRois)
                        {
                            is_LockRois = true;
                            if (SmartWindow.InvokeRequired) SmartWindow.Invoke(new Action(() => { SmartWindow.LockRoi = true; }));
                            else SmartWindow.LockRoi = true;
                        }
                    }
                    else // bỏ lock rois 
                    {
                        is_LockRois = false;
                        if (SmartWindow.InvokeRequired) SmartWindow.Invoke(new Action(() => { SmartWindow.LockRoi = false; }));
                        else SmartWindow.LockRoi = false;
                    }
                    #endregion
                }
                catch (Exception)
                {
                }
                Thread.Sleep(500);
            }
        }
        #endregion

        #region HIỂN THỊ TRẠNG THÁI MAIN PROCESS + CHECK LỖI ERROR TỪ PLC
        bool IsModeOption = false;
        // TODO: 090525
        bool IsReset_PLC = false;
        private void CheckAlarm_MainProcess()
        {
            while (true)
            {
                Thread.Sleep(100);
                try
                {
                    // TODO: 290626
                    Lib.UpdateLabelText(lbl_Step_progress, Lib.ToString(step_progress));
                    Lib.UpdateLabelText(lbl_Show_progress, Lib.ToString(show_progress));
                    if (lblLockRois.InvokeRequired)
                        lblLockRois.Invoke((MethodInvoker)delegate
                        {
                            if (is_LockRois)
                                lblLockRois.BackColor = Color.Red;
                            else
                                lblLockRois.BackColor = Color.Green;
                        });
                    else
                    {
                        if (is_LockRois)
                            lblLockRois.BackColor = Color.Red;
                        else
                            lblLockRois.BackColor = Color.Green;
                    }

                    #region Hiển thị trạng thái đang chạy đến công đoạn nào 
                    try
                    {
                        switch (show_progress)
                        {
                            case -1:
                                Lib.UpdateMultipleButtonBackColor(Color.Green, btnConcentricState, btnGrindAutoState, btnInspectState);
                                Lib.UpdateMultipleButtonForeColor(Color.White, btnConcentricState, btnGrindAutoState, btnInspectState);
                                Lib.UpdateMultipleButtonText("Completed", btnConcentricState, btnGrindAutoState, btnInspectState);
                                break;
                            case 0:
                                Lib.UpdateMultipleButtonBackColor(Color.FromArgb(255, 128, 0), btnConcentricState, btnGrindAutoState, btnInspectState);
                                Lib.UpdateMultipleButtonForeColor(Color.White, btnConcentricState, btnGrindAutoState, btnInspectState);
                                Lib.UpdateMultipleButtonText("Waiting", btnConcentricState, btnGrindAutoState, btnInspectState);
                                break;
                            case 1:
                                Lib.UpdateMultipleButtonBackColor(Color.FromArgb(255, 128, 0), btnGrindAutoState, btnInspectState);
                                Lib.UpdateMultipleButtonForeColor(Color.White, btnGrindAutoState, btnInspectState);

                                Lib.UpdateButtonText_BColor_FColor(btnConcentricState, "Running", Color.Transparent, Color.Black);
                                Thread.Sleep(500);
                                Lib.UpdateButtonText_BColor_FColor(btnConcentricState, "Running", Color.LimeGreen, Color.Black);
                                break;
                            case 2:
                                Lib.UpdateButtonText_BColor_FColor(btnInspectState, "Waiting", Color.FromArgb(255, 128, 0), Color.White);
                                Lib.UpdateButtonText_BColor_FColor(btnConcentricState, "Completed", Color.Green, Color.White);

                                Lib.UpdateButtonText_BColor_FColor(btnGrindAutoState, "Running", Color.Transparent, Color.Black);
                                Thread.Sleep(500);
                                Lib.UpdateButtonText_BColor_FColor(btnGrindAutoState, "Running", Color.LimeGreen, Color.Black);
                                break;
                            case 3:
                                Lib.UpdateButtonText_BColor_FColor(btnConcentricState, "Completed", Color.Green, Color.White);
                                Lib.UpdateButtonText_BColor_FColor(btnGrindAutoState, "Completed", Color.Green, Color.White);

                                Lib.UpdateButtonText_BColor_FColor(btnInspectState, "Running", Color.Transparent, Color.Black);
                                Thread.Sleep(500);
                                Lib.UpdateButtonText_BColor_FColor(btnInspectState, "Running", Color.LimeGreen, Color.Black);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.saveToErr(ex.ToString());
                    }
                    #endregion

                    #region Check lỗi từ PLC và hiển thị lên màn hình
                    if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                    {
                        CheckAlarm("R600");
                        CheckAlarm("R601");
                        CheckAlarm("R602");
                        if (_lstErrorWarningPLC.Count > 0)
                        {
                            var firstElement = _lstErrorWarningPLC.First();
                            string contentNew = firstElement[1].ToString();
                            if (contentNew != txtContent.Text.Trim())
                                ShowPanel_WaitingForm(contentNew, 1);
                        }
                    }
                    #endregion

                    if (GetBits("M1033") && !IsModeOption)
                    {
                        cDeviceManager._plcMC.SetSingleRegister_Int16("D250", 1);
                        IsReset_PLC = true;
                        ClearProcess();
                        ClosePanel();
                    }
                }
                catch (Exception ex)
                {
                    Lib.saveToErr(ex.ToString());
                }
                Thread.Sleep(100);
            }
        }
        List<string[]> _lstErrorWarningPLC = new List<string[]>();
        Dictionary<string, bool> teachingValue = new Dictionary<string, bool>();
        void CheckAlarm(string Address)
        {
            try
            {
                int value = 16;
                int check = cDeviceManager._plcMC.GetSingleRegister_Int16(Address, out value);
                int check1 = cDeviceManager._plcMC.GetSingleRegister_Int16("R600", out int value1);
                BitArray bits = new BitArray(new int[] { value });
                for (int i = 0; i < bits.Length; i++)
                {
                    string errorAddress = string.Empty;
                    if (i < 10)
                        errorAddress = Address + "." + i;
                    else if (i == 10)
                        errorAddress = Address + "." + "A";
                    else if (i == 11)
                        errorAddress = Address + "." + "B";
                    else if (i == 12)
                        errorAddress = Address + "." + "C";
                    else if (i == 13)
                        errorAddress = Address + "." + "D";
                    else if (i == 14)
                        errorAddress = Address + "." + "E";
                    else if (i == 15)
                        errorAddress = Address + "." + "F";

                    if (bits[i])
                    {
                        string errorWarning = string.Empty;
                        // Kiểm tra trên server để lấy mã + tên lỗi
                        register_error error = _mySQLHelper.FindByField<register_error>("RegisterAddress", errorAddress);
                        if (error != null && error.ID > 0)
                        {
                            errorWarning = errorAddress + "_" + error.ErrorWarning;
                            var target = new string[2] { errorAddress, errorWarning };
                            teachingValue[errorAddress] = true;
                            _lstErrorWarningPLC.Add(target);
                        }
                    }
                    else
                    {
                        teachingValue[errorAddress] = false;
                        if (_lstErrorWarningPLC.Count > 0)
                        {
                            _lstErrorWarningPLC.RemoveAll(t => t[0] == errorAddress);
                            if (_lstErrorWarningPLC.Count == 0)
                                ClosePanel();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #endregion

        #region CONNECT, LIVE VÀ TRẠNG THÁI CAMERA

        #region CONNECT CAMERA 
        // Lấy ra list Camera có thể kết nối
        private void DeviceListAcq()
        {
            try
            {
                cbDeviceList.Items.Clear();
                MyHikCamera.MV_CC_DEVICE_INFO_LIST deviceInfoList = GetHikrobotDeviceInfoList();
                if (deviceInfoList.nDeviceNum > 0)
                {
                    // ch:在窗体列表中显示设备名 | en:Display device name in the form list
                    for (int i = 0; i < deviceInfoList.nDeviceNum; i++)
                    {
                        MyHikCamera.MV_CC_DEVICE_INFO device = (MyHikCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceInfoList.pDeviceInfo[i], typeof(MyHikCamera.MV_CC_DEVICE_INFO));
                        if (device.nTLayerType == MyHikCamera.MV_GIGE_DEVICE)
                        {
                            MyHikCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyHikCamera.MV_GIGE_DEVICE_INFO)MyHikCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyHikCamera.MV_GIGE_DEVICE_INFO));

                            if (gigeInfo.chUserDefinedName != "")
                            {
                                cbDeviceList.Items.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                            }
                            else
                            {
                                cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " " +
                                    "(" + gigeInfo.chSerialNumber + ")");
                            }
                        }
                        else if (device.nTLayerType == MyHikCamera.MV_USB_DEVICE)
                        {
                            MyHikCamera.MV_USB3_DEVICE_INFO usbInfo = (MyHikCamera.MV_USB3_DEVICE_INFO)MyHikCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyHikCamera.MV_USB3_DEVICE_INFO));
                            if (usbInfo.chUserDefinedName != "")
                            {
                                cbDeviceList.Items.Add("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                            }
                            else
                            {
                                cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                            }
                        }
                    }
                    cbDeviceList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi detect Camera" + ex.ToString());
            }

        }

        public static MyHikCamera.MV_CC_DEVICE_INFO_LIST GetHikrobotDeviceInfoList()
        {
            try
            {
                GC.Collect();
                MyHikCamera.MV_CC_DEVICE_INFO_LIST mStDeviceList = new MyHikCamera.MV_CC_DEVICE_INFO_LIST();
                int nRet = MyHikCamera.MV_CC_EnumDevices_NET(MyHikCamera.MV_GIGE_DEVICE | MyHikCamera.MV_USB_DEVICE, ref mStDeviceList);
                if (0 != nRet)
                    return new MyHikCamera.MV_CC_DEVICE_INFO_LIST();
                return mStDeviceList;
            }
            catch
            {
                return new MyHikCamera.MV_CC_DEVICE_INFO_LIST();
            }
        }
        private void DisconnectCamera_Hikrobot()
        {
            if (cDeviceManager.HikCamera == null || cDeviceManager.HikCamera.GetCameraHandle() == IntPtr.Zero) return;
            SetLiveCamera_Hikrobot(false);
            IsLive = false;
            Thread.Sleep(200);
            cDeviceManager.HikCamera.MV_CC_StopGrabbing_NET();
            Thread.Sleep(200);
            cDeviceManager.HikCamera.MV_CC_CloseDevice_NET();
            Thread.Sleep(200);
            cDeviceManager.HikCamera.MV_CC_DestroyDevice_NET();
            Thread.Sleep(200);
            cDeviceManager.HikCamera = null;
        }
        #endregion

        #region LIVE CAMERA
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
            // SnapImage_Camera_Basler();
            SnapImage_Camera_Hikrobot();
            if (liveBackgroundWorker != null)
            {
                liveBackgroundWorker.Dispose();

                liveBackgroundWorker = null;
            }
        }
        public static object lockImage = new object();
        public static object lockImage2 = new object();
        //CAMERA HIKROBOT
        Bitmap bitmap_Live = null;
        bool firstShow = true;

        bool IsStop_LiveCamera_RealTime = false;
        private void BackgroundWorkerLiveWebCam_Hikrobot_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsLive)
            {
                Thread.Sleep(10);

                if (IsShowLiveCamera)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // TODO: 281125 - NB
                if (IsStop_LiveCamera_RealTime) continue;
                if (isStopLive_GaKimCuong && !IsStop_LiveCamera_RealTime)
                {
                    IsStop_LiveCamera_RealTime = true;
                    continue;
                }
                try
                {
                    if (cDeviceManager.HikCamera == null || !IsLive) return;
                    if (SmartWindow == null) return;
                    lock (lockImage)
                    {
                        // TODO: 230925 - NB
                        //bitmap_Live = new Bitmap(@"D:\00. Project\03. 2024\129. LADI.24.001\SW\Image Test\Image_20250212102203646.bmp");
                        bitmap_Live = cDeviceManager.HikCamera.SingleShotGrabbing();

                        if (bitmap_Live != null)
                        {
                            Bitmap safeBitmap;
                            lock (bitmap_Live) // tránh race condition
                            {
                                safeBitmap = (Bitmap)bitmap_Live.Clone();
                            }

                            if (SmartWindow.InvokeRequired)
                            {
                                SmartWindow.Invoke(new Action(() =>
                                {
                                    SmartWindow.Image?.Dispose(); // giải phóng ảnh cũ để tránh leak
                                    SmartWindow.Image = safeBitmap;
                                }));
                            }
                            else
                            {
                                SmartWindow.Image?.Dispose(); // giải phóng ảnh cũ để tránh leak
                                SmartWindow.Image = safeBitmap;
                            }
                        }
                        if (!IsLive) return;
                        if (liveBackgroundWorker != null)
                        {
                            if (liveBackgroundWorker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        if (firstShow)
                        {
                            firstShow = false;
                            // TODO: 240425
                            SmartWindow.FunctionFitImage();
                            SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.1f, 1.1f);
                        }
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
            catch (Exception)
            {
            }
        }

        #endregion

        #endregion

        #region BUTTON CLICK
        private void btnSettingVision_Click(object sender, EventArgs e)
        {
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang load form settings");
            wait.Show();
            try
            {
                SmartWindow.ClearAllRoi();
                lst_SmartWindow_Rois.Clear();
                DrawingObjects_Circle.Clear();
                DrawingObjects_LowestPoint_Diamond.Clear();
                lst_SmartWindow_LowestPoint_Diamond.Clear();
                FrmSetting_Vision fSettings = new FrmSetting_Vision();
                fSettings.DeviceName = cbDeviceList.Text;
                if (IsLiveCamera_Auto)
                    btnLiveCamera_Click(null, null);
                wait.Close();
                if (fSettings.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _register_root_axis = _mySQLHelper.FindAll<register_root_axis>().FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi load dữ liệu: " + ex.ToString());
                    }

                    DrawingObjects = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
                    DrawingObjects_Crow_TotalDept = new Dictionary<Guid, GraphicsWindow.GraphicsWindow.DataRoi>();
                    DrawingObjects_LowestPoint_Diamond = new Dictionary<Guid, DataRoi>();
                    ROIProperties_Parameters = new Dictionary<Guid, cROIProperty>();
                    ROIS = new Dictionary<Guid, cRectangType>();
                    ROIS_Stone_TotalDept = new Dictionary<Guid, cRectangType>();
                    ROIS_LowestPoint_Diamond = new Dictionary<Guid, cRectangType>();
                    lst_SmartWindow_Rois = new List<DataRoi>();
                    lst_SmartWindow_Crow_TotalDept = new List<DataRoi>();
                    lst_SmartWindow_LowestPoint_Diamond = new List<DataRoi>();

                    FrmMain_Load(null, null);
                    SmartWindow.m_bCheckMouseAll = true;
                    SmartWindow_MouseUp(null, null);
                    SmartWindow.m_bCheckMouseAll = false;

                    // TODO: 311025 - NB
                    LoadConfig_Data();
                    loadParameterPLC();

                    if (!IsLiveCamera_Auto)
                        btnLiveCamera_Click(null, null);
                }
            }
            catch (Exception)
            {
            }
            wait.Close();
        }

        private void btnShowAction_CheckedChanged(object sender, EventArgs e)
        {
            if (btnShowAction.Checked)
            {
                if (!IsStartProcess && !IsModeOption)
                    btnLiveCamera.Enabled = true;
                Lib.UpdateVisibleControl(pnSettingInfor, true);
                // TODO: 101224
                LoadConfig_Data();
                loadParameterPLC();
            }
            else
            {
                if (IsLiveCamera_Auto)
                    btnLiveCamera_Click(null, null);
                btnLiveCamera.Enabled = false;
                btnLiveCamera.Appearance.BackColor = btnLiveCamera.Appearance.ForeColor = Color.Empty;
                btnLiveCamera.Appearance.Options.UseBackColor = btnLiveCamera.Appearance.Options.UseForeColor = false;
                Lib.UpdateVisibleControl(pnSettingInfor, false);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FrmHistory fHistory = new FrmHistory();
            fHistory.Show();
        }
        private void btnCloseAction_Click(object sender, EventArgs e)
        {
            // btnShowAction.Checked = false;
            if (btnShowAction.InvokeRequired)
                btnShowAction.Invoke((MethodInvoker)delegate
                {
                    btnShowAction.Checked = false;
                }
                );
            else
                btnShowAction.Checked = false;
            btnShowAction_CheckedChanged(null, null);
        }

        private void num_CheckInspect_ValueChanged(object sender, EventArgs e)
        {
            // TODO: 090925 - NB 
            if (cDeviceManager._plcMC != null)
                cDeviceManager._plcMC.SetSingleRegister_Int16("D3125", Lib.ToInt(num_Inspect.Value));
            _config_data.InspectNumber = Lib.ToString(num_Inspect.Value);
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
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
        /// <summary>
        /// Hiển thị thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetGrindLayer_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: 090925 - NB
                LoadConfig_Data();
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }

        private void btnSetGrindLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtGreater.Text))
                {
                    MessageBox.Show("Xin vui lòng nhập giá trị [Greater than 0.03] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtFromTo.Text))
                {
                    MessageBox.Show("Xin vui lòng nhập giá trị [From 0.01 to 0.03] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtLess.Text))
                {
                    MessageBox.Show("Xin vui lòng nhập giá trị [Less thanh 0.01] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (Lib.ToDouble(txtLess.Text.Trim()) >= 10)
                {
                    MessageBox.Show("Giá trị [Less than 0.01] không thể lớn hơn 10 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (Lib.ToDouble(txtFromTo.Text.Trim()) >= 30)
                {
                    MessageBox.Show("Giá trị [From 0.01 to 0.03] không thể lớn hơn 30 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (Lib.ToDouble(txtGreater.Text.Trim()) > 10 || Lib.ToDouble(txtFromTo.Text.Trim()) > 10)
                {
                    if (MessageBox.Show("Giá trị bạn nhập vào đang rất lớn. Bạn có muốn lưu các thông tin vừa nhập không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                }

                _config_data = _mySQLHelper.FindAll<config_data>().FirstOrDefault();
                _config_data.GreaterHeight = txtGreater.Text.Trim();
                _config_data.AverageHeight = txtFromTo.Text.Trim();
                _config_data.LessHeight = txtLess.Text.Trim();
                _config_data.AllowableDeviation_MM = txtAllowableDeviation_Milimet.Text.Trim();
                _config_data.ValueConvert = txtValueConvert.Text.Trim();
                _config_data.OffsetGrindingStone = txtOffsetGrinding.Text.Trim();
                _config_data.InspectNumber = txtInspectActual.Text.Trim();
                // TODO: 271025 - NB
                _config_data.DistanceMove_Preview = txtDistanceMove_Preview.Text.Trim();
                _config_data.IsTotalDepth_TAB = Lib.ToInt(chk_TotalDepth_TAB.Checked);

                if (_config_data != null)
                {
                    try
                    {
                        var update = _mySQLHelper.Update<config_data>(_config_data);
                        if (update.IsSuccess)
                        {
                            _allowableDeviation = Lib.ToDouble(txtAllowableDeviation_Milimet.Text.Trim()) * 1000;
                            valueConvert = Lib.ToDouble(_config_data.ValueConvert);
                            offsetGrind = Lib.ToDouble(_config_data.OffsetGrindingStone);
                            num_Inspect.Value = Lib.ToInt(_config_data.InspectNumber);

                            // TODO: 271025 - NB
                            distanceMove_Preview = Lib.ToDouble(_config_data.DistanceMove_Preview);
                            Is_Calculate_TotalDepth_TAB = Lib.ToBoolean(_config_data.IsTotalDepth_TAB);
                        }
                        else
                        {
                            Lib.saveToErr(update.ErrorText);
                        }
                    }
                    catch (Exception ex)
                    {
                        Lib.saveToErr(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        private void btnGetConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath;

                string fileName = path + "\\" + "ValueConvert" + ".txt";
                string convertValue = txtValueConvert.Text;
                if (!File.Exists(fileName))
                    return;
                else
                {
                    string[] result = File.ReadAllLines(fileName);
                    txtValueConvert.Text = result[0];
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region PANEL CẢNH BÁO

        #region SHOW CẢNH BÁO
        /// <summary>
        /// show lên màn hình chờ
        /// </summary>
        /// <param name="content"></param>
        private void ShowPanel_WaitingForm(string content, int typeWaiting, int typeShow = 1)
        {
            // TODO: 050525
            Color _color;
            string title = string.Empty;
            string fileName = string.Empty;
            try
            {
                if (typeWaiting == 1)
                {
                    _color = Color.Red;
                    title = "ERROR";
                    fileName = "/images/Error.gif";
                }
                else if (typeWaiting == 2)
                {
                    _color = Color.FromArgb(255, 128, 0);
                    title = "WARNING";
                    fileName = "/images/Warning.gif";
                }
                else
                {
                    _color = Color.Black;
                    title = "PLEASE WAIT";
                    fileName = "/images/loader.gif";
                }
                Lib.UpdateLabelText(lblTitle, title.ToUpper());
                Lib.UpdateTextBoxText(txtContent, content.ToUpper());

                if (pc_logo.InvokeRequired)
                    pc_logo.Invoke(new Action(() => pc_logo.Image = Image.FromFile(Application.StartupPath + fileName)));
                else
                    pc_logo.Image = Image.FromFile(Application.StartupPath + fileName);

                Panel parentPanel = null;
                if (typeShow == 1)
                {
                    if (btnClosePanel.InvokeRequired) btnClosePanel.Invoke((MethodInvoker)delegate { btnClosePanel.Visible = true; });
                    else btnClosePanel.Visible = true;
                    if (btnConfirmWaitingForm.InvokeRequired) btnConfirmWaitingForm.Invoke((MethodInvoker)delegate { btnConfirmWaitingForm.Visible = true; });
                    else btnConfirmWaitingForm.Visible = true;
                    parentPanel = pnlMain;
                }
                else
                {
                    if (btnClosePanel.InvokeRequired) btnClosePanel.Invoke((MethodInvoker)delegate { btnClosePanel.Visible = false; });
                    else btnClosePanel.Visible = false;
                    if (btnConfirmWaitingForm.InvokeRequired) btnConfirmWaitingForm.Invoke((MethodInvoker)delegate { btnConfirmWaitingForm.Visible = false; });
                    else btnConfirmWaitingForm.Visible = false;
                    parentPanel = pnlMain;
                }

                int panelX = 0;
                int panelY = 0;

                if (pnlWaittingForm.InvokeRequired)
                    pnlWaittingForm.Invoke((MethodInvoker)delegate
                    {
                        if (pnlWaittingForm.Parent != parentPanel)
                            pnlWaittingForm.Parent = parentPanel;
                        // TODO: 090925 - NB
                        //panelX = (parentPanel.Width - pnlWaittingForm.Width) / 2;
                        //panelY = (parentPanel.Height - pnlWaittingForm.Height) / 2;
                        panelX = parentPanel.Width - pnlWaittingForm.Width - 3;
                        panelY = parentPanel.Height - pnlWaittingForm.Height - 22;

                        pnlWaittingForm.Left = panelX;
                        pnlWaittingForm.Top = panelY;
                        pnlWaittingForm.Anchor = AnchorStyles.None;
                        pnlWaittingForm.BringToFront();
                        pnlWaittingForm.Visible = true;
                    });
                else
                {
                    if (pnlWaittingForm.Parent != parentPanel)
                        pnlWaittingForm.Parent = parentPanel;
                    // TODO: 090925 - NB
                    //panelX = (parentPanel.Width - pnlWaittingForm.Width) / 2;
                    //panelY = (parentPanel.Height - pnlWaittingForm.Height) / 2;
                    panelX = parentPanel.Width - pnlWaittingForm.Width - 3;
                    panelY = parentPanel.Height - pnlWaittingForm.Height - 22;

                    pnlWaittingForm.Left = panelX;
                    pnlWaittingForm.Top = panelY;
                    pnlWaittingForm.Anchor = AnchorStyles.None;
                    pnlWaittingForm.BringToFront();
                    pnlWaittingForm.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }
        #endregion

        #region TẮT CẢNH BÁO
        /// <summary>
        /// đóng thông báo
        /// </summary>
        private void ClosePanel()
        {
            try
            {
                if (pnlWaittingForm.InvokeRequired)
                    pnlWaittingForm.Invoke((MethodInvoker)delegate
                    {
                        ckb_isTightenForce.Visible = false;
                        pnlWaittingForm.Visible = false;
                        pnlWaittingForm.Dock = DockStyle.None;
                        pnlWaittingForm.SendToBack();
                    });
                else
                {
                    ckb_isTightenForce.Visible = false;
                    pnlWaittingForm.Visible = false;
                    pnlWaittingForm.Dock = DockStyle.None;
                    pnlWaittingForm.SendToBack();
                }
            }
            catch (Exception ex)
            {
                Lib.saveToErr(ex.ToString());
            }
        }

        bool IsConfirm_ConcentricManual = false;
        private void btnConfirmWaitingForm_Click(object sender, EventArgs e)
        {
            ClosePanel();
            if (IsConfirm_ConcentricManual)
            {
                IsConfirm_ConcentricManual = false;
                SetBitsOn("D260.0");
                if (btnRunMode_Auto.InvokeRequired)
                    btnRunMode_Auto.Invoke((MethodInvoker)delegate { btnRunMode_Auto.Enabled = false; });
                else btnRunMode_Auto.Enabled = false;
                if (btnRunMode_Manual.InvokeRequired)
                    btnRunMode_Manual.Invoke((MethodInvoker)delegate { btnRunMode_Manual.Enabled = false; });
                else btnRunMode_Manual.Enabled = false;
            }

            if (Is_OpenFrontLight) Is_OpenFrontLight = false;
        }

        private void btnClosePanel_Click(object sender, EventArgs e)
        {
            ClosePanel();
        }
        #endregion

        #endregion

        #region BUTTON CLICK NEW
        bool isCloseGrind = true;
        private void btnGrindInfor_Click(object sender, EventArgs e)
        {
            Lib.UpdateVisibleControl(pnGrindInfor, !isCloseGrind);
            Lib.UpdateVisibleControl(pnSettingInfor, !isCloseGrind);
            isCloseGrind = !isCloseGrind;
        }

        private void btnClose_GrindingInfor_Click(object sender, EventArgs e)
        {
            Lib.UpdateVisibleControl(pnGrindInfor, false);
            isCloseGrind = true;
        }


        double DistanceOriginalMin = 0;
        double DistanceOriginalMax = 0;
        bool get_Diameter_Manual = false;
        List<double> lstDuongKinh = new List<double>();
        private void btnGetDiameter_Click(object sender, EventArgs e)
        {
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang lấy đường kính viên...");
            wait.Show();
            try
            {
                try
                {
                    if (Is_OpenFrontLight)
                    {
                        WaitDialogForm wait1 = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Back Light");
                        wait1.Show();
                        Open_BackLight();
                        Is_OpenFrontLight = false;
                        wait1.Close();
                    }
                }
                catch (Exception)
                {
                }

                Lib.UpdateTextBoxText(txtMax, string.Empty);
                Lib.UpdateTextBoxText(txtMin, string.Empty);
                // TODO: 191125 - NB
                if (!chkResizeTarget.Checked)
                {
                    Lib.UpdateTextBoxText(txtDistance_Target_Milimet, string.Empty);
                    Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, string.Empty);
                }

                SmartWindow.LockRoi = false;
                if (cDeviceManager.HikCamera == null || !cDeviceManager.HikCamera.IsOpen)
                {
                    ShowPanel_WaitingForm("Xin vui lòng kết nối Camera!", 2);
                    wait.Close();
                    return;
                }

                List<cRectangType> lstCircleRec_RoiCircle = ROIS.Values.ToList().FindAll(x => x.ActionID == 10);
                if (lstCircleRec_RoiCircle.Count == 1)
                {
                    ROIS.Remove(lstCircleRec_RoiCircle[0].ID);
                    DrawingObjects_Circle.Remove(lstCircleRec_RoiCircle[0].ID);
                }
                if (SmartWindow.m_fTOP <= 0 && !IsShowROIs_Full)
                    LoadShapeDiamond();

                #region TÌM MIN ĐƯỜNG KÍNH KIM CƯƠNG
                //TODO: 140525 - HieuDV
                if (!cDeviceManager._plcMC.Connected) return;
                if (!cDeviceManager.GetBits("M415")) //KIỂM TRA ĐÃ VẶN SANG CHẠY AUTO HAY CHƯA
                {
                    GetDiameter();
                    wait.Close();
                    return;
                }
                SetBitsOn("D222.0");

                if (!isFindMin)
                {
                    lstDuongKinh = new List<double>();
                    if (!cDeviceManager.GetBits("D218.0"))//plc gửi bit bắt đầu proces tìm min
                    {
                        ShowPanel_WaitingForm("Vui lòng nhấn [START] để bắt đầu chu trình!", 2);
                        wait.Close();
                        return;
                    }

                    // TODO: 031025 - NB - gửi số lần lấy min / max xuống PLC 
                    cDeviceManager._plcMC.SetSingleRegister_Int32("D3196", Lib.ToInt(numCountSet_Min_Max.Value));

                    ClosePanel();
                    SetBitsOn("D218.1");//gửi bit xác nhận cho PLC
                    isFindMin = true;
                }
                #endregion

                // TODO: 031025 - NB
                btnStartProcess.Enabled = true;
                btnGetDiameter.Appearance.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
                btnGetDiameter.Appearance.ForeColor = Color.White; // Chữ trắng
            }
            catch (Exception)
            {

            }
            wait.Close();
        }

        int IsFist_Process = 0;
        bool isFindMin = false;
        void FindMin()
        {
            while (true)
            {
                //Thread.Sleep(10);
                if (!isFindMin)
                {
                    Thread.Sleep(200);
                    continue;
                }
                else
                    Thread.Sleep(10);
                //if (!isFindMin) continue;

                SetBitsOn("D222.0");//READY
                SetBitsOff("D222.1");//BUSY
                SetBitsOff("D220.1");//gửi oke sau mỗi lần xử lí
                if (cDeviceManager.GetBits("D220.0"))
                {
                    SetBitsOff("D222.0");//READY
                    SetBitsOn("D222.1");//BUSY
                    SetBitsOff("D218.1");
                    get_Diameter_Manual = true;

                    // TODO: 060825 - NB
                    //LineFindRun();
                    LineFindRun_Grinding(get_Diameter_Manual);

                    get_Diameter_Manual = false;
                    lstDuongKinh.Add(_distanceGrinding);
                    SetBitsOn("D220.1");//gửi oke sau mỗi lần xử lí

                    // TODO: 031025 - NB
                    if (lstDuongKinh.Count == Lib.ToInt(numCountSet_Min_Max.Value)) //đủ 10 lần là hoàn thành
                    {
                        SetBitsOn("D218.2");//gửi bit hoàn thành
                        SetBitsOn("D222.0");//READY
                        SetBitsOff("D222.1");
                        isFindMin = false;

                        double distanceGrinding = lstDuongKinh.Min() - Lib.ToDouble(txtCutting_Value.Text.Trim());
                        Lib.UpdateTextBoxText(txtMin, (lstDuongKinh.Min() / 1000).ToString("F3"));
                        Lib.UpdateTextBoxText(txtMax, (lstDuongKinh.Max() / 1000).ToString("F3"));

                        // TODO: 191125 - NB
                        if (!chkResizeTarget.Checked)
                        {
                            // đường kính mài thực tế
                            Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, distanceGrinding.ToString("F0"));
                            Lib.UpdateTextBoxText(txtDistance_Target_Milimet, (distanceGrinding / 1000).ToString("F3"));
                        }

                        SetBitsOff("D218.2");//gửi bit hoàn thành

                        //original size
                        DistanceOriginalMin = lstDuongKinh.Min() / 1000;
                        DistanceOriginalMax = lstDuongKinh.Max() / 1000;

                        if (pnGrindInfor.InvokeRequired)
                            pnGrindInfor.Invoke((MethodInvoker)delegate
                            {
                                btnStartProcess.Enabled = true;
                                btnGetDiameter.Appearance.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
                                btnGetDiameter.Appearance.ForeColor = Color.White; // Chữ trắng
                            });
                        else
                        {
                            btnStartProcess.Enabled = true;
                            btnGetDiameter.Appearance.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
                            btnGetDiameter.Appearance.ForeColor = Color.White; // Chữ trắng
                        }
                        //TODO : HuyNT - 12062025
                        KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                        txtDistanceRequest_Milimet_KeyDown(null, args);

                        if (distanceGrinding == 0)
                        {
                            ShowPanel_WaitingForm("Xin vui lòng kiểm tra lại đèn Light hoặc điều chỉnh ROIs kim cương ...!", 2);
                            return;
                        }

                        SmartWindow_DoubleClick(null, null);
                        SmartWindow.m_bCheckMouseAll = true;
                        SmartWindow_MouseMove(null, null);
                        SmartWindow.m_bCheckMouseAll = false;

                        IsFist_Process = 0;
                        ShowPanel_WaitingForm("Vui lòng nhấn [Start Process] để tiếp tục đánh giá viên kim cương tiếp theo!", 2);
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void GetDiameter()
        {
            get_Diameter_Manual = true;
            // TODO: 060826 - NB
            //LineFindRun();
            LineFindRun_Grinding(true);
            get_Diameter_Manual = false;

            if (!chkResizeTarget.Checked)
            {
                // đường kính mài thực tế
                Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, _distanceGrinding.ToString("F0"));
                Lib.UpdateTextBoxText(txtDistance_Target_Milimet, (_distanceGrinding / 1000).ToString("F3"));
            }

            //original size
            DistanceOriginalMin = _distanceGrinding / 1000;
            DistanceOriginalMax = _distanceGrinding / 1000;

            if (_distanceGrinding == 0)
            {
                ShowPanel_WaitingForm("Xin vui lòng kiểm tra lại đèn Light hoặc điều chỉnh ROIs kim cương ...!", 2);
                return;
            }

            SmartWindow_DoubleClick(null, null);
            SmartWindow.m_bCheckMouseAll = true;
            SmartWindow_MouseMove(null, null);
            SmartWindow.m_bCheckMouseAll = false;

            ShowPanel_WaitingForm("Vui lòng nhấn [Start Process] để bắt đầu chu trình!", 2);
        }

        bool IsLiveCamera_Auto = false;
        private void btnLiveCamera_Click(object sender, EventArgs e)
        {
            if (!IsLiveCamera_Auto)
            {
                firstShow = IsLiveCamera_Auto = IsLive = true;
                Thread.Sleep(100);
                if (!SetLiveCamera_Hikrobot(IsLive))
                {
                    Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Error Camera", Color.Red, Color.White);
                    return;
                }

                // TODO: 120825 - NB
                cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(exposureTimeLive);
                cDeviceManager.HikCamera.MV_CC_SetGain_NET(gainCameraLive);
                cDeviceManager.HikCamera.MV_CC_SetGamma_NET(gammaCameraLive);

                btnLiveCamera.Appearance.BackColor = Color.Green;
                btnLiveCamera.Appearance.ForeColor = Color.White; // Chữ trắng

                btnSnapCamera.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
                btnSnapCamera.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
                btnSnapCamera.Appearance.Options.UseBackColor = false;
                btnSnapCamera.Appearance.Options.UseForeColor = false;
            }
            else
            {
                firstShow = IsLiveCamera_Auto = false;
                if (!SetLiveCamera_Hikrobot(false))
                {
                    Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                    return;
                }
                Thread.Sleep(100);
                IsLive = false;

                // TODO: 120825 - NB
                cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(exposureTimeLive);
                cDeviceManager.HikCamera.MV_CC_SetGain_NET(gainCameraLive);
                cDeviceManager.HikCamera.MV_CC_SetGamma_NET(gammaCameraLive);

                // TODO: 081224
                cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
                btnLiveCamera.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
                btnLiveCamera.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
                btnLiveCamera.Appearance.Options.UseBackColor = false;
                btnLiveCamera.Appearance.Options.UseForeColor = false;
            }
            //btnGetDiameter.Enabled = btnStartProcess.Enabled = btnCalib.Enabled = btnSettingVision.Enabled = !IsLive;
        }
        private void btnSnapCamera_Click(object sender, EventArgs e)
        {
            if (cDeviceManager.HikCamera == null) return;
            if (IsLiveCamera_Auto)
            {
                btnLiveCamera_Click(null, null);
            }
            // TODO: 010125 - NB
            if (bitmap_Live != null)
            {
                if (SmartWindow.InvokeRequired)
                {
                    SmartWindow.Invoke(new Action(() =>
                    {
                        SmartWindow.Image = bitmap_Live;
                    }));
                }
                else
                    SmartWindow.Image = bitmap_Live;
            }

            InputImage = bitmap_Live;
            if (InputImage == null) return;
            Bitmap bitmapImage = null;
            bitmapImage = (Bitmap)InputImage.Clone();
            SmartWindow_DoubleClick(null, null);
            GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)bitmapImage.Clone());

            btnStartProcess.Visible = btnCalib.Enabled = btnSettingVision.Enabled = !IsLive;
            btnSnapCamera.Appearance.BackColor = Color.Green;
            btnSnapCamera.Appearance.ForeColor = Color.White; // Chữ trắng

            btnLiveCamera.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
            btnLiveCamera.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
        }
        #endregion

        #region OTHER + KEYDOWN / KEY PRESS

        #region SET BIT ON/OFF
        /// <summary>
        /// Hàm để set ON các bit cụ thể (đặt bit thành 1)
        /// </summary>
        /// <param name="deviceName"> địa chỉ thanh ghi</param>
        /// <param name="bitPositions"> danh sách vị trí cần on</param>
        public void SetBitsOn(string deviceName)
        {
            try
            {
                if (cDeviceManager._plcMC == null) return;
                deviceName = deviceName.Replace(" ", "").Trim();
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    // Đọc giá trị hiện tại của thanh ghi
                    int currentValue;
                    cDeviceManager._plcMC.GetSingleRegister_Int16(split[0], out currentValue);
                    // Bật các bit cụ thể   
                    foreach (int bitPosition in split[1])
                    {
                        currentValue |= (1 << Lib.ToInt(split[1])); // Đặt bit thành 1
                    }
                    // Ghi lại giá trị vào thanh ghi
                    cDeviceManager._plcMC.SetSingleRegister_Int16(split[0], currentValue);
                }
                else
                    cDeviceManager._plcMC.SetSingleBit(deviceName, true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Hàm để set OFF các bit cụ thể (đặt bit thành 0)
        /// </summary>
        /// <param name="deviceName"> địa chỉ thanh ghi</param>
        /// <param name="bitPositions"> danh sách vị trí cần off</param>
        public void SetBitsOff(string deviceName)
        {
            try
            {
                if (cDeviceManager._plcMC == null) return;
                deviceName = deviceName.Replace(" ", "").Trim();
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    // Đọc giá trị hiện tại của thanh ghi
                    int currentValue;
                    cDeviceManager._plcMC.GetSingleRegister_Int16(split[0], out currentValue);
                    // Tắt các bit cụ thể
                    foreach (int bitPosition in split[1])
                    {
                        currentValue &= ~(1 << Lib.ToInt(split[1])); // Đặt bit thành 0
                    }
                    // Ghi lại giá trị vào thanh ghi
                    cDeviceManager._plcMC.SetSingleRegister_Int16(split[0], currentValue);
                }
                else
                    cDeviceManager._plcMC.SetSingleBit(deviceName, false);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Hàm để set OFF các bit cụ thể (đặt bit thành 0)
        /// </summary>
        /// <param name="deviceName"> địa chỉ thanh ghi</param>
        /// <param name="bitPositions"> danh sách vị trí cần off</param>
        public bool GetBits(string deviceName)
        {
            deviceName = deviceName.ToUpper();
            bool result = false;
            try
            {
                if (cDeviceManager._plcMC == null) return false;
                deviceName = deviceName.Replace(" ", "").Trim();
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    int value = 16;
                    cDeviceManager._plcMC.GetSingleRegister_Int16(split[0], out value);
                    BitArray bitArray = new BitArray(new int[] { value });
                    result = bitArray[Lib.ToInt(split[1])];
                }
                else
                    cDeviceManager._plcMC.GetSingleBit(deviceName, out result);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        #endregion
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaitDialogForm waitDialog = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang ngắt kết nối thiết bị");
            waitDialog.Show();
            IsRun = false;
            show_progress = 0;
            DisconnectCamera_Hikrobot();
            AbortThread(_checkTriggerConcentric);
            AbortThread(_functionMeasure);
            AbortThread(_runFunctionInspect);
            AbortThread(_functionMeasureManual);
            AbortThread(_reconnectPLC_MC);
            AbortThread(_findMin);
            AbortThread(_process_ReadDataPLC);
            AbortThread(_mapIO);

            SetBitsOff("D202.0"); //ready
            SetBitsOff("D202.1"); //busy
            SetBitsOff("D202.2"); //OK
            SetBitsOff("D202.3"); //NG               
                                  //SetBitsOff("D202.4"); //ByPass

            SetBitsOff("D206.0"); //ready
            SetBitsOff("D206.1"); //busy
            SetBitsOff("D206.2"); //OK
            SetBitsOff("D206.3"); //NG

            SetBitsOff("D210.0"); //ready
            SetBitsOff("D210.1"); //busy
            SetBitsOff("D210.2"); //OK
            SetBitsOff("D210.3"); //NG

            //TODO : HuyNT - 230525
            SetBitsOff("D218.1"); //pm gửi tín hiệu get diameter
            SetBitsOff("D218.2"); //complete get diameter

            //TODO : HuyNT 10062025
            SetBitsOff("D202.5");

            cDeviceManager.Disconnect_PLC();
            waitDialog.Close();
        }
        double _distanceRequest = 0;
        private void txtDistanceRequest_Milimet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            _distanceRequest = Lib.ToDouble(txtDistance_Target_Milimet.Text.Trim()) * 1000;
            Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, _distanceRequest.ToString("F0"));
            Lib.UpdateTextBoxText(txtDistance_Target_Milimet, Lib.ToDouble(txtDistance_Target_Milimet.Text.Trim()).ToString("F3"));
            try
            {
                double _distanceShapeDiamond = Lib.ToDouble(SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y) * 2 * PIXEL_TO_MM;
                float distanceDiamondMove = (float)((_distanceShapeDiamond - _distanceRequest) / (2 * PIXEL_TO_MM));

                SmartWindow.m_pTableUp.Y = SmartWindow.m_pTableUp.Y - distanceDiamondMove; // trục a
                SmartWindow.m_pTable.Y = SmartWindow.m_pTable.Y - distanceDiamondMove; // trục b
                SmartWindow.m_pLowerGirdle.Y = SmartWindow.m_pLowerGirdle.Y - distanceDiamondMove;
                SmartWindow.m_pLowerGirdleUp.Y = SmartWindow.m_pLowerGirdleUp.Y - distanceDiamondMove;
                SmartWindow.m_pUpperGirldleUp.Y = SmartWindow.m_pUpperGirldleUp.Y - distanceDiamondMove;
                SmartWindow.m_pUpperGirdle.Y = SmartWindow.m_pUpperGirdle.Y - distanceDiamondMove;

                // TODO: 091224 - NB
                shape_diamond_infor shapeDiamond = _mySQLHelper.FindAll<shape_diamond_infor>().FirstOrDefault();
                shapeDiamond.CuletX = Lib.ToDecimal(SmartWindow.m_pCulet.X);
                shapeDiamond.CuletY = Lib.ToDecimal(SmartWindow.m_pCulet.Y);
                shapeDiamond.LowerGirdleX = Lib.ToDecimal(SmartWindow.m_pLowerGirdle.X);
                shapeDiamond.LowerGirdleY = Lib.ToDecimal(SmartWindow.m_pLowerGirdle.Y);
                shapeDiamond.TableX = Lib.ToDecimal(SmartWindow.m_pTable.X);
                shapeDiamond.TableY = Lib.ToDecimal(SmartWindow.m_pTable.Y);
                try
                {
                    if (shapeDiamond != null)
                        _mySQLHelper.Update<shape_diamond_infor>(shapeDiamond);
                    else
                        _mySQLHelper.Insert<shape_diamond_infor>(shapeDiamond);
                }
                catch (Exception ex)
                {
                }
                SmartWindow_DoubleClick(null, null);
                SmartWindow.m_bCheckMouseAll = true;
                SmartWindow_MouseMove(null, null);
                SmartWindow.m_bCheckMouseAll = false;
                SmartWindow_MouseUp(null, null);
            }
            catch (Exception)
            {
            }
            // TODO: 090925 - NB
            if (txtAllowableDeviation_Milimet.InvokeRequired)
                txtAllowableDeviation_Milimet.Invoke((MethodInvoker)delegate
                { txtAllowableDeviation_Milimet.Focus(); });
            else
                txtAllowableDeviation_Milimet.Focus();
            SmartWindow.m_bCheckMouseAll = false;
            SmartWindow_MouseUp(null, null);
        }
        private void txtDistanceRequest_Milimet_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự có phải là số hoặc dấu chấm hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }
        private void txtHeightGrindLayer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }
        double _allowableDeviation = 0;
        private void txtAllowableDeviation_Milimet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            try
            {
                // Bổ sung thêm lưu log lại khi start chương trình
                // TODO: 090925 - NB  
                _config_data.AllowableDeviation_MM = txtAllowableDeviation_Milimet.Text.Trim();
                if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
            }
            catch { }
            _allowableDeviation = Lib.ToDouble(txtAllowableDeviation_Milimet.Text.Trim()) * 1000;
            Lib.UpdateTextBoxText(txtAllowableDeviation_Micromet, $"{_allowableDeviation}");

            // TODO: 090925 - NB  
            if (txtDiamondCode.InvokeRequired)
                txtDiamondCode.Invoke((MethodInvoker)delegate
                { txtDiamondCode.Focus(); });
            else
                txtDiamondCode.Focus();
        }

        private void txtAllowableDeviation_Milimet_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự có phải là số hoặc dấu chấm hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }
        private void txtValueConvert_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }

        private void txtFromTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }

        private void txtLess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }
        public static void AbortThread(Thread thread)
        {
            try
            {
                if (thread != null)
                {
                    thread.Abort();
                    Thread.Sleep(100);
                    thread.Join();
                }
            }
            catch (Exception ex)
            {
            }
        }

        int modeRunning = 1;
        private void btnRunMode_Auto_Click(object sender, EventArgs e)
        {
            modeRunning = 1;
            // TODO: 020726 - NB
            Is_Semi_Bypass_Inspect = false;
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
            {
                // TODO: 070526 - NB
                SetBitsOff("D252.0");
                SetBitsOff("D252.1");
                cDeviceManager._plcMC.SetSingleRegister_Int16("D250", modeRunning);
                groupBox10.Enabled = false;
            }
            ShowButtonRunning(modeRunning);
            Lib.UpdateSimpleButtonEnable(btnStartProcess, true);
        }

        private void btnRunMode_Manual_Click(object sender, EventArgs e)
        {
            modeRunning = 4;
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int16("D250", modeRunning);
            ShowButtonRunning(modeRunning);
            Lib.UpdateSimpleButtonEnable(btnStartProcess, false);
            groupBox10.Enabled = true;
        }

        private void rdNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNormal.Checked)
            {
                // TODO: 020726 - NB
                Is_Semi_Bypass_Inspect = false;
                rdBypassInspect.Checked = false;
                if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                {
                    SetBitsOff("D252.1");
                    SetBitsOn("D252.0");
                }
            }
        }

        // TODO: 020726 - NB
        bool Is_Semi_Bypass_Inspect = false;
        private void rdBypassInspect_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBypassInspect.Checked)
            {
                rdNormal.Checked = false;
                // TODO: 020726 - NB
                Is_Semi_Bypass_Inspect = true;

                if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                {
                    // TODO: 020726 - NB
                    //SetBitsOff("D252.0");
                    //SetBitsOn("D252.1");

                    // TODO: 020726 - NB - CHUYỂN SANG VẪN CHO PHÂN LOẠI NHƯNG KHÔNG ĐO CÁC GÓC XUNG QUANH
                    SetBitsOff("D252.1");
                    SetBitsOn("D252.0");
                }
            }
        }

        private void ShowButtonRunning(int mode)
        {
            // TODO: 070526 - NB
            switch (mode)
            {
                case 1:
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Auto, Color.Green, Color.White);
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Manual, Color.Transparent, Color.Black);
                    rdNormal.Enabled = rdBypassInspect.Enabled = false;
                    break;
                case 2:
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Auto, Color.Orange, Color.White);
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Manual, Color.Transparent, Color.Black);
                    rdNormal.Enabled = rdBypassInspect.Enabled = false;
                    break;
                case 4:
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Manual, Color.Red, Color.White);
                    Lib.UpdateButton_BColor_FColor(btnRunMode_Auto, Color.Transparent, Color.Black);
                    rdNormal.Enabled = rdBypassInspect.Enabled = true;
                    rdNormal.Checked = true;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region KHU VỰC CHỈNH PLC TẠI MÀN HÌNH GÓC TRÁI
        private void btnDiamondSpin_ON_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_OFF, Color.White, Color.Black);
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_ON, Color.LimeGreen, Color.White);
            string address = "D7005.1";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(btnDiamondSpin_ON, address, 2000);
        }

        private void btnDiamondSpin_OFF_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_ON, Color.White, Color.Black);
            Lib.UpdateButton_BColor_FColor(btnDiamondSpin_OFF, Color.LimeGreen, Color.White);
            string address = "D7005.5";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(btnDiamondSpin_OFF, address, 2000);
        }

        private void btnRotaryGrinding_Setup_ON_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_OFF, Color.White, Color.Black);
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_ON, Color.LimeGreen, Color.White);
            string address = "R2000.0";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(btnRotaryGrinding_Setup_ON, address, 2000);
        }

        private void btnRotaryGrinding_Setup_OFF_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_ON, Color.White, Color.Black);
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_OFF, Color.LimeGreen, Color.White);
            string address = "R2000.1";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(btnRotaryGrinding_Setup_OFF, address, 2000);
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

        private void btnRotaryGrinding_Setup_Reset_Click(object sender, EventArgs e)
        {
            Lib.UpdateButton_BColor_FColor(btnRotaryGrinding_Setup_Reset, Color.LimeGreen, Color.White);
            string address = "M404";
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(btnRotaryGrinding_Setup_Reset, address, 2000, true);
        }

        private async void ResetOffPLC_ButtonClick(Button btn, string address, int delay, bool isClear_BackColor = false)
        {
            await Task.Delay(delay);
            cDeviceManager.SetBitsOff(address);
            if (isClear_BackColor)
                Lib.UpdateButton_BColor_FColor(btn, Color.White, Color.Black);
        }
        #endregion

        #region XỬ LÍ CALIB
        Blob_Muilti_ROI.BlobTool Blobs = new Blob_Muilti_ROI.BlobTool();
        private void btnCalib_Click(object sender, EventArgs e)
        {
            register_location_rois _registerRoi;
            foreach (cRectangType ROI in this.ROIS.Values.ToList().FindAll(x => x.ActionID == 10))
            {
                _registerRoi = _mySQLHelper.FindByField<register_location_rois>("IDAction", 10);
                _registerRoi.CenterX = Lib.ToInt(ROI.Center.Row);
                _registerRoi.CenterY = Lib.ToInt(ROI.Center.Col);
                _registerRoi.Phi = Lib.ToInt(ROI.Phi);
                _registerRoi.Width = Lib.ToInt(ROI.Width);
                _registerRoi.Height = Lib.ToInt(ROI.Height);
                try
                {
                    if (_registerRoi.IDGuid != null && _registerRoi.IDGuid != "0")
                        _mySQLHelper.Update<register_location_rois>(_registerRoi);
                    else
                    {
                        _registerRoi.IDGuid = ROI.ID.ToString();
                        _registerRoi.IDAction = ROI.ActionID;
                        _mySQLHelper.Insert<register_location_rois>(_registerRoi);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            #region TÍNH PIXEL TO MM
            SetOrigin.SetOrigin BlobROI = new SetOrigin.SetOrigin();
            List<RTCRectangle> listROI = new List<RTCRectangle>();
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), (double)0);

            var DataROI = SmartWindow.GetDataRoi();
            RTCRectangle _ROI = new RTCRectangle();
            _ROI.Center = new RTCPoint(DataROI.Item2, DataROI.Item3, 0);
            _ROI.Phi = (double)DataROI.Item6 * Math.PI / 180;
            _ROI.Width = (double)DataROI.Item4 * 2;
            _ROI.Height = (double)DataROI.Item5 * 2;
            listROI.Add(_ROI);

            Blobs.ROI = listROI;
            var InSetOrigin = BlobROI.Run(listROI, toolOrigin);
            Blobs.ToolOrigin = toolOrigin;
            Blobs.InSetOrigin = InSetOrigin;
            //Blobs.InputImage = inputImage.Clone();
            Blobs.InputImage = GlobFuncs.BitmapToGrayImage((Bitmap)SmartWindow.Image.Clone());
            Blobs.DetectType = cBlobTool.DectectType_DetectInRangeBlobs;
            Blobs.FillHoles = true;
            Blobs.GreyLevelThresholdType = cBlobTool.GreyLevelThresholdType_FixedThresholdRange;
            Blobs.ThresholdRange = Tuple.Create(0, 150);
            Blobs.RequireNumberOfBlobs = Tuple.Create(0, 100);
            Blobs.EnableAreaFilter = true;
            Blobs.AreaRange = Tuple.Create(100.0, 99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999.0);
            Blobs.IsShowImageResult = true;
            var result = Blobs.Run();
            int Height = Blobs.OutputHeightList.Max();
            double scale = double.Parse(cbStoneSize.Text) / Height;

            Lib.UpdateTextBoxText(txtRatio_Pixel_To_Mm, (scale * 1000).ToString());
            if (SmartWindow.InvokeRequired)
            {
                SmartWindow.Invoke(new Action(() =>
                {
                    SmartWindow.Image = Blobs.OutputImage;
                }));
            }
            else
                SmartWindow.Image = Blobs.OutputImage;

            PIXEL_TO_MM = Lib.ToDouble(txtRatio_Pixel_To_Mm.Text);
            #endregion

            // TODO: 090925 - NB 
            _config_data.Ratio_Pixel_MM = txtRatio_Pixel_To_Mm.Text.Trim();
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        Image<Gray, byte> inputImage;
        bool IsCalib = false;
        private void btnSnapCalib_Click(object sender, EventArgs e)
        {
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang snap image");
            wait.Show();
            try
            {
                if (IsLive)
                {
                    if (!SetLiveCamera_Hikrobot(IsLive))
                    {
                        Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Disconnected", Color.Red, Color.White);
                        wait.Close();
                        return;
                    }
                    Thread.Sleep(100);
                    IsLive = false;
                    Lib.UpdateButtonText_BColor_FColor(btnStatusCamera, "Connected", Color.Green, Color.White);

                    // TODO: 120825 - NB
                    cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(exposureTimeLive);
                    cDeviceManager.HikCamera.MV_CC_SetGain_NET(gainCameraLive);
                    cDeviceManager.HikCamera.MV_CC_SetGamma_NET(gammaCameraLive);

                    // TODO: 081224
                    cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
                }

                SmartWindow.m_fTOP = 0;
                SmartWindow.m_fBOT = 0;
                SmartWindow.m_fCrow = 0;
                SmartWindow.m_fGirdle = 0;
                SmartWindow.m_pCulet = new PointF(0, 0);
                SmartWindow.m_pTable = new PointF(0, 0);
                SmartWindow.m_pLowerGirdle = new PointF(0, 0);

                // TODO: 250925 - NB
                IsCalib = true;
                LoadROICircle_Calib();

                // TODO: 271124
                if (cDeviceManager.HikCamera == null) return;
                //Bitmap bitmap_Camera = new Bitmap(@"C:\Users\ADMIN\MVS\Data\Image_20241212155911853.bmp");
                Bitmap bitmap_Camera = cDeviceManager.HikCamera.SingleShotGrabbing();
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

                Lib.UpdateSimpleButtonEnable(btnLock, true);
                Lib.UpdateSimpleButtonEnable(btnCalib, true);
                IsCalib = false;

                SmartWindow_DoubleClick(null, null);
                SmartWindow.m_bCheckMouseAll = true;
                SmartWindow_MouseMove(null, null);
                SmartWindow.m_bCheckMouseAll = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            wait.Close();
        }

        List<register_location_rois> lstROICircle_Calib;
        void LoadROICircle_Calib()
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
                }
                else
                {
                    Rec.Center.Row = SmartWindow.Image.Width / 2;
                    Rec.Center.Col = SmartWindow.Image.Height / 2;
                }
                ROI = new GraphicsWindow.GraphicsWindow.DataRoi(GraphicsWindow.GraphicsWindow.ERoiTypes.Rectangle, (float)Rec.Center.Row, (float)Rec.Center.Col, (float)Rec.Width, (float)Rec.Height, (float)Rec.Phi,
                   GraphicsWindow.GraphicsWindow.EConnectTypes.None, Color.Red, GraphicsWindow.GraphicsWindow.ECentreTypes.TheCenterDoesNotChange, ID);
                Rec.ID = ROI.Key;

                // TODO: 250925 - NB
                List<cRectangType> lstCircleRec_RoiCircle = ROIS.Values.ToList().FindAll(x => x.ActionID == 10);
                if (lstCircleRec_RoiCircle.Count == 1)
                {
                    ROIS.Remove(Rec.ID);
                    DrawingObjects_Circle.Clear();
                }

                ROIS.Add(Rec.ID, Rec);
                DrawingObjects_Circle.Add(ROI.Key, ROI);
            }

            // TODO: 250925 - NB
            //if (!IsShowROIs_Full || btnSaveRois.Visible == true) return;

            if (!IsCalib) return;
            SmartWindow.ClearAllRoi();

            List<cRectangType> lstCircleRec = ROIS.Values.ToList().FindAll(x => x.ActionID == 10);
            if (lstCircleRec.Count < 1)
            {
                CreateROICircle(-1, -1, 180, 10, 0);
            }
            else
            {
                List<GraphicsWindow.GraphicsWindow.DataRoi> lst = new List<GraphicsWindow.GraphicsWindow.DataRoi>();
                foreach (cRectangType roi in lstCircleRec)
                {
                    if (roi.ActionID != 10) continue;
                    lst.Add(DrawingObjects_Circle[roi.ID]);
                }
                SmartWindow.DrawROI(lst);
            }
        }
        private void CreateROICircle(int col, int row, int phi = 0, int IDAction = 10, int NumPosition = 1)
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
            DrawingObjects_Circle.Add(ROI.Key, ROI);
            AddROIProperties(ROI.Key, ROIS.Count <= 1, IDAction);
            SmartWindow.AddRoi(ROI);
        }
        public void AddROIProperties(Guid _ID, bool _IsSelected = false, int ActionID = 0)
        {
            if (_IsSelected)
                foreach (cROIProperty roiProperty in ROIProperties_Parameters.Values)
                    roiProperty.Selected = false;
            cROIProperty crp = new cROIProperty(_ID, _IsSelected, ActionID);
            if (ROIProperties_Parameters.Count <= 0)
                crp.Selected = true;
            ROIProperties_Parameters.Add(_ID, crp);
        }
        private void btnLock_Click(object sender, EventArgs e)
        {
            btnLock.Appearance.BackColor = Color.Empty;
            btnCalib.Appearance.Options.UseBackColor = false;
            if (btnLock.Text == "Lock")
            {
                Lib.UpdateSimpleButtonText(btnLock, "Unlock");
                btnLock.Appearance.BackColor = Color.FromArgb(0, 200, 83); // Xanh lá cây tươi
                btnLock.Appearance.ForeColor = Color.White; // Chữ trắng
                SmartWindow.LockRoi = false;
            }
            else
            {
                Lib.UpdateSimpleButtonText(btnLock, "Lock");
                btnLock.Appearance.BackColor = Color.Red;
                btnLock.Appearance.ForeColor = Color.White; // Chữ trắng
                btnCalib.Appearance.Options.UseBackColor = false;
                SmartWindow.LockRoi = true;
            }
        }
        #endregion

        #region VẼ THÊM ĐƯỜNG GIAO NHAU
        private void LineFindRun_Crow_TotalDept()
        {
            Tuple<PointF, double> toolOrigin = Tuple.Create(new PointF(0, 0), 0.0);

            List<RTCRectangle> ROIs = new List<RTCRectangle>();
            int count = 0;
            RTCRectangle Data = new RTCRectangle();
            GrayImage = GlobFuncs.BitmapToGrayImage((Bitmap)InputImage.Clone());
            DisplayImage = GrayImage.Convert<Bgr, byte>();
            if (DisplayImage == null) return;

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
                LineFind.EdgeTransition = ROIProperties_Parameters[OldID].EdgeTransition;
                LineFind.EdgeType = ROIProperties_Parameters[OldID].EdgeType;
                LineFind.EdgeDetectionThreshold = Lib.ToInt(ROIProperties_Parameters[OldID].EdgeThreshold);
                LineFind.SamplingPercent = ROIProperties_Parameters[OldID].SamplingPercent;
                LineFind.OutlierDistanceThreshold = 1;
                LineFind.StepFindPoint = 1;
                LineFind.MaxPercentageOfOutliers = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MaxPercentageOfOutliers);
                LineFind.MinEdgePointNumber = GlobFuncs.ToInt(ROIProperties_Parameters[OldID].MinEdgePointNumber);
                LineFind.IsShowImageResult = true;

                bool result = LineFind.Run();
                // Giá trị output tool
                string err = LineFind.ErrMessage;
                // Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 0)
                {
                    Line_Cross_Diamond_Up = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line_Cross_Diamond_Up, DisplayImage, Color.Blue);
                }
                else if (LineFind.LineSegment != null && count == 1)
                {
                    Line_Cross_Diamond_Down = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line_Cross_Diamond_Down, DisplayImage, Color.Blue);
                }
                //Lấy ra các điểm trả về từ tool FineLine, mục đích để gắn cho tool DistanceMeasure
                if (LineFind.LineSegment != null && count == 2)
                {
                    Line_Diamond_Top_Full = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line_Diamond_Top_Full, DisplayImage, Color.Orange);
                }
                else if (LineFind.LineSegment != null && count == 3)
                {
                    Line_Diamond_Bottom_Full = new List<double>() { LineFind.LineSegment.Item1.X, LineFind.LineSegment.Item1.Y, LineFind.LineSegment.Item2.X, LineFind.LineSegment.Item2.Y };
                    //DisplayGraphics(SmartWindow, Line_Diamond_Bottom_Full, DisplayImage, Color.Orange);

                    Calc_Crowheight_TotalDepth();
                    break;
                }
                count++;
            }
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

        #region SỰ KIỆN SMARTWINDOW
        private void SmartWindow_MouseMove(object sender, MouseEventArgs e)
        {
            Lib.UpdateLabelText(lbl_Mouse_Coor, $"X: { SmartWindow.PointImage.X} Y:{ SmartWindow.PointImage.Y}");
        }

        private void SmartWindow_DoubleClick(object sender, EventArgs e)
        {
            SmartWindow.FunctionFitImage();
            SmartWindow.Zoom(new Point(SmartWindow.Width / 2, SmartWindow.Height / 2), 1.1f, 1.1f);
        }

        private GraphicsWindow.GraphicsWindow.DataRoi CurrentDrawings = new GraphicsWindow.GraphicsWindow.DataRoi();
        double diamondSizeOrigin = 0;
        private void SmartWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (!chkResizeTarget.Checked)
            {
                double distanceValueChange = ((SmartWindow.m_pUpperGirdle.Y - SmartWindow.m_pUpperGirldleUp.Y) * PIXEL_TO_MM);
                Lib.UpdateTextBoxText(txtDistanceRequest_Micromet, distanceValueChange.ToString("F0"));
                Lib.UpdateTextBoxText(txtDistance_Target_Milimet, (distanceValueChange / 1000).ToString("F3"));

                _distanceRequest = distanceValueChange;
            }
            else _distanceRequest = Lib.ToDouble(txtDistanceRequest_Micromet.Text);

            diamondSizeOrigin = (SmartWindow.m_pLowerGirdle.Y - SmartWindow.m_pCulet.Y) * 2;
            if (diamondSizeOrigin < 0) return;

            if (IsShowROIs_Full || IsShowROIs_FirtDiamond || IsShowROI_LowestPoint)
            {
                #region ĐƯỜNG CẠNH TRÊN / DƯỚI KIM CƯƠNG
                // TODO: 010726 - NB
                // Thay đổi giá trị của vị trí ROI tại Dictionary quản lý ROIS
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

                    float ratio_BOT = Convert.ToSingle(_config_Rois_Diamond.BOT_DIAMOND.Ratio);
                    double h_BOT = Lib.ToDouble(_config_Rois_Diamond.BOT_DIAMOND.Height);
                    double w_BOT = Lib.ToDouble(_config_Rois_Diamond.BOT_DIAMOND.Width);

                    double h_TAB = Lib.ToDouble(_config_Rois_Diamond.TAB_DIAMOND.Height);
                    double w_TAB = Lib.ToDouble(_config_Rois_Diamond.TAB_DIAMOND.Width);

                    switch (_dep)
                    {
                        case 1:
                            y = SmartWindow.m_pUpperGirdle.Y - ((SmartWindow.m_pUpperGirdle.Y - SmartWindow.m_pCulet.Y) * ratio_BOT) / 100;
                            x = SmartWindow.m_pUpperGirdle.X - ((SmartWindow.m_pUpperGirdle.X - SmartWindow.m_pCulet.X) * ratio_BOT) / 100;
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
                            y = SmartWindow.m_pUpperGirldleUp.Y - ((SmartWindow.m_pUpperGirldleUp.Y - SmartWindow.m_pCulet.Y) * ratio_BOT) / 100;
                            x = SmartWindow.m_pUpperGirldleUp.X - ((SmartWindow.m_pUpperGirldleUp.X - SmartWindow.m_pCulet.X) * ratio_BOT) / 100;
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
            }

            if ((IsShowROIs_Full || IsShowROI_LowestPoint) && diamondSizeOrigin != 0)
            {
                if (ROIS_LowestPoint_Diamond.Count > 0)
                {
                    Guid id = ROIS_LowestPoint_Diamond.Values.ToList()[0].ID;
                    var lowest_ROI_Output = SmartWindow.ListDataRoiOutput.Where(x => x.Key == id).FirstOrDefault();
                    if (lowest_ROI_Output != null)
                    {
                        ROIS_LowestPoint_Diamond[id].Center.Row = lowest_ROI_Output.CenterPointX;
                        ROIS_LowestPoint_Diamond[id].Center.Col = lowest_ROI_Output.CenterPointY;
                        ROIS_LowestPoint_Diamond[id].Height = lowest_ROI_Output.ShapeHeight;
                        ROIS_LowestPoint_Diamond[id].Width = lowest_ROI_Output.ShapeWidth;
                    }
                }
            }

            CurrentDrawings = DrawingObjects_Circle.FirstOrDefault(x => x.Key == SmartWindow.KeySelect).Value;
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
                DrawingObjects_Circle[SmartWindow.KeySelect] = SmartWindow.ListDataRoiOutput.Find(X => X.Key == SmartWindow.KeySelect);
            }
            else
            {
                // TODO: 010125 - NB
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

            // TODO: 090925 - NB
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang lưu thông tin rois");
            wait.Show();
            SaveRealTime();
            wait.Close();

            Lib.UpdateTextBoxText(txtCuletX, SmartWindow.m_pCulet.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtCuletY, SmartWindow.m_pCulet.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableX, SmartWindow.m_pTable.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtTableY, SmartWindow.m_pTable.Y.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleX, SmartWindow.m_pLowerGirdle.X.ToString("F1"));
            Lib.UpdateTextBoxText(txtGirldleY, SmartWindow.m_pLowerGirdle.Y.ToString("F1"));
        }

        private async void SaveRealTime()
        {
            // TODO: 090925 - NB
            Task task = Task.Factory.StartNew(() =>
            {
                SaveDiamondShape();
                SaveROI();
            });
            await task;
        }
        #endregion

        #region ROIS - CHO PHÉP CHỈNH SỬA ROIS
        bool IsShowROIs_Full = false;
        bool IsShowROIs_FirtDiamond = false;
        bool IsShowROI_LowestPoint = false;
        private void btnShowROIs_Click(object sender, EventArgs e)
        {
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang shows full rois");
            wait.Show();
            try
            {
                if (!IsShowROIs_Full)
                {
                    btnSaveRois.Visible = IsShowROIs_Full = true;
                    IsShowROIs_FirtDiamond = false;
                    btnShowROIs.Appearance.BackColor = Color.Green;
                    btnShowROIs.Appearance.ForeColor = Color.White; // Chữ trắng
                }
                else
                {
                    btnSaveRois.Visible = IsShowROIs_Full = false;
                    IsShowROIs_FirtDiamond = true;
                    btnShowROIs.Appearance.BackColor = Color.Empty; // Khôi phục màu nền mặc định
                    btnShowROIs.Appearance.ForeColor = Color.Empty; // Khôi phục màu chữ mặc định
                    btnShowROIs.Appearance.Options.UseBackColor = false;
                    btnShowROIs.Appearance.Options.UseForeColor = false;
                }

                Reload_FullRois();

                btnGetDiameter.Enabled = btnStartProcess.Enabled = btnStartProcess.Enabled = !IsShowROIs_Full;
            }
            catch (Exception)
            {
            }
            wait.Close();
        }

        private void Reload_FullRois()
        {
            SmartWindow.ClearAllRoi();

            ROIS.Clear();
            DrawingObjects.Clear();
            lst_SmartWindow_Rois.Clear();
            LoadROIs_Diamond();

            ROIProperties_Parameters.Clear();
            LoadROIParameters();

            DrawingObjects_Circle.Clear();
            LoadROICircle_Calib();

            lst_SmartWindow_Crow_TotalDept.Clear();
            DrawingObjects_Crow_TotalDept.Clear();
            ROIS_Stone_TotalDept.Clear();
            LoadROI_Crow_TotalDept();

            lst_SmartWindow_LowestPoint_Diamond.Clear();
            DrawingObjects_LowestPoint_Diamond.Clear();
            ROIS_LowestPoint_Diamond.Clear();
            LoadROI_LowestPoint_Diamond();
        }

        private void btnSaveRois_Click(object sender, EventArgs e)
        {
            // TODO: 210825 - NB
            WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang lưu thông tin rois");
            wait.Show();
            SaveDiamondShape();
            SaveROI();
            LoadShapeDiamond();
            btnShowROIs_Click(null, null);
            wait.Close();
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
                // TODO: 230925 - NB
                if (roi.DescriptionCode == 5) continue;
                roi.CenterX = Lib.ToInt(ROI.Center.Row);
                roi.CenterY = Lib.ToInt(ROI.Center.Col);
                roi.Phi = Lib.ToInt(ROI.Phi);
                roi.Width = Lib.ToInt(ROI.Width);
                roi.Height = Lib.ToInt(ROI.Height);
                try
                {
                    if (roi != null)
                        _mySQLHelper.Update<register_location_rois>(roi);
                    else
                    {
                        roi.IDGuid = ROI.ID.ToString();
                        roi.IDAction = ROI.ActionID;
                        _mySQLHelper.Update<register_location_rois>(roi);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Hàm lưu thông số ROI bắt điểm thấp nhất của kim cương
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SaveROILowest()
        {
            foreach (cRectangType ROI in ROIS_LowestPoint_Diamond.Values)
            {
                register_location_rois roi = _mySQLHelper.FindByField<register_location_rois>("IDGUID", ROI.ID);
                roi.CenterX = Lib.ToInt(ROI.Center.Row);
                roi.CenterY = Lib.ToInt(ROI.Center.Col);
                roi.Phi = Lib.ToInt(ROI.Phi);
                roi.Width = Lib.ToInt(ROI.Width);
                roi.Height = Lib.ToInt(ROI.Height);
                try
                {
                    if (roi != null)
                        _mySQLHelper.Update<register_location_rois>(roi);
                    else
                    {
                        roi.IDGuid = ROI.ID.ToString();
                        roi.IDAction = ROI.ActionID;
                        _mySQLHelper.Update<register_location_rois>(roi);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ZOOM LIVE CAMERA
        bool isStopLive_GaKimCuong = false;
        bool Is_ChangeSettingCamera = false;
        private void btnChangeSettingCamera_Click(object sender, EventArgs e)
        {
            // FrontLight
            if (!Is_ChangeSettingCamera)
            {
                Is_ChangeSettingCamera = true;
                WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Front Light");
                wait.Show();
                Open_FrontLight();
                Lib.UpdateButtonText_BColor_FColor(btnChangeSettingCamera, "GÁ KIM CƯƠNG", Color.Green, Color.White);
                wait.Close();
            }
            else // BackLight
            {
                Is_ChangeSettingCamera = false;
                WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Back Light");
                wait.Show();
                Open_BackLight();
                Lib.UpdateButtonText_BColor_FColor(btnChangeSettingCamera, "GÁ KIM CƯƠNG", Color.Transparent, Color.Black);
                wait.Close();
            }
        }

        private void Open_FrontLight()
        {
            // TODO: 281125 - NB
            isStopLive_GaKimCuong = true;
            int count = 0;
            while (true)
            {
                Thread.Sleep(10);
                count++;
                if (IsStop_LiveCamera_RealTime || count > 20) break;
                else continue;
            }
            cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(exposureTimeStone);
            cDeviceManager.HikCamera.MV_CC_SetGain_NET(gainCameraStone);
            cDeviceManager.HikCamera.MV_CC_SetGamma_NET(gammaCameraStone);
            cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
            if (liveBackgroundWorker == null)
            {
                MessageBox.Show("ERROR");
                liveBackgroundWorker = new BackgroundWorker();
                liveBackgroundWorker.WorkerSupportsCancellation = true;
                liveBackgroundWorker.RunWorkerAsync();
            }
            Thread.Sleep(400);
            IsStop_LiveCamera_RealTime = isStopLive_GaKimCuong = false;
        }

        private void Open_BackLight()
        {
            // TODO: 281125 - NB
            isStopLive_GaKimCuong = true;
            int count = 0;
            while (true)
            {
                Thread.Sleep(10);
                count++;
                if (IsStop_LiveCamera_RealTime || count > 20) break;
                else continue;
            }
            cDeviceManager.HikCamera.MV_CC_SetExposureTime_NET(exposureTimeLive);
            cDeviceManager.HikCamera.MV_CC_SetGain_NET(gainCameraLive);
            cDeviceManager.HikCamera.MV_CC_SetGamma_NET(gammaCameraLive);
            cDeviceManager.HikCamera?.NecessaryOperBeforeGrab();
            if (liveBackgroundWorker == null)
            {
                MessageBox.Show("ERROR");
                liveBackgroundWorker = new BackgroundWorker();
                liveBackgroundWorker.WorkerSupportsCancellation = true;
                liveBackgroundWorker.RunWorkerAsync();
            }
            ClosePanel();
            Thread.Sleep(400);
            IsStop_LiveCamera_RealTime = isStopLive_GaKimCuong = false;
        }

        private void num_CheckInspect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager._plcMC != null)
                cDeviceManager._plcMC.SetSingleRegister_Int16("D3125", Lib.ToInt(num_Inspect.Value));
            Lib.UpdateTextBoxText(txtInspectActual, $"{num_Inspect.Value}");

            // TODO: 090925 - NB 
            if (txtDiamondCode.InvokeRequired)
                txtDiamondCode.Invoke((MethodInvoker)delegate
                { txtDiamondCode.Focus(); });
            else
                txtDiamondCode.Focus();
            _config_data.InspectNumber = Lib.ToString(num_Inspect.Value);
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        private void GrindingStone_L_EditValueChanged(object sender, EventArgs e)
        {
            int step = 1000;
            int current = GrindingStone_L.Value;
            int snapped = (int)Math.Round(current / (double)step) * step;

            if (current != snapped)
                GrindingStone_L.Value = snapped;

            Lib.UpdateLabelText(lbGrindingStone_L, GrindingStone_L.Value.ToString());
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3248", GrindingStone_L.Value);
        }

        private void GrindingStone_R_EditValueChanged(object sender, EventArgs e)
        {
            int step = 1000;
            int current = GrindingStone_R.Value;
            int snapped = (int)Math.Round(current / (double)step) * step;

            if (current != snapped)
                GrindingStone_R.Value = snapped;

            Lib.UpdateLabelText(lbGrindingStone_R, GrindingStone_R.Value.ToString());
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3242", GrindingStone_R.Value);
        }

        private void GrindingWheelSpeed_L_EditValueChanged(object sender, EventArgs e)
        {
            int step = 50;
            int current = GrindingWheelSpeed_L.Value;
            int snapped = (int)Math.Round(current / (double)step) * step;

            if (current != snapped)
                GrindingWheelSpeed_L.Value = snapped;

            Lib.UpdateLabelText(lbGrindingWheelSpeed_L, GrindingWheelSpeed_L.Value.ToString());
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3250", GrindingWheelSpeed_L.Value);
        }

        private void GrindingWheelSpeed_R_EditValueChanged(object sender, EventArgs e)
        {
            int step = 50;
            int current = GrindingWheelSpeed_R.Value;
            int snapped = (int)Math.Round(current / (double)step) * step;

            if (current != snapped)
                GrindingWheelSpeed_R.Value = snapped;

            Lib.UpdateLabelText(lbGrindingWheelSpeed_R, GrindingWheelSpeed_R.Value.ToString());
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3244", GrindingWheelSpeed_R.Value);
        }


        private void zoom_Wheel_Speed_To_Left_Cutting_EditValueChanged(object sender, EventArgs e)
        {
            // TODO: 060826 - NB
            int step = 50;
            int current = zoom_Wheel_Speed_To_Left_Cutting.Value;
            int snapped = (int)Math.Round(current / (double)step) * step;

            if (current != snapped)
                zoom_Wheel_Speed_To_Left_Cutting.Value = snapped;

            Lib.UpdateLabelText(lblWheel_Speed_To_Left_Cutting, zoom_Wheel_Speed_To_Left_Cutting.Value.ToString());
            if (cDeviceManager._plcMC != null && cDeviceManager._plcMC.Connected)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3238", zoom_Wheel_Speed_To_Left_Cutting.Value);
        }

        private void btnGetLowest_Diameter_Click(object sender, EventArgs e)
        {
            if (!IsShowROI_LowestPoint)
            {
                IsShowROI_LowestPoint = true;
                SmartWindow.ClearAllRoi();
                ROIS_LowestPoint_Diamond.Clear();
                lst_SmartWindow_LowestPoint_Diamond.Clear();
                DrawingObjects_LowestPoint_Diamond.Clear();
                LoadROI_LowestPoint_Diamond();
                ShowPanel_WaitingForm("Vui lòng nhấn [Save Lowest ROI] để lưu thông tin ROI vừa điều chỉnh!", 2);
                Lib.UpdateSimpleButtonEnable(btnStartProcess, false);
            }
            else
            {
                IsShowROI_LowestPoint = false;
                if (IsShowROIs_Full) btnShowROIs_Click(null, null);
            }
        }

        private void btnSaveLowestROI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu thông tin ROIS bắt cạnh đá mài chế độ Semi không ?".ToUpper(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveROILowest();
                SmartWindow_MouseUp(null, null);
                Lib.UpdateSimpleButtonEnable(btnStartProcess, true);
                ShowPanel_WaitingForm("Vui lòng vận hành thao tác:\r\n1. Vặn switch sang [AUTO]\r\n2. Nhấn nút cứng [Start]\r\n3. Bấm [Start Process]", 2);
                IsShowROI_LowestPoint = false;
                if (IsShowROIs_Full)
                    btnShowROIs_Click(null, null);

                // TODO: 070526 - NB
                lst_SmartWindow_LowestPoint_Diamond.Clear();
                DrawingObjects_LowestPoint_Diamond.Clear();
                SmartWindow.ClearAllRoi();
            }
        }

        private void btnEditRatio_Click(object sender, EventArgs e)
        {
            // TODO: 210825 - NB
            if (MessageBox.Show("Bạn có muốn chỉnh sửa Ratio không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtRatio_Pixel_To_Mm.ReadOnly = false;
                btnSaveRatio.Enabled = true;
            }
        }

        private void txtOffsetGrinding_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            try
            {
                // TODO: 090925 - NB  
                if (Lib.ToInt(txtCutting_Value.Text.Trim()) <= 1)
                {
                    MessageBox.Show("Xin vui lòng nhập lại, tối thiểu giá trị bằng [1]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _config_data.DistanceGrind = txtCutting_Value.Text.Trim();
                if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
            }
            catch { }
        }

        private void txtOffsetGrinding_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự có phải là số hoặc dấu chấm hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }

        private void txtGrind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            try
            {
                // TODO: 031025 - NB  
                _config_data.DistanceGrind = txtCutting_Value.Text.Trim();
                if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
            }
            catch { }
        }

        private void txtGrind_KeyPress(object sender, KeyPressEventArgs e)
        {
            // TODO: 031025 - NB  
            // Kiểm tra xem ký tự có phải là số hoặc dấu chấm hay không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') // Cho phép dấu chấm
                e.Handled = true;
            // Kiểm tra chỉ cho phép một dấu chấm trong TextBox
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
                e.Handled = true;
        }

        private void numCountSet_Min_Max_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (cDeviceManager._plcMC != null)
                cDeviceManager._plcMC.SetSingleRegister_Int32("D3196", Lib.ToInt(numCountSet_Min_Max.Value));

            _config_data.CountSet_Min_Max = Lib.ToString(numCountSet_Min_Max.Value);
            if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
        }

        bool Is_Bypass_Concentric = false;
        private void btnBypass_Concentric_Click(object sender, EventArgs e)
        {
            if (step_progress >= 2)
            {
                MessageBox.Show("Chu trình hiện tại không phải chu trình gõ đồng tâm\r\nBạn không thể thao tác!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Is_Bypass_Concentric)
            {
                MessageBox.Show("Bạn đã sử dụng chức năng BYPASS gõ đồng tâm\r\nXin vui lòng reset chu trình hoặc khởi động lại máy để sử dụng chức năng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn sử dụng BYPASS chu trình gõ đồng tâm ?".ToUpper(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Is_Bypass_Concentric = true;
                    Lib.UpdateButton_BColor_FColor(btnBypass_Concentric, Color.FromArgb(0, 200, 83), Color.White);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LineFindRun_Crow_TotalDept();
        }

        private void chk_TotalDepth_TAB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // TODO: 2710825 - NB 
                _config_data.IsTotalDepth_TAB = Lib.ToInt(chk_TotalDepth_TAB.Checked);
                if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
            }
            catch { }
        }

        private void chkResizeTarget_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: 191125 - NB 
            //Lib.UpdateTextboxReadOnly(txtDistanceRequest_Milimet, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Lib.UpdateLabelText(lblTime, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private void rdBackLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBackLight.Checked)
            {
                rdFrontLight.Checked = false;
                // TODO: 090526 - NB 
                WaitDialogForm wait1 = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Back Light");
                wait1.Show();
                Open_BackLight();
                Is_OpenFrontLight = false;
                wait1.Close();
            }
        }

        private void rdFrontLight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFrontLight.Checked)
            {
                rdBackLight.Checked = false;
                // TODO: 090526 - NB 
                WaitDialogForm wait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", "Đang chuyển chế độ đèn Front Light");
                wait.Show();
                Open_FrontLight();
                Is_OpenFrontLight = true;
                wait.Close();
            }
        }

        private void btnSaveRatio_Click(object sender, EventArgs e)
        {
            // TODO: 210825 - NB 
            if (MessageBox.Show("Bạn có muốn lưu thông tin Ratio vừa chỉnh sửa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PIXEL_TO_MM = Lib.ToDouble(txtRatio_Pixel_To_Mm.Text);

                // TODO: 090925 - NB 
                _config_data.Ratio_Pixel_MM = txtRatio_Pixel_To_Mm.Text.Trim();
                if (_config_data != null) _mySQLHelper.Update<config_data>(_config_data);
            }
            txtRatio_Pixel_To_Mm.ReadOnly = true;
            btnSaveRatio.Enabled = false;
        }

        private void chkMove_ROI_CheckedChanged(object sender, EventArgs e)
        {
            SmartWindow.isChangeYPosition = chkMove_ROI.Checked;
        }

        private void chkZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZoom.Checked)
            {
                SmartWindow.ZoomIN = true;
                SmartWindow.ZoomOut = true;
                SmartWindow.ZoomMouseWheel = true;
                SmartWindow.ZoomClick = true;
            }
            else
            {
                SmartWindow.ZoomIN = false;
                SmartWindow.ZoomOut = false;
                SmartWindow.ZoomMouseWheel = false;
                SmartWindow.ZoomClick = false;
            }
        }
        #endregion

        #region CHECK DỮ LIỆU TỪ PLC
        private void btnCheckValuePLC_Click(object sender, EventArgs e)
        {
            try
            {
                string value = GetBits(txtPLCAddressCheckBit.Text.Trim()).ToString();
                Lib.UpdateTextBoxText(txtBitValueCheck, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCheckIntPLC_Click(object sender, EventArgs e)
        {
            try
            {
                //int value = 0;
                //cDeviceManager._plcMC.GetSingleRegister_Int32(txtIntValueCheck.Text.Trim(), out value); ;
                //Lib.UpdateTextBoxText(txtIntValueCheck, value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region KHỞI TẠO VÀ LOAD CONFIG .INI
        // TODO: 010726 - NB
        public class Config_Rois_Diamond
        {
            public Device BOT_DIAMOND { get; set; }
            public Device TAB_DIAMOND { get; set; }
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

        private Config_Rois_Diamond _config_Rois_Diamond;

        private Config_Rois_Diamond Get_Config_Rois_Diamond()
        {
            return new Config_Rois_Diamond
            {
                TAB_DIAMOND = new Device
                {
                    Height = ReadIni("TAB", "HEIGHT"),
                    Width = ReadIni("TAB", "WIDTH")
                },

                BOT_DIAMOND = new Device
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
