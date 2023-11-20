using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchoolApp.Work.Instructor
{
    public partial class Instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validate current UserType for Instructor page, redirct to login page if userType is Member
            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() == "Member")
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Work/Logon.aspx", true);
                }
            }

            // Convert the current Instructors ID to an Int
            int ID = Convert.ToInt32(HttpContext.Current.Session["userID"]);

            // Connect to Database
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elias\\Documents\\CSCI-213\\Assignment4\\Elias-Larson\\Assignment4\\KarateSchoolApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
            var db = new KarateSchoolDataContext(conn);


            // Get the name to be displayed in the Instructor Label
            string fname, lname;
            try
            {
                // Get the current instructors name from the Instructor Table
                fname = (from x in db.Instructors
                         where x.InstructorID == ID
                         select x.InstructorFirstName).Single();
                lname = (from x in db.Instructors
                         where x.InstructorID == ID
                         select x.InstructorLastName).Single();

                // Display the logged in Members name in the lblMembers
                lblInstructor.Text = fname + " " + lname;
            }
            // Redirect to login page if exception is thrown
            catch (Exception ex)
            {
                Response.Redirect("~/Work/Logon.aspx");
            }


            /*
             * Get Data for the instructorGridView
             * data includes section name, member first name, and member last name
             */
            var result = from x in db.Instructors
                         join s in db.Sections
                         on x.InstructorID equals s.Instructor_ID
                         where x.InstructorID == ID
                         join m in db.Members
                         on s.Member_ID equals m.Member_UserID
                         select new
                         {
                             s.SectionName,
                             m.MemberFirstName,
                             m.MemberLastName
                         };
            // put the result dataset into the instructorGridView
            instructorGridView.DataSource = result;
            instructorGridView.DataBind();
        }
    }
}