using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Net;



public partial class SqlUnid
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static SqlInt32  LoadSingleUnid(string sUnid )
    {
     
        // Created by Michael Moen
        // 10 Feb 2011
        // Description
        // Will Load a Single Unid

        // Lets pull a unid 
        try
        {
          
            CookWebUtility.xml_instance xmli = new CookWebUtility.xml_instance();
            SqlUnidLoad.CookSingle.GetCustomersNewService cs
                = new SqlUnidLoad.CookSingle.GetCustomersNewService();

            NetworkCredential objCredential =
              new NetworkCredential("mmoen", "bobbob1", "");


            cs.Credentials = objCredential;
            cs.Timeout = 600000;
            cs.PreAuthenticate = true;
            
            xmli.sXmlClean = cs.GETCUSTOMERSNEW("changeor.nsf", "XMLOutput", sUnid);

        

            xmli.LoadXDoc();
            xmli.xml_Configure();

            CookWebUtility.DBWrite db = new CookWebUtility.DBWrite(xmli.sView);
            db.DBProcessSingle(sUnid);

            
            CookWebUtility.BldXml.MainSub(sUnid);
           
            return 0;
        }
        catch (Exception ex)
        { throw ex; } 
    }
};
