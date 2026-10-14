using AutoBrutingDiamond.Class;
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

namespace AutoBrutingDiamond
{
    public partial class FrmErrorDetail : Form
    {
        MySQLLib.MySQLHelper _mySqlHelper = new MySQLLib.MySQLHelper();
        public register_error errModel = new register_error();
        public FrmErrorDetail()
        {
            InitializeComponent();
        }
        private void FrmErrorDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if(errModel.ID > 0)
            {
                txtAddress.Text = errModel.Address;
                txtAddressType.Text = errModel.Type;
                txtBitNumber.Text = errModel.BitNumber;
                txtErrorCode.Text = errModel.ErrorCode;
                txtErrorName.Text = errModel.ErrorName;
            }
        }
        private void btnSaveCLose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool SaveData()
        {
            if (!ValidateData()) return false;
            errModel.Type = txtAddressType.Text.Trim();
            errModel.Address = txtAddress.Text.Trim();
            errModel.BitNumber = txtBitNumber.Text.Trim();
            errModel.RegisterAddress = errModel.Type + errModel.Address + "." + errModel.BitNumber;
            errModel.ErrorCode = txtErrorCode.Text.Trim();
            errModel.ErrorName = txtErrorName.Text.Trim();
            errModel.ErrorWarning = errModel.ErrorCode + " ERROR " + errModel.ErrorName;
            try
            {
                if (errModel.ID <= 0)
                {
                    _mySqlHelper.Insert<register_error>(errModel);
                }
                else
                {
                    _mySqlHelper.Update<register_error>(errModel);
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtAddressType.Text))
            {
                MessageBox.Show("Please enter Address Type !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please enter Address !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBitNumber.Text))
            {
                MessageBox.Show("Please enter Bit Number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtErrorCode.Text))
            {
                MessageBox.Show("Please enter Error Code !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (string.IsNullOrWhiteSpace(txtErrorName.Text))
            //{
            //    MessageBox.Show("Please enter Error Name !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //DataTable dtCheck = Lib.GetTableData($"Select * from ErrorAddress where ErrorAddress = '{errorAddress}' and ID <> {errModel.ID}");
            //if (dtCheck.Rows.Count > 0)
            //{
            //    MessageBox.Show("This Error Address is Exist ! Please enter new Error Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            string errorAddress = txtAddressType.Text + txtAddress.Text + "." + txtBitNumber.Text;
            var exp1 = new Expression("RegisterAddress", errorAddress);
            var exp2 = new Expression("ID", errModel.ID, "<>");
            List<register_error> lstErrorCheck = _mySqlHelper.FindByExpression<register_error>(exp1.And(exp2));
            if (lstErrorCheck.Count > 0)
            {
                MessageBox.Show("This Error Address is Exist ! Please enter new Error Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                txtAddress.Text = "";
                txtAddressType.Text = "";
                txtBitNumber.Text = "";
                txtErrorCode.Text = "";
                txtErrorName.Text = "";
                txtErrorAddress.Text = "";
                txtErrorWarning.Text = "";
                errModel = new register_error();
            }
        }

        private void txtAddressType_TextChanged(object sender, EventArgs e)
        {
            txtErrorAddress.Text = txtAddressType.Text + txtAddress.Text + "." + txtBitNumber.Text;   
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            txtErrorAddress.Text = txtAddressType.Text + txtAddress.Text + "." + txtBitNumber.Text;
        }

        private void txtBitNumber_TextChanged(object sender, EventArgs e)
        {
            txtErrorAddress.Text = txtAddressType.Text + txtAddress.Text + "." + txtBitNumber.Text;
        }

        private void FrmErrorDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtErrorCode_TextChanged(object sender, EventArgs e)
        {
            txtErrorWarning.Text = txtErrorCode.Text + " ERROR " + txtErrorName.Text;
        }

        private void txtErrorName_TextChanged(object sender, EventArgs e)
        {
            txtErrorWarning.Text = txtErrorCode.Text + " ERROR " + txtErrorName.Text;
        }
    }
}
