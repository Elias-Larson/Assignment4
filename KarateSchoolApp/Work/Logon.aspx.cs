using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchoolApp.Work
{
    public partial class Logon : System.Web.UI.Page
    {
        // Connect to the Database
        KarateSchoolDataContext db;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elias\\Documents\\CSCI-213\\Assignment4\\Elias-Larson\\Assignment4\\KarateSchoolApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize DataContext object
            db = new KarateSchoolDataContext(conn);

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            // Variables for first and last name
            string nUserName = Login1.UserName;
            string nPassword = Login1.Password;

            // Store login info in Session
            HttpContext.Current.Session["nUserName"] = nUserName;
            HttpContext.Current.Session["uPass"] = nPassword;


            // Search for the current user, validate UserName and Password
            // using FirstOrDefault method so we can redirect to login page if no matching value is found
            var myUser = (from x in db.NetUsers
                          where x.UserName == HttpContext.Current.Session["nUserName"].ToString()
                          && x.UserPassword == HttpContext.Current.Session["uPass"].ToString()
                          select x).FirstOrDefault();


            // Add UserID and type to the Session
            if (myUser != null)
            {
                // Add UserID and User type to the Session
                HttpContext.Current.Session["UserID"] = myUser.UserID;
                HttpContext.Current.Session["userType"] = myUser.UserType;
            }

            // Redirect to Member if member type
            if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Member")
            {
                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(),
                    true);
                Response.Redirect("~/Work/Member/Member.aspx");
            }

            // else if Instructor, redirect to Instructor page
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim().Equals("Instructor"))  //HERe
            {
                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(),
                    true);
                Response.Redirect("~/Work/Instructor/Instructor.aspx");
            }

            // else if admin, redirect to admin page
            else if (myUser != null && HttpContext.Current.Session["userType"].ToString().Trim() == "Administrator")
            {
                FormsAuthentication.RedirectFromLoginPage(HttpContext.Current.Session["nUserName"].ToString(),
                    true);
                Response.Redirect("~/Work/Administrator.aspx");
            }

            // Else redirect to Logon Page again
            else
            {
                Response.Redirect("~/Work/Logon.aspx", true);
            }



        }
    }
}