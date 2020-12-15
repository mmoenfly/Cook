using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;
using System.Data; 
 

namespace UpDateSheet
{
    class ReadDBandWriteXML
    {
      private  SqlCommand cmd;
      private SqlConnection cn;
      public XmlDocument xmldoc;
      
      public ReadDBandWriteXML()
          // Construct the Class
      {
      }
      ~ReadDBandWriteXML()
      {
          cn.Close();
          cn.Dispose();
      }
        public void WriteXML(string sUnid )
        {
            try
            {
                if (cn == null)
                {
                    cn = new SqlConnection(Properties.Settings.Default.Connectionstr);
                    cn.Open();

                }
                if (cmd == null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "usp_retrieve_xml";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn; 

                }


            }
            catch (SqlException ex) { }
            catch (Exception ex) { }

        }




    }
}
