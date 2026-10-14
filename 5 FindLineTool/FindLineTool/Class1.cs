using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCBase.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace LineFindTool
{
    public class LineFind
    {
        /// <summary>
        /// INPUT
        /// </summary>
        public Image<Gray, byte> InputImage
        {
            set { _inputImage = value; }
        }
        public Tuple<PointF, double> ToolOrigin
        {
            set { _toolOrigin = value; }
        }
        public PointF[] InSetOrigin
        {
            set { _inSetOrigin = value; }
        }
        public RTCRectangle ROI
        {
            set { _ROI = value; }
        }
        public string EdgeTransition
        {
            set { _edgeTransition = value; }
        }
        public string EdgeType
        {
            set { _edgeType = value; }
        }
        public int EdgeDetectionThreshold
        {
            set { _edgeDetectionThreshold = value; }
        }
        public int OutlierDistanceThreshold
        {
            set { _outlierDistanceThreshold = value; }
        }
        public int SamplingPercent
        {
            set { _samplingPercent = value; }
        }
        public int StepFindPoint
        {
            set { _stepFindPoint = value; }
        }
        public double MaxPercentageOfOutliers
        {
            set { _maxPercentageOfOutliers = value; }
        }
        public int MinEdgePointNumber
        {
            set { _minEdgePointNumber = value; }
        }
        public bool EnableGapLengthCheck
        {
            set { _enableGapLengthCheck = value; }
        }
        public bool EnableLineLengthCheck
        {
            set { _enableLineLengthCheck = value; }
        }
        public bool EnableAngleRangeCheck
        {
            set { _enableAngleRangeCheck = value; }
        }
        public Tuple<double, double> GapLengthRange
        {
            set { _gapLengthRange = value; }
        }
        public Tuple<double, double, double> LineLengthTolerance
        {
            set { _lineLengthTolerance = value; }
        }
        public Tuple<double, double, double> LineAngleTolerance
        {
            set { _lineAngleTolerance = value; }
        }
        public bool IsShowImageResult
        {
            set { _isShowImageResult = value; }
        }
        /// <summary>
        /// OUTPUT
        /// </summary>
        public List<Point> EdgePoints
        {
            get { return _edgePoints; }
        }
        public List<Point> OutlierPoints
        {
            get { return _outlierPoints; }
        }
        public List<Point> OutputPointList
        {
            get { return _outputPointList; }
        }
        public Tuple<Point, Point> LineSegment
        {
            get { return _lineSegment; }
        }
        public double GreatestGapLength
        {
            get { return _greatestGapLength; }
        }
        public double GreatestOutlierDistance
        {
            get { return _greatestOutlierDistance; }
        }
        public double PercentageOfOutliers
        {
            get { return _percentageOfOutliers; }
        }
        public double LineLengthActual
        {
            get { return _lineLengthActual; }
        }
        public double LineAngleActual
        {
            get { return _lineAngleActual; }
        }
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

        /// <summary>
        /// Các biến cục bộ sủ dụng trong Class
        /// </summary>
        private Image<Gray, byte> _inputImage = null;
        private RTCRectangle _ROI = null;
        private Tuple<PointF, double> _toolOrigin = null;
        private PointF[] _inSetOrigin = null;
        private string _edgeTransition = null;
        private string _edgeType = null;
        private int _edgeDetectionThreshold = -1;
        private int _outlierDistanceThreshold = -1;
        private int _samplingPercent = -1;
        private double _maxPercentageOfOutliers = -1;
        private int _minEdgePointNumber = -1;
        private bool _enableGapLengthCheck = false;
        private bool _enableLineLengthCheck = false;
        private bool _enableAngleRangeCheck = false;
        private Tuple<double, double> _gapLengthRange = null;
        private Tuple<double, double, double> _lineLengthTolerance = null;
        private Tuple<double, double, double> _lineAngleTolerance = null;
        private int _stepFindPoint = 5;
        private bool _isShowImageResult = false;


        private string _errMessage = null;
        private List<Point> _edgePoints = null;
        private List<Point> _outlierPoints = null;
        private List<Point> _outputPointList = null;
        private Tuple<Point, Point> _lineSegment = null;
        private double _greatestGapLength = 0;
        private double _greatestOutlierDistance = 0;
        private double _percentageOfOutliers = 0;
        private double _lineLengthActual = 0;
        private double _lineAngleActual = 0;
        private bool _passed = false;
        private Bitmap _outputImageShow = null;

        /// <summary>
        ///  SetROI() có chức năng cài đặt ROI gắn với ToolOrigin .
        ///  Cần set thuộc tính: ToolOrigin, ROI (có thể Set ROI == null) .
        ///  Mỗi lần thay đổi link ToolOrigin hoặc ROI thì cần chạy lại hàm .
        /// </summary>
        /// <returns></returns>
        private bool SetROI()
        {
            if (_toolOrigin == null)
            {
                _errMessage = "SetToolOrigin: Lỗi chưa set ToolOrigin";
                return false;
            }
            try
            {
                _inSetOrigin = new PointF[4];
                if (_ROI == null || _ROI.Width == 0 || _ROI.Height == 0)
                {
                    _inSetOrigin = null;
                }
                else
                {
                    //Hàm GetRectangleVertices() lấy tọa độ 4 đỉnh của rectangle
                    GetRectangleVertices(_ROI, out PointF[] inPointROI);

                    //Chuyển lần lượt tọa độ 4 đỉnh ROI trong hệ tọa độ ảnh về hệ tọa độ ToolOrigin
                    for (int i = 0; i < 4; i++)
                    {
                        _inSetOrigin[i] = ConvertCoordinatesToNewOrigin(_toolOrigin, inPointROI[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                _errMessage = "SetToolOrigin: " + ex.Message + "\n" + ex.StackTrace;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Phải chạy SetROI trước khi chạy Run() .
        /// Phải set lại ToolOrigin nếu ToolOrigin thay đổi .
        /// Set các thuộc tính: InputImage,ToolOrigin,EdgeTransition,EdgeType,EdgeDetectionThreshold,OutlierDistanceThreshold
        /// SamplingPercent,MaxPercentageOfOutliers,MinEdgePointNumber,EnableGapLengthCheck,EnableLineLengthCheck,EnableAngleRangeCheck,
        /// GapLengthRange,LineLengthTolerance,LineAngleTolerance .
        /// Output: EdgePoints,OutlierPoints,OutputPointList,LineSegment,GreatestGapLength,GreatestOutlierDistance,PercentageOfOutliers,
        /// LineLengthActual,LineAngleActual,Passed,OutputImage,ErrMessage .
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            if (_inputImage == null || _inputImage.Width == 0)
            {
                _errMessage = "Run: Lỗi InputImage = null";
                return false;
            }
            if (_edgeTransition == null)
            {
                _errMessage = "Run: Lỗi chưa set EdgeTransition";
                return false;
            }
            if (_edgeType == null)
            {
                _errMessage = "Run: Lỗi chưa set EdgeType";
                return false;
            }
            if (_edgeDetectionThreshold == -1)
            {
                _errMessage = "Run: Lỗi chưa set EdgeDetectionThreshold";
                return false;
            }
            if (_outlierDistanceThreshold == -1)
            {
                _errMessage = "Run: Lỗi chưa set OutlierDistanceThreshold";
                return false;
            }
            if (_samplingPercent == -1)
            {
                _errMessage = "Run: Lỗi chưa set SamplingPercent";
                return false;
            }
            if (_maxPercentageOfOutliers == -1)
            {
                _errMessage = "Run: Lỗi chưa set MaxPercentageOfOutliers";
                return false;
            }
            if (_minEdgePointNumber == -1)
            {
                _errMessage = "Run: Lỗi chưa set MinEdgePointNumber";
                return false;
            }


            _outputPointList = new List<Point>();
            _edgePoints = new List<Point>();
            _outlierPoints = new List<Point>();
            _lineSegment = null;
            _greatestGapLength = 0;
            _greatestOutlierDistance = 0;
            _percentageOfOutliers = 0;
            _lineLengthActual = 0;
            _lineAngleActual = 0;
            _passed = false;
            _outputImageShow = null;
            _errMessage = null;

            //Set ToolOrigin cho ROI
            //SetROI();
            try
            {

                //var _outputImage = _inputImage.Copy();
                //var imgFind = _inputImage.ToImage<Gray,byte>();
                //_outputImage = _inputImage;
                PointF[] pointRs = new PointF[4];
                for (int i = 0; i < 4; i++)
                {
                    pointRs[i] = ConvertCoordinatesToOrigin(_toolOrigin, _inSetOrigin[i]);
                }
                int width = (int)Math.Round(_ROI.Width);
                int height = (int)Math.Round(_ROI.Height);
                var srcPoints = pointRs;
                var dstPoints = new PointF[] {
                         new PointF((float)_ROI.Width - 1, 0),
                        new PointF(0, 0),
                        new PointF(0, (float)_ROI.Height - 1),
                        new PointF((float)_ROI.Width - 1,  (float)_ROI.Height - 1)};
                var M = CvInvoke.GetPerspectiveTransform(srcPoints, dstPoints);
                var Matrix = CvInvoke.GetPerspectiveTransform(dstPoints, srcPoints);
                var warped = new Mat();
                CvInvoke.WarpPerspective(_inputImage, warped, M, new Size(width, height));
                //var imgTest =  warped.ToBitmap();

                Matrix<byte> matrix = new Matrix<byte>(warped.Size);
                warped.CopyTo(matrix);
                List<Point> points = new List<Point>();
                var step = (int)Math.Round(100.0 / _samplingPercent);

                var watch1 = new System.Diagnostics.Stopwatch();
                watch1.Start();

                //Tìm các điểm thỏa mãm điều kiện để hồi quy đường thẳng
                if (_stepFindPoint < 3)
                {
                    _stepFindPoint = 3;
                }
                if (_edgeTransition == RTCConst.cLineFindTool.EdgeTransition_LighttoDark && _edgeType == RTCConst.cLineFindTool.EdgeType_FirstEdge)
                {
                    for (int i = 0; i < warped.Height;)
                    {
                        for (int j = _stepFindPoint; j < warped.Width; j = j + 2)
                        {
                            if (matrix[i, j - _stepFindPoint] - matrix[i, j] >= _edgeDetectionThreshold)
                            {
                                if (j > _stepFindPoint)
                                {
                                    if (matrix[i, j - _stepFindPoint - 1] - matrix[i, j - 1] >= _edgeDetectionThreshold)
                                    {
                                        _outputPointList.Add(new Point(j - 1, i));
                                    }
                                    else
                                    {
                                        _outputPointList.Add(new Point(j, i));
                                    }
                                }
                                else
                                {
                                    _outputPointList.Add(new Point(j, i));
                                }
                                break;
                            }

                        }
                        i = i + step;
                    }
                }
                if (_edgeTransition == RTCConst.cLineFindTool.EdgeTransition_DarktoLight && _edgeType == RTCConst.cLineFindTool.EdgeType_FirstEdge)
                {
                    for (int i = 0; i < warped.Height;)
                    {
                        for (int j = _stepFindPoint; j < warped.Width; j = j + 2)
                        {
                            if (matrix[i, j] - matrix[i, j - _stepFindPoint] >= _edgeDetectionThreshold)
                            {
                                if (j > _stepFindPoint)
                                {
                                    if (matrix[i, j - 1] - matrix[i, j - _stepFindPoint - 1] >= _edgeDetectionThreshold)
                                    {
                                        _outputPointList.Add(new Point(j - 1, i));
                                    }
                                    else
                                    {
                                        _outputPointList.Add(new Point(j, i));
                                    }
                                }
                                else
                                {
                                    _outputPointList.Add(new Point(j, i));
                                }
                                break;
                            }
                        }
                        i = i + step;
                    }
                }
                if (_edgeTransition == RTCConst.cLineFindTool.EdgeTransition_LighttoDark && _edgeType == RTCConst.cLineFindTool.EdgeType_LastEdge)
                {
                    for (int i = 0; i < warped.Height;)
                    {
                        for (int j = warped.Width - 1; j > _stepFindPoint - 1; j = j - 2)
                        {
                            if (matrix[i, j - _stepFindPoint] - matrix[i, j] >= _edgeDetectionThreshold)
                            {
                                if (j < warped.Width)
                                {
                                    if (matrix[i, j - _stepFindPoint + 1] - matrix[i, j + 1] >= _edgeDetectionThreshold)
                                    {
                                        _outputPointList.Add(new Point(j - _stepFindPoint + 1, i));
                                    }
                                    else
                                    {
                                        _outputPointList.Add(new Point(j - _stepFindPoint, i));
                                    }
                                }
                                else
                                {
                                    _outputPointList.Add(new Point(j - _stepFindPoint, i));
                                }
                                break;
                            }
                        }
                        i = i + step;
                    }
                }
                if (_edgeTransition == RTCConst.cLineFindTool.EdgeTransition_DarktoLight && _edgeType == RTCConst.cLineFindTool.EdgeType_LastEdge)
                {
                    for (int i = 0; i < warped.Height;)
                    {
                        for (int j = warped.Width - 1; j > _stepFindPoint - 1; j--)
                        {
                            if (matrix[i, j] - matrix[i, j - _stepFindPoint] >= _edgeDetectionThreshold)
                            {
                                if (j < warped.Width)
                                {
                                    if (matrix[i, j + 1] - matrix[i, j - _stepFindPoint + 1] >= _edgeDetectionThreshold)
                                    {
                                        _outputPointList.Add(new Point(j - _stepFindPoint + 1, i));
                                    }
                                    else
                                    {
                                        _outputPointList.Add(new Point(j - _stepFindPoint, i));
                                    }
                                }
                                else
                                {
                                    _outputPointList.Add(new Point(j - _stepFindPoint, i));
                                }

                                break;
                            }
                        }
                        i = i + step;
                    }
                }


                //PointF[] pointFs = points.Select(p => new PointF(p.X, p.Y)).ToArray();
                //pointFs = CvInvoke.PerspectiveTransform(pointFs, Matrix);
                //points = pointFs.Select(p => new Point((int)p.X, (int)p.Y)).ToList();

                if (_outputPointList.Count == 0)
                {
                    _errMessage = "number of points found is null";
                    if (_isShowImageResult)
                    {
                        _outputImageShow = _inputImage.Convert<Bgr, byte>().ToBitmap();
                    }
                    return false;

                }

                Point pointSegment1 = new Point();
                Point pointSegment2 = new Point();
                PointF pSeg1 = new PointF();
                PointF pSeg2 = new PointF();
                double a = new double();
                double b = new double();
                double c = new double();

                //Test thuat toan RANSAC/////////////////////////////////////////////////
                //var watch = new System.Diagnostics.Stopwatch();

                var lineBest = FitLine(_outputPointList, out _edgePoints, out _outlierPoints);


                ////////////////////////////////////////////////////////////////////////

                pSeg1 = lineBest.P1;
                pSeg2 = lineBest.P2;

                // Tìm khoảng cách lớn nhất giữa 2 edgePoint liên tiếp
                for (int i = 1; i < _edgePoints.Count; i++)
                {
                    double longGap = (double)Math.Sqrt(Math.Pow(_edgePoints[i - 1].X - _edgePoints[i].X, 2) + Math.Pow(_edgePoints[i - 1].Y - _edgePoints[i].Y, 2));
                    if (_greatestGapLength < longGap)
                    {
                        _greatestGapLength = longGap;
                    }
                }

                //Tìm khoảng cách lớn nhất từ điểm ngoại lệ đến đường thẳng tìm được
                if (_outlierPoints.Count > 0)
                {
                    for (int i = 1; i < _outlierPoints.Count; i++)
                    {
                        double outlierDistance = Math.Abs(a * _outlierPoints[i].X + b * _outlierPoints[i].Y + c) / (Math.Sqrt(a * a + b * b));
                        if (_greatestOutlierDistance < outlierDistance)
                        {
                            _greatestOutlierDistance = outlierDistance;
                        }
                    }
                }



                _percentageOfOutliers = 100 * (double)_outlierPoints.Count / (double)_outputPointList.Count;
                _passed = true;
                if (_percentageOfOutliers > _maxPercentageOfOutliers || _minEdgePointNumber > _edgePoints.Count)
                {
                    _passed = false;
                }
                if (_enableGapLengthCheck)
                {
                    if (_gapLengthRange == null)
                    {
                        _errMessage = "Run: Lỗi chưa set GapLengthRange";
                        _passed = false;
                        return false;
                    }
                    if (_greatestGapLength < _gapLengthRange.Item1 || _greatestGapLength > _gapLengthRange.Item2)
                    {
                        _passed = false;
                    }
                }


                PointF[] outPL = _outputPointList.Select(p => new PointF((int)p.X, (int)p.Y)).ToArray();
                outPL = CvInvoke.PerspectiveTransform(outPL, Matrix);
                _outputPointList = outPL.Select(p => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y))).ToList();

                outPL = _edgePoints.Select(p => new PointF((int)p.X, (int)p.Y)).ToArray();
                outPL = CvInvoke.PerspectiveTransform(outPL, Matrix);
                _edgePoints = outPL.Select(p => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y))).ToList();

                outPL = _outlierPoints.Select(p => new PointF((int)p.X, (int)p.Y)).ToArray();
                outPL = CvInvoke.PerspectiveTransform(outPL, Matrix);
                _outlierPoints = outPL.Select(p => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y))).ToList();

                outPL = new PointF[] { pSeg1, pSeg2 };
                outPL = CvInvoke.PerspectiveTransform(outPL, Matrix);
                pSeg1 = outPL[0];
                pointSegment1 = new Point((int)Math.Round(pSeg1.X), (int)Math.Round(pSeg1.Y));
                pSeg2 = outPL[1];
                pointSegment2 = new Point((int)Math.Round(pSeg2.X), (int)Math.Round(pSeg2.Y));



                _lineSegment = new Tuple<Point, Point>(pointSegment1, pointSegment2);

                //Tìm chiều dài đường thẳng tìm được
                _lineLengthActual = (double)Math.Sqrt(Math.Pow(pointSegment1.X - pointSegment2.X, 2) + Math.Pow(pointSegment1.Y - pointSegment2.Y, 2));
                // Tìm góc của đường thẳng tìm được
                PointF vecLine = new PointF(pointSegment2.X - pointSegment1.X, pointSegment2.Y - pointSegment1.Y);
                double CosAngle = (double)vecLine.X / Math.Sqrt(Math.Pow(vecLine.X, 2) + Math.Pow(vecLine.Y, 2));
                _lineAngleActual = Math.Acos(CosAngle);
                if (pointSegment2.Y > pointSegment1.Y)
                {
                    _lineAngleActual = -_lineAngleActual;
                }

                //var outImg = _inputImage.ToImage<Bgr, byte>();

                if (_enableLineLengthCheck)
                {
                    if (_lineLengthTolerance == null)
                    {
                        _errMessage = "Run: Lỗi chưa set LineLengthTolerance";
                        _passed = false;
                        return false;
                    }
                    if (_lineLengthActual < _lineLengthTolerance.Item2 - _lineLengthTolerance.Item1 || _lineLengthActual > _lineLengthTolerance.Item2 + _lineLengthTolerance.Item3)
                    {
                        _passed = false;
                    }
                }
                if (_enableAngleRangeCheck)
                {
                    if (_lineAngleTolerance == null)
                    {
                        _errMessage = "Run: Lỗi chưa set LineAngleTolerance";
                        _passed = false;
                        return false;
                    }
                    if (_lineAngleActual < _lineAngleTolerance.Item2 - _lineAngleTolerance.Item1 || _lineAngleActual > _lineAngleTolerance.Item2 + _lineAngleTolerance.Item3)
                    {
                        _passed = false;
                    }
                }
                if (_isShowImageResult)
                {
                    GetImageResult();
                }
                //if (_passed)
                //{
                //    CvInvoke.ArrowedLine(_outputImage, pointSegment1, pointSegment2, new MCvScalar(0, 255, 0), (int)_inputImage.Width / 400);
                //}
                //else
                //{
                //    CvInvoke.ArrowedLine(_outputImage, pointSegment1, pointSegment2, new MCvScalar(0, 0, 255), (int)_inputImage.Width / 400);
                //}


                //Vẽ ROI
                //CvInvoke.Line(outImg, intBox[0], intBox[1], new MCvScalar(0, 150, 255), 2);
                //CvInvoke.Line(outImg, intBox[1], intBox[2], new MCvScalar(0, 150, 255), 2);
                //CvInvoke.ArrowedLine(outImg, intBox[2], intBox[3], new MCvScalar(0, 150, 255), 2);
                //CvInvoke.Line(outImg, intBox[3], intBox[0], new MCvScalar(0, 150, 255), 2);
                //_outputImage = _outputImage.ToBitmap();
                //_outputImage = imgTest;

            }
            catch (Exception ex)
            {
                _errMessage = "Run: " + ex.Message + "\n" + ex.StackTrace;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ConvertCoordinatesToNewOrigin() chuyển tọa độ điểm "point" từ tọa độ gốc ảnh về hệ tọa độ "toolOrigin"
        /// </summary>
        /// <param name="POrigin"></param>
        /// <param name="Angle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private PointF ConvertCoordinatesToNewOrigin(Tuple<PointF, double> toolOrigin, PointF point)
        {
            PointF p = new PointF();
            var A = new Matrix<double>(new double[,] {
                { Math.Cos(toolOrigin.Item2),-Math.Sin(toolOrigin.Item2),toolOrigin.Item1.X },
                { Math.Sin(toolOrigin.Item2),Math.Cos(toolOrigin.Item2),toolOrigin.Item1.Y },
                { 0,0,1} });
            var B = new Matrix<double>(new double[,]
            {
                {point.X },
                {point.Y},
                {1 }
            });
            var AT = new Matrix<double>(3, 1);
            CvInvoke.Invert(A, AT, DecompMethod.LU);
            var C = new Matrix<double>(3, 1);
            CvInvoke.Gemm(AT, B, 1.0, null, 0, C);
            p.X = (int)C.Data[0, 0];
            p.Y = (int)C.Data[1, 0];
            return p;
        }

        /// <summary>
        /// ConvertCoordinatesToOrigin() Chuyển tọa độ điểm "point" từ hệ tọa độ "toolOrigin" về hệ tọa độ ảnh
        /// </summary>
        /// <param name="toolOrigin"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static PointF ConvertCoordinatesToOrigin(Tuple<PointF, double> Coordinates, PointF point)
        {
            PointF p = new Point();
            var Angle = Coordinates.Item2 * Math.PI / 180;
            var A = new Matrix<double>(new double[,] {
                { Math.Cos(Angle),-Math.Sin(Angle),Coordinates.Item1.X},
                { Math.Sin(Angle),Math.Cos(Angle),Coordinates.Item1.Y},
                { 0,0,1} });
            var B = new Matrix<double>(new double[,]
            {
                {point.X },
                {point.Y},
                {1 }
            });
            var C = new Matrix<double>(3, 1);
            CvInvoke.Gemm(A, B, 1.0, null, 0, C);
            p.X = (int)C.Data[0, 0];
            p.Y = (int)C.Data[1, 0];
            return p;

        }

        /// <summary>
        /// GetRectangleVertices() lấy tọa độ 4 đỉnh của rectangle
        /// </summary>
        /// <param name="recROI"></param>
        /// <param name="pointRec"></param>
        private void GetRectangleVertices(RTCRectangle recROI, out PointF[] pointRec)
        {
            var halfwidth = recROI.Width;
            var halfheight = recROI.Height;
            var angle = recROI.Phi;
            var centerPoint = recROI.Center;
            double diag = Math.Sqrt(halfwidth * halfwidth + halfheight * halfheight);
            PointF vertex1 = new PointF(Convert.ToSingle(centerPoint.X + diag / 2 * Math.Cos(angle + Math.Atan(halfheight / halfwidth))), Convert.ToSingle(centerPoint.Y - diag / 2 * Math.Sin(angle + Math.Atan(halfheight / halfwidth))));
            PointF vertex2 = new PointF(Convert.ToSingle(centerPoint.X - diag / 2 * Math.Cos(angle - Math.Atan(halfheight / halfwidth))), Convert.ToSingle(centerPoint.Y + diag / 2 * Math.Sin(angle - Math.Atan(halfheight / halfwidth))));
            PointF vertex3 = new PointF(Convert.ToSingle(centerPoint.X - diag / 2 * Math.Cos(angle + Math.Atan(halfheight / halfwidth))), Convert.ToSingle(centerPoint.Y + diag / 2 * Math.Sin(angle + Math.Atan(halfheight / halfwidth))));
            PointF vertex4 = new PointF(Convert.ToSingle(centerPoint.X + diag / 2 * Math.Cos(angle - Math.Atan(halfheight / halfwidth))), Convert.ToSingle(centerPoint.Y - diag / 2 * Math.Sin(angle - Math.Atan(halfheight / halfwidth))));
            pointRec = new PointF[] { vertex1, vertex2, vertex3, vertex4 };
        }

        private LineSegment2D FitLine(List<Point> listPoint, out List<Point> edgePoints, out List<Point> outlierPoints)
        {
            edgePoints = new List<Point>();
            outlierPoints = new List<Point>();
            PointF[] points = listPoint.ConvertAll(p => new PointF(p.X, p.Y)).ToArray();
            PointF direction = new PointF();
            PointF pointOnLine = new PointF();
            CvInvoke.FitLine(points, out direction, out pointOnLine, Emgu.CV.CvEnum.DistType.L12, 0, 0.01, 0.01);
            double a = direction.Y / direction.X;
            double b = pointOnLine.Y - a * pointOnLine.X;
            LineSegment2D line = new LineSegment2D(new Point((int)Math.Round((listPoint[0].Y - b) / a), (int)listPoint[0].Y), new Point((int)Math.Round((listPoint[listPoint.Count - 1].Y - b) / a), (int)listPoint[listPoint.Count - 1].Y));
            foreach (var point in points)
            {
                double distance = DistancePointToLine(point, line);
                if (distance <= 3)
                {
                    edgePoints.Add(new Point((int)Math.Round(point.X), (int)Math.Round(point.Y)));
                }
                else
                {
                    outlierPoints.Add(new Point((int)Math.Round(point.X), (int)Math.Round(point.Y)));
                }
            }
            var bestLine = new LineSegment2D(new Point((int)Math.Round((edgePoints[0].Y - b) / a), edgePoints[0].Y), new Point((int)Math.Round((edgePoints[edgePoints.Count - 1].Y - b) / a), edgePoints[edgePoints.Count - 1].Y));
            return bestLine;
        }
        private double DistancePointToLine(PointF point, LineSegment2D line)
        {
            double a = line.P1.Y - line.P2.Y;
            double b = line.P2.X - line.P1.X;
            double c = line.P1.X * line.P2.Y - line.P2.X * line.P1.Y;

            double numerator = Math.Abs(a * point.X + b * point.Y + c);
            double denominator = Math.Sqrt(a * a + b * b);
            double distace = numerator / denominator;
            return distace;
        }
        private bool GetImageResult()
        {
            if (_inputImage == null)
                return false;
            var imgShow = _inputImage.Convert<Bgr, byte>();
            if (_passed)
            {
                CvInvoke.ArrowedLine(imgShow, _lineSegment.Item1, _lineSegment.Item2, new MCvScalar(0, 255, 0), 1);
            }
            else
            {
                CvInvoke.ArrowedLine(imgShow, _lineSegment.Item1, _lineSegment.Item2, new MCvScalar(255, 0, 255), 1);
            }
            _outputImageShow = imgShow.ToBitmap();
            imgShow.Dispose();
            return true;
        }
        public bool GetImageSetting()
        {
            if (_inputImage == null)
                return false;
            var imgShow = _inputImage.Convert<Bgr, byte>();
            for (int i = 0; i < _edgePoints.Count; i++)
            {
                CvInvoke.Circle(imgShow, _edgePoints[i], 1, new MCvScalar(0, 255, 0), -1);
            }
            for (int i = 0; i < _outlierPoints.Count; i++)
            {
                CvInvoke.Circle(imgShow, _outlierPoints[i], 1, new MCvScalar(0, 0, 255), -1);
            }
            _outputImageShow = imgShow.ToBitmap();
            imgShow.Dispose();
            return true;
        }
    }
}
