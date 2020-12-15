
using System;
//using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
 
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Data;
using System.Configuration;

namespace CookWebUtility
{
   public  partial class BldXml
    {
        private static SqlConnection cn;
        public bool bSingleton;
 
        private static void SqlCust()
        {
            SqlCommand cmd;
            string sServer = ConfigurationManager.AppSettings["Host"]; 
            string sConnect = ConfigurationManager.ConnectionStrings[sServer].ConnectionString;
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
                ArrayList cCustomer = new ArrayList();

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
       // add mjm
        private static void SqlCust(string sUnid)
        {
            SqlCommand cmd;
            string sServer = ConfigurationManager.AppSettings["Host"];
            string sConnect = ConfigurationManager.ConnectionStrings[sServer].ConnectionString;
            cn = new SqlConnection(sConnect);
 

            if (cn.State != ConnectionState.Open)
            { cn.Open(); }
            try
            {
          

 
                ArrayList cCustomer = new ArrayList();

             
                 
                    cCustomer.Add(sUnid);
         

            

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

       //
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

               
                sXmlDoc = (string)dr.GetString(0);
                
                dr.Close();
                dr.Dispose(); 

                string sXmlDoce;
                sB1 = str.GetBytes(sXmlDoc);

                //    sB1 = str.GetBytes(sXmlDoc);
                sXmlDoce = Convert.ToBase64String(sB1);
                sB1 = str.GetBytes(sXmlDoce);
                //   sB1 = str.GetBytes(sXmlDoc);

                BasicFtp.BasicFTPClient bftp = new 
                    BasicFtp.BasicFTPClient(
                     ConfigurationManager.AppSettings["webusername"]
                    ,ConfigurationManager.AppSettings["webpassword"]
                    ,ConfigurationManager.AppSettings["webhost"]);

                string sPath =   @"/XML/" + sUnid  ;

                // if missing build it!
                try
                {
                    bftp.MakeDir(sPath);
                }
                catch
                { } 
                sPath += @"/Cust.Xml";
                bftp.Username = "cooksupport";
                bftp.Password = "Helpme01";
                bftp.Host = @"ccisupportsite.com"; 
                bftp.UploadData(sPath, sB1); 
                


            }
            catch (Exception ex)
            { throw ex; }


        }



       public static  void MainSub( )
        {
            // MJM  3 Jan 2011
            // Purpose
            // Open XML Sheet and set the properties on the sheet 
            try
            {
                
                SqlCust();

             

            }
            catch (Exception ex)
            {
                throw ex; 
            }

        }

       public static    void MainSub(string sUnid)
       {
           // MJM  3 Jan 2011
           // Purpose
           // Open XML Sheet and set the properties on the sheet 
           try
           {
             
               SqlCust(sUnid);



           }
           catch (Exception ex)
           {
               throw ex;
           }

       }


        private static int ReadFile(String sFile)
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