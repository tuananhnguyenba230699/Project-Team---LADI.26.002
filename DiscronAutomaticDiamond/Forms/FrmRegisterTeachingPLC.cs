using AutoBrutingDiamond.Class;
using AutoBrutingDiamond.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrutingDiamond.Forms
{
    public partial class FrmRegisterTeachingPLC : Form
    {
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        public FrmRegisterTeachingPLC()
        {
            InitializeComponent();
        }


        private void FrmRegisterTeachingPLC_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            #region MANUAL PLC

            #region  Jog Axis
            txtJogPlusAxis1.Text = cDeviceManager._teachings_plc.JogPlusAxis1;
            txtJogPlusAxis1Lamp.Text = cDeviceManager._teachings_plc.JogPlusAxis1Lamp;
            txtJogPlusAxis2.Text = cDeviceManager._teachings_plc.JogPlusAxis2;
            txtJogPlusAxis2Lamp.Text = cDeviceManager._teachings_plc.JogPlusAxis2Lamp;
            txtJogPlusAxis3.Text = cDeviceManager._teachings_plc.JogPlusAxis3;
            txtJogPlusAxis3Lamp.Text = cDeviceManager._teachings_plc.JogPlusAxis3Lamp;
            txtJogPlusAxis4.Text = cDeviceManager._teachings_plc.JogPlusAxis4;
            txtJogPlusAxis4Lamp.Text = cDeviceManager._teachings_plc.JogPlusAxis4Lamp;

            txtJogMinusAxis1.Text = cDeviceManager._teachings_plc.JogMinusAxis1;
            txtJogMinusAxis1Lamp.Text = cDeviceManager._teachings_plc.JogMinusAxis1Lamp;
            txtJogMinusAxis2.Text = cDeviceManager._teachings_plc.JogMinusAxis2;
            txtJogMinusAxis2Lamp.Text = cDeviceManager._teachings_plc.JogMinusAxis2Lamp;
            txtJogMinusAxis3.Text = cDeviceManager._teachings_plc.JogMinusAxis3;
            txtJogMinusAxis3Lamp.Text = cDeviceManager._teachings_plc.JogMinusAxis3Lamp;
            txtJogMinusAxis4.Text = cDeviceManager._teachings_plc.JogMinusAxis4;
            txtJogMinusAxis4Lamp.Text = cDeviceManager._teachings_plc.JogMinusAxis4Lamp;

            txtJogSpeedAxis1.Text = cDeviceManager._teachings_plc.JogSpeedAxis1;
            txtJogSpeedAxis2.Text = cDeviceManager._teachings_plc.JogSpeedAxis2;
            txtJogSpeedAxis3.Text = cDeviceManager._teachings_plc.JogSpeedAxis3;
            txtJogSpeedAxis4.Text = cDeviceManager._teachings_plc.JogSpeedAxis4;
            #endregion

            #region Manual Axis

            txtMARunPositionAxis1.Text = cDeviceManager._teachings_plc.MARunPositionAxis1;
            txtMARunPositionAxis1Lamp.Text = cDeviceManager._teachings_plc.MARunPositionAxis1Lamp;
            txtMARunPositionAxis2.Text = cDeviceManager._teachings_plc.MARunPositionAxis2;
            txtMARunPositionAxis2Lamp.Text = cDeviceManager._teachings_plc.MARunPositionAxis2Lamp;
            txtMARunPositionAxis3.Text = cDeviceManager._teachings_plc.MARunPositionAxis3;
            txtMARunPositionAxis3Lamp.Text = cDeviceManager._teachings_plc.MARunPositionAxis3Lamp;
            txtMARunPositionAxis4.Text = cDeviceManager._teachings_plc.MARunPositionAxis4;
            txtMARunPositionAxis4Lamp.Text = cDeviceManager._teachings_plc.MARunPositionAxis4Lamp;

            txtMAStopPositionAxis1.Text = cDeviceManager._teachings_plc.MAStopPositionAxis1;
            txtMAStopPositionAxis1Lamp.Text = cDeviceManager._teachings_plc.MAStopPositionAxis1Lamp;
            txtMAStopPositionAxis2.Text = cDeviceManager._teachings_plc.MAStopPositionAxis2;
            txtMAStopPositionAxis2Lamp.Text = cDeviceManager._teachings_plc.MAStopPositionAxis2Lamp;
            txtMAStopPositionAxis3.Text = cDeviceManager._teachings_plc.MAStopPositionAxis3;
            txtMAStopPositionAxis3Lamp.Text = cDeviceManager._teachings_plc.MAStopPositionAxis3Lamp;
            txtMAStopPositionAxis4.Text = cDeviceManager._teachings_plc.MAStopPositionAxis4;
            txtMAStopPositionAxis4Lamp.Text = cDeviceManager._teachings_plc.MAStopPositionAxis4Lamp;

            txtPositionRunAxis1.Text = cDeviceManager._teachings_plc.PositionRunAxis1;
            txtPositionRunAxis2.Text = cDeviceManager._teachings_plc.PositionRunAxis2;
            txtPositionRunAxis3.Text = cDeviceManager._teachings_plc.PositionRunAxis3;
            txtPositionRunAxis4.Text = cDeviceManager._teachings_plc.PositionRunAxis4;

            txtPositionCurentAxis1.Text = cDeviceManager._teachings_plc.PositionCurentAxis1;
            txtPositionCurentAxis2.Text = cDeviceManager._teachings_plc.PositionCurentAxis2;
            txtPositionCurentAxis3.Text = cDeviceManager._teachings_plc.PositionCurentAxis3;
            txtPositionCurentAxis4.Text = cDeviceManager._teachings_plc.PositionCurentAxis4;

            #endregion

            #region Origin Axis
            txtOriginAxis2.Text = cDeviceManager._teachings_plc.OriginAxis2;
            txtOriginAxis2Lamp.Text = cDeviceManager._teachings_plc.OriginAxis2Lamp;
            txtOriginAxis3.Text = cDeviceManager._teachings_plc.OriginAxis3;
            txtOriginAxis3Lamp.Text = cDeviceManager._teachings_plc.OriginAxis3Lamp;
            txtOriginAxis4.Text = cDeviceManager._teachings_plc.OriginAxis4;
            txtOriginAxis4Lamp.Text = cDeviceManager._teachings_plc.OriginAxis4Lamp;

            txtOriginCompleteAxis2.Text = cDeviceManager._teachings_plc.OriginCompleteAxis2;
            txtOriginCompleteAxis2Lamp.Text = cDeviceManager._teachings_plc.OriginCompleteAxis2Lamp;
            txtOriginCompleteAxis3.Text = cDeviceManager._teachings_plc.OriginCompleteAxis3;
            txtOriginCompleteAxis3Lamp.Text = cDeviceManager._teachings_plc.OriginCompleteAxis3Lamp;
            txtOriginCompleteAxis4.Text = cDeviceManager._teachings_plc.OriginCompleteAxis4;
            txtOriginCompleteAxis4Lamp.Text = cDeviceManager._teachings_plc.OriginCompleteAxis4Lamp;

            #endregion
            #endregion

            #region TEACHING  
            //Axis 1
            txtOffsetcheckvision_Speed_1.Text = cDeviceManager._teachings_plc.OffsetCheckVisionSpeedAxis1;
            txtRotateMinus_Speed_1.Text = cDeviceManager._teachings_plc.RotateMinus90SpeedAxis1;
            txtRotatePlus_Speed_1.Text = cDeviceManager._teachings_plc.Rotate90SpeedAxis1;
            txtGrinding_Position_1.Text = cDeviceManager._teachings_plc.RotateGrindingPositionAxis1;
            txtGrinding_Speed_1.Text = cDeviceManager._teachings_plc.RotateGrindingSpeedAxis1;

            //Axis 2
            txtReady_Position_2.Text = cDeviceManager._teachings_plc.ReadyPositionAxis2;
            txtReady_Speed_2.Text = cDeviceManager._teachings_plc.ReadySpeedAxis2;
            txtConcentrick_Position_2.Text = cDeviceManager._teachings_plc.StartConcentricPositionAxis2;
            txtConcentrick_Speed_2.Text = cDeviceManager._teachings_plc.StartConcentricSpeedAxis2;
            txtConcentrick_Adjustment_Position_2.Text = cDeviceManager._teachings_plc.ConcentricAdjustmentPositionAxis2;
            txtConcentrick_Adjustment_Speed_2.Text = cDeviceManager._teachings_plc.ConcentricAdjustmentSpeedAxis2;

            //Axis3 
            txtReady_Position_3.Text = cDeviceManager._teachings_plc.ReadyPositionAxis3;
            txtReady_Speed_3.Text = cDeviceManager._teachings_plc.ReadySpeedAxis3;
            txtGrinding_Position_3.Text = cDeviceManager._teachings_plc.StartGrindingPositionAxis3;
            txtGrinding_Speed_3.Text = cDeviceManager._teachings_plc.StartGrindingSpeedAxis3;
            txtOffset_Grinding_Position_3.Text = cDeviceManager._teachings_plc.OffsetGrindingPositionAxis3;
            txtOffset_Grinding_Speed_3.Text = cDeviceManager._teachings_plc.OffsetGrindingSpeedAxis3;

            //Axis4
            txtGrinding_Position_4.Text = cDeviceManager._teachings_plc.GrindingPositionAxis4;
            txtGrinding_Speed_4.Text = cDeviceManager._teachings_plc.GrindingSpeedAxis4;
            txtGrinding_R_Position_4.Text = cDeviceManager._teachings_plc.ManualGrindingRightPositionAxis4;
            txtGrinding_R_Speed_4.Text = cDeviceManager._teachings_plc.ManualGrindingRightSpeedAxis4;
            txtGrinding_L_Position_4.Text = cDeviceManager._teachings_plc.ManualGrindingLeftPositionAxis4;
            txtGrinding_L_Speed_4.Text = cDeviceManager._teachings_plc.ManualGrindingLeftSpeedAxis4;
            #endregion

            #region ALARM
            //Inventor
            txtInventorOnGet.Text = cDeviceManager._teachings_plc.InventorOn;
            txtInventorOnSet.Text = cDeviceManager._teachings_plc.SetInventorOn;
            txtInventorOffGet.Text = cDeviceManager._teachings_plc.InventorOff;
            txtInventorOffSet.Text = cDeviceManager._teachings_plc.SetInventorOff;

            //Air flow start
            txtAirFlowStartOnGet.Text = cDeviceManager._teachings_plc.AirFlowStartOn;
            txtAirFlowStartOnSet.Text = cDeviceManager._teachings_plc.SetAirFlowStartOn;
            txtAirFlowStartOffGet.Text = cDeviceManager._teachings_plc.AirFlowStartOff;
            txtAirFlowStartOffSet.Text = cDeviceManager._teachings_plc.SetAirFlowStartOff;

            //Air flow end
            txtAirFlowEndOnGet.Text = cDeviceManager._teachings_plc.AirFlowEndOn;
            txtAirFlowEndOnSet.Text = cDeviceManager._teachings_plc.SetAirFlowEndOn;
            txtAirFlowEndOffGet.Text = cDeviceManager._teachings_plc.AirFlowEndOff;
            txtAirFlowEndOffSet.Text = cDeviceManager._teachings_plc.SetAirFlowEndOff;

            #endregion
        }

        private void btnSaveTeachingAndAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDataTeachingAlarm()) return;

                #region TEACHING  
                // Axis 1
                cDeviceManager._teachings_plc.OffsetCheckVisionSpeedAxis1 = txtOffsetcheckvision_Speed_1.Text.Trim();
                cDeviceManager._teachings_plc.RotateMinus90SpeedAxis1 = txtRotateMinus_Speed_1.Text.Trim();
                cDeviceManager._teachings_plc.Rotate90SpeedAxis1 = txtRotatePlus_Speed_1.Text.Trim();
                cDeviceManager._teachings_plc.RotateGrindingPositionAxis1 = txtGrinding_Position_1.Text.Trim();
                cDeviceManager._teachings_plc.RotateGrindingSpeedAxis1 = txtGrinding_Speed_1.Text.Trim();

                // Axis 2
                cDeviceManager._teachings_plc.ReadyPositionAxis2 = txtReady_Position_2.Text.Trim();
                cDeviceManager._teachings_plc.ReadySpeedAxis2 = txtReady_Speed_2.Text.Trim();
                cDeviceManager._teachings_plc.StartConcentricPositionAxis2 = txtConcentrick_Position_2.Text.Trim();
                cDeviceManager._teachings_plc.StartConcentricSpeedAxis2 = txtConcentrick_Speed_2.Text.Trim();
                cDeviceManager._teachings_plc.ConcentricAdjustmentPositionAxis2 = txtConcentrick_Adjustment_Position_2.Text.Trim();
                cDeviceManager._teachings_plc.ConcentricAdjustmentSpeedAxis2 = txtConcentrick_Adjustment_Speed_2.Text.Trim();

                // Axis 3
                cDeviceManager._teachings_plc.ReadyPositionAxis3 = txtReady_Position_3.Text.Trim();
                cDeviceManager._teachings_plc.ReadySpeedAxis3 = txtReady_Speed_3.Text.Trim();
                cDeviceManager._teachings_plc.StartGrindingPositionAxis3 = txtGrinding_Position_3.Text.Trim();
                cDeviceManager._teachings_plc.StartGrindingSpeedAxis3 = txtGrinding_Speed_3.Text.Trim();
                cDeviceManager._teachings_plc.OffsetGrindingPositionAxis3 = txtOffset_Grinding_Position_3.Text.Trim();
                cDeviceManager._teachings_plc.OffsetGrindingSpeedAxis3 = txtOffset_Grinding_Speed_3.Text.Trim();

                // Axis 4
                cDeviceManager._teachings_plc.GrindingPositionAxis4 = txtGrinding_Position_4.Text.Trim();
                cDeviceManager._teachings_plc.GrindingSpeedAxis4 = txtGrinding_Speed_4.Text.Trim();
                cDeviceManager._teachings_plc.ManualGrindingRightPositionAxis4 = txtGrinding_R_Position_4.Text.Trim();
                cDeviceManager._teachings_plc.ManualGrindingRightSpeedAxis4 = txtGrinding_R_Speed_4.Text.Trim();
                cDeviceManager._teachings_plc.ManualGrindingLeftPositionAxis4 = txtGrinding_L_Position_4.Text.Trim();
                cDeviceManager._teachings_plc.ManualGrindingLeftSpeedAxis4 = txtGrinding_L_Speed_4.Text.Trim();

                #endregion

                #region ALARM
                // Inventor
                cDeviceManager._teachings_plc.InventorOn = txtInventorOnGet.Text.Trim();
                cDeviceManager._teachings_plc.SetInventorOn = txtInventorOnSet.Text.Trim();
                cDeviceManager._teachings_plc.InventorOff = txtInventorOffGet.Text.Trim();
                cDeviceManager._teachings_plc.SetInventorOff = txtInventorOffSet.Text.Trim();

                // Air flow start
                cDeviceManager._teachings_plc.AirFlowStartOn = txtAirFlowStartOnGet.Text.Trim();
                cDeviceManager._teachings_plc.SetAirFlowStartOn = txtAirFlowStartOnSet.Text.Trim();
                cDeviceManager._teachings_plc.AirFlowStartOff = txtAirFlowStartOffGet.Text.Trim();
                cDeviceManager._teachings_plc.SetAirFlowStartOff = txtAirFlowStartOffSet.Text.Trim();

                // Air flow end
                cDeviceManager._teachings_plc.AirFlowEndOn = txtAirFlowEndOnGet.Text.Trim();
                cDeviceManager._teachings_plc.SetAirFlowEndOn = txtAirFlowEndOnSet.Text.Trim();
                cDeviceManager._teachings_plc.AirFlowEndOff = txtAirFlowEndOffGet.Text.Trim();
                cDeviceManager._teachings_plc.SetAirFlowEndOff = txtAirFlowEndOffSet.Text.Trim();
                #endregion

                _mySQLHelper.Update<register_teachings_plc>(cDeviceManager._teachings_plc);
                MessageBox.Show("Save Manual PLC data successfull!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Save Teaching And Alarm data fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSaveManualPlc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateDataManualPLC()) return;
                #region Jog Axis
                cDeviceManager._teachings_plc.JogPlusAxis1 = txtJogPlusAxis1.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis1Lamp = txtJogPlusAxis1Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis2 = txtJogPlusAxis2.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis2Lamp = txtJogPlusAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis3 = txtJogPlusAxis3.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis3Lamp = txtJogPlusAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis4 = txtJogPlusAxis4.Text.Trim();
                cDeviceManager._teachings_plc.JogPlusAxis4Lamp = txtJogPlusAxis4Lamp.Text.Trim();

                cDeviceManager._teachings_plc.JogMinusAxis1 = txtJogMinusAxis1.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis1Lamp = txtJogMinusAxis1Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis2 = txtJogMinusAxis2.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis2Lamp = txtJogMinusAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis3 = txtJogMinusAxis3.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis3Lamp = txtJogMinusAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis4 = txtJogMinusAxis4.Text.Trim();
                cDeviceManager._teachings_plc.JogMinusAxis4Lamp = txtJogMinusAxis4Lamp.Text.Trim();

                cDeviceManager._teachings_plc.JogSpeedAxis1 = txtJogSpeedAxis1.Text.Trim();
                cDeviceManager._teachings_plc.JogSpeedAxis2 = txtJogSpeedAxis2.Text.Trim();
                cDeviceManager._teachings_plc.JogSpeedAxis3 = txtJogSpeedAxis3.Text.Trim();
                cDeviceManager._teachings_plc.JogSpeedAxis4 = txtJogSpeedAxis4.Text.Trim();
                #endregion

                #region Manual Axis
                cDeviceManager._teachings_plc.MARunPositionAxis1 = txtMARunPositionAxis1.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis1Lamp = txtMARunPositionAxis1Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis2 = txtMARunPositionAxis2.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis2Lamp = txtMARunPositionAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis3 = txtMARunPositionAxis3.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis3Lamp = txtMARunPositionAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis4 = txtMARunPositionAxis4.Text.Trim();
                cDeviceManager._teachings_plc.MARunPositionAxis4Lamp = txtMARunPositionAxis4Lamp.Text.Trim();

                cDeviceManager._teachings_plc.MAStopPositionAxis1 = txtMAStopPositionAxis1.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis1Lamp = txtMAStopPositionAxis1Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis2 = txtMAStopPositionAxis2.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis2Lamp = txtMAStopPositionAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis3 = txtMAStopPositionAxis3.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis3Lamp = txtMAStopPositionAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis4 = txtMAStopPositionAxis4.Text.Trim();
                cDeviceManager._teachings_plc.MAStopPositionAxis4Lamp = txtMAStopPositionAxis4Lamp.Text.Trim();

                cDeviceManager._teachings_plc.PositionRunAxis1 = txtPositionRunAxis1.Text.Trim();
                cDeviceManager._teachings_plc.PositionRunAxis2 = txtPositionRunAxis2.Text.Trim();
                cDeviceManager._teachings_plc.PositionRunAxis3 = txtPositionRunAxis3.Text.Trim();
                cDeviceManager._teachings_plc.PositionRunAxis4 = txtPositionRunAxis4.Text.Trim();

                cDeviceManager._teachings_plc.PositionCurentAxis1 = txtPositionCurentAxis1.Text.Trim();
                cDeviceManager._teachings_plc.PositionCurentAxis2 = txtPositionCurentAxis2.Text.Trim();
                cDeviceManager._teachings_plc.PositionCurentAxis3 = txtPositionCurentAxis3.Text.Trim();
                cDeviceManager._teachings_plc.PositionCurentAxis4 = txtPositionCurentAxis4.Text.Trim();
                #endregion

                #region Origin Axis
                cDeviceManager._teachings_plc.OriginAxis2 = txtOriginAxis2.Text.Trim();
                cDeviceManager._teachings_plc.OriginAxis2Lamp = txtOriginAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.OriginAxis3 = txtOriginAxis3.Text.Trim();
                cDeviceManager._teachings_plc.OriginAxis3Lamp = txtOriginAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.OriginAxis4 = txtOriginAxis4.Text.Trim();
                cDeviceManager._teachings_plc.OriginAxis4Lamp = txtOriginAxis4Lamp.Text.Trim();

                cDeviceManager._teachings_plc.OriginCompleteAxis2 = txtOriginCompleteAxis2.Text.Trim();
                cDeviceManager._teachings_plc.OriginCompleteAxis2Lamp = txtOriginCompleteAxis2Lamp.Text.Trim();
                cDeviceManager._teachings_plc.OriginCompleteAxis3 = txtOriginCompleteAxis3.Text.Trim();
                cDeviceManager._teachings_plc.OriginCompleteAxis3Lamp = txtOriginCompleteAxis3Lamp.Text.Trim();
                cDeviceManager._teachings_plc.OriginCompleteAxis4 = txtOriginCompleteAxis4.Text.Trim();
                cDeviceManager._teachings_plc.OriginCompleteAxis4Lamp = txtOriginCompleteAxis4Lamp.Text.Trim();
                #endregion

                _mySQLHelper.Update<register_teachings_plc>(cDeviceManager._teachings_plc);
                MessageBox.Show("Save Manual PLC data successfull!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Save Manual PLC data fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        bool ValidateDataManualPLC()
        {
            #region Validation Jog Axis
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis1.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis1Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 1 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis2.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis3.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis4.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogPlusAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Plus Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtJogMinusAxis1.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis1Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 1 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis2.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis3.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis4.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogMinusAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for Jog Minus Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtJogSpeedAxis1.Text))
            {
                MessageBox.Show("Please enter value for Jog Speed Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogSpeedAxis2.Text))
            {
                MessageBox.Show("Please enter value for Jog Speed Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogSpeedAxis3.Text))
            {
                MessageBox.Show("Please enter value for Jog Speed Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtJogSpeedAxis4.Text))
            {
                MessageBox.Show("Please enter value for Jog Speed Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            #endregion

            #region Validation - Manual Axis
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis1.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis1Lamp.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 1 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis2.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis3.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis4.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMARunPositionAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for MARunPosition Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis1.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis1Lamp.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 1 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis2.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis3.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis4.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMAStopPositionAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for MAStopPosition Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionRunAxis1.Text))
            {
                MessageBox.Show("Please enter value for Position Run Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionRunAxis2.Text))
            {
                MessageBox.Show("Please enter value for Position Run Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionRunAxis3.Text))
            {
                MessageBox.Show("Please enter value for Position Run Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionRunAxis4.Text))
            {
                MessageBox.Show("Please enter value for Position Run Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionCurentAxis1.Text))
            {
                MessageBox.Show("Please enter value for Current Position Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionCurentAxis2.Text))
            {
                MessageBox.Show("Please enter value for Current Position Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionCurentAxis3.Text))
            {
                MessageBox.Show("Please enter value for Current Position Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPositionCurentAxis4.Text))
            {
                MessageBox.Show("Please enter value for Current Position Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            #endregion

            #region Validation - Origin Axis
            if (string.IsNullOrWhiteSpace(txtOriginAxis2.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginAxis3.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginAxis4.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis2.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis2Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 2 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis3.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis3Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 3 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis4.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOriginCompleteAxis4Lamp.Text))
            {
                MessageBox.Show("Please enter value for Origin Complete Axis 4 Lamp!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            #endregion

            return true;
        }

        bool ValidateDataTeachingAlarm()
        {
            #region Validation - TEACHING
            // Axis 1
            if (string.IsNullOrWhiteSpace(txtOffsetcheckvision_Speed_1.Text))
            {
                MessageBox.Show("Please enter value for Offset Check Vision Speed Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRotateMinus_Speed_1.Text))
            {
                MessageBox.Show("Please enter value for Rotate Minus 90 Speed Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRotatePlus_Speed_1.Text))
            {
                MessageBox.Show("Please enter value for Rotate 90 Speed Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_Position_1.Text))
            {
                MessageBox.Show("Please enter value for Grinding Position Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_Speed_1.Text))
            {
                MessageBox.Show("Please enter value for Grinding Speed Axis 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Axis 2
            if (string.IsNullOrWhiteSpace(txtReady_Position_2.Text))
            {
                MessageBox.Show("Please enter value for Ready Position Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtReady_Speed_2.Text))
            {
                MessageBox.Show("Please enter value for Ready Speed Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtConcentrick_Position_2.Text))
            {
                MessageBox.Show("Please enter value for Start Concentric Position Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtConcentrick_Speed_2.Text))
            {
                MessageBox.Show("Please enter value for Start Concentric Speed Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtConcentrick_Adjustment_Position_2.Text))
            {
                MessageBox.Show("Please enter value for Concentric Adjustment Position Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtConcentrick_Adjustment_Speed_2.Text))
            {
                MessageBox.Show("Please enter value for Concentric Adjustment Speed Axis 2!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Axis 3
            if (string.IsNullOrWhiteSpace(txtReady_Position_3.Text))
            {
                MessageBox.Show("Please enter value for Ready Position Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtReady_Speed_3.Text))
            {
                MessageBox.Show("Please enter value for Ready Speed Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_Position_3.Text))
            {
                MessageBox.Show("Please enter value for Start Grinding Position Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_Speed_3.Text))
            {
                MessageBox.Show("Please enter value for Start Grinding Speed Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOffset_Grinding_Position_3.Text))
            {
                MessageBox.Show("Please enter value for Offset Grinding Position Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtOffset_Grinding_Speed_3.Text))
            {
                MessageBox.Show("Please enter value for Offset Grinding Speed Axis 3!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Axis 4
            if (string.IsNullOrWhiteSpace(txtGrinding_Position_4.Text))
            {
                MessageBox.Show("Please enter value for Grinding Position Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_Speed_4.Text))
            {
                MessageBox.Show("Please enter value for Grinding Speed Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_R_Position_4.Text))
            {
                MessageBox.Show("Please enter value for Manual Grinding Right Position Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_R_Speed_4.Text))
            {
                MessageBox.Show("Please enter value for Manual Grinding Right Speed Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_L_Position_4.Text))
            {
                MessageBox.Show("Please enter value for Manual Grinding Left Position Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGrinding_L_Speed_4.Text))
            {
                MessageBox.Show("Please enter value for Manual Grinding Left Speed Axis 4!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            #endregion

            #region Validation - ALARM
            if (string.IsNullOrWhiteSpace(txtInventorOnGet.Text))
            {
                MessageBox.Show("Please enter value for Inventor On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtInventorOnSet.Text))
            {
                MessageBox.Show("Please enter value for Set Inventor On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtInventorOffGet.Text))
            {
                MessageBox.Show("Please enter value for Inventor Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtInventorOffSet.Text))
            {
                MessageBox.Show("Please enter value for Set Inventor Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowStartOnGet.Text))
            {
                MessageBox.Show("Please enter value for Air Flow Start On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowStartOnSet.Text))
            {
                MessageBox.Show("Please enter value for Set Air Flow Start On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowStartOffGet.Text))
            {
                MessageBox.Show("Please enter value for Air Flow Start Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowStartOffSet.Text))
            {
                MessageBox.Show("Please enter value for Set Air Flow Start Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowEndOnGet.Text))
            {
                MessageBox.Show("Please enter value for Air Flow End On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowEndOnSet.Text))
            {
                MessageBox.Show("Please enter value for Set Air Flow End On!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowEndOffGet.Text))
            {
                MessageBox.Show("Please enter value for Air Flow End Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAirFlowEndOffSet.Text))
            {
                MessageBox.Show("Please enter value for Set Air Flow End Off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            #endregion

            return true;
        }


    }
}
