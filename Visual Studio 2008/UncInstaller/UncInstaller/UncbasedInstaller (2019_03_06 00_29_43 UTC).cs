
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Collections;
using System.Web;

namespace Installer
{
    class CopyInstall
    {

        DocDetails dt;
        private ArrayList nProd, nMethColl;
        private NameValueCollection nMethods;
        private static string sHost = @"", sTarget = @"C:\AS400DA\", sUnid, sCurrentFile, sEmail, sOfficeSw;
        private static int iBatchCl, iLogCount = 0, iDotCount = 0 ;
        private static bool bWebLog = true;

        struct sTheMethods
        {
            public string sSheet;
            public string sPassed;
            public string sKeyName;
            public string sParent;

            public sTheMethods(string nSheet, string nPassed, string nKeyed, string nParent)
            {
                sSheet = nSheet;
                sPassed = nPassed;
                sKeyName = nKeyed;
                sParent = nParent;
            }
        }
        struct sTheProducts
        {
            public string sProduct;
            public string sReadonly;
            public string sFileName;
            public sTheProducts(string sProd, string sFile, string sRead)
            {
                sProduct = sProd;
                sFileName = sFile;
                sReadonly = sRead;
            }
        }

        static void Main(string[] args)
        {
            // Initialize the program 
            // Set starting conditions
            bool bError = false; 





            //string url = sHost + "Sheets/Version/070110/";
            //   HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //   using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //   {
            //       using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //       {
            //           string html = reader.ReadToEnd();

            //       }

            //   }


            /* Arguments
             * 0 Install Location  \\server\share\    -  Install
             * 1 target directory  \\server\share\  - tied to unid
             * 2 unid 
             * 
             */
            PcId pc;
            try
            {
              
                if (File.Exists("Clients.xml"))
                    // We will drive via file. Do the config 
                {
                    XmlDocument xmld = new XmlDocument();
                    xmld.Load(@"Clients.xml");
                    XmlNodeList nodesl = xmld.SelectNodes("//Client");

                    string sFileName = "";
                    sFileName = sHost + @"\Install\Xml\Products.xml";


                    Console.WriteLine("Getting file {0}", sFileName);
                    DownLoadASheet(sFileName, sTarget + @"\Products.Xml");

                    //if (bWebLog)
                    //    pc.LogEvent(sUnid, "File Download Complete on "
                    //       + sTarget + @"Products.Xml", 0, pc.IBatchNo);
                    //else
                    //    pc.FileLog(sUnid, "File Download Complete on "
                    //        + sTarget + @"Products.Xml", 0, pc.IBatchNo);


                    foreach (XmlNode n in nodesl)
                    {
                         

                        sHost = n.Attributes["Host"].Value;
                        sUnid = n.Attributes["Unid"].Value;
                        sTarget = n.Attributes["Target"].Value;
                        sEmail = n.Attributes["Email"].Value;
                        sOfficeSw = n.Attributes["Office"].Value;

                        // call the main config class
                        Config_Process.ProcessMain(sHost, sUnid, sTarget, sEmail, sOfficeSw);


                        // reset the logger object
                        

                    }
                    goto Done;
                }
                else
                {

                sHost = args[0];
                if (sHost.Contains("$"))
                    sHost = sHost.Replace("$", "");

                sTarget = args[1];
                if (sTarget.Contains("$"))
                    sTarget=sTarget.Replace("$",""); 

          
                sUnid = args[2];
                sEmail = args[3];
                sOfficeSw = args[4];

            }
            }
            catch
            {
                sHost = @"Server Error";
                sTarget = @"Drive Error";
                sUnid = "";
                sEmail = "email@error.net";
                sOfficeSw = "2007";
            }
            iBatchCl = -999;
            // Lets Id the box
              pc = new PcId(sUnid, sTarget, sEmail);
            // 21 Jan 2011 - Added support for godaddy webservice and Id on box 
            try
            {
                pc.LogEvent();
                iBatchCl = pc.IBatchNo;
                bWebLog = true;
                if (iBatchCl == -1)
                {
                    // logging is not available
                    bWebLog = false;
                    pc.CreateLogFile(sTarget);
                }

            }
            catch (Exception ex)
            {
                bWebLog = false;
                pc.CreateLogFile(sTarget);
            }

            try
            {
                Directory.CreateDirectory(sTarget);
            }
            catch { }

            ArrayList nProd = new ArrayList();

            //if (args.GetUpperBound(0) > 0)
            //    sUnid = args[0];

            // make sure our work directory exists
            // Clear out old sheets 
            string[] sFiles = Directory.GetFiles(sTarget);
            foreach (string f in sFiles)
            {
                FileInfo fd = new FileInfo(f);

                if (fd.Extension == ".xls" || fd.Extension == ".xlsm")
                {
                    fd.IsReadOnly = false;
                    fd.Delete();
                }
            }
          

            if (!Directory.Exists(sTarget))
            {


                
            }
            else
            {
               sFiles = Directory.GetFiles(sTarget);
                foreach (string f in sFiles)
                {
                    FileInfo fd = new FileInfo(f);
                    if (fd.Extension == ".xls" || fd.Extension == ".xlsm")
                    {
                        fd.IsReadOnly = false;
                        fd.Delete();
                    }
                }
            }
            try
            {
                string sFileName = "";
                sFileName = sHost + @"\Install\Xml\Products.xml";


                Console.WriteLine("Getting file {0}", sFileName); 
                DownLoadASheet(sFileName, sTarget + @"\Products.Xml");

                if (bWebLog)
                    pc.LogEvent(sUnid, "File Download Complete on "
                       + sTarget + @"Products.Xml", 0, pc.IBatchNo);
                else
                    pc.FileLog(sUnid, "File Download Complete on "
                        + sTarget + @"Products.Xml", 0, pc.IBatchNo);

                sFileName = sHost + @"\Install\Xml\" + sUnid + @"\Cust.Xml";
                Console.WriteLine("Getting file {0}", sFileName); 
                DownLoadASheet(sFileName, sTarget + @"\Cust.Xml");

                if (bWebLog)
                    pc.LogEvent(sUnid, "File Download Complete on "
                   + sTarget + @"Cust.Xml", 0, pc.IBatchNo);
                else
                    pc.FileLog(sUnid, "File Download Complete on "
                           + sTarget + @"Cust.Xml", 0, pc.IBatchNo);

            }
            catch (Exception ex)
            {
                if (bWebLog)
                    pc.LogEvent(sUnid, ex.Message + " - Program Abort", -1, pc.IBatchNo);
                else
                    pc.FileLog(sUnid, ex.Message + " - Program Abort", -1, pc.IBatchNo);
                try
                {
                    pc.Close();
                }
                catch { }

                Console.WriteLine("Program Stopping - " + ex.Message);
                return;
            }
            if (bWebLog)
                pc.LogEvent(sUnid, "File Download Complete", 0, pc.IBatchNo);
            else
                pc.FileLog(sUnid, "File Download Complete", 0, pc.IBatchNo);



            // Process the xmlDoc

            XmlDocument xmldoc = new XmlDocument();
            XmlDocument xmldocProducts = new XmlDocument();

            xmldocProducts.Load(sTarget + @"\Products.Xml");


            System.Text.UTF8Encoding str = new System.Text.UTF8Encoding();

            string sContents;
            StreamReader sr = new StreamReader(sTarget + @"\Cust.Xml");
            sContents = sr.ReadToEnd();

            sr.Close();
            sr.Dispose();

            byte[] b = Convert.FromBase64String(sContents);


            sContents = System.Text.Encoding.UTF8.GetString(b);

            //Byte[] sb1 = str.GetBytes(sContents);
            //sContents = Convert.ToBase64String(sb1);
            //sContents = Convert.ToString(sb1);
            StreamWriter sw = new StreamWriter(sTarget + @"\Cust.Xml", false);

            sw.Write(sContents);
            sw.Close();
            sw.Dispose();




            xmldoc.Load(sTarget + @"\Cust.Xml");

            NameValueCollection nMethods = new NameValueCollection();
            //       LoadMethods(ref nMethods, xmldocMethods); 

            //  Call the Config Clas
            DocDetails dt = new DocDetails(xmldocProducts);

            dt.SetProducts(nProd);

            NameValueCollection nParms = new NameValueCollection();

            XmlNode root;

            root = xmldoc.DocumentElement;

            XmlNodeList nodes;


            //  nodes = root.SelectNodes("Customer/Parms[@Sheet='Y']");
            nodes = root.SelectNodes("Customer/Parms");
            // Build all the nodes, mark which are sheets, which are parms.
            // Match it by queryand and sheet to determine the parms.

            string sQry = "";
            foreach (XmlNode n in nodes)
            {

                if (n.Name == "Parms")
                {
                    try
                    {

                        if (sQry == "")
                            sQry = n.Attributes["queryend"].Value;




                    }
                    catch (Exception ex)
                    { }
                }




                foreach (XmlAttribute att in n.Attributes)
                {
                    Debug.Print("att value={0} Attribute Name{1}", att.Value, att.Name);

                    if (att.Name == "Product" && att.Value == "Y")
                    {
                        Debug.Print("Got a Product{0}", n.Attributes["Value"].Value);

                        //     string sRemoteName = "http://localhost/sheets/" + n.Attributes["Value"].Value + ".xls";
                        //     string sLocalName = @"C:\cci\" + n.Attributes["Value"].Value + ".xls";
                        try
                        {
                            char[] delimiterChars = { ',' };
                            string[] sqrysplit;
                            try
                            {
                                sQry = nParms.Get("queryend").ToString();
                                sqrysplit = sQry.Split(delimiterChars);
                                sQry = sqrysplit[0];
                            }
                            catch (Exception ex)
                            {
                                sQry = @"070110";
                            }

                            NameValueCollection tParms = FindParmSet(n.Attributes["Value"].Value, sQry, dt.cSheetColl, nParms);
                            string sFile = FindLiveFile(n.Attributes["Value"].Value, sQry, dt.cSheetColl);
                            string sRemoteName = sHost + @"\Install\sheets\version\" + sQry + @"\" + sFile;
                                 //HttpUtility.UrlPathEncode(sFile);
                            string sLocalName = sTarget  +@"\" + sFile;

                            if (bWebLog)
                                pc.LogEvent(sUnid, "Downloading Sheet" + sRemoteName, 0, pc.IBatchNo);
                            else
                                pc.FileLog(sUnid, "Downloading Sheet" + sRemoteName, 0, pc.IBatchNo);
                            
                            iDotCount = 0;

                            sCurrentFile = sRemoteName;


                            if (File.Exists(sLocalName))
                            {
                                FileInfo fi2 = new FileInfo(sLocalName);
                                fi2.IsReadOnly = false;
                                fi2.Delete();
                            }


// Added 26 August 2011 
// Purpose
// Retry 3 times
                            int iTries = 0;
                            
                            ftpRetry:

                            Console.WriteLine("Getting file {0}", sRemoteName);

                            try
                            {
                                iTries += 1;
                                DownLoadASheet(sRemoteName, sLocalName);
                            }
                            catch (Exception x)
                            {
                                if (iTries > 3)
                                    throw x;
                                else
                                {

                                    Console.WriteLine("Retrying file {0}", sLocalName);

                                    goto ftpRetry;
                                }
                            }

                            if (bWebLog)
                                pc.LogEvent(sUnid, "Download Complete Sheet" + sRemoteName, 0, pc.IBatchNo);
                            else
                                pc.FileLog(sUnid, "Download Complete Sheet" + sRemoteName, 0, pc.IBatchNo);

                            //    Call Configure on it!
                            //       ConfigureASheet(sLocalName, ref  dt.cSheetColl, n.Value, sQry, nParms, dt);
                            FileInfo fi;

                            bool bProcess = false;

                            fi = new FileInfo(sLocalName);


                            if (File.Exists(sLocalName) && fi.Length > 0)
                            {
                                try
                                {
                                    // inc the logger 
                                    iLogCount += 1;

                                    if (!pc.bCust)
                                    {
                                        pc.LogEvent(sUnid, "", 10000, pc.IBatchNo);
                                        pc.bCust = true;
                                    }

                                    if (bWebLog)
                                        pc.LogEvent(sUnid, "Trying to configure" + sLocalName, 0, pc.IBatchNo);
                                    else
                                        pc.FileLog(sUnid, "Trying to configure" + sLocalName, 0, pc.IBatchNo);

                                    ConfigureASheet(sLocalName, ref  dt.cSheetColl, n.Attributes["Value"].Value, sQry, tParms, dt, ref pc);
                                    if (bWebLog)
                                        pc.LogEvent(sUnid, "Configuring " + sLocalName, 0, pc.IBatchNo);
                                    else
                                        pc.FileLog(sUnid, "Configuring " + sLocalName, 0, pc.IBatchNo);


      

                                    sQry = "";
                                }

                                catch (Exception ex)
                                {
                                    pc.LogEvent(sUnid, ex.Message + " - Configuring Sheet Fails", -1, pc.IBatchNo);
                                }
                                if (bWebLog)
                                    pc.LogEvent(sUnid, "Processing Completed", 0, pc.IBatchNo);
                                else
                                    pc.FileLog(sUnid, "Processing Completed", 0, pc.IBatchNo);
                


                                if (bWebLog)
                                    pc.LogEvent(sUnid, " Installer Closing", 11000, pc.IBatchNo);
                                else
                                    pc.FileLog(sUnid, " Installer Closing", 11000, pc.IBatchNo);
                                pc.Close();

                            }
                            else
                            { fi.Delete(); }
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (bWebLog)
                                pc.LogEvent(sUnid, ex.Message + " - Downloading Sheet", -1, pc.IBatchNo);
                            else
                                pc.FileLog(sUnid, ex.Message + " - Downloading Sheet", -1, pc.IBatchNo);

                            Console.WriteLine("Error - " + ex.Message);

                        }

                    }
                    else
                    { // it is not a sheet
                        try
                        {
                            if (nParms.Get(n.Attributes["LkeyName"].Value) != null)
                            {//skip it 
                                string sKeyName = n.Attributes["LkeyName"].Value;
                                string sKeyValue = n.Attributes["Value"].Value;
                                string sCurrentVal = nParms.Get(n.Attributes["LkeyName"].Value);
                                Debug.Print("Atrib {0} ", n.Attributes["Value"].Value);
                                //       if (sKeyValue == sCurrentVal) goto Lp1; 
                                switch (sKeyName)
                                {

                                    //salariedcodes
                                    //salariedwithsupp
                                    //certifiedcodes
                                    //princodes
                                    //suppcodes


                                    case "suppcodes":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    case "princodes":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    case "certifiedcodes":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;

                                    case "salariedwithsupp":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    case "salariedcodes":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    case "otherdepts":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                            , sCurrentVal
                                            + "," + sKeyValue);
                                        break;
                                    case "budgetind":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    case "specialind":
                                        nParms.Remove(n.Attributes["LkeyName"].Value);
                                        nParms.Add(n.Attributes["LkeyName"].Value
                                           , sCurrentVal
                                           + "," + sKeyValue);
                                        break;
                                    default:
                                        break;
                                }
                                goto Lp1;
                            }

                            else
                            { nParms.Add(n.Attributes["LkeyName"].Value, n.Attributes["Value"].Value); }
                        }
                        catch (ArgumentOutOfRangeException ex)
                        { // is missing
                        }
                        catch
                        { }


                    }

                Lp1: ;
                };
            }

            // Calling Configuration Class 
            //  dt = new DocDetails(xmldocProducts );
            //  dt.SetProducts(nProd);
            //   dt.CallBudgetMethod(new sTheProducts()); 



               Done:
            if (bError)
            {

            }

 

            FileInfo
             fi1 = new FileInfo(sTarget + @"\Cust.xml");
            fi1.Delete();
            fi1 = new FileInfo(sTarget +@"\Products.xml");
            fi1.Delete();

            Console.WriteLine("Installer Processing Completed!");

 
        }

        private static NameValueCollection FindParmSet(string sProductName
            , string sQueryEnd
            , ArrayList allProducts
            , NameValueCollection nVals)
        {
            try
            {
                foreach (string s in nVals.Keys)
                { Debug.Print("Keys={0},Vals={1}", s, nVals.Get(s)); }
                string sRet = "";
                foreach (object o in allProducts)
                {
                    Products pd = (Products)o;

                    Debug.Print("Product is {0}", pd.sProduct);
                    if (pd.sProduct == sProductName && pd.sQueryEnd == sQueryEnd)
                    {  // Got It
                        // Marshall the Parms

                        NameValueCollection nvParms = new NameValueCollection();
                        foreach (string sk in pd.Nvp.AllKeys)
                        {
                            Debug.Print("Value = {0} {1}   {2}", sk, pd.Nvp.Get(sk), nVals.Get(sk));
                            if (pd.Nvp.Get(sk) == "Y")
                            { nvParms.Add(sk, nVals.Get(sk)); }
                        }
                        return nvParms;

                        break;
                    }


                }
                throw new Exception("File :" + sProductName + " is not found in Products.xml for parameters");
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string FindLiveFile(string sProductName
            , string sQueryEnd
            , ArrayList allProducts)
        {
            try
            {
                string sRet = "";
                foreach (object o in allProducts)
                {
                    Products pd = (Products)o;
                    Debug.Print("Product is {0}", pd.sProduct);
                    if (pd.sProduct == sProductName && pd.sQueryEnd == sQueryEnd)
                    {  // Got It
                        // Marshall the Parms


                        return pd.sFileName;

                        break;
                    }


                }
                throw new Exception("File :" + sProductName + " is not found in Products.xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private static void ConfigureASheet(
            string sLocalName
            , ref ArrayList allProducts
            , string sProductName
            , string sQueryEnd
            , NameValueCollection nParms
            , DocDetails dt
            , ref PcId pc
            )
        {
            /*
             * Sub to Call config on the Sheet with the Method 
             *  
             *
             */
            try
            {
                foreach (object o in allProducts)
                {
                    Products pd = (Products)o;
                    if (pd.sProduct == sProductName && pd.sQueryEnd == sQueryEnd)
                    {  // Got It
                        // Marshall the Parms
                        try
                        {
                            pc.LogEvent(sUnid
                                , "At dt.CallXlConfigure for " + sLocalName
                                , 0
                                , pc.IBatchNo);
                            dt.sTarget = sTarget;
                            dt.sOfficesw = sOfficeSw; 
                            dt.CallXlConfigure(pd, sLocalName, nParms);
                           
                        }
                        catch (Exception ex)
                        {
                            if (bWebLog)
                                pc.LogEvent(sUnid
                                   , ex.Message + "Results from Configure on  " + sLocalName
                                   , -1
                                   , pc.IBatchNo);
                            else
                                pc.FileLog(sUnid
                             , ex.Message + "Results from Configure on  " + sLocalName
                             , -1
                             , pc.IBatchNo);
                        }
                        finally
                        {

                            FileInfo fi = new FileInfo(sLocalName);
                            fi.Delete();
                            if (bWebLog)
                                pc.LogEvent(sUnid
                                   , "Success Results w/Delete from Configure on  " + sLocalName
                                   , 0
                                   , pc.IBatchNo);
                            else
                                pc.FileLog(sUnid
                             , "Success Results w/Delete from Configure on  " + sLocalName
                             , 0
                             , pc.IBatchNo);
                        }
                        break;
                    }


                }


            }
            catch (Exception ex)
            {
            }

        }
        private static void LoadMethods(ref NameValueCollection nV, XmlDocument xdoc, string sUnid)
        {

            try
            {
                XmlNodeList nodelist;
                nodelist = xdoc.SelectNodes("/Sheets/Parms");
                foreach (XmlNode n in nodelist)
                {


                }

            }
            catch (Exception ex) { }

        }

        private static void DownLoadASheet(string sAddr, string sFileName)
        {
            RequestCachePolicy policy = new RequestCachePolicy(RequestCacheLevel.Reload);
            bool bIsWeb = false;

            if (sAddr.Contains(@"http"))
                bIsWeb = true;
            else
                bIsWeb = false;
            if (bIsWeb)
            {
                string address = sAddr;
                //"http://localhost/Sheets/" + sFileName + ".xls";

                System.Threading.AutoResetEvent waiter = new System.Threading.AutoResetEvent(false);
                Uri uri = new Uri(address);
                try
                {



                    WebClient wc = new WebClient();
                    wc.Proxy = null;
                    wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                    wc.CachePolicy = policy;


                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wcDownLoadDone);
                    wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownLoadDone);
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcCallbackDown);

                    wc.DownloadFileAsync(uri, sFileName);
                    Console.Write("DownLoading with {0}", sFileName);
                    while (wc.IsBusy)
                    {
                        waiter.WaitOne(100);
                        Debug.Assert(wc.IsBusy, "Still Busy");
                    }
                    try
                    {
                        Debug.Print("Done");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    Console.WriteLine("Done with {0}", sFileName);
                    // Got the File
                    // Download the Files 

                }

                catch (WebException ex)
                {


                    Console.WriteLine("  Download Error -" + ex.Message);
                    throw ex;
                    //   Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
            else
            {// is a file copy 
                try
                {
                    const string quote = "\"";

                    File.Copy(  sAddr   ,   sFileName   ,true);
                }
                catch (Exception ex)
                { throw ex; }
               
            }
        }
        private static void wcDownLoadDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("  Error Downloading  - Message - {0}", e.Error.Message);
                UncBasedInstaller.wslogger.WS_Logger ws = new UncBasedInstaller.wslogger.WS_Logger(); 
              //  Installer.wslogger.WS_Logger ws = new Installer.wslogger.WS_Logger();
                ws.Logger(sUnid, sCurrentFile + " - " + e.Error.Message, -1, iBatchCl);
                //       throw   new Exception(e.Error.Message ) ;
                //   Console.ReadLine(); 

            }

            Console.WriteLine("File Download Complete");
        }
        //private static void wcDownLoadDone(object sender, DownloadDataCompletedEventArgs e)
        //{
        //    Debug.Print("DONE!");
        //}
        private static void wcCallbackDown(object sender, DownloadProgressChangedEventArgs e)
        {
            if ( (iDotCount % 100) == 0 ) 
            Console.Write(".");
            
            iDotCount += 1;

        }

    }
}

