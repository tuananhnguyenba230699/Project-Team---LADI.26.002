using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.Util;

namespace Blob_View
{
    public class BlobView
    {
        /// <summary>
        /// INPUT
        /// </summary>
        public Image<Gray, byte> InputImage
        {
            get {  return _inputImage; }
            set { _inputImage = value; }
        }

        public List<VectorOfVectorOfPoint> InputBlobList
        {
            set { _inputBlobList = value; }
        }
        public List<double> InputAreaList
        {
            set { _inputAreaList = value; }
        }
        public List<int> InputWidthList
        {
            set { _inputWidthList = value; }
        }
        public List<int> InputHeightList
        {
            set { _inputHeightList = value; }
        }
        public PointF PositionMouse
        {
            set { _positionMouse = value; }
        }
        public bool IsShowImageResult
        {
            set { _isShowImageResult = value; }
        }
        /// <summary>
        /// OUTPUT
        /// </summary>


        public bool Passed
        {
            get { return _passed; }
        }
        public string ErrMessage
        {
            get { return _errMessage; }
        }
        public Bitmap OutputImageShow
        {
            get { return _outputImageShow; }
        }
        public double AreaActual
        {
            get { return _areaActual; }
        }
        public double RowActual
        {
            get { return _rowActual; }
        }
        public double ColumnActual
        {
            get { return _columnActual; }
        }
        public double OuterRadiusActual
        {
            get { return _outerRadiusActual; }
        }
        public double CircularityActual
        {
            get { return _circularityActual; }
        }
        public double WidthActual
        {
            get { return _widthActual; }
        }
        public double HeightActual
        {
            get { return _heightActual; }
        }

        /// <summary>
        /// Các biến cục bộ sủ dụng trong Class
        /// </summary>
        private Image<Gray, byte> _inputImage = null;
        private List<VectorOfVectorOfPoint> _inputBlobList = null;
        private List<double> _inputAreaList = null;
        private List<int> _inputWidthList = null;
        private List<int> _inputHeightList = null;
        private bool _isShowImageResult = false;
        private PointF _positionMouse = new PointF(-1, -1);


        private string _errMessage = null;

        private bool _passed = false;
        private Bitmap _outputImageShow = null;
        private double _areaActual = 0;
        private double _rowActual = 0;
        private double _columnActual = 0;
        private double _outerRadiusActual = 0;
        private double _circularityActual = 0;
        private double _widthActual = 0;
        private double _heightActual = 0;

        public bool Run()
        {
            //Kiểm tra thuộc tính đầu vào
            if (_inputImage == null || _inputImage.Width == 0)
            {
                _errMessage = "Run: Lỗi InputImage = null";
                return false;
            }
            if (_inputBlobList == null)
            {
                _errMessage = "Run: Lỗi InputBlobList = null";
                return false;
            }
            if (_inputAreaList == null)
            {
                _errMessage = "Run: Lỗi InputAreaList = null";
                return false;
            }
            if (_inputWidthList == null)
            {
                _errMessage = "Run: Lỗi InputWidthList = null";
                return false;
            }
            if (_inputHeightList == null)
            {
                _errMessage = "Run: Lỗi InputHeightList = null";
                return false;
            }
            try
            {
                //Paint Region
                if (_isShowImageResult)
                {
                    var imgShow = _inputImage.Convert<Bgr, byte>();
                    for (int i = 0; i < _inputBlobList.Count; i++)
                    {
                        //CvInvoke.DrawContours(imgShow, _outputBlobList[i], -1, new MCvScalar(0, 255, 0), -1);
                        CvInvoke.FillPoly(imgShow, _inputBlobList[i], new MCvScalar(0, 255, 0));
                    }
                    _outputImageShow = imgShow.ToBitmap();
                   // _outputImage.Save(@"D:\Test\test.bmp");
                    imgShow.Dispose();
                }
            }
            catch (Exception ex)
            {
                _errMessage = "Run: " + ex.Message + "\n" + ex.StackTrace;
                return false;
            }
            return true;
        }
        public bool ClickMouse()
        {
            if (_positionMouse.X == -1 || _positionMouse.Y == -1)
            {
                _errMessage = "ClickMouse: Lỗi chưa set PositionMouse";
                return false;
            }

            try
            {
                _areaActual = 0;
                _widthActual = 0;
                _heightActual = 0;
                _rowActual = 0;
                _columnActual = 0;
                _outerRadiusActual = 0;
                _circularityActual = 0;
                double minDistance = (double)1 / 0;
                int indexPosition = -1;
                if (_inputBlobList!= null && _inputBlobList.Count > 0)
                {
                    for (int i = 0; i < _inputBlobList.Count; i++)
                    {
                        var contour = _inputBlobList[i];
                        double result = CvInvoke.PointPolygonTest(contour[0], _positionMouse, false);
                        bool isInside = false;
                        if (contour.Size > 1)
                        {
                            for (int j = 1; j < contour.Size; j++)
                            {
                                double inSide = CvInvoke.PointPolygonTest(contour[j], _positionMouse, false);
                                if (inSide >= 0)
                                {
                                    isInside = true;
                                    break;
                                }

                            }
                        }
                        if (result >= 0 && isInside == false)
                        {
                            double distance = CvInvoke.PointPolygonTest(contour[0], _positionMouse, true);
                            if (distance < minDistance)
                            {
                                indexPosition = i;
                            }
                        }

                    }
                    if (indexPosition != -1)
                    {
                        _areaActual = _inputAreaList[indexPosition];
                        _widthActual = _inputWidthList[indexPosition];
                        _heightActual = _inputHeightList[indexPosition];
                    }
                }

            }
            catch (Exception ex)
            {
                _errMessage = "ClickMouse: " + ex.Message + "\n" + ex.StackTrace;
                return false;
            }
            return true;
        }
    }
}
