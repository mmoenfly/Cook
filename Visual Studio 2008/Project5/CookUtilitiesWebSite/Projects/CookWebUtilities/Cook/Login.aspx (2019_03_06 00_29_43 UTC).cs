using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;
using System.Data;

public partial class Pages_Masters_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lgon_Authenticate(object sender, AuthenticateEventArgs e)
    {
        


            // Validate the user against the Membership framework user store if (Membership.ValidateUser(UserName.Text, Password.Text)) { // Log the user into the site FormsAuthentication.RedirectFromLoginPage(UserName.Text, RememberMe.Checked); } // If we reach here, the user's credentials were invalid InvalidCredentialsMessage.Visible = true; 

            string uname = lgon.UserName.Trim(); //Get the username from the control

            string password = lgon.Password.Trim(); //get the Password from the control

            if (Membership.ValidateUser(lgon.UserName, lgon.Password) )
            { // Log the user into the site 
           
                FormsAuthentication.RedirectFromLoginPage(lgon.UserName,false); }

            Lblerr.Visible = true;
            Lblerr.Text = "Fails!";

         
    }
    
            // If we reach here, the user's credentials were invalid 
           

//            bool flag = AuthenticateUser(uname, password);

//            if (flag == true)

//            {

//                e.Authenticated = true;

//                lgon.DestinationPageUrl = "/Pages/Customer.aspx";

//            }

//            else

//                e.Authenticated = false;

//        }

//        catch (Exception)

//        {

//            e.Authenticated = false;

//        }

//    }
//    private bool AuthenticateUser(string uname, string password)

//    {

//       bool bflag = false;

// string connString =  ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString; 

//string strSQL = "select * from tbl_user where user_name ='"  +   uname + "' AND user_password ='" + password + "'";

//       DataSet userDS = new DataSet();

//       SqlConnection m_conn;

//       SqlDataAdapter m_dataAdapter;

//       SqlCommand m_Command;

//       try

//       {

//           m_conn = new SqlConnection(connString);

//           m_conn.Open();

//           m_dataAdapter = new SqlDataAdapter(strSQL, m_conn);

//           m_dataAdapter.Fill(userDS);

//           m_conn.Close();

//       }

//       catch (Exception ex)

//       {

//           userDS = null;

//       }

 

//       if (userDS != null)

//       {

//           if(userDS.Tables[0].Rows.Count > 0)

//             bflag = true;

//       }

//       return bflag;

//    }
      
   
}

 
 

