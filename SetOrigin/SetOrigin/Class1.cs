using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCBase.Drawing;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Shape;

namespace SetOrigin
{
    public class SetOrigin
    {
        public List<PointF[]> Run (List<RTCRectangle> _ROI,Tuple<PointF,double> _toolOrigin)
        {
            List<PointF[]> _outSetOrigin = new List<PointF[]>();
            if (_toolOrigin == null)
            {
                return _outSetOrigin;
            }

            if (_ROI == null)
            {
                _outSetOrigin = null;
            }
            else
            {
                for (int i = 0; i < _ROI.Count; i++)
                {
                    PointF[] pROI = new PointF[4];

                    _ROI[i].Center.X = _ROI[i].Center.X;
                    _ROI[i].Center.Y = _ROI[i].Center.Y;
                    //_ROI[i].Phi = -_ROI[i].Phi;

                    //Hàm GetRectangleVertices() lấy tọa độ 4 đỉnh của rectangle
                    GetRectangleVertices(_ROI[i], out PointF[] inPointROI);

                    //Chuyển lần lượt tọa độ 4 đỉnh ROI trong hệ tọa độ ảnh về hệ tọa độ ToolOrigin
                    for (int j = 0; j < 4; j++)
                    {
                        pROI[j] = ConvertCoordinatesToNewOrigin(_toolOrigin, inPointROI[j]);
                    }
                    _outSetOrigin.Add(pROI);
                }
            }

            return _outSetOrigin;
        }
        public PointF[] Run(RTCRectangle _ROI, Tuple<PointF, double> _toolOrigin)
        {
            PointF[] _outSetOrigin = new PointF[4];
            if (_toolOrigin == null)
            {
                return _outSetOrigin;
            }

            if (_ROI == null)
            {
                _outSetOrigin = null;
            }
            else
            {
                //Hàm GetRectangleVertices() lấy tọa độ 4 đỉnh của rectangle
                _ROI.Width = _ROI.Width;
                _ROI.Height = _ROI.Height;
                GetRectangleVertices(_ROI, out PointF[] inPointROI);
                //_errMessage = inPointROI[2].ToString();
                //Chuyển lần lượt tọa độ 4 đỉnh ROI trong hệ tọa độ ảnh về hệ tọa độ ToolOrigin
                for (int i = 0; i < 4; i++)
                {
                    _outSetOrigin[i] = ConvertCoordinatesToNewOrigin(_toolOrigin, inPointROI[i]);
                }
            }
            return _outSetOrigin;
        }

        private void GetRectangleVertices(RTCRectangle recROI, out PointF[] pointRec)
        {
            var halfwidth = recROI.Width;
            var halfheight = recROI.Height;
            var angle = recROI.Phi;
            var centerPoint = recROI.Center;
            double diag = Math.Sqrt(halfwidth * halfwidth + halfheight * halfheight);
            PointF vertex1 = new PointF((float)Convert.ToSingle(centerPoint.X + diag / 2 * Math.Cos(angle + Math.Atan(halfheight / halfwidth))), (float)Convert.ToSingle(centerPoint.Y - diag / 2 * Math.Sin(angle + Math.Atan(halfheight / halfwidth))));
            PointF vertex2 = new PointF((float)Convert.ToSingle(centerPoint.X - diag / 2 * Math.Cos(angle - Math.Atan(halfheight / halfwidth))), (float)Convert.ToSingle(centerPoint.Y + diag / 2 * Math.Sin(angle - Math.Atan(halfheight / halfwidth))));
            PointF vertex3 = new PointF((float)Convert.ToSingle(centerPoint.X - diag / 2 * Math.Cos(angle + Math.Atan(halfheight / halfwidth))), (float)Convert.ToSingle(centerPoint.Y + diag / 2 * Math.Sin(angle + Math.Atan(halfheight / halfwidth))));
            PointF vertex4 = new PointF((float)Convert.ToSingle(centerPoint.X + diag / 2 * Math.Cos(angle - Math.Atan(halfheight / halfwidth))), (float)Convert.ToSingle(centerPoint.Y - diag / 2 * Math.Sin(angle - Math.Atan(halfheight / halfwidth))));
            pointRec = new PointF[] { vertex1, vertex2, vertex3, vertex4 };
        }
        private PointF ConvertCoordinatesToNewOrigin(Tuple<PointF, double> toolOrigin, PointF point)
        {
            PointF p = new PointF();
            var Angle = toolOrigin.Item2 * Math.PI / 180;
            var A = new Matrix<double>(new double[,] {
                { Math.Cos(Angle),-Math.Sin(Angle),toolOrigin.Item1.X },
                { Math.Sin(Angle),Math.Cos(Angle),toolOrigin.Item1.Y },
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
    }
}
