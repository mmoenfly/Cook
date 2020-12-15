using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Management;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using Microsoft.Win32; 

namespace TestHarness
{
    class PcId
    {
        private string sProduct, sSerialNumber, sUnid, sXmlDoc,sOffice;
        private XmlDocument xmldoc;
        public PcId(string sVal)
        {
            sUnid = sVal;
             sOffice = checkOffice(); 
            ManagementObjectSearcher searcher =
                   new ManagementObjectSearcher(@"root\cimv2", "SELECT Product, SerialNumber FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject mo in information)
            {
                sProduct = Convert.ToString(mo["Product"]);
                sSerialNumber = Convert.ToString(mo["SerialNumber"]);
            }

            // Build an xml to send to logger

            xmldoc = new XmlDocument();
           
            XmlNode docnode = xmldoc.CreateXmlDeclaration(@"1.0", @"UTF-8", @"no");

            xmldoc.AppendChild(docnode);


            XmlNode root = xmldoc.CreateElement("PCID");
            xmldoc.AppendChild(root);

            XmlNode PC = xmldoc.CreateElement("PC");
            XmlAttribute idattrib = xmldoc.CreateAttribute("id");
            idattrib.Value = sSerialNumber;
            PC.Attributes.Append(idattrib);

            idattrib = xmldoc.CreateAttribute("unid");
            idattrib.Value = sUnid;
            PC.Attributes.Append(idattrib);

            idattrib = xmldoc.CreateAttribute("os");
            idattrib.Value = Convert.ToString(Environment.OSVersion);
            PC.Attributes.Append(idattrib);

            idattrib = xmldoc.CreateAttribute("officeversion");
            idattrib.Value = Convert.ToString(sOffice);
            PC.Attributes.Append(idattrib);


            idattrib = xmldoc.CreateAttribute("systemname");
            idattrib.Value = Convert.ToString(Environment.MachineName);
            PC.Attributes.Append(idattrib);
            root.AppendChild(PC); 

         //   xmldoc.AppendChild(PC);
           //     StringWriter sw = new StringWriter();
           //XmlTextWriter xw = new XmlTextWriter(sw);
           //  xmldoc.Save(xw);
              
          
             sXmlDoc = xmldoc.OuterXml;
      
            //    xmldoc.Save(

            //XmlNode nameNode = doc.CreateElement("Name");
            //nameNode.AppendChild(doc.CreateTextNode("Java"));
            //productNode.AppendChild(nameNode);
            //XmlNode priceNode = doc.CreateElement("Price");
            //priceNode.AppendChild(doc.CreateTextNode("Free"));
            //productNode.AppendChild(priceNode);

            //// Create and add another product node.
            //productNode = doc.CreateElement("product");
            //productAttribute = doc.CreateAttribute("id");
            //productAttribute.Value = "02";
            //productNode.Attributes.Append(productAttribute);
            //productsNode.AppendChild(productNode);
            //nameNode = doc.CreateElement("Name");
            //nameNode.AppendChild(doc.CreateTextNode("C#"));
            //productNode.AppendChild(nameNode);
            //priceNode = doc.CreateElement("Price");
            //priceNode.AppendChild(doc.CreateTextNode("Free"));
            //productNode.AppendChild(priceNode);

        }
        public void setUnid(string sval)
        {
            sUnid = sval;
        }
        public void LogEvent()
        {
            TestHarness.com.ccisupportsite.WS_Logger ws = new TestHarness.com.ccisupportsite.WS_Logger();
           
            int iBatch = int.Parse(ws.FindNextId(sUnid));
            ws.Logger(sUnid, sXmlDoc, 0, iBatch);
            ws.Dispose();

        }
           private string checkOffice()

        { 
           
                    string sOffice ="";
            
            string[] sPaths = new string[]  
                {@"Software\Microsoft\Windows\CurrentVersion\App Paths\excel.exe"
            ,@"Software\Microsoft\Windows\CurrentVersion\App Paths"};
             try
            {
                RegistryKey pRegKey = Registry.LocalMachine;
              
                    pRegKey = pRegKey.OpenSubKey(sPaths[0]);
                 if (pRegKey == null)
                 {
                     pRegKey = Registry.CurrentUser;
                       pRegKey = pRegKey.OpenSubKey(sPaths[1]);
                 }
                  sOffice= Convert.ToString(pRegKey.GetValue(""));
                  sOffice = sOffice.ToUpper();
                  if (sOffice.Contains("14"))
                      return "14";

                  if (sOffice.Contains("12"))
                      return "12";

                  if (sOffice.Contains("11"))
                      return "12";
                 
                 
                 return "Unknown Office Version" ;
            
            }

            catch
            {return "Unknown";}
        }


    }
}
