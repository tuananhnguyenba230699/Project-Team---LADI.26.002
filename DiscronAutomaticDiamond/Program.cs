using AutoBrutingDiamond.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrutingDiamond
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(fvi.OriginalFilename));
                if (processes.Length > 1)
                {
                    MessageBox.Show("Chương trình đang chạy. Xin vui lòng kiểm tra lại!".ToUpper());
                }
                else
                {
                    Application.Run(new FrmMain());
                }
            }
            catch (TargetInvocationException e)
            {
                Lib.saveToLog(e.InnerException.Message);
            }
            catch (Exception ex)
            {
                Lib.saveToLog(ex.Message);
            }
        }
    }
}
