﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data; 
 
using System.Collections;

namespace EncryptFile
{
    //****************************************************
    // Created by Michael Moen
    // 28 Dec 2010
    // Datbase work class for writing out the xml objects 
    //*****************************************************
   partial class DBWrite
    {
       private sViewEntry sView;
       private SqlConnection cn;
       private SqlCommand cmd;
       private int iParentid;
       private string sCurrentUnid; 
      // Constructor 
       public  DBWrite()
       {
       }
       public DBWrite(sViewEntry eSview)
       {
           sView = eSview; 

       }
       public void  DBProcess() 
       {
         
           int irowid = 0; 
           cn = new SqlConnection(Properties.Settings.Default.Connectionstr);
           cmd = new SqlCommand();
        
       
           try
           {
            
           
                   cn.Open();

                   cmd.CommandText = "delete from dbo.XMLParmsDtl ; delete from dbo.XMLParmsHdr ;";
                   cmd.CommandType = CommandType.Text;
                   cmd.Connection = cn;
                   cmd.ExecuteNonQuery(); 
                  
                   foreach (sNode s in sView.aList)
                   {
                       sCurrentUnid = s.sUnid;

                       //    string scmd = "";
                       //    scmd = "INSERT INTO  [dbo].[XMLParmsHdr]"
                       //+ "([unid]"
                       //+ " ,[xmlFld]"
                       //+ ",[crtdate])"
                       //+ " VALUES "
                       //+ s.sUnid.ToString() + ","
                       //+ s.val.ToString() + ","
                       //+ "getdate())";

                       // cmd.CommandText = scmd;
                       try
                       {
                           cmd.CommandText = "usp_AddXmlHdr";
                           cmd.CommandType = CommandType.StoredProcedure;
                     
                           SqlCommandBuilder.DeriveParameters(cmd);

                           cmd.Parameters["@unid"].Value = s.sUnid;
                           cmd.Parameters["@xml"].Value = s.val;
                           cmd.Parameters["@res"].Value = 0;
                           cmd.ExecuteNonQuery();
                           irowid = (int)cmd.Parameters["@res"].Value;
                           // Load the current Parent this will change 
                           iParentid = irowid; 


                      //     foreach (sKeyValue sk in s.aColl)
                           foreach(object o in s.aColl ) 
                           {
                               if (o is sKeyValue)
                               {
                                   sKeyValue sk = (sKeyValue)o;
                                   Debug.Print("SK Name {0}", sk.sKey);


                                   cmd.CommandText = "usp_AddXmlDtl";
                                   SqlCommandBuilder.DeriveParameters(cmd);
                                   cmd.Parameters["@unid"].Value = sCurrentUnid ;

                                   cmd.Parameters["@rowid"].Value = 0;
                                   cmd.Parameters.Add("@res", SqlDbType.Int);
                                   cmd.Parameters["@res"].Value = 0;
                                   cmd.Parameters["@res"].Direction = ParameterDirection.ReturnValue;
                                   cmd.Parameters["@sheet"].Value = "N"; 
                                   cmd.Parameters["@val"].Value = sk.sValue;
                                   cmd.Parameters["@key"].Value = sk.sKey;
                                   cmd.Parameters["@id"].Value = irowid;
                                   cmd.Parameters["@parentid"].Value = 0; 
                                   cmd.Parameters["@att"].Value = sk.sAttributes; 
                                   cmd.ExecuteNonQuery();
                               
                                   
                                   if (sk.bHasChildren)
                                   iParentid =(int)  cmd.Parameters["@rowid"].Value;
                                   
                                   if ((int)(cmd.Parameters["@res"].Value) != 0)
                                       break;
                               }
                               if (o is sNode)
                               ProcessChildren(o, irowid);
                           }

                       }
                       catch (SqlException ex)
                       {
                           Console.WriteLine("Error - {0}", ex.Message);
                       }
                       catch (Exception ex)
                       {
                           Console.WriteLine("Error - {0}", ex.Message);
                       }


                   }


               
           }
           catch (SqlException ex)
           {
           }
           catch (Exception ex)
           {
           }
           finally
           {
               cn.Close();
               cn.Dispose();
           }


           
       }    
       private void ProcessChildren(object o, int iRowId)

       {
           try
           {
               if (o is sNode)
               {
                   sNode s = (sNode)o;
// MJM 
// Added to reflect Heirarchies 
// 30 Dec 2010
                
             
                   //cmd.CommandText = "usp_AddXmlDtl";
                   //SqlCommandBuilder.DeriveParameters(cmd);
                   //cmd.Parameters["@unid"].Value = s.sUnid;


                   //cmd.Parameters.Add("@res", SqlDbType.Int);
                   //cmd.Parameters["@res"].Value = 0;
                   //cmd.Parameters["@res"].Direction = ParameterDirection.ReturnValue;

                   //cmd.Parameters["@val"].Value = s.val;
                   //cmd.Parameters["@key"].Value = s.sUnid;
                   //cmd.Parameters["@id"].Value = iRowId;
                   //cmd.Parameters["@parentid"].Value = 0;
                   //cmd.ExecuteNonQuery();


                   foreach (object o1 in s.aColl)
                   {
                       if (o1 is sKeyValue)
                       {
                           sKeyValue sk = (sKeyValue)o1; 
                           cmd.CommandText = "usp_AddXmlDtl";
                           SqlCommandBuilder.DeriveParameters(cmd);
                           cmd.Parameters["@unid"].Value = sCurrentUnid;
                           if (sk.bProduct)
                               cmd.Parameters["@sheet"].Value = "Y"; 


                           cmd.Parameters["@rowid"].Value = 0;
                            cmd.Parameters["@att"].Value = sk.sAttributes; 
                           cmd.Parameters.Add("@res", SqlDbType.Int);
                           cmd.Parameters["@res"].Value = 0;
                           cmd.Parameters["@res"].Direction = ParameterDirection.ReturnValue;

                           cmd.Parameters["@val"].Value = sk.sValue;
                           cmd.Parameters["@key"].Value = sk.sKey;
                           cmd.Parameters["@id"].Value = iRowId;
                           cmd.Parameters["@parentid"].Value = iParentid;
                           cmd.ExecuteNonQuery();

                           if ((int)(cmd.Parameters["@res"].Value) != 0)
                               break;
                       }
                       if (o1 is sNode)
                           ProcessChildren(o1, iRowId); 
                   }
               }
               if (o is sKeyValue)
               {
                   sKeyValue sk = (sKeyValue)o; cmd.CommandText = "usp_AddXmlDtl";
                   SqlCommandBuilder.DeriveParameters(cmd);
                   cmd.Parameters["@unid"].Value = sCurrentUnid;
                    cmd.Parameters["@att"].Value = sk.sAttributes; 
                   cmd.Parameters["@rowid"].Value = 0;
                   cmd.Parameters["@issheet"].Value = "N";
                   cmd.Parameters.Add("@res", SqlDbType.Int);
                   cmd.Parameters["@res"].Value = 0;
                   cmd.Parameters["@res"].Direction = ParameterDirection.ReturnValue;

                   cmd.Parameters["@val"].Value = sk.sValue;
                   cmd.Parameters["@key"].Value = sk.sKey;
                   cmd.Parameters["@id"].Value = iRowId;
                  
                   cmd.ExecuteNonQuery();

               }
               
           }
             

           catch (Exception ex)
           {
           }
           }
   
    }
}
