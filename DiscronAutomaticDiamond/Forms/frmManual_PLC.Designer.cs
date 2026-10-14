using System.Drawing;
using System.Windows.Forms;

namespace AutoBrutingDiamond
{
    partial class frmManual_PLC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManual_PLC));
            this.pShowManualDevice = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPositionCurrentAxis4Set = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnInventorOn = new System.Windows.Forms.Button();
            this.btnInventorOff = new System.Windows.Forms.Button();
            this.btnPositionRunAxis4Set = new System.Windows.Forms.Button();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.btnAirFlowStart_On = new System.Windows.Forms.Button();
            this.btnAirFlowStart_Off = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis4Get = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnAirFlowEnd_On = new System.Windows.Forms.Button();
            this.btnAirFlowEnd_Off = new System.Windows.Forms.Button();
            this.btnPositionRunAxis4Get = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnOriginExis4 = new System.Windows.Forms.Button();
            this.btnOriginCompleteExis4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnOriginExis3 = new System.Windows.Forms.Button();
            this.btnOriginCompleteExis3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtPositionCurrentAxis4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPositionRunAxis4 = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnSet_ManualPositionRun4 = new System.Windows.Forms.Button();
            this.btnSet_ManualPositionStop4 = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis2Set = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.btnOriginExis2 = new System.Windows.Forms.Button();
            this.btnOriginCompleteExis2 = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis3Set = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSet_JogPlusAxis1 = new System.Windows.Forms.Button();
            this.btnSet_JogMinusAxis1 = new System.Windows.Forms.Button();
            this.btnPositionRunAxis2Set = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnSet_ManualPositionRun3 = new System.Windows.Forms.Button();
            this.btnSet_ManualPositionStop3 = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis1Set = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSet_JogPlusAxis2 = new System.Windows.Forms.Button();
            this.btnSet_JogMinusAxis2 = new System.Windows.Forms.Button();
            this.btnPositionRunAxis3Set = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSet_ManualPositionRun2 = new System.Windows.Forms.Button();
            this.btnSet_ManualPositionStop2 = new System.Windows.Forms.Button();
            this.btnPositionRunAxis1Set = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSet_JogPlusAxis3 = new System.Windows.Forms.Button();
            this.btnSet_JogMinusAxis3 = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis2Get = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis3Get = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSet_ManualPositionRun1 = new System.Windows.Forms.Button();
            this.btnSet_ManualPositionStop1 = new System.Windows.Forms.Button();
            this.btnPositionRunAxis2Get = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSet_JogPlusAxis4 = new System.Windows.Forms.Button();
            this.btnSet_JogMinusAxis4 = new System.Windows.Forms.Button();
            this.btnPositionCurrentAxis1Get = new System.Windows.Forms.Button();
            this.txtJogSpeedAxis3 = new System.Windows.Forms.TextBox();
            this.btnPositionRunAxis3Get = new System.Windows.Forms.Button();
            this.txtJogSpeedAxis1 = new System.Windows.Forms.TextBox();
            this.btnPositionRunAxis1Get = new System.Windows.Forms.Button();
            this.txtJogSpeedAxis2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnJogSpeedAxis3Get = new System.Windows.Forms.Button();
            this.txtPositionCurrentAxis2 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis1Get = new System.Windows.Forms.Button();
            this.txtPositionCurrentAxis3 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis2Get = new System.Windows.Forms.Button();
            this.txtPositionRunAxis2 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis3Set = new System.Windows.Forms.Button();
            this.txtPositionRunAxis3 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis1Set = new System.Windows.Forms.Button();
            this.txtPositionCurrentAxis1 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis2Set = new System.Windows.Forms.Button();
            this.txtPositionRunAxis1 = new System.Windows.Forms.TextBox();
            this.txtJogSpeedAxis4 = new System.Windows.Forms.TextBox();
            this.btnJogSpeedAxis4Set = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btnJogSpeedAxis4Get = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pTeaching = new System.Windows.Forms.Panel();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.btnSet_Grinding_L_Speed_4 = new System.Windows.Forms.Button();
            this.txtGrinding_L_Speed_4 = new System.Windows.Forms.TextBox();
            this.btnGet_Grinding_L_Speed_4 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSet_Grinding_L_Position_4 = new System.Windows.Forms.Button();
            this.txtGrinding_L_Position_4 = new System.Windows.Forms.TextBox();
            this.btnGet_Grinding_L_Position_4 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnSet_Grinding_R_Speed_4 = new System.Windows.Forms.Button();
            this.btnGet_Grinding_R_Position_4 = new System.Windows.Forms.Button();
            this.txtGrinding_R_Position_4 = new System.Windows.Forms.TextBox();
            this.btnSet_Grinding_R_Position_4 = new System.Windows.Forms.Button();
            this.txtGrinding_R_Speed_4 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnGet_Grinding_R_Speed_4 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.btnSet_Grinding_Speed_4 = new System.Windows.Forms.Button();
            this.btnGet_Grinding_Position_4 = new System.Windows.Forms.Button();
            this.txtGrinding_Position_4 = new System.Windows.Forms.TextBox();
            this.btnSet_Grinding_Position_4 = new System.Windows.Forms.Button();
            this.txtGrinding_Speed_4 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnGet_Grinding_Speed_4 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.btnSet_Offset_Grinding_Speed_3 = new System.Windows.Forms.Button();
            this.txtOffset_Grinding_Speed_3 = new System.Windows.Forms.TextBox();
            this.btnGet_Offset_Grinding_Speed_3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSet_Offset_Grinding_Position_3 = new System.Windows.Forms.Button();
            this.txtOffset_Grinding_Position_3 = new System.Windows.Forms.TextBox();
            this.btnGet_Offset_Grinding_Position_3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSet_Grinding_Speed_3 = new System.Windows.Forms.Button();
            this.btnGet_Grinding_Position_3 = new System.Windows.Forms.Button();
            this.txtGrinding_Position_3 = new System.Windows.Forms.TextBox();
            this.btnSet_Grinding_Position_3 = new System.Windows.Forms.Button();
            this.txtGrinding_Speed_3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnGet_Grinding_Speed_3 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSet_Ready_Speed_3 = new System.Windows.Forms.Button();
            this.btnGet_Ready_Position_3 = new System.Windows.Forms.Button();
            this.txtReady_Position_3 = new System.Windows.Forms.TextBox();
            this.btnSet_Ready_Position_3 = new System.Windows.Forms.Button();
            this.txtReady_Speed_3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnGet_Ready_Speed_3 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btnSet_Concentrick_Adjustment_Speed_2 = new System.Windows.Forms.Button();
            this.txtConcentrick_Adjustment_Speed_2 = new System.Windows.Forms.TextBox();
            this.btnGet_Concentrick_Adjustment_Speed_2 = new System.Windows.Forms.Button();
            this.btnSet_Concentrick_Adjustment_2 = new System.Windows.Forms.Button();
            this.txtConcentrick_Adjustment_Position_2 = new System.Windows.Forms.TextBox();
            this.btnGet_Concentrick_Adjustment_2 = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.btnSet_Concentrick_Speed_2 = new System.Windows.Forms.Button();
            this.btnGet_Concentrick_Position_2 = new System.Windows.Forms.Button();
            this.txtConcentrick_Position_2 = new System.Windows.Forms.TextBox();
            this.btnSet_Concentrick_Position_2 = new System.Windows.Forms.Button();
            this.txtConcentrick_Speed_2 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.btnGet_Concentrick_Speed_2 = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.btnSet_Ready_Speed_2 = new System.Windows.Forms.Button();
            this.btnGet_Ready_Position_2 = new System.Windows.Forms.Button();
            this.txtReady_Position_2 = new System.Windows.Forms.TextBox();
            this.btnSet_Ready_Position_2 = new System.Windows.Forms.Button();
            this.txtReady_Speed_2 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnGet_Ready_Speed_2 = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.btnCancelTeaching = new System.Windows.Forms.Button();
            this.label131 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnSet_Grinding_Speed_1 = new System.Windows.Forms.Button();
            this.txtGrinding_Speed_1 = new System.Windows.Forms.TextBox();
            this.btnGet_Grinding_Speed_1 = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.btnSet_Grinding_Position_1 = new System.Windows.Forms.Button();
            this.btnGet_RotateMinus_1 = new System.Windows.Forms.Button();
            this.txtRotateMinus_1 = new System.Windows.Forms.TextBox();
            this.btnSet_RotateMinus_1 = new System.Windows.Forms.Button();
            this.txtGrinding_Position_1 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.btnGet_Grinding_Position_1 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.btnSet_RotatePlus_1 = new System.Windows.Forms.Button();
            this.btnGet_Offsetcheckvision_1 = new System.Windows.Forms.Button();
            this.txtOffsetcheckvision_Speed_1 = new System.Windows.Forms.TextBox();
            this.btnSet_Offsetcheckvision_1 = new System.Windows.Forms.Button();
            this.txtRotatePlus_Speed_1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGet_RotatePlus_1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.btnSaveDeviceInfor = new System.Windows.Forms.Button();
            this.txtPlcPort = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtPlcIpAddress = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.btnCloseManualDevice = new System.Windows.Forms.Button();
            this.pShowManualDevice.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pTeaching.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.SuspendLayout();
            // 
            // pShowManualDevice
            // 
            this.pShowManualDevice.BackColor = System.Drawing.Color.Transparent;
            this.pShowManualDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pShowManualDevice.Controls.Add(this.tabControl1);
            this.pShowManualDevice.Controls.Add(this.btnCloseManualDevice);
            this.pShowManualDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pShowManualDevice.Location = new System.Drawing.Point(0, 0);
            this.pShowManualDevice.Margin = new System.Windows.Forms.Padding(4);
            this.pShowManualDevice.Name = "pShowManualDevice";
            this.pShowManualDevice.Size = new System.Drawing.Size(1234, 752);
            this.pShowManualDevice.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(107, 50);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1232, 750);
            this.tabControl1.TabIndex = 104;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 54);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1224, 692);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Manual PLC";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPositionCurrentAxis4Set);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.btnPositionRunAxis4Set);
            this.panel2.Controls.Add(this.groupBox18);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis4Get);
            this.panel2.Controls.Add(this.groupBox13);
            this.panel2.Controls.Add(this.btnPositionRunAxis4Get);
            this.panel2.Controls.Add(this.groupBox11);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox10);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.txtPositionCurrentAxis4);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtPositionRunAxis4);
            this.panel2.Controls.Add(this.groupBox9);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis2Set);
            this.panel2.Controls.Add(this.groupBox14);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis3Set);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnPositionRunAxis2Set);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis1Set);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.btnPositionRunAxis3Set);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.btnPositionRunAxis1Set);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis2Get);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis3Get);
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.btnPositionRunAxis2Get);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.btnPositionCurrentAxis1Get);
            this.panel2.Controls.Add(this.txtJogSpeedAxis3);
            this.panel2.Controls.Add(this.btnPositionRunAxis3Get);
            this.panel2.Controls.Add(this.txtJogSpeedAxis1);
            this.panel2.Controls.Add(this.btnPositionRunAxis1Get);
            this.panel2.Controls.Add(this.txtJogSpeedAxis2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label133);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label145);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label136);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label139);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label142);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.btnJogSpeedAxis3Get);
            this.panel2.Controls.Add(this.txtPositionCurrentAxis2);
            this.panel2.Controls.Add(this.btnJogSpeedAxis1Get);
            this.panel2.Controls.Add(this.txtPositionCurrentAxis3);
            this.panel2.Controls.Add(this.btnJogSpeedAxis2Get);
            this.panel2.Controls.Add(this.txtPositionRunAxis2);
            this.panel2.Controls.Add(this.btnJogSpeedAxis3Set);
            this.panel2.Controls.Add(this.txtPositionRunAxis3);
            this.panel2.Controls.Add(this.btnJogSpeedAxis1Set);
            this.panel2.Controls.Add(this.txtPositionCurrentAxis1);
            this.panel2.Controls.Add(this.btnJogSpeedAxis2Set);
            this.panel2.Controls.Add(this.txtPositionRunAxis1);
            this.panel2.Controls.Add(this.txtJogSpeedAxis4);
            this.panel2.Controls.Add(this.btnJogSpeedAxis4Set);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.btnJogSpeedAxis4Get);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1218, 686);
            this.panel2.TabIndex = 103;
            // 
            // btnPositionCurrentAxis4Set
            // 
            this.btnPositionCurrentAxis4Set.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis4Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis4Set.Location = new System.Drawing.Point(790, 1055);
            this.btnPositionCurrentAxis4Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis4Set.Name = "btnPositionCurrentAxis4Set";
            this.btnPositionCurrentAxis4Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis4Set.TabIndex = 175;
            this.btnPositionCurrentAxis4Set.Text = "Set";
            this.btnPositionCurrentAxis4Set.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis4Set.Click += new System.EventHandler(this.btnPositionCurrentAxis4Set_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnInventorOn);
            this.groupBox5.Controls.Add(this.btnInventorOff);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox5.Location = new System.Drawing.Point(4, 52);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(185, 124);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "INVENTOR";
            // 
            // btnInventorOn
            // 
            this.btnInventorOn.BackColor = System.Drawing.Color.White;
            this.btnInventorOn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnInventorOn.Location = new System.Drawing.Point(8, 24);
            this.btnInventorOn.Margin = new System.Windows.Forms.Padding(4);
            this.btnInventorOn.Name = "btnInventorOn";
            this.btnInventorOn.Size = new System.Drawing.Size(168, 42);
            this.btnInventorOn.TabIndex = 0;
            this.btnInventorOn.Text = " ON";
            this.btnInventorOn.UseVisualStyleBackColor = false;
            this.btnInventorOn.Click += new System.EventHandler(this.btnInventorOn_Click);
            // 
            // btnInventorOff
            // 
            this.btnInventorOff.BackColor = System.Drawing.Color.White;
            this.btnInventorOff.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnInventorOff.Location = new System.Drawing.Point(8, 74);
            this.btnInventorOff.Margin = new System.Windows.Forms.Padding(4);
            this.btnInventorOff.Name = "btnInventorOff";
            this.btnInventorOff.Size = new System.Drawing.Size(168, 42);
            this.btnInventorOff.TabIndex = 1;
            this.btnInventorOff.Text = "OFF";
            this.btnInventorOff.UseVisualStyleBackColor = false;
            this.btnInventorOff.Click += new System.EventHandler(this.btnInventorOff_Click);
            // 
            // btnPositionRunAxis4Set
            // 
            this.btnPositionRunAxis4Set.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis4Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis4Set.Location = new System.Drawing.Point(1145, 533);
            this.btnPositionRunAxis4Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis4Set.Name = "btnPositionRunAxis4Set";
            this.btnPositionRunAxis4Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis4Set.TabIndex = 176;
            this.btnPositionRunAxis4Set.Text = "Set";
            this.btnPositionRunAxis4Set.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis4Set.Click += new System.EventHandler(this.btnPositionRunAxis4Set_Click);
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.btnAirFlowStart_On);
            this.groupBox18.Controls.Add(this.btnAirFlowStart_Off);
            this.groupBox18.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox18.Location = new System.Drawing.Point(197, 52);
            this.groupBox18.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox18.Size = new System.Drawing.Size(185, 124);
            this.groupBox18.TabIndex = 6;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "AIR FLOW START";
            // 
            // btnAirFlowStart_On
            // 
            this.btnAirFlowStart_On.BackColor = System.Drawing.Color.White;
            this.btnAirFlowStart_On.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAirFlowStart_On.Location = new System.Drawing.Point(8, 24);
            this.btnAirFlowStart_On.Margin = new System.Windows.Forms.Padding(4);
            this.btnAirFlowStart_On.Name = "btnAirFlowStart_On";
            this.btnAirFlowStart_On.Size = new System.Drawing.Size(168, 42);
            this.btnAirFlowStart_On.TabIndex = 1;
            this.btnAirFlowStart_On.Text = "ON";
            this.btnAirFlowStart_On.UseVisualStyleBackColor = false;
            this.btnAirFlowStart_On.Click += new System.EventHandler(this.btnAirFlowStart_On_Click);
            // 
            // btnAirFlowStart_Off
            // 
            this.btnAirFlowStart_Off.BackColor = System.Drawing.Color.White;
            this.btnAirFlowStart_Off.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAirFlowStart_Off.Location = new System.Drawing.Point(8, 74);
            this.btnAirFlowStart_Off.Margin = new System.Windows.Forms.Padding(4);
            this.btnAirFlowStart_Off.Name = "btnAirFlowStart_Off";
            this.btnAirFlowStart_Off.Size = new System.Drawing.Size(168, 42);
            this.btnAirFlowStart_Off.TabIndex = 2;
            this.btnAirFlowStart_Off.Text = "OFF";
            this.btnAirFlowStart_Off.UseVisualStyleBackColor = false;
            this.btnAirFlowStart_Off.Click += new System.EventHandler(this.btnAirFlowStart_Off_Click);
            // 
            // btnPositionCurrentAxis4Get
            // 
            this.btnPositionCurrentAxis4Get.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis4Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis4Get.Location = new System.Drawing.Point(729, 1055);
            this.btnPositionCurrentAxis4Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis4Get.Name = "btnPositionCurrentAxis4Get";
            this.btnPositionCurrentAxis4Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis4Get.TabIndex = 178;
            this.btnPositionCurrentAxis4Get.Text = "Get";
            this.btnPositionCurrentAxis4Get.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis4Get.Click += new System.EventHandler(this.btnPositionCurrentAxis4Get_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnAirFlowEnd_On);
            this.groupBox13.Controls.Add(this.btnAirFlowEnd_Off);
            this.groupBox13.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox13.Location = new System.Drawing.Point(4, 184);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox13.Size = new System.Drawing.Size(185, 124);
            this.groupBox13.TabIndex = 6;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "AIR FLOW END";
            // 
            // btnAirFlowEnd_On
            // 
            this.btnAirFlowEnd_On.BackColor = System.Drawing.Color.White;
            this.btnAirFlowEnd_On.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAirFlowEnd_On.Location = new System.Drawing.Point(8, 24);
            this.btnAirFlowEnd_On.Margin = new System.Windows.Forms.Padding(4);
            this.btnAirFlowEnd_On.Name = "btnAirFlowEnd_On";
            this.btnAirFlowEnd_On.Size = new System.Drawing.Size(168, 42);
            this.btnAirFlowEnd_On.TabIndex = 3;
            this.btnAirFlowEnd_On.Text = "ON";
            this.btnAirFlowEnd_On.UseVisualStyleBackColor = false;
            this.btnAirFlowEnd_On.Click += new System.EventHandler(this.btnAirFlowEnd_On_Click);
            // 
            // btnAirFlowEnd_Off
            // 
            this.btnAirFlowEnd_Off.BackColor = System.Drawing.Color.White;
            this.btnAirFlowEnd_Off.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAirFlowEnd_Off.Location = new System.Drawing.Point(8, 74);
            this.btnAirFlowEnd_Off.Margin = new System.Windows.Forms.Padding(4);
            this.btnAirFlowEnd_Off.Name = "btnAirFlowEnd_Off";
            this.btnAirFlowEnd_Off.Size = new System.Drawing.Size(168, 42);
            this.btnAirFlowEnd_Off.TabIndex = 3;
            this.btnAirFlowEnd_Off.Text = "OFF";
            this.btnAirFlowEnd_Off.UseVisualStyleBackColor = false;
            this.btnAirFlowEnd_Off.Click += new System.EventHandler(this.btnAirFlowEnd_Off_Click);
            // 
            // btnPositionRunAxis4Get
            // 
            this.btnPositionRunAxis4Get.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis4Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis4Get.Location = new System.Drawing.Point(1084, 533);
            this.btnPositionRunAxis4Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis4Get.Name = "btnPositionRunAxis4Get";
            this.btnPositionRunAxis4Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis4Get.TabIndex = 179;
            this.btnPositionRunAxis4Get.Text = "Get";
            this.btnPositionRunAxis4Get.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis4Get.Click += new System.EventHandler(this.btnPositionRunAxis4Get_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox11.Controls.Add(this.btnOriginExis4);
            this.groupBox11.Controls.Add(this.btnOriginCompleteExis4);
            this.groupBox11.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox11.Location = new System.Drawing.Point(583, 318);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox11.Size = new System.Drawing.Size(185, 122);
            this.groupBox11.TabIndex = 113;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "ORIGIN EXIS #4";
            // 
            // btnOriginExis4
            // 
            this.btnOriginExis4.BackColor = System.Drawing.Color.White;
            this.btnOriginExis4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginExis4.ForeColor = System.Drawing.Color.Black;
            this.btnOriginExis4.Location = new System.Drawing.Point(8, 24);
            this.btnOriginExis4.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginExis4.Name = "btnOriginExis4";
            this.btnOriginExis4.Size = new System.Drawing.Size(168, 42);
            this.btnOriginExis4.TabIndex = 0;
            this.btnOriginExis4.Text = "ORIGIN";
            this.btnOriginExis4.UseVisualStyleBackColor = false;
            this.btnOriginExis4.Click += new System.EventHandler(this.btnOriginExis4_Click);
            // 
            // btnOriginCompleteExis4
            // 
            this.btnOriginCompleteExis4.BackColor = System.Drawing.Color.White;
            this.btnOriginCompleteExis4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginCompleteExis4.ForeColor = System.Drawing.Color.Black;
            this.btnOriginCompleteExis4.Location = new System.Drawing.Point(8, 74);
            this.btnOriginCompleteExis4.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginCompleteExis4.Name = "btnOriginCompleteExis4";
            this.btnOriginCompleteExis4.Size = new System.Drawing.Size(168, 42);
            this.btnOriginCompleteExis4.TabIndex = 1;
            this.btnOriginCompleteExis4.Text = "COMPLETE";
            this.btnOriginCompleteExis4.UseVisualStyleBackColor = false;
            this.btnOriginCompleteExis4.Click += new System.EventHandler(this.btnOriginCompleteExis4_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(420, 1060);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 22);
            this.label2.TabIndex = 169;
            this.label2.Text = "Postion Current Axis#4";
            // 
            // groupBox10
            // 
            this.groupBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox10.Controls.Add(this.btnOriginExis3);
            this.groupBox10.Controls.Add(this.btnOriginCompleteExis3);
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox10.Location = new System.Drawing.Point(390, 318);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox10.Size = new System.Drawing.Size(185, 124);
            this.groupBox10.TabIndex = 113;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "ORIGIN EXIS #3";
            // 
            // btnOriginExis3
            // 
            this.btnOriginExis3.BackColor = System.Drawing.Color.White;
            this.btnOriginExis3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginExis3.ForeColor = System.Drawing.Color.Black;
            this.btnOriginExis3.Location = new System.Drawing.Point(8, 24);
            this.btnOriginExis3.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginExis3.Name = "btnOriginExis3";
            this.btnOriginExis3.Size = new System.Drawing.Size(168, 42);
            this.btnOriginExis3.TabIndex = 0;
            this.btnOriginExis3.Text = "ORIGIN";
            this.btnOriginExis3.UseVisualStyleBackColor = false;
            this.btnOriginExis3.Click += new System.EventHandler(this.btnOriginExis3_Click);
            // 
            // btnOriginCompleteExis3
            // 
            this.btnOriginCompleteExis3.BackColor = System.Drawing.Color.White;
            this.btnOriginCompleteExis3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginCompleteExis3.ForeColor = System.Drawing.Color.Black;
            this.btnOriginCompleteExis3.Location = new System.Drawing.Point(8, 74);
            this.btnOriginCompleteExis3.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginCompleteExis3.Name = "btnOriginCompleteExis3";
            this.btnOriginCompleteExis3.Size = new System.Drawing.Size(168, 42);
            this.btnOriginCompleteExis3.TabIndex = 1;
            this.btnOriginCompleteExis3.Text = "COMPLETE";
            this.btnOriginCompleteExis3.UseVisualStyleBackColor = false;
            this.btnOriginCompleteExis3.Click += new System.EventHandler(this.btnOriginCompleteExis3_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(775, 538);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 22);
            this.label3.TabIndex = 170;
            this.label3.Text = "Position Run Axis#4";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1666, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 25);
            this.button3.TabIndex = 63;
            // 
            // txtPositionCurrentAxis4
            // 
            this.txtPositionCurrentAxis4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionCurrentAxis4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionCurrentAxis4.Location = new System.Drawing.Point(606, 1058);
            this.txtPositionCurrentAxis4.Name = "txtPositionCurrentAxis4";
            this.txtPositionCurrentAxis4.Size = new System.Drawing.Size(112, 27);
            this.txtPositionCurrentAxis4.TabIndex = 172;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1216, 44);
            this.label4.TabIndex = 0;
            this.label4.Text = "MANUAL PLC";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPositionRunAxis4
            // 
            this.txtPositionRunAxis4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionRunAxis4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionRunAxis4.Location = new System.Drawing.Point(961, 536);
            this.txtPositionRunAxis4.Name = "txtPositionRunAxis4";
            this.txtPositionRunAxis4.Size = new System.Drawing.Size(112, 27);
            this.txtPositionRunAxis4.TabIndex = 173;
            // 
            // groupBox9
            // 
            this.groupBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox9.Controls.Add(this.btnSet_ManualPositionRun4);
            this.groupBox9.Controls.Add(this.btnSet_ManualPositionStop4);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox9.Location = new System.Drawing.Point(583, 55);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox9.Size = new System.Drawing.Size(185, 122);
            this.groupBox9.TabIndex = 111;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "MANUAL POSITION #4";
            // 
            // btnSet_ManualPositionRun4
            // 
            this.btnSet_ManualPositionRun4.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionRun4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionRun4.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionRun4.Location = new System.Drawing.Point(8, 24);
            this.btnSet_ManualPositionRun4.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionRun4.Name = "btnSet_ManualPositionRun4";
            this.btnSet_ManualPositionRun4.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionRun4.TabIndex = 0;
            this.btnSet_ManualPositionRun4.Text = "RUN";
            this.btnSet_ManualPositionRun4.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionRun4.Click += new System.EventHandler(this.btnSet_ManualPositionRun4_Click);
            // 
            // btnSet_ManualPositionStop4
            // 
            this.btnSet_ManualPositionStop4.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionStop4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionStop4.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionStop4.Location = new System.Drawing.Point(8, 74);
            this.btnSet_ManualPositionStop4.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionStop4.Name = "btnSet_ManualPositionStop4";
            this.btnSet_ManualPositionStop4.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionStop4.TabIndex = 1;
            this.btnSet_ManualPositionStop4.Text = "STOP";
            this.btnSet_ManualPositionStop4.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionStop4.Click += new System.EventHandler(this.btnSet_ManualPositionStop4_Click);
            // 
            // btnPositionCurrentAxis2Set
            // 
            this.btnPositionCurrentAxis2Set.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis2Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis2Set.Location = new System.Drawing.Point(1145, 404);
            this.btnPositionCurrentAxis2Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis2Set.Name = "btnPositionCurrentAxis2Set";
            this.btnPositionCurrentAxis2Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis2Set.TabIndex = 161;
            this.btnPositionCurrentAxis2Set.Text = "Set";
            this.btnPositionCurrentAxis2Set.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis2Set.Click += new System.EventHandler(this.btnPositionCurrentAxis2Set_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox14.Controls.Add(this.btnOriginExis2);
            this.groupBox14.Controls.Add(this.btnOriginCompleteExis2);
            this.groupBox14.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox14.Location = new System.Drawing.Point(583, 186);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox14.Size = new System.Drawing.Size(185, 124);
            this.groupBox14.TabIndex = 112;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "ORIGIN EXIS #2";
            // 
            // btnOriginExis2
            // 
            this.btnOriginExis2.BackColor = System.Drawing.Color.White;
            this.btnOriginExis2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginExis2.ForeColor = System.Drawing.Color.Black;
            this.btnOriginExis2.Location = new System.Drawing.Point(8, 24);
            this.btnOriginExis2.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginExis2.Name = "btnOriginExis2";
            this.btnOriginExis2.Size = new System.Drawing.Size(168, 42);
            this.btnOriginExis2.TabIndex = 0;
            this.btnOriginExis2.Text = "ORIGIN";
            this.btnOriginExis2.UseVisualStyleBackColor = false;
            this.btnOriginExis2.Click += new System.EventHandler(this.btnOriginExis2_Click);
            // 
            // btnOriginCompleteExis2
            // 
            this.btnOriginCompleteExis2.BackColor = System.Drawing.Color.White;
            this.btnOriginCompleteExis2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnOriginCompleteExis2.ForeColor = System.Drawing.Color.Black;
            this.btnOriginCompleteExis2.Location = new System.Drawing.Point(8, 74);
            this.btnOriginCompleteExis2.Margin = new System.Windows.Forms.Padding(4);
            this.btnOriginCompleteExis2.Name = "btnOriginCompleteExis2";
            this.btnOriginCompleteExis2.Size = new System.Drawing.Size(168, 42);
            this.btnOriginCompleteExis2.TabIndex = 1;
            this.btnOriginCompleteExis2.Text = "COMPLETE";
            this.btnOriginCompleteExis2.UseVisualStyleBackColor = false;
            this.btnOriginCompleteExis2.Click += new System.EventHandler(this.btnOriginCompleteExis2_Click);
            // 
            // btnPositionCurrentAxis3Set
            // 
            this.btnPositionCurrentAxis3Set.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis3Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis3Set.Location = new System.Drawing.Point(1145, 490);
            this.btnPositionCurrentAxis3Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis3Set.Name = "btnPositionCurrentAxis3Set";
            this.btnPositionCurrentAxis3Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis3Set.TabIndex = 158;
            this.btnPositionCurrentAxis3Set.Text = "Set";
            this.btnPositionCurrentAxis3Set.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis3Set.Click += new System.EventHandler(this.btnPositionCurrentAxis3Set_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.btnSet_JogPlusAxis1);
            this.groupBox1.Controls.Add(this.btnSet_JogMinusAxis1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(4, 316);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(185, 124);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "JOG EXIS #1";
            // 
            // btnSet_JogPlusAxis1
            // 
            this.btnSet_JogPlusAxis1.BackColor = System.Drawing.Color.White;
            this.btnSet_JogPlusAxis1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogPlusAxis1.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogPlusAxis1.Location = new System.Drawing.Point(8, 24);
            this.btnSet_JogPlusAxis1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogPlusAxis1.Name = "btnSet_JogPlusAxis1";
            this.btnSet_JogPlusAxis1.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogPlusAxis1.TabIndex = 0;
            this.btnSet_JogPlusAxis1.Text = "JOG +";
            this.btnSet_JogPlusAxis1.UseVisualStyleBackColor = false;
            this.btnSet_JogPlusAxis1.Click += new System.EventHandler(this.btnJogPlusAxis1Set_Click);
            // 
            // btnSet_JogMinusAxis1
            // 
            this.btnSet_JogMinusAxis1.BackColor = System.Drawing.Color.White;
            this.btnSet_JogMinusAxis1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogMinusAxis1.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogMinusAxis1.Location = new System.Drawing.Point(8, 74);
            this.btnSet_JogMinusAxis1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogMinusAxis1.Name = "btnSet_JogMinusAxis1";
            this.btnSet_JogMinusAxis1.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogMinusAxis1.TabIndex = 1;
            this.btnSet_JogMinusAxis1.Text = "JOG -";
            this.btnSet_JogMinusAxis1.UseVisualStyleBackColor = false;
            this.btnSet_JogMinusAxis1.Click += new System.EventHandler(this.btnSet_JogMinusAxis1_Click);
            // 
            // btnPositionRunAxis2Set
            // 
            this.btnPositionRunAxis2Set.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis2Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis2Set.Location = new System.Drawing.Point(1145, 361);
            this.btnPositionRunAxis2Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis2Set.Name = "btnPositionRunAxis2Set";
            this.btnPositionRunAxis2Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis2Set.TabIndex = 159;
            this.btnPositionRunAxis2Set.Text = "Set";
            this.btnPositionRunAxis2Set.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis2Set.Click += new System.EventHandler(this.btnPositionRunAxis2Set_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox8.Controls.Add(this.btnSet_ManualPositionRun3);
            this.groupBox8.Controls.Add(this.btnSet_ManualPositionStop3);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox8.Location = new System.Drawing.Point(197, 448);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(185, 122);
            this.groupBox8.TabIndex = 110;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "MANUAL POSITION #3";
            // 
            // btnSet_ManualPositionRun3
            // 
            this.btnSet_ManualPositionRun3.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionRun3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionRun3.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionRun3.Location = new System.Drawing.Point(8, 24);
            this.btnSet_ManualPositionRun3.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionRun3.Name = "btnSet_ManualPositionRun3";
            this.btnSet_ManualPositionRun3.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionRun3.TabIndex = 0;
            this.btnSet_ManualPositionRun3.Text = "RUN";
            this.btnSet_ManualPositionRun3.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionRun3.Click += new System.EventHandler(this.btnSet_ManualPositionRun3_Click);
            // 
            // btnSet_ManualPositionStop3
            // 
            this.btnSet_ManualPositionStop3.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionStop3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionStop3.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionStop3.Location = new System.Drawing.Point(8, 74);
            this.btnSet_ManualPositionStop3.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionStop3.Name = "btnSet_ManualPositionStop3";
            this.btnSet_ManualPositionStop3.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionStop3.TabIndex = 1;
            this.btnSet_ManualPositionStop3.Text = "STOP";
            this.btnSet_ManualPositionStop3.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionStop3.Click += new System.EventHandler(this.btnSet_ManualPositionStop3_Click);
            // 
            // btnPositionCurrentAxis1Set
            // 
            this.btnPositionCurrentAxis1Set.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis1Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis1Set.Location = new System.Drawing.Point(1145, 318);
            this.btnPositionCurrentAxis1Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis1Set.Name = "btnPositionCurrentAxis1Set";
            this.btnPositionCurrentAxis1Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis1Set.TabIndex = 168;
            this.btnPositionCurrentAxis1Set.Text = "Set";
            this.btnPositionCurrentAxis1Set.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis1Set.Click += new System.EventHandler(this.btnPositionCurrentAxis1Set_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.btnSet_JogPlusAxis2);
            this.groupBox2.Controls.Add(this.btnSet_JogMinusAxis2);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(4, 448);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(185, 122);
            this.groupBox2.TabIndex = 104;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "JOG EXIS #2";
            // 
            // btnSet_JogPlusAxis2
            // 
            this.btnSet_JogPlusAxis2.BackColor = System.Drawing.Color.White;
            this.btnSet_JogPlusAxis2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogPlusAxis2.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogPlusAxis2.Location = new System.Drawing.Point(8, 24);
            this.btnSet_JogPlusAxis2.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogPlusAxis2.Name = "btnSet_JogPlusAxis2";
            this.btnSet_JogPlusAxis2.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogPlusAxis2.TabIndex = 0;
            this.btnSet_JogPlusAxis2.Text = "JOG +";
            this.btnSet_JogPlusAxis2.UseVisualStyleBackColor = false;
            this.btnSet_JogPlusAxis2.Click += new System.EventHandler(this.btnSet_JogPlusAxis2_Click);
            // 
            // btnSet_JogMinusAxis2
            // 
            this.btnSet_JogMinusAxis2.BackColor = System.Drawing.Color.White;
            this.btnSet_JogMinusAxis2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogMinusAxis2.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogMinusAxis2.Location = new System.Drawing.Point(8, 74);
            this.btnSet_JogMinusAxis2.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogMinusAxis2.Name = "btnSet_JogMinusAxis2";
            this.btnSet_JogMinusAxis2.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogMinusAxis2.TabIndex = 1;
            this.btnSet_JogMinusAxis2.Text = "JOG -";
            this.btnSet_JogMinusAxis2.UseVisualStyleBackColor = false;
            this.btnSet_JogMinusAxis2.Click += new System.EventHandler(this.btnSet_JogMinusAxis2_Click);
            // 
            // btnPositionRunAxis3Set
            // 
            this.btnPositionRunAxis3Set.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis3Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis3Set.Location = new System.Drawing.Point(1145, 447);
            this.btnPositionRunAxis3Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis3Set.Name = "btnPositionRunAxis3Set";
            this.btnPositionRunAxis3Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis3Set.TabIndex = 162;
            this.btnPositionRunAxis3Set.Text = "Set";
            this.btnPositionRunAxis3Set.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis3Set.Click += new System.EventHandler(this.btnPositionRunAxis3Set_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox6.Controls.Add(this.btnSet_ManualPositionRun2);
            this.groupBox6.Controls.Add(this.btnSet_ManualPositionStop2);
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(197, 316);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(185, 124);
            this.groupBox6.TabIndex = 109;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "MANUAL POSITION #2";
            // 
            // btnSet_ManualPositionRun2
            // 
            this.btnSet_ManualPositionRun2.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionRun2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionRun2.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionRun2.Location = new System.Drawing.Point(8, 24);
            this.btnSet_ManualPositionRun2.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionRun2.Name = "btnSet_ManualPositionRun2";
            this.btnSet_ManualPositionRun2.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionRun2.TabIndex = 0;
            this.btnSet_ManualPositionRun2.Text = "RUN";
            this.btnSet_ManualPositionRun2.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionRun2.Click += new System.EventHandler(this.btnSet_ManualPositionRun2_Click);
            // 
            // btnSet_ManualPositionStop2
            // 
            this.btnSet_ManualPositionStop2.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionStop2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionStop2.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionStop2.Location = new System.Drawing.Point(8, 74);
            this.btnSet_ManualPositionStop2.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionStop2.Name = "btnSet_ManualPositionStop2";
            this.btnSet_ManualPositionStop2.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionStop2.TabIndex = 1;
            this.btnSet_ManualPositionStop2.Text = "STOP";
            this.btnSet_ManualPositionStop2.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionStop2.Click += new System.EventHandler(this.btnSet_ManualPositionStop2_Click);
            // 
            // btnPositionRunAxis1Set
            // 
            this.btnPositionRunAxis1Set.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis1Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis1Set.Location = new System.Drawing.Point(1145, 275);
            this.btnPositionRunAxis1Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis1Set.Name = "btnPositionRunAxis1Set";
            this.btnPositionRunAxis1Set.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis1Set.TabIndex = 164;
            this.btnPositionRunAxis1Set.Text = "Set";
            this.btnPositionRunAxis1Set.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis1Set.Click += new System.EventHandler(this.btnPositionRunAxis1Set_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox3.Controls.Add(this.btnSet_JogPlusAxis3);
            this.groupBox3.Controls.Add(this.btnSet_JogMinusAxis3);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(390, 54);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(185, 124);
            this.groupBox3.TabIndex = 105;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "JOG EXIS #3";
            // 
            // btnSet_JogPlusAxis3
            // 
            this.btnSet_JogPlusAxis3.BackColor = System.Drawing.Color.White;
            this.btnSet_JogPlusAxis3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogPlusAxis3.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogPlusAxis3.Location = new System.Drawing.Point(8, 24);
            this.btnSet_JogPlusAxis3.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogPlusAxis3.Name = "btnSet_JogPlusAxis3";
            this.btnSet_JogPlusAxis3.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogPlusAxis3.TabIndex = 0;
            this.btnSet_JogPlusAxis3.Text = "JOG +";
            this.btnSet_JogPlusAxis3.UseVisualStyleBackColor = false;
            this.btnSet_JogPlusAxis3.Click += new System.EventHandler(this.btnSet_JogPlusAxis3_Click);
            // 
            // btnSet_JogMinusAxis3
            // 
            this.btnSet_JogMinusAxis3.BackColor = System.Drawing.Color.White;
            this.btnSet_JogMinusAxis3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogMinusAxis3.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogMinusAxis3.Location = new System.Drawing.Point(8, 74);
            this.btnSet_JogMinusAxis3.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogMinusAxis3.Name = "btnSet_JogMinusAxis3";
            this.btnSet_JogMinusAxis3.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogMinusAxis3.TabIndex = 1;
            this.btnSet_JogMinusAxis3.Text = "JOG -";
            this.btnSet_JogMinusAxis3.UseVisualStyleBackColor = false;
            this.btnSet_JogMinusAxis3.Click += new System.EventHandler(this.btnSet_JogMinusAxis3_Click);
            // 
            // btnPositionCurrentAxis2Get
            // 
            this.btnPositionCurrentAxis2Get.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis2Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis2Get.Location = new System.Drawing.Point(1084, 404);
            this.btnPositionCurrentAxis2Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis2Get.Name = "btnPositionCurrentAxis2Get";
            this.btnPositionCurrentAxis2Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis2Get.TabIndex = 166;
            this.btnPositionCurrentAxis2Get.Text = "Get";
            this.btnPositionCurrentAxis2Get.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis2Get.Click += new System.EventHandler(this.btnPositionCurrentAxis2Get_Click);
            // 
            // btnPositionCurrentAxis3Get
            // 
            this.btnPositionCurrentAxis3Get.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis3Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis3Get.Location = new System.Drawing.Point(1084, 490);
            this.btnPositionCurrentAxis3Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis3Get.Name = "btnPositionCurrentAxis3Get";
            this.btnPositionCurrentAxis3Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis3Get.TabIndex = 155;
            this.btnPositionCurrentAxis3Get.Text = "Get";
            this.btnPositionCurrentAxis3Get.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis3Get.Click += new System.EventHandler(this.btnPositionCurrentAxis3Get_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox7.Controls.Add(this.btnSet_ManualPositionRun1);
            this.groupBox7.Controls.Add(this.btnSet_ManualPositionStop1);
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox7.Location = new System.Drawing.Point(197, 184);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(185, 124);
            this.groupBox7.TabIndex = 108;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "MANUAL POSITION #1";
            // 
            // btnSet_ManualPositionRun1
            // 
            this.btnSet_ManualPositionRun1.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionRun1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionRun1.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionRun1.Location = new System.Drawing.Point(8, 24);
            this.btnSet_ManualPositionRun1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionRun1.Name = "btnSet_ManualPositionRun1";
            this.btnSet_ManualPositionRun1.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionRun1.TabIndex = 0;
            this.btnSet_ManualPositionRun1.Text = "RUN";
            this.btnSet_ManualPositionRun1.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionRun1.Click += new System.EventHandler(this.btnSet_ManualPositionRun1_Click);
            // 
            // btnSet_ManualPositionStop1
            // 
            this.btnSet_ManualPositionStop1.BackColor = System.Drawing.Color.White;
            this.btnSet_ManualPositionStop1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_ManualPositionStop1.ForeColor = System.Drawing.Color.Black;
            this.btnSet_ManualPositionStop1.Location = new System.Drawing.Point(8, 74);
            this.btnSet_ManualPositionStop1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_ManualPositionStop1.Name = "btnSet_ManualPositionStop1";
            this.btnSet_ManualPositionStop1.Size = new System.Drawing.Size(168, 42);
            this.btnSet_ManualPositionStop1.TabIndex = 1;
            this.btnSet_ManualPositionStop1.Text = "STOP";
            this.btnSet_ManualPositionStop1.UseVisualStyleBackColor = false;
            this.btnSet_ManualPositionStop1.Click += new System.EventHandler(this.btnSet_ManualPositionStop1_Click);
            // 
            // btnPositionRunAxis2Get
            // 
            this.btnPositionRunAxis2Get.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis2Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis2Get.Location = new System.Drawing.Point(1084, 361);
            this.btnPositionRunAxis2Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis2Get.Name = "btnPositionRunAxis2Get";
            this.btnPositionRunAxis2Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis2Get.TabIndex = 152;
            this.btnPositionRunAxis2Get.Text = "Get";
            this.btnPositionRunAxis2Get.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis2Get.Click += new System.EventHandler(this.btnPositionRunAxis2Get_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox4.Controls.Add(this.btnSet_JogPlusAxis4);
            this.groupBox4.Controls.Add(this.btnSet_JogMinusAxis4);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(390, 186);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(185, 124);
            this.groupBox4.TabIndex = 106;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "JOG EXIS #4";
            // 
            // btnSet_JogPlusAxis4
            // 
            this.btnSet_JogPlusAxis4.BackColor = System.Drawing.Color.White;
            this.btnSet_JogPlusAxis4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogPlusAxis4.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogPlusAxis4.Location = new System.Drawing.Point(8, 24);
            this.btnSet_JogPlusAxis4.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogPlusAxis4.Name = "btnSet_JogPlusAxis4";
            this.btnSet_JogPlusAxis4.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogPlusAxis4.TabIndex = 0;
            this.btnSet_JogPlusAxis4.Text = "JOG +";
            this.btnSet_JogPlusAxis4.UseVisualStyleBackColor = false;
            this.btnSet_JogPlusAxis4.Click += new System.EventHandler(this.btnSet_JogPlusAxis4_Click);
            // 
            // btnSet_JogMinusAxis4
            // 
            this.btnSet_JogMinusAxis4.BackColor = System.Drawing.Color.White;
            this.btnSet_JogMinusAxis4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSet_JogMinusAxis4.ForeColor = System.Drawing.Color.Black;
            this.btnSet_JogMinusAxis4.Location = new System.Drawing.Point(8, 74);
            this.btnSet_JogMinusAxis4.Margin = new System.Windows.Forms.Padding(4);
            this.btnSet_JogMinusAxis4.Name = "btnSet_JogMinusAxis4";
            this.btnSet_JogMinusAxis4.Size = new System.Drawing.Size(168, 42);
            this.btnSet_JogMinusAxis4.TabIndex = 1;
            this.btnSet_JogMinusAxis4.Text = "JOG -";
            this.btnSet_JogMinusAxis4.UseVisualStyleBackColor = false;
            this.btnSet_JogMinusAxis4.Click += new System.EventHandler(this.btnSet_JogMinusAxis4_Click);
            // 
            // btnPositionCurrentAxis1Get
            // 
            this.btnPositionCurrentAxis1Get.BackColor = System.Drawing.Color.White;
            this.btnPositionCurrentAxis1Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionCurrentAxis1Get.Location = new System.Drawing.Point(1084, 318);
            this.btnPositionCurrentAxis1Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionCurrentAxis1Get.Name = "btnPositionCurrentAxis1Get";
            this.btnPositionCurrentAxis1Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionCurrentAxis1Get.TabIndex = 151;
            this.btnPositionCurrentAxis1Get.Text = "Get";
            this.btnPositionCurrentAxis1Get.UseVisualStyleBackColor = false;
            this.btnPositionCurrentAxis1Get.Click += new System.EventHandler(this.btnPositionCurrentAxis1Get_Click);
            // 
            // txtJogSpeedAxis3
            // 
            this.txtJogSpeedAxis3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJogSpeedAxis3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtJogSpeedAxis3.Location = new System.Drawing.Point(961, 192);
            this.txtJogSpeedAxis3.Name = "txtJogSpeedAxis3";
            this.txtJogSpeedAxis3.Size = new System.Drawing.Size(112, 27);
            this.txtJogSpeedAxis3.TabIndex = 30;
            // 
            // btnPositionRunAxis3Get
            // 
            this.btnPositionRunAxis3Get.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis3Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis3Get.Location = new System.Drawing.Point(1084, 447);
            this.btnPositionRunAxis3Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis3Get.Name = "btnPositionRunAxis3Get";
            this.btnPositionRunAxis3Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis3Get.TabIndex = 150;
            this.btnPositionRunAxis3Get.Text = "Get";
            this.btnPositionRunAxis3Get.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis3Get.Click += new System.EventHandler(this.btnPositionRunAxis3Get_Click);
            // 
            // txtJogSpeedAxis1
            // 
            this.txtJogSpeedAxis1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJogSpeedAxis1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtJogSpeedAxis1.Location = new System.Drawing.Point(961, 106);
            this.txtJogSpeedAxis1.Name = "txtJogSpeedAxis1";
            this.txtJogSpeedAxis1.Size = new System.Drawing.Size(112, 27);
            this.txtJogSpeedAxis1.TabIndex = 30;
            // 
            // btnPositionRunAxis1Get
            // 
            this.btnPositionRunAxis1Get.BackColor = System.Drawing.Color.White;
            this.btnPositionRunAxis1Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnPositionRunAxis1Get.Location = new System.Drawing.Point(1084, 275);
            this.btnPositionRunAxis1Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositionRunAxis1Get.Name = "btnPositionRunAxis1Get";
            this.btnPositionRunAxis1Get.Size = new System.Drawing.Size(52, 32);
            this.btnPositionRunAxis1Get.TabIndex = 148;
            this.btnPositionRunAxis1Get.Text = "Get";
            this.btnPositionRunAxis1Get.UseVisualStyleBackColor = false;
            this.btnPositionRunAxis1Get.Click += new System.EventHandler(this.btnPositionRunAxis1Get_Click);
            // 
            // txtJogSpeedAxis2
            // 
            this.txtJogSpeedAxis2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJogSpeedAxis2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtJogSpeedAxis2.Location = new System.Drawing.Point(961, 149);
            this.txtJogSpeedAxis2.Name = "txtJogSpeedAxis2";
            this.txtJogSpeedAxis2.Size = new System.Drawing.Size(112, 27);
            this.txtJogSpeedAxis2.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.Location = new System.Drawing.Point(775, 495);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 22);
            this.label6.TabIndex = 125;
            this.label6.Text = "Postion Current Axis#3";
            // 
            // label133
            // 
            this.label133.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(775, 73);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(153, 22);
            this.label133.TabIndex = 29;
            this.label133.Text = "Select Pos";
            this.label133.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(775, 452);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 22);
            this.label7.TabIndex = 126;
            this.label7.Text = "Position Run Axis#3";
            // 
            // label145
            // 
            this.label145.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(951, 73);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(133, 22);
            this.label145.TabIndex = 29;
            this.label145.Text = "Position (mm)";
            this.label145.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label10.Location = new System.Drawing.Point(775, 409);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(180, 22);
            this.label10.TabIndex = 129;
            this.label10.Text = "Postion Current Axis#2";
            // 
            // label136
            // 
            this.label136.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label136.Location = new System.Drawing.Point(775, 108);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(180, 22);
            this.label136.TabIndex = 29;
            this.label136.Text = "Jog Speed Axis#1";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label11.Location = new System.Drawing.Point(775, 366);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(180, 22);
            this.label11.TabIndex = 130;
            this.label11.Text = "Position Run Axis#2";
            // 
            // label139
            // 
            this.label139.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label139.Location = new System.Drawing.Point(775, 151);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(180, 22);
            this.label139.TabIndex = 29;
            this.label139.Text = "Jog Speed Axis#2";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label14.Location = new System.Drawing.Point(775, 323);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(180, 22);
            this.label14.TabIndex = 132;
            this.label14.Text = "Postion Current Axis#1";
            // 
            // label142
            // 
            this.label142.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label142.Location = new System.Drawing.Point(775, 194);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(180, 22);
            this.label142.TabIndex = 29;
            this.label142.Text = "Jog Speed Axis#3";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label18.Location = new System.Drawing.Point(775, 280);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(180, 22);
            this.label18.TabIndex = 135;
            this.label18.Text = "Position Run Axis#1";
            // 
            // btnJogSpeedAxis3Get
            // 
            this.btnJogSpeedAxis3Get.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis3Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis3Get.Location = new System.Drawing.Point(1084, 189);
            this.btnJogSpeedAxis3Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis3Get.Name = "btnJogSpeedAxis3Get";
            this.btnJogSpeedAxis3Get.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis3Get.TabIndex = 55;
            this.btnJogSpeedAxis3Get.Text = "Get";
            this.btnJogSpeedAxis3Get.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis3Get.Click += new System.EventHandler(this.btnJogSpeedAxis3Get_Click);
            // 
            // txtPositionCurrentAxis2
            // 
            this.txtPositionCurrentAxis2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionCurrentAxis2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionCurrentAxis2.Location = new System.Drawing.Point(961, 407);
            this.txtPositionCurrentAxis2.Name = "txtPositionCurrentAxis2";
            this.txtPositionCurrentAxis2.Size = new System.Drawing.Size(112, 27);
            this.txtPositionCurrentAxis2.TabIndex = 137;
            // 
            // btnJogSpeedAxis1Get
            // 
            this.btnJogSpeedAxis1Get.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis1Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis1Get.Location = new System.Drawing.Point(1084, 103);
            this.btnJogSpeedAxis1Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis1Get.Name = "btnJogSpeedAxis1Get";
            this.btnJogSpeedAxis1Get.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis1Get.TabIndex = 55;
            this.btnJogSpeedAxis1Get.Text = "Get";
            this.btnJogSpeedAxis1Get.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis1Get.Click += new System.EventHandler(this.btnJogSpeedAxis1Get_Click);
            // 
            // txtPositionCurrentAxis3
            // 
            this.txtPositionCurrentAxis3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionCurrentAxis3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionCurrentAxis3.Location = new System.Drawing.Point(961, 493);
            this.txtPositionCurrentAxis3.Name = "txtPositionCurrentAxis3";
            this.txtPositionCurrentAxis3.Size = new System.Drawing.Size(112, 27);
            this.txtPositionCurrentAxis3.TabIndex = 140;
            // 
            // btnJogSpeedAxis2Get
            // 
            this.btnJogSpeedAxis2Get.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis2Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis2Get.Location = new System.Drawing.Point(1084, 146);
            this.btnJogSpeedAxis2Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis2Get.Name = "btnJogSpeedAxis2Get";
            this.btnJogSpeedAxis2Get.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis2Get.TabIndex = 55;
            this.btnJogSpeedAxis2Get.Text = "Get";
            this.btnJogSpeedAxis2Get.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis2Get.Click += new System.EventHandler(this.btnJogSpeedAxis2Get_Click);
            // 
            // txtPositionRunAxis2
            // 
            this.txtPositionRunAxis2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionRunAxis2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionRunAxis2.Location = new System.Drawing.Point(961, 364);
            this.txtPositionRunAxis2.Name = "txtPositionRunAxis2";
            this.txtPositionRunAxis2.Size = new System.Drawing.Size(112, 27);
            this.txtPositionRunAxis2.TabIndex = 141;
            // 
            // btnJogSpeedAxis3Set
            // 
            this.btnJogSpeedAxis3Set.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis3Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis3Set.Location = new System.Drawing.Point(1145, 189);
            this.btnJogSpeedAxis3Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis3Set.Name = "btnJogSpeedAxis3Set";
            this.btnJogSpeedAxis3Set.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis3Set.TabIndex = 55;
            this.btnJogSpeedAxis3Set.Text = "Set";
            this.btnJogSpeedAxis3Set.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis3Set.Click += new System.EventHandler(this.btnJogSpeedAxis3Set_Click);
            // 
            // txtPositionRunAxis3
            // 
            this.txtPositionRunAxis3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionRunAxis3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionRunAxis3.Location = new System.Drawing.Point(961, 450);
            this.txtPositionRunAxis3.Name = "txtPositionRunAxis3";
            this.txtPositionRunAxis3.Size = new System.Drawing.Size(112, 27);
            this.txtPositionRunAxis3.TabIndex = 143;
            // 
            // btnJogSpeedAxis1Set
            // 
            this.btnJogSpeedAxis1Set.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis1Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis1Set.Location = new System.Drawing.Point(1145, 103);
            this.btnJogSpeedAxis1Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis1Set.Name = "btnJogSpeedAxis1Set";
            this.btnJogSpeedAxis1Set.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis1Set.TabIndex = 55;
            this.btnJogSpeedAxis1Set.Text = "Set";
            this.btnJogSpeedAxis1Set.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis1Set.Click += new System.EventHandler(this.btnJogSpeedAxis1Set_Click);
            // 
            // txtPositionCurrentAxis1
            // 
            this.txtPositionCurrentAxis1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionCurrentAxis1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionCurrentAxis1.Location = new System.Drawing.Point(961, 321);
            this.txtPositionCurrentAxis1.Name = "txtPositionCurrentAxis1";
            this.txtPositionCurrentAxis1.Size = new System.Drawing.Size(112, 27);
            this.txtPositionCurrentAxis1.TabIndex = 144;
            // 
            // btnJogSpeedAxis2Set
            // 
            this.btnJogSpeedAxis2Set.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis2Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis2Set.Location = new System.Drawing.Point(1145, 146);
            this.btnJogSpeedAxis2Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis2Set.Name = "btnJogSpeedAxis2Set";
            this.btnJogSpeedAxis2Set.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis2Set.TabIndex = 55;
            this.btnJogSpeedAxis2Set.Text = "Set";
            this.btnJogSpeedAxis2Set.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis2Set.Click += new System.EventHandler(this.btnJogSpeedAxis2Set_Click);
            // 
            // txtPositionRunAxis1
            // 
            this.txtPositionRunAxis1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPositionRunAxis1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPositionRunAxis1.Location = new System.Drawing.Point(961, 278);
            this.txtPositionRunAxis1.Name = "txtPositionRunAxis1";
            this.txtPositionRunAxis1.Size = new System.Drawing.Size(112, 27);
            this.txtPositionRunAxis1.TabIndex = 136;
            // 
            // txtJogSpeedAxis4
            // 
            this.txtJogSpeedAxis4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJogSpeedAxis4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtJogSpeedAxis4.Location = new System.Drawing.Point(961, 235);
            this.txtJogSpeedAxis4.Name = "txtJogSpeedAxis4";
            this.txtJogSpeedAxis4.Size = new System.Drawing.Size(112, 27);
            this.txtJogSpeedAxis4.TabIndex = 116;
            // 
            // btnJogSpeedAxis4Set
            // 
            this.btnJogSpeedAxis4Set.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis4Set.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis4Set.Location = new System.Drawing.Point(1145, 232);
            this.btnJogSpeedAxis4Set.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis4Set.Name = "btnJogSpeedAxis4Set";
            this.btnJogSpeedAxis4Set.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis4Set.TabIndex = 119;
            this.btnJogSpeedAxis4Set.Text = "Set";
            this.btnJogSpeedAxis4Set.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis4Set.Click += new System.EventHandler(this.btnJogSpeedAxis4Set_Click);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label17.Location = new System.Drawing.Point(775, 237);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(180, 22);
            this.label17.TabIndex = 113;
            this.label17.Text = "Jog Speed Axis#4";
            // 
            // btnJogSpeedAxis4Get
            // 
            this.btnJogSpeedAxis4Get.BackColor = System.Drawing.Color.White;
            this.btnJogSpeedAxis4Get.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnJogSpeedAxis4Get.Location = new System.Drawing.Point(1084, 232);
            this.btnJogSpeedAxis4Get.Margin = new System.Windows.Forms.Padding(0);
            this.btnJogSpeedAxis4Get.Name = "btnJogSpeedAxis4Get";
            this.btnJogSpeedAxis4Get.Size = new System.Drawing.Size(52, 32);
            this.btnJogSpeedAxis4Get.TabIndex = 122;
            this.btnJogSpeedAxis4Get.Text = "Get";
            this.btnJogSpeedAxis4Get.UseVisualStyleBackColor = false;
            this.btnJogSpeedAxis4Get.Click += new System.EventHandler(this.btnJogSpeedAxis4Get_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pTeaching);
            this.tabPage2.Location = new System.Drawing.Point(4, 54);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1224, 692);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Teachings Axis";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pTeaching
            // 
            this.pTeaching.BackColor = System.Drawing.Color.Transparent;
            this.pTeaching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTeaching.Controls.Add(this.groupBox17);
            this.pTeaching.Controls.Add(this.groupBox16);
            this.pTeaching.Controls.Add(this.groupBox15);
            this.pTeaching.Controls.Add(this.btnCancelTeaching);
            this.pTeaching.Controls.Add(this.label131);
            this.pTeaching.Controls.Add(this.groupBox12);
            this.pTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTeaching.Location = new System.Drawing.Point(3, 3);
            this.pTeaching.Name = "pTeaching";
            this.pTeaching.Size = new System.Drawing.Size(1218, 686);
            this.pTeaching.TabIndex = 101;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.btnSet_Grinding_L_Speed_4);
            this.groupBox17.Controls.Add(this.txtGrinding_L_Speed_4);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_L_Speed_4);
            this.groupBox17.Controls.Add(this.label19);
            this.groupBox17.Controls.Add(this.btnSet_Grinding_L_Position_4);
            this.groupBox17.Controls.Add(this.txtGrinding_L_Position_4);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_L_Position_4);
            this.groupBox17.Controls.Add(this.label20);
            this.groupBox17.Controls.Add(this.btnSet_Grinding_R_Speed_4);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_R_Position_4);
            this.groupBox17.Controls.Add(this.txtGrinding_R_Position_4);
            this.groupBox17.Controls.Add(this.btnSet_Grinding_R_Position_4);
            this.groupBox17.Controls.Add(this.txtGrinding_R_Speed_4);
            this.groupBox17.Controls.Add(this.label21);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_R_Speed_4);
            this.groupBox17.Controls.Add(this.label22);
            this.groupBox17.Controls.Add(this.btnSet_Grinding_Speed_4);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_Position_4);
            this.groupBox17.Controls.Add(this.txtGrinding_Position_4);
            this.groupBox17.Controls.Add(this.btnSet_Grinding_Position_4);
            this.groupBox17.Controls.Add(this.txtGrinding_Speed_4);
            this.groupBox17.Controls.Add(this.label23);
            this.groupBox17.Controls.Add(this.btnGet_Grinding_Speed_4);
            this.groupBox17.Controls.Add(this.label24);
            this.groupBox17.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox17.Location = new System.Drawing.Point(483, 325);
            this.groupBox17.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox17.Size = new System.Drawing.Size(464, 258);
            this.groupBox17.TabIndex = 246;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "AXIS #4";
            // 
            // btnSet_Grinding_L_Speed_4
            // 
            this.btnSet_Grinding_L_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_L_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_L_Speed_4.Location = new System.Drawing.Point(398, 212);
            this.btnSet_Grinding_L_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_L_Speed_4.Name = "btnSet_Grinding_L_Speed_4";
            this.btnSet_Grinding_L_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_L_Speed_4.TabIndex = 244;
            this.btnSet_Grinding_L_Speed_4.Text = "Set";
            this.btnSet_Grinding_L_Speed_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_L_Speed_4.Click += new System.EventHandler(this.btnSet_Grinding_L_Speed_4_Click);
            // 
            // txtGrinding_L_Speed_4
            // 
            this.txtGrinding_L_Speed_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_L_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_L_Speed_4.Location = new System.Drawing.Point(214, 215);
            this.txtGrinding_L_Speed_4.Name = "txtGrinding_L_Speed_4";
            this.txtGrinding_L_Speed_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_L_Speed_4.TabIndex = 243;
            // 
            // btnGet_Grinding_L_Speed_4
            // 
            this.btnGet_Grinding_L_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_L_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_L_Speed_4.Location = new System.Drawing.Point(337, 212);
            this.btnGet_Grinding_L_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_L_Speed_4.Name = "btnGet_Grinding_L_Speed_4";
            this.btnGet_Grinding_L_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_L_Speed_4.TabIndex = 245;
            this.btnGet_Grinding_L_Speed_4.Text = "Get";
            this.btnGet_Grinding_L_Speed_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_L_Speed_4.Click += new System.EventHandler(this.btnGet_Grinding_L_Speed_4_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label19.Location = new System.Drawing.Point(11, 219);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(190, 19);
            this.label19.TabIndex = 242;
            this.label19.Text = "Manual Grinding L_Speed";
            // 
            // btnSet_Grinding_L_Position_4
            // 
            this.btnSet_Grinding_L_Position_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_L_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_L_Position_4.Location = new System.Drawing.Point(398, 174);
            this.btnSet_Grinding_L_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_L_Position_4.Name = "btnSet_Grinding_L_Position_4";
            this.btnSet_Grinding_L_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_L_Position_4.TabIndex = 240;
            this.btnSet_Grinding_L_Position_4.Text = "Set";
            this.btnSet_Grinding_L_Position_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_L_Position_4.Click += new System.EventHandler(this.btnSet_Grinding_L_Position_4_Click);
            // 
            // txtGrinding_L_Position_4
            // 
            this.txtGrinding_L_Position_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_L_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_L_Position_4.Location = new System.Drawing.Point(214, 177);
            this.txtGrinding_L_Position_4.Name = "txtGrinding_L_Position_4";
            this.txtGrinding_L_Position_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_L_Position_4.TabIndex = 239;
            // 
            // btnGet_Grinding_L_Position_4
            // 
            this.btnGet_Grinding_L_Position_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_L_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_L_Position_4.Location = new System.Drawing.Point(337, 174);
            this.btnGet_Grinding_L_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_L_Position_4.Name = "btnGet_Grinding_L_Position_4";
            this.btnGet_Grinding_L_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_L_Position_4.TabIndex = 241;
            this.btnGet_Grinding_L_Position_4.Text = "Get";
            this.btnGet_Grinding_L_Position_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_L_Position_4.Click += new System.EventHandler(this.btnGet_Grinding_L_Position_4_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label20.Location = new System.Drawing.Point(11, 181);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(203, 19);
            this.label20.TabIndex = 238;
            this.label20.Text = "Manual Grinding L_Position";
            // 
            // btnSet_Grinding_R_Speed_4
            // 
            this.btnSet_Grinding_R_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_R_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_R_Speed_4.Location = new System.Drawing.Point(398, 137);
            this.btnSet_Grinding_R_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_R_Speed_4.Name = "btnSet_Grinding_R_Speed_4";
            this.btnSet_Grinding_R_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_R_Speed_4.TabIndex = 234;
            this.btnSet_Grinding_R_Speed_4.Text = "Set";
            this.btnSet_Grinding_R_Speed_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_R_Speed_4.Click += new System.EventHandler(this.btnSet_Grinding_R_Speed_4_Click);
            // 
            // btnGet_Grinding_R_Position_4
            // 
            this.btnGet_Grinding_R_Position_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_R_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_R_Position_4.Location = new System.Drawing.Point(337, 100);
            this.btnGet_Grinding_R_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_R_Position_4.Name = "btnGet_Grinding_R_Position_4";
            this.btnGet_Grinding_R_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_R_Position_4.TabIndex = 237;
            this.btnGet_Grinding_R_Position_4.Text = "Get";
            this.btnGet_Grinding_R_Position_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_R_Position_4.Click += new System.EventHandler(this.btnGet_Grinding_R_Position_4_Click);
            // 
            // txtGrinding_R_Position_4
            // 
            this.txtGrinding_R_Position_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_R_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_R_Position_4.Location = new System.Drawing.Point(214, 103);
            this.txtGrinding_R_Position_4.Name = "txtGrinding_R_Position_4";
            this.txtGrinding_R_Position_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_R_Position_4.TabIndex = 233;
            // 
            // btnSet_Grinding_R_Position_4
            // 
            this.btnSet_Grinding_R_Position_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_R_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_R_Position_4.Location = new System.Drawing.Point(398, 100);
            this.btnSet_Grinding_R_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_R_Position_4.Name = "btnSet_Grinding_R_Position_4";
            this.btnSet_Grinding_R_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_R_Position_4.TabIndex = 235;
            this.btnSet_Grinding_R_Position_4.Text = "Set";
            this.btnSet_Grinding_R_Position_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_R_Position_4.Click += new System.EventHandler(this.btnSet_Grinding_R_Position_4_Click);
            // 
            // txtGrinding_R_Speed_4
            // 
            this.txtGrinding_R_Speed_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_R_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_R_Speed_4.Location = new System.Drawing.Point(214, 140);
            this.txtGrinding_R_Speed_4.Name = "txtGrinding_R_Speed_4";
            this.txtGrinding_R_Speed_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_R_Speed_4.TabIndex = 232;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label21.Location = new System.Drawing.Point(11, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(214, 22);
            this.label21.TabIndex = 231;
            this.label21.Text = "Manual Grinding R_Position";
            // 
            // btnGet_Grinding_R_Speed_4
            // 
            this.btnGet_Grinding_R_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_R_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_R_Speed_4.Location = new System.Drawing.Point(337, 137);
            this.btnGet_Grinding_R_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_R_Speed_4.Name = "btnGet_Grinding_R_Speed_4";
            this.btnGet_Grinding_R_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_R_Speed_4.TabIndex = 236;
            this.btnGet_Grinding_R_Speed_4.Text = "Get";
            this.btnGet_Grinding_R_Speed_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_R_Speed_4.Click += new System.EventHandler(this.btnGet_Grinding_R_Speed_4_Click);
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label22.Location = new System.Drawing.Point(11, 142);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(196, 22);
            this.label22.TabIndex = 230;
            this.label22.Text = "Manual Grinding R_Speed";
            // 
            // btnSet_Grinding_Speed_4
            // 
            this.btnSet_Grinding_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Speed_4.Location = new System.Drawing.Point(398, 63);
            this.btnSet_Grinding_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Speed_4.Name = "btnSet_Grinding_Speed_4";
            this.btnSet_Grinding_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Speed_4.TabIndex = 226;
            this.btnSet_Grinding_Speed_4.Text = "Set";
            this.btnSet_Grinding_Speed_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Speed_4.Click += new System.EventHandler(this.btnSet_Grinding_Speed_4_Click);
            // 
            // btnGet_Grinding_Position_4
            // 
            this.btnGet_Grinding_Position_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Position_4.Location = new System.Drawing.Point(337, 26);
            this.btnGet_Grinding_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Position_4.Name = "btnGet_Grinding_Position_4";
            this.btnGet_Grinding_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Position_4.TabIndex = 229;
            this.btnGet_Grinding_Position_4.Text = "Get";
            this.btnGet_Grinding_Position_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Position_4.Click += new System.EventHandler(this.btnGet_Grinding_Position_4_Click);
            // 
            // txtGrinding_Position_4
            // 
            this.txtGrinding_Position_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Position_4.Location = new System.Drawing.Point(214, 29);
            this.txtGrinding_Position_4.Name = "txtGrinding_Position_4";
            this.txtGrinding_Position_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Position_4.TabIndex = 225;
            // 
            // btnSet_Grinding_Position_4
            // 
            this.btnSet_Grinding_Position_4.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Position_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Position_4.Location = new System.Drawing.Point(398, 26);
            this.btnSet_Grinding_Position_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Position_4.Name = "btnSet_Grinding_Position_4";
            this.btnSet_Grinding_Position_4.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Position_4.TabIndex = 227;
            this.btnSet_Grinding_Position_4.Text = "Set";
            this.btnSet_Grinding_Position_4.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Position_4.Click += new System.EventHandler(this.btnSet_Grinding_Position_4_Click);
            // 
            // txtGrinding_Speed_4
            // 
            this.txtGrinding_Speed_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Speed_4.Location = new System.Drawing.Point(214, 66);
            this.txtGrinding_Speed_4.Name = "txtGrinding_Speed_4";
            this.txtGrinding_Speed_4.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Speed_4.TabIndex = 224;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label23.Location = new System.Drawing.Point(11, 31);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(180, 22);
            this.label23.TabIndex = 223;
            this.label23.Text = "Grinding_Position";
            // 
            // btnGet_Grinding_Speed_4
            // 
            this.btnGet_Grinding_Speed_4.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Speed_4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Speed_4.Location = new System.Drawing.Point(337, 63);
            this.btnGet_Grinding_Speed_4.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Speed_4.Name = "btnGet_Grinding_Speed_4";
            this.btnGet_Grinding_Speed_4.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Speed_4.TabIndex = 228;
            this.btnGet_Grinding_Speed_4.Text = "Get";
            this.btnGet_Grinding_Speed_4.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Speed_4.Click += new System.EventHandler(this.btnGet_Grinding_Speed_4_Click);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label24.Location = new System.Drawing.Point(11, 68);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(180, 22);
            this.label24.TabIndex = 222;
            this.label24.Text = "Grinding_Speed";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.btnSet_Offset_Grinding_Speed_3);
            this.groupBox16.Controls.Add(this.txtOffset_Grinding_Speed_3);
            this.groupBox16.Controls.Add(this.btnGet_Offset_Grinding_Speed_3);
            this.groupBox16.Controls.Add(this.label16);
            this.groupBox16.Controls.Add(this.btnSet_Offset_Grinding_Position_3);
            this.groupBox16.Controls.Add(this.txtOffset_Grinding_Position_3);
            this.groupBox16.Controls.Add(this.btnGet_Offset_Grinding_Position_3);
            this.groupBox16.Controls.Add(this.label8);
            this.groupBox16.Controls.Add(this.btnSet_Grinding_Speed_3);
            this.groupBox16.Controls.Add(this.btnGet_Grinding_Position_3);
            this.groupBox16.Controls.Add(this.txtGrinding_Position_3);
            this.groupBox16.Controls.Add(this.btnSet_Grinding_Position_3);
            this.groupBox16.Controls.Add(this.txtGrinding_Speed_3);
            this.groupBox16.Controls.Add(this.label9);
            this.groupBox16.Controls.Add(this.btnGet_Grinding_Speed_3);
            this.groupBox16.Controls.Add(this.label12);
            this.groupBox16.Controls.Add(this.btnSet_Ready_Speed_3);
            this.groupBox16.Controls.Add(this.btnGet_Ready_Position_3);
            this.groupBox16.Controls.Add(this.txtReady_Position_3);
            this.groupBox16.Controls.Add(this.btnSet_Ready_Position_3);
            this.groupBox16.Controls.Add(this.txtReady_Speed_3);
            this.groupBox16.Controls.Add(this.label13);
            this.groupBox16.Controls.Add(this.btnGet_Ready_Speed_3);
            this.groupBox16.Controls.Add(this.label15);
            this.groupBox16.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox16.Location = new System.Drawing.Point(4, 325);
            this.groupBox16.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox16.Size = new System.Drawing.Size(471, 258);
            this.groupBox16.TabIndex = 243;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "AXIS #3";
            // 
            // btnSet_Offset_Grinding_Speed_3
            // 
            this.btnSet_Offset_Grinding_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Offset_Grinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Offset_Grinding_Speed_3.Location = new System.Drawing.Point(397, 212);
            this.btnSet_Offset_Grinding_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Offset_Grinding_Speed_3.Name = "btnSet_Offset_Grinding_Speed_3";
            this.btnSet_Offset_Grinding_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Offset_Grinding_Speed_3.TabIndex = 244;
            this.btnSet_Offset_Grinding_Speed_3.Text = "Set";
            this.btnSet_Offset_Grinding_Speed_3.UseVisualStyleBackColor = false;
            this.btnSet_Offset_Grinding_Speed_3.Click += new System.EventHandler(this.btnSet_Offset_Grinding_Speed_3_Click);
            // 
            // txtOffset_Grinding_Speed_3
            // 
            this.txtOffset_Grinding_Speed_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOffset_Grinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtOffset_Grinding_Speed_3.Location = new System.Drawing.Point(213, 215);
            this.txtOffset_Grinding_Speed_3.Name = "txtOffset_Grinding_Speed_3";
            this.txtOffset_Grinding_Speed_3.Size = new System.Drawing.Size(112, 27);
            this.txtOffset_Grinding_Speed_3.TabIndex = 243;
            // 
            // btnGet_Offset_Grinding_Speed_3
            // 
            this.btnGet_Offset_Grinding_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Offset_Grinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Offset_Grinding_Speed_3.Location = new System.Drawing.Point(336, 212);
            this.btnGet_Offset_Grinding_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Offset_Grinding_Speed_3.Name = "btnGet_Offset_Grinding_Speed_3";
            this.btnGet_Offset_Grinding_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Offset_Grinding_Speed_3.TabIndex = 245;
            this.btnGet_Offset_Grinding_Speed_3.Text = "Get";
            this.btnGet_Offset_Grinding_Speed_3.UseVisualStyleBackColor = false;
            this.btnGet_Offset_Grinding_Speed_3.Click += new System.EventHandler(this.btnGet_Offset_Grinding_Speed_3_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label16.Location = new System.Drawing.Point(11, 218);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(169, 19);
            this.label16.TabIndex = 242;
            this.label16.Text = "Offset Grinding_Speed";
            // 
            // btnSet_Offset_Grinding_Position_3
            // 
            this.btnSet_Offset_Grinding_Position_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Offset_Grinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Offset_Grinding_Position_3.Location = new System.Drawing.Point(397, 174);
            this.btnSet_Offset_Grinding_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Offset_Grinding_Position_3.Name = "btnSet_Offset_Grinding_Position_3";
            this.btnSet_Offset_Grinding_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Offset_Grinding_Position_3.TabIndex = 240;
            this.btnSet_Offset_Grinding_Position_3.Text = "Set";
            this.btnSet_Offset_Grinding_Position_3.UseVisualStyleBackColor = false;
            this.btnSet_Offset_Grinding_Position_3.Click += new System.EventHandler(this.btnSet_Offset_Grinding_Position_3_Click);
            // 
            // txtOffset_Grinding_Position_3
            // 
            this.txtOffset_Grinding_Position_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOffset_Grinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtOffset_Grinding_Position_3.Location = new System.Drawing.Point(213, 177);
            this.txtOffset_Grinding_Position_3.Name = "txtOffset_Grinding_Position_3";
            this.txtOffset_Grinding_Position_3.Size = new System.Drawing.Size(112, 27);
            this.txtOffset_Grinding_Position_3.TabIndex = 239;
            // 
            // btnGet_Offset_Grinding_Position_3
            // 
            this.btnGet_Offset_Grinding_Position_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Offset_Grinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Offset_Grinding_Position_3.Location = new System.Drawing.Point(336, 174);
            this.btnGet_Offset_Grinding_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Offset_Grinding_Position_3.Name = "btnGet_Offset_Grinding_Position_3";
            this.btnGet_Offset_Grinding_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Offset_Grinding_Position_3.TabIndex = 241;
            this.btnGet_Offset_Grinding_Position_3.Text = "Get";
            this.btnGet_Offset_Grinding_Position_3.UseVisualStyleBackColor = false;
            this.btnGet_Offset_Grinding_Position_3.Click += new System.EventHandler(this.btnGet_Offset_Grinding_Position_3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.Location = new System.Drawing.Point(11, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 19);
            this.label8.TabIndex = 238;
            this.label8.Text = "Offset Grinding_Position";
            // 
            // btnSet_Grinding_Speed_3
            // 
            this.btnSet_Grinding_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Speed_3.Location = new System.Drawing.Point(397, 137);
            this.btnSet_Grinding_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Speed_3.Name = "btnSet_Grinding_Speed_3";
            this.btnSet_Grinding_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Speed_3.TabIndex = 234;
            this.btnSet_Grinding_Speed_3.Text = "Set";
            this.btnSet_Grinding_Speed_3.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Speed_3.Click += new System.EventHandler(this.btnSet_Grinding_Speed_3_Click);
            // 
            // btnGet_Grinding_Position_3
            // 
            this.btnGet_Grinding_Position_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Position_3.Location = new System.Drawing.Point(336, 100);
            this.btnGet_Grinding_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Position_3.Name = "btnGet_Grinding_Position_3";
            this.btnGet_Grinding_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Position_3.TabIndex = 237;
            this.btnGet_Grinding_Position_3.Text = "Get";
            this.btnGet_Grinding_Position_3.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Position_3.Click += new System.EventHandler(this.btnGet_Grinding_Position_3_Click);
            // 
            // txtGrinding_Position_3
            // 
            this.txtGrinding_Position_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Position_3.Location = new System.Drawing.Point(213, 103);
            this.txtGrinding_Position_3.Name = "txtGrinding_Position_3";
            this.txtGrinding_Position_3.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Position_3.TabIndex = 233;
            // 
            // btnSet_Grinding_Position_3
            // 
            this.btnSet_Grinding_Position_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Position_3.Location = new System.Drawing.Point(397, 100);
            this.btnSet_Grinding_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Position_3.Name = "btnSet_Grinding_Position_3";
            this.btnSet_Grinding_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Position_3.TabIndex = 235;
            this.btnSet_Grinding_Position_3.Text = "Set";
            this.btnSet_Grinding_Position_3.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Position_3.Click += new System.EventHandler(this.btnSet_Grinding_Position_3_Click);
            // 
            // txtGrinding_Speed_3
            // 
            this.txtGrinding_Speed_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Speed_3.Location = new System.Drawing.Point(213, 140);
            this.txtGrinding_Speed_3.Name = "txtGrinding_Speed_3";
            this.txtGrinding_Speed_3.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Speed_3.TabIndex = 232;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label9.Location = new System.Drawing.Point(11, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(196, 22);
            this.label9.TabIndex = 231;
            this.label9.Text = "Start Grinding_Position";
            // 
            // btnGet_Grinding_Speed_3
            // 
            this.btnGet_Grinding_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Speed_3.Location = new System.Drawing.Point(336, 137);
            this.btnGet_Grinding_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Speed_3.Name = "btnGet_Grinding_Speed_3";
            this.btnGet_Grinding_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Speed_3.TabIndex = 236;
            this.btnGet_Grinding_Speed_3.Text = "Get";
            this.btnGet_Grinding_Speed_3.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Speed_3.Click += new System.EventHandler(this.btnGet_Grinding_Speed_3_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label12.Location = new System.Drawing.Point(11, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(196, 22);
            this.label12.TabIndex = 230;
            this.label12.Text = "Start Grinding_Speed";
            // 
            // btnSet_Ready_Speed_3
            // 
            this.btnSet_Ready_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Ready_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Ready_Speed_3.Location = new System.Drawing.Point(397, 63);
            this.btnSet_Ready_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Ready_Speed_3.Name = "btnSet_Ready_Speed_3";
            this.btnSet_Ready_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Ready_Speed_3.TabIndex = 226;
            this.btnSet_Ready_Speed_3.Text = "Set";
            this.btnSet_Ready_Speed_3.UseVisualStyleBackColor = false;
            this.btnSet_Ready_Speed_3.Click += new System.EventHandler(this.btnSet_Ready_Speed_3_Click);
            // 
            // btnGet_Ready_Position_3
            // 
            this.btnGet_Ready_Position_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Ready_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Ready_Position_3.Location = new System.Drawing.Point(336, 26);
            this.btnGet_Ready_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Ready_Position_3.Name = "btnGet_Ready_Position_3";
            this.btnGet_Ready_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Ready_Position_3.TabIndex = 229;
            this.btnGet_Ready_Position_3.Text = "Get";
            this.btnGet_Ready_Position_3.UseVisualStyleBackColor = false;
            this.btnGet_Ready_Position_3.Click += new System.EventHandler(this.btnGet_Ready_Position_3_Click);
            // 
            // txtReady_Position_3
            // 
            this.txtReady_Position_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReady_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReady_Position_3.Location = new System.Drawing.Point(213, 29);
            this.txtReady_Position_3.Name = "txtReady_Position_3";
            this.txtReady_Position_3.Size = new System.Drawing.Size(112, 27);
            this.txtReady_Position_3.TabIndex = 225;
            // 
            // btnSet_Ready_Position_3
            // 
            this.btnSet_Ready_Position_3.BackColor = System.Drawing.Color.White;
            this.btnSet_Ready_Position_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Ready_Position_3.Location = new System.Drawing.Point(397, 26);
            this.btnSet_Ready_Position_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Ready_Position_3.Name = "btnSet_Ready_Position_3";
            this.btnSet_Ready_Position_3.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Ready_Position_3.TabIndex = 227;
            this.btnSet_Ready_Position_3.Text = "Set";
            this.btnSet_Ready_Position_3.UseVisualStyleBackColor = false;
            this.btnSet_Ready_Position_3.Click += new System.EventHandler(this.btnSet_Ready_Position_3_Click);
            // 
            // txtReady_Speed_3
            // 
            this.txtReady_Speed_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReady_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReady_Speed_3.Location = new System.Drawing.Point(213, 66);
            this.txtReady_Speed_3.Name = "txtReady_Speed_3";
            this.txtReady_Speed_3.Size = new System.Drawing.Size(112, 27);
            this.txtReady_Speed_3.TabIndex = 224;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label13.Location = new System.Drawing.Point(11, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(180, 22);
            this.label13.TabIndex = 223;
            this.label13.Text = "Ready_Position";
            // 
            // btnGet_Ready_Speed_3
            // 
            this.btnGet_Ready_Speed_3.BackColor = System.Drawing.Color.White;
            this.btnGet_Ready_Speed_3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Ready_Speed_3.Location = new System.Drawing.Point(336, 63);
            this.btnGet_Ready_Speed_3.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Ready_Speed_3.Name = "btnGet_Ready_Speed_3";
            this.btnGet_Ready_Speed_3.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Ready_Speed_3.TabIndex = 228;
            this.btnGet_Ready_Speed_3.Text = "Get";
            this.btnGet_Ready_Speed_3.UseVisualStyleBackColor = false;
            this.btnGet_Ready_Speed_3.Click += new System.EventHandler(this.btnGet_Ready_Speed_3_Click);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label15.Location = new System.Drawing.Point(11, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(180, 22);
            this.label15.TabIndex = 222;
            this.label15.Text = "Ready_Speed";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label33);
            this.groupBox15.Controls.Add(this.btnSet_Concentrick_Adjustment_Speed_2);
            this.groupBox15.Controls.Add(this.txtConcentrick_Adjustment_Speed_2);
            this.groupBox15.Controls.Add(this.btnGet_Concentrick_Adjustment_Speed_2);
            this.groupBox15.Controls.Add(this.btnSet_Concentrick_Adjustment_2);
            this.groupBox15.Controls.Add(this.txtConcentrick_Adjustment_Position_2);
            this.groupBox15.Controls.Add(this.btnGet_Concentrick_Adjustment_2);
            this.groupBox15.Controls.Add(this.label28);
            this.groupBox15.Controls.Add(this.btnSet_Concentrick_Speed_2);
            this.groupBox15.Controls.Add(this.btnGet_Concentrick_Position_2);
            this.groupBox15.Controls.Add(this.txtConcentrick_Position_2);
            this.groupBox15.Controls.Add(this.btnSet_Concentrick_Position_2);
            this.groupBox15.Controls.Add(this.txtConcentrick_Speed_2);
            this.groupBox15.Controls.Add(this.label29);
            this.groupBox15.Controls.Add(this.btnGet_Concentrick_Speed_2);
            this.groupBox15.Controls.Add(this.label30);
            this.groupBox15.Controls.Add(this.btnSet_Ready_Speed_2);
            this.groupBox15.Controls.Add(this.btnGet_Ready_Position_2);
            this.groupBox15.Controls.Add(this.txtReady_Position_2);
            this.groupBox15.Controls.Add(this.btnSet_Ready_Position_2);
            this.groupBox15.Controls.Add(this.txtReady_Speed_2);
            this.groupBox15.Controls.Add(this.label31);
            this.groupBox15.Controls.Add(this.btnGet_Ready_Speed_2);
            this.groupBox15.Controls.Add(this.label32);
            this.groupBox15.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox15.Location = new System.Drawing.Point(483, 54);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox15.Size = new System.Drawing.Size(464, 272);
            this.groupBox15.TabIndex = 242;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "AXIS #2";
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label33.Location = new System.Drawing.Point(11, 209);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(190, 39);
            this.label33.TabIndex = 245;
            this.label33.Text = "Concentrick Adjustment Speed";
            // 
            // btnSet_Concentrick_Adjustment_Speed_2
            // 
            this.btnSet_Concentrick_Adjustment_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Concentrick_Adjustment_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Concentrick_Adjustment_Speed_2.Location = new System.Drawing.Point(398, 212);
            this.btnSet_Concentrick_Adjustment_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Concentrick_Adjustment_Speed_2.Name = "btnSet_Concentrick_Adjustment_Speed_2";
            this.btnSet_Concentrick_Adjustment_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Concentrick_Adjustment_Speed_2.TabIndex = 243;
            this.btnSet_Concentrick_Adjustment_Speed_2.Text = "Set";
            this.btnSet_Concentrick_Adjustment_Speed_2.UseVisualStyleBackColor = false;
            this.btnSet_Concentrick_Adjustment_Speed_2.Click += new System.EventHandler(this.btnSet_Concentrick_Adjustment_Speed_2_Click);
            // 
            // txtConcentrick_Adjustment_Speed_2
            // 
            this.txtConcentrick_Adjustment_Speed_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcentrick_Adjustment_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtConcentrick_Adjustment_Speed_2.Location = new System.Drawing.Point(214, 215);
            this.txtConcentrick_Adjustment_Speed_2.Name = "txtConcentrick_Adjustment_Speed_2";
            this.txtConcentrick_Adjustment_Speed_2.Size = new System.Drawing.Size(112, 27);
            this.txtConcentrick_Adjustment_Speed_2.TabIndex = 242;
            // 
            // btnGet_Concentrick_Adjustment_Speed_2
            // 
            this.btnGet_Concentrick_Adjustment_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Concentrick_Adjustment_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Concentrick_Adjustment_Speed_2.Location = new System.Drawing.Point(337, 212);
            this.btnGet_Concentrick_Adjustment_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Concentrick_Adjustment_Speed_2.Name = "btnGet_Concentrick_Adjustment_Speed_2";
            this.btnGet_Concentrick_Adjustment_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Concentrick_Adjustment_Speed_2.TabIndex = 244;
            this.btnGet_Concentrick_Adjustment_Speed_2.Text = "Get";
            this.btnGet_Concentrick_Adjustment_Speed_2.UseVisualStyleBackColor = false;
            this.btnGet_Concentrick_Adjustment_Speed_2.Click += new System.EventHandler(this.btnGet_Concentrick_Adjustment_Speed_2_Click);
            // 
            // btnSet_Concentrick_Adjustment_2
            // 
            this.btnSet_Concentrick_Adjustment_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Concentrick_Adjustment_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Concentrick_Adjustment_2.Location = new System.Drawing.Point(398, 173);
            this.btnSet_Concentrick_Adjustment_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Concentrick_Adjustment_2.Name = "btnSet_Concentrick_Adjustment_2";
            this.btnSet_Concentrick_Adjustment_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Concentrick_Adjustment_2.TabIndex = 240;
            this.btnSet_Concentrick_Adjustment_2.Text = "Set";
            this.btnSet_Concentrick_Adjustment_2.UseVisualStyleBackColor = false;
            this.btnSet_Concentrick_Adjustment_2.Click += new System.EventHandler(this.btnSet_Concentrick_Adjustment_Position_2_Click);
            // 
            // txtConcentrick_Adjustment_Position_2
            // 
            this.txtConcentrick_Adjustment_Position_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcentrick_Adjustment_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtConcentrick_Adjustment_Position_2.Location = new System.Drawing.Point(214, 176);
            this.txtConcentrick_Adjustment_Position_2.Name = "txtConcentrick_Adjustment_Position_2";
            this.txtConcentrick_Adjustment_Position_2.Size = new System.Drawing.Size(112, 27);
            this.txtConcentrick_Adjustment_Position_2.TabIndex = 239;
            // 
            // btnGet_Concentrick_Adjustment_2
            // 
            this.btnGet_Concentrick_Adjustment_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Concentrick_Adjustment_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Concentrick_Adjustment_2.Location = new System.Drawing.Point(337, 173);
            this.btnGet_Concentrick_Adjustment_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Concentrick_Adjustment_2.Name = "btnGet_Concentrick_Adjustment_2";
            this.btnGet_Concentrick_Adjustment_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Concentrick_Adjustment_2.TabIndex = 241;
            this.btnGet_Concentrick_Adjustment_2.Text = "Get";
            this.btnGet_Concentrick_Adjustment_2.UseVisualStyleBackColor = false;
            this.btnGet_Concentrick_Adjustment_2.Click += new System.EventHandler(this.btnGet_Concentrick_Adjustment_Position_2_Click);
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label28.Location = new System.Drawing.Point(11, 170);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(190, 39);
            this.label28.TabIndex = 238;
            this.label28.Text = "Concentrick Adjustment Position";
            // 
            // btnSet_Concentrick_Speed_2
            // 
            this.btnSet_Concentrick_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Concentrick_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Concentrick_Speed_2.Location = new System.Drawing.Point(398, 136);
            this.btnSet_Concentrick_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Concentrick_Speed_2.Name = "btnSet_Concentrick_Speed_2";
            this.btnSet_Concentrick_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Concentrick_Speed_2.TabIndex = 234;
            this.btnSet_Concentrick_Speed_2.Text = "Set";
            this.btnSet_Concentrick_Speed_2.UseVisualStyleBackColor = false;
            this.btnSet_Concentrick_Speed_2.Click += new System.EventHandler(this.btnSet_Concentrick_Speed_2_Click);
            // 
            // btnGet_Concentrick_Position_2
            // 
            this.btnGet_Concentrick_Position_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Concentrick_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Concentrick_Position_2.Location = new System.Drawing.Point(337, 99);
            this.btnGet_Concentrick_Position_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Concentrick_Position_2.Name = "btnGet_Concentrick_Position_2";
            this.btnGet_Concentrick_Position_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Concentrick_Position_2.TabIndex = 237;
            this.btnGet_Concentrick_Position_2.Text = "Get";
            this.btnGet_Concentrick_Position_2.UseVisualStyleBackColor = false;
            this.btnGet_Concentrick_Position_2.Click += new System.EventHandler(this.btnGet_Concentrick_Position_2_Click);
            // 
            // txtConcentrick_Position_2
            // 
            this.txtConcentrick_Position_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcentrick_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtConcentrick_Position_2.Location = new System.Drawing.Point(214, 102);
            this.txtConcentrick_Position_2.Name = "txtConcentrick_Position_2";
            this.txtConcentrick_Position_2.Size = new System.Drawing.Size(112, 27);
            this.txtConcentrick_Position_2.TabIndex = 233;
            // 
            // btnSet_Concentrick_Position_2
            // 
            this.btnSet_Concentrick_Position_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Concentrick_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Concentrick_Position_2.Location = new System.Drawing.Point(398, 99);
            this.btnSet_Concentrick_Position_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Concentrick_Position_2.Name = "btnSet_Concentrick_Position_2";
            this.btnSet_Concentrick_Position_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Concentrick_Position_2.TabIndex = 235;
            this.btnSet_Concentrick_Position_2.Text = "Set";
            this.btnSet_Concentrick_Position_2.UseVisualStyleBackColor = false;
            this.btnSet_Concentrick_Position_2.Click += new System.EventHandler(this.btnSet_Concentrick_Position_2_Click);
            // 
            // txtConcentrick_Speed_2
            // 
            this.txtConcentrick_Speed_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcentrick_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtConcentrick_Speed_2.Location = new System.Drawing.Point(214, 139);
            this.txtConcentrick_Speed_2.Name = "txtConcentrick_Speed_2";
            this.txtConcentrick_Speed_2.Size = new System.Drawing.Size(112, 27);
            this.txtConcentrick_Speed_2.TabIndex = 232;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label29.Location = new System.Drawing.Point(11, 104);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(196, 22);
            this.label29.TabIndex = 231;
            this.label29.Text = "Start concentrick_Position";
            // 
            // btnGet_Concentrick_Speed_2
            // 
            this.btnGet_Concentrick_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Concentrick_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Concentrick_Speed_2.Location = new System.Drawing.Point(337, 136);
            this.btnGet_Concentrick_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Concentrick_Speed_2.Name = "btnGet_Concentrick_Speed_2";
            this.btnGet_Concentrick_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Concentrick_Speed_2.TabIndex = 236;
            this.btnGet_Concentrick_Speed_2.Text = "Get";
            this.btnGet_Concentrick_Speed_2.UseVisualStyleBackColor = false;
            this.btnGet_Concentrick_Speed_2.Click += new System.EventHandler(this.btnGet_Concentrick_Speed_2_Click);
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label30.Location = new System.Drawing.Point(11, 141);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(196, 22);
            this.label30.TabIndex = 230;
            this.label30.Text = "Start concentrick_Speed";
            // 
            // btnSet_Ready_Speed_2
            // 
            this.btnSet_Ready_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Ready_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Ready_Speed_2.Location = new System.Drawing.Point(398, 62);
            this.btnSet_Ready_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Ready_Speed_2.Name = "btnSet_Ready_Speed_2";
            this.btnSet_Ready_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Ready_Speed_2.TabIndex = 226;
            this.btnSet_Ready_Speed_2.Text = "Set";
            this.btnSet_Ready_Speed_2.UseVisualStyleBackColor = false;
            this.btnSet_Ready_Speed_2.Click += new System.EventHandler(this.btnSet_Ready_Speed_2_Click);
            // 
            // btnGet_Ready_Position_2
            // 
            this.btnGet_Ready_Position_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Ready_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Ready_Position_2.Location = new System.Drawing.Point(337, 25);
            this.btnGet_Ready_Position_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Ready_Position_2.Name = "btnGet_Ready_Position_2";
            this.btnGet_Ready_Position_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Ready_Position_2.TabIndex = 229;
            this.btnGet_Ready_Position_2.Text = "Get";
            this.btnGet_Ready_Position_2.UseVisualStyleBackColor = false;
            this.btnGet_Ready_Position_2.Click += new System.EventHandler(this.btnGet_Ready_Position_2_Click);
            // 
            // txtReady_Position_2
            // 
            this.txtReady_Position_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReady_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReady_Position_2.Location = new System.Drawing.Point(214, 28);
            this.txtReady_Position_2.Name = "txtReady_Position_2";
            this.txtReady_Position_2.Size = new System.Drawing.Size(112, 27);
            this.txtReady_Position_2.TabIndex = 225;
            // 
            // btnSet_Ready_Position_2
            // 
            this.btnSet_Ready_Position_2.BackColor = System.Drawing.Color.White;
            this.btnSet_Ready_Position_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Ready_Position_2.Location = new System.Drawing.Point(398, 25);
            this.btnSet_Ready_Position_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Ready_Position_2.Name = "btnSet_Ready_Position_2";
            this.btnSet_Ready_Position_2.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Ready_Position_2.TabIndex = 227;
            this.btnSet_Ready_Position_2.Text = "Set";
            this.btnSet_Ready_Position_2.UseVisualStyleBackColor = false;
            this.btnSet_Ready_Position_2.Click += new System.EventHandler(this.btnSet_Ready_Position_2_Click);
            // 
            // txtReady_Speed_2
            // 
            this.txtReady_Speed_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReady_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReady_Speed_2.Location = new System.Drawing.Point(214, 65);
            this.txtReady_Speed_2.Name = "txtReady_Speed_2";
            this.txtReady_Speed_2.Size = new System.Drawing.Size(112, 27);
            this.txtReady_Speed_2.TabIndex = 224;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label31.Location = new System.Drawing.Point(11, 30);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(180, 22);
            this.label31.TabIndex = 223;
            this.label31.Text = "Ready_Position";
            // 
            // btnGet_Ready_Speed_2
            // 
            this.btnGet_Ready_Speed_2.BackColor = System.Drawing.Color.White;
            this.btnGet_Ready_Speed_2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Ready_Speed_2.Location = new System.Drawing.Point(337, 62);
            this.btnGet_Ready_Speed_2.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Ready_Speed_2.Name = "btnGet_Ready_Speed_2";
            this.btnGet_Ready_Speed_2.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Ready_Speed_2.TabIndex = 228;
            this.btnGet_Ready_Speed_2.Text = "Get";
            this.btnGet_Ready_Speed_2.UseVisualStyleBackColor = false;
            this.btnGet_Ready_Speed_2.Click += new System.EventHandler(this.btnGet_Ready_Speed_2_Click);
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label32.Location = new System.Drawing.Point(11, 67);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(180, 22);
            this.label32.TabIndex = 222;
            this.label32.Text = "Ready_Speed";
            // 
            // btnCancelTeaching
            // 
            this.btnCancelTeaching.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTeaching.Location = new System.Drawing.Point(1666, 14);
            this.btnCancelTeaching.Name = "btnCancelTeaching";
            this.btnCancelTeaching.Size = new System.Drawing.Size(44, 25);
            this.btnCancelTeaching.TabIndex = 63;
            // 
            // label131
            // 
            this.label131.Dock = System.Windows.Forms.DockStyle.Top;
            this.label131.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.label131.Location = new System.Drawing.Point(0, 0);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(1216, 44);
            this.label131.TabIndex = 0;
            this.label131.Text = "TEACHING AXIS";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.btnSet_Grinding_Speed_1);
            this.groupBox12.Controls.Add(this.txtGrinding_Speed_1);
            this.groupBox12.Controls.Add(this.btnGet_Grinding_Speed_1);
            this.groupBox12.Controls.Add(this.label27);
            this.groupBox12.Controls.Add(this.btnSet_Grinding_Position_1);
            this.groupBox12.Controls.Add(this.btnGet_RotateMinus_1);
            this.groupBox12.Controls.Add(this.txtRotateMinus_1);
            this.groupBox12.Controls.Add(this.btnSet_RotateMinus_1);
            this.groupBox12.Controls.Add(this.txtGrinding_Position_1);
            this.groupBox12.Controls.Add(this.label25);
            this.groupBox12.Controls.Add(this.btnGet_Grinding_Position_1);
            this.groupBox12.Controls.Add(this.label26);
            this.groupBox12.Controls.Add(this.btnSet_RotatePlus_1);
            this.groupBox12.Controls.Add(this.btnGet_Offsetcheckvision_1);
            this.groupBox12.Controls.Add(this.txtOffsetcheckvision_Speed_1);
            this.groupBox12.Controls.Add(this.btnSet_Offsetcheckvision_1);
            this.groupBox12.Controls.Add(this.txtRotatePlus_Speed_1);
            this.groupBox12.Controls.Add(this.label5);
            this.groupBox12.Controls.Add(this.btnGet_RotatePlus_1);
            this.groupBox12.Controls.Add(this.label1);
            this.groupBox12.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.groupBox12.Location = new System.Drawing.Point(4, 54);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox12.Size = new System.Drawing.Size(471, 217);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "AXIS #1";
            // 
            // btnSet_Grinding_Speed_1
            // 
            this.btnSet_Grinding_Speed_1.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Speed_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Speed_1.Location = new System.Drawing.Point(397, 173);
            this.btnSet_Grinding_Speed_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Speed_1.Name = "btnSet_Grinding_Speed_1";
            this.btnSet_Grinding_Speed_1.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Speed_1.TabIndex = 240;
            this.btnSet_Grinding_Speed_1.Text = "Set";
            this.btnSet_Grinding_Speed_1.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Speed_1.Click += new System.EventHandler(this.btnSet_Grinding_Speed_1_Click);
            // 
            // txtGrinding_Speed_1
            // 
            this.txtGrinding_Speed_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Speed_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Speed_1.Location = new System.Drawing.Point(213, 176);
            this.txtGrinding_Speed_1.Name = "txtGrinding_Speed_1";
            this.txtGrinding_Speed_1.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Speed_1.TabIndex = 239;
            // 
            // btnGet_Grinding_Speed_1
            // 
            this.btnGet_Grinding_Speed_1.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Speed_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Speed_1.Location = new System.Drawing.Point(336, 173);
            this.btnGet_Grinding_Speed_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Speed_1.Name = "btnGet_Grinding_Speed_1";
            this.btnGet_Grinding_Speed_1.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Speed_1.TabIndex = 241;
            this.btnGet_Grinding_Speed_1.Text = "Get";
            this.btnGet_Grinding_Speed_1.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Speed_1.Click += new System.EventHandler(this.btnGet_Grinding_Speed_Click);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label27.Location = new System.Drawing.Point(11, 178);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(196, 22);
            this.label27.TabIndex = 238;
            this.label27.Text = "Rotate Grinding_Speed";
            // 
            // btnSet_Grinding_Position_1
            // 
            this.btnSet_Grinding_Position_1.BackColor = System.Drawing.Color.White;
            this.btnSet_Grinding_Position_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Grinding_Position_1.Location = new System.Drawing.Point(397, 136);
            this.btnSet_Grinding_Position_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Grinding_Position_1.Name = "btnSet_Grinding_Position_1";
            this.btnSet_Grinding_Position_1.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Grinding_Position_1.TabIndex = 234;
            this.btnSet_Grinding_Position_1.Text = "Set";
            this.btnSet_Grinding_Position_1.UseVisualStyleBackColor = false;
            this.btnSet_Grinding_Position_1.Click += new System.EventHandler(this.btnSet_Grinding_Position_Click);
            // 
            // btnGet_RotateMinus_1
            // 
            this.btnGet_RotateMinus_1.BackColor = System.Drawing.Color.White;
            this.btnGet_RotateMinus_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_RotateMinus_1.Location = new System.Drawing.Point(336, 99);
            this.btnGet_RotateMinus_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_RotateMinus_1.Name = "btnGet_RotateMinus_1";
            this.btnGet_RotateMinus_1.Size = new System.Drawing.Size(52, 32);
            this.btnGet_RotateMinus_1.TabIndex = 237;
            this.btnGet_RotateMinus_1.Text = "Get";
            this.btnGet_RotateMinus_1.UseVisualStyleBackColor = false;
            this.btnGet_RotateMinus_1.Click += new System.EventHandler(this.btnGet_RotateMinus_Speed_1_Click);
            // 
            // txtRotateMinus_1
            // 
            this.txtRotateMinus_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRotateMinus_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtRotateMinus_1.Location = new System.Drawing.Point(213, 102);
            this.txtRotateMinus_1.Name = "txtRotateMinus_1";
            this.txtRotateMinus_1.Size = new System.Drawing.Size(112, 27);
            this.txtRotateMinus_1.TabIndex = 233;
            // 
            // btnSet_RotateMinus_1
            // 
            this.btnSet_RotateMinus_1.BackColor = System.Drawing.Color.White;
            this.btnSet_RotateMinus_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_RotateMinus_1.Location = new System.Drawing.Point(397, 99);
            this.btnSet_RotateMinus_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_RotateMinus_1.Name = "btnSet_RotateMinus_1";
            this.btnSet_RotateMinus_1.Size = new System.Drawing.Size(52, 32);
            this.btnSet_RotateMinus_1.TabIndex = 235;
            this.btnSet_RotateMinus_1.Text = "Set";
            this.btnSet_RotateMinus_1.UseVisualStyleBackColor = false;
            this.btnSet_RotateMinus_1.Click += new System.EventHandler(this.btnSet_RotateMinus_Speed_1_Click);
            // 
            // txtGrinding_Position_1
            // 
            this.txtGrinding_Position_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrinding_Position_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtGrinding_Position_1.Location = new System.Drawing.Point(213, 139);
            this.txtGrinding_Position_1.Name = "txtGrinding_Position_1";
            this.txtGrinding_Position_1.Size = new System.Drawing.Size(112, 27);
            this.txtGrinding_Position_1.TabIndex = 232;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label25.Location = new System.Drawing.Point(11, 104);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(180, 22);
            this.label25.TabIndex = 231;
            this.label25.Text = "Rotate -90_Speed";
            // 
            // btnGet_Grinding_Position_1
            // 
            this.btnGet_Grinding_Position_1.BackColor = System.Drawing.Color.White;
            this.btnGet_Grinding_Position_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Grinding_Position_1.Location = new System.Drawing.Point(336, 136);
            this.btnGet_Grinding_Position_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Grinding_Position_1.Name = "btnGet_Grinding_Position_1";
            this.btnGet_Grinding_Position_1.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Grinding_Position_1.TabIndex = 236;
            this.btnGet_Grinding_Position_1.Text = "Get";
            this.btnGet_Grinding_Position_1.UseVisualStyleBackColor = false;
            this.btnGet_Grinding_Position_1.Click += new System.EventHandler(this.btnGet_Grinding_Position_Click);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label26.Location = new System.Drawing.Point(11, 141);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(196, 22);
            this.label26.TabIndex = 230;
            this.label26.Text = "Rotate Grinding_Position";
            // 
            // btnSet_RotatePlus_1
            // 
            this.btnSet_RotatePlus_1.BackColor = System.Drawing.Color.White;
            this.btnSet_RotatePlus_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_RotatePlus_1.Location = new System.Drawing.Point(397, 62);
            this.btnSet_RotatePlus_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_RotatePlus_1.Name = "btnSet_RotatePlus_1";
            this.btnSet_RotatePlus_1.Size = new System.Drawing.Size(52, 32);
            this.btnSet_RotatePlus_1.TabIndex = 226;
            this.btnSet_RotatePlus_1.Text = "Set";
            this.btnSet_RotatePlus_1.UseVisualStyleBackColor = false;
            this.btnSet_RotatePlus_1.Click += new System.EventHandler(this.btnSet_RotatePlus_Speed_1_Click);
            // 
            // btnGet_Offsetcheckvision_1
            // 
            this.btnGet_Offsetcheckvision_1.BackColor = System.Drawing.Color.White;
            this.btnGet_Offsetcheckvision_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_Offsetcheckvision_1.Location = new System.Drawing.Point(336, 25);
            this.btnGet_Offsetcheckvision_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_Offsetcheckvision_1.Name = "btnGet_Offsetcheckvision_1";
            this.btnGet_Offsetcheckvision_1.Size = new System.Drawing.Size(52, 32);
            this.btnGet_Offsetcheckvision_1.TabIndex = 229;
            this.btnGet_Offsetcheckvision_1.Text = "Get";
            this.btnGet_Offsetcheckvision_1.UseVisualStyleBackColor = false;
            this.btnGet_Offsetcheckvision_1.Click += new System.EventHandler(this.btnGet_Offsetcheckvision_Speed_1_Click);
            // 
            // txtOffsetcheckvision_Speed_1
            // 
            this.txtOffsetcheckvision_Speed_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOffsetcheckvision_Speed_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtOffsetcheckvision_Speed_1.Location = new System.Drawing.Point(213, 28);
            this.txtOffsetcheckvision_Speed_1.Name = "txtOffsetcheckvision_Speed_1";
            this.txtOffsetcheckvision_Speed_1.Size = new System.Drawing.Size(112, 27);
            this.txtOffsetcheckvision_Speed_1.TabIndex = 225;
            // 
            // btnSet_Offsetcheckvision_1
            // 
            this.btnSet_Offsetcheckvision_1.BackColor = System.Drawing.Color.White;
            this.btnSet_Offsetcheckvision_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSet_Offsetcheckvision_1.Location = new System.Drawing.Point(397, 25);
            this.btnSet_Offsetcheckvision_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSet_Offsetcheckvision_1.Name = "btnSet_Offsetcheckvision_1";
            this.btnSet_Offsetcheckvision_1.Size = new System.Drawing.Size(52, 32);
            this.btnSet_Offsetcheckvision_1.TabIndex = 227;
            this.btnSet_Offsetcheckvision_1.Text = "Set";
            this.btnSet_Offsetcheckvision_1.UseVisualStyleBackColor = false;
            this.btnSet_Offsetcheckvision_1.Click += new System.EventHandler(this.btnSet_Offsetcheckvision_Speed_1_Click);
            // 
            // txtRotatePlus_Speed_1
            // 
            this.txtRotatePlus_Speed_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRotatePlus_Speed_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtRotatePlus_Speed_1.Location = new System.Drawing.Point(213, 65);
            this.txtRotatePlus_Speed_1.Name = "txtRotatePlus_Speed_1";
            this.txtRotatePlus_Speed_1.Size = new System.Drawing.Size(112, 27);
            this.txtRotatePlus_Speed_1.TabIndex = 224;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(11, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 22);
            this.label5.TabIndex = 223;
            this.label5.Text = "Offset check vision_Speed";
            // 
            // btnGet_RotatePlus_1
            // 
            this.btnGet_RotatePlus_1.BackColor = System.Drawing.Color.White;
            this.btnGet_RotatePlus_1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnGet_RotatePlus_1.Location = new System.Drawing.Point(336, 62);
            this.btnGet_RotatePlus_1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGet_RotatePlus_1.Name = "btnGet_RotatePlus_1";
            this.btnGet_RotatePlus_1.Size = new System.Drawing.Size(52, 32);
            this.btnGet_RotatePlus_1.TabIndex = 228;
            this.btnGet_RotatePlus_1.Text = "Get";
            this.btnGet_RotatePlus_1.UseVisualStyleBackColor = false;
            this.btnGet_RotatePlus_1.Click += new System.EventHandler(this.btnGet_RotatePlus_Speed_1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(11, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 22);
            this.label1.TabIndex = 222;
            this.label1.Text = "Rotate 90_Speed";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox19);
            this.tabPage3.Location = new System.Drawing.Point(4, 54);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1224, 692);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Information PLC";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.btnSaveDeviceInfor);
            this.groupBox19.Controls.Add(this.txtPlcPort);
            this.groupBox19.Controls.Add(this.label36);
            this.groupBox19.Controls.Add(this.txtPlcIpAddress);
            this.groupBox19.Controls.Add(this.label34);
            this.groupBox19.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox19.Location = new System.Drawing.Point(7, 6);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(344, 190);
            this.groupBox19.TabIndex = 7;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Connect PLC";
            // 
            // btnSaveDeviceInfor
            // 
            this.btnSaveDeviceInfor.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnSaveDeviceInfor.Location = new System.Drawing.Point(27, 123);
            this.btnSaveDeviceInfor.Name = "btnSaveDeviceInfor";
            this.btnSaveDeviceInfor.Size = new System.Drawing.Size(89, 43);
            this.btnSaveDeviceInfor.TabIndex = 10;
            this.btnSaveDeviceInfor.Text = "Save";
            this.btnSaveDeviceInfor.UseVisualStyleBackColor = true;
            this.btnSaveDeviceInfor.Click += new System.EventHandler(this.btnSaveDeviceInfor_Click);
            // 
            // txtPlcPort
            // 
            this.txtPlcPort.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPlcPort.Location = new System.Drawing.Point(124, 76);
            this.txtPlcPort.Name = "txtPlcPort";
            this.txtPlcPort.Size = new System.Drawing.Size(190, 27);
            this.txtPlcPort.TabIndex = 4;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label36.Location = new System.Drawing.Point(23, 80);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(38, 19);
            this.label36.TabIndex = 3;
            this.label36.Text = "Port";
            // 
            // txtPlcIpAddress
            // 
            this.txtPlcIpAddress.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPlcIpAddress.Location = new System.Drawing.Point(124, 31);
            this.txtPlcIpAddress.Name = "txtPlcIpAddress";
            this.txtPlcIpAddress.Size = new System.Drawing.Size(190, 27);
            this.txtPlcIpAddress.TabIndex = 4;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label34.Location = new System.Drawing.Point(23, 35);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(86, 19);
            this.label34.TabIndex = 3;
            this.label34.Text = "IP Address";
            // 
            // btnCloseManualDevice
            // 
            this.btnCloseManualDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseManualDevice.BackColor = System.Drawing.Color.White;
            this.btnCloseManualDevice.Location = new System.Drawing.Point(2685, 4);
            this.btnCloseManualDevice.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseManualDevice.Name = "btnCloseManualDevice";
            this.btnCloseManualDevice.Size = new System.Drawing.Size(57, 39);
            this.btnCloseManualDevice.TabIndex = 64;
            this.btnCloseManualDevice.UseVisualStyleBackColor = false;
            // 
            // frmManual_PLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 752);
            this.Controls.Add(this.pShowManualDevice);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmManual_PLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MANUAL PLC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManualDevice_FormClosed);
            this.Load += new System.EventHandler(this.frmManual_PLC_Load);
            this.pShowManualDevice.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pTeaching.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pShowManualDevice;
        internal Button btnCloseManualDevice;
        internal Button btnJogSpeedAxis2Set;
        internal Button btnJogSpeedAxis1Set;
        internal Button btnJogSpeedAxis3Set;
        internal Button btnJogSpeedAxis2Get;
        internal Button btnJogSpeedAxis1Get;
        internal Button btnJogSpeedAxis3Get;
        internal Label label142;
        internal Label label139;
        internal Label label136;
        internal Label label145;
        internal Label label133;
        internal TextBox txtJogSpeedAxis2;
        internal TextBox txtJogSpeedAxis1;
        internal TextBox txtJogSpeedAxis3;
        private GroupBox groupBox5;
        private Button btnInventorOn;
        private Button btnInventorOff;
        private GroupBox groupBox18;
        private Button btnAirFlowStart_On;
        private Button btnAirFlowStart_Off;
        private GroupBox groupBox13;
        private Button btnAirFlowEnd_On;
        private Button btnAirFlowEnd_Off;
        internal Button btnJogSpeedAxis4Set;
        internal Button btnJogSpeedAxis4Get;
        internal Label label17;
        internal TextBox txtJogSpeedAxis4;
        internal Button btnPositionCurrentAxis4Set;
        internal Button btnPositionRunAxis4Set;
        internal Button btnPositionCurrentAxis4Get;
        internal Button btnPositionRunAxis4Get;
        internal Label label2;
        internal Label label3;
        internal TextBox txtPositionCurrentAxis4;
        internal TextBox txtPositionRunAxis4;
        internal Button btnPositionCurrentAxis2Set;
        internal Button btnPositionCurrentAxis3Set;
        internal Button btnPositionRunAxis2Set;
        internal Button btnPositionCurrentAxis1Set;
        internal Button btnPositionRunAxis3Set;
        internal Button btnPositionRunAxis1Set;
        internal Button btnPositionCurrentAxis2Get;
        internal Button btnPositionCurrentAxis3Get;
        internal Button btnPositionRunAxis2Get;
        internal Button btnPositionCurrentAxis1Get;
        internal Button btnPositionRunAxis3Get;
        internal Button btnPositionRunAxis1Get;
        internal Label label6;
        internal Label label7;
        internal Label label10;
        internal Label label11;
        internal Label label14;
        internal Label label18;
        internal TextBox txtPositionCurrentAxis2;
        internal TextBox txtPositionCurrentAxis3;
        internal TextBox txtPositionRunAxis2;
        internal TextBox txtPositionRunAxis3;
        internal TextBox txtPositionCurrentAxis1;
        internal TextBox txtPositionRunAxis1;
        private GroupBox groupBox1;
        private Button btnSet_JogPlusAxis1;
        private Button btnSet_JogMinusAxis1;
        private GroupBox groupBox2;
        private Button btnSet_JogPlusAxis2;
        private Button btnSet_JogMinusAxis2;
        private GroupBox groupBox3;
        private Button btnSet_JogPlusAxis3;
        private Button btnSet_JogMinusAxis3;
        private GroupBox groupBox4;
        private Button btnSet_JogPlusAxis4;
        private Button btnSet_JogMinusAxis4;
        private Panel panel2;
        internal Button button3;
        private Label label4;
        private GroupBox groupBox9;
        private Button btnSet_ManualPositionRun4;
        private Button btnSet_ManualPositionStop4;
        private GroupBox groupBox8;
        private Button btnSet_ManualPositionRun3;
        private Button btnSet_ManualPositionStop3;
        private GroupBox groupBox6;
        private Button btnSet_ManualPositionRun2;
        private Button btnSet_ManualPositionStop2;
        private GroupBox groupBox7;
        private Button btnSet_ManualPositionRun1;
        private Button btnSet_ManualPositionStop1;
        private GroupBox groupBox14;
        private Button btnOriginExis2;
        private Button btnOriginCompleteExis2;
        private GroupBox groupBox11;
        private Button btnOriginExis4;
        private Button btnOriginCompleteExis4;
        private GroupBox groupBox10;
        private Button btnOriginExis3;
        private Button btnOriginCompleteExis3;
        internal Button btnSet_RotatePlus_1;
        internal Button btnSet_Offsetcheckvision_1;
        internal Button btnGet_RotatePlus_1;
        internal Button btnGet_Offsetcheckvision_1;
        internal Label label1;
        internal Label label5;
        internal TextBox txtRotatePlus_Speed_1;
        internal TextBox txtOffsetcheckvision_Speed_1;
        private Panel pTeaching;
        private GroupBox groupBox16;
        internal Button btnSet_Offset_Grinding_Position_3;
        internal TextBox txtOffset_Grinding_Position_3;
        internal Button btnGet_Offset_Grinding_Position_3;
        internal Label label8;
        internal Button btnSet_Grinding_Speed_3;
        internal Button btnGet_Grinding_Position_3;
        internal TextBox txtGrinding_Position_3;
        internal Button btnSet_Grinding_Position_3;
        internal TextBox txtGrinding_Speed_3;
        internal Label label9;
        internal Button btnGet_Grinding_Speed_3;
        internal Label label12;
        internal Button btnSet_Ready_Speed_3;
        internal Button btnGet_Ready_Position_3;
        internal TextBox txtReady_Position_3;
        internal Button btnSet_Ready_Position_3;
        internal TextBox txtReady_Speed_3;
        internal Label label13;
        internal Button btnGet_Ready_Speed_3;
        internal Label label15;
        private GroupBox groupBox15;
        internal Button btnSet_Concentrick_Adjustment_2;
        internal TextBox txtConcentrick_Adjustment_Position_2;
        internal Button btnGet_Concentrick_Adjustment_2;
        internal Label label28;
        internal Button btnSet_Concentrick_Speed_2;
        internal Button btnGet_Concentrick_Position_2;
        internal TextBox txtConcentrick_Position_2;
        internal Button btnSet_Concentrick_Position_2;
        internal TextBox txtConcentrick_Speed_2;
        internal Label label29;
        internal Button btnGet_Concentrick_Speed_2;
        internal Label label30;
        internal Button btnSet_Ready_Speed_2;
        internal Button btnGet_Ready_Position_2;
        internal TextBox txtReady_Position_2;
        internal Button btnSet_Ready_Position_2;
        internal TextBox txtReady_Speed_2;
        internal Label label31;
        internal Button btnGet_Ready_Speed_2;
        internal Label label32;
        private GroupBox groupBox12;
        internal Button btnSet_Grinding_Speed_1;
        internal TextBox txtGrinding_Speed_1;
        internal Button btnGet_Grinding_Speed_1;
        internal Label label27;
        internal Button btnSet_Grinding_Position_1;
        internal Button btnGet_RotateMinus_1;
        internal TextBox txtRotateMinus_1;
        internal Button btnSet_RotateMinus_1;
        internal TextBox txtGrinding_Position_1;
        internal Label label25;
        internal Button btnGet_Grinding_Position_1;
        internal Label label26;
        internal Button btnCancelTeaching;
        private Label label131;
        internal Button btnSet_Offset_Grinding_Speed_3;
        internal TextBox txtOffset_Grinding_Speed_3;
        internal Button btnGet_Offset_Grinding_Speed_3;
        internal Label label16;
        private GroupBox groupBox17;
        internal Button btnSet_Grinding_L_Speed_4;
        internal TextBox txtGrinding_L_Speed_4;
        internal Button btnGet_Grinding_L_Speed_4;
        internal Label label19;
        internal Button btnSet_Grinding_L_Position_4;
        internal TextBox txtGrinding_L_Position_4;
        internal Button btnGet_Grinding_L_Position_4;
        internal Label label20;
        internal Button btnSet_Grinding_R_Speed_4;
        internal Button btnGet_Grinding_R_Position_4;
        internal TextBox txtGrinding_R_Position_4;
        internal Button btnSet_Grinding_R_Position_4;
        internal TextBox txtGrinding_R_Speed_4;
        internal Label label21;
        internal Button btnGet_Grinding_R_Speed_4;
        internal Label label22;
        internal Button btnSet_Grinding_Speed_4;
        internal Button btnGet_Grinding_Position_4;
        internal TextBox txtGrinding_Position_4;
        internal Button btnSet_Grinding_Position_4;
        internal TextBox txtGrinding_Speed_4;
        internal Label label23;
        internal Button btnGet_Grinding_Speed_4;
        internal Label label24;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        internal Label label33;
        internal Button btnSet_Concentrick_Adjustment_Speed_2;
        internal TextBox txtConcentrick_Adjustment_Speed_2;
        internal Button btnGet_Concentrick_Adjustment_Speed_2;
        private TabPage tabPage3;
        private GroupBox groupBox19;
        private TextBox txtPlcPort;
        private Label label36;
        private TextBox txtPlcIpAddress;
        private Label label34;
        private Button btnSaveDeviceInfor;
    }
}