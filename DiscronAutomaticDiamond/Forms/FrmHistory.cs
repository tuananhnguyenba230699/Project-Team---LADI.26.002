using AutoBrutingDiamond.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using System.Diagnostics;
using AutoBrutingDiamond.Models;

namespace AutoBrutingDiamond
{
    public partial class FrmHistory : Form
    {
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        public FrmHistory()
        {
            InitializeComponent();
        }

        private void FrmHistory_Load(object sender, EventArgs e)
        {
            dtpFrom.DateTime = DateTime.Now.Date.AddDays(-1);
            dtpTo.DateTime = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            LoadData();
        }

        void LoadData()
        {
            List<history_data> listDataHistory = _mySQLHelper.FindAll<history_data>();
            grdHistory.DataSource = listDataHistory.Where(a => a.DateTimeStart > dtpFrom.DateTime && a.DateTimeStart < dtpTo.DateTime).OrderByDescending(x => x.DateTimeStart);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DataHistory_{dtpFrom.DateTime.ToString("ddMMyy")}_{dtpTo.DateTime.ToString("ddMMyy")}.xlsx");

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdHistory;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvHistory_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = grvHistory.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpTo_EditValueChanged(object sender, EventArgs e)
        {
            dtpFrom.Properties.MaxValue = Lib.ToDate(dtpTo.EditValue.ToString());
        }
    }
}
