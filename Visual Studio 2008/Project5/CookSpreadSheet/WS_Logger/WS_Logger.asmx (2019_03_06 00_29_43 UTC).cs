using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.Collections.Specialized; 
namespace WS_Logger
{
    /// <summary>
    // Summary description for Ws_Logger
    // This web service will log to the database the results from a configuration step  
    /// </summary>
    [WebService(Namespace = "http://ccisupportsite.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_Logger : System.Web.Services.WebService
    {

        public  struct MyTest
        {
            public string name;
            public string[] test;
            public string errmsg; 
        }
        [WebMethod]
        public string Logger(string sUnid, string sMessage, int iStatus, int iBatchid)
        {
            try
            {
               

                string sconnectionString = "";

                sconnectionString = Properties.Settings.Default.Connection; 


                using (SqlConnection connection = new SqlConnection(sconnectionString))
                {
                   
                   

                
                    try
                    {
                        connection.ConnectionString = sconnectionString; 
                    
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.CommandText = "usp_CCI_Logger";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlCommandBuilder.DeriveParameters(command);

                        command.Parameters["@unid"].Value = sUnid;

                        command.Parameters["@msg"].Value = sMessage;

                        command.Parameters["@istatus"].Value = iStatus;

                        command.Parameters["@ibatchid"].Value = iBatchid;



                        object ret = command.ExecuteNonQuery();

                        command.Dispose();


                    }
                    catch (SqlException ex)
                    {
                        return ex.Message; 
                    }

                    catch(Exception ex) 
                    {
                        return "Sql Command Fails! " + ex.Message;
                    }
                }




                return "0";
            }
            catch
            {
                return "-1";
            }
           
        }


        [WebMethod]
        public string FindNextId(string sUnid)
        {
            try
            {


                string sconnectionString = "";

                sconnectionString = Properties.Settings.Default.Connection;


                using (SqlConnection connection = new SqlConnection(sconnectionString))
                {




                    try
                    {
                        connection.ConnectionString = sconnectionString;

                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.CommandText = "usp_Rtv_Batchid" ;
                        command.CommandType = CommandType.StoredProcedure;

                        SqlCommandBuilder.DeriveParameters(command);

                        command.Parameters["@unid"].Value = sUnid;
                        
                        command.Parameters["@batchid"].Value = 0; 
 



                        object ret = command.ExecuteNonQuery();

                        command.Dispose();

                        return (string)command.Parameters["@batchid"].Value;
                    }
                    catch (SqlException ex)
                    {
                        return ex.Message;
                    }

                    catch (Exception ex)
                    {
                        return "Sql Command Fails! " + ex.Message;
                    }
                }




               
            }
            catch
            {
                return "-1";
            }

        }

        [WebMethod]
        public MyTest Testd(string sName, string sList)
        {
              MyTest mt = new MyTest() ;
            try
            {
                mt.name = sName;
                string[] oSplit = sList.Split(',');
                mt.test = oSplit; 
                return mt; 
 
             }
                    

                    catch (Exception ex)
                    {
                        mt.errmsg = "BAD!"; 
                        return mt;
                    }
             }





            } 
   
}
