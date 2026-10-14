
namespace AutoBrutingDiamond
{
    partial class FrmHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistory));
            this.grdHistory = new DevExpress.XtraGrid.GridControl();
            this.grvHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiamondCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDatetime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDepth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDistanceOriginalMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDistanceOriginalMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCrowHeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiameterRequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvalMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvalMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDatetimeEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCycleConcentric = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCycleGrinding = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvalAverage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiameterAverage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.dtpTo = new DevExpress.XtraEditors.DateEdit();
            this.dtpFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdHistory
            // 
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.Location = new System.Drawing.Point(0, 67);
            this.grdHistory.MainView = this.grvHistory;
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdHistory.Size = new System.Drawing.Size(1592, 596);
            this.grdHistory.TabIndex = 78;
            this.grdHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvHistory,
            this.gridView1});
            // 
            // grvHistory
            // 
            this.grvHistory.ColumnPanelRowHeight = 48;
            this.grvHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.gridColumn1,
            this.colDiamondCode,
            this.colDatetime,
            this.colTotalDepth,
            this.colDistanceOriginalMin,
            this.colDistanceOriginalMax,
            this.colCrowHeight,
            this.colDiameterRequest,
            this.colOvalMin,
            this.colOvalMax,
            this.colDatetimeEnd,
            this.colCycleConcentric,
            this.colCycleGrinding,
            this.colOvalAverage,
            this.colDiameterAverage,
            this.gridColumn2,
            this.gridColumn3});
            this.grvHistory.GridControl = this.grdHistory;
            this.grvHistory.Name = "grvHistory";
            this.grvHistory.OptionsBehavior.ReadOnly = true;
            this.grvHistory.OptionsFind.AlwaysVisible = true;
            this.grvHistory.OptionsFind.FindNullPrompt = "Nhập thông tin tìm kiếm ...";
            this.grvHistory.OptionsView.AllowCellMerge = true;
            this.grvHistory.OptionsView.ColumnAutoWidth = false;
            this.grvHistory.OptionsView.RowAutoHeight = true;
            this.grvHistory.OptionsView.ShowFooter = true;
            this.grvHistory.OptionsView.ShowGroupPanel = false;
            this.grvHistory.RowHeight = 35;
            this.grvHistory.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.grvHistory_CustomUnboundColumnData);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.gridColumn1.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "No";
            this.gridColumn1.FieldName = "No";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 46;
            // 
            // colDiamondCode
            // 
            this.colDiamondCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDiamondCode.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDiamondCode.AppearanceCell.Options.HighPriority = true;
            this.colDiamondCode.AppearanceCell.Options.UseFont = true;
            this.colDiamondCode.AppearanceCell.Options.UseForeColor = true;
            this.colDiamondCode.AppearanceCell.Options.UseTextOptions = true;
            this.colDiamondCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiamondCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiamondCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDiamondCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDiamondCode.AppearanceHeader.Options.UseBackColor = true;
            this.colDiamondCode.AppearanceHeader.Options.UseFont = true;
            this.colDiamondCode.AppearanceHeader.Options.UseForeColor = true;
            this.colDiamondCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiamondCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiamondCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiamondCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiamondCode.Caption = "Code";
            this.colDiamondCode.FieldName = "DiamondCode";
            this.colDiamondCode.Name = "colDiamondCode";
            this.colDiamondCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDiamondCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "DiamondCode", "COUNT: {0}")});
            this.colDiamondCode.Visible = true;
            this.colDiamondCode.VisibleIndex = 1;
            this.colDiamondCode.Width = 91;
            // 
            // colDatetime
            // 
            this.colDatetime.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDatetime.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDatetime.AppearanceCell.Options.HighPriority = true;
            this.colDatetime.AppearanceCell.Options.UseFont = true;
            this.colDatetime.AppearanceCell.Options.UseForeColor = true;
            this.colDatetime.AppearanceCell.Options.UseTextOptions = true;
            this.colDatetime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatetime.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatetime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDatetime.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDatetime.AppearanceHeader.Options.UseBackColor = true;
            this.colDatetime.AppearanceHeader.Options.UseFont = true;
            this.colDatetime.AppearanceHeader.Options.UseForeColor = true;
            this.colDatetime.AppearanceHeader.Options.UseTextOptions = true;
            this.colDatetime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDatetime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatetime.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatetime.Caption = "Thời gian bắt đầu";
            this.colDatetime.DisplayFormat.FormatString = "HH:mm:ss dd/MM/yyyy";
            this.colDatetime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDatetime.FieldName = "DateTimeStart";
            this.colDatetime.Name = "colDatetime";
            this.colDatetime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDatetime.Visible = true;
            this.colDatetime.VisibleIndex = 2;
            this.colDatetime.Width = 154;
            // 
            // colTotalDepth
            // 
            this.colTotalDepth.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colTotalDepth.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colTotalDepth.AppearanceCell.Options.UseFont = true;
            this.colTotalDepth.AppearanceCell.Options.UseForeColor = true;
            this.colTotalDepth.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDepth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDepth.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDepth.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDepth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colTotalDepth.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalDepth.AppearanceHeader.Options.UseFont = true;
            this.colTotalDepth.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDepth.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDepth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDepth.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDepth.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDepth.Caption = "Chiều cao viên (%)";
            this.colTotalDepth.FieldName = "TotalDepth";
            this.colTotalDepth.Name = "colTotalDepth";
            this.colTotalDepth.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalDepth.Visible = true;
            this.colTotalDepth.VisibleIndex = 12;
            this.colTotalDepth.Width = 87;
            // 
            // colDistanceOriginalMin
            // 
            this.colDistanceOriginalMin.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDistanceOriginalMin.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDistanceOriginalMin.AppearanceCell.Options.UseFont = true;
            this.colDistanceOriginalMin.AppearanceCell.Options.UseForeColor = true;
            this.colDistanceOriginalMin.AppearanceCell.Options.UseTextOptions = true;
            this.colDistanceOriginalMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDistanceOriginalMin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDistanceOriginalMin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDistanceOriginalMin.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDistanceOriginalMin.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDistanceOriginalMin.AppearanceHeader.Options.UseFont = true;
            this.colDistanceOriginalMin.AppearanceHeader.Options.UseForeColor = true;
            this.colDistanceOriginalMin.AppearanceHeader.Options.UseTextOptions = true;
            this.colDistanceOriginalMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDistanceOriginalMin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDistanceOriginalMin.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDistanceOriginalMin.Caption = "Kích thước viên ban đầu Min";
            this.colDistanceOriginalMin.FieldName = "DistanceOriginalMin";
            this.colDistanceOriginalMin.Name = "colDistanceOriginalMin";
            this.colDistanceOriginalMin.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDistanceOriginalMin.Visible = true;
            this.colDistanceOriginalMin.VisibleIndex = 3;
            this.colDistanceOriginalMin.Width = 127;
            // 
            // colDistanceOriginalMax
            // 
            this.colDistanceOriginalMax.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDistanceOriginalMax.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDistanceOriginalMax.AppearanceCell.Options.UseFont = true;
            this.colDistanceOriginalMax.AppearanceCell.Options.UseForeColor = true;
            this.colDistanceOriginalMax.AppearanceCell.Options.UseTextOptions = true;
            this.colDistanceOriginalMax.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDistanceOriginalMax.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDistanceOriginalMax.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDistanceOriginalMax.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDistanceOriginalMax.AppearanceHeader.Options.UseFont = true;
            this.colDistanceOriginalMax.AppearanceHeader.Options.UseForeColor = true;
            this.colDistanceOriginalMax.AppearanceHeader.Options.UseTextOptions = true;
            this.colDistanceOriginalMax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDistanceOriginalMax.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDistanceOriginalMax.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDistanceOriginalMax.Caption = "Kích thước viên ban đầu Max";
            this.colDistanceOriginalMax.FieldName = "DistanceOriginalMax";
            this.colDistanceOriginalMax.Name = "colDistanceOriginalMax";
            this.colDistanceOriginalMax.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDistanceOriginalMax.Visible = true;
            this.colDistanceOriginalMax.VisibleIndex = 4;
            this.colDistanceOriginalMax.Width = 125;
            // 
            // colCrowHeight
            // 
            this.colCrowHeight.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colCrowHeight.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colCrowHeight.AppearanceCell.Options.UseFont = true;
            this.colCrowHeight.AppearanceCell.Options.UseForeColor = true;
            this.colCrowHeight.AppearanceCell.Options.UseTextOptions = true;
            this.colCrowHeight.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCrowHeight.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCrowHeight.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCrowHeight.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colCrowHeight.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCrowHeight.AppearanceHeader.Options.UseFont = true;
            this.colCrowHeight.AppearanceHeader.Options.UseForeColor = true;
            this.colCrowHeight.AppearanceHeader.Options.UseTextOptions = true;
            this.colCrowHeight.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCrowHeight.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCrowHeight.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCrowHeight.Caption = "Chiều cao TOP (%)";
            this.colCrowHeight.FieldName = "CrowHeight";
            this.colCrowHeight.Name = "colCrowHeight";
            this.colCrowHeight.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCrowHeight.Visible = true;
            this.colCrowHeight.VisibleIndex = 13;
            this.colCrowHeight.Width = 137;
            // 
            // colDiameterRequest
            // 
            this.colDiameterRequest.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDiameterRequest.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDiameterRequest.AppearanceCell.Options.UseFont = true;
            this.colDiameterRequest.AppearanceCell.Options.UseForeColor = true;
            this.colDiameterRequest.AppearanceCell.Options.UseTextOptions = true;
            this.colDiameterRequest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiameterRequest.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiameterRequest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiameterRequest.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDiameterRequest.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDiameterRequest.AppearanceHeader.Options.UseFont = true;
            this.colDiameterRequest.AppearanceHeader.Options.UseForeColor = true;
            this.colDiameterRequest.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiameterRequest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiameterRequest.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiameterRequest.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiameterRequest.Caption = "Kích thước cài đặt";
            this.colDiameterRequest.FieldName = "DistanceGrindingRequest";
            this.colDiameterRequest.Name = "colDiameterRequest";
            this.colDiameterRequest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDiameterRequest.Visible = true;
            this.colDiameterRequest.VisibleIndex = 5;
            this.colDiameterRequest.Width = 95;
            // 
            // colOvalMin
            // 
            this.colOvalMin.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colOvalMin.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colOvalMin.AppearanceCell.Options.UseFont = true;
            this.colOvalMin.AppearanceCell.Options.UseForeColor = true;
            this.colOvalMin.AppearanceCell.Options.UseTextOptions = true;
            this.colOvalMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalMin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalMin.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colOvalMin.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colOvalMin.AppearanceHeader.Options.UseFont = true;
            this.colOvalMin.AppearanceHeader.Options.UseForeColor = true;
            this.colOvalMin.AppearanceHeader.Options.UseTextOptions = true;
            this.colOvalMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalMin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalMin.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOvalMin.Caption = "Kích thước viên sau mài Min";
            this.colOvalMin.FieldName = "OvalMin";
            this.colOvalMin.Name = "colOvalMin";
            this.colOvalMin.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colOvalMin.Visible = true;
            this.colOvalMin.VisibleIndex = 6;
            this.colOvalMin.Width = 126;
            // 
            // colOvalMax
            // 
            this.colOvalMax.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colOvalMax.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colOvalMax.AppearanceCell.Options.UseFont = true;
            this.colOvalMax.AppearanceCell.Options.UseForeColor = true;
            this.colOvalMax.AppearanceCell.Options.UseTextOptions = true;
            this.colOvalMax.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalMax.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalMax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOvalMax.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colOvalMax.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colOvalMax.AppearanceHeader.Options.UseFont = true;
            this.colOvalMax.AppearanceHeader.Options.UseForeColor = true;
            this.colOvalMax.AppearanceHeader.Options.UseTextOptions = true;
            this.colOvalMax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalMax.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalMax.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOvalMax.Caption = "Kích thước viên sau mài Max";
            this.colOvalMax.FieldName = "OvalMax";
            this.colOvalMax.Name = "colOvalMax";
            this.colOvalMax.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colOvalMax.Visible = true;
            this.colOvalMax.VisibleIndex = 7;
            this.colOvalMax.Width = 128;
            // 
            // colDatetimeEnd
            // 
            this.colDatetimeEnd.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDatetimeEnd.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDatetimeEnd.AppearanceCell.Options.UseFont = true;
            this.colDatetimeEnd.AppearanceCell.Options.UseForeColor = true;
            this.colDatetimeEnd.AppearanceCell.Options.UseTextOptions = true;
            this.colDatetimeEnd.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatetimeEnd.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatetimeEnd.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDatetimeEnd.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDatetimeEnd.AppearanceHeader.Options.UseFont = true;
            this.colDatetimeEnd.AppearanceHeader.Options.UseForeColor = true;
            this.colDatetimeEnd.AppearanceHeader.Options.UseTextOptions = true;
            this.colDatetimeEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDatetimeEnd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatetimeEnd.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatetimeEnd.Caption = "Thời gian kết thúc";
            this.colDatetimeEnd.DisplayFormat.FormatString = "HH:mm:ss dd/MM/yyyy";
            this.colDatetimeEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDatetimeEnd.FieldName = "DateTimeEnd";
            this.colDatetimeEnd.Name = "colDatetimeEnd";
            this.colDatetimeEnd.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDatetimeEnd.Visible = true;
            this.colDatetimeEnd.VisibleIndex = 16;
            this.colDatetimeEnd.Width = 160;
            // 
            // colCycleConcentric
            // 
            this.colCycleConcentric.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colCycleConcentric.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colCycleConcentric.AppearanceCell.Options.UseFont = true;
            this.colCycleConcentric.AppearanceCell.Options.UseForeColor = true;
            this.colCycleConcentric.AppearanceCell.Options.UseTextOptions = true;
            this.colCycleConcentric.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCycleConcentric.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCycleConcentric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCycleConcentric.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colCycleConcentric.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCycleConcentric.AppearanceHeader.Options.UseFont = true;
            this.colCycleConcentric.AppearanceHeader.Options.UseForeColor = true;
            this.colCycleConcentric.AppearanceHeader.Options.UseTextOptions = true;
            this.colCycleConcentric.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCycleConcentric.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCycleConcentric.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCycleConcentric.Caption = "Thời gian gõ (s)";
            this.colCycleConcentric.FieldName = "CycleConcentric";
            this.colCycleConcentric.Name = "colCycleConcentric";
            this.colCycleConcentric.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCycleConcentric.Visible = true;
            this.colCycleConcentric.VisibleIndex = 9;
            this.colCycleConcentric.Width = 92;
            // 
            // colCycleGrinding
            // 
            this.colCycleGrinding.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colCycleGrinding.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colCycleGrinding.AppearanceCell.Options.UseFont = true;
            this.colCycleGrinding.AppearanceCell.Options.UseForeColor = true;
            this.colCycleGrinding.AppearanceCell.Options.UseTextOptions = true;
            this.colCycleGrinding.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCycleGrinding.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCycleGrinding.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCycleGrinding.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colCycleGrinding.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCycleGrinding.AppearanceHeader.Options.UseFont = true;
            this.colCycleGrinding.AppearanceHeader.Options.UseForeColor = true;
            this.colCycleGrinding.AppearanceHeader.Options.UseTextOptions = true;
            this.colCycleGrinding.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCycleGrinding.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCycleGrinding.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCycleGrinding.Caption = "Thời gian mài (s)";
            this.colCycleGrinding.FieldName = "CycleGrinding";
            this.colCycleGrinding.Name = "colCycleGrinding";
            this.colCycleGrinding.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colCycleGrinding.Visible = true;
            this.colCycleGrinding.VisibleIndex = 10;
            this.colCycleGrinding.Width = 88;
            // 
            // colOvalAverage
            // 
            this.colOvalAverage.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colOvalAverage.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colOvalAverage.AppearanceCell.Options.UseFont = true;
            this.colOvalAverage.AppearanceCell.Options.UseForeColor = true;
            this.colOvalAverage.AppearanceCell.Options.UseTextOptions = true;
            this.colOvalAverage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalAverage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalAverage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOvalAverage.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colOvalAverage.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colOvalAverage.AppearanceHeader.Options.UseFont = true;
            this.colOvalAverage.AppearanceHeader.Options.UseForeColor = true;
            this.colOvalAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colOvalAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvalAverage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOvalAverage.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOvalAverage.Caption = "Độ tròn";
            this.colOvalAverage.FieldName = "OvalAverage";
            this.colOvalAverage.Name = "colOvalAverage";
            this.colOvalAverage.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colOvalAverage.Visible = true;
            this.colOvalAverage.VisibleIndex = 11;
            this.colOvalAverage.Width = 71;
            // 
            // colDiameterAverage
            // 
            this.colDiameterAverage.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colDiameterAverage.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colDiameterAverage.AppearanceCell.Options.UseFont = true;
            this.colDiameterAverage.AppearanceCell.Options.UseForeColor = true;
            this.colDiameterAverage.AppearanceCell.Options.UseTextOptions = true;
            this.colDiameterAverage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiameterAverage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiameterAverage.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colDiameterAverage.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDiameterAverage.AppearanceHeader.Options.UseFont = true;
            this.colDiameterAverage.AppearanceHeader.Options.UseForeColor = true;
            this.colDiameterAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiameterAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiameterAverage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiameterAverage.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiameterAverage.Caption = "Kích thước sau mài trung bình";
            this.colDiameterAverage.FieldName = "DiameterAverage";
            this.colDiameterAverage.Name = "colDiameterAverage";
            this.colDiameterAverage.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDiameterAverage.Visible = true;
            this.colDiameterAverage.VisibleIndex = 8;
            this.colDiameterAverage.Width = 119;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Chiều dày cắt (µm)";
            this.gridColumn2.FieldName = "Cutting_Value";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 14;
            this.gridColumn2.Width = 111;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Resize";
            this.gridColumn3.FieldName = "IsResize_Target";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 15;
            this.gridColumn3.Width = 103;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdHistory;
            this.gridView1.Name = "gridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.btnOpenFile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1340, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 67);
            this.panel2.TabIndex = 5;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Image = global::AutoBrutingDiamond.Properties.Resources.ConvertToRange_32x32;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportExcel.Location = new System.Drawing.Point(113, 16);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(111, 38);
            this.btnExportExcel.TabIndex = 0;
            this.btnExportExcel.Text = "EXPORT ";
            this.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Image = global::AutoBrutingDiamond.Properties.Resources.Open2_32x32;
            this.btnOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenFile.Location = new System.Drawing.Point(3, 16);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(104, 38);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "OPEN FILE";
            this.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Visible = false;
            // 
            // dtpTo
            // 
            this.dtpTo.EditValue = new System.DateTime(2024, 10, 23, 16, 20, 30, 0);
            this.dtpTo.Location = new System.Drawing.Point(237, 16);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Properties.Appearance.Options.UseFont = true;
            this.dtpTo.Properties.AutoHeight = false;
            this.dtpTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpTo.Properties.NullDate = new System.DateTime(2024, 3, 22, 11, 1, 47, 0);
            this.dtpTo.Properties.NullDateCalendarValue = new System.DateTime(2024, 3, 21, 17, 54, 39, 0);
            this.dtpTo.Size = new System.Drawing.Size(130, 33);
            this.dtpTo.TabIndex = 81;
            this.dtpTo.EditValueChanged += new System.EventHandler(this.dtpTo_EditValueChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.EditValue = new System.DateTime(2024, 10, 23, 16, 20, 2, 0);
            this.dtpFrom.Location = new System.Drawing.Point(69, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Properties.Appearance.Options.UseFont = true;
            this.dtpFrom.Properties.AutoHeight = false;
            this.dtpFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFrom.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpFrom.Properties.NullDate = new System.DateTime(2024, 3, 22, 11, 1, 24, 0);
            this.dtpFrom.Properties.NullDateCalendarValue = new System.DateTime(2024, 3, 22, 11, 1, 10, 0);
            this.dtpFrom.Size = new System.Drawing.Size(130, 33);
            this.dtpFrom.TabIndex = 80;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Location = new System.Drawing.Point(209, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(19, 19);
            this.labelControl4.TabIndex = 82;
            this.labelControl4.Text = "To";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(21, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 19);
            this.labelControl3.TabIndex = 79;
            this.labelControl3.Text = "From";
            // 
            // btnLoad
            // 
            this.btnLoad.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Appearance.Options.UseFont = true;
            this.btnLoad.Appearance.Options.UseForeColor = true;
            this.btnLoad.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLoad.ImageOptions.SvgImage")));
            this.btnLoad.Location = new System.Drawing.Point(387, 14);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(144, 37);
            this.btnLoad.TabIndex = 83;
            this.btnLoad.Text = "LOAD DATA";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1592, 67);
            this.panel1.TabIndex = 0;
            // 
            // FrmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1592, 663);
            this.Controls.Add(this.grdHistory);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HISTORY DATA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView grvHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colDiamondCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDatetime;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDepth;
        private DevExpress.XtraGrid.Columns.GridColumn colDistanceOriginalMin;
        private DevExpress.XtraGrid.Columns.GridColumn colCrowHeight;
        private DevExpress.XtraGrid.Columns.GridColumn colDiameterRequest;
        private DevExpress.XtraGrid.Columns.GridColumn colOvalMax;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnOpenFile;
        private DevExpress.XtraEditors.DateEdit dtpTo;
        private DevExpress.XtraEditors.DateEdit dtpFrom;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn colDatetimeEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colCycleConcentric;
        private DevExpress.XtraGrid.Columns.GridColumn colCycleGrinding;
        private DevExpress.XtraGrid.Columns.GridColumn colOvalAverage;
        private DevExpress.XtraGrid.Columns.GridColumn colDistanceOriginalMax;
        private DevExpress.XtraGrid.Columns.GridColumn colOvalMin;
        private DevExpress.XtraGrid.Columns.GridColumn colDiameterAverage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}