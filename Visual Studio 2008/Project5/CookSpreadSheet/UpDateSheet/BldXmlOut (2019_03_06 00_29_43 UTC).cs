
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Data; 
 

namespace BldXml
{
    class BldXml
    {
        private static SqlConnection cn;
        partial class cXLWorker
        {
            public Microsoft.Office.Interop.Excel.Workbook wb;
            public Microsoft.Office.Interop.Excel.ApplicationClass xl;
            public cXLWorker()
            {
            }
            public void RunXML(object[] sProc)
            {
                // start here on Mon 11 Dec 2010
                // add the ability to be called via the XML
                RunMacro(this.xl, new Object[] { sProc });
                //	RunMacro(oExcel, new Object[]{"DoKbTestWithParameter",

                //RunMacro(oWord, new Object[]{"DoKbTestWithParameter",
                //                         "Hello from C# Client."});



                //      //        xl.Run("Test", this,                        //
                //null, null, null, null, null, null,
                //null, null, null, null, null, null,
                //null, null, null, null, null, null,
                //null, null, null, null, null, null,
                //null, null, null, null, null);

            }
            public cXLWorker(string sFileName)
            {
                try
                {


                    xl = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    xl.Visible = true;

                    wb = xl.Workbooks.Open(
                        // "e:\\ccidev\\testCall1.xlsm",
                                                 sFileName, 0,
                                  true,
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
                    RunMacro(this.xl,new Object[] { sFileName, true}); 
                    //true,
                    //              "5",
                    //              "",
                    //              "",
                    //              "true",
                    //              "Microsoft.Office.Interop.Excel.XlPlatform.xlWindows",
                    //              "\t",
                    //              "false",
                    //              "false",
                    //              "0",
                    //              "true",
                    //              "1",
                    //              "0"});
                        //  xl.Run("Sheet1.Test",Type.Missing) 

                    //xl.Run("Sheet1.Test","Stest",Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                    //              Type.Missing,Type.Missing,Type.Missing);

                }
                catch (Exception ex)
                {
                    xl.Quit();
                    throw ex;
                }
                finally
                { }
            }
            public void xlShutDown()
            {
                xl.Quit();
            }

            //private void RunMacro(  object[] oRunArgs)
            //{ 
            //    this.xl.GetType().InvokeMember("Run",
            //        System.Reflection.BindingFlags.Default |
            //        System.Reflection.BindingFlags.InvokeMethod,
            //        null, this.xl, oRunArgs);

            public void RunMacro(object oApp, object[] oRunArgs)
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
        private static void SqlCust()
        {
           SqlCommand cmd;
  
            string sConnect = "DATA SOURCE=MMDELL\\SQL08R2;INITIAL CATALOG=CCI;INTEGRATED SECURITY=SSPI;";
            cn = new SqlConnection(sConnect);

            cmd = new SqlCommand("usp_Rtv_Unids", cn);
            string sUnid; 
        
            if (cn.State != ConnectionState.Open)
            { cn.Open(); }
            try
            {
                SqlDataReader dr;
                cmd.CommandType = CommandType.StoredProcedure;
                

                dr = cmd.ExecuteReader();
                ArrayList cCustomer = new ArrayList() ;

                while (dr.Read())
                {
                    sUnid = dr.GetString(0);
                    cCustomer.Add(sUnid);
                }

                dr.Close();

                foreach (string s in cCustomer)
                {
                    SqlWrite(s);
                }

                cn.Close();
                cn.Dispose();
            
            }
            catch (Exception ex)
            { }



        }

        private static void SqlWrite(string sUnid)
        {
               System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();

            try
            {
                SqlCommand cmd2;
                byte[] sB1;
             
                if (cn.State != ConnectionState.Open)
                { cn.Open(); }

                cmd2 = new SqlCommand("usp_Rtv_Parms", cn);
                cmd2.CommandType = CommandType.StoredProcedure;
        
                string sXmlDoc;

                SqlDataReader dr;
                
                SqlCommandBuilder.DeriveParameters(cmd2);
                cmd2.Parameters["@unid"].Value = sUnid;

                dr = cmd2.ExecuteReader();


              
                
                dr.Read();
                sXmlDoc = (string) dr.GetString(0);
                string sXmlDoce;
                sB1 = str.GetBytes(sXmlDoc);
              
                //    sB1 = str.GetBytes(sXmlDoc);
                 sXmlDoce = Convert.ToBase64String(sB1);
            
               //   sB1 = str.GetBytes(sXmlDoc);

                string sPath = @"e:\ccidev\xmlout\" + sUnid;
                
                // if missing build it!
                if (!Directory.Exists(sPath)) Directory.CreateDirectory(sPath);
                sPath += @"\Cust.Xml"; 
                StreamWriter sw = new StreamWriter(sPath,false);
           


          
                sw.WriteLine(sXmlDoce  );
                // + sXmlDoce);

                sw.Flush(); 
                sw.Close();
                sw.Dispose(); 
               
           //     XmlDocument xmldoc = new XmlDocument();
              
                //  xmldoc.LoadXml(sXmlDoc);
                
                // i am done with this
                dr.Close();
            
            
            }
            catch (Exception ex)
            { throw ex; }


        }



        static void Main(string[] args)
        {
            // MJM  3 Jan 2011
            // Purpose
            // Open XML Sheet and set the properties on the sheet 
            try
            {
              string sFile = "" ;   // args[0];
              string sXlFile = ""; //args[1];
                string sUnid = "41283C73EA57DA718525770A005A6839";

                int iRes = 0;

                SqlCust() ;

                if (sFile != null)
                {
                    if (File.Exists(sFile))
                    {
                        iRes = ReadFile(sFile);
                        if (iRes == 0)
                        {
                            cXLWorker xl = new cXLWorker(sXlFile);

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read(); 
            }

        }



        private static int ReadFile(String sFile )
        {
            StreamReader sr;
       
            System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();
            try
            {
                sr = new StreamReader(sFile);

           //     sw = new StreamWriter(@"e:\ccidev\test_out.xml", true);


                if (sr != null)
                {
                    String sLine;
              //    byte[] sB1, sB2;
                    sLine = sr.ReadToEnd();

              //    String sELine;
              //    sB1 = str.GetBytes(sLine);
              //    sELine = Convert.ToBase64String(sB1);
              //    sw.Write(sELine);
              //    sw.Close();
              //    sw.Dispose();
              //    sB2=   Convert.FromBase64String(sELine )  ;
              //    sELine = str.GetString(sB2) ;
              //    Debug.Print(sELine) ;
              //    Debug.Print(Convert.ToBase64String(sB1));
              //    sOUt = sLine;

                    sr.Close();
                    sr.Dispose();

                    return 0;
                    /*         Console.ReadLine();  */
                }
                return 0;
            }

            catch (Exception ex)
            {
                return -1; 
            }








        }
    }
}