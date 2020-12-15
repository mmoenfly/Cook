using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration; 
using System.Net;
using System.IO;


namespace LoadSingleUnid
{
    class Program
    {
        
        static int Main(string[] args)
        {
            try
            {
                
                int iRes = 0;
                BasicFtp.BasicFTPClient ftp = new BasicFtp.BasicFTPClient(Properties.Settings.Default.webusername
                    ,Properties.Settings.Default.webpassword,Properties.Settings.Default.Host);

                string sUnid = Convert.ToString(args[0]);
                if (sUnid.Length == 0)
                { return -1; }
                else
                {


                    if (args[0] == "/all")
                    {

                        // Lets pull a unid 
                        CookWebUtility.xml_instance xmli = new CookWebUtility.xml_instance();


                        NetworkCredential objCredential =
                          new NetworkCredential("mmoen", "bobbob1", "");
                        CookCustomers.GetCustomersService cs = new CookCustomers.GetCustomersService();
                        cs.Credentials = objCredential;
                        cs.Timeout = 600000;
                        cs.PreAuthenticate = true;
                        Console.WriteLine("Retrieving XML Begin!");
                        xmli.sXmlClean = cs.GETCUSTOMERS("changeor.nsf", "XMLOutput");
                        StreamWriter sw = new StreamWriter(@"c:\xml.xml");
                        sw.Write(xmli.sXmlClean);
                        sw.Close();
                        sw.Dispose(); 
                        Console.WriteLine("Retrieving XML Completed!");

                       

                        xmli.LoadXDoc();
                        xmli.xml_Configure();

                        CookWebUtility.DBWrite db = new CookWebUtility.DBWrite(xmli.sView);
                        db.DBProcess();


                        Console.WriteLine("Ftp Unid Replace Begin!");
                        CookWebUtility.BldXml.MainSub();
                        Console.WriteLine("Process Completed!");
                        //Console.WriteLine("Press Enter Key to End");
                        //Console.ReadLine();
                        return 0;
                    }
                    else
                    {

                        // Lets pull a unid 
                        CookWebUtility.xml_instance xmli = new CookWebUtility.xml_instance();


                        NetworkCredential objCredential =
                          new NetworkCredential("mmoen", "bobbob1", "");
                        CookSingleCustomer.GetCustomersNewService cs = new CookSingleCustomer.GetCustomersNewService();

                        cs.Credentials = objCredential;
                        cs.Timeout = 600000;
                        cs.PreAuthenticate = true;
                        Console.WriteLine("Retrieving XML Begin!");
                        xmli.sXmlClean = cs.GETCUSTOMERSNEW("changeor.nsf", "XMLOutput", sUnid );

                        Console.WriteLine("Retrieving XML Completed!");
                        StreamWriter sw = new StreamWriter(@"c:\xml.xml");
                        sw.Write(xmli.sXmlClean);
                        sw.Close();
                        sw.Dispose(); 

                        xmli.LoadXDoc();
                        xmli.xml_Configure();

                        CookWebUtility.DBWrite db = new CookWebUtility.DBWrite(xmli.sView);
                        db.DBProcessSingle(sUnid);


                        Console.WriteLine("Ftp Unid Replace Begin!");
                        CookWebUtility.BldXml.MainSub(sUnid);
                        Console.WriteLine("Process Completed!");
                     //   Console.WriteLine("Press Enter Key to End");
                     //   Console.ReadLine();
                        return 0;

                    }
                }

            }


            catch (Exception ex) 
            { Console.WriteLine(ex.Message + " - BOMBED!");
            return -1;
            }
        }
    }
}
    



 
   

 