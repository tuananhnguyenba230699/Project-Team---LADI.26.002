using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;

namespace DistanceMeasurementTool
{
    public class Measurement
    {
        /// <summary>
        /// INPUT
        /// </summary>
        public Bitmap InputImage
        {
            set { _inputImage = value; }
        }
        public List<Point> Point
        {
            set { _point = value; }
        }
        public List<Point> PointTwo
        {
            set { _pointTwo = value; }
        }
        public Tuple<Point, Point> Line
        {
            set { _line = value; }
        }
        public Tuple<Point, Point> LineTwo
        {
            set { _lineTwo = value; }
        }
        public string DistanceType
        {
            set { _distanceType = value; }
        }
        public string MeasurementType
        {
            set { _measurementType = value; }
        }
        public string AngleDirection
        {
            set { _angleDirection = value; }
        }
        public string AngleEndPoint
        {
            set { _angleEndPoint = value; }
        }
        private Tuple<Point, Point> _lineSegment = null;
        public Tuple<Point, Point> LineSegment
        {
            get { return _lineSegment; }
        }
        public Tuple<double, double, double> Tolerance
        {
            set { _tolerance = value; }
        }
        public bool IsShowImageResult
        {
            set { _isShowImageResult = value; }
        }

        /// <summary>
        /// OUTPUT
        /// </summary>
        public List<double> Distance
        {
            get { return _distance; }
        }
        public double DistanceMin
        {
            get { return _distanceMin; }
        }
        public double AngleActual
        {
            get { return _angleActual; }
        }
        public Bitmap OutputImage
        {
            get { return _outputImage; }
        }
        public bool Passed
        {
            get { return _passed; }
        }
        public string ErrMessage
        {
            get { return _errMessage; }
        }

        private Bitmap _inputImage = null;
        private List<Point> _point = null;
        private List<Point> _pointTwo = null;
        private Tuple<Point, Point> _line = null;
        private Tuple<Point, Point> _lineTwo = null;
        private string _distanceType = null;
        private string _measurementType = null;
        private string _angleDirection = null;
        private string _angleEndPoint = null;
        private Tuple<double, double, double> _tolerance = null;
        private bool _isShowImageResult = false;


        private List<double> _distance = null;
        private double _distanceMin = 0;
        private double _angleActual = 0;
        private Bitmap _outputImage;
        private bool _passed = false;
        private string _errMessage;


        public bool Run()
        {
            _distance = new List<double>();
            _distanceMin = 0;
            _passed = false;
            if (_distanceType == null)
            {
                _errMessage = "Run: Lỗi chưa set DistanceType";
                return false;
            }
            try
            {
                switch (_distanceType)
                {
                    case RTCConst.cDistanceMeasurementTool.DistanceType_PointToPoint:
                        {
                            if (_inputImage == null)
                            {
                                _errMessage = "Run: Lỗi chưa set InputImage";
                                return false;
                            }
                            if (_point == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Point";
                                return false;
                            }
                            if (_pointTwo == null)
                            {
                                _errMessage = "Run: Lỗi chưa set PointTwo";
                                return false;
                            }
                            double dis = DistancePointToPoint(_point[0], _pointTwo[0]);
                            _distance.Add(dis);
                            _distanceMin = dis;
                            if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                            {
                                _passed = true;
                            }
                            if (_isShowImageResult)
                            {
                                var imageShow = _inputImage.ToImage<Bgr, byte>();
                                if (_passed)
                                {
                                    CvInvoke.ArrowedLine(imageShow, _point[0], _pointTwo[0], new MCvScalar(0, 255, 0), 2);
                                }
                                else
                                {
                                    CvInvoke.ArrowedLine(imageShow, _point[0], _pointTwo[0], new MCvScalar(0, 0, 255), 2);
                                }
                                _outputImage = imageShow.ToBitmap();
                            }

                            break;
                        }
                    case RTCConst.cDistanceMeasurementTool.DistanceType_PointToListPoint:
                        {
                            if (_inputImage == null)
                            {
                                _errMessage = "Run: Lỗi chưa set InputImage";
                                return false;
                            }
                            if (_point == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Point";
                                return false;
                            }
                            if (_pointTwo == null)
                            {
                                _errMessage = "Run: Lỗi chưa set PointTwo";
                                return false;
                            }
                            _passed = true;
                            if (_isShowImageResult)
                            {
                                var imageShow = _inputImage.ToImage<Bgr, byte>();
                                for (int i = 0; i < _pointTwo.Count; i++)
                                {
                                    double dis = DistancePointToPoint(_point[i], _pointTwo[i]);
                                    _distance.Add(dis);
                                    if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                    {
                                        CvInvoke.ArrowedLine(imageShow, _point[0], _pointTwo[0], new MCvScalar(0, 255, 0), (int)Math.Round((double)_inputImage.Width / 150));
                                    }
                                    else
                                    {
                                        CvInvoke.ArrowedLine(imageShow, _point[0], _pointTwo[0], new MCvScalar(0, 0, 255), (int)Math.Round((double)_inputImage.Width / 150));
                                        _passed = false;
                                    }
                                }
                                _outputImage = imageShow.ToBitmap();
                            }

                            _distanceMin = _distance.Min();
                            break;
                        }
                    case RTCConst.cDistanceMeasurementTool.DistanceType_PointToLine:
                        {
                            if (_point == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Point";
                                return false;
                            }
                            if (_line == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Line";
                                return false;
                            }
                            double dis = DistancePointToLine(_point[0], _line, out Point endPoint);
                            _distance.Add(dis);
                            _distanceMin = dis;
                            if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                            {
                                _passed = true;
                            }
                            if (_isShowImageResult)
                            {
                                var imageShow = _inputImage.ToImage<Bgr, byte>();
                                if (_passed)
                                {
                                    CvInvoke.ArrowedLine(imageShow, _point[0], endPoint, new MCvScalar(0, 255, 0), (int)Math.Round((double)_inputImage.Width / 150));
                                }
                                else
                                {
                                    CvInvoke.ArrowedLine(imageShow, _point[0], endPoint, new MCvScalar(0, 0, 255), (int)Math.Round((double)_inputImage.Width / 150));
                                }
                                _outputImage = imageShow.ToBitmap();
                            }

                            break;
                        }
                    case RTCConst.cDistanceMeasurementTool.DistanceType_LineToLine:
                        {
                            if (_line == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Line";
                                return false;
                            }
                            if (_lineTwo == null)
                            {
                                _errMessage = "Run: Lỗi chưa set LineTwo";
                                return false;
                            }
                            if (_measurementType == null)
                            {
                                _errMessage = "Run: Lỗi chưa set MeasurementType";
                                return false;
                            }
                            switch (_measurementType)
                            {
                                case RTCConst.cDistanceMeasurementTool.MeasurementType_Euclidean:
                                    {
                                        var mid_Line1 = new Point((int)Math.Round((double)(_line.Item1.X + _line.Item2.X) / 2), (int)Math.Round((double)(_line.Item1.Y + _line.Item2.Y) / 2));
                                        double dis = DistancePointToLine(mid_Line1, _lineTwo, out Point endPoint);
                                        _lineSegment = new Tuple<Point, Point>(new Point(mid_Line1.X, mid_Line1.Y), new Point(endPoint.X, endPoint.Y));
                                        _distance.Add(dis);
                                        _distanceMin = dis;
                                        if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                        {
                                            _passed = true;
                                        }
                                        if (_isShowImageResult)
                                        {
                                            var imageShow = _inputImage.ToImage<Bgr, byte>();
                                            if (_passed)
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, endPoint, new MCvScalar(0, 255, 0), (int)Math.Round((double)_inputImage.Width / 150));
                                            }
                                            else
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, endPoint, new MCvScalar(0, 0, 255), 2);
                                            }
                                            _outputImage = imageShow.ToBitmap();
                                        }

                                        break;
                                    }
                                case RTCConst.cDistanceMeasurementTool.MeasurementType_MidPointToLine:
                                    {
                                        var mid_Line1 = new Point((int)Math.Round((double)(_line.Item1.X + _line.Item2.X) / 2), (int)Math.Round((double)(_line.Item1.Y + _line.Item2.Y) / 2));

                                        double dis = DistancePointToLine(mid_Line1, _lineTwo, out Point endPoint);
                                        _lineSegment = new Tuple<Point, Point>(new Point(mid_Line1.X, mid_Line1.Y), new Point(endPoint.X, endPoint.Y));

                                        _distance.Add(dis);
                                        _distanceMin = dis;
                                        if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                        {
                                            _passed = true;
                                        }
                                        if (_isShowImageResult)
                                        {
                                            var imageShow = _inputImage.ToImage<Bgr, byte>();
                                            if (_passed)
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, endPoint, new MCvScalar(0, 255, 0), 2);
                                            }
                                            else
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, endPoint, new MCvScalar(0, 0, 255), 2);
                                            }
                                            _outputImage = imageShow.ToBitmap();
                                        }

                                        break;
                                    }
                                case RTCConst.cDistanceMeasurementTool.MeasurementType_MidPointToMidPoint:
                                    {
                                        var mid_Line1 = new Point((int)Math.Round((double)(_line.Item1.X + _line.Item2.X) / 2), (int)Math.Round((double)(_line.Item1.Y + _line.Item2.Y) / 2));
                                        var mid_Line2 = new Point((int)Math.Round((double)(_lineTwo.Item1.X + _lineTwo.Item2.X) / 2), (int)Math.Round((double)(_lineTwo.Item1.Y + _lineTwo.Item2.Y) / 2));
                                        _lineSegment = new Tuple<Point, Point>(new Point(mid_Line1.X, mid_Line1.Y), new Point(mid_Line2.X, mid_Line2.Y));

                                        double dis = DistancePointToPoint(mid_Line1, mid_Line2);
                                        _distance.Add(dis);
                                        _distanceMin = dis;
                                        if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                        {
                                            _passed = true;
                                        }
                                        if (_isShowImageResult)
                                        {
                                            var imageShow = _inputImage.ToImage<Bgr, byte>();
                                            if (_passed)
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, mid_Line2, new MCvScalar(0, 255, 0), 2);
                                            }
                                            else
                                            {
                                                CvInvoke.ArrowedLine(imageShow, mid_Line1, mid_Line2, new MCvScalar(0, 0, 255), 2);
                                            }
                                            _outputImage = imageShow.ToBitmap();
                                        }

                                        break;
                                    }
                            }
                            break;
                        }
                    case RTCConst.cDistanceMeasurementTool.DistanceType_AngleOf2Lines:
                        {
                            if (_line == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Line";
                                return false;
                            }
                            if (_lineTwo == null)
                            {
                                _errMessage = "Run: Lỗi chưa set LineTwo";
                                return false;
                            }
                            if (_angleDirection == null)
                            {
                                _errMessage = "Run: Lỗi chưa set AngleDirection";
                                return false;
                            }
                            if (_angleEndPoint == null)
                            {
                                _errMessage = "Run: Lỗi chưa set AngleEndPoint";
                                return false;
                            }
                            switch (_angleDirection)
                            {
                                case RTCConst.cDistanceMeasurementTool.AngleDirection_CWFromLine1ToLine2:
                                    {
                                        CaculateAngle(_line, _lineTwo, out Tuple<double, double, double, double> outAngle);
                                        switch (_angleEndPoint)
                                        {
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_ShortestAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item1;
                                                    break;
                                                }
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_FullAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item3;
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case RTCConst.cDistanceMeasurementTool.AngleDirection_CCWFromLine1ToLine2:
                                    {
                                        CaculateAngle(_line, _lineTwo, out Tuple<double, double, double, double> outAngle);
                                        switch (_angleEndPoint)
                                        {
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_ShortestAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item2;
                                                    break;
                                                }
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_FullAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item4;
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case RTCConst.cDistanceMeasurementTool.AngleDirection_CWFromLine2ToLine1:
                                    {
                                        CaculateAngle(_line, _lineTwo, out Tuple<double, double, double, double> outAngle);
                                        switch (_angleEndPoint)
                                        {
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_ShortestAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item1;
                                                    break;
                                                }
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_FullAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item3;
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case RTCConst.cDistanceMeasurementTool.AngleDirection_CCWFromLine2ToLine1:
                                    {
                                        CaculateAngle(_line, _lineTwo, out Tuple<double, double, double, double> outAngle);
                                        switch (_angleEndPoint)
                                        {
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_ShortestAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item2;
                                                    break;
                                                }
                                            case RTCConst.cDistanceMeasurementTool.AngleEndPoint_FullAngleToEndingLine:
                                                {
                                                    _angleActual = outAngle.Item4;
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case RTCConst.cDistanceMeasurementTool.DistanceType_ListPointToLine:
                        {
                            if (_point == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Point";
                                return false;
                            }
                            if (_line == null)
                            {
                                _errMessage = "Run: Lỗi chưa set Line";
                                return false;
                            }
                            _passed = true;
                            if (_isShowImageResult)
                            {
                                var imageShow = _inputImage.ToImage<Bgr, byte>();
                                for (int i = 0; i < _point.Count; i++)
                                {
                                    double dis = DistancePointToLine(_point[i], _line, out Point endPoint);
                                    _distance.Add(dis);
                                    if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                    {
                                        CvInvoke.ArrowedLine(imageShow, _point[i], endPoint, new MCvScalar(0, 255, 0), 2);
                                    }
                                    else
                                    {
                                        CvInvoke.ArrowedLine(imageShow, _point[i], endPoint, new MCvScalar(0, 0, 255), 2);
                                        _passed = false;
                                    }
                                    _outputImage = imageShow.ToBitmap();
                                }
                            }
                            else
                            {
                                for (int i = 0; i < _point.Count; i++)
                                {
                                    double dis = DistancePointToLine(_point[i], _line, out Point endPoint);
                                    _distance.Add(dis);
                                    if (dis >= _tolerance.Item2 - _tolerance.Item1 && dis <= _tolerance.Item2 + _tolerance.Item3)
                                    { }
                                    else
                                    {
                                        _passed = false;
                                    }
                                }
                            }
                            _distanceMin = _distance.Min();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                _errMessage = ex.Message + "\n" + ex.StackTrace;
                return false;
            }
            return true;
        }
        private double DistancePointToPoint(Point point1, Point point2)
        {
            double dis = 0;
            dis = Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
            return dis;
        }
        private double DistancePointToLine(Point point, Tuple<Point, Point> LineSegment, out Point endPoint)
        {
            endPoint = new Point();
            double dis = 0;
            //Thiết lập pt đường thẳng ax + by + c =0
            double a = LineSegment.Item2.Y - LineSegment.Item1.Y;
            double b = LineSegment.Item1.X - LineSegment.Item2.X;
            double c = -b * LineSegment.Item1.Y - a * LineSegment.Item1.X;
            //Tính khoảng cách từ point tới đường thẳng
            dis = (double)Math.Abs(a * point.X + point.Y * b + c) / Math.Sqrt(a * a + b * b);
            endPoint.X = (int)Math.Round((double)(1 / a * point.X + point.Y - b) / (a + 1 / a));
            endPoint.Y = (int)(a * endPoint.X + b);
            return dis;
        }
        private double AngleBetweenVectorAndLine(Tuple<Point, Point> LineSegment)
        {
            PointF vecLine = new PointF(LineSegment.Item2.X - LineSegment.Item1.X, LineSegment.Item2.Y - LineSegment.Item1.Y);
            double cosAngle = (double)vecLine.X / Math.Sqrt(Math.Pow(vecLine.X, 2) + Math.Pow(vecLine.Y, 2));
            double angle = Math.Acos(cosAngle);
            if (LineSegment.Item2.Y > LineSegment.Item1.Y)
            {
                angle = 2 * Math.PI - angle;
            }
            return angle;

        }
        private void CaculateAngle(Tuple<Point, Point> lineSegment1, Tuple<Point, Point> lineSegment2, out Tuple<double, double, double, double> angleOut)
        {
            double CWS = 0;//CWS - CW + shortest
            double CCWS = 0;//CCWS - CCW + shortest
            double CWF = 0; //CWF - CW + full
            double CCWF = 0; //CCWF - CCW + full
            var angleLine1 = AngleBetweenVectorAndLine(lineSegment1);
            var angleLine2 = AngleBetweenVectorAndLine(lineSegment2);
            if (angleLine2 >= angleLine1 && angleLine2 - angleLine1 <= 180)
            {
                CCWS = angleLine2 - angleLine1;
                CWS = Math.PI - CCWS;
                CCWF = CCWS;
                CWF = 2 * Math.PI - CCWF;
            }
            if (angleLine2 >= angleLine1 && angleLine2 - angleLine1 > 180)
            {
                CWS = 2 * Math.PI - angleLine2 + angleLine1;
                CCWS = Math.PI - CWS;
                CWF = CWS;
                CCWF = 2 * Math.PI - CWF;
            }
            if (angleLine2 <= angleLine1 && angleLine1 - angleLine2 <= 180)
            {
                CWS = angleLine1 - angleLine2;
                CCWS = Math.PI - CWS;
                CWF = CWS;
                CCWF = 2 * Math.PI - CWF;
            }
            if (angleLine2 <= angleLine1 && angleLine1 - angleLine2 > 180)
            {
                CWS = angleLine1 - angleLine2 - Math.PI;
                CCWS = Math.PI - CWS;
                CCWF = CCWS;
                CWF = 2 * Math.PI - CCWF;
            }

            angleOut = Tuple.Create(CWS, CCWS, CWF, CCWF);
        }
    }
}
