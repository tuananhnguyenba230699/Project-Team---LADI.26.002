using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrutingDiamond.Class
{
    public static class Lib
    {
        public static DateTime MIN_DATE = new DateTime(1950, 1, 1);

        public static void saveToLog(string log)
        {
            try
            {
                string path = Application.StartupPath + @"\Log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - LOG" + "\n" + "THÔNG SỐ: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }
            }
            catch (Exception)
            {
            }
        }
        public static void saveToLogConcentric(TextBox txtLog, string log)
        {
            try
            {
                string path = Application.StartupPath + @"\LogConcentric";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - LOG" + "\n" + "MESSAGE: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }

                if (txtLog.InvokeRequired)
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 1] " +  log + Environment.NewLine);
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();
                    }
                    );
                else
                {
                    txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 1] " + log + Environment.NewLine);
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
            }
            catch (Exception)
            {
            }
        }

        public static void saveToLogGrinding(TextBox txtLog, string log)
        {
            try
            {
                string path = Application.StartupPath + @"\LogGrinding";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - LOG" + "\n" + "MESSAGE: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }

                if (txtLog.InvokeRequired)
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 2] " + log + Environment.NewLine);
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();
                    }
                    );
                else
                {
                    txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 2] " + log + Environment.NewLine);
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
            }
            catch (Exception)
            {
            }
        }
        public static void saveToLogInspect(TextBox txtLog, string log)
        {
            try
            {
                string path = Application.StartupPath + @"\LogInspect";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - LOG" + "\n" + "MESSAGE: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }

                if (txtLog.InvokeRequired)
                    txtLog.Invoke((MethodInvoker)delegate
                    {
                        txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 3] " + log + Environment.NewLine);
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();
                    }
                    );
                else
                {
                    txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + "[STEP 3] " + log + Environment.NewLine);
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
            }
            catch (Exception)
            {
            }
        }
        public static void saveToErr(string log)
        {
            try
            {
                string path = Application.StartupPath + @"\Err";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - Error" + "\n" + "THÔNG SỐ: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }
            }
            catch (Exception)
            {
            }
        }
        public static void saveToLog_HeightDiamond(string log)
        {
            try
            {
                string path = Application.StartupPath + @"\LogHeightDiamond";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string fullMessage = string.Empty;
                fullMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - LOG" + "\n" + "MESSAGE: " + "\n" + log;
                if (!File.Exists(fileName))
                    File.WriteAllText(fileName, fullMessage);
                else
                {
                    string existingContent = File.ReadAllText(fileName);
                    File.WriteAllText(fileName, fullMessage + "\r" + existingContent);
                }
            }
            catch (Exception)
            {
            }
        }
        public static void UpdateMultipleButtonBackColor(Color color, params Button[] buttonBoxes)
        {
            foreach (var btn in buttonBoxes)
            {
                if (btn == null || btn.IsDisposed) continue;
                try
                {
                    if (btn.InvokeRequired)
                    {
                        btn.Invoke((MethodInvoker)delegate
                        {
                            btn.BackColor = color;
                        });
                    }
                    else
                        btn.BackColor = color;
                }
                catch { }
            }
        }

        public static void UpdateMultipleButtonForeColor(Color color, params Button[] buttonBoxes)
        {
            foreach (var btn in buttonBoxes)
            {
                if (btn == null || btn.IsDisposed) continue;
                try
                {
                    if (btn.InvokeRequired)
                    {
                        btn.Invoke((MethodInvoker)delegate
                        {
                            btn.ForeColor = color;
                        });
                    }
                    else
                        btn.ForeColor = color;
                }
                catch { }
            }
        }

        public static void UpdateLabelText_BColor_FColor(Label lbl, string text, Color bColor, Color fColor)
        {
            if (lbl.IsDisposed) return;
            try
            {
                if (lbl.InvokeRequired)
                    lbl.Invoke((MethodInvoker)delegate
                    {
                        lbl.Text = text;
                        lbl.BackColor = bColor;
                        lbl.ForeColor = fColor;
                    }
                    );
                else
                {
                    lbl.Text = text;
                    lbl.BackColor = bColor;
                    lbl.ForeColor = fColor;
                }
            }
            catch
            { }
        }

        public static void UpdateButtonText_BColor_FColor(Button btn, string text, Color bColor, Color fColor)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Text = text;
                        btn.BackColor = bColor;
                        btn.ForeColor = fColor;
                    }
                    );
                else
                {
                    btn.Text = text;
                    btn.BackColor = bColor;
                    btn.ForeColor = fColor;
                }
            }
            catch
            { }
        }

        public static void UpdateButton_BColor_FColor(Button btn, Color bColor, Color fColor)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.BackColor = bColor;
                        btn.ForeColor = fColor;
                    }
                    );
                else
                {
                    btn.BackColor = bColor;
                    btn.ForeColor = fColor;
                }
            }
            catch
            { }
        }

        public static void UpdateButton_Result_BColor_FColor(Button btn, bool result, Color Color_T, Color Color_F)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        if (result)
                            btn.BackColor = Color_T;
                        else
                            btn.BackColor = Color_F;
                        btn.ForeColor = Color.White;
                    }
                    );
                else
                {
                    if (result)
                        btn.BackColor = Color_T;
                    else
                        btn.BackColor = Color_F;
                    btn.ForeColor = Color.White;
                }
            }
            catch
            { }
        }

        public static void UpdateComboboxSelectedIndex(ComboBox cbo, int index)
        {
            if (cbo.IsDisposed) return;
            try
            {
                if (cbo.InvokeRequired)
                    cbo.Invoke((MethodInvoker)delegate
                    {
                        cbo.SelectedIndex = index;
                    }
                    );
                else
                    cbo.SelectedIndex = index;
            }
            catch
            { }
        }
        public static void UpdateBringToFrontPanel(Panel panel)
        {
            if (panel.IsDisposed) return;
            try
            {
                if (panel.InvokeRequired)
                    panel.Invoke((MethodInvoker)delegate
                    {
                        panel.BringToFront();
                    }
                    );
                else
                    panel.BringToFront();
            }
            catch
            { }
        }
        public static void UpdateVisibleControl(Control control, bool state)
        {
            if (control.IsDisposed) return;
            try
            {
                if (control.InvokeRequired)
                    control.Invoke((MethodInvoker)delegate
                    {
                        control.Visible = state;
                    }
                    );
                else
                    control.Visible = state;
            }
            catch
            { }
        }
        public static void UpdateButtonBackColor(Button btn, Color color)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.BackColor = color;
                    }
                    );
                else
                    btn.BackColor = color;
            }
            catch
            { }
        }
        public static void UpdateButtonForeColor(Button btn, Color color)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.ForeColor = color;
                    }
                    );
                else
                    btn.ForeColor = color;
            }
            catch
            { }
        }

        public static void UpdateButtonFontStyle(Button btn, FontStyle font)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Font = new Font(btn.Font, font);
                    }
                    );
                else
                    btn.Font = new Font(btn.Font, font);
            }
            catch
            { }
        }
        public static void UpdateButtonText(Button btn, string text)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Text = text;
                    }
                    );
                else
                    btn.Text = text;
            }
            catch
            { }
        }
        public static void UpdateSimpleButtonText(DevExpress.XtraEditors.SimpleButton btn, string text)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Text = text;
                    }
                    );
                else
                    btn.Text = text;
            }
            catch
            { }
        }
        public static void UpdateMultipleButtonText(string text, params Button[] buttonBoxes)
        {
            foreach (var btn in buttonBoxes)
            {
                if (btn == null || btn.IsDisposed) continue;
                try
                {
                    if (btn.InvokeRequired)
                    {
                        btn.Invoke((MethodInvoker)delegate
                        {
                            btn.Text = text;
                        });
                    }
                    else
                        btn.Text = text;
                }
                catch { }
            }
        }

        public static void AddTextBoxText(TextBox txt, string text)
        {
            if (txt.IsDisposed) return;
            try
            {
                if (txt.InvokeRequired)
                    txt.Invoke((MethodInvoker)delegate
                    {
                        txt.Text = text + txt.Text;
                    }
                    );
                else
                    txt.Text = text + txt.Text;

            }
            catch
            { }
        }
        public static void UpdateTextBoxText(TextBox txt, string text)
        {
            if (txt.IsDisposed) return;
            try
            {
                if (txt.InvokeRequired)
                    txt.Invoke((MethodInvoker)delegate
                    {
                        txt.Text = text;
                    }
                    );
                else
                    txt.Text = text;
            }
            catch
            { }
        }

        public static void UpdateLabelText(Label lbl, string text)
        {
            if (lbl.IsDisposed) return;
            try
            {
                if (lbl.InvokeRequired)
                    lbl.Invoke((MethodInvoker)delegate
                    {
                        lbl.Text = text;
                    }
                    );
                else
                    lbl.Text = text;
            }
            catch
            { }
        }
        public static void UpdateButtonEnable(Button btn, bool state)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Enabled = state;
                    }
                    );
                else
                    btn.Enabled = state;
            }
            catch
            { }
        }
        public static void UpdateSimpleButtonEnable(DevExpress.XtraEditors.SimpleButton btn, bool state)
        {
            if (btn.IsDisposed) return;
            try
            {
                if (btn.InvokeRequired)
                    btn.Invoke((MethodInvoker)delegate
                    {
                        btn.Enabled = state;
                    }
                    );
                else
                    btn.Enabled = state;
            }
            catch
            { }
        }
        public static void UpdateNumericUpDownValue(NumericUpDown num, decimal value)
        {
            if (num.IsDisposed) return;
            try
            {
                if (num.InvokeRequired)
                    num.Invoke((MethodInvoker)delegate
                    {
                        num.Value = value;
                    }
                    );
                else
                    num.Value = value;
            }
            catch
            { }
        }
        public static void UpdateTextboxReadOnly(TextBox txt, bool state)
        {
            if (txt.IsDisposed) return;
            try
            {
                if (txt.InvokeRequired)
                    txt.Invoke((MethodInvoker)delegate
                    {
                        txt.ReadOnly = state;
                    }
                    );
                else
                    txt.ReadOnly = state;
            }
            catch
            { }
        }
        public static void UpdateNumericReadOnly(NumericUpDown num, bool state)
        {
            if (num.IsDisposed) return;
            try
            {
                if (num.InvokeRequired)
                    num.Invoke((MethodInvoker)delegate
                    {
                        num.ReadOnly = state;
                    }
                    );
                else
                    num.ReadOnly = state;
            }
            catch
            { }
        }
        public static int ExecuteQuery(string sqlQuery)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=VisionDB.db;Version=3;");
                SQLiteCommand cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ExecuteDeleteQuery(string table, string field, object value)
        {
            try
            {
                string sqlQuery = string.Format(@"delete from {0} where {1} = '{2}'", table, field, value);
                SQLiteConnection con = new SQLiteConnection("Data Source=VisionDB.db;Version=3;");
                SQLiteCommand cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static object ExecuteScalar(string sqlQuery)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=VisionDB.db;Version=3;");
                SQLiteCommand cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;//"delete from Student where ID = 0";
                object value = cmd.ExecuteScalar();
                con.Close();
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetTableData(string sqlQuery)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=VisionDB.db;Version=3;");
                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlQuery, con);

                con.Open();
                da.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return dt;
        }
        #region Chuyển kiểu, ép kiểu
        public static string ToString(object x)
        {
            if (x != null)
            {
                return x.ToString().Trim();
            }
            return "";
        }

        /// <summary>
        /// Chuyển giá trị sang kiểu integer
        /// </summary>
        /// <param name="x">giá trị cần chuyển</param>
        /// <returns></returns>
        public static int ToInt(object x)
        {
            try
            {
                return Convert.ToInt32(x);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Guid ToGuid(object x)
        {
            Guid result = Guid.Empty;
            try
            {
                Guid.TryParse(Lib.ToString(x), out result);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        /// <summary>
        /// Chuyển giá trị sang kiểu long
        /// </summary>
        /// <param name="x">giá trị cần chuyển</param>
        /// <returns></returns>
        public static Int64 ToInt64(object x)
        {
            try
            {
                return Convert.ToInt64(x);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển giá trị sang kiểu decimal
        /// </summary>
        /// <param name="x">giá trị cần chuyển</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object x)
        {
            try
            {
                return Convert.ToDecimal(x);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển giá trị sang kiểu double
        /// </summary>
        /// <param name="x">giá trị cần chuyển</param>
        /// <returns></returns>
        public static Double ToDouble(object x)
        {
            try
            {
                return Convert.ToDouble(x);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static long Tolong(object x)
        {
            try
            {
                return Convert.ToInt64(x);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Chuyển giá trị sang kiểu bool
        /// </summary>
        /// <param name="x">giá trị cần chuyển</param>
        /// <returns></returns>
        public static bool ToBoolean(object x)
        {
            try
            {
                if (Lib.ToInt(x) == 1)
                    return true;
                return Convert.ToBoolean(x);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Chuyển giá trị chuỗi sang kiểu datetime
        /// </summary>
        /// <param name="date">chuỗi cần chuyển</param>
        /// <returns></returns>
        public static DateTime ToDate(string date)
        {
            try
            {
                try
                {
                    return DateTime.Parse(date, new CultureInfo("en-US", true));
                }
                catch
                {
                    return DateTime.Parse(date, new CultureInfo("fr-FR", true));
                }
            }
            catch
            {
                return Lib.MIN_DATE;
            }
        }

        public static DateTime ToDate1(string date)
        {
            try
            {
                try
                {
                    return DateTime.Parse(date, new CultureInfo("vi-VN", true));
                }
                catch
                {
                    return DateTime.Parse(date, new CultureInfo("fr-FR", true));
                }
            }
            catch
            {
                return Lib.MIN_DATE;
            }
        }

        public static DateTime? ToDate2(object x)
        {
            string date = "";
            if (x != null)
            {
                date = x.ToString();
            }
            try
            {
                try
                {
                    return DateTime.Parse(date, new CultureInfo("en-US", true));
                }
                catch
                {
                    return DateTime.Parse(date, new CultureInfo("fr-FR", true));
                }
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDate3(object x)
        {
            string date = "";
            if (x != null)
            {
                date = x.ToString();
            }
            try
            {
                try
                {
                    return DateTime.Parse(date, new CultureInfo("en-US", true));
                }
                catch
                {
                    return DateTime.Parse(date, new CultureInfo("fr-FR", true));
                }
            }
            catch
            {
                return Lib.MIN_DATE;
            }
        }

        /// <summary>
        /// Chuyển giá trị datetime sang kiểu chuỗi ngày tháng định dạng Việt Nam
        /// </summary>
        /// <param name="date">Ngày cần chuyển</param>
        /// <returns></returns>
        public static string DateToStringVN(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Chuyển dạng số thành dạng %
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string FormatPercentNumber(Decimal x)
        {
            return String.Format("{0:#0.00}%", x);
        }

        #endregion Các hàm chuyển kiểu


    }
}
