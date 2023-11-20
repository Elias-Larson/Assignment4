using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchoolApp.Work
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validate current UserType for Member page, redirect to Login page if Instructor
            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() == "Instructor")
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Work/Logon.aspx", true);
                }
            }


            // Convert the current Members ID to an Int
            int ID = Convert.ToInt32(HttpContext.Current.Session["userID"]);

            // Connect to Database
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elias\\Documents\\CSCI-213\\Assignment4\\Elias-Larson\\Assignment4\\KarateSchoolApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
            var db = new KarateSchoolDataContext(conn);


            // Get the Members name for the member label using their userID
            string fname, lname;
            try
            {
                // Get the current members name from the Member Table
                fname = (from x in db.Members
                         where x.Member_UserID == ID
                         select x.MemberFirstName).Single();
                lname = (from x in db.Members
                         where x.Member_UserID == ID
                         select x.MemberLastName).Single();

                // Display the logged in Members name in the lblMembers
                lblMember.Text = fname + " " + lname;
            }
            // catch exception, redirect to Login page if one is thrown
            catch (Exception ex)
            {
                Response.Redirect("~/Work/Logon.aspx");
            }


            /*
             * Get Data for the memberGridView
             * Data includes section name, section fee, instructor first and last name,
             * and the date member joined
             */
            var result = from member in db.Members
                         join s in db.Sections
                         on member.Member_UserID equals s.Member_ID
                         where member.Member_UserID == ID
                         join i in db.Instructors
                         on s.Instructor_ID equals i.InstructorID
                         select new
                         {
                             s.SectionName, s.SectionFee, i.InstructorFirstName,
                             i.InstructorLastName, member.MemberDateJoined
                         };

            // Display results in memberGridView
            memberGridView.DataSource = result;
            memberGridView.DataBind();
        }
    }
}