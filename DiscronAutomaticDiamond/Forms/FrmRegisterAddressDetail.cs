using AutoBrutingDiamond.Models;
using MySQLLib;
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
    public partial class FrmRegisterAddressDetail : Form
    {
        MySQLLib.MySQLHelper _mySQLHelper = new MySQLLib.MySQLHelper();
        public RegisterPlcAddress addressModel = new RegisterPlcAddress();
        public FrmRegisterAddressDetail()
        {
            InitializeComponent();
        }

        private void FrmRegisterAddressDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if(addressModel.ID > 0)
            {
                txtAddressCode.Text = addressModel.AddressCode;
                txtAddressName.Text = addressModel.AddressName;
                txtRegisterAddress.Text = addressModel.RegisterAddress;
            }
        }

        private bool SaveData()
        {
            if (!ValidateData()) return false;
            addressModel.AddressCode = txtAddressCode.Text.Trim();
            addressModel.AddressName = txtAddressName.Text.Trim();
            addressModel.RegisterAddress = txtRegisterAddress.Text.Trim();
            addressModel.CreatedDate = DateTime.Now;
            try
            {
                if(addressModel.ID > 0)
                {
                    _mySQLHelper.Update<RegisterPlcAddress>(addressModel);
                }
                else
                {
                    _mySQLHelper.Insert<RegisterPlcAddress>(addressModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtRegisterAddress.Text))
            {
                MessageBox.Show("Please enter RegisterAddress !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAddressCode.Text))
            {
                MessageBox.Show("Please enter Address Code !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var exp1 = new Expression("AddressCode", txtAddressCode.Text.Trim());
            var exp2 = new Expression("ID", addressModel.ID, "<>");
            List<RegisterPlcAddress> lstAddressCheck = _mySQLHelper.FindByExpression<RegisterPlcAddress>(exp1.And(exp2));

            if (lstAddressCheck.Count > 0)
            {
                MessageBox.Show("This Address Code is exist ! Please enter new Address Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnSaveCLose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                txtAddressCode.Text = "";
                txtAddressName.Text = "";
                txtRegisterAddress.Text = "";
                addressModel = new RegisterPlcAddress();
            }
        }

        private void FrmRegisterAddressDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
