using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GraphicsWindow
{

    public class GraphicsWindow : PictureBox
    {
        Point startPoint = new Point(50, 50);
        Point endPoint = new Point(150, 150);

        bool isDragging = false;
        Point mouseOffset;            // Độ lệch khi bắt đầu kéo

        public delegate void CallBack();
        public void OnResize(CallBack taskCompletedCallBack)
        {
            try
            {
                if (taskCompletedCallBack != null && reSizeCheck > 0)
                    taskCompletedCallBack.Invoke();
            }
            catch (Exception ex)
            {
                Exception = ">>>>>  Void OnResize: " + ex.ToString();
            }
        }
        float m_nStoneLayerDiaInPxl = 1.00F;
        public bool m_bCheckMouseLower, m_bCheckMouseLowerUp, m_bCheckMouseAll;

        public System.Drawing.PointF m_pCulet, m_pBriCon, m_pEnhGam;
        public System.Drawing.PointF m_pOldMousePos, m_pOldPointPos;
        public System.Drawing.PointF m_pExpoBlack, m_pStoWh;
        public System.Drawing.PointF m_pLowerGirdle, m_pLowerGirdleUp, m_pTable, m_pTableUp, m_pUpperGirdle, m_pUpperGirldleUp;
        public System.Drawing.Point m_pGrayPoint;
        public System.Drawing.Point m_pRectPos, m_pRectSize, m_pWheelRealImgBR;
        public System.Drawing.PointF m_pCuletOld, m_pUpperGirdleOld, m_pLowerGirdleOld, m_pTableOld;

        //TODO : 22/07/2025 - HuyNT
        public System.Drawing.PointF m_pStartPointNewLine, m_pEndPointNewLine;

        // TODO : 120825 - NB - vẽ thêm mặt đường cạnh kim cương
        public System.Drawing.PointF m_pTopPointLine_Side, m_pBottomPointLine_Side;

        public double m_fTOP, m_fCrow, m_fGirdle, m_fBOT; 
        Pen m_PenRed = new Pen(Color.Red, 1); 
        Pen m_PenBlue = new Pen(Color.Blue, 1); 

        public double percentLineCheck = 0.165;
        // TODO : 120825 - NB 
        Pen m_PenGreen = new Pen(Color.LimeGreen, 1);
        public enum EConnectTypes
        {
            Different = 0,
            Union = 1,
            Intersection = 2,
            Reference = 3,
            None = 99
        }
        public enum ERoiTypes
        {
            Rectangle = 1,
            Circle = 2,
        }
        public enum ECentreTypes
        {
            CenterOfChange = 1,
            TheCenterDoesNotChange = 2,
        }
        public class DataRoi
        {
            // Cusom KimCuong

            public ERoiTypes RoiType { get; set; } = ERoiTypes.Rectangle;
            public float CenterPointX { get; set; } = 0;
            public float CenterPointY { get; set; } = 0;
            public float ShapeWidth { get; set; } = 0;
            public float ShapeHeight { get; set; } = 0;
            public float Angle { get; set; } = 0;
            public EConnectTypes ConnectType { get; set; } = EConnectTypes.Union;
            public Color ColorRoi { get; set; } = Color.Red;
            public Guid Key { get; set; } = new Guid();
            public ECentreTypes CenterTypes { get; set; } = ECentreTypes.TheCenterDoesNotChange;

            public DataRoi() { }
            public DataRoi(ERoiTypes RoiType_Input, float CenterPointX_Input, float CenterPointY_Input, float ShapeWidth_Input, float ShapeHeight_Input, float Angle_Input, EConnectTypes ConnectType_Input, Color ColorRoi_Input, ECentreTypes CenterTypes_Input = ECentreTypes.TheCenterDoesNotChange, Guid Key_Input = new Guid())
            {
                RoiType = RoiType_Input;
                CenterPointX = CenterPointX_Input;
                CenterPointY = CenterPointY_Input;
                ShapeWidth = ShapeWidth_Input;
                ShapeHeight = ShapeHeight_Input;
                Angle = Angle_Input;
                ConnectType = ConnectType_Input;
                ColorRoi = ColorRoi_Input;
                Key = Key_Input;
                CenterTypes = CenterTypes_Input;
                if (Key_Input == new Guid())
                {
                    Key = Guid.NewGuid();
                }
                else
                {
                    Key = Key_Input;
                }
            }
            public DataRoi Clone()
            {
                DataRoi clone = new DataRoi();
                Type type = clone.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (var property in properties)
                {
                    property.SetValue(clone, property.GetValue(this));
                }
                return clone;
            }
        }
        public class DataDispText
        {
            public string TextView { get; set; }
            public PointF InPoint { get; set; }
            public float InSize { get; set; } = 10;
            public string InColor { get; set; } = "Red";
            public string InFont { get; set; } = "Times New Roman";
            public Guid Key { get; set; } = new Guid();
            public DataDispText() { }
            public DataDispText(string TextView_Input, PointF InPoint_Input, string InColor_Input = "Red", 
                string InFont_Input = "Times New Roman", float InSize_Input = 10, Guid InKey_Input = new Guid())
            {
                TextView = TextView_Input;
                InPoint = InPoint_Input;
                InColor = InColor_Input;
                InFont = InFont_Input;
                InSize = InSize_Input;
                if (InKey_Input == new Guid())
                {
                    Key = Guid.NewGuid();
                }
                else
                {
                    Key = InKey_Input;
                }
            }
            public DataDispText Clone()
            {
                DataDispText clone = new DataDispText();
                Type type = clone.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (var property in properties)
                {
                    property.SetValue(clone, property.GetValue(this));
                }
                return clone;
            }
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PCBCustomNew
            //
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        public void SetDefaultLayer()
        {
            m_pCulet.X = 521;//G
            m_pCulet.Y = 480;
            m_pLowerGirdle.X = 649;//C
            m_pLowerGirdle.Y = 617;
            m_pTable.X = 685;//A
            m_pTable.Y = 563;
        }
        public GraphicsWindow()
        {
            this.BackColor = Color.Black;
            this.BorderStyle = BorderStyle.None;
        }
        public List<DataRoi> ListDataRoiOutput
        {
            get
            {
                if (ListDataRoiF != null)
                {
                    return ListDataRoiF;
                }
                else
                {
                    return null;
                }
            }
        }
        public PointF GetXY { get; set; }
        int imgRow = 0;
        int imgCol = 0;
        int imgWidth = 10;
        int imgHeight = 10;
        private Point Old;
        private int STT_ListRoi = 0;
        Matrix Tranform = new Matrix();
        Matrix TranformOld = new Matrix();
        Matrix TranformTam = new Matrix();
        public Guid KeySelect
        {
            get
            {
                if (ListDataRoiF.Count > 0)
                {
                    return ListDataRoiF[STT_ListRoi].Key;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set { }
        }
        public bool ZoomIN { get; set; }
        public bool ZoomOut { get; set; }
        public bool ZoomMouseWheel { get; set; }
        public bool ZoomClick { get; set; }
        public bool MoveImage { get; set; }
        public bool LockRoi { get; set; }
        public bool isChangeYPosition { get; set; }
        //public Color ColorRoi { get; set; } = Color.Red;
        float tyle = 1;
        private bool Fit = false;
        public bool FitImageDoubleClick = false;
        public bool FitImage
        {
            get => Fit;
            set
            {
                Fit = value;
                if (Fit)
                {
                    FunctionFitImage();
                }
            }
        }
        // Set số lần zoom In Zoom Out
        public int numberZoom { get; set; }
        public bool ZoomLimit { get; set; }

        int numZoom = 0;
        private PointF PointWindow = new PointF();
        public PointF PointImage
        {
            get
            {
                PointF _PointImage = ConvertXYToPixel(new PointF(PointWindow.X, PointWindow.Y));
                return _PointImage;
            }
        }
        public double ImageWidth
        {
            get
            {
                if (Image != null)
                {
                    double _ImageWidth = Image.Width;
                    return _ImageWidth;
                }
                else
                {
                    return 0;
                }

            }
        }
        public double ImageHeight
        {
            get
            {
                if (Image != null)
                {
                    double _ImageHeight = Image.Height;
                    return _ImageHeight;
                }
                else
                {
                    return 0;
                }

            }
        }
        public string GetException
        {
            get
            {
                if (Exception.Length > 0)
                {
                    return Exception;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        //int typeROI = 0;
        //float CenterPointX = 0;
        //float CenterPointY = 0;
        //float ShapeWidth = 0;
        //float ShapeHeight = 0;
        //float Angle = 0;
        Matrix Rotary = new Matrix();
        Matrix RotaryOrigin = new Matrix();
        public float DrawSelect = 1f;
        public float DrawRoi = 1f;
        int circlePoint = 20;
        Graphics workingGraphics;
        RectangleF ClipBound = new RectangleF();
        int reSize = -1;
        float exOld = 0;
        float eyOld = 0;
        public Image myBmp = null;
        private bool MouseDown = false;
        private string Exception = string.Empty;

        private PointF ConvertXY(PointF PGet)
        {
            PointF returnxy = new PointF();
            returnxy.X = ((PGet.X - Tranform.Elements[4]) * tyle) / Tranform.Elements[0];
            returnxy.Y = ((PGet.Y - Tranform.Elements[5]) * tyle) / Tranform.Elements[0];
            return returnxy;
        }
        private PointF ConvertXYToPixel(PointF currentPoint)
        {
            PointF pointTemp = new PointF();
            pointTemp.X = (float)(currentPoint.X * (ClipBound.Width / this.Width)) + ClipBound.X;
            pointTemp.Y = (float)(currentPoint.Y * (ClipBound.Height / this.Height)) + ClipBound.Y;
            return pointTemp;
        }
        private PointF ConvertXYToImage(PointF PointInput)
        {
            PointF pointTemp = new PointF();
            pointTemp.X = (float)(PointInput.X * (this.Width / ClipBound.Width)) - ClipBound.X;
            pointTemp.Y = (float)(PointInput.Y * (this.Height / ClipBound.Height)) - ClipBound.Y;
            return pointTemp;
        }
        private void FunctionMoveImage(float X, float Y)
        {
            if (MoveImage && reSizeCheck == 0 && !(m_bCheckMouseAll || m_bCheckMouseLowerUp || m_bCheckMouseLower))
            {
                try
                {
                    Tranform.Translate(X, Y, MatrixOrder.Append);
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    Exception = ">>>>>  Void FunctionMoveImage: " + ex.ToString();
                }

            }
            //Tranform = TranformOld.Clone();

        }
        public void ReadImage(string ImagePath)
        {
            try
            {
                Image Image = Image.FromFile(ImagePath);
                this.Image = new Bitmap(Image);
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void ReadImage: " + ex.ToString();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                e.Graphics.Clear(BackColor);
                e.Graphics.Transform = Tranform;
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                if (Image != null)
                    try
                    {
                        tyle = Image.VerticalResolution / e.Graphics.DpiX;
                        e.Graphics.DrawImage(this.Image, 0f, 0f);
                    }
                    catch (Exception)
                    {

                    }
                ClipBound = e.Graphics.ClipBounds;
                workingGraphics = e.Graphics;
                Rotary = Tranform.Clone();
                RotaryOrigin = Tranform.Clone();
                _ViewText = e.Graphics;

                m_pLowerGirdleUp.Y = m_pLowerGirdle.Y + 2 * (m_pCulet.Y - m_pLowerGirdle.Y);//D.y
                m_nStoneLayerDiaInPxl = m_pLowerGirdle.Y - m_pLowerGirdleUp.Y;

                //tính vị trí table trước, X TAB phải giữ nguyên, chỉ Y thay đổi
                try
                {
                    //if (numericUpDown_TOP.Text != "" && float.Parse(numericUpDown_TOP.Text) > 0)
                    {
                        int temp = (int)(m_nStoneLayerDiaInPxl * m_fCrow / 100 * Math.Tan((90 - m_fTOP) * Math.PI / 180));//nửa mặt Table
                        m_pTable.Y = m_pLowerGirdle.Y - temp;
                        //tính TAB Up
                        m_pTableUp = new System.Drawing.PointF(m_pTable.X, m_pLowerGirdleUp.Y + temp);
                    }
                }
                catch
                {

                    return;
                }

                //tính Lower Girdle X
                m_pLowerGirdle.X = m_pTable.X - (int)(m_fCrow / 100 * m_nStoneLayerDiaInPxl);
                //tính Upper Girdle X, như vậy điểm đã đầy đủ
                m_pLowerGirdleUp.X = m_pLowerGirdle.X;
                try
                {
                    //if (float.Parse(numericUpDown_Girdle.Text) > 0 && numericUpDown_Girdle.Text != "")
                    {
                        //độ dày Girdle
                        int girdleThick = (int)(m_fGirdle * m_nStoneLayerDiaInPxl / 100);
                        if (girdleThick <= 1)
                            girdleThick = 2;
                        //Tính Upper Girdle
                        m_pUpperGirdle.X = m_pLowerGirdle.X - girdleThick;
                        m_pUpperGirdle.Y = m_pLowerGirdle.Y;
                    }
                }
                catch
                {
                    return;
                }
                //tính Upper Girdle Up
                m_pUpperGirldleUp.X = m_pUpperGirdle.X;
                m_pUpperGirldleUp.Y = m_pLowerGirdleUp.Y;

                try
                {
                    //if (float.Parse(numericUpDown_BOT.Text) > 0 && numericUpDown_BOT.Text != "")
                    {
                        //Culet.X, như vậy điểm này đã đầy đủ
                        m_pCulet.X = (int)m_pUpperGirdle.X - (int)(m_nStoneLayerDiaInPxl / 2 / Math.Tan((90 - (m_fBOT)) * Math.PI / 180));
                    }
                }
                catch
                {
                    return;
                }

                e.Graphics.DrawLine(m_PenRed, m_pUpperGirdle, m_pUpperGirldleUp); // Cạnh phẳng ở giữa kim cương
                e.Graphics.DrawLine(m_PenRed, m_pCulet, m_pUpperGirdle); // Cạnh từ tâm kim cương lên cạnh mép dưới
                e.Graphics.DrawLine(m_PenRed, m_pCulet, m_pUpperGirldleUp); // Cạnh từ tâm kim cương lên cạnh mép trên

                e.Graphics.DrawLine(m_PenRed, m_pUpperGirdle, m_pLowerGirdle);
                e.Graphics.DrawLine(m_PenRed, m_pLowerGirdle, m_pLowerGirdleUp);
                e.Graphics.DrawLine(m_PenRed, m_pLowerGirdleUp, m_pUpperGirldleUp);

                //e.Graphics.DrawLine(m_PenBlue, m_pTable, m_pTableUp); // Cạnh phẳng bên phải kim cương
                e.Graphics.DrawLine(m_PenRed, m_pTable, m_pTableUp); // Cạnh phẳng bên phải kim cương
                e.Graphics.DrawLine(m_PenRed, m_pLowerGirdle, m_pTable); // Cạnh chéo từ cạnh mép dưới đến cạnh bên phải 
                e.Graphics.DrawLine(m_PenRed, m_pTableUp, m_pLowerGirdleUp); // Cạnh chéo từ cạnh mép trên đến cạnh bên phải

                e.Graphics.DrawLine(m_PenRed, new System.Drawing.PointF(m_pCulet.X + (float)0.125 * m_nStoneLayerDiaInPxl, m_pUpperGirldleUp.Y),
                   new System.Drawing.PointF(m_pUpperGirldleUp.X - 5, m_pUpperGirldleUp.Y));
                e.Graphics.DrawLine(m_PenRed, new System.Drawing.PointF(m_pCulet.X + (float)0.125 * m_nStoneLayerDiaInPxl, m_pUpperGirdle.Y),
                    new System.Drawing.PointF(m_pUpperGirdle.X - 5, m_pUpperGirdle.Y));

                //TAB line
                e.Graphics.DrawLine(m_PenRed, new System.Drawing.PointF(m_pLowerGirdle.X + 5, m_pLowerGirdle.Y),
                    new System.Drawing.PointF(m_pTable.X, m_pLowerGirdle.Y));
                e.Graphics.DrawLine(m_PenRed, new System.Drawing.PointF(m_pLowerGirdleUp.X + 5, m_pLowerGirdleUp.Y),
                    new System.Drawing.PointF(m_pTable.X, m_pLowerGirdleUp.Y));

                //TODO : 22/07/2025 - HuyNT
                m_pStartPointNewLine.X = (float)(m_pTableUp.X - Math.Abs(m_pLowerGirdleUp.Y - m_pLowerGirdle.Y) * percentLineCheck);
                m_pStartPointNewLine.Y = m_pUpperGirldleUp.Y - 40;
                m_pEndPointNewLine.X = (float)(m_pTableUp.X - Math.Abs(m_pLowerGirdleUp.Y - m_pLowerGirdle.Y) * percentLineCheck);
                m_pEndPointNewLine.Y = m_pUpperGirdle.Y + 40;
                e.Graphics.DrawLine(m_PenGreen, m_pStartPointNewLine, m_pEndPointNewLine);

                //TODO : 120825 - NB
                m_pTopPointLine_Side.X = m_pTableUp.X;
                m_pTopPointLine_Side.Y = m_pLowerGirdleUp.Y - 40;
                e.Graphics.DrawLine(m_PenGreen, m_pTopPointLine_Side, m_pTableUp);
                m_pBottomPointLine_Side.X = m_pTable.X;
                m_pBottomPointLine_Side.Y = m_pLowerGirdle.Y + 40;
                e.Graphics.DrawLine(m_PenGreen, m_pTable, m_pBottomPointLine_Side);

                if (View)
                {
                    RunDispText();
                }
                if (ListDataRoiF.Count > 0 && (ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Rectangle || ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Circle))
                {
                    DrawROISum();
                }
                //thêm line mốc vị trí lắp cho cơ 
                //e.Graphics.DrawLine(m_PenRed1, startPoint, endPoint);
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnPaint: " + ex.ToString();
            }
        }
        public void ClearImage()
        {
            try
            {
                Image = null;
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void ClearImage: " + ex.ToString();
            }
        }
        public void CleraRoi(Guid KeyClear)
        {
            try
            {
                if (KeyClear != Guid.Empty)
                {
                    int STTDataClear = ListDataRoiF.FindIndex(X => X.Key == KeyClear);
                    if (STTDataClear != -1)
                    {
                        ListDataRoiF.RemoveAt(STTDataClear);
                        if (ListDataRoiF.Count > 0)
                        {
                            STT_ListRoi = 0;
                        }
                        else
                        {
                            KeySelect = Guid.NewGuid();
                        }
                        this.Invalidate();
                    }
                    else
                    {
                        KeySelect = Guid.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void CleraRoi: " + ex.ToString();
            }
        }
        public void AddRoi(DataRoi DataRoiAdd)
        {
            try
            {
                ProcessInputData(DataRoiAdd);
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void AddRoi: " + ex.ToString();
            }
        }
        public void ClearAllRoi()
        {
            ListDataRoiF = new List<DataRoi>();
            this.Invalidate();
        }
        public void FunctionFitImage()
        {
            try
            {
                if (Image != null)
                {
                    numZoom = 0;
                    Tranform = new Matrix();
                    float zoom = 1.3f;
                    if (((float)Height / (float)Width) > ((float)Image.Size.Height / (float)Image.Size.Width))
                    {
                        zoom = Width / (Image.Size.Width * (1 / tyle));
                        imgCol = 0;
                        imgWidth = Width;
                        imgHeight = Width * Image.Size.Height / Image.Size.Width;
                        imgRow = (int)(Height - imgHeight) / 2;

                    }
                    else
                    {
                        zoom = (float)Height / (float)(Image.Size.Height * (1 / tyle));
                        imgRow = 0;
                        imgHeight = Height;
                        imgWidth = Height * Image.Size.Width / Image.Size.Height;
                        imgCol = (int)(Width - imgWidth) / 2;
                    }
                    Tranform.Scale(zoom, zoom, MatrixOrder.Append);
                    Tranform.Translate(imgCol, imgRow, MatrixOrder.Append);
                    DrawSelect = 1;
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void FunctionFitImage: " + ex.ToString();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                isMove = false;
                m_bCheckMouseAll = false;
                m_bCheckMouseLower = false;
                m_bCheckMouseLowerUp = false;
                if (e.Button == MouseButtons.Left)
                {
                    MouseDown = false;
                    reSize = -1;
                    if (ZoomClick)
                    {
                        if (ZoomIN)
                        {
                            Zoom(e.Location);
                        }
                        if (ZoomOut)
                        {
                            Zoom(e.Location);
                        }
                    }
                    if (reSizeCheck > 0)
                    {
                        reSizeCheck = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnMouseUp: " + ex.ToString();
            }
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                if (ZoomMouseWheel)
                {
                    if (ZoomLimit)
                    {
                        if (e.Delta > 0) // Zoom in
                        {
                            if (numZoom < numberZoom)
                            {
                                numZoom++;
                                ZoomScroll(e.Location, e.Delta > 0);
                            }
                        }
                        else // Zoom out
                        {
                            if (numZoom > -numberZoom)
                            {
                                numZoom--;
                                ZoomScroll(e.Location, e.Delta > 0);
                            }
                        }
                    }
                    else
                    {
                        ZoomScroll(e.Location, e.Delta > 0);
                    }
                }
                object k = e.GetType();
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnMouseWheel: " + ex.ToString();
            }
            base.OnMouseWheel(e);
        }
        public void MoveROI(PointF Location, Guid ROIKey)
        {
            int ROISelect = ListDataRoiF.FindIndex(x => x.Key == ROIKey);
            if (ROISelect != -1)
            {
                ListDataRoiF[ROISelect].CenterPointX = Location.X;
                ListDataRoiF[ROISelect].CenterPointY = Location.Y;
            }

        }
        private void ZoomScroll(Point location, bool zoomIn)
        {
            try
            {
                if (zoomIn && Tranform.Elements[0] > 622)
                {
                    return;
                }
                else if (!zoomIn && Tranform.Elements[3] < 0.001)
                {
                    return;
                }
                if (zoomIn && DrawSelect > 0.05)
                    DrawSelect = Math.Abs(DrawSelect - Math.Abs((622 - Tranform.Elements[0]) / (622 * 12)));
                else if (!zoomIn && DrawSelect < 2)
                    DrawSelect += Math.Abs((622 - Tranform.Elements[0]) / (622 * 12));
                // Figure out what the new scale will be. Ensure the scale factor remains between
                float newScale1 = zoomIn ? 1.2f : 0.8f;
                // Translate mouse point to origin
                Tranform.Translate(-location.X, -location.Y, MatrixOrder.Append);
                // Scale view
                Tranform.Scale(newScale1, newScale1, MatrixOrder.Append);
                // Translate origin back to original mouse point.
                Tranform.Translate(location.X, location.Y, MatrixOrder.Append);
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void ZoomScroll: " + ex.ToString();
            }
            this.Invalidate();
        }
        private void ZoomInLocation(int X, int Y)
        {
            ZoomScroll(new Point(X, Y), ZoomIN);
            if (ZoomClick)
            {
                if (ZoomIN)
                {
                    Zoom(new PointF(X, Y));
                }
                if (ZoomOut)
                {
                    Zoom(new PointF(X, Y));
                }
            }
        }
        private void Zoom(PointF OffsetMove)
        {
            try
            {
                if (ZoomIN)
                {
                    numZoom++;
                    if (Math.Abs(numZoom) >= numberZoom) return;
                    Tranform.Translate(-OffsetMove.X, -OffsetMove.Y, MatrixOrder.Append);
                    Tranform.Scale(1.2f, 1.2f, MatrixOrder.Append);
                    Tranform.Translate(OffsetMove.X, OffsetMove.Y, MatrixOrder.Append);
                }
                if (ZoomOut)
                {
                    numZoom = numberZoom - 1;
                    if (Math.Abs(numZoom) >= numberZoom) return;
                    Tranform.Translate(-OffsetMove.X, -OffsetMove.Y, MatrixOrder.Append);
                    Tranform.Scale(0.8f, 0.8f, MatrixOrder.Append);
                    Tranform.Translate(OffsetMove.X, OffsetMove.Y, MatrixOrder.Append);
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void Zoom: " + ex.ToString();
            }
        }
        public void Zoom(PointF OffsetMove, float ZoomX, float ZoomY)
        {
            try
            {
                if (ZoomIN)
                {
                    Tranform.Translate(-OffsetMove.X, -OffsetMove.Y, MatrixOrder.Append);
                    Tranform.Scale(ZoomX, ZoomY, MatrixOrder.Append);
                    Tranform.Translate(OffsetMove.X, OffsetMove.Y, MatrixOrder.Append);
                }
                if (ZoomOut)
                {
                    Tranform.Translate(-OffsetMove.X, -OffsetMove.Y, MatrixOrder.Append);
                    Tranform.Scale(ZoomX, ZoomY, MatrixOrder.Append);
                    Tranform.Translate(OffsetMove.X, OffsetMove.Y, MatrixOrder.Append);
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void Zoom: " + ex.ToString();
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (FitImageDoubleClick)
            {
                FunctionFitImage();
                base.OnMouseDoubleClick(e);
            }
        }
        DataRoi DataRoiOLD = new DataRoi();
        Matrix MTOLD;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    MouseDown = true;
                    Old = e.Location;
                    PointF TempPoint = ConvertXYToPixel(new PointF(e.X, e.Y));
                    //TranformOld = new Matrix();
                    exOld = e.X;
                    eyOld = e.Y;
                    //TranformOld = Tranform.Clone();
                    if (reSize > 0)
                    {
                        DataRoiOLD = ListDataRoiF[STT_ListRoi].Clone();
                        MTOLD = Rotary;
                    }
                    m_pOldMousePos = e.Location;
                    if (IsNearPointDown(TempPoint))
                    {
                        m_pOldPointPos = new System.Drawing.Point((int)m_pLowerGirdle.X, (int)m_pLowerGirdle.Y);
                        m_bCheckMouseLower = true;
                    }

                    else if (IsNearPointUp(TempPoint))
                    {
                        m_bCheckMouseLowerUp = true;
                        m_pOldPointPos = new System.Drawing.Point((int)m_pLowerGirdle.X, (int)m_pLowerGirdle.Y);
                    }


                    else if (IsCheckMouseAll(TempPoint))
                    {
                        m_pCuletOld = m_pCulet;
                        m_pUpperGirdleOld = new System.Drawing.Point((int)m_pUpperGirdle.X, (int)m_pUpperGirdle.Y);
                        m_pLowerGirdleOld = new System.Drawing.Point((int)m_pLowerGirdle.X, (int)m_pLowerGirdle.Y);
                        m_pTableOld = new System.Drawing.Point((int)m_pTable.X, (int)m_pTable.Y);
                        m_bCheckMouseAll = true;
                    }
                }

                if (reSizeCheck == 0)
                {
                    //Stopwatch TimePatternTesst = new Stopwatch();
                    //TimePatternTesst.Start();
                    ListRoiSelect = new List<int>();
                    PointF TempPoint = ConvertXYToPixel(new PointF(e.X, e.Y));

                    for (int i = 0; i < ListDataRoiF.Count; i++)
                    {
                        Matrix RotaryListRoi = Tranform.Clone();
                        RotaryListRoi.RotateAt(ListDataRoiF[i].Angle, PointMulMatrix(ListDataRoiF[i].CenterPointX, ListDataRoiF[i].CenterPointY, RotaryListRoi), MatrixOrder.Append);
                        PointF currentPoint = ConvertXYToPixel(PointMulMatrix(TempPoint.X, TempPoint.Y, RotaryListRoi));
                        if (currentPoint.X > ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth &&
                        currentPoint.X < ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth &&
                        currentPoint.Y > ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight &&
                        currentPoint.Y < ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight)
                        {
                            ListRoiSelect.Add(i);
                        }
                    }
                    try
                    {
                        if (ListRoiSelect.Count == 1)
                        {
                            CountClick = 0;
                            STT_ListRoi = ListRoiSelect[0];
                        }
                        else
                        {
                            if (ListRoiSelect.Count > 1 && CountClick <= ListRoiSelect.Count)
                            {
                                if (CountClick == ListRoiSelect.Count - 1)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else if (CountClick > ListRoiSelect.Count)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else if (CountClick == ListRoiSelect.Count)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else
                                {
                                    CountClick += 1;
                                    STT_ListRoi = ListRoiSelect[CountClick];
                                }
                            }
                            else
                            {
                                CountClick = 0;
                                if (ListRoiSelect.Count > 0)
                                {
                                    STT_ListRoi = 0;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        STT_ListRoi = ListRoiSelect[0];
                        CountClick = 0;
                    }
                    //TimePatternTesst.Stop();
                    //string Tm = TimePatternTesst.ElapsedTicks.ToString();
                }

                // Check if mouse is near the line (tolerance ~5 pixels)
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddLine(startPoint, endPoint);
                    using (Pen pen = new Pen(Color.Black, 10)) // 10 for easier hit testing
                    {
                        if (path.IsOutlineVisible(e.Location, pen))
                        {
                            isDragging = true;
                            mouseOffset = new Point(e.X - startPoint.X, e.Y - startPoint.Y);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnMouseDown: " + ex.ToString();
            }
            this.Invalidate();
            base.OnMouseDown(e);
        }
        private int reSizeCheck = 0;
        private bool isMove = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                PointWindow.X = e.X;
                PointWindow.Y = e.Y;
                PointF TempPoint = ConvertXYToPixel(e.Location);


                if (MouseDown)
                {
                    isMove = true;
                    reSizeCheck = reSize;
                    //m_bCanCrop = false;//don't crop image while moving layer
                    if (reSize == 0 && !(m_bCheckMouseAll || m_bCheckMouseLowerUp || m_bCheckMouseLower))
                    {
                        FunctionMoveImage(e.Location.X - exOld, e.Location.Y - eyOld);
                        exOld = e.Location.X;
                        eyOld = e.Location.Y;
                    }
                    else
                    {
                        if (ListDataRoiF.Count > 0 && !LockRoi)
                        {
                            Task RunEvent = Task.Factory.StartNew(() => ResizeROI(e.Location, ListDataRoiF[STT_ListRoi].RoiType));
                            //ResizeROI(e.Location, ListDataRoiF[STT_ListRoi].RoiType);
                        }
                        else if (!LockRoi)
                        {
                            Task RunEvent = Task.Factory.StartNew(() => ResizeROI(e.Location, 0));
                        }

                    }
                }
                //}
                else
                {
                    if (ListDataRoiF.Count > 0 && !LockRoi)
                    {
                        reSizeCheck = reSize = CheckResize(ConvertXYToPixel(new PointF((float)(e.Location.X), (float)(e.Location.Y))), ListDataRoiF[STT_ListRoi].RoiType);
                    }
                    else
                    {
                        reSize = 0;
                    }

                }
                if (IsNearPointDown(TempPoint))
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (IsNearPointUp(TempPoint))
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (IsCheckMouseAll(TempPoint))
                {
                    this.Cursor = Cursors.Hand;
                }

                if (isDragging)
                {
                    int dx = e.X - startPoint.X - mouseOffset.X;
                    int dy = e.Y - startPoint.Y - mouseOffset.Y;

                    startPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
                    endPoint = new Point(endPoint.X + dx, endPoint.Y + dy);
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnMouseMove: " + ex.ToString();
            }
            base.OnMouseMove(e);
        }
        bool IsNearPointUp(System.Drawing.PointF p)//done
        {
            bool bResultX = false, bResultY = false;
            if (p.X > 0)
                bResultX = true;
            if (p.Y > m_pLowerGirdleUp.Y - 100 && p.Y < m_pLowerGirdleUp.Y + 100)
                bResultY = true;
            return bResultX && bResultY;
        }



        bool IsNearPointDown(System.Drawing.PointF p)//done
        {
            bool bResultX = false, bResultY = false;
            if (p.X > 0)
                bResultX = true;
            if (p.Y > m_pLowerGirdle.Y - 100 && p.Y < m_pLowerGirdle.Y + 100)
                bResultY = true;
            return bResultX && bResultY;
        }
        bool IsCheckMouseAll(System.Drawing.PointF pAll)//done
        {
            if (pAll.X > m_pCulet.X && pAll.X < m_pTable.X
                    && pAll.Y >= m_pUpperGirldleUp.Y + 100 && pAll.Y <= m_pUpperGirdle.Y - 100)
                return true;
            else
                return false;
        }
        private List<int> ListRoiSelect = new List<int>();
        private int CountClick = 0;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            try
            {
                if (reSizeCheck != 0 && !isMove)
                {
                    //Stopwatch TimePatternTesst = new Stopwatch();
                    //TimePatternTesst.Start();
                    ListRoiSelect = new List<int>();
                    PointF TempPoint = ConvertXYToPixel(new PointF(e.X, e.Y));

                    for (int i = 0; i < ListDataRoiF.Count; i++)
                    {
                        Matrix RotaryListRoi = Tranform.Clone();
                        RotaryListRoi.RotateAt(ListDataRoiF[i].Angle, PointMulMatrix(ListDataRoiF[i].CenterPointX, ListDataRoiF[i].CenterPointY, RotaryListRoi), MatrixOrder.Append);
                        PointF currentPoint = ConvertXYToPixel(PointMulMatrix(TempPoint.X, TempPoint.Y, RotaryListRoi));
                        if (currentPoint.X > ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth &&
                        currentPoint.X < ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth &&
                        currentPoint.Y > ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight &&
                        currentPoint.Y < ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight)
                        {
                            ListRoiSelect.Add(i);
                        }
                    }
                    try
                    {
                        if (ListRoiSelect.Count == 1)
                        {
                            CountClick = 0;
                            STT_ListRoi = ListRoiSelect[0];
                        }
                        else
                        {
                            if (ListRoiSelect.Count > 1 && CountClick <= ListRoiSelect.Count)
                            {
                                if (CountClick == ListRoiSelect.Count - 1)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else if (CountClick > ListRoiSelect.Count)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else if (CountClick == ListRoiSelect.Count)
                                {
                                    STT_ListRoi = ListRoiSelect[0];
                                    CountClick = 0;
                                }
                                else
                                {
                                    CountClick += 1;
                                    STT_ListRoi = ListRoiSelect[CountClick];
                                }
                            }
                            else
                            {
                                CountClick = 0;
                                if (ListRoiSelect.Count > 0)
                                {
                                    STT_ListRoi = 0;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        STT_ListRoi = ListRoiSelect[0];
                        CountClick = 0;
                    }
                    //TimePatternTesst.Stop();
                    //string Tm = TimePatternTesst.ElapsedTicks.ToString();
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void OnMouseClick: " + ex.ToString();
            }
            base.OnMouseClick(e);
        }
        private List<DataRoi> ListDataRoiF = new List<DataRoi>();
        public void DrawROI(List<DataRoi> ListDataRoi)
        {
            try
            {
                if (ListDataRoi.Count > 0)
                {
                    //ListDataRoiF = new List<DataRoi>();
                    //ppoint1 = new List<PointF>();
                    //ppoint2 = new List<PointF>();
                    //ppoint3 = new List<PointF>();
                    //ppoint4 = new List<PointF>();
                }
                foreach (DataRoi item in ListDataRoi)
                {
                    ProcessInputData(item);
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void DrawROI: " + ex.ToString();
            }
            //for (int i = 0; i < ListDataRoi.Count; i++)
            //{
            //    DataRoi DataProcessing = new DataRoi();
            //    DataProcessing.RoiType = ListDataRoi[i].RoiType;
            //    DataProcessing.CenterPointX = (float)(ListDataRoi[i].CenterPointX);
            //    DataProcessing.CenterPointY = (float)(ListDataRoi[i].CenterPointY);
            //    DataProcessing.ShapeWidth = (float)(ListDataRoi[i].ShapeWidth);
            //    DataProcessing.ShapeHeight = (float)(ListDataRoi[i].ShapeHeight);
            //    DataProcessing.Angle = (float)(ListDataRoi[i].Angle);
            //    DataProcessing.ConnectType = ListDataRoi[i].ConnectType;
            //    DataProcessing.ColorRoi = ListDataRoi[i].ColorRoi;
            //    if (ListDataRoi[i].Key == new Guid())
            //    {
            //        DataProcessing.Key = Guid.NewGuid();
            //    }
            //    else
            //    {
            //        DataProcessing.Key = ListDataRoi[i].Key;
            //    }
            //    DataProcessing.CenterTypes = ListDataRoi[i].CenterTypes;
            //    ListDataRoiF.Add(DataProcessing);
            //    ppoint1.Add(new PointF() { X = ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight });
            //    ppoint2.Add(new PointF() { X = ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight });
            //    ppoint3.Add(new PointF() { X = ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight });
            //    ppoint4.Add(new PointF() { X = ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight });
            //}


            //Pen pen1 = new Pen(ColorRoi, 0.05f);
            //Graphics workingGraphicsListRoi = Graphics.FromImage(Image);
            //workingGraphicsListRoi.InterpolationMode = InterpolationMode.NearestNeighbor;
            //Matrix Rotary1 = new Matrix();
            //for (int i = 0; i < ListDataRoiF.Count; i++)
            //{
            //    if (i != STT_ListRoi)
            //    {
            //        Rotary1 = Tranform.Clone();
            //        Rotary1.RotateAt(ListDataRoiF[i].AngleF, PointMulMatrix(ListDataRoiF[i].CenterPointXF, ListDataRoiF[i].CenterPointYF, Rotary1), MatrixOrder.Append);
            //        workingGraphicsListRoi.Transform = Rotary1;                
            //        workingGraphicsListRoi.DrawPolygon(pen1, new PointF[] {
            //                    new PointF() { X = ListDataRoiF[i].CenterPointXF + ListDataRoiF[i].ShapeWidthF, Y = ListDataRoiF[i].CenterPointYF + ListDataRoiF[i].ShapeHeightF },
            //                    new PointF() { X = ListDataRoiF[i].CenterPointXF + ListDataRoiF[i].ShapeWidthF, Y = ListDataRoiF[i].CenterPointYF - ListDataRoiF[i].ShapeHeightF },
            //                    new PointF() { X = ListDataRoiF[i].CenterPointXF - ListDataRoiF[i].ShapeWidthF, Y = ListDataRoiF[i].CenterPointYF - ListDataRoiF[i].ShapeHeightF },
            //                    new PointF() { X = ListDataRoiF[i].CenterPointXF - ListDataRoiF[i].ShapeWidthF, Y = ListDataRoiF[i].CenterPointYF + ListDataRoiF[i].ShapeHeightF } });
            //    }
            //}
            this.Invalidate();
        }
        private void ProcessInputData(DataRoi DataInput)
        {
            try
            {
                DataRoi DataProcessing = new DataRoi();
                DataProcessing.RoiType = DataInput.RoiType;
                DataProcessing.CenterPointX = (float)(DataInput.CenterPointX);
                DataProcessing.CenterPointY = (float)(DataInput.CenterPointY);
                DataProcessing.ShapeWidth = (float)(DataInput.ShapeWidth);
                DataProcessing.ShapeHeight = (float)(DataInput.ShapeHeight);
                DataProcessing.Angle = (float)(DataInput.Angle);
                DataProcessing.ConnectType = DataInput.ConnectType;
                DataProcessing.ColorRoi = DataInput.ColorRoi;
                if (DataInput.Key == new Guid())
                {
                    DataProcessing.Key = Guid.NewGuid();
                }
                else
                {
                    DataProcessing.Key = DataInput.Key;
                }
                DataProcessing.CenterTypes = DataInput.CenterTypes;
                ListDataRoiF.Add(DataProcessing);
                ppoint1.Add(new PointF() { X = DataProcessing.CenterPointX + DataProcessing.ShapeWidth, Y = DataProcessing.CenterPointY + DataProcessing.ShapeHeight });
                ppoint2.Add(new PointF() { X = DataProcessing.CenterPointX + DataProcessing.ShapeWidth, Y = DataProcessing.CenterPointY - DataProcessing.ShapeHeight });
                ppoint3.Add(new PointF() { X = DataProcessing.CenterPointX - DataProcessing.ShapeWidth, Y = DataProcessing.CenterPointY - DataProcessing.ShapeHeight });
                ppoint4.Add(new PointF() { X = DataProcessing.CenterPointX - DataProcessing.ShapeWidth, Y = DataProcessing.CenterPointY + DataProcessing.ShapeHeight });
                if (ListDataRoiF.Count == 1)
                {
                    STT_ListRoi = 0;
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void ProcessInputData: " + ex.ToString();
            }
        }
        private PointF PointMulMatrix(double pX, double pY, Matrix matrix)
        {
            try
            {
                float[] matrix2D = matrix.Elements;
                System.Windows.Point testp = new System.Windows.Point() { X = pX, Y = pY };
                System.Windows.Media.Matrix matrixMedia = new System.Windows.Media.Matrix(matrix2D[0], matrix2D[1], matrix2D[2], matrix2D[3], matrix2D[4], matrix2D[5]);
                System.Windows.Point pReturn = System.Windows.Point.Multiply(testp, matrixMedia);
                PointF convertReturn = new PointF() { X = (float)(pReturn.X), Y = (float)(pReturn.Y) };
                return convertReturn;
            }
            catch (Exception ex)
            {
                Exception = ">>>>> PointF PointMulMatrix: " + ex.ToString();
                return Point.Empty;
            }
        }
        private List<PointF> ppoint1 = new List<PointF> { new PointF(0, 0) };
        private List<PointF> ppoint2 = new List<PointF> { new PointF(0, 0) };
        private List<PointF> ppoint3 = new List<PointF> { new PointF(0, 0) };
        private List<PointF> ppoint4 = new List<PointF> { new PointF(0, 0) };

        private void RotateRectangle(float Phi)
        {
            double diag = Math.Sqrt((ListDataRoiF[STT_ListRoi].ShapeHeight * ListDataRoiF[STT_ListRoi].ShapeHeight) + (ListDataRoiF[STT_ListRoi].ShapeWidth * ListDataRoiF[STT_ListRoi].ShapeWidth));
            ppoint1.Add(new PointF(Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointX + diag * Math.Cos(Phi + Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth))), Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointY - diag * Math.Sin(Phi + Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth)))));
            ppoint2.Add(new PointF(Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointX - diag * Math.Cos(Phi - Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth))), Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointY + diag * Math.Sin(Phi - Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth)))));
            ppoint3.Add(new PointF(Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointX - diag * Math.Cos(Phi + Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth))), Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointY + diag * Math.Sin(Phi + Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth)))));
            ppoint4.Add(new PointF(Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointX + diag * Math.Cos(Phi - Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth))), Convert.ToSingle(ListDataRoiF[STT_ListRoi].CenterPointY - diag * Math.Sin(Phi - Math.Atan(ListDataRoiF[STT_ListRoi].ShapeHeight / ListDataRoiF[STT_ListRoi].ShapeWidth)))));
        }
        private PointF RotatePoint(PointF TempPoint, float Angle)
        {
            float Deg = Convert.ToSingle((180 * Math.Atan(Math.Abs(TempPoint.Y - ListDataRoiF[STT_ListRoi].CenterPointY) / Math.Abs(TempPoint.X - ListDataRoiF[STT_ListRoi].CenterPointX))) / Math.PI);
            if ((TempPoint.Y <= ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
            {
                Deg = Deg;
            }
            else if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X <= ListDataRoiF[STT_ListRoi].CenterPointX))
            {
                Deg = 90 + (90 - Deg);
            }
            else if ((TempPoint.Y >= ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
            {
                Deg = 180 + Deg;
            }
            else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X >= ListDataRoiF[STT_ListRoi].CenterPointX))
            {
                Deg = 270 + (90 - Deg);
            }
            return TempPoint;
        }
        Pen pen1 = new Pen(Color.Red, 0.05f);
        Graphics workingGraphicsListRoi;
        Matrix Rotary1 = new Matrix();
        private void DrawROISum()
        {
            try
            {
                //using (Pen pen1 = new Pen(Color.Yellow, draw))
                //{ 
                //    RotateRectangle((float)(((360 - Angle) * Math.PI)/180));
                //    workingGraphics1.DrawPolygon(pen1, new PointF[] { ppoint1, ppoint2, ppoint3, ppoint4 });
                //}

                Rotary.RotateAt(360 - ListDataRoiF[STT_ListRoi].Angle, PointMulMatrix(ListDataRoiF[STT_ListRoi].CenterPointX, ListDataRoiF[STT_ListRoi].CenterPointY, Rotary), MatrixOrder.Append);
                RotaryOrigin.RotateAt(ListDataRoiF[STT_ListRoi].Angle, PointMulMatrix(ListDataRoiF[STT_ListRoi].CenterPointX, ListDataRoiF[STT_ListRoi].CenterPointY, RotaryOrigin), MatrixOrder.Append);
                float Select = Math.Min(ListDataRoiF[STT_ListRoi].ShapeWidth, ListDataRoiF[STT_ListRoi].ShapeHeight);
                using (Pen pen = new Pen(ListDataRoiF[STT_ListRoi].ColorRoi, DrawSelect))
                {
                    ppoint1[STT_ListRoi] = (new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + ListDataRoiF[STT_ListRoi].ShapeWidth, Y = ListDataRoiF[STT_ListRoi].CenterPointY + ListDataRoiF[STT_ListRoi].ShapeHeight });
                    ppoint2[STT_ListRoi] = (new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + ListDataRoiF[STT_ListRoi].ShapeWidth, Y = ListDataRoiF[STT_ListRoi].CenterPointY - ListDataRoiF[STT_ListRoi].ShapeHeight });
                    ppoint3[STT_ListRoi] = (new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX - ListDataRoiF[STT_ListRoi].ShapeWidth, Y = ListDataRoiF[STT_ListRoi].CenterPointY - ListDataRoiF[STT_ListRoi].ShapeHeight });
                    ppoint4[STT_ListRoi] = (new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX - ListDataRoiF[STT_ListRoi].ShapeWidth, Y = ListDataRoiF[STT_ListRoi].CenterPointY + ListDataRoiF[STT_ListRoi].ShapeHeight });
                    PointF pointLine = new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + (ListDataRoiF[STT_ListRoi].ShapeWidth + Select / 5), Y = ListDataRoiF[STT_ListRoi].CenterPointY + (Select / 2 - Select / 3) };
                    PointF pointLine1 = new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + (ListDataRoiF[STT_ListRoi].ShapeWidth + Select / 4), Y = ListDataRoiF[STT_ListRoi].CenterPointY + (Select / 2 - Select / 2) };
                    PointF pointLine2 = new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + (ListDataRoiF[STT_ListRoi].ShapeWidth + Select / 5), Y = ListDataRoiF[STT_ListRoi].CenterPointY - (Select / 2 - Select / 3) };
                    PointF pointLine3 = new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + (ListDataRoiF[STT_ListRoi].ShapeWidth + Select / 3 + Select / 5), Y = ListDataRoiF[STT_ListRoi].CenterPointY };
                    PointF pointLine4 = new PointF() { X = ListDataRoiF[STT_ListRoi].CenterPointX + (ListDataRoiF[STT_ListRoi].ShapeWidth - Select / 5), Y = ListDataRoiF[STT_ListRoi].CenterPointY };
                    if (ListDataRoiF[STT_ListRoi].CenterPointX != 0 && ListDataRoiF[STT_ListRoi].CenterPointY != 0 && ListDataRoiF[STT_ListRoi].ShapeWidth != 0 && ListDataRoiF[STT_ListRoi].ShapeHeight != 0)
                    {
                        workingGraphics.Transform = Rotary;
                        workingGraphics.DrawPolygon(pen, new PointF[] { pointLine, pointLine1, pointLine2, pointLine3 });
                        workingGraphics.DrawLine(pen, pointLine3.X, pointLine3.Y, pointLine4.X, pointLine4.Y);
                        workingGraphics.DrawLine(pen, ListDataRoiF[STT_ListRoi].CenterPointX - 3, ListDataRoiF[STT_ListRoi].CenterPointY, ListDataRoiF[STT_ListRoi].CenterPointX + 3, ListDataRoiF[STT_ListRoi].CenterPointY);
                        workingGraphics.DrawLine(pen, ListDataRoiF[STT_ListRoi].CenterPointX, ListDataRoiF[STT_ListRoi].CenterPointY - 3, ListDataRoiF[STT_ListRoi].CenterPointX, ListDataRoiF[STT_ListRoi].CenterPointY + 3);
                        if (ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Rectangle)
                        {
                            workingGraphics.DrawPolygon(pen, new PointF[] { ppoint1[STT_ListRoi], ppoint2[STT_ListRoi], ppoint3[STT_ListRoi], ppoint4[STT_ListRoi] });
                        }
                        else if (ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Circle)
                        {
                            workingGraphics.DrawEllipse(pen, ListDataRoiF[STT_ListRoi].CenterPointX - ListDataRoiF[STT_ListRoi].ShapeWidth,
                                                             ListDataRoiF[STT_ListRoi].CenterPointY - ListDataRoiF[STT_ListRoi].ShapeHeight,
                                                             ListDataRoiF[STT_ListRoi].ShapeWidth * 2f,
                                                             ListDataRoiF[STT_ListRoi].ShapeHeight * 2f);
                        }
                        KeySelect = ListDataRoiF[STT_ListRoi].Key;
                    }

                    workingGraphicsListRoi = workingGraphics;
                    //Stopwatch TimePainRoi = new Stopwatch();
                    //TimePainRoi.Start();
                    for (int i = 0; i < ListDataRoiF.Count; i++)
                    {
                        if (i != STT_ListRoi)
                        {
                            pen1 = new Pen(ListDataRoiF[i].ColorRoi, DrawRoi);
                            Rotary1 = Tranform.Clone();
                            Rotary1.RotateAt(360 - ListDataRoiF[i].Angle, PointMulMatrix(ListDataRoiF[i].CenterPointX, ListDataRoiF[i].CenterPointY, Rotary1), MatrixOrder.Append);
                            workingGraphicsListRoi.Transform = Rotary1;
                            if (ListDataRoiF[i].RoiType == ERoiTypes.Rectangle)
                            {
                                workingGraphicsListRoi.DrawPolygon(pen1, new PointF[] {
                                new PointF() { X = ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight },
                                new PointF() { X = ListDataRoiF[i].CenterPointX + ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight },
                                new PointF() { X = ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight },
                                new PointF() { X = ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth, Y = ListDataRoiF[i].CenterPointY + ListDataRoiF[i].ShapeHeight } });
                            }
                            else if (ListDataRoiF[i].RoiType == ERoiTypes.Circle)
                            {
                                workingGraphicsListRoi.DrawEllipse(pen1, ListDataRoiF[i].CenterPointX - ListDataRoiF[i].ShapeWidth,
                                                                         ListDataRoiF[i].CenterPointY - ListDataRoiF[i].ShapeHeight,
                                                                         ListDataRoiF[i].ShapeWidth * 2f,
                                                                         ListDataRoiF[i].ShapeHeight * 2f);
                            }
                            //workingGraphicsListRoi.DrawRectangle(pen1, ListDataRoiF[i].CenterPointXF - ListDataRoiF[i].ShapeWidthF, ListDataRoiF[i].CenterPointYF - ListDataRoiF[i].ShapeHeightF, ListDataRoiF[i].ShapeWidthF + ListDataRoiF[i].ShapeWidthF, ListDataRoiF[i].ShapeHeightF + ListDataRoiF[i].ShapeHeightF);
                        }
                    }

                    //TimePainRoi.Stop();
                    //string TimePR = TimePainRoi.ElapsedTicks.ToString();
                    //Console.WriteLine(TimePR);

                }

            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void DrawROISum: " + ex.ToString();
            }
        }
        Matrix RotaryOrigin1 = new Matrix();

        private void ResizeROI(PointF currentPoint, ERoiTypes TypeROI)
        {
            try
            {

                PointF TempPoint = ConvertXYToPixel(currentPoint);
                PointF TempPointOLD = ConvertXYToPixel(Old);
                PointF TempPointOLDDiamond = ConvertXYToPixel(m_pOldMousePos);
                float tempX = TempPoint.X - (int)TempPointOLDDiamond.X;
                float tempY = TempPoint.Y - (int)TempPointOLDDiamond.Y;

                //float tempX = currentPoint.X - (int)m_pOldMousePos.X;
                //float tempY = currentPoint.Y - (int)m_pOldMousePos.Y;
                //PointF TempPoint = ConvertXYToPixel(currentPoint);
                PointF TempPointO = ConvertXYToPixel(PointMulMatrix(TempPoint.X, TempPoint.Y, RotaryOrigin));
                // PointF TempPointOLD = ConvertXYToPixel(Old);
                PointF TempPointOLDO = ConvertXYToPixel(PointMulMatrix(TempPointOLD.X, TempPointOLD.Y, RotaryOrigin));
                if (TypeROI == ERoiTypes.Rectangle || TypeROI == ERoiTypes.Circle)
                {
                    float startPointXNew = ListDataRoiF[STT_ListRoi].CenterPointX;
                    float startPointYNew = ListDataRoiF[STT_ListRoi].CenterPointY;
                    float WidthF = ListDataRoiF[STT_ListRoi].ShapeWidth;
                    switch (reSizeCheck)
                    {
                        case 1:
                            {
                                //double withTemp = TempPoint1.X - startPointXNew;
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + (float)(TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + ((TempPointO.X - TempPointOLDO.X) / 2));


                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), DataRoiOLD.CenterPointY);
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }

                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                break;
                            }
                        case 2:
                            {
                                //double HeightTemp = Math.Abs(TempPoint1.Y - startPointYNew);
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - (TempPointO.Y - TempPointOLDO.Y));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - ((TempPointO.Y - TempPointOLDO.Y) / 2));

                                    PointF Data = new PointF(DataRoiOLD.CenterPointX, (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }

                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                break;
                            }
                        case 3:
                            {
                                //double withTemp = TempPoint1.X - startPointXNew;
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - (TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - ((TempPointO.X - TempPointOLDO.X) / 2));


                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), DataRoiOLD.CenterPointY);
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                startPointXNew = ListDataRoiF[STT_ListRoi].CenterPointX;
                                WidthF = ListDataRoiF[STT_ListRoi].ShapeWidth;
                                break;
                            }
                        case 4:
                            {
                                //double HeightTemp = Math.Abs(startPointYNew - TempPoint1.Y);
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + (TempPointO.Y - TempPointOLDO.Y));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + ((TempPointO.Y - TempPointOLDO.Y) / 2));

                                    PointF Data = new PointF(DataRoiOLD.CenterPointX, (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                break;
                            }
                        case 5:
                            {
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + (TempPointO.Y - TempPointOLDO.Y));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + (TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + ((TempPointO.Y - TempPointOLDO.Y) / 2));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + ((TempPointO.X - TempPointOLDO.X) / 2));

                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                break;
                            }
                        case 6:
                            {
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - (TempPointO.Y - TempPointOLDO.Y));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + (TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - ((TempPointO.Y - TempPointOLDO.Y) / 2));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth + ((TempPointO.X - TempPointOLDO.X) / 2));

                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                break;
                            }
                        case 7:
                            {
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - (TempPointO.Y - TempPointOLDO.Y));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - (TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight - ((TempPointO.Y - TempPointOLDO.Y) / 2));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - ((TempPointO.X - TempPointOLDO.X) / 2));

                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }

                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                break;
                            }
                        case 8:
                            {
                                //double HeightTemp = Math.Abs(startPointYNew - TempPoint1.Y);
                                if (ListDataRoiF[STT_ListRoi].CenterTypes == ECentreTypes.TheCenterDoesNotChange)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + (TempPointO.Y - TempPointOLDO.Y));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - (TempPointO.X - TempPointOLDO.X));
                                }
                                else
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = Math.Abs(DataRoiOLD.ShapeHeight + ((TempPointO.Y - TempPointOLDO.Y) / 2));
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = Math.Abs(DataRoiOLD.ShapeWidth - ((TempPointO.X - TempPointOLDO.X) / 2));

                                    PointF Data = new PointF((DataRoiOLD.CenterPointX + ((TempPointO.X - TempPointOLDO.X) / 2)), (DataRoiOLD.CenterPointY + ((TempPointO.Y - TempPointOLDO.Y) / 2)));
                                    PointF CenterNew = ConvertXYToPixel(PointMulMatrix(Data.X, Data.Y, MTOLD));
                                    ListDataRoiF[STT_ListRoi].CenterPointX = CenterNew.X;
                                    ListDataRoiF[STT_ListRoi].CenterPointY = CenterNew.Y;
                                }

                                if (ListDataRoiF[STT_ListRoi].ShapeHeight < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeHeight = 0.5f;
                                }
                                if (ListDataRoiF[STT_ListRoi].ShapeWidth < 0.5f)
                                {
                                    ListDataRoiF[STT_ListRoi].ShapeWidth = 0.5f;
                                }
                                break;
                            }
                        case 9:
                            {
                                ListDataRoiF[STT_ListRoi].CenterPointX = DataRoiOLD.CenterPointX + (TempPoint.X - TempPointOLD.X);
                                ListDataRoiF[STT_ListRoi].CenterPointY = DataRoiOLD.CenterPointY + (TempPoint.Y - TempPointOLD.Y);
                                break;
                            }
                        ////case 3:
                        ////    {
                        ////        int with1 = Math.Abs(TempPoint.Y - startPointYNew - ShapeHeight / 2);
                        ////        startPointYNew = startPointYNew + ShapeHeight / 2 - with1;
                        ////        ShapeHeight = with1 * 2;
                        ////        break;
                        ////    }
                        ////case 4:
                        ////    {

                        ////        int with1 = Math.Abs(TempPoint.X - startPointXNew - ShapeWidth / 2);
                        ////        startPointXNew = startPointXNew + ShapeWidth / 2 - with1;
                        ////        ShapeWidth = with1 * 2;
                        ////        break;
                        ////    }

                        case 10:
                            {
                                //Tính toán góc Xoay
                                double TempTan = (float)(TempPoint.Y - ListDataRoiF[STT_ListRoi].CenterPointY) / (TempPoint.X - ListDataRoiF[STT_ListRoi].CenterPointX);
                                float Deg = Math.Abs((float)((180 * Math.Atan(TempTan)) / Math.PI));
                                if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = Deg;
                                }
                                else if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 180 - Deg;
                                }
                                else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 180 + Deg;

                                }
                                else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 360 - Deg;
                                }

                                //if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
                                //{
                                //    ListDataRoiF[STT_ListRoi].Angle = 360 - (-Deg);
                                //}
                                //else if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
                                //{
                                //    ListDataRoiF[STT_ListRoi].Angle = 360 - (180 - Deg);
                                //}
                                //else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
                                //{
                                //    ListDataRoiF[STT_ListRoi].Angle = 360 - (180 - Deg);
                                //}
                                //else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
                                //{
                                //    ListDataRoiF[STT_ListRoi].Angle = 360 - (360 - Deg);
                                //}


                                else if ((TempPoint.Y == ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X > ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 0;
                                }
                                else if ((TempPoint.Y < ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X == ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 270;
                                }
                                else if ((TempPoint.Y == ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X < ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 180;
                                }
                                else if ((TempPoint.Y > ListDataRoiF[STT_ListRoi].CenterPointY) && (TempPoint.X == ListDataRoiF[STT_ListRoi].CenterPointX))
                                {
                                    ListDataRoiF[STT_ListRoi].Angle = 90;
                                }
                                break;
                            }
                        case 0:
                            {
                                if (m_bCheckMouseLower && (m_pLowerGirdle.Y >= m_pCulet.Y + 20 || tempY < 0))
                                {
                                    //m_pLowerGirdle.X = m_pOldPointPos.X + tempX;
                                    m_pLowerGirdle.Y = m_pOldPointPos.Y + tempY;
                                }
                                if (m_bCheckMouseLowerUp && (m_pLowerGirdleUp.Y <= m_pCulet.Y - 20 || tempY < 0))
                                {
                                    // m_pLowerGirdle.X = m_pOldPointPos.X + tempX;
                                    m_pLowerGirdle.Y = m_pOldPointPos.Y - tempY;
                                }
                                //if (m_bCheckMouseLower )
                                //{
                                //    if (m_pLowerGirdle.Y > m_pCulet.Y + 10f)
                                //        return;
                                //    //m_pLowerGirdle.X = m_pOldPointPos.X + tempX;
                                //    m_pLowerGirdle.Y = m_pOldPointPos.Y + tempY;
                                //}
                                //if (m_bCheckMouseLowerUp )
                                //{
                                //    if(m_pLowerGirdleUp.Y > m_pCulet.Y -10f)
                                //        return;
                                //    // m_pLowerGirdle.X = m_pOldPointPos.X + tempX;
                                //    m_pLowerGirdle.Y = m_pOldPointPos.Y - tempY;
                                //}

                                if (m_bCheckMouseAll)
                                {
                                    if (isChangeYPosition)
                                    {
                                        m_pCulet.X = m_pCuletOld.X + tempX;
                                        m_pCulet.Y = m_pCuletOld.Y + tempY;

                                        m_pUpperGirdle.X = m_pUpperGirdleOld.X + tempX;
                                        m_pUpperGirdle.Y = m_pUpperGirdleOld.Y + tempY;

                                        m_pLowerGirdle.X = m_pLowerGirdleOld.X + tempX;
                                        m_pLowerGirdle.Y = m_pLowerGirdleOld.Y + tempY;

                                        m_pTable.X = m_pTableOld.X + tempX;
                                        m_pTable.Y = m_pTableOld.Y + tempY;
                                    }
                                    else
                                    {
                                        m_pCulet.X = m_pCuletOld.X + tempX;
                                        m_pUpperGirdle.X = m_pUpperGirdleOld.X + tempX;
                                        m_pLowerGirdle.X = m_pLowerGirdleOld.X + tempX;
                                        m_pTable.X = m_pTableOld.X + tempX;

                                    }
                                }
                                FunctionMoveImage(currentPoint.X - exOld, currentPoint.Y - eyOld);
                                exOld = currentPoint.X;
                                eyOld = currentPoint.Y;
                                break;
                            }
                        default:
                            break;
                    }
                }




                //if (m_pCulet.Y - m_pLowerGirdle.Y < 5)
                //{
                //    m_pLowerGirdle.Y = m_pCulet.Y + 5;
                //}
                //if (m_pCulet.Y - m_pLowerGirdleUp.Y <5)
                //{
                //    m_pLowerGirdleUp.Y = m_pCulet.Y + 5;
                //}
                //if (m_pTable.X - m_pLowerGirdle.X < 15f)
                //{
                //    m_pLowerGirdle.X = m_pTable.X - 15f;
                //    m_pLowerGirdleUp.X = m_pTable.X - 15f;

                //}
                //if (m_pLowerGirdle.X - m_pUpperGirdle.X < 15f)
                //{
                //    m_pUpperGirdle.X = m_pLowerGirdle.X - 15f;
                //    m_pUpperGirldleUp.X = m_pLowerGirdle.X - 15f;

                //}
                //if (m_pLowerGirdle.Y - m_pUpperGirdle.Y < 0.5f)
                //{
                //    m_pUpperGirdle.Y = m_pLowerGirdle.Y - 0.5f;
                //}

                //if (m_pTable.Y - m_pLowerGirdle.Y < 0.5f)
                //{
                //    m_pLowerGirdle.Y = m_pTable.Y - 0.5f;
                //}



                this.Invalidate();
                //FunctionMoveImage(currentPoint.X - exOld, currentPoint.Y - eyOld);
                //exOld = currentPoint.X;
                //eyOld = currentPoint.Y;
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void ResizeROI: " + ex.ToString();
            }
        }
        float DataLimitX, DataLimitY = 5;
        PointF[] arPoint = new PointF[5];
        //List<Point> arPointEdge = new List<Point>();
        private PointF CalculateTheDifference(PointF OriginalPoint, PointF CurrentPoint)
        {
            float dxCorner = Math.Abs(Math.Max(OriginalPoint.X, CurrentPoint.X) - Math.Min(OriginalPoint.X, CurrentPoint.X));
            float dyCorner = Math.Abs(Math.Max(OriginalPoint.Y, CurrentPoint.Y) - Math.Min(OriginalPoint.Y, CurrentPoint.Y));
            return new PointF(dxCorner, dyCorner);
        }
        private int CheckResize(PointF currentPoint, ERoiTypes TypeROI)
        {
            try
            {
                float SelectMin = Math.Min(ListDataRoiF[STT_ListRoi].ShapeWidth, ListDataRoiF[STT_ListRoi].ShapeHeight);
                PointF currentPointC = ConvertXYToPixel(PointMulMatrix(currentPoint.X, currentPoint.Y, RotaryOrigin));
                float CenterXNew = ListDataRoiF[STT_ListRoi].CenterPointX;
                float CenterYNew = ListDataRoiF[STT_ListRoi].CenterPointY;
                float returnResize = 0;
                arPoint = new PointF[] { ppoint1[STT_ListRoi], ppoint2[STT_ListRoi], ppoint3[STT_ListRoi], ppoint4[STT_ListRoi], ppoint1[STT_ListRoi] };

                float minX = arPoint.Min(x => x.X);
                float minY = arPoint.Min(x => x.Y);
                float maxX = arPoint.Max(x => x.X);
                float maxY = arPoint.Max(x => x.Y);
                if ((currentPointC.X < minX - SelectMin || currentPointC.X > maxX + SelectMin || currentPointC.Y < minY - SelectMin || currentPointC.Y > maxY + SelectMin
                    || minX == maxX || minY == maxY) && !(m_bCheckMouseAll || m_bCheckMouseLowerUp || m_bCheckMouseLower))
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    return 0;
                }
                for (int i = 0; i < arPoint.Length - 1; i++)
                {
                    PointF CornerRec = CalculateTheDifference(arPoint[i], currentPointC);
                    float G = SelectMin / 5;
                    DataLimitX = DataLimitY = SelectMin / 3.9f;
                    if (ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Circle && (i == 1 || i == 3))
                    {
                        DataLimitX = -ListDataRoiF[STT_ListRoi].ShapeWidth / 1.5f;
                        DataLimitY = ListDataRoiF[STT_ListRoi].ShapeHeight / 5f;
                    }
                    if (ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Circle && (i == 0 || i == 2))
                    {
                        DataLimitX = ListDataRoiF[STT_ListRoi].ShapeWidth / 5f;
                        DataLimitY = -ListDataRoiF[STT_ListRoi].ShapeHeight / 1.5f;
                    }

                    if (currentPointC.X < Math.Min(arPoint[i].X, arPoint[i + 1].X) - DataLimitX ||
                    currentPointC.X > Math.Max(arPoint[i].X, arPoint[i + 1].X) + DataLimitX ||
                    currentPointC.Y < Math.Min(arPoint[i].Y, arPoint[i + 1].Y) - DataLimitY ||
                    currentPointC.Y > Math.Max(arPoint[i].Y, arPoint[i + 1].Y) + DataLimitY
                    || CornerRec.X <= G && CornerRec.Y <= G)
                    {
                        if (CornerRec.X <= G && CornerRec.Y <= G && ListDataRoiF[STT_ListRoi].RoiType == ERoiTypes.Rectangle)
                        {
                            returnResize = i + 5;
                            if (returnResize == 5 || returnResize == 7)
                            {
                                this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                            }
                            else
                            {
                                this.Cursor = System.Windows.Forms.Cursors.SizeNESW;
                            }
                            //Console.WriteLine(returnResize);
                            return (int)(returnResize);
                        }

                        float dx1 = Math.Abs(CenterXNew - currentPointC.X);
                        float dy1 = Math.Abs(CenterYNew - currentPointC.Y);
                        if (dy1 <= ListDataRoiF[STT_ListRoi].ShapeHeight / 2 && dx1 <= ListDataRoiF[STT_ListRoi].ShapeWidth / 2)
                        {
                            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
                            returnResize = 9;
                            return 9;
                        }
                        float dx2 = Math.Abs(CenterXNew + (ListDataRoiF[STT_ListRoi].ShapeWidth + SelectMin / 4 + (SelectMin / 3) / 2) - currentPointC.X);
                        float dy2 = Math.Abs(CenterYNew - currentPointC.Y);

                        if (dx2 <= SelectMin / 5 && dy2 <= SelectMin / 5)
                        {
                            this.Cursor = System.Windows.Forms.Cursors.NoMoveVert;
                            returnResize = 10;
                            return 10;
                        }
                    }
                    else
                    {
                        returnResize = i + 1;
                        this.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        //Console.WriteLine(returnResize);         

                        //else if (Math.Abs(currentPointC.X - arPoint[i + 1].X) <= (Select / 5) &&
                        //    Math.Abs(currentPointC.Y - arPoint[i + 1].Y) <= (Select / 5))
                        //{
                        //    this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                        //    returnResize = i + 4;
                        //}             

                        //return (int)(returnResize);

                        //float dx1 = Math.Abs(arPoint[i].X - currentPointC.X);
                        //float dy1 = Math.Abs(arPoint[i].Y - currentPointC.Y);
                        //if (dx1 <= DataLimit && dy1 <= DataLimit)
                        //{
                        //    this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                        //    returnResize = i + 5;
                        //}
                        //else
                        //{
                        //    dx1 = Math.Abs(arPoint[i + 1].X - currentPointC.X);
                        //    dy1 = Math.Abs(arPoint[i + 1].Y - currentPointC.Y);
                        //    if (dx1 <= DataLimit && dy1 <= DataLimit)
                        //    {
                        //        this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                        //        returnResize = i + 6;
                        //        if (returnResize == 9)
                        //        {
                        //            returnResize = 5;
                        //        }
                        //    }
                        //    else
                        //        this.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        //}
                    }
                    if (returnResize == 0)
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                    }
                }
                return (int)(returnResize);
            }
            catch (Exception ex)
            {
                return 0;
                Exception = ">>>>> Int CheckResize: " + ex.ToString();
            }


            ////Down Bottom a = 4;
            //Point Ptemp4 = ConvertXYToPixel(PointMulMatrix(startPointXNew + ShapeWidth / 2, startPointYNew + ShapeHeight, Rotary));
            //arPoint.Add(Ptemp4);
            //Center a = 8;
            //Point Ptemp5 = ConvertXYToPixel(PointMulMatrix(startPointXNew + (ShapeWidth / 2), startPointYNew + (ShapeHeight / 2), Rotary));
            //arPoint.Add(Ptemp5);

            //Center a = 9;
            //Point Ptemp6 = ConvertXYToPixel(PointMulMatrix(startPointXNew, startPointYNew, Rotary));
            //arPoint.Add(Ptemp6);
            //Rotary a = 10;
            //Point Ptemp7 = ConvertXYToPixel(PointMulMatrix(startPointXNew, startPointYNew - (ShapeHeight / 2 + circlePoint / 2 + circlePoint / 4), Rotary));
            //arPoint.Add(Ptemp7);
            //////for (int i = 1; i <= arPoint.Count; i++)
            //////{
            //////    int dx = Math.Abs(arPoint[i - 1].X - currentPoint.X);
            //////    int dy = Math.Abs(arPoint[i - 1].Y - currentPoint.Y);
            //////    if (dx + dy <= circlePoint / 2) returnResize = i;
            //////    if (dx > circlePoint / 2) returnResize = 0;
            //////    if (dy > circlePoint / 2) returnResize = 0;
            //////    if (((dx * dx) + (dy * dy)) <= (circlePoint * circlePoint)) returnResize = i;
            //////    if (returnResize != 0)
            //////    {
            //////        break;
            //////    }
            //////}
        }
        public Tuple<int, double, double, double, double, double> GetDataRoi()
        {
            Tuple<int, double, double, double, double, double> Outdata = Tuple.Create(0, 0.0, 0.0, 0.0, 0.0, 0.0);
            try
            {
                if (ListDataRoiF.Count > 0)
                {
                    Outdata = Tuple.Create(((int)ListDataRoiF[STT_ListRoi].RoiType), (double)(ListDataRoiF[STT_ListRoi].CenterPointX), (double)(ListDataRoiF[STT_ListRoi].CenterPointY), (double)(ListDataRoiF[STT_ListRoi].ShapeWidth), (double)(ListDataRoiF[STT_ListRoi].ShapeHeight), (double)(ListDataRoiF[STT_ListRoi].Angle));
                    return Outdata;
                }
                else
                {
                    return Outdata;
                }
            }
            catch (Exception ex)
            {
                return Outdata;
                Exception = ">>>>> GetDataRoi: " + ex.ToString();
            }

        }
        private Graphics _ViewText;
        private bool View = false;
        private List<DataDispText> ListDataDispText = new List<DataDispText>();
        public void DispText(DataDispText InDataDispText)
        {
            DataDispText DataInput = new DataDispText();
            if (InDataDispText.TextView.Length > 0 && InDataDispText.InPoint != null)
            {
                View = true;
                DataInput.TextView = InDataDispText.TextView;
                DataInput.InPoint = InDataDispText.InPoint;
                DataInput.InColor = InDataDispText.InColor;
                DataInput.InFont = InDataDispText.InFont;
                DataInput.InSize = InDataDispText.InSize;
                ListDataDispText.Add(DataInput);
                this.Invalidate();
            }
        }
        public void DispText(List<DataDispText> InDataDispText)
        {
            if (InDataDispText.Count > 0)
            {
                View = true;
                foreach (DataDispText item in InDataDispText)
                {
                    ListDataDispText.Add(item);
                }
                this.Invalidate();
            }
        }
        public void ClearDispText()
        {
            ListDataDispText.Clear();
            this.Invalidate();
        }
        private void RunDispText()
        {
            try
            {
                if (ListDataDispText.Count > 0)
                {
                    _ViewText.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    _ViewText.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    _ViewText.SmoothingMode = SmoothingMode.AntiAlias;
                    _ViewText.PixelOffsetMode = PixelOffsetMode.Half;
                    foreach (DataDispText item in ListDataDispText)
                    {
                        Font font = new Font(item.InFont, item.InSize, FontStyle.Bold, GraphicsUnit.Pixel);
                        Brush brush = GetBrushFromString(item.InColor);
                        if (brush != null)
                        {
                            StringFormat format = new StringFormat();
                            format.Alignment = StringAlignment.Near;
                            format.LineAlignment = StringAlignment.Near;
                            _ViewText.DrawString(item.TextView, font, brush, item.InPoint, format);
                        }
                        else
                        {
                            Exception = ">>>>> Brush: Not Null, Please Check InColor";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void RunDispText: " + ex.ToString();
            }
        }
        private static Brush GetBrushFromString(string brushName)
        {
            // Lấy thuộc tính tĩnh từ lớp Brushes
            var brushProperty = typeof(Brushes).GetProperty(brushName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            return brushProperty?.GetValue(null) as Brush;
        }
        //public void MoveText(Guid KeyMove, PointF PoinMove)
        //{
        //    if (ListDataDispText.Count > 0)
        //    {
        //        foreach (DataDispText item in ListDataDispText)
        //        {
        //            if (KeyMove == item.Key)
        //            {
        //                item.InPoint = PoinMove;
        //            }
        //        }
        //    }
        //}
        public void MoveText(Guid KeyMove, PointF PoinMove, float ReSize = -1)
        {
            try
            {
                if (ListDataDispText.Count > 0)
                {
                    foreach (DataDispText item in ListDataDispText)
                    {
                        if (KeyMove == item.Key)
                        {
                            item.InPoint = PoinMove;
                            if (ReSize > 0)
                            {
                                item.InSize = ReSize;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception = ">>>>> Void MoveText: " + ex.ToString();
            }
        }
        //public void DisplayRegion(Image<Bgr, byte> InputImage, object Region, Color Color)
        //{
        //    if (Region.GetType() == typeof(List<VectorOfVectorOfPoint>))
        //    {
        //        List<VectorOfVectorOfPoint> Disp = (List<VectorOfVectorOfPoint>)Region;
        //        for (int i = 0; i < Disp.Count; i++)
        //        {
        //            //CvInvoke.DrawContours(imgShow, _outputBlobList[i], -1, new MCvScalar(0, 255, 0), -1);
        //            CvInvoke.FillPoly(InputImage, Disp[i], new MCvScalar(0, 255, 0));
        //        }
        //        Image = InputImage.ToBitmap();
        //    }
        //}
    }
}
