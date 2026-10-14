using Basler.Pylon;
using Emgu.CV;
using Emgu.CV.Structure;
using RTCBaslerSdk;
using RTCConst;
using RTCHikSdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoBrutingDiamond
{
    class GlobFuncs
    {
        internal static void DisposeImage(Image Image, bool genEmptyObj = false)
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
        }
        /// <summary>
        /// Set ảnh chi wun
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="Window"></param>
        public static void SmartSetPart(Image Image, GraphicsWindow.GraphicsWindow Window)
        {

            if (Image == null)
                return;
            int imgRow;
            Window.Image = Image;

        }
        /// <summary>
        /// Hàm chuyển đổi sang ảnh Gray của thư viện Emgu
        /// </summary>
        /// <param name="bitmapImage">Ảnh bitmap đầu vào</param>
        /// <returns></returns>
        public static Image<Gray, byte> BitmapToGrayImage(Bitmap bitmapImage)
        {

            // Chuyển đổi Bitmap sang Image<Bgr, byte>
            //Image<Gray, byte> outputImage = bitmapImage.ToImage<Gray, byte>();
            //return outputImage;
            using (Bitmap tempBitmap = new Bitmap(bitmapImage))
            {
                return tempBitmap.ToImage<Gray, byte>();
            }

        }
        public static int ToInt(object x)
        {
            try
            {
                return Convert.ToInt32(x);
            }
            catch
            {
                return 0;
            }
        }
        public static Dictionary<string, ICameraInfo> GetBaslerCameraInfos()
        {
            Dictionary<string, ICameraInfo> foundCameraInfos = null;
            try
            {
                // Ask the camera finder for a list of cameras.
                List<ICameraInfo> cameraInfos = CameraFinder.Enumerate();

                if (cameraInfos.Count > 0)
                {
                    // Maps the camera name to the camera info for use with the combo box.
                    foundCameraInfos = new Dictionary<string, ICameraInfo>();
                    foreach (ICameraInfo cameraInfo in cameraInfos)
                    {
                        if (!foundCameraInfos.ContainsKey(cameraInfo[CameraInfoKey.FriendlyName]))
                            foundCameraInfos.Add(cameraInfo[CameraInfoKey.FriendlyName], cameraInfo);
                    }
                }
            }
            catch
            {
                foundCameraInfos = null;
            }
            return foundCameraInfos;
        }
        public static bool Basler_IsTriggerOn(MyBaslerCamera baslerCamera)
        {
            if (baslerCamera == null || !baslerCamera.IsOpen)
                return false;
            IEnumParameter enumParameter = baslerCamera.Parameters[new EnumName(cParamName.TriggerMode)];
            if (enumParameter != null)
                return enumParameter.GetValue() == Basler.Pylon.PLCamera.TriggerMode.On;
            else
                return false;
        }
        public static MyHikCamera.MV_CC_DEVICE_INFO GetHikrobotDeviceInfo(string deviceName, out bool isExists)
        {
            isExists = false;
            System.GC.Collect();
            MyHikCamera.MV_CC_DEVICE_INFO_LIST mStDeviceList = new MyHikCamera.MV_CC_DEVICE_INFO_LIST();
            int nRet = MyHikCamera.MV_CC_EnumDevices_NET(MyHikCamera.MV_GIGE_DEVICE | MyHikCamera.MV_USB_DEVICE, ref mStDeviceList);
            if (0 != nRet)
                return new MyHikCamera.MV_CC_DEVICE_INFO();
            isExists = true;
            for (var i = 0; i < mStDeviceList.nDeviceNum; i++)
            {
                MyHikCamera.MV_CC_DEVICE_INFO device =
                    (MyHikCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(mStDeviceList.pDeviceInfo[i],
                    typeof(MyHikCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyHikCamera.MV_GIGE_DEVICE)
                {
                    MyHikCamera.MV_GIGE_DEVICE_INFO gigeInfo =
                        (MyHikCamera.MV_GIGE_DEVICE_INFO)MyHikCamera.ByteToStruct(device.SpecialInfo.stGigEInfo,
                        typeof(MyHikCamera.MV_GIGE_DEVICE_INFO));

                    if (gigeInfo.chUserDefinedName != "" &&
                        "GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")" == deviceName)
                        return device;
                    else if (gigeInfo.chUserDefinedName == "" &&
                         "GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName +
                         " (" + gigeInfo.chSerialNumber + ")" == deviceName)
                        return device;
                }
                else if (device.nTLayerType == MyHikCamera.MV_USB_DEVICE)
                {
                    MyHikCamera.MV_USB3_DEVICE_INFO usbInfo =
                        (MyHikCamera.MV_USB3_DEVICE_INFO)MyHikCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo,
                        typeof(MyHikCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "" &&
                        "U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")" == deviceName)
                        return device;
                    else if (usbInfo.chUserDefinedName == "" &&
                        "U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")" == deviceName)
                        return device;
                }

            }
            return new MyHikCamera.MV_CC_DEVICE_INFO();
        }
        public static string Hik_GetErrorMessage(int nErrorNum)
        {
            string errorMsg = string.Empty;
            if (nErrorNum != 0)
            {
                errorMsg = "Error =" + string.Format("{0:X}", nErrorNum);
                switch (nErrorNum)
                {
                    case MyHikCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                    case MyHikCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                    case MyHikCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                    case MyHikCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                    case MyHikCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                    case MyHikCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                    case MyHikCamera.MV_E_NODATA: errorMsg += " No data "; break;
                    case MyHikCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                    case MyHikCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                    case MyHikCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                    case MyHikCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                    case MyHikCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                    case MyHikCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                    case MyHikCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                    case MyHikCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                    case MyHikCamera.MV_E_NETER: errorMsg += " Network error "; break;
                    case MyHikCamera.MV_E_ABNORMAL_IMAGE: errorMsg += " Abnormal image, maybe incomplete image because of lost packet "; break;
                    case MyHikCamera.MV_E_ENCRYPT: errorMsg += " Encryption error "; break;
                    case MyHikCamera.MV_E_GC_ARGUMENT: errorMsg += " Illegal parameters "; break;
                    case MyHikCamera.MV_E_GC_DYNAMICCAST: errorMsg += " Transformation exception "; break;
                    case MyHikCamera.MV_E_GC_LOGICAL: errorMsg += " Logical error "; break;
                    case MyHikCamera.MV_E_GC_PROPERTY: errorMsg += " Property "; break;
                    case MyHikCamera.MV_E_GC_RANGE: errorMsg += " The value is out of range "; break;
                    case MyHikCamera.MV_E_GC_RUNTIME: errorMsg += " Running environment error "; break;
                    case MyHikCamera.MV_E_GC_TIMEOUT: errorMsg += " Timeout "; break;
                    case MyHikCamera.MV_E_GC_UNKNOW: errorMsg += " GenICam unknown error "; break;
                    case MyHikCamera.MV_E_UPG_UNKNOW: errorMsg += " Unknown error during upgrade "; break;
                    case MyHikCamera.MV_E_UPG_INNER_ERR: errorMsg += " Camera internal error during upgrade "; break;
                    case MyHikCamera.MV_E_UPG_CONFLICT: errorMsg += " Upgrading conflicted (repeated upgrading requests during device upgrade) "; break;
                    case MyHikCamera.MV_E_UPG_LANGUSGE_MISMATCH: errorMsg += " Firmware language mismatches "; break;
                    case MyHikCamera.MV_E_UPG_FILE_MISMATCH: errorMsg += " Firmware mismatches "; break;
                    case MyHikCamera.MV_E_USB_UNKNOW: errorMsg += " USB unknown error "; break;
                    case MyHikCamera.MV_E_USB_DRIVER: errorMsg += " Driver mismatch or unmounted drive "; break;
                    case MyHikCamera.MV_E_USB_BANDWIDTH: errorMsg += " Insufficient bandwidth, this error code is newly added "; break;
                    case MyHikCamera.MV_E_USB_GENICAM: errorMsg += " GenICam error "; break;
                    case MyHikCamera.MV_E_USB_DEVICE: errorMsg += " Device exception "; break;
                    case MyHikCamera.MV_E_USB_WRITE: errorMsg += " Writing USB error "; break;
                    case MyHikCamera.MV_E_USB_READ: errorMsg += " Reading USB error "; break;
                    case MyHikCamera.MV_E_IP_CONFLICT: errorMsg += " Device IP conflict "; break;
                    case MyHikCamera.MV_E_PACKET: errorMsg += " Network data packet error "; break;
                    case MyHikCamera.MV_E_WRITE_PROTECT: errorMsg += " The target address is not writable "; break;
                    case MyHikCamera.MV_E_INVALID_ADDRESS: errorMsg += " The target address being accessed does not exist "; break;
                    case MyHikCamera.MV_E_NOT_IMPLEMENTED: errorMsg += " The command is not supported by device "; break;
                    case MyHikCamera.MV_E_LOAD_LIBRARY: errorMsg += " Load library failed "; break;
                }
            }
            return errorMsg;
        }
        public static string SwitchToUnsigned(string _Text)
        {
            string[] CoDau = new[]
            {
                "aàáảãạăằắẳẵặâầấẩẫậ", "AÀÁÃẢẠĂẰẮẴẲẶÂẦẤẪẨẬ", "đ", "Đ", "eèéẽẻẹêềếễểệ", "EÈÉẼẺẸÊỀẾỄỂỆ", "iìíĩỉị",
                "IÌÍĨỈỊ", "oòóõỏọôồốỗổộơờớỡởợ", "OÒÓÕỎỌÔỒỐỖỔỘƠỜỚỠỞỢ", "uùúũủụưừứữửự", "UÙÚŨỦỤƯỪỨỮỬỰ", "yỳýỹỷỵ", "YỲÝỸỶỴ"
            };
            string[] KoDau = new[] { "a", "A", "d", "D", "e", "E", "i", "I", "o", "O", "u", "U", "y", "Y" };
            string str = _Text;
            string strReturn = "";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                string iStr = str.Substring(i, 1);
                string rStr = iStr;
                for (int j = 0; j <= CoDau.Length - 1; j++)
                {
                    if (CoDau[j].IndexOf(iStr) >= 0)
                    {
                        rStr = KoDau[j];
                        break;
                    }
                }
                strReturn += rStr;
            }
            return strReturn;
        }

    }
}
