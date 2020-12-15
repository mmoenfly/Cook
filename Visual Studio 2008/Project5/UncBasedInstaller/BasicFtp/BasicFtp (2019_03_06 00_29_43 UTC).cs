using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace BasicFtp
{
    public class BasicFTPClient
    {
        public string Username;
        public string Password;
        public string Host, sUser, Spass;
        public int Port;

        public BasicFTPClient()
        {
            Username = "anonymous";
            Password = "anonymous@internet.com";
            Port = 21;
            Host = "";
        }

        public BasicFTPClient(string theUser, string thePassword, string theHost)
        {
            Username = theUser;
            Password = thePassword;
            Host = theHost;
            Port = 21;
        }

        private Uri BuildServerUri(string Path)
        {
            return new Uri(String.Format("ftp://{0}:{1}/{2}", Host, Port, Path));
        }

        /// <summary>
        /// This method downloads the given file name from the FTP server
        /// and returns a byte array containing its contents.
        /// Throws a WebException on encountering a network error.
        /// </summary>

        public byte[] DownloadData(string path)
        {
            // Get the object used to communicate with the server.
            WebClient request = new WebClient();


            // Logon to the server using username + password
            request.Credentials = new NetworkCredential(Username, Password);
            return request.DownloadData(BuildServerUri(path));
        }

        /// <summary>
        /// This method downloads the FTP file specified by "ftppath" and saves
        /// it to "destfile".
        /// Throws a WebException on encountering a network error.
        /// </summary>
        public void DownloadFile(string ftppath, string destfile)
        {
            // Download the data
            byte[] Data = DownloadData(ftppath);

            // Save the data to disk
            FileStream fs = new FileStream(destfile, FileMode.Create);
            fs.Write(Data, 0, Data.Length);
            fs.Close();
        }

        /// <summary>
        /// Upload a byte[] to the FTP server
        /// </summary>
        /// <param name="path">Path on the FTP server (upload/myfile.txt)</param>
        /// <param name="Data">A byte[] containing the data to upload</param>
        /// <returns>The server response in a byte[]</returns>

        public byte[] UploadData(string path, byte[] Data)
        {
            // Get the object used to communicate with the server.
            WebClient request = new WebClient();

            // Logon to the server using username + password
            request.Credentials = new NetworkCredential(Username, Password);
            return request.UploadData(BuildServerUri(path), Data);
        }

        /// <summary>
        /// Load a file from disk and upload it to the FTP server
        /// </summary>
        /// <param name="ftppath">Path on the FTP server (/upload/myfile.txt)</param>
        /// <param name="srcfile">File on the local harddisk to upload</param>
        /// <returns>The server response in a byte[]</returns>

        public byte[] UploadFile(string ftppath, string srcfile)
        {
            // Read the data from disk
            FileStream fs = new FileStream(srcfile, FileMode.Open);
            byte[] FileData = new byte[fs.Length];

            int numBytesToRead = (int)fs.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = fs.Read(FileData, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0) break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = FileData.Length;
            fs.Close();

            // Upload the data from the buffer
            return UploadData(ftppath, FileData);
        }

        public int MakeDir(string ftppath)
        {
            WebRequest request = WebRequest.Create(BuildServerUri(ftppath));

            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(Username, Password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                return 0;
            }
        }
        public int RemoveFile(string ftppath)
        {
            WebRequest request = (FtpWebRequest)FtpWebRequest.Create(BuildServerUri(ftppath));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            // request.PreAuthenticate = true; 
            request.Credentials = new NetworkCredential(Username, Password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                return 0;
            }
        }

        public int RemoveDir(string ftppath)
        {
            WebRequest request = (FtpWebRequest)FtpWebRequest.Create(BuildServerUri(ftppath));
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            // request.PreAuthenticate = true; 
            request.Credentials = new NetworkCredential(Username, Password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                return 0;
            }
        }

        // added 9 August 2011
        // Find all the folders in a tree

        public String[] FTPListFolders(String FtpUri,bool bRecurse)
        {
           
            List<String> files = new List<String>();
            List<String> wFolders = new List<String>();
            Queue<String> folders = new Queue<String>();
            folders.Enqueue(FtpUri);

            while (folders.Count > 0)
            {
                String fld = folders.Dequeue();

                List<String> newFiles = new List<String>();

                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream())) 
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        newFiles.Add(line.Trim());
                        line = resp.ReadLine();
                    }
                }

                ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                try
                {
                    using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                    {
                        String line = resp.ReadLine();
                        while (line != null)
                        {
                            if (line.Trim().ToLower().StartsWith("d") || line.Contains(" <DIR> "))
                            {
                                String dir = newFiles.First(x => line.EndsWith(x));
                                newFiles.Remove(dir);
                                if(bRecurse)
                                   folders.Enqueue(fld + "/" + dir);
                                wFolders.Add(fld + "/" + dir);
                                Console.WriteLine("Accessing folder {0}", fld + "/" + dir);
                            }
                            line = resp.ReadLine();
                        }
                    }
                }
                catch {       } 
           //     files.AddRange(from f in newFiles select fld + @"/" + f);
            }
        //    return files.ToArray();
            return wFolders.ToArray(); 
        }




        // End Add 9 August 2011 
        public String[] FTPListTree(String FtpUri)
        {
            
            List<String> files = new List<String>();
            Queue<String> folders = new Queue<String>();
            folders.Enqueue(FtpUri);

            while (folders.Count > 0)
            {
                String fld = folders.Dequeue();
                
                List<String> newFiles = new List<String>();

                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        newFiles.Add(line.Trim());
                        line = resp.ReadLine();
                    }
                }

                ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        if (line.Trim().ToLower().StartsWith("d") || line.Contains(" <DIR> "))
                        {
                            String dir = newFiles.First(x => line.EndsWith(x));
                            newFiles.Remove(dir);
                            folders.Enqueue(fld + "/" + dir);
                           
                        }
                        line = resp.ReadLine();
                    }
                }
                files.AddRange(from f in newFiles select fld + @"/" + f);
            }
            return files.ToArray();
        }



        public void DeleteTree(String FtpUri)
        {
           Stack<String> sParents = new Stack<string>(); 
            List<String> files = new List<String>();
            Queue<String> folders = new Queue<String>();

            folders.Enqueue(FtpUri);

            while (folders.Count > 0)
            {
                String fld = folders.Dequeue();
                List<String> newFiles = new List<String>();




                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        //ftp = (FtpWebRequest)FtpWebRequest.Create(fld + @"/" + line);
                        //ftp.Credentials = new NetworkCredential(Username, Password);
                        //ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                        //using (StreamReader resp2 = new StreamReader(ftp.GetResponse().GetResponseStream()))
                        //{

                        //}

                        newFiles.Add(line.Trim());
                        line = resp.ReadLine();

                    }
                }
                bool bFileSwitch = true; 
                ftp = (FtpWebRequest)FtpWebRequest.Create(fld);
                ftp.Credentials = new NetworkCredential(Username, Password);
                ftp.UsePassive = false;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (StreamReader resp = new StreamReader(ftp.GetResponse().GetResponseStream()))
                {
                    String line = resp.ReadLine();
                    while (line != null)
                    {
                        if (line.Trim().ToLower().StartsWith("d") || line.Contains(" <DIR> "))
                        {
                            String dir = newFiles.First(x => line.EndsWith(x));
                            newFiles.Remove(dir);
                           
                            folders.Enqueue(fld + "/" + dir);
                            sParents.Push(fld);
                            bFileSwitch = false && bFileSwitch;

                        }
                        
                        line = resp.ReadLine();
                    }
                }
                //  files.AddRange(from f in newFiles select fld + @"/" + f);
                // walk over these files
                foreach (string f1 in newFiles)
                {
                    //WebRequest request = (FtpWebRequest)FtpWebRequest.Create(BuildServerUri(f1));
                    //request.Method = WebRequestMethods.Ftp.DeleteFile;
                    //// request.PreAuthenticate = true; 
                    //request.Credentials = new NetworkCredential(Username, Password);
                    //using (var resp = (FtpWebResponse)request.GetResponse())
                    {
                        // WebRequest request = (FtpWebRequest)FtpWebRequest.Create(BuildServerUri(fld + @"/" + f1));
                        WebRequest request = (FtpWebRequest)FtpWebRequest.Create(fld + @"/" + f1);
                        request.Method = WebRequestMethods.Ftp.DeleteFile;
                        // request.PreAuthenticate = true; 
                        request.Credentials = new NetworkCredential(Username, Password);
                        using (var resp = (FtpWebResponse)request.GetResponse())
                        {

                        }
                    }

                } 
                if (bFileSwitch)
                {
                    try
                    {
                        WebRequest request1 = (FtpWebRequest)FtpWebRequest.Create(fld);
                        request1.Method = WebRequestMethods.Ftp.RemoveDirectory;
                        // request.PreAuthenticate = true; 
                        request1.Credentials = new NetworkCredential(Username, Password);
                        using (var resp = (FtpWebResponse)request1.GetResponse())
                        { }
                    }
                    catch { }
                    if(sParents.Count > 0 ) 
                       folders.Enqueue(sParents.Pop());    
                
                }
              
            }






        }


    }
}
