using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Management;
using System.Net; 

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PcId pc = new PcId("Test123456");
               
                pc.LogEvent();
                return;

                NetworkCredential nc =
                    new NetworkCredential("mmoen", "bobbob1");

                Uri requestUri = null;
                CredentialCache myCache = new CredentialCache();

                
myCache.Add(new Uri("http://www.cookconsulting.net/names.nsf"),"Basic",nc);


               

              

                Uri.TryCreate("http://www.cookconsulting.net/names.nsf",
    UriKind.RelativeOrAbsolute, out requestUri);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create
                    (requestUri);
                request.Credentials = myCache;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
               // request.Method = WebRequestMethods.Http.Get;
             

                CookCustomers.GetCustomersService cc = new CookCustomers.GetCustomersService();
               
                cc.PreAuthenticate = true; 
               
                NetworkCredential objCredential =
new NetworkCredential("MMoen", "bobbob1");
               
                cc.Credentials = objCredential;
                
            //    cc.UseDefaultCredentials = true; 
                string sRes = cc.GETCUSTOMERS("changeor.nsf", "XMLOutput"); 

                ManagementObjectSearcher searcher =
                 new ManagementObjectSearcher("SELECT Product, SerialNumber FROM Win32_BaseBoard");

                ManagementObjectCollection information = searcher.Get();
                foreach (ManagementObject obj in information)
                {
                    // Retrieving the properties (columns)    
                    // Writing column name then its value    
                    foreach (PropertyData data in obj.Properties)
                        Console.WriteLine("{0} = {1}", data.Name, data.Value);
                    Console.WriteLine();
                }
                // For typical use of disposable objects enclose it in a using statement instead.
                searcher.Dispose();

                //if (args[0] != null)
                //    Console.WriteLine(args[0]);
                //else Console.WriteLine("Nothing Showed!");
              
                TestHarness.com.ccisupportsite.WS_Logger log = new TestHarness.com.ccisupportsite.WS_Logger();
                sRes = log.FindNextId("ABASCSDAFD");
                log.Logger("ABASCSDAFD", "This is my message!", 0, Convert.ToInt16(sRes));
                Console.WriteLine(sRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ouch - " + ex.Message);
            }
                Console.ReadLine(); 
        }
    }
}
