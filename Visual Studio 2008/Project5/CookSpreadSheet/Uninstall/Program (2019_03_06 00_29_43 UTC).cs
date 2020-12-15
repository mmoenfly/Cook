using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; 
namespace Uninstall
{
    class Program
    {
        static void Main(string[] args)
        {
        //    System.Threading.Thread.Sleep(60000);

            Process uninstallProcess = new Process();

            uninstallProcess.StartInfo.FileName = "MsiExec.exe";

            uninstallProcess.StartInfo.Arguments = " /q /x {7D49038C-745C-4E3E-BD8D-40891A2534BD} ";

            uninstallProcess.StartInfo.UseShellExecute = false;

            uninstallProcess.Start(); 
        }
    }
}
