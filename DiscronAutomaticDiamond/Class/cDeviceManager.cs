using AutoBrutingDiamond.Models;
using MC_Protocol_RTCTechnology;
using RTCHikSdk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AutoBrutingDiamond.Class
{
    public class cDeviceManager
    {
        public static MySQLLib.MySQLHelper MySQLHelper = new MySQLLib.MySQLHelper();

        #region Khai báo
        public static MC_Protocol _plcMC;
        public static register_teachings_plc _teachings_plc;
        public static MyHikCamera HikCamera = null;
        #endregion

        #region INFORMARION CAMERA
        public static bool ConnectCamera_Hikrobot(string deviceName, ref string errMessage)
        {
            errMessage = string.Empty;
            var result = false;

            try
            {
                HikCamera = new MyHikCamera();
                MyHikCamera.MV_CC_DEVICE_INFO device = GlobFuncs.GetHikrobotDeviceInfo(deviceName, out bool isExists);
                var nRet = HikCamera.MV_CC_CreateDevice_NET(ref device);
                if (MyHikCamera.MV_OK != nRet)
                {
                    return false;
                }
                nRet = HikCamera.MV_CC_OpenDevice_NET();
                if (MyHikCamera.MV_OK != nRet)
                {
                    HikCamera.MV_CC_DestroyDevice_NET();
                    errMessage = GlobFuncs.Hik_GetErrorMessage(nRet);
                    return false;
                }
                if (device.nTLayerType == MyHikCamera.MV_GIGE_DEVICE)
                {
                    var nPacketSize = HikCamera.MV_CC_GetOptimalPacketSize_NET();
                    if (nPacketSize > 0)
                    {
                        nRet = HikCamera.MV_CC_SetIntValueEx_NET(
                            "GevSCPSPacketSize", nPacketSize);
                        if (nRet != MyHikCamera.MV_OK)
                        {
                            errMessage = GlobFuncs.Hik_GetErrorMessage(nRet);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (HikCamera != null)
                {

                    nRet = HikCamera.MV_CC_StartGrabbing_NET();
                    if (MyHikCamera.MV_OK != nRet)
                    {
                        //ErrMessage = GlobFuncs.Hik_GetErrorMessage(nRet);
                        return false;
                    }
                    //DeviceName = deviceName;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion

        #region INFROMATION PLC
        public static void LoadPlcTeachingAddress()
        {
            _teachings_plc = MySQLHelper.FindAll<register_teachings_plc>().FirstOrDefault();
            _teachings_plc = _teachings_plc == null ? new register_teachings_plc() : _teachings_plc;
        }
        public static bool Connect_PLC()
        {
            bool IsConnected = false;
            //connect PLC
            if (_plcMC == null)
                _plcMC = new MC_Protocol(true);
            else
                _plcMC.Disconnect();
            try
            {
                register_device plcDevice = MySQLHelper.FindByField<register_device>("Type", 2);
                if(plcDevice != null)
                {
                    string ipAddress = plcDevice.IpAddress.ToString();
                    int port = Lib.ToInt(plcDevice.Port.ToString());
                    //_plcMC.IP_Adress = "127.0.0.1";
                    _plcMC.IP_Adress = ipAddress;
                    _plcMC.Port = port;
                    _plcMC.Connect();
                    IsConnected = _plcMC.Connected;
                }
            }
            catch (Exception ex)
            {
            }
            return IsConnected;
        }

        public static void Disconnect_PLC()
        {
            try
            {
                //connect PLC
                if (_plcMC != null)
                    _plcMC.Disconnect();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region SET BIT ON/OFF - PLC
        /// <summary>
        /// Hàm để set ON các bit cụ thể (đặt bit thành 1)
        /// </summary>
        /// <param name="deviceName"> địa chỉ thanh ghi</param>
        /// <param name="bitPositions"> danh sách vị trí cần on</param>
        public static void SetBitsOn(string deviceName)
        {
            try
            {
                if (_plcMC == null || !_plcMC.Connected) return;
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    // Đọc giá trị hiện tại của thanh ghi
                    int currentValue;
                    _plcMC.GetSingleRegister_Int16(split[0], out currentValue);

                    // Bật các bit cụ thể
                    foreach (int bitPosition in split[1])
                    {
                        currentValue |= (1 << Lib.ToInt(split[1])); // Đặt bit thành 1
                    }

                    // Ghi lại giá trị vào thanh ghi
                    _plcMC.SetSingleRegister_Int16(split[0], currentValue);
                }
                else
                    _plcMC.SetSingleBit(deviceName, true);
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
        public static void SetBitsOff(string deviceName)
        {
            try
            {
                if (_plcMC == null || !_plcMC.Connected) return;
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    // Đọc giá trị hiện tại của thanh ghi
                    int currentValue;
                    _plcMC.GetSingleRegister_Int16(split[0], out currentValue);
                    // Tắt các bit cụ thể
                    foreach (int bitPosition in split[1])
                    {
                        currentValue &= ~(1 << Lib.ToInt(split[1])); // Đặt bit thành 0
                    }
                    // Ghi lại giá trị vào thanh ghi
                    _plcMC.SetSingleRegister_Int16(split[0], currentValue);
                }
                else
                    _plcMC.SetSingleBit(deviceName, false);
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
        public static bool GetBits(string deviceName)
        {
            bool result = false;
            if (_plcMC == null || !_plcMC.Connected) return result;
            try
            {
                if (deviceName.Contains("."))
                {
                    string[] split = deviceName.Split('.');
                    int value = 16;
                    _plcMC.GetSingleRegister_Int16(split[0], out value);
                    BitArray bitArray = new BitArray(new int[] { value });
                    result = bitArray[Lib.ToInt(split[1])];
                }
                else
                    _plcMC.GetSingleBit(deviceName, out result);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        #endregion
    }
}
