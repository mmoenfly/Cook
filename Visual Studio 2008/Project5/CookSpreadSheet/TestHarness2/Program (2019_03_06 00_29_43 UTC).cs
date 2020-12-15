using System;
using System.Collections.Generic;
 
using System.Text;
using Microsoft.Office.Interop.Excel; 

namespace TestHarness2
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Office.Interop.Excel.Workbook wb;
            Microsoft.Office.Interop.Excel.ApplicationClass xl;
            xl = new Microsoft.Office.Interop.Excel.ApplicationClass();
     
            xl.Visible = true;
            //Microsoft.Office.Core.MsoAutomationSecurity secAuto = (MsoAutomationSecurity)MsoAutomationSecurity.msoAutomationSecurityLow;

            //xl.Application.AutomationSecurity = secAuto;



            wb = xl.Workbooks.Open(

                         @"e:\\ccidev\testcall2.xlsm"
                          , 0
                          , true,
                          5,
                          "",
                          "",
                          true,
                          Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                          "\t",
                          false,
                          false,
                          0,
                          true,
                          1,
                          0);

            xl.Visible = true;
            xl.Interactive = true; 
            object[] mytest = new object[] { 5,0 };
            object mytest2 = new object();
            mytest2 = mytest; 
            object[] oParms = new object[] { "Sheet1.Test"
                ,""
                ,""
                , mytest
                , ""
                , ""
                ,null
                ,""
                 ,0
                ,"1"
            
                ,  new object[] {"test",0} 
                ,""
                 };
            try
            {
                RunMacro(xl, oParms);
            }
            catch(Exception ex){ Console.WriteLine(ex.Message) ;};
            xl.Quit();
        }
        private static void RunMacro(object oApp, object[] oRunArgs)
        {
            try
            {
                oApp.GetType().InvokeMember("Run",
                      System.Reflection.BindingFlags.Default |
                      System.Reflection.BindingFlags.InvokeMethod,
                      null, oApp, oRunArgs);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
