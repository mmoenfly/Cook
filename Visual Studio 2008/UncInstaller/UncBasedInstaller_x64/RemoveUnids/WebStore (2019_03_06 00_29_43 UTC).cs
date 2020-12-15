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
using System.Data.SqlClient;
using System.Data.Sql;
 
 
namespace RemoveUnids
{
    class WebStore
    {
        static string sHost, sUser, sPass;
        static List<String>  slCustomers = new List<string>() ;
        public bool bDelFlag;
        public WebStore(string sActionVerb)
        {
            if (sActionVerb == "DELETE")
                bDelFlag = true;
            else
                bDelFlag = false;

               listUnid(  ) ;
        }

        private void listUnid( )
        {
            try
            {

                // Retrieve Unids from Domino
                Console.WriteLine("Domino Web Service session Begins");
                fetchCustomerList();



                sHost = Properties.Settings.Default.Host;
                sUser = Properties.Settings.Default.User;
                sPass = Properties.Settings.Default.Password;

                BasicFTPClient ftp = new BasicFTPClient(sUser, sPass, sHost);
                //Retrieve Folders from Ftp Server
                Console.WriteLine("Ftp session Begins");
                string[] sFolders = ftp.FTPListFolders(@"ftp://" + sHost + @"/XML", false);


                Console.WriteLine("Compare session Begins");
                // 9 August 2011
                // do not execute without a proper customer list
                // this would clean out the xml direcotry.

                if (slCustomers.Count > 0)
                    CompareLists(sFolders, bDelFlag);
            }
            catch (Exception ex)
            { throw ex; }

         return;
       
            

            

        }

        private void CompareLists(string[] sFolders, bool bDeleteFlag)
        {
         string  sFileName = Properties.Settings.Default.LogFileLoc 
                           + @"\" + sHost + @"_Log_"  + Convert.ToString(DateTime.Now.DayOfWeek) 
                           +  ".txt";
         StreamWriter sw = new StreamWriter(sFileName,false) ;
         sw.WriteLine("Cleanup Begins on {0} at {1}", sHost, DateTime.Now);
            if (bDeleteFlag)
                sw.WriteLine("Delete Flag is active.");
            else
                sw.WriteLine("Delete Flag is inactive.");
            int iHits=0, iMisses = 0;

            BasicFTPClient ftp = new BasicFTPClient(sUser, sPass, sHost);
            // Compare the Folders on FTP Server
            // Against the Contents of the Domino Server
            foreach (string fld in sFolders)
            {
                string usfld = fld.ToUpper(); 

                string ssTerm = usfld.Replace(@"FTP://" +  sHost.ToUpper() + @"/XML/", "");
                if (slCustomers.Contains(ssTerm))
                {
                    // do nothing
                    Debug.Print("Keep it!{0}",ssTerm);
                    Console.WriteLine("Keeping {0}", ssTerm);
                    iHits += 1;
                    sw.WriteLine("Keeping {0}", ssTerm);
                }
                else
                {
                  
                    Debug.Print("Get Rid of it {0}",ssTerm);
                    Console.WriteLine("Killing {0}", ssTerm);
                    sw.WriteLine("Killing {0}", ssTerm);
                    
                    if (bDeleteFlag)
                    {
                        try
                        {
                            iMisses += 1;
                            // recursive delete on folder - bye bye
                            // 9 August 2011
                            ftp.DeleteTree(@"ftp://" + sHost + @"/xml/" + ssTerm);
                            // Chg -001 
                            CleanXml(ssTerm);
                            // End change
                        }
                        catch (Exception ex)

                        { throw ex;  }
                    }

                   
                   
                }
            }

            // -001
            // Compare the xml on file in sql server to Domino List
            // If missing Delete

            SqlDataReader dr;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.SqlConnString))
            {
                cn.Open();
                SqlConnection cn2 = new SqlConnection(Properties.Settings.Default.SqlConnString);
                cn2.Open(); 

                SqlCommand cmd = new SqlCommand("select distinct unid from dbo.XmlParmsDtl order by unid");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string sOnFileUnid = dr["Unid"].ToString();

                    if (slCustomers.Contains(sOnFileUnid))
                    {
                    }
                    else
                    // Get rid of it
                    {

                        if (bDeleteFlag)
                        {

                            sw.WriteLine("Killing {0} on SQL Server", sOnFileUnid);
                            using (SqlCommand cmd2 = cn2.CreateCommand())
                            {
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.CommandText = "usp_Clean_Xml_Rows";
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.Add(
                      new SqlParameter("@Unid", sOnFileUnid));


                               cmd2.ExecuteNonQuery();


                            }
                        }

                    }

                }

                cn.Close();
                cn2.Close();
                cn2.Dispose(); 




            }




            // -001 End

            Debug.Print("Hits = {0}, and Misses = {1}", iHits, iMisses);
            Console.WriteLine("Hits = {0}, and Misses = {1}", iHits, iMisses);
            sw.WriteLine("Unids Hits = {0}, and Unids Deleted = {1}", iHits, iMisses);
            sw.WriteLine("Cleanup Ends on {0} at {1}",sHost, DateTime.Now); 
            sw.Flush() ; 
            sw.Close();
            sw.Dispose() ;
        }

        private void CleanXml(  string sUnid)
        {
            // Implemented 6 Sept 2011
            // 001 
             // Method to back Clean the xml out of the store on 
            // Sql Server 

            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.SqlConnString))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "usp_Clean_Xml_Rows";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
          new SqlParameter("@Unid", sUnid));


                    cmd.ExecuteNonQuery();
                    cn.Close(); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fetchCustomerList()
        {

            CookWebSrv.ListCustomers ws = new CookWebSrv.ListCustomers();
            ws.PreAuthenticate = true;

            ws.Credentials = new NetworkCredential(Properties.Settings.Default.DominoUser, Properties.Settings.Default.DominoPass);
            try
            {
                string sXml = ws.LISTCUSTOMERS("changeor.nsf", "xmloutput2");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(sXml);
                XmlNodeList nodes = xmldoc.SelectNodes("//unid");
                foreach (XmlNode n in nodes)
                {
                    slCustomers.Add(n.InnerXml.ToString());
                }
            }
            catch (Exception ex)
            { throw ex; } 
        }
    }
}

