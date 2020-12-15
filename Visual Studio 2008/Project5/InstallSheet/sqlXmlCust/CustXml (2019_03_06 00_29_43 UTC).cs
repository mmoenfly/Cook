using System;
using System.Collections.Generic;
 
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
 
 
using System.Xml;
namespace CookWebUtilities
{
    public class XmlCustomer
    {
        private  string  
              sEmail
           
            , sUnid
            , sProduct
            , sOs
            , sTargetdir
            , sSystemname 
            , sOfficeVersion
            , sId;

        public NameValueCollection nvAttrib;
        public XmlCustomer()
        {
            throw new NotImplementedException();
        }
        public XmlCustomer(string sconnectionString, string sUnid, int iBatch)
        {
            string xml = sqlLookup(sconnectionString, sUnid, iBatch);
            setProperties(xml);


        }
        public XmlCustomer(string xmldoc)
        {
            setProperties(xmldoc);



        }

        public string Id
        {
            get
            {
                return sId;
            }


            set
            {
                sId = value;
            }
        }

        public string OfficeVersion
        {
            get
            {
                return sOfficeVersion;
            }


            set
            {
                sOfficeVersion = value;
            }
        }


        public string SystemName
        {
            get
            {
                return sSystemname;
            }


            set
            {
                sSystemname = value;
            }
        }

        public string TargetDir
        {
            get
            {
                return sTargetdir;
            }


            set
            {
               sTargetdir = value;
            }
        }

        public string OS
        {
            get
            {
                return sOs;
            }


            set
            {
                sOs = value;
            }
        }

        public string Product
        {
            get
            {
                return sProduct;
            }


            set
            {
                sProduct = value;
            }
        }

        public string Unid
        {
            get
            {
                return sUnid;
            }


            set
            {
                sUnid = value;
            }
        }


        

        public string Email
        {
            get
            {
                return sEmail;
            }


            set
            {
                sEmail = value;
            }
        }


        private void setProperties(string xmlstr) 
        {

            nvAttrib = new NameValueCollection();

                       XmlDocument xmldoc = new XmlDocument();
                       //  xmlstr = xmlstr.Replace(@"\","") ; 
                       xmldoc.LoadXml(xmlstr);

                       XmlNode root;

                       root = xmldoc.DocumentElement;

                       XmlNodeList nodes;
                       nodes = root.SelectNodes("PC");
                       foreach (XmlNode aNode in nodes)
                       {
                           try
                           {
                               foreach (XmlAttribute xatt in aNode.Attributes)
                               {
                                   nvAttrib.Add(xatt.Name, xatt.Value);
                               }
                           }
                           catch
                           { }


                           
                           //try
                           //{
                           //    this.Email = aNode.Attributes["email"].Value;
                             
                           //}

                           //catch { }
                           //try
                           //{
                           //    this.OfficeVersion = aNode.Attributes["officeversion"].Value;
                               
                           //}


                           //catch { }
                           //try
                           //{
                           //    this.OS = aNode.Attributes["os"].Value;
                              
                           //}


                           //catch { }
                           //try
                           //{
                           //    this.Id= aNode.Attributes["id"].Value;
                              
                           //}


                           //catch { }

                           //try
                           //{
                           //    this.Product = aNode.Attributes["product"].Value;
                              
                           //}


                           //catch { }
                             

                           //try
                           //{
                           //    this.TargetDir = aNode.Attributes["targetdir"].Value;
                                
                           //}


                           //catch { }

                           //try
                           //{
                           //    this.SystemName = aNode.Attributes["systemname"].Value;
                                
                           //}

                           //catch { }




                       }


                   }
       
       public string sqlLookup(string sConnectionString, string sUnid, int iBatch)
       {
           using (SqlConnection connection = new SqlConnection(sConnectionString))
           {
               connection.ConnectionString = sConnectionString;

               connection.Open();
               SqlCommand command = connection.CreateCommand();


               try
               {
                   command.CommandType = CommandType.Text;
                   // this is a no no
                   command.CommandText = "select  logmsg  from [control].[customerlog] where status = 99999 and  batchid = "
                       + Convert.ToString(iBatch) + " and unid = '" + sUnid + "'";
                   SqlDataReader dr;

                   dr = command.ExecuteReader();
                   if (dr.HasRows)
                   {
                       dr.Read();
                       string xmlstr = Convert.ToString(dr.GetString(0));

                       dr.Close();

                   return xmlstr;
                   }
                   else 
                       return null; 

               }

               catch (Exception ex)
               { 
                  throw ex;
               }



           }
       }
    }

}



    