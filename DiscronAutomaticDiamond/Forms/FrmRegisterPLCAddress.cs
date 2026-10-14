using AutoBrutingDiamond.Class;
using AutoBrutingDiamond.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrutingDiamond.Forms
{   
    public partial class FrmRegisterPLCAddress : Form
    {
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        public FrmRegisterPLCAddress()
        {
            InitializeComponent();
        }

        private void FrmRegisterPLCAddress_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<RegisterPlcAddress> lstRegisterAddress = _mySQLHelper.FindAll<RegisterPlcAddress>();
            grdData.DataSource = lstRegisterAddress;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmRegisterAddressDetail frm = new FrmRegisterAddressDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Lib.ToInt(grvData.GetFocusedRowCellValue(colID));
            RegisterPlcAddress addressModel = _mySQLHelper.FindByID<RegisterPlcAddress>(id);
            FrmRegisterAddressDetail frm = new FrmRegisterAddressDetail();
            frm.addressModel = addressModel;
           if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }

        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData) e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedID = Lib.ToInt(grvData.GetFocusedRowCellValue(colID));
            string addressCode = grvData.GetFocusedRowCellValue(colAddressCode).ToString();
            if (MessageBox.Show($"Do you want to delete Register Address {addressCode.ToLower()} ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _mySQLHelper.DeleteByID<RegisterPlcAddress>(selectedID);
                LoadData();
            }

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }
    }
}
