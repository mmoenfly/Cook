using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicFtp;
using System.IO; 


 

namespace LoadServers
{
    class DownloadFiles
    {
        // Created 12n Feb 2012
        // Purpose
        // Check Godaddy For Downloads
        // Program Flow
        // Open Godaddy
        // Retrieve Current Folders
        // Open XML
        // Download Sheets

        
        static void Main(string[] args)
        {
            try{
                WebStore ws;
               
                    
              

                    ws = new WebStore("null");
            }
            catch
            (Exception ex)
            { throw ex; } 
        }
    }
}
