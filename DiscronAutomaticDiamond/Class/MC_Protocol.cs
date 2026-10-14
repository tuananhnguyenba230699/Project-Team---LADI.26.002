// Decompiled with JetBrains decompiler
// Type: MC_Protocol_NTT.MC_Protocol
// Assembly: MC_PROTOCOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE847827-2561-4D98-9F3B-2BDEF9BC33E4
// Assembly location: F:\2. CODE NHA MAY\01. PANASONIC\Duplicate Code New - 23082022 ver 2.0\bin\Debug\MC_PROTOCOL.dll

using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;

namespace MC_Protocol_RTCTechnology
{
    public class MC_Protocol
    {
        private static System.Timers.Timer tmr_KeepAlive;
        private object LockCommunication = new object();
        private bool Shift_Disconnect = false;
        private bool flag_Retry = false;

        private McProtocolTcp MC_TCP { get; set; }

        private Support_Convert Sp_Cv { get; set; } = new Support_Convert();

        public string IP_Adress { get; set; }

        public int Port { get; set; }

        public bool KeepAlive { get; set; }

        private bool _connected;

        public bool Connected
        {
            get => this.MC_TCP != null && this.MC_TCP.Connected;
            set => _connected = value;
        }

        public uint CountNumReconnect { get; set; }

        public MC_Protocol(bool AutoReconnect)
        {
            this.KeepAlive = AutoReconnect;
            if (!this.KeepAlive)
                return;
            MC_Protocol.tmr_KeepAlive = new System.Timers.Timer(1000.0);
            MC_Protocol.tmr_KeepAlive.Elapsed += new ElapsedEventHandler(this.Tmr_KeepAlive_Elapsed);
            MC_Protocol.tmr_KeepAlive.AutoReset = false;
        }

        private void Tmr_KeepAlive_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (this.PingHost(this.IP_Adress))
            //{
            //    if (this.MC_TCP != null)
            //    {
            //        this.MC_TCP.Close();
            //        this.MC_TCP.Dispose();
            //    }
            //    Console.WriteLine("Connecting ...");
            //    this.MC_TCP = new McProtocolTcp(this.IP_Adress, this.Port);
            //    if (this.MC_TCP.Open() == 0)
            //    {
            //        try
            //        {
            //            Console.WriteLine("Test Connecting ...");
            //            Thread.Sleep(500);
            //            if (this.MC_TCP.GetDevice("M0", out int _) == 0)
            //            {
            //                Console.WriteLine("Reconnect Success!");
            //                this.MC_TCP.ClearError();
            //                this.flag_Retry = false;
            //                this.Connected = true;
            //            }
            //            else
            //            {
            //                Console.WriteLine("Reconnect Fail!");
            //                MC_Protocol.tmr_KeepAlive.Enabled = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Reconnect Fail!");
            //            MC_Protocol.tmr_KeepAlive.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        if (this.MC_TCP != null)
            //        {
            //            this.MC_TCP.Close();
            //            this.MC_TCP.Dispose();
            //        }
            //        Console.WriteLine("Reconnect Fail!");
            //        MC_Protocol.tmr_KeepAlive.Enabled = true;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Ping Ip Fail: {0}", (object)e.SignalTime);
            //    MC_Protocol.tmr_KeepAlive.Enabled = true;
            //}
            //++this.CountNumReconnect;
        }

        private void ReconnectAlive(string msg)
        {
            try
            {
                if (this.Shift_Disconnect)
                    return;
                Thread.Sleep(100);
                if (this.KeepAlive)
                {
                    if (!this.flag_Retry)
                    {
                        this.CountNumReconnect = 0U;
                        this.Connected = false;
                        this.flag_Retry = true;
                        MC_Protocol.tmr_KeepAlive.Enabled = true;
                    }
                    Console.WriteLine("Reconnect:" + msg);
                }
                else
                {
                    if (this.MC_TCP != null)
                    {
                        this.MC_TCP.Close();
                        this.MC_TCP.Dispose();
                    }
                    this.Connected = false;
                }
            }
            catch
            {
            }
        }

        public bool Connect()
        {
            try
            {
                if (this.IP_Adress != null || (uint)this.Port > 0U)
                {
                    this.MC_TCP = new McProtocolTcp(this.IP_Adress, this.Port);
                    if (this.MC_TCP.Open() == 0)
                    {
                        this.Connected = true;
                        return true;
                    }
                    this.Connected = false;
                    return false;
                }
                this.Connected = false;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error Connect:{0}", (object)ex));
                this.Connected = false;
                return false;
            }
        }

        public bool Connect(string ip_Adress, int port)
        {
            try
            {
                if (this.MC_TCP != null && this.MC_TCP.Connected)
                    return true;

                this.IP_Adress = ip_Adress;
                this.Port = port;
                if (this.MC_TCP != null)
                    this.MC_TCP.Close();
                this.MC_TCP = new McProtocolTcp(this.IP_Adress, this.Port);
                if (this.MC_TCP.Open() == 0)
                {
                    this.Connected = true;
                    return true;
                }
                this.Connected = false;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error Connect:{0}", (object)ex));
                this.Connected = false;
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                this.Shift_Disconnect = true;
                if (this.MC_TCP != null)
                {
                    this.MC_TCP.Close();
                    this.MC_TCP.Dispose();
                }
                Thread.Sleep(500);
                this.Connected = false;
                this.Shift_Disconnect = false;
            }
            catch
            {
            }
        }

        public void GetInfoCPU(out string NameModel, out int CodeModel) => this.MC_TCP.GetCpuType(out NameModel, out CodeModel);

        public void ClearError() => this.MC_TCP.ClearError();

        public int GetSingleBit(string Device, out bool oData)
        {
            oData = false;
            int[] oData1 = new int[1];
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.GetBitDevice(Device, 1, oData1);
                    if (num == 0)
                    {
                        if (oData1[0] == 1)
                            oData = true;
                        else if (oData1[0] == 0)
                            oData = false;
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                // TODO: 18102022
                return 3;
                //throw ex;
            }
        }

        public int GetSingleRegister_Int16(string Device, out int oData)
        {
            oData = 0;
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.GetDevice(Device, out oData);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                // TODO: 18102022
                return 3;
            }
        }

        public int GetSingleRegister_Int32(string Device, out int oData)
        {
            oData = 0;
            try
            {
                int num = 0;
                int[] numArray = new int[2];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, 2, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToInt32(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetSingleRegister_Float32(string Device, out float oData)
        {
            oData = 0.0f;
            try
            {
                int num = 0;
                int[] numArray = new int[2];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, 2, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToFloat32(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetSingleRegister_Float64(string Device, out double oData)
        {
            oData = 0.0;
            try
            {
                int num = 0;
                int[] numArray = new int[4];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, 4, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToFloat64(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetMultiBit(string Device, int Size, out bool[] oData)
        {
            oData = new bool[Size];
            try
            {
                int num = 0;
                int[] oData1 = new int[Size];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.GetBitDevice(Device, Size, oData1);
                    if (num == 0)
                    {
                        for (int index = 0; index < Size; ++index)
                        {
                            if (oData1[index] == 1)
                                oData[index] = true;
                            else if (oData1[index] == 0)
                                oData[index] = false;
                        }
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetMultiRegister_Int16(string Device, int Size, out int[] oData)
        {
            oData = new int[Size];
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, Size, oData);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetMultiRegister_Int32(string Device, int Size, out int[] oData)
        {
            oData = new int[Size];
            try
            {
                int num = 0;
                int[] numArray = new int[Size * 2];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, Size * 2, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToArrayInt32(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetMultiRegister_Float32(string Device, int Size, out float[] oData)
        {
            oData = new float[Size];
            try
            {
                int num = 0;
                int[] numArray = new int[Size * 2];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, Size * 2, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToArrayFloat32(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetMultiRegister_Float64(string Device, int Size, out double[] oData)
        {
            oData = new double[Size];
            try
            {
                int num = 0;
                int[] numArray = new int[Size * 4];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, Size * 4, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToArrayFloat64(numArray);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int GetRegister_String(string Device, int Size_Characters, out string oData)
        {
            oData = "";
            try
            {
                int num = 0;
                int iSize = (uint)(Size_Characters % 2) <= 0U ? Size_Characters / 2 : Size_Characters / 2 + 1;
                int[] numArray = new int[iSize];
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.ReadDeviceBlock(Device, iSize, numArray);
                    if (num == 0)
                    {
                        oData = this.Sp_Cv.ArrayInt16ToString(numArray).Substring(0, Size_Characters);
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                // TODO: 18102022
                return 3;
                //throw ex;
            }
        }

        public int SetSingleBit(string Device, bool iData)
        {
            int[] iData1 = new int[1];
            if (iData)
                iData1[0] = 1;
            else if (!iData)
                iData1[0] = 0;
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.SetBitDevice(Device, 1, iData1);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                // TODO: 18102022
                return 3;
                //throw ex;
            }
        }

        public int SetSingleRegister_Int16(string Device, int iData)
        {
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.SetDevice(Device, iData);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                // TODO: 18102022
                return 3;
                //throw ex;
            }
        }

        public int SetSingleRegister_Int32(string Device, int iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.Int32ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(Device, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetSingleRegister_Float32(string Device, float iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.Float32ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(Device, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetSingleRegister_Float64(string Device, double iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.Float64ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(Device, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetMultiBit(string Device, bool[] iData)
        {
            int[] iData1 = new int[iData.Length];
            for (int index = 0; index < iData.Length; ++index)
            {
                if (iData[index])
                    iData1[index] = 1;
                else if (!iData[index])
                    iData1[index] = 0;
            }
            try
            {
                int num = 0;
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.SetBitDevice(Device, iData.Length, iData1);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetMultiRegister_Int16(string NameDevice, int[] iData)
        {
            int num = 0;
            int length = iData.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(NameDevice, length, iData);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetMultiRegister_Int32(string NameDevice, int[] iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.ArrayInt32ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(NameDevice, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
                //throw ex;
            }
        }

        public int SetMultiRegister_Float32(string NameDevice, float[] iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.ArrayFloat32ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(NameDevice, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetMultiRegister_Float64(string NameDevice, double[] iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.ArrayFloat64ToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(NameDevice, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        public int SetRegister_String(string NameDevice, string iData)
        {
            int num = 0;
            int[] arrayInt16 = this.Sp_Cv.StringToArrayInt16(iData);
            int length = arrayInt16.Length;
            try
            {
                if (this.MC_TCP != null)
                {
                    lock (this.LockCommunication)
                        num = this.MC_TCP.WriteDeviceBlock(NameDevice, length, arrayInt16);
                    if (num == 0)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    this.Connected = false;
                    return 1;
                }
                this.Connected = false;
                return 2;
            }
            catch (Exception ex)
            {
                this.ReconnectAlive(ex.ToString());
                return 3;
            }
        }

        private bool PingHost(string nameOrAddress)
        {
            bool flag = false;
            Ping ping = (Ping)null;
            try
            {
                ping = new Ping();
                flag = ping.Send(nameOrAddress).Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
            }
            finally
            {
                ping?.Dispose();
            }
            return flag;
        }
    }
}
