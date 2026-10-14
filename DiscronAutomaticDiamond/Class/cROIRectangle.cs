using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Class
{
    public class cPointType
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the col
        /// </summary>
        /// <value> the col </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Col { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the row
        /// </summary>
        /// <value> the row </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Row { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the Z
        /// </summary>
        /// <value> the Z </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Z { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public cPointType()
        {
            Col = 0;
            Row = 0;
            Z = 0;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"> The x coordinate</param>
        /// <param name="y"> the y coordinate</param>
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public cPointType(double x, double y)
        {
            Col = x;
            Row = y;
            Z = 0;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"> The x coordinate</param>
        /// <param name="y"> the y coordinate</param>
        /// <param name="z"> the z coordinate</param>

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public cPointType(double x, double y, double z)
        {
            Col = x;
            Row = y;
            Z = z;
        }
        internal cPointType Clone()
        {
            cPointType point = new cPointType
            {
                Row = this.Row,
                Col = this.Col,
                Z = this.Z
            };
            return point;
        }
    }

    public class cDrawingBaseType
    {
        /// <summary>
        /// the identifier
        /// </summary>
        private Guid _ID;
        private string _name;
        private string _nameUnsigned;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the identifier
        /// </summary>  
        /// <value> the identifier </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Guid ID
        {
            get => _ID;
            set
            {
                _ID = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// get or set the name
        /// </summary>
        ///  <value> the name</value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _nameUnsigned = GlobFuncs.SwitchToUnsigned(_name);
            }
        }
        public string NameUnsigned => _nameUnsigned;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets or sets the identifier of the parent
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public long ParentID { get; set; }
        public List<double> MarkID { get; set; }
        public long MasterID { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets or sets a value indicating whether this is parent
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool IsParent { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// File or

        /// </summary> 
        /// <value> The Mode</value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public EDrawingModes Mode { get; set; }

        public EDrawingtypes DrawingType { get; set; }
        private ERoiTypes roiType;
        public ERoiTypes RoiType
        {
            get => roiType;
            set
            {
                roiType = value;
                //GetColor();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the color
        /// </summary>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Color Color { get; set; }
        public Color ColorWithAlpha { get; set; }
        private EConnectTypes _ConnectType;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or sets the type of the connect
        /// </summary>
        /// <value> the type of the connect </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public EConnectTypes ConnectType
        {
            get => _ConnectType;
            set
            {
                _ConnectType = value;
                //GetColor();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the type of the animation
        /// </summary>
        /// <value> the types of the animation </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 

        public EAnimationTypes AnimationType { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set the center
        /// </summary>
        /// <value> the center </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public cPointType Center { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set a value indicating weather the passed
        /// </summary>
        /// <value> true if the pass, false if not </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Passed { get; set; }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get or set a Value indicating wheather this is display output
        /// </summary>
        /// <value> True if the display output, false if not </value>
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsDisplayOutPut { get; set; }
        /// <summary>
        /// Dùng để định dạng xem roi này có phải là các roi đặc biệt không
        /// </summary>
        //private EROILegend _roiLegend;
        //public EROILegend RoiLegend
        //{
        //    get => _roiLegend;
        //    set
        //    {
        //        _roiLegend = value;
        //        GetColor();
        //    }
        //}

        public cDrawingBaseType()
        {
            ID = Guid.Empty;
            Name = "NEW ROI";
            ParentID = -1;
            MarkID = new List<double> { };
            MasterID = -1;
            IsParent = false;
            DrawingType = EDrawingtypes.Rectang;
            ConnectType = EConnectTypes.None;
            AnimationType = EAnimationTypes.e2D;
            Mode = EDrawingModes.fill;
            RoiType = ERoiTypes.Normal;
            Center = null;
            Passed = false;
            IsDisplayOutPut = false;
        }

        private void GetColor_ByConnectType()
        {
            switch (_ConnectType)
            {
                case EConnectTypes.Different:
                    Color = Color.Yellow;
                    ColorWithAlpha = Color.FromArgb(80, Color.Yellow);
                    break;
                case EConnectTypes.Intersection:
                    Color = Color.Blue;
                    ColorWithAlpha = Color.FromArgb(80, Color.Blue);
                    break;
                case EConnectTypes.Union:
                    Color = Color.Green;
                    ColorWithAlpha = Color.FromArgb(80, Color.Green);
                    break;
                case EConnectTypes.Reference:
                    Color = Color.Magenta;
                    ColorWithAlpha = Color.FromArgb(80, Color.Magenta);
                    break;
                case EConnectTypes.None:
                    Color = Color.Red;
                    ColorWithAlpha = Color.FromArgb(80, Color.Red);
                    break;
                default:
                    Color = Color.Red;
                    ColorWithAlpha = Color.FromArgb(80, Color.Red);
                    break;
            }
        }
        //private void GetColor()
        //{
        //    if (_roiLegend != EROILegend.None)
        //    {
        //        switch (_roiLegend)
        //        {
        //            case EROILegend.PrimaryRoi:
        //                Color = Color.Violet;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Violet);
        //                break;
        //            case EROILegend.SecondaryROI:
        //                Color = Color.Blue;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Blue);
        //                break;
        //            case EROILegend.PrimaryStepROI:
        //                Color = Color.Red;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Red);
        //                break;
        //            case EROILegend.PrimaryStepROIMaster:
        //                Color = Color.Yellow;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Yellow);
        //                break;
        //        }
        //    }
        //    else
        //        switch (RoiType)
        //        {
        //            case ERoiTypes.Normal:
        //                GetColor_ByConnectType();
        //                break;
        //            case ERoiTypes.Mark:
        //                Color = Color.Gray;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Gray);
        //                break;
        //            case ERoiTypes.Master:
        //                Color = Color.Pink;
        //                ColorWithAlpha = Color.FromArgb(80, Color.Pink);
        //                break;
        //            default:
        //                GetColor_ByConnectType();
        //                break;
        //        }
        //}
    }
    internal class cExtendData
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Row { get; set; }
        public double Col { get; set; }
        public double Phi { get; set; }
        public cExtendData()
        {
            Width = 0;
            Height = 0;
            Row = 0;
            Col = 0;
            Phi = 0;
        }
    }
    /// <summary>
    /// A rectang type 
    /// </summary>
    public class cRectangType : cDrawingBaseType
    {
        /// <summary>
        /// Chiều rộng
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Chiều cao
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Góc xoay
        /// </summary>
        public double Phi { get; set; }
        public int ActionID {  get; set; }

        // TODO: 181224
        public int DescriptionCode { get; set; }
        public string DescriptionName { get; set; }

        /// <summary>
        /// Check vị trị: nếu là 0 thì check đỉnh của kim cương
        /// </summary>
        public int Position { get; set; }
        public cRectangType()
        {
            Mode = EDrawingModes.fill;
            DrawingType = EDrawingtypes.Rectang;
            Width = 100;
            Height = 100;
            Phi = 0;
            Center = new cPointType(100, 100);
        }
        public cRectangType(Image Image)
        {
            Mode = EDrawingModes.fill;
            DrawingType = EDrawingtypes.Rectang;
            Width = 100;
            Height = 20;
            Phi = 0;
            Center = new cPointType(100, 100);

            if (Image != null)
            {
                int width = Image.Width;
                int height = Image.Height;
                if (Width > width)
                {
                    Width = (int)Width / 4;
                    Center.Row = Width;
                }
                else if (width / Width >= 10)
                {
                    Width = width / 10;
                    Center.Row = Width;
                }
                if (Height >= height)
                {
                    Height = (int)height / 4;
                    Center.Col = Height;
                }
                else if (height / Height >= 10)
                {
                    Height = height / 10;
                    Center.Col = Height;
                }
            }
        }
        internal void UpdateExtendData(cExtendData extendData)
        {
            Width += extendData.Width;
            Height += extendData.Height;
            Center.Row += extendData.Row;
            Phi += extendData.Phi;
        }

        /// <summary>
        /// Khởi tạo đối tượng đi kèm ảnh gốc
        /// </summary>
        /// <param name="image">ẢNH GỐC</param>
        public cRectangType(Bitmap bitmap)
        {
            Mode = EDrawingModes.fill;
            DrawingType = EDrawingtypes.Rectang;
            Width = 100;
            Height = 100;
            Phi = 0;
            Center = new cPointType(100, 100);
            if (bitmap != null)
            {
                // Lấy ra chiều cao của ảnh
                double width = bitmap.Size.Width;
                double height = bitmap.Size.Height;
                if (Width >= width)
                {
                    Width = (int)width / 4;
                    Center.Row = Width;
                }
                else if (width / Width >= 10)
                {
                    Width = width / 10;
                    Center.Row = Width;
                }
                if (Height >= height)
                {
                    Height = (int)height / 4;
                    Center.Col = Height;
                }
                else if (width / Width >= 10)
                {
                    Height = height / 10;
                    Center.Col = Height;
                }
            }
        }
        internal cRectangType Clone()
        {
            cRectangType clone = new cRectangType
            {
                Width = this.Width,
                Height = this.Height,
                Center = this.Center.Clone(),
                Phi = this.Phi
            };
            return clone;
        }


    }
}
