using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond.Class
{
    public enum EDrawingtypes
    {
        Rectang = 1,
        Ellipse = 2,
        Line = 3,
        Polygon = 4,
        Rectang1 = 5,
        none = 99
    }
    public enum ERoiTypes
    {
        Normal = 0,
        Mark = 1,
        Master = 2
    }
    public enum EConnectTypes
    {
        Different = 0,
        Union = 1,
        Intersection = 2,
        Reference = 3,
        None = 99
    }
    public enum EAnimationTypes
    {
        e2D = 0,
        e3D = 1
    }
    public enum EDrawingModes
    {
        fill = 0,
        Margin = 1,
        None = 99
    }
    /// 
    public enum EVRI_SortMode
    {
        None = 0,
        ByRow = 1,
        ByCol = 2
    }
    public enum EVRI_StartPosition
    {
        Left = 0,
        Right = 1,
        Center = 2,
        Top = 3,
        Bottom = 4
    }
}
