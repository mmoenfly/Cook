using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicFtp;
using System.IO; 


 

namespace RemoveUnids
{
    class RemoveUnids
    {
        // Created 27 July 2011
        // Purpose
        // Clean up Godaddy unused Unids
        // Program Flow
        // Open Godaddy
        // Retrieve Current Folders
        // Call Domino and Validate
        // If folder is no longer required - Delete/Log

        
        static void Main(string[] args)
        {
            try{
                WebStore ws;
                if (args[0] == @"DELETE")
                    
                   ws = new WebStore("DELETE");
            
                else

                    ws = new WebStore("null");
            }
            catch
            (Exception ex)
            { throw ex; } 
        }
    }
}
