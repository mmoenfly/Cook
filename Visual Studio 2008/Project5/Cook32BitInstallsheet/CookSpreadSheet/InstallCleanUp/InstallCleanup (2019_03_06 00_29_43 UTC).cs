using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstallCleanUp
{
    class Clean
    {
        private static wslogger.WS_Logger ws = new wslogger.WS_Logger();
        private static string sUnid;
        private static int iBatchId; 
        static void Main(string[] args)
        {
            // created by michael moen
            // 12 May 2010 
            // Purpose
            // Cleanup the shortcuts and report back to home

            // if i dont have a unid dont do anything 
            if (args != null)
                sUnid = args[0];
            else
                return;

            LoadBatch();


        }

        private static void LoadBatch()
        {
            iBatchId = int.Parse(ws.FindNextId(sUnid));
            if (iBatchId == 1)
            { }
            else
                iBatchId -= iBatchId;
        }

    }
}
