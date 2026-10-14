// Decompiled with JetBrains decompiler
// Type: MC_Protocol_NTT.Support_Convert
// Assembly: MC_PROTOCOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE847827-2561-4D98-9F3B-2BDEF9BC33E4
// Assembly location: F:\2. CODE NHA MAY\01. PANASONIC\Duplicate Code New - 23082022 ver 2.0\bin\Debug\MC_PROTOCOL.dll

using System;
using System.Text;

namespace MC_Protocol_RTCTechnology
{
    internal class Support_Convert
    {
        public string ArrayInt16ToString(int[] dataInput)
        {
            int length = dataInput.Length;
            byte[] bytes1 = new byte[length * 2];
            for (int index = 0; index <= length - 1; ++index)
            {
                byte[] bytes2 = BitConverter.GetBytes(dataInput[index]);
                bytes1[index * 2] = bytes2[0];
                bytes1[index * 2 + 1] = bytes2[1];
            }
            return Encoding.Default.GetString(bytes1);
        }

        public int[] StringToArrayInt16(string dataInput)
        {
            int length = (uint)(dataInput.Length % 2) <= 0U ? dataInput.Length / 2 : dataInput.Length / 2 + 1;
            int[] numArray = new int[length];
            byte[] bytes = Encoding.Default.GetBytes(dataInput);
            int num = Math.Min(bytes.Length, length * 2);
            for (int startIndex = 0; startIndex <= num - 2; startIndex += 2)
                numArray[startIndex / 2] = (int)BitConverter.ToInt16(bytes, startIndex);
            if (num % 2 == 1)
                numArray[num / 2] = (int)bytes[num - 1];
            return numArray;
        }

        public int ArrayInt16ToInt32(int[] dataInput)
        {
            byte[] numArray = new byte[4];
            for (int index = 0; index <= 1; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(dataInput[index]);
                numArray[index * 2] = bytes[0];
                numArray[index * 2 + 1] = bytes[1];
            }
            return Convert.ToInt32(BitConverter.ToInt32(numArray, 0));
        }

        public int[] Int32ToArrayInt16(int dataInput)
        {
            int[] numArray = new int[2];
            byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(dataInput));
            numArray[0] = (int)BitConverter.ToInt16(bytes, 0);
            numArray[1] = (int)BitConverter.ToInt16(bytes, 2);
            return numArray;
        }

        public int[] ArrayInt16ToArrayInt32(int[] dataInput)
        {
            int length = dataInput.Length;
            int[] numArray1 = new int[length / 2];
            byte[] numArray2 = new byte[4];
            int index1 = 0;
            for (int index2 = 0; index2 <= length / 2 - 1; ++index2)
            {
                for (int index3 = 0; index3 <= 1; ++index3)
                {
                    byte[] bytes = BitConverter.GetBytes(dataInput[index1]);
                    numArray2[index3 * 2] = bytes[0];
                    numArray2[index3 * 2 + 1] = bytes[1];
                    ++index1;
                }
                numArray1[index2] = Convert.ToInt32(BitConverter.ToInt32(numArray2, 0));
            }
            return numArray1;
        }

        public int[] ArrayInt32ToArrayInt16(int[] dataInput)
        {
            int length = dataInput.Length;
            int[] numArray = new int[length * 2];
            for (int index = 0; index <= length - 1; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(dataInput[index]));
                numArray[index * 2] = (int)BitConverter.ToInt16(bytes, 0);
                numArray[index * 2 + 1] = (int)BitConverter.ToInt16(bytes, 2);
            }
            return numArray;
        }

        public float ArrayInt16ToFloat32(int[] dataInput)
        {
            byte[] numArray = new byte[4];
            for (int index = 0; index <= 1; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(dataInput[index]);
                numArray[index * 2] = bytes[0];
                numArray[index * 2 + 1] = bytes[1];
            }
            return Convert.ToSingle(BitConverter.ToSingle(numArray, 0));
        }

        public int[] Float32ToArrayInt16(float dataInput)
        {
            int[] numArray = new int[2];
            byte[] bytes = BitConverter.GetBytes(Convert.ToSingle(dataInput));
            numArray[0] = (int)BitConverter.ToInt16(bytes, 0);
            numArray[1] = (int)BitConverter.ToInt16(bytes, 2);
            return numArray;
        }

        public float[] ArrayInt16ToArrayFloat32(int[] dataInput)
        {
            int length = dataInput.Length;
            float[] numArray1 = new float[length / 2];
            byte[] numArray2 = new byte[4];
            int index1 = 0;
            for (int index2 = 0; index2 <= length / 2 - 1; ++index2)
            {
                for (int index3 = 0; index3 <= 1; ++index3)
                {
                    byte[] bytes = BitConverter.GetBytes(dataInput[index1]);
                    numArray2[index3 * 2] = bytes[0];
                    numArray2[index3 * 2 + 1] = bytes[1];
                    ++index1;
                }
                numArray1[index2] = Convert.ToSingle(BitConverter.ToSingle(numArray2, 0));
            }
            return numArray1;
        }

        public double ArrayInt16ToFloat64(int[] dataInput)
        {
            byte[] numArray = new byte[8];
            for (int index = 0; index <= 3; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(dataInput[index]);
                numArray[index * 2] = bytes[0];
                numArray[index * 2 + 1] = bytes[1];
            }
            return (double)Convert.ToSingle(BitConverter.ToDouble(numArray, 0));
        }

        public int[] Float64ToArrayInt16(double dataInput)
        {
            int[] numArray = new int[4];
            byte[] bytes = BitConverter.GetBytes(Convert.ToDouble(dataInput));
            numArray[0] = (int)BitConverter.ToInt16(bytes, 0);
            numArray[1] = (int)BitConverter.ToInt16(bytes, 2);
            numArray[2] = (int)BitConverter.ToInt16(bytes, 4);
            numArray[3] = (int)BitConverter.ToInt16(bytes, 6);
            return numArray;
        }

        public double[] ArrayInt16ToArrayFloat64(int[] dataInput)
        {
            int length = dataInput.Length;
            double[] numArray1 = new double[length / 4];
            byte[] numArray2 = new byte[8];
            int index1 = 0;
            for (int index2 = 0; index2 <= length / 4 - 1; ++index2)
            {
                for (int index3 = 0; index3 <= 3; ++index3)
                {
                    byte[] bytes = BitConverter.GetBytes(dataInput[index1]);
                    numArray2[index3 * 2] = bytes[0];
                    numArray2[index3 * 2 + 1] = bytes[1];
                    ++index1;
                }
                numArray1[index2] = (double)Convert.ToSingle(BitConverter.ToDouble(numArray2, 0));
            }
            return numArray1;
        }

        public int[] ArrayFloat32ToArrayInt16(float[] dataInput)
        {
            int length = dataInput.Length;
            int[] numArray = new int[length * 2];
            for (int index = 0; index <= length - 1; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(Convert.ToSingle(dataInput[index]));
                numArray[index * 2] = (int)BitConverter.ToInt16(bytes, 0);
                numArray[index * 2 + 1] = (int)BitConverter.ToInt16(bytes, 2);
            }
            return numArray;
        }

        public int[] ArrayFloat64ToArrayInt16(double[] dataInput)
        {
            int length = dataInput.Length;
            int[] numArray = new int[length * 4];
            for (int index = 0; index <= length - 1; ++index)
            {
                byte[] bytes = BitConverter.GetBytes(Convert.ToDouble(dataInput[index]));
                numArray[index * 4] = (int)BitConverter.ToInt16(bytes, 0);
                numArray[index * 4 + 1] = (int)BitConverter.ToInt16(bytes, 2);
                numArray[index * 4 + 2] = (int)BitConverter.ToInt16(bytes, 4);
                numArray[index * 4 + 3] = (int)BitConverter.ToInt16(bytes, 6);
            }
            return numArray;
        }
    }
}
