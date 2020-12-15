using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicFtp;
using System.IO; 
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.Data;
using System.Collections;
using System.Web;
using System.Net.Cache;
 
using System.Collections.Specialized;
 
 
using System.ComponentModel;
 

 


namespace LoadServers
{
    class WebStore
    {
        static string sHost, sUser, sPass;
        static int iDotCount;
        static List<String>  slCustomers = new List<string>() ;
        static BasicFTPClient ftp;
       
        public WebStore(string sActionVerb)
        {
           
               rtvFiles(  ) ;
        }

        private void rtvFiles( )
        {
            try
            {
                string sTrainDir = LoadClient.Properties.Settings.Default.TrainDir;

                if (Directory.Exists(sTrainDir))
                {
                    Directory.Delete(sTrainDir, true);
                    Directory.CreateDirectory(sTrainDir);
                }
                else
                { Directory.CreateDirectory(sTrainDir); }

                sHost = LoadClient.Properties.Settings.Default.Host;
                sUser = LoadClient.Properties.Settings.Default.User;
                sPass = LoadClient.Properties.Settings.Default.Password;
                DownLoadASheet(@"Http://ccisupportsite.com/Apps/LoadCLient/trainingfiles.txt"
                    , LoadClient.Properties.Settings.Default.TrainDir + "trainingfiles.txt");
                //    ftp = new BasicFTPClient(sUser, sPass, sHost);
                //Retrieve Folders from Ftp Server
                Console.WriteLine("Ftp session Begins");
                //  string[] sFolders = ftp.FTPListTree(@"ftp://" +
                //       sHost + LoadClient.Properties.Settings.Default.FileStore  );
                ArrayList sFolders = new ArrayList();
                using (StreamReader sr = new StreamReader(LoadClient.Properties.Settings.Default.TrainDir + "trainingfiles.txt"))  
                {
                    while (! sr.EndOfStream)
                    {
                        string line;
                        line = sr.ReadLine();
                        line = line.Trim(); 
                        if (line.Length > 0 )
                           sFolders.Add(line);


                    }

                }
                Console.WriteLine("Download session Begins");
                // 9 August 2011
                // do not execute without a proper customer list
                // this would clean out the xml direcotry.


                DownloadFiles(sFolders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(); 
                throw ex; }

         return;
       
            

            

        }

        private void DownloadFiles(ArrayList sFolders)
        {

            string sDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\CCI SpreadSheets";

            if (Directory.Exists(sDesktop))
            {
                Directory.Delete(sDesktop, true);
                Directory.CreateDirectory(sDesktop);
            }
            else
            { Directory.CreateDirectory(sDesktop); }

            DirectoryInfo di = new DirectoryInfo(sDesktop);


            if (Directory.Exists(LoadClient.Properties.Settings.Default.LogFileLoc))
            {
                Directory.Delete(LoadClient.Properties.Settings.Default.LogFileLoc, true);
                Directory.CreateDirectory(LoadClient.Properties.Settings.Default.LogFileLoc);
            }
            else
            { Directory.CreateDirectory(LoadClient.Properties.Settings.Default.LogFileLoc); }




            string sFileName = LoadClient.Properties.Settings.Default.LogFileLoc
                            + sHost + @"_Log_" + Convert.ToString(DateTime.Now.DayOfWeek)
                           + ".txt";
            StreamWriter sw = new StreamWriter(sFileName, false);
            sw.WriteLine("Download Begins on {0} at {1}", sHost, DateTime.Now);
            char[] delimiterChars = { '/' };
            int iHits = 0;
           
            //   BasicFTPClient ftp = new BasicFTPClient(sUser, sPass, sHost);
            // Compare the Folders on FTP Server
            // Against the Contents of the Domino Server
            foreach (string fld in sFolders)
            {
                // Download each file
                string usfld = fld.ToUpper();

                usfld = HttpUtility.UrlDecode(usfld); 
               // string ssTerm =
               //     usfld.Replace(@"FTP://" + sHost.ToUpper(), "");
               // string[] sSplit = ssTerm.Split(delimiterChars);
                iHits += 1;
                Console.WriteLine("Downloading {0} File {1} of {2}", fld, iHits, sFolders.Count);
                string sLoc = sDesktop + @"\" + usfld;
             //   ftp.DownloadFile(ssTerm, sLoc);
                DownLoadASheet(@"http://" + sHost + @"/Apps/TrainingFiles/" + usfld, sLoc); 






            }

            Console.WriteLine("Total Downloaded Files{0}", iHits);

            sw.WriteLine("Download Ends on {0} at {1}", sHost, DateTime.Now);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        //private void CleanXml(  string sUnid)
        //{
        //    // Implemented 6 Sept 2011
        //    // 001 
        //     // Method to back Clean the xml out of the store on 
        //    // Sql Server 

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.SqlConnString))
        //        {
        //            cn.Open();
        //            SqlCommand cmd = cn.CreateCommand();
        //            cmd.CommandText = "usp_Clean_Xml_Rows";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(
        //  new SqlParameter("@Unid", sUnid));


        //            cmd.ExecuteNonQuery();
        //            cn.Close(); 
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void fetchCustomerList()
        //{

        //    CookWebSrv.ListCustomers ws = new CookWebSrv.ListCustomers();
        //    ws.PreAuthenticate = true;

        //    ws.Credentials = new NetworkCredential(Properties.Settings.Default.DominoUser, Properties.Settings.Default.DominoPass);
        //    try
        //    {
        //        string sXml = ws.LISTCUSTOMERS("changeor.nsf", "xmloutput2");
        //        XmlDocument xmldoc = new XmlDocument();
        //        xmldoc.LoadXml(sXml);
        //        XmlNodeList nodes = xmldoc.SelectNodes("//unid");
        //        foreach (XmlNode n in nodes)
        //        {
        //            slCustomers.Add(n.InnerXml.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; } 
        //}



        private static void DownLoadASheet(string sAddr, string sFileName)
        {


            try
            {
                if (File.Exists(sFileName))
                {
                    FileInfo fi = new FileInfo(sFileName);

                    fi.Delete();
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }


            RequestCachePolicy policy = new RequestCachePolicy(RequestCacheLevel.Reload);

            string address = sAddr;
            //"http://localhost/Sheets/" + sFileName + ".xls";

            System.Threading.AutoResetEvent waiter = new System.Threading.AutoResetEvent(false);
            Uri uri = new Uri(address);
            try
            {
                WebClient wc = new WebClient();
                wc.Proxy = null;
                wc.Credentials = CredentialCache.DefaultCredentials;
                wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                wc.CachePolicy = policy;


                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wcDownLoadDone);
                wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownLoadDone1);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcCallbackDown);

                wc.DownloadFileAsync(uri, sFileName);
                Console.Write("DownLoading with {0} ", sFileName);
                while (wc.IsBusy)
                {

                    try
                    {
                        waiter.WaitOne(100);
                    }
                    catch (Exception ex)

                    { Console.WriteLine(ex.Message); }
                }

                try
                {
                    Debug.Print("Done");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " Error in Download");
                }


                Console.WriteLine("Done with {0} ", sFileName);
                // Got the File
                // Download the Files 

            }

            catch (WebException ex)
            {



                if (ex.Response is WebResponse)
                {
                    HttpWebResponse o = (HttpWebResponse)ex.Response;
                    
                    Console.WriteLine(o.StatusDescription + "  Download Error -" + ex.Message);
                    if (o.StatusCode == HttpStatusCode.OK)
                    { }
                    else
                    {
                        Console.WriteLine(ex.Status + "  Download Error -" + ex.Message);
                        Console.ReadLine();
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
        private static void wcDownLoadDone(object sender, AsyncCompletedEventArgs e)
        {
            // if (e.Error != null)
            //{
            // //    Console.WriteLine("  Error Downloading Async Completed Handler - Message - {0}", e.Error.Message);
            // //    Console.WriteLine(e.Error.StackTrace.ToString());
            // //    Console.WriteLine(e.Error.InnerException.ToString());
            //     Installer.wslogger.WS_Logger ws = new Installer.wslogger.WS_Logger();
            //   ws.Logger(sUnid, sCurrentFile + " - " + e.Error.Message, -1, iBatchCl);
            // //    //    throw new Exception("Downloading Error"); 
            // //       throw   new Exception(e.Error.Message ) ;
            // //    //   Console.ReadLine(); 

            // }

            Console.WriteLine("File Download AsyncComplete Complete");
        }
        private static void wcDownLoadDone1(object sender, DownloadDataCompletedEventArgs e)
        {
            //if (e.Error != null)
            //    {
            //        //Console.WriteLine("Status Code is {0}", 1);
            //        //Console.WriteLine("  Error Downloading Data Completed Handler  - Message - {0}", e.Error.Message);
            //        //Console.WriteLine(e.Error.StackTrace.ToString());
            //        //Console.WriteLine(e.Error.InnerException.ToString());
            //         Installer.wslogger.WS_Logger ws = new Installer.wslogger.WS_Logger();
            //         ws.Logger(sUnid, sCurrentFile + " - " + e.Error.Message, -1, iBatchCl);
            //        //throw new Exception("Downloading Error");
            //     //   throw new Exception(e.Error.Message);
            //      //  Console.ReadLine(); 

            //  }
            Console.WriteLine("File Download DoneComplete Complete");
        }
        private static void wcCallbackDown(object sender, DownloadProgressChangedEventArgs e)
        {
            if ((iDotCount % 50) == 0)
                Console.Write(".");

            iDotCount += 1;
        }

    }

}

