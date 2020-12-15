using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Collections;
using System.Net;
using System.Net.Cache;
using System.ComponentModel;
using System.Collections.Specialized; 
 
//*************************************
// Created by Michael Moen
// 9 Decuember 2010
// Purpose Read and work with XML 
// Load an XL sheet from an XML File Driver 
//************************************

namespace DownLoadDB
{
    public struct sKeyValue
    {
        public string sKey;
        public string sValue;
        public Boolean bHasChildren;
        public string sAttributes;
    }
    public class sNode
    {

        public string sUnid;
        public Boolean bChildren;
        public Boolean isSHeets;
        public string val;
        public ArrayList aColl;
        public string sParent;
        public sNode(string val)
        {
            sUnid = val;
            aColl = new ArrayList();
            aColl.Clear();
        }
    }
    public class sViewEntry
    {
        public ArrayList aList;

        public sViewEntry()
        {
            aList = new ArrayList();
            aList.Clear();

        }


    }


    //partial class cXLWorker
    //{
    //    public Microsoft.Office.Interop.Excel.Workbook wb;
    //    public Microsoft.Office.Interop.Excel.ApplicationClass xl;
    //    public cXLWorker()
    //    {
    //    }
    //    public void RunXML(object[] sProc)
    //    {
    //        // start here on Mon 11 Dec 2010
    //        // add the ability to be called via the XML
    //        RunMacro(this.xl, new Object[] { sProc });
    //        //	RunMacro(oExcel, new Object[]{"DoKbTestWithParameter",

    //        //RunMacro(oWord, new Object[]{"DoKbTestWithParameter",
    //        //                         "Hello from C# Client."});



    //        //      //        xl.Run("Test", this,                        //
    //        //null, null, null, null, null, null,
    //        //null, null, null, null, null, null,
    //        //null, null, null, null, null, null,
    //        //null, null, null, null, null, null,
    //        //null, null, null, null, null);

    //    }
    //    public cXLWorker(string sFileName)
    //    {
    //        try
    //        {


    //            xl = new Microsoft.Office.Interop.Excel.ApplicationClass();
    //            xl.Visible = true;

    //            wb = xl.Workbooks.Open(
    //                // "e:\\ccidev\\testCall1.xlsm",
    //                                         sFileName, 0,
    //                          true,
    //                          5,
    //                          "",
    //                          "",
    //                          true,
    //                          Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
    //                          "\t",
    //                          false,
    //                          false,
    //                          0,
    //                          true,
    //                          1,
    //                          0);

    //            //  xl.Run("Sheet1.Test",Type.Missing) 

    //            //xl.Run("Sheet1.Test","Stest",Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing,Type.Missing,
    //            //              Type.Missing,Type.Missing,Type.Missing);

    //        }
    //        catch (Exception ex)
    //        {
    //            xl.Quit();
    //            throw ex;
    //        }
    //        finally
    //        { }
    //    }
    //    public void xlShutDown()
    //    {
    //        xl.Quit();
    //    }

    //    //private void RunMacro(  object[] oRunArgs)
    //    //{ 
    //    //    this.xl.GetType().InvokeMember("Run",
    //    //        System.Reflection.BindingFlags.Default |
    //    //        System.Reflection.BindingFlags.InvokeMethod,
    //    //        null, this.xl, oRunArgs);

    //    public void RunMacro(object oApp, object[] oRunArgs)
    //    {
    //        try
    //        {
    //            oApp.GetType().InvokeMember("Run",
    //                  System.Reflection.BindingFlags.Default |
    //                  System.Reflection.BindingFlags.InvokeMethod,
    //                  null, oApp, oRunArgs);
    //        }
    //        catch (Exception ex)
    //        { throw ex; }
    //    }


   // }
    partial class xml_instance
    {
        public string sXmlClean;
        public sViewEntry sView;
        private bool bProcFlag;
        public XmlDocument xDoc;
      //  private cXLWorker xl;
        private sNode s;
        private string sCurrentParent;




        public void LoadXDoc()
        {

            try
            {
                xDoc = new XmlDocument();
                xDoc.LoadXml(sXmlClean);
            }
            catch (XmlException ex)
            {
                //  Exception ex = new Exception("Error on XML Load");
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("General Error");
            }


        }

        public void xml_Configure()
        // This method reads the nodes from the list
        {
            try
            {
                //      xl = new cXLWorker(@"e:\ccidev\testCall2.xlsm");




                sView = new sViewEntry();
                int iCnt = 0;
                bProcFlag = false;
                XmlNodeList nodes;
                nodes = xDoc.SelectNodes("//User");
                foreach (XmlNode n in nodes)
                {
                    s = new sNode("");
                    s.val = n.InnerXml.ToString();
                    iCnt += 1;
                    // if (iCnt>=5) break ;
                    LoopThruChildren(n);
                    sView.aList.Add(s);
                }

            }


            catch (XmlException ex)
            {
                //  Exception ex = new Exception("Error on XML Load");
                throw ex;
            }
            catch (Exception ex)
            {
              
                throw new ApplicationException("General Error");

            }

        }
        private void LoopThruChildren(XmlNode rootnode)
        {

            if (rootnode.Name == "unid")
            {
                Debug.Print("Top Level User ID");
                Debug.Print("Got a Hit!");
                s.sUnid = rootnode.Value;
                s.val = rootnode.InnerXml.ToString();


            }
            //    string sgotUnid = rootnode.Attributes.GetNamedItem("unid").ToString(); 
            //  foreach (XmlAttribute a in rootnode.Attributes)
            //  {
            //   Debug.Print(a.Name, a.Value, rootnode.Name);
            //if (a.Name == "unid")
            //{
            //    Debug.Print("Got a Hit!");
            //    s.sUnid = a.Value;
            //    s.val = rootnode.InnerXml.ToString(); 
            //    // sView.aList.Add(s);
            //    break; 
            //}
            //  sView.aList.Add(s )  ;}
            // } 
            /*     if (rootnode.Name == "SubName")
                 {
                     ArrayList sSubArray = new ArrayList(); 
                     sSubArray.Clear();
                     // we have a call here
            
                     sSubArray.Add(   rootnode.Attributes["Proc"].Value.ToString())  ;
                     bProcFlag = true;
                     foreach (XmlNode x in rootnode.ChildNodes)
                     {
               
                   
                         sSubArray.Add ( x.Attributes["Value"].Value.ToString()) ;
                  
                               
                     };
                
                    //     Object[] sOut;
                    //     sOut = sSubArray.ToArray();

                   //      this.xl.RunMacro(xl.xl, sOut);
                
               
     // this.xl.RunMacro(xl.xl, new Object[] { "Sheet1.Test","Hello World" }
     //    xl.xl.Run("Sheet1.Test", sSubArray);
     //   xl.RunXML( sSubArray ); 
               
                 } */





            //  if (rootnode.Value != null ) 
            //      Debug.Print(rootnode.Value.ToString()) ; 

            foreach (XmlNode x in rootnode.ChildNodes)
            {
                LoopThruChildren(x, ref s);


            };

        }
        private void LoopThruChildren(XmlNode rootnode, ref sNode s)
        {
            Debug.Print(rootnode.Name);
            int iEvalChildren = 0;
            if (rootnode.Name == "unid")
            {
                // Got the unid
                s.sUnid = rootnode.InnerText;

            }
            if (sCurrentParent == "" && rootnode.Name == "Sheets")
            {
                sKeyValue sMystruc = new sKeyValue();
                sMystruc.sKey = rootnode.Name;
                sMystruc.bHasChildren = true;
                sMystruc.sValue = rootnode.InnerXml;
                sMystruc.sAttributes = "";
                if (rootnode.Attributes != null)
                {
                    foreach (XmlAttribute att in rootnode.Attributes)
                    {
                        sMystruc.sAttributes += att.Value + "$";
                    }
                }
                s.aColl.Add(sMystruc);
                sCurrentParent = rootnode.Name;
            }
            foreach (XmlNode x in rootnode.ChildNodes)
            {
                Debug.Print("The name is {0}, value is {1}", x.ParentNode.Name, x.InnerText);
                // mjm 21 Dec 2010
                // if there is no value just blow through to the next one.

                if (x.InnerText == null) continue;
                iEvalChildren = x.ChildNodes.Count;

                if (iEvalChildren > 0) iEvalChildren = 1;
                if (rootnode.Name == "Sheets")
                    s.isSHeets = true;

                switch (iEvalChildren)
                {
                    case 1:
                        // store the Parent in the collection as an anchor

                        Debug.Print("Has Children");



                        sNode s1 = new sNode("");
                        s1.sUnid = s.sUnid;
                        s1.val = x.InnerXml.ToString();
                        s1.isSHeets = s.isSHeets;
                        s1.sParent = sCurrentParent;
                        s.bChildren = true;
                        LoopThruChildren(x, ref s1);
                        s.aColl.Add(s1);
                        break;

                    //case "text":
                    //   //   s.aColl.Add(x.InnerXML);
                    //      break;
                    //case "CustomerName":
                    //      s.aColl.Add(x.InnerText);
                    //      break;
                    //case "entrydata":
                    //      s.aColl.Add(x.InnerText);
                    //      break;
                    //case "UserName":
                    //    s.aColl.Add(x.InnerText);
                    //    break;
                    //case "ConnectionString":
                    //     s.aColl.Add(x.InnerText);
                    //     break;

                    //case "UserType":
                    //  s.aColl.Add(x.InnerText);
                    //  break;
                    //case "BudSummary":
                    //   s.aColl.Add(x.InnerText);
                    //   break;
                    //case "BudDet":
                    //   s.aColl.Add(x.InnerText);
                    //   break;
                    //case "BudCn":
                    //     s.aColl.Add(x.InnerText);
                    //     break;
                    //case "PO":
                    //     s.aColl.Add(x.InnerText);
                    //     break;

                    //case "AbsenceSettings":
                    //s.aColl.Add(x.InnerText);
                    //break;
                    //case "Substitues":
                    //    s.aColl.Add(x.InnerText);
                    //    break;
                    //case "MasterEmployee":
                    //   s.aColl.Add(x.InnerText);
                    //   break;





                    default:
                        if (x.ChildNodes.Count == 0)
                        {
                            sCurrentParent = "";
                            sKeyValue sMystruc1 = new sKeyValue();
                            sMystruc1.sKey = x.ParentNode.Name;
                            sMystruc1.sValue = x.InnerText;
                            sMystruc1.sAttributes = "";
                            if (x.Attributes != null)
                            {
                                foreach (XmlAttribute att in x.Attributes)
                                {
                                    sMystruc1.sAttributes += att.Value + "$";
                                }
                            }
                            s.aColl.Add(sMystruc1);
                        }
                        break;

                }




            };





            foreach (XmlNode x in rootnode.ChildNodes)
            {
                LoopThruChildren(x, ref s);
            }
        }


    }

    class PGM
    {

        static void Main(string[] args)
        // entry point
        // purpose
        // Run unpacker for the xml encrypted document 
        {
            try
            {
                String sClean = "";
                int iRes = 0;
             
                NameValueCollection form = new NameValueCollection() ;
                form.Add("Username", "mmoen");
                form.Add("Password", "bobob1"); 

                RequestCachePolicy policy = new RequestCachePolicy(RequestCacheLevel.Reload);

                string address = "http://www.cookconsulting.net/changeor.nsf/XMLOutput?OpenAgent";

                System.Threading.AutoResetEvent waiter = new System.Threading.AutoResetEvent(false);
                Uri uri = new Uri(address);

                WebClient wc = new WebClient();
                wc.Proxy = null;
                wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                wc.CachePolicy = policy;
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wcDownLoadDone);
                wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownLoadDone);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcCallbackDown);
                NetworkCredential nc = new NetworkCredential("mmoen", "bobbob1");
              //  wc.Credentials = nc;


                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
System.Text.ASCIIEncoding   ASCIIEncoding = new System.Text.ASCIIEncoding  (); 
Byte[] resp =  ASCIIEncoding.GetBytes("UserName=" +  "mmoen" +
"Password=" + "bobbob1");
String ResultHTML =
ASCIIEncoding.GetString(wc.UploadData("http://www.cookconsulting.net/changeor.nsf/XMLOutput?OpenAgent", "POST", resp));
                Byte[] responseData = wc.UploadValues("http://www.cookconsulting.net/changeor.nsf/XMLOutput?OpenAgent","Post", form);     
            Console.WriteLine(Encoding.ASCII.GetString(responseData));


                sClean = wc.DownloadString(uri);
                while (wc.IsBusy)

                { waiter.WaitOne(); }


                //    DBWrite db = new DBWrite();

               
 
                xml_instance xli = new xml_instance();
                xli.sXmlClean = sClean;
                xli.LoadXDoc();
                xli.xml_Configure();
                DBWrite db = new DBWrite(xli.sView);

                db.DBProcess();


                //  Console.ReadLine();
                Console.WriteLine("Done for Now {0}", 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error = " + ex.Message);
            }
        }


        private static void wcDownLoadDone(object sender, AsyncCompletedEventArgs e)
        {
            Debug.Print("DONE!");
            Debug.Print(e.Error.ToString());


        }
        private static void wcDownLoadDone(object sender, DownloadDataCompletedEventArgs e)
        {
            Debug.Print("DONE!");
        }
        private static void wcCallbackDown(object sender, DownloadProgressChangedEventArgs e)
        {
        }

        private static int ReadFile(String sFile, ref String sOUt)
        {
            StreamReader sr;
            StreamWriter sw;
            System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();
            try
            {
                sr = new StreamReader(sFile);
                sw = new StreamWriter(@"e:\ccidev\test_out.xml", true);
                if (sr != null)
                {
                    String sLine;
                    byte[] sB1, sB2;
                    sLine = sr.ReadToEnd();
                    String sELine;
                    sB1 = str.GetBytes(sLine);
                    sELine = Convert.ToBase64String(sB1);
                    sw.Write(sELine);
                    sw.Close();
                    sw.Dispose();
                    //sB2=   Convert.FromBase64String(sELine )  ;
                    //sELine = str.GetString(sB2) ;
                    // Debug.Print(sELine) ;
                    //      Debug.Print(Convert.ToBase64String(sB1));
                    sOUt = sLine;

                    /*         Console.ReadLine();  */

                }
            }
            catch (Exception ex)
            {
            }

            return 0;



        }
    }
}
