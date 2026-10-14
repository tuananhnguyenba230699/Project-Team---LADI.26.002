using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MC_Protocol_RTCTechnology
{
    internal class McProtocolTcp
    {
        private const int BlockSize = 16;

        protected TcpClient Client { get; set; }

        private NetworkStream Stream { get; set; }

        public McProtocolTcp.McFrame CommandFrame { get; set; }

        public string HostName { get; set; }

        public int PortNumber { get; set; }

        private bool _connected = false;

        public bool Connected
        {
            get => this.Client != null && this.Client.Connected;
            set => _connected = value;
        }

        public McProtocolTcp(string iHostName, int iPortNumber, McProtocolTcp.McFrame SetCommand)
        {
            this.Client = new TcpClient();
            this.CommandFrame = SetCommand;
            this.HostName = iHostName;
            this.PortNumber = iPortNumber;
        }

        public McProtocolTcp(string iHostName, int iPortNumber)
        {
            this.Client = new TcpClient();
            this.CommandFrame = McProtocolTcp.McFrame.MC3E;
            this.HostName = iHostName;
            this.PortNumber = iPortNumber;
        }

        protected void DoConnect()
        {
            DoDisconnect();
            if (this.Client == null)
                this.Client = new TcpClient();

            TcpClient client = this.Client;
            if (client.Connected)
                return;
            List<byte> byteList = new List<byte>(12);
            byteList.AddRange((IEnumerable<byte>)BitConverter.GetBytes(1U));
            byteList.AddRange((IEnumerable<byte>)BitConverter.GetBytes(4500U));
            byteList.AddRange((IEnumerable<byte>)BitConverter.GetBytes(5000U));
            client.Client.IOControl(IOControlCode.KeepAliveValues, byteList.ToArray(), (byte[])null);
            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;
            var c = client.BeginConnect(IPAddress.Parse(this.HostName), this.PortNumber, null, null);
            var success = c.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(20));
            if (success)
            {
                if (client.Connected)
                    this.Stream = client.GetStream();
                Connected = this.Client.Connected;
            }
        }
        public void Dispose() => this.Close();
        protected void DoDisconnect()
        {
            if (this.Client != null)
            {
                try
                {
                    this.Client.GetStream()?.Close();
                    this.Client.GetStream()?.Dispose();
                }
                catch(Exception ex)
                {
                }
                this.Client.Close();
                this.Client.Dispose();
                this.Client = null;
            }
        }

        protected byte[] Execute(byte[] iCommand)
        {
            if (this.Stream == null)
                return new byte[] { };

            NetworkStream stream = this.Stream;
            stream.Write(iCommand, 0, iCommand.Length);
            stream.Flush();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] buffer = new byte[256];
                do
                {
                    int count = stream.Read(buffer, 0, buffer.Length);
                    if (count == 0)
                        throw new Exception("Disconnected");
                    memoryStream.Write(buffer, 0, count);
                }
                while (stream.DataAvailable);
                return memoryStream.ToArray();
            }
        }

        public int Open()
        {
            this.DoConnect();
            this.Command = new McProtocolTcp.McCommand(this.CommandFrame);
            return 0;
        }

        public int Close()
        {
            this.DoDisconnect();
            return 0;
        }

        public int Self_Test(out bool ResultTest)
        {
            byte[] iData = new byte[7]
            {
        (byte) 5,
        (byte) 0,
        (byte) 65,
        (byte) 66,
        (byte) 67,
        (byte) 68,
        (byte) 69
            };
            int num = this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(1561U, 0U, iData)));
            if (0 < num)
            {
                ResultTest = false;
            }
            else
            {
                byte[] response = this.Command.Response;
                ResultTest = ((IEnumerable<byte>)iData).SequenceEqual<byte>((IEnumerable<byte>)response);
            }
            return num;
        }

        public int ClearError() => this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(5655U, 0U, new byte[0])));

        public int GetCpuType(out string oCpuName, out int oCpuType)
        {
            int num = this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(257U, 0U, new byte[0])));
            if (0 < num)
            {
                oCpuName = "dummy";
                oCpuType = 0;
            }
            else
            {
                byte[] response = this.Command.Response;
                oCpuName = Encoding.Default.GetString(response, 0, 16).Replace(" ", "");
                oCpuType = (int)BitConverter.ToInt16(response, 16);
            }
            return num;
        }

        public int SetBitDevice(string iDeviceName, int iSize, int[] iData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.SetBitDevice(oType, oAddress, iSize, iData);
        }

        private int SetBitDevice(
          McProtocolTcp.PlcDeviceType iType,
          int iAddress,
          int iSize,
          int[] iData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num1 = iAddress;
            List<byte> byteList = new List<byte>(6)
      {
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) plcDeviceType,
        (byte) iSize,
        (byte) (iSize >> 8)
      };
            byte num2 = (byte)iData[0];
            int index;
            for (index = 0; index < iData.Length; ++index)
            {
                if (index % 2 == 0)
                {
                    num2 = (byte)((uint)(byte)iData[index] << 4);
                }
                else
                {
                    num2 |= (byte)(iData[index] & 1);
                    byteList.Add(num2);
                }
            }
            if ((uint)(index % 2) > 0U)
                byteList.Add(num2);
            return this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(5121U, 1U, byteList.ToArray())));
        }

        public int GetBitDevice(string iDeviceName, int iSize, int[] oData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.GetBitDevice(oType, oAddress, iSize, oData);
        }

        private int GetBitDevice(
          McProtocolTcp.PlcDeviceType iType,
          int iAddress,
          int iSize,
          int[] oData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num1 = iAddress;
            int num2 = 0;
            if (this.Command != null)
            {

                num2 = this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(1025U, 1U, new List<byte>(6)
      {
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) plcDeviceType,
        (byte) iSize,
        (byte) (iSize >> 8)
      }.ToArray())));
                byte[] response = this.Command.Response;
                for (int index = 0; index < iSize; ++index)
                    oData[index] = index % 2 != 0 ? (num2 == 0 ? (int)response[index / 2] & 1 : 0) : (num2 == 0 ? (int)response[index / 2] >> 4 & 1 : 0);
            }
            return num2;
        }

        public int WriteDeviceBlock(string iDeviceName, int iSize, int[] iData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.WriteDeviceBlock(oType, oAddress, iSize, iData);
        }

        private int WriteDeviceBlock(
          McProtocolTcp.PlcDeviceType iType,
          int iAddress,
          int iSize,
          int[] iData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num1 = iAddress;
            List<byte> byteList = new List<byte>(6)
      {
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) plcDeviceType,
        (byte) iSize,
        (byte) (iSize >> 8)
      };
            foreach (int num2 in iData)
            {
                byteList.Add((byte)num2);
                byteList.Add((byte)(num2 >> 8));
            }
            return this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(5121U, 0U, byteList.ToArray())));
        }

        public int ReadDeviceBlock(string iDeviceName, int iSize, int[] oData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.ReadDeviceBlock(oType, oAddress, iSize, oData);
        }

        private int ReadDeviceBlock(
          McProtocolTcp.PlcDeviceType iType,
          int iAddress,
          int iSize,
          int[] oData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num1 = iAddress;
            int num2 = this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(1025U, 0U, new List<byte>(6)
      {
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) plcDeviceType,
        (byte) iSize,
        (byte) (iSize >> 8)
      }.ToArray())));
            byte[] response = this.Command.Response;
            for (int index = 0; index < iSize; ++index)
                oData[index] = num2 == 0 ? (int)BitConverter.ToInt16(response, index * 2) : 0;
            return num2;
        }

        public int SetDevice(string iDeviceName, int iData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.SetDevice(oType, oAddress, iData);
        }

        private int SetDevice(McProtocolTcp.PlcDeviceType iType, int iAddress, int iData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num = iAddress;
            return this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(5121U, 0U, new List<byte>(6)
      {
        (byte) num,
        (byte) (num >> 8),
        (byte) (num >> 16),
        (byte) plcDeviceType,
        (byte) 1,
        (byte) 0,
        (byte) iData,
        (byte) (iData >> 8)
      }.ToArray())));
        }

        public int GetDevice(string iDeviceName, out int oData)
        {
            McProtocolTcp.PlcDeviceType oType;
            int oAddress;
            McProtocolTcp.GetDeviceCode(iDeviceName, out oType, out oAddress);
            return this.GetDevice(oType, oAddress, out oData);
        }

        private int GetDevice(McProtocolTcp.PlcDeviceType iType, int iAddress, out int oData)
        {
            McProtocolTcp.PlcDeviceType plcDeviceType = iType;
            int num1 = iAddress;
            int num2 = this.Command.SetResponse(this.TryExecution(this.Command.SetCommand(1025U, 0U, new List<byte>(6)
      {
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) plcDeviceType,
        (byte) 1,
        (byte) 0
      }.ToArray())));
            if (0 < num2)
            {
                oData = 0;
            }
            else
            {
                byte[] response = this.Command.Response;
                oData = (int)BitConverter.ToInt16(response, 0);
            }
            return num2;
        }

        public static McProtocolTcp.PlcDeviceType GetDeviceType(string s) => s == "M" ? McProtocolTcp.PlcDeviceType.M : (s == "SM" ? McProtocolTcp.PlcDeviceType.SM : (s == "L" ? McProtocolTcp.PlcDeviceType.L : (s == "F" ? McProtocolTcp.PlcDeviceType.F : (s == "V" ? McProtocolTcp.PlcDeviceType.V : (s == "S" ? McProtocolTcp.PlcDeviceType.S : (s == "X" ? McProtocolTcp.PlcDeviceType.X : (s == "Y" ? McProtocolTcp.PlcDeviceType.Y : (s == "B" ? McProtocolTcp.PlcDeviceType.B : (s == "SB" ? McProtocolTcp.PlcDeviceType.SB : (s == "DX" ? McProtocolTcp.PlcDeviceType.DX : (s == "DY" ? McProtocolTcp.PlcDeviceType.DY : (s == "D" ? McProtocolTcp.PlcDeviceType.D : (s == "SD" ? McProtocolTcp.PlcDeviceType.SD : (s == "R" ? McProtocolTcp.PlcDeviceType.R : (s == "ZR" ? McProtocolTcp.PlcDeviceType.ZR : (s == "W" ? McProtocolTcp.PlcDeviceType.W : (s == "SW" ? McProtocolTcp.PlcDeviceType.SW : (s == "TC" ? McProtocolTcp.PlcDeviceType.TC : (s == "TS" ? McProtocolTcp.PlcDeviceType.TS : (s == "TN" ? McProtocolTcp.PlcDeviceType.TN : (s == "CC" ? McProtocolTcp.PlcDeviceType.CC : (s == "CS" ? McProtocolTcp.PlcDeviceType.CS : (s == "CN" ? McProtocolTcp.PlcDeviceType.CN : (s == "SC" ? McProtocolTcp.PlcDeviceType.SC : (s == "SS" ? McProtocolTcp.PlcDeviceType.SS : (s == "SN" ? McProtocolTcp.PlcDeviceType.SN : (s == "Z" ? McProtocolTcp.PlcDeviceType.Z : (s == "TT" ? McProtocolTcp.PlcDeviceType.TT : (s == "TM" ? McProtocolTcp.PlcDeviceType.TM : (s == "CT" ? McProtocolTcp.PlcDeviceType.CT : (s == "CM" ? McProtocolTcp.PlcDeviceType.CM : (s == "A" ? McProtocolTcp.PlcDeviceType.A : McProtocolTcp.PlcDeviceType.Max))))))))))))))))))))))))))))))));

        public static bool IsBitDevice(McProtocolTcp.PlcDeviceType type) => type != McProtocolTcp.PlcDeviceType.D && type != McProtocolTcp.PlcDeviceType.SD && type != McProtocolTcp.PlcDeviceType.Z && type != McProtocolTcp.PlcDeviceType.ZR && type != McProtocolTcp.PlcDeviceType.R && type != McProtocolTcp.PlcDeviceType.W;

        public static bool IsHexDevice(McProtocolTcp.PlcDeviceType type) => type == McProtocolTcp.PlcDeviceType.X || type == McProtocolTcp.PlcDeviceType.Y || type == McProtocolTcp.PlcDeviceType.B || type == McProtocolTcp.PlcDeviceType.W;

        public static void GetDeviceCode(
          string iDeviceName,
          out McProtocolTcp.PlcDeviceType oType,
          out int oAddress)
        {
            // Kiểm tra iDeviceName có null hoặc rỗng hay không
            if (string.IsNullOrEmpty(iDeviceName))
            {
                throw new ArgumentNullException(nameof(iDeviceName), "Device name cannot be null or empty.");
            }

            string upper = iDeviceName.ToUpper();

            // Kiểm tra chiều dài của chuỗi để tránh lỗi Substring
            if (upper.Length < 1)
            {
                throw new ArgumentException("Invalid device name format.", nameof(iDeviceName));
            }
            //string upper = iDeviceName.ToUpper();
            string s = upper.Substring(0, 1);
            string str1;
            switch (s)
            {
                case "A":
                case "B":
                case "D":
                case "F":
                case "L":
                case "M":
                case "R":
                case "V":
                case "W":
                case "X":
                case "Y":
                    str1 = upper.Substring(1);
                    break;
                case "C":
                    s = upper.Substring(0, 2);
                    string str2 = s;
                    if (!(str2 == "CC") && !(str2 == "CM") && !(str2 == "CN") && !(str2 == "CS") && !(str2 == "CT"))
                        throw new Exception("Invalid format.");
                    str1 = upper.Substring(2);
                    break;
                case "S":
                    s = upper.Substring(0, 2);
                    string str3 = s;
                    if (!(str3 == "SD") && !(str3 == "SM"))
                        throw new Exception("Invalid format.");
                    str1 = upper.Substring(2);
                    break;
                case "T":
                    s = upper.Substring(0, 2);
                    string str4 = s;
                    if (!(str4 == "TC") && !(str4 == "TM") && !(str4 == "TN") && !(str4 == "TS") && !(str4 == "TT"))
                        throw new Exception("Invalid format.");
                    str1 = upper.Substring(2);
                    break;
                case "Z":
                    s = upper.Substring(0, 2);
                    str1 = upper.Substring(s.Equals("ZR") ? 2 : 1);
                    break;
                default:
                    throw new Exception("Invalid format.");
            }
            oType = McProtocolTcp.GetDeviceType(s);
            oAddress = McProtocolTcp.IsHexDevice(oType) ? Convert.ToInt32(str1, 16) : Convert.ToInt32(str1);
        }

        private McProtocolTcp.McCommand Command { get; set; }

        private byte[] TryExecution(byte[] iCommand)
        {
            int num = 10;
            byte[] iResponse;
            do
            {
                iResponse = this.Execute(iCommand);
                --num;
                if (num < 0)
                    throw new Exception("Unable to get correct value from PLC.");
            }
            while (this.Command.IsIncorrectResponse(iResponse));
            return iResponse;
        }

        private static bool IsSocketConnected(Socket s) => (!s.Poll(1000, SelectMode.SelectRead) || s.Available != 0) && s.Connected;

        private class McCommand
        {
            private McProtocolTcp.McFrame FrameType { get; set; }

            private uint SerialNumber { get; set; }

            private uint NetwrokNumber { get; set; }

            private uint PcNumber { get; set; }

            private uint IoNumber { get; set; }

            private uint ChannelNumber { get; set; }

            private uint CpuTimer { get; set; }

            private int ResultCode { get; set; }

            public byte[] Response { get; private set; }

            public McCommand(McProtocolTcp.McFrame iFrame)
            {
                this.FrameType = iFrame;
                this.SerialNumber = 1U;
                this.NetwrokNumber = 0U;
                this.PcNumber = (uint)byte.MaxValue;
                this.IoNumber = 1023U;
                this.ChannelNumber = 0U;
                this.CpuTimer = 16U;
            }

            public byte[] SetCommand(uint iMainCommand, uint iSubCommand, byte[] iData)
            {
                uint num1 = (uint)(iData.Length + 6);
                List<byte> byteList = new List<byte>(iData.Length + 20);
                uint num2 = this.FrameType == McProtocolTcp.McFrame.MC3E ? 80U : (this.FrameType == McProtocolTcp.McFrame.MC4E ? 84U : 0U);
                byteList.Add((byte)num2);
                byteList.Add((byte)(num2 >> 8));
                if (this.FrameType == McProtocolTcp.McFrame.MC4E)
                {
                    byteList.Add((byte)this.SerialNumber);
                    byteList.Add((byte)(this.SerialNumber >> 8));
                    byteList.Add((byte)0);
                    byteList.Add((byte)0);
                }
                byteList.Add((byte)this.NetwrokNumber);
                byteList.Add((byte)this.PcNumber);
                byteList.Add((byte)this.IoNumber);
                byteList.Add((byte)(this.IoNumber >> 8));
                byteList.Add((byte)this.ChannelNumber);
                byteList.Add((byte)num1);
                byteList.Add((byte)(num1 >> 8));
                byteList.Add((byte)this.CpuTimer);
                byteList.Add((byte)(this.CpuTimer >> 8));
                byteList.Add((byte)iMainCommand);
                byteList.Add((byte)(iMainCommand >> 8));
                byteList.Add((byte)iSubCommand);
                byteList.Add((byte)(iSubCommand >> 8));
                byteList.AddRange((IEnumerable<byte>)iData);
                return byteList.ToArray();
            }

            public int SetResponse(byte[] iResponse)
            {
                int srcOffset = this.FrameType == McProtocolTcp.McFrame.MC3E ? 11 : 15;
                if (srcOffset <= iResponse.Length)
                {
                    byte[] numArray1 = new byte[2]
                    {
            iResponse[srcOffset - 4],
            iResponse[srcOffset - 3]
                    };
                    byte[] numArray2 = new byte[2]
                    {
            iResponse[srcOffset - 2],
            iResponse[srcOffset - 1]
                    };
                    int uint16 = (int)BitConverter.ToUInt16(numArray1, 0);
                    this.ResultCode = (int)BitConverter.ToUInt16(numArray2, 0);
                    this.Response = new byte[uint16 - 2];
                    Buffer.BlockCopy((Array)iResponse, srcOffset, (Array)this.Response, 0, this.Response.Length);
                }
                return this.ResultCode;
            }

            public bool IsIncorrectResponse(byte[] iResponse)
            {
                int num1 = this.FrameType == McProtocolTcp.McFrame.MC3E ? 11 : 15;
                byte[] numArray1 = new byte[2]
                {
          iResponse[num1 - 4],
          iResponse[num1 - 3]
                };
                byte[] numArray2 = new byte[2]
                {
          iResponse[num1 - 2],
          iResponse[num1 - 1]
                };
                int num2 = (int)BitConverter.ToUInt16(numArray1, 0) - 2;
                return BitConverter.ToUInt16(numArray2, 0) == (ushort)0 && num2 != iResponse.Length - num1;
            }
        }

        public enum PlcDeviceType
        {
            M = 144, // 0x00000090
            SM = 145, // 0x00000091
            L = 146, // 0x00000092
            F = 147, // 0x00000093
            V = 148, // 0x00000094
            S = 152, // 0x00000098
            X = 156, // 0x0000009C
            Y = 157, // 0x0000009D
            B = 160, // 0x000000A0
            SB = 161, // 0x000000A1
            DX = 162, // 0x000000A2
            DY = 163, // 0x000000A3
            D = 168, // 0x000000A8
            SD = 169, // 0x000000A9
            R = 175, // 0x000000AF
            ZR = 176, // 0x000000B0
            W = 180, // 0x000000B4
            SW = 181, // 0x000000B5
            TC = 192, // 0x000000C0
            TS = 193, // 0x000000C1
            TN = 194, // 0x000000C2
            CC = 195, // 0x000000C3
            CS = 196, // 0x000000C4
            CN = 197, // 0x000000C5
            SC = 198, // 0x000000C6
            SS = 199, // 0x000000C7
            SN = 200, // 0x000000C8
            Z = 204, // 0x000000CC
            TT = 205, // 0x000000CD
            TM = 206, // 0x000000CE
            CT = 207, // 0x000000CF
            CM = 208, // 0x000000D0
            A = 209, // 0x000000D1
            Max = 210, // 0x000000D2
        }

        public enum McFrame
        {
            MC3E,
            MC4E,
        }
    }
}
