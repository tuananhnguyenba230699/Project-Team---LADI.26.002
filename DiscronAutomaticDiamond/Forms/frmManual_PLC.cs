using AutoBrutingDiamond.Class;
using AutoBrutingDiamond.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrutingDiamond
{
    public partial class frmManual_PLC : Form
    {
        MySQLLib.MySQLHelper _mySqlHelper = new MySQLLib.MySQLHelper();
        bool isCloseThread = false;
        Thread _mappingManualPLC;
        public frmManual_PLC()
        {
            InitializeComponent();
        }

        private void frmManual_PLC_Load(object sender, EventArgs e)
        {
            LoadInfor_PLC();
            if (cDeviceManager._plcMC == null) return;
            if (cDeviceManager._plcMC.Connected)
            {
                _mappingManualPLC = new Thread(MappingManualPLC);
                _mappingManualPLC.IsBackground = true;
                _mappingManualPLC.Start();
            }
        }

        private void LoadInfor_PLC()
        {
            register_device plcDevice = _mySqlHelper.FindByField<register_device>("Type", 2);
            if (plcDevice != null)
            {
                txtPlcIpAddress.Text = plcDevice.IpAddress.ToString();
                txtPlcPort.Text = plcDevice.Port.ToString();
            }
        }

        #region MAPPING BUTTON COLOR
        private void MappingManualPLC()
        {
            while (true)
            {
                Thread.Sleep(100);
                if (isCloseThread) return;

                #region Manual PLC
                try
                {
                    //R2020
                    SetButtonColorManual("R2021", btnInventorOn);
                    //R2021
                    SetButtonColorManual("R2022", btnInventorOff);
                    //R2022
                    SetButtonColorManual("R2023", btnAirFlowStart_On);
                    //R2023
                    SetButtonColorManual("R2024", btnAirFlowStart_Off);
                    //R2024
                    SetButtonColorManual("R2025", btnAirFlowEnd_On);
                    //R2025
                    SetButtonColorManual("R2026", btnAirFlowEnd_Off);

                    //SetButtonColorManual(cDeviceManager.plcTeaching.InventorOn, btnInventorOn);
                    ////R2021
                    //SetButtonColorManual(cDeviceManager.plcTeaching.InventorOff, btnInventorOff);
                    ////R2022
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowStartOn, btnAirFlowStart_On);
                    ////R2023
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowStartOff, btnAirFlowStart_Off);
                    ////R2024
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowEndOn, btnAirFlowEnd_On);
                    ////R2025
                    //SetButtonColorManual(cDeviceManager.plcTeaching.AirFlowEndOff, btnAirFlowEnd_Off);
                }
                catch (Exception ex)
                {
                }
                #endregion

                #region Teachings PLC
                try
                {
                    //R2100
                    SetButtonColorManual("R2100", btnSet_JogPlusAxis1);
                    //R2101
                    SetButtonColorManual("R2101", btnSet_JogMinusAxis1);
                    //R2103
                    SetButtonColorManual("R2103", btnSet_JogPlusAxis2);
                    //R2104
                    SetButtonColorManual("R2104", btnSet_JogMinusAxis2);
                    //R2106
                    SetButtonColorManual("R2106", btnSet_JogPlusAxis3);
                    //R2107
                    SetButtonColorManual("R2107", btnSet_JogMinusAxis3);
                    //R2109
                    SetButtonColorManual("R2109", btnSet_JogPlusAxis4);
                    //R2110
                    SetButtonColorManual("R21010", btnSet_JogMinusAxis4);
                    //R2112
                    SetButtonColorManual("R2112", btnSet_ManualPositionRun1);
                    //R2113
                    SetButtonColorManual("R2113", btnSet_ManualPositionStop1);
                    //R2116
                    SetButtonColorManual("R2116", btnSet_ManualPositionRun2);
                    //R2117
                    SetButtonColorManual("R2117", btnSet_ManualPositionStop2);
                    //R2120
                    SetButtonColorManual("R2120", btnSet_ManualPositionRun3);
                    //R2121
                    SetButtonColorManual("R2121", btnSet_ManualPositionStop3);
                    //R2124
                    SetButtonColorManual("R2124", btnSet_ManualPositionRun4);
                    //R2125
                    SetButtonColorManual("R2125", btnSet_ManualPositionStop4);

                    //R2128
                    SetButtonColorManual("R2128", btnOriginExis2);
                    //R2129
                    SetButtonColorManual("R2129", btnOriginCompleteExis2);
                    //R2130
                    SetButtonColorManual("R2130", btnOriginExis3);
                    //R2131
                    SetButtonColorManual("R2131", btnOriginCompleteExis3);
                    //R2132
                    SetButtonColorManual("R2132", btnOriginExis4);
                    //R2133
                    SetButtonColorManual("R2133", btnOriginCompleteExis4);
                }
                catch (Exception ex)
                {
                }
                #endregion
            }
        }
        #endregion

        #region MANUAL DIVECE
        private void btnInventorOn_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.InventorOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnInventorOff_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.InventorOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnAirFlowStart_On_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowStartOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnAirFlowStart_Off_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowStartOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnAirFlowEnd_On_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowEndOn;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnAirFlowEnd_Off_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.AirFlowEndOff;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }
        #endregion

        #region TEACHING SERVO

        #region JOG + / -
        private void btnJogPlusAxis1Set_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogPlusAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogMinusAxis1_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogMinusAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogPlusAxis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogPlusAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogMinusAxis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogMinusAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogPlusAxis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogPlusAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogMinusAxis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogMinusAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogPlusAxis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogPlusAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_JogMinusAxis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.JogMinusAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }
        #endregion

        #region MANUAL POSITION
        private void btnSet_ManualPositionRun1_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionStop1_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis1;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionRun2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionStop2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionRun3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionStop3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionRun4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MARunPositionAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnSet_ManualPositionStop4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.MAStopPositionAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }
        #endregion

        #region GET SPEED
        private void btnJogSpeedAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis1, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis1, value.ToString());
        }

        private void btnJogSpeedAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis2, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis2, value.ToString());
        }

        private void btnJogSpeedAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis3, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis3, value.ToString());
        }

        private void btnJogSpeedAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis4, out int value);
            Lib.UpdateTextBoxText(txtJogSpeedAxis4, value.ToString());
        }

        private void btnPositionRunAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis1, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis1, value.ToString());
        }

        private void btnPositionCurrentAxis1Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis1, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis1, value.ToString());
        }

        private void btnPositionRunAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis2, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis2, value.ToString());
        }

        private void btnPositionCurrentAxis2Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis2, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis2, value.ToString());
        }

        private void btnPositionRunAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis3, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis3, value.ToString());
        }

        private void btnPositionCurrentAxis3Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis3, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis3, value.ToString());
        }

        private void btnPositionRunAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis4, out int value);
            Lib.UpdateTextBoxText(txtPositionRunAxis4, value.ToString());
        }

        private void btnPositionCurrentAxis4Get_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis4, out int value);
            Lib.UpdateTextBoxText(txtPositionCurrentAxis4, value.ToString());
        }
        #endregion

        #region SET SPEED
        private void btnJogSpeedAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis1, value);
        }

        private void btnJogSpeedAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis2, value);
        }

        private void btnJogSpeedAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis3, value);
        }

        private void btnJogSpeedAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtJogSpeedAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.JogSpeedAxis4, value);
        }

        private void btnPositionRunAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis1, value);
        }

        private void btnPositionCurrentAxis1Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis1, value);
        }

        private void btnPositionRunAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis2, value);
        }

        private void btnPositionCurrentAxis2Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis2, value);
        }

        private void btnPositionRunAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis3, value);
        }

        private void btnPositionCurrentAxis3Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis3, value);
        }

        private void btnPositionRunAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionRunAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionRunAxis4, value);
        }

        private void btnPositionCurrentAxis4Set_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtPositionCurrentAxis4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16(cDeviceManager._teachings_plc.PositionCurentAxis4, value);
        }
        #endregion

        #endregion

        #region ORIGIN
        private void btnOriginExis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnOriginCompleteExis2_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis2;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnOriginExis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnOriginCompleteExis3_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis3;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnOriginExis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }

        private void btnOriginCompleteExis4_Click(object sender, EventArgs e)
        {
            string address = cDeviceManager._teachings_plc.OriginCompleteAxis4;
            cDeviceManager.SetBitsOn(address);
            ResetOffPLC_ButtonClick(address, 2000);
        }
        #endregion

        #region GET DATA
        private void GetDataTeaching(string Address, string AdressPos, string adressSpeed, out int TeachingAxisPosition, out int TeachingAxisSpeed)
        {
            TeachingAxisPosition = 0;
            TeachingAxisSpeed = 0;
            try
            {
                cDeviceManager.GetBits(Address);
                cDeviceManager.GetBits(AdressPos);
                TeachingAxisPosition = TeachingAxisPosition / 1000;
                cDeviceManager.GetBits(adressSpeed);
                TeachingAxisSpeed = TeachingAxisSpeed / 100;

                // cDeviceManager.plcMC.SetDevice(Adress, 0);
            }
            catch (Exception ex)
            {
            }
        }
        private void GetSpeedTeaching(string address, TextBox txt)
        {
            try
            {
                int result = cDeviceManager._plcMC.GetSingleRegister_Int16(address, out int oData_Speed);
                Lib.UpdateTextBoxText(txt, oData_Speed.ToString());
            }
            catch (Exception ex)
            {
            }
        }

        private void SetSpeedTeaching(string address, int speed)
        {
            try
            {
                int result = cDeviceManager._plcMC.SetSingleRegister_Int16(address, speed);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region OTHER
        private void frmManualDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                isCloseThread = true;
                Thread.Sleep(200);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
            }
        }
        private async void SetButtonColorManual(string address, Button btn)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    int valueCheck = cDeviceManager._plcMC.GetSingleRegister_Int16(address, out int value);
                    switch (value)
                    {
                        case 0:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Green, Color.White);
                            break;
                        case 1:
                            Lib.UpdateButton_BColor_FColor(btn, Color.FromArgb(251, 196, 191), Color.Black);
                            break;
                        case 2:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Lime, Color.Black);
                            break;
                        case 3:
                            Lib.UpdateButton_BColor_FColor(btn, Color.Gray, Color.White);
                            break;
                        case 4:
                            Lib.UpdateButton_BColor_FColor(btn, Color.FromArgb(252, 242, 131), Color.Black);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            });
            await task;
        }

        private async void ResetOffPLC_ButtonClick(string address, int delay)
        {
            await Task.Delay(delay);
            cDeviceManager.SetBitsOff(address);
        }

        private async void ResetOnPLC_ButtonClick(string address, int delay)
        {
            await Task.Delay(delay);
            cDeviceManager.SetBitsOn(address);
        }
        #endregion

        #region TEACHING AXIS

        #region Axis #1
        private void btnGet_Offsetcheckvision_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1402", out int value);
            Lib.UpdateTextBoxText(txtOffsetcheckvision_Speed_1, value.ToString());
        }

        private void btnSet_Offsetcheckvision_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffsetcheckvision_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1402", value);
        }

        private void btnGet_RotatePlus_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1408", out int value);
            Lib.UpdateTextBoxText(txtRotatePlus_Speed_1, value.ToString());
        }

        private void btnSet_RotatePlus_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtRotatePlus_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1408", value);
        }

        private void btnGet_RotateMinus_Speed_1_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1414", out int value);
            Lib.UpdateTextBoxText(txtRotateMinus_1, value.ToString());
        }

        private void btnSet_RotateMinus_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtRotateMinus_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1414", value);
        }

        private void btnGet_Grinding_Position_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1418", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_1, value.ToString());
        }

        private void btnSet_Grinding_Position_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1418", value);
        }

        private void btnGet_Grinding_Speed_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1420", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_1, value.ToString());
        }

        private void btnSet_Grinding_Speed_1_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_1.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1420", value);
        }
        #endregion

        #region Exis #2
        private void btnGet_Ready_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1600", out int value);
            Lib.UpdateTextBoxText(txtReady_Position_2, value.ToString());
        }

        private void btnSet_Ready_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1600", value);
        }

        private void btnGet_Ready_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1602", out int value);
            Lib.UpdateTextBoxText(txtReady_Speed_2, value.ToString());
        }

        private void btnSet_Ready_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1602", value);
        }

        private void btnGet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1606", out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Position_2, value.ToString());
        }

        private void btnSet_Concentrick_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1656", value);
            // TODO: 240425
            //cDeviceManager._plcMC.SetSingleRegister_Int16("D1606", value);
        }

        private void btnGet_Concentrick_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1608", out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Speed_2, value.ToString());
        }

        private void btnSet_Concentrick_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1608", value);
        }

        private void btnGet_Concentrick_Adjustment_Position_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1612", out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Adjustment_Position_2, value.ToString());
        }

        private void btnSet_Concentrick_Adjustment_Position_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Adjustment_Position_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1612", value);
        }

        private void btnGet_Concentrick_Adjustment_Speed_2_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1614", out int value);
            Lib.UpdateTextBoxText(txtConcentrick_Adjustment_Speed_2, value.ToString());
        }

        private void btnSet_Concentrick_Adjustment_Speed_2_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtConcentrick_Adjustment_Speed_2.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1614", value);
        }
        #endregion

        #region Axis #3
        private void btnGet_Ready_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1700", out int value);
            Lib.UpdateTextBoxText(txtReady_Position_3, value.ToString());
        }

        private void btnSet_Ready_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1700", value);
        }

        private void btnGet_Ready_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1702", out int value);
            Lib.UpdateTextBoxText(txtReady_Speed_3, value.ToString());
        }

        private void btnSet_Ready_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtReady_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1702", value);
        }

        private void btnGet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1706", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_3, value.ToString());
        }

        private void btnSet_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1706", value);
        }

        private void btnGet_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1708", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_3, value.ToString());
        }

        private void btnSet_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1708", value);
        }

        private void btnGet_Offset_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1712", out int value);
            Lib.UpdateTextBoxText(txtOffset_Grinding_Position_3, value.ToString());
        }

        private void btnSet_Offset_Grinding_Position_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffset_Grinding_Position_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1712", value);
        }

        private void btnGet_Offset_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1714", out int value);
            Lib.UpdateTextBoxText(txtOffset_Grinding_Speed_3, value.ToString());
        }

        private void btnSet_Offset_Grinding_Speed_3_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtOffset_Grinding_Speed_3.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1714", value);
        }
        #endregion

        #region Axis #4
        private void btnGet_Grinding_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1800", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Position_4, value.ToString());
        }

        private void btnSet_Grinding_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1800", value);
        }

        private void btnGet_Grinding_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1802", out int value);
            Lib.UpdateTextBoxText(txtGrinding_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1802", value);
        }

        private void btnGet_Grinding_R_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1806", out int value);
            Lib.UpdateTextBoxText(txtGrinding_R_Position_4, value.ToString());
        }

        private void btnSet_Grinding_R_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_R_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1806", value);
        }

        private void btnGet_Grinding_R_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1804", out int value);
            Lib.UpdateTextBoxText(txtGrinding_R_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_R_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_R_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1804", value);
        }

        private void btnGet_Grinding_L_Position_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int32("D1812", out int value);
            Lib.UpdateTextBoxText(txtGrinding_L_Position_4, value.ToString());
        }

        private void btnSet_Grinding_L_Position_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_L_Position_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int32("D1812", value);
        }

        private void btnGet_Grinding_L_Speed_4_Click(object sender, EventArgs e)
        {
            cDeviceManager._plcMC.GetSingleRegister_Int16("D1814", out int value);
            Lib.UpdateTextBoxText(txtGrinding_L_Speed_4, value.ToString());
        }

        private void btnSet_Grinding_L_Speed_4_Click(object sender, EventArgs e)
        {
            int value = Lib.ToInt(txtGrinding_L_Speed_4.Text.Trim());
            cDeviceManager._plcMC.SetSingleRegister_Int16("D1814", value);
        }
        #endregion

        #endregion

        private void btnSaveDeviceInfor_Click(object sender, EventArgs e)
        {
            register_device register_Device = _mySqlHelper.FindAll<register_device>().FirstOrDefault();
            register_Device.IpAddress = txtPlcIpAddress.Text.Trim();
            register_Device.Port = Lib.ToInt(txtPlcPort.Text.Trim());
            _mySqlHelper.Update<register_device>(register_Device); 
        }
    }
}
