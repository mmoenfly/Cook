using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class CookClr
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString XmlShred(SqlString sXmlStr, string sKey)
    {
        // created by mm
        // 11 Mar 2011
        // Purupose
        // Encapsulate the xml shred of the internal document 
        // within an upload transcript


        // Put your code here
        SqlString res = "";
        string sDocstr = Convert.ToString(sXmlStr);
        CookWebUtilities.XmlCustomer xmlcs = new CookWebUtilities.XmlCustomer(sDocstr);
   
        //switch (sKey)
        //{
        //    case "id":
        //        res = xmlcs.Id;
        //        break; 
        //    case "targetdir":
        //        res= xmlcs.TargetDir;
        //        break;
        //    case "systemname":
        //        res = xmlcs.SystemName;
        //        break;
        //    case "unid":
        //        res = xmlcs.Unid;
        //        break; 
           
        //    case "email" :
        //        res = xmlcs.Email ;
        //        break;

        //    case "product":
        //        res = xmlcs.Product;
        //        break;

        //    case "os":
        //        res = xmlcs.OS;
        //        break;

        //    case "officeversion":
        //        res = xmlcs.OfficeVersion;
        //        break;


        //    default:
        //        res = "Unknown";
        //        break;

        //}
        res = ""; 
        res = xmlcs.nvAttrib.Get(sKey); 
        return   res;


    }
};
 
