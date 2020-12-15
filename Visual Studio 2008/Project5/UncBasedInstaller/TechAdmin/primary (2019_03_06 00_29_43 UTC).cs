using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Configuration; 
using BasicFtp;
using System.Xml;
 

namespace TechAdmin
{
    public partial class frmMain : Form
    {
        public struct stUnid
        {
            public string sUnid;
            public string  sUserName;
            public stUnid(string val1, string val2)
            {
                sUnid = val1;
                sUserName = val2; 
            }
        }
        public frmMain()
        {
            InitializeComponent();
            chkAll0.Visible =false; 
        }

        private void pullCustomerFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void primary_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cC2DataSetUnid1.XMLParmsDtl' table. You can move, or remove it, as needed.
         
            // TODO: This line of code loads data into the 'cCIDataSet.XMLParmsDtl' table. You can move, or remove it, as needed.
            this.xMLParmsDtlTableAdapter.Fill(this.cCIDataSet.XMLParmsDtl);
            // TODO: This line of code loads data into the 'cCIDataSet.XMLParmsHdr' table. You can move, or remove it, as needed.
       
        }

        private void DwnCustFiles_Click(object sender, EventArgs e)
        {
            Custset.Select();

            this.xMLParmsDtlTableAdapter1.Fill(this.cC2DataSetUnid1.XMLParmsDtl,@"Orange County Schools");
            chkAll0.Enabled = true;
            chkAll0.Visible = true;
        }

        private void dgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                DataGridViewRow GridRow = dgCustomers.CurrentRow;

            
              //  dgUnid.Visible = true;
                string sUnid = Convert.ToString(GridRow.Cells[0].Value);
                this.xMLParmsDtlTableAdapter1.Fill(this.cC2DataSetUnid1.XMLParmsDtl, sUnid);
                bool bChk = (bool)this.dgCustomers.CurrentRow.Cells[2].EditedFormattedValue;
                if (bChk)
                {
                    dgUnid.Visible = true;
                    chkAll0.Visible = true; 
                }
                else dgUnid.Visible = false;
              //  string sUnid = e.Item.Cells[1].Text; 
            }
            catch (Exception ex)
            {
            }
        }


      private void dgCustomers_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridViewRow GridRow = this.dgCustomers.CurrentRow;

         
            
        }

      private void btnSubmit_Click(object sender, EventArgs e)
      {

          lblStatus.Visible = false;
          lblStatus.Text = "";
          if (Custset.SelectedTab == Custset.TabPages[0])
          {
              // working with page 1


              foreach (DataGridViewRow dgr in dgUnid.Rows)
              {
                  // Iterate the rows

                  bool bChk = (bool)dgr.Cells[3].EditedFormattedValue;
                  if (bChk)
                  {

                      lsUnids.Items.Add(dgr.Cells[0].EditedFormattedValue + "&" + dgr.Cells[1].EditedFormattedValue
                          + "&" + dgr.Cells[4].EditedFormattedValue
                       + "&" + dgr.Cells[5].EditedFormattedValue);

                  }
                  Custset.SelectedTab = tbpShare;
                  cmbOffice.SelectedIndex = 1;
                   
              }





               }

               if (Custset.SelectedTab == Custset.TabPages[1])
               {
                  lblStatus.Visible = true;
                  lblStatus.Text = "Download Beginning";
                  // working with page 2
                  // validate the conditions
                  if (chkShare.CheckState == CheckState.Checked)
                  {
                      string sHost, sUser, sPass, sInstallLoc;

                      string sDest, sSource;

                      sInstallLoc = txtShare.Text + @"\Install";

                      sHost = Properties.Settings.Default.Host;
                      sUser = Properties.Settings.Default.User;
                      sPass = Properties.Settings.Default.Password;
                      BasicFtp.BasicFTPClient ftp
                          = new BasicFtp.BasicFTPClient(sUser, sPass, sHost);
                      try
                      {
                          // lets write a script out in the install directory
                          if (Directory.Exists(sInstallLoc) && chkSheets.Checked)
                          {
                              Directory.Delete(sInstallLoc, true);

                              Directory.CreateDirectory(sInstallLoc);
                          }
                          if (!Directory.Exists(sInstallLoc + @"\Install"))
                              Directory.CreateDirectory(sInstallLoc + @"\Install");
                          else
                          {
                              Directory.Delete(sInstallLoc + @"\Install", true);
                              Directory.CreateDirectory(sInstallLoc + @"\Install");
                          }

                  

                          if (!Directory.Exists(sInstallLoc + @"\Xml"))
                              Directory.CreateDirectory(sInstallLoc + @"\Xml");
                          else
                          {
                              Directory.Delete(sInstallLoc + @"\Xml",true   );
                              Directory.CreateDirectory(sInstallLoc + @"\Xml");
                          }

                      }
                      catch { }
                      string sLoc = sInstallLoc + @"\xml\"; // copy to location
                      try
                      {
                          try { Directory.CreateDirectory(sInstallLoc + @"\Sheets"); }
                          catch { }




                          sSource = @"/Sheets";
                          if (!chkSheets.Checked)
                              goto Unids;
                          string[] sFileList = ftp.FTPListTree(@"ftp://" + sHost + @"/Sheets");
                          sDest = sInstallLoc + @"Sheets";
                          //    goto Unids;
                          foreach (string sFile in sFileList)
                          {
                              UriBuilder uri = new UriBuilder(sFile);
                              lblStatus.BackColor = Color.GreenYellow;
                              lblStatus.Text = "Downloading " + sFile;
                              Application.DoEvents();

                              string sPath = uri.Path;
                              sPath = sPath.Replace(@"/", @"\");
                              //Path.GetFullPath(sFile.Replace("ftp://ccisupportsite.com/",""));

                              string sF1 = Path.GetFileName(sFile);
                              sPath = Path.GetDirectoryName(sPath);
                              try
                              {
                                  Directory.CreateDirectory(sInstallLoc + @"\" + sPath);
                              }
                              catch { }

                              string sUri = Uri.EscapeUriString(sFile);
                              sUri = sUri.Replace("ftp://ccisupportsite.com", "");

                              Application.DoEvents();
                              ftp.DownloadFile(sUri, sInstallLoc + sPath + @"\" + sF1);
                              //StreamWriter sw2 = new StreamWriter(sInstallLoc + sPath + @"\" + sF1);
                              //sw2.WriteLine("0");
                              //sw2.Flush();
                              //sw2.Close();
                              //sw2.Dispose(); 
                          }


                          // we have a button push and  a check state

                         Unids:

                          char[] delimiterChars = { '&' };
                          Directory.CreateDirectory(sLoc);
                          // go get the customer unids and create a directory structure on share
                          foreach (object o in lsUnids.Items)
                          {
                              string sUnid = (string)o.ToString();
                              string[] sArr = sUnid.Split(delimiterChars);
                              sUnid = sArr[0];
                              try
                              {

                                  lblStatus.BackColor = Color.GreenYellow;
                                  lblStatus.Text = @"Downloading /" + sUnid + @"/Cust.xml";

                                  try
                                  {


                                      Directory.CreateDirectory(sLoc + @"\" + sUnid);
                                  }
                                  catch { }

                                  sSource = @"/xml/" + sUnid + @"/Cust.xml";
                                  sDest = sLoc
                                  + @"\" + sUnid + @"\cust.xml";
                                  Application.DoEvents();
                                  int iTries = 0;

                              ftpRetry:
                                  try
                                  {
                                      iTries += 1;
                                      ftp.DownloadFile(sSource, sDest);
                                  }
                                  catch (Exception x)
                                  {
                                      if (iTries < 3)
                                      {
                                          lblStatus.BackColor = Color.Yellow;
                                          lblStatus.Text = @"Retry Downloading /" + sUnid + @"/Cust.xml on try number " + Convert.ToString(iTries);
                                          goto ftpRetry;
                                      }
                                      else
                                      { throw x; }
                                  }



                              }
                              catch { }
                          }
                      }
                              
                       catch { }

                              // write out the script for installs 

                              lblStatus.Text = "Downloading Products.xml for  Install";
                              Application.DoEvents();
                              ftp.DownloadFile("/Xml/Products.xml", sInstallLoc + @"\XML\Products.xml");

                              //lblStatus.Text = "Downloading uncinstaller for  Install";
                              //Application.DoEvents();
                              //ftp.DownloadFile(@"/Xml/uncinstall.msi", sInstallLoc + @"\install\uncintstall.msi");

                              if (chkXML.Checked)
                              {
                              
                              // Use XML
                              // 15 Sept 2011
                                  XmlDocument xDoc = new XmlDocument();


                                 
                                // Create the xml document containe
                                  XmlDeclaration dec = xDoc.CreateXmlDeclaration("1.0", null, null);
                                xDoc.AppendChild(dec);// Create the root element
                                XmlElement root = xDoc.CreateElement("Clients");
                                root.SetAttribute("xml:space", "preserve"); 
                                xDoc.AppendChild(root);
                                string[] dirNames = Directory.GetDirectories(sInstallLoc + @"\XML");
                                int ix = 0;
                                
                                


                                foreach (string de in dirNames)
                                {
                                    string hUnid = lsUnids.Items[ix].ToString();
                                    string[] sArr1 = hUnid.Split('&');
                                    hUnid = sArr1[0];
                                    lblStatus.Text = "Scripting " + de + " for install";

                              

                                    XmlElement xClient = xDoc.CreateElement("Client");
                                    xClient.SetAttribute("Install", "Y");
                                    xClient.SetAttribute("host", sInstallLoc);
                                    xClient.SetAttribute("name", sArr1[1].ToString());
                                    xClient.SetAttribute("identification", sArr1[2].ToString() + " - " + sArr1[3].ToString()); 
                                    xClient.SetAttribute("target", "@Replace");
                                    xClient.SetAttribute("unid", hUnid);
                                    xClient.SetAttribute("email", txtEmail.Text);
                                    xClient.SetAttribute("office",cmbOffice.Text);
                                    xDoc.DocumentElement.AppendChild(xClient); 
                                    
                                    
                                    
                                    xClient = null; 


                                    //if (chkNet.Checked)
                                    //    sw.WriteLine("rem   net use " + txtLetter.Text + ": " + txtShare.Text + @"\" + hUnid);
                                    //sw.WriteLine("rem " + lsUnids.Items[ix].ToString());
                                    //sw.WriteLine("rem " + sArr1[2].ToString() + " - " + sArr1[3].ToString());
                                    //sw.WriteLine(txtShare.Text + @"software\uncbasedinstaller.exe  "
                                    //   + txtShare.Text + " @targetloc  " + hUnid + "   @useremail    @office");
                                    //sw.WriteLine(txtShare.Text + @"software\setattribute2009.vbs @targetloc");
                                    //if (chkNet.Checked)
                                    //    sw.WriteLine("rem   net use " + txtLetter.Text + ":  /DELETE YES");
                                    //Application.DoEvents();
                                    ix += 1;
                                }


                                xDoc.Save(sInstallLoc + @"\Install\Clients.xml"); 
                                  
                              }
                              else
                              {
                                  // Use Notepad
                                  // 15 Sept 2011
                                  // Its not XML so we check Partial
                                  string[] dirNames = Directory.GetDirectories(sInstallLoc + @"\XML");
                                  if (!Directory.Exists(sInstallLoc + @"\Install\"))
                                      Directory.CreateDirectory(sInstallLoc + @"\Install\");


                                  StreamWriter sw = new StreamWriter(sInstallLoc + @"\Install\install.cmd");
                                  int ix = 0;
                                  if (txtShare.Text.EndsWith(@"\"))
                                  { }
                                  else
                                      txtShare.Text += @"\";

                                  foreach (string de in dirNames)
                                  {
                                      if (!chkPartial.Checked)
                                      {
                                          string hUnid = lsUnids.Items[ix].ToString();
                                          string[] sArr1 = hUnid.Split('&');
                                          hUnid = sArr1[0];
                                          lblStatus.Text = "Scripting " + de + " for install";
                                          if (chkNet.Checked)
                                              sw.WriteLine("rem   net use " + txtLetter.Text + ": " + txtShare.Text + @"\" + hUnid);
                                          sw.WriteLine("rem " + lsUnids.Items[ix].ToString());
                                          sw.WriteLine("rem " + sArr1[2].ToString() + " - " + sArr1[3].ToString());
                                          sw.WriteLine(txtShare.Text + @"software\uncbasedinstaller.exe  "
                                             + hUnid + "   @useremail    @office" + txtShare.Text + " @targetloc  ");
                                          sw.WriteLine(txtShare.Text + @"software\setattribute2009.vbs @targetloc");
                                          if (chkNet.Checked)
                                              sw.WriteLine("rem   net use " + txtLetter.Text + ":  /DELETE YES");
                                          Application.DoEvents();
                                          ix += 1;
                                      }
                                      else
                                      {
                                          string hUnid = lsUnids.Items[ix].ToString();
                                          string[] sArr1 = hUnid.Split('&');
                                          hUnid = sArr1[0];
                                          lblStatus.Text = "Scripting " + de + " for install";
                                          if (chkNet.Checked)
                                              sw.WriteLine("rem   net use " + txtLetter.Text + ": " + txtShare.Text + @"\" + hUnid);
                                          sw.WriteLine("rem " + lsUnids.Items[ix].ToString());
                                          sw.WriteLine("rem " + sArr1[2].ToString() + " - " + sArr1[3].ToString());
                                          sw.WriteLine(txtShare.Text + @"partialinstaller.exe  http://ccisupportsite.com "
                                             + hUnid + "   " + txtEmail.Text  + " @targetloc  ");
                                      //  sw.WriteLine(txtShare.Text + @"software\setattribute2009.vbs @targetloc");
                                          if (chkNet.Checked)
                                              sw.WriteLine("rem   net use " + txtLetter.Text + ":  /DELETE YES");
                                          Application.DoEvents();
                                          ix += 1;
                                      }
                                  }

                                  sw.Close();
                                  sw.Dispose();
                              }


                              lblStatus.BackColor = Color.GreenYellow;
                              lblStatus.Text = "Success";
                          }
                       else
               {
                   lblStatus.BackColor = Color.Red;
                   lblStatus.Text = "Hover over the checkbox for uncpath to validate";
                   lblStatus.Visible = true;
               }    
                      }
                      //catch (Exception ex)
                      //{
                      //    lblStatus.BackColor = Color.Red;
                      //    lblStatus.Text = ex.Message;

                      //}

           
      }
               
            
      
     
     




      private void chkAll_CheckedChanged(object sender, EventArgs e)
      { 
          for(int i=0 ; i < lsUnids.Items.Count   ; i ++ )
          {
              Debug.WriteLine(lsUnids.Items[i].ToString()); 
              lsUnids.SetSelected(i, true);

          }

      }


      void chkShare_MouseHover(object sender, System.EventArgs e)
      { // implemented to use check on file share as a valid
          chkShare.CheckState=CheckState.Unchecked;
          //lblShareErr.Visible = false;
          //lblShareErr.Text = "";
          txtShare.BackColor = System.Drawing.Color.White;
          if (Directory.Exists(txtShare.Text))
          {
              txtShare.BackColor = Color.GreenYellow; 
              chkShare.CheckState = CheckState.Checked;
              chkShare.BackColor = System.Drawing.Color.Green;
              lblStatus.Visible = false;
              lblStatus.Text = "";
          }
          else
          {
              try
              {
                  Directory.CreateDirectory(txtShare.Text);
              }
              catch (Exception ex)
              {

                  chkShare.CheckState = CheckState.Unchecked;
                   lblShareErr.Visible = true;
                   lblShareErr.Text = "That share not accessible";
                  txtShare.BackColor = Color.Red; 
              }
          }
           

      }

      private void chkAll0_CheckedChanged(object sender, EventArgs e)
      {
         

              foreach (DataGridViewRow dgr in dgUnid.Rows)
              {
                  // Iterate the rows
                   if (chkAll0.Checked)
          
                  dgr.Cells[3].Value = true;
                   else

                       dgr.Cells[3].Value = false;

              }
          


      }

      private void xMLParmsDtlBindingSource1_CurrentChanged(object sender, EventArgs e)
      {

      }

      private void tbpCust_Click(object sender, EventArgs e)
      {
          lsUnids.Items.Clear();
       //   dgUnid.Rows.Clear();
          dgUnid.Visible = false; 
         
      }

      private void label3_Click(object sender, EventArgs e)
      {

      }

      private void tbpShare_Click(object sender, EventArgs e)
      {

      }

      private void chkShare_CheckedChanged(object sender, EventArgs e)
      {

      }

      private void resetFormToolStripMenuItem_Click(object sender, EventArgs e)
      {
          // Code to clear form and start all over
          Custset.SelectedTab = Custset.TabPages[0]; 
         // Clear the customer pane 
          dgUnid.Visible = false;
          this.cC2DataSetUnid1.XMLParmsDtl.Clear();
          chkAll0.Enabled = false;
          chkAll0.Visible = false;


          foreach (DataGridViewRow dgr in dgCustomers.Rows)
          {
              // Iterate the rows
              dgr.Cells[2].Value = false;


          }
 
        
      }
      
      private void dgUnid_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {

      }

      private void xMLParmsDtlBindingSource_CurrentChanged(object sender, EventArgs e)
      {

      }

      private void txtLetter_TextChanged(object sender, EventArgs e)
      {

      }

     private void txtLetter_Validating(object sender, EventArgs e)
      {

          if (chkNet.Checked)
          {
              if (txtLetter.Text.Length == 0  )
              {
                  txtLetter.Focus();
                  lblStatus.Text = "The drive letter must be entered";
                  lblStatus.BackColor = Color.Red;
              }

          }          Debug.Print("Ha"); 


      }

     private void label5_Click(object sender, EventArgs e)
     {

     }

     private void checkBox1_CheckedChanged(object sender, EventArgs e)
     {

     }
      
    

    }
}
