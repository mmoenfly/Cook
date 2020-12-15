using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration; 

using System.Collections;

namespace CookWebUtility
{
    //****************************************************
    // Created by Michael Moen
    // 28 Dec 2010
    // Datbase work class for writing out the xml objects 
    //*****************************************************
  public   partial class DBWrite
    {
        private sViewEntry sView;
        private SqlConnection cn;
        private SqlCommand cmd;
        private int iParentid;
        private string sCurrentUnid;
      const string Host =  "ccisupportsite.com"; 
        // Constructor 
        public DBWrite()
        {
        }
        public DBWrite(sViewEntry eSview)
        {
            sView = eSview;

        }
        public void DBProcess()
        {
            string sConnSrv =  Host;
            string sConnStr = "Context Connection=true";
            int irowid = 0;
            cn = new SqlConnection(sConnStr);
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
                        foreach (object o in s.aColl)
                        {
                            if (o is sKeyValue)
                            {
                                sKeyValue sk = (sKeyValue)o;
                                Debug.Print("SK Name {0}", sk.sKey);


                                cmd.CommandText = "usp_AddXmlDtl";
                                SqlCommandBuilder.DeriveParameters(cmd);
                                cmd.Parameters["@unid"].Value = sCurrentUnid;

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
                                    iParentid = (int)cmd.Parameters["@rowid"].Value;

                                if ((int)(cmd.Parameters["@res"].Value) != 0)
                                    break;
                            }
                            if (o is sNode)
                                ProcessChildren(o, irowid);
                        }

                    }
                    catch (SqlException ex)
                    {
                        throw ex; 
                    }
                    catch (Exception ex)
                    {
                        throw ex; 
                    }


                }



            }
            catch (SqlException ex)
            {
                throw ex; 
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
      // add mjm
        public void DBProcessSingle(string sUnid)
        {
            string sConnSrv =  Host;
            string sConnStr = "Context Connection=true";
            int irowid = 0;
            cn = new SqlConnection(sConnStr);
            cmd = new SqlCommand();


            try
            {
                cn.Open(); 
                string sCmdText = @"delete from dbo.XMLParmsDtl Where unid = '"
                      + sUnid + "';  delete from dbo.XMLParmsHdr where unid = '"
                      + sUnid + "';";
                cmd.CommandText = sCmdText;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                

                foreach (sNode s in sView.aList)
                {
                    sCurrentUnid = s.sUnid;


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
                        foreach (object o in s.aColl)
                        {
                            if (o is sKeyValue)
                            {
                                sKeyValue sk = (sKeyValue)o;
                                Debug.Print("SK Name {0}", sk.sKey);


                                cmd.CommandText = "usp_AddXmlDtl";
                                SqlCommandBuilder.DeriveParameters(cmd);
                                cmd.Parameters["@unid"].Value = sCurrentUnid;

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
                                    iParentid = (int)cmd.Parameters["@rowid"].Value;

                                if ((int)(cmd.Parameters["@res"].Value) != 0)
                                    break;
                            }
                            if (o is sNode)
                                ProcessChildren(o, irowid);
                        }

                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                }



            }
            catch (SqlException ex)
            {
                throw ex; 
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
      // add mjm 

        private void ProcessChildren(object o, int iRowId)
        {
            try
            {
                if (o is sNode)
                {
                    sNode s = (sNode)o;
                


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
