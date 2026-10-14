
namespace AutoBrutingDiamond.Forms
{
    partial class FrmRegisterPLCAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegisterPLCAddress));
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegisterAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsert = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 67);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemButtonEdit1});
            this.grdData.Size = new System.Drawing.Size(1136, 570);
            this.grdData.TabIndex = 81;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData,
            this.gridView1});
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 48;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colNo,
            this.colAddressCode,
            this.colAddressName,
            this.colRegisterAddress});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsFind.FindNullPrompt = "Nhập thông tin tìm kiếm ...";
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowHeight = 35;
            this.grvData.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.grvData_CustomUnboundColumnData);
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colNo
            // 
            this.colNo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colNo.AppearanceCell.Options.UseFont = true;
            this.colNo.AppearanceCell.Options.UseTextOptions = true;
            this.colNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNo.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNo.AppearanceHeader.Options.UseFont = true;
            this.colNo.AppearanceHeader.Options.UseForeColor = true;
            this.colNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNo.Caption = "No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colNo.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 100;
            // 
            // colAddressCode
            // 
            this.colAddressCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colAddressCode.AppearanceCell.Options.HighPriority = true;
            this.colAddressCode.AppearanceCell.Options.UseFont = true;
            this.colAddressCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAddressCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAddressCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAddressCode.AppearanceHeader.Options.UseBackColor = true;
            this.colAddressCode.AppearanceHeader.Options.UseFont = true;
            this.colAddressCode.AppearanceHeader.Options.UseForeColor = true;
            this.colAddressCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddressCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressCode.Caption = "Address Code";
            this.colAddressCode.FieldName = "AddressCode";
            this.colAddressCode.Name = "colAddressCode";
            this.colAddressCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAddressCode.Visible = true;
            this.colAddressCode.VisibleIndex = 2;
            this.colAddressCode.Width = 365;
            // 
            // colAddressName
            // 
            this.colAddressName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colAddressName.AppearanceCell.Options.HighPriority = true;
            this.colAddressName.AppearanceCell.Options.UseFont = true;
            this.colAddressName.AppearanceCell.Options.UseTextOptions = true;
            this.colAddressName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAddressName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAddressName.AppearanceHeader.Options.UseBackColor = true;
            this.colAddressName.AppearanceHeader.Options.UseFont = true;
            this.colAddressName.AppearanceHeader.Options.UseForeColor = true;
            this.colAddressName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddressName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddressName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressName.Caption = "Address Name";
            this.colAddressName.FieldName = "AddressName";
            this.colAddressName.Name = "colAddressName";
            this.colAddressName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAddressName.Visible = true;
            this.colAddressName.VisibleIndex = 3;
            this.colAddressName.Width = 390;
            // 
            // colRegisterAddress
            // 
            this.colRegisterAddress.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.colRegisterAddress.AppearanceCell.Options.UseFont = true;
            this.colRegisterAddress.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRegisterAddress.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colRegisterAddress.AppearanceHeader.Options.UseFont = true;
            this.colRegisterAddress.AppearanceHeader.Options.UseForeColor = true;
            this.colRegisterAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colRegisterAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRegisterAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRegisterAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRegisterAddress.Caption = "Register Address";
            this.colRegisterAddress.FieldName = "RegisterAddress";
            this.colRegisterAddress.Name = "colRegisterAddress";
            this.colRegisterAddress.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colRegisterAddress.Visible = true;
            this.colRegisterAddress.VisibleIndex = 1;
            this.colRegisterAddress.Width = 256;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.Appearance.Options.UseImage = true;
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdData;
            this.gridView1.Name = "gridView1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 67);
            this.panel1.TabIndex = 82;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Appearance.Options.UseForeColor = true;
            this.btnDelete.ImageOptions.Image = global::AutoBrutingDiamond.Properties.Resources.delete_32x32;
            this.btnDelete.Location = new System.Drawing.Point(244, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 58);
            this.btnDelete.TabIndex = 85;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Appearance.Options.UseFont = true;
            this.btnUpdate.Appearance.Options.UseForeColor = true;
            this.btnUpdate.ImageOptions.Image = global::AutoBrutingDiamond.Properties.Resources.edit_32x32;
            this.btnUpdate.Location = new System.Drawing.Point(126, 6);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 58);
            this.btnUpdate.TabIndex = 85;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnInsert.Appearance.Options.UseFont = true;
            this.btnInsert.Appearance.Options.UseForeColor = true;
            this.btnInsert.ImageOptions.Image = global::AutoBrutingDiamond.Properties.Resources.add_32x32;
            this.btnInsert.Location = new System.Drawing.Point(8, 6);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(110, 58);
            this.btnInsert.TabIndex = 85;
            this.btnInsert.Text = "INSERT ";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Appearance.Options.UseForeColor = true;
            this.btnLoad.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLoad.ImageOptions.SvgImage")));
            this.btnLoad.Location = new System.Drawing.Point(360, 6);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(146, 58);
            this.btnLoad.TabIndex = 83;
            this.btnLoad.Text = "LOAD DATA";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // FrmRegisterPLCAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 637);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRegisterPLCAddress";
            this.Text = "REGISTER PLC ADDRES";
            this.Load += new System.EventHandler(this.FrmRegisterPLCAddress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressName;
        private DevExpress.XtraGrid.Columns.GridColumn colRegisterAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnInsert;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
    }
}