using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchoolApp.Work
{
    public partial class Administrator : System.Web.UI.Page
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elias\\Documents\\CSCI-213\\Assignment4\\KarateSchoolApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dbcon;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateSchoolDataContext(connString);
            
            //Retrieve members and display
            var resultMembers = from item in dbcon.Members
                                select new { item.Member_UserID, item.MemberFirstName, item.MemberLastName, 
                                            item.MemberPhoneNumber, item.MemberDateJoined };
            //Show result
            MemberGridView.DataSource = resultMembers;
            MemberGridView.DataBind();

            //Retrieve instructors and display
            var resultInstructors = from item2 in dbcon.Instructors
                                    select new { item2.InstructorID, item2.InstructorFirstName, item2.InstructorLastName };
            //Show result
            InstructorGridView.DataSource = resultInstructors;
            InstructorGridView.DataBind();
            
            

        }

        //Add new user
        private int userId;
        private string userType;
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPassword = txtUserPassword.Text.Trim();
            userType = DropDownListType.SelectedItem.Text.Trim();
            //Edit input
            if (DropDownListType.SelectedValue.Equals("Member"))
            {
                txtEmail.Text = "";
                txtDateJoined.Text = "";
            }
            else if (DropDownListType.SelectedValue.Equals("Instructor"))
            {
                txtEmail.Text = "N/A";
                txtDateJoined.Text = "N/A";
            }

            try
            {
                using(SqlConnection conn = new SqlConnection(connString))
                {
                    userType = DropDownListType.SelectedValue;
                    string insertQuery = "INSERT INTO NetUser(UserName, UserPassword, UserType)" +
                                            " VALUES('" + userName + "', '" + userPassword + "', '"
                                            + userType + "')";

                    try
                    {
                        conn.Open();
                        SqlCommand sqlcom = new SqlCommand(insertQuery, conn);
                        sqlcom.ExecuteNonQuery();
                        

                    }
                    catch(SqlException ex)
                    {
                        Label1.Text = ex.Message;
                    }
                    conn.Close();

                }
            }
            catch(Exception ex)
            {

            }

            dbcon = new KarateSchoolDataContext(connString);

            var resultUserId = (from item in dbcon.NetUsers
                               where item.UserName == userName
                               select item.UserID).Single();
            userId = Convert.ToInt32(resultUserId);
            lblID.Text = userId.ToString();
            lblType.Text = userType;

            InstructorGridView.DataBind();
            MemberGridView.DataBind();

        }

        //Add info to Member/Instructor
        protected void btnAddInfo_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            DateTime dateTime = new DateTime();
            string dateJoined = txtDateJoined.Text;
            if (DropDownListType.SelectedValue.Equals("Member"))
            {
                dateTime = DateTime.Parse(dateJoined);
            }
            
            
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            int id = Convert.ToInt32(lblID.Text);
            userType = lblType.Text;
            if(userType == "Member")
            {
                
                
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        string insertQuery = "INSERT INTO Member(Member_UserID, MemberFirstName, MemberLastName, MemberDateJoined, MemberPhoneNumber, MemberEmail)" +
                                             " VALUES('" + id + "', '" + firstName + "', '" + lastName + "', '" +
                                             dateTime + "', '" + phone + "', '" + email + "')";
                        try
                        {
                            conn.Open();
                            SqlCommand sqlcom = new SqlCommand(insertQuery, conn);
                            sqlcom.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Label1.Text = ex.Message;
                        }
                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    Label1.Text = ex.Message;
                }
            }
            if(userType == "Instructor")
            {
                txtDateJoined.Text = "N/A";
                txtEmail.Text = "N/A";
                try
                {
                    using(SqlConnection conn = new SqlConnection(connString))
                    {
                        string insertQuery = "INSERT INTO Instructor(InstructorID, InstructorFirstName, InstructorLastName, InstructorPhoneNumber)" +
                                            " VALUES('" + id + "', '" + firstName + "', '" + lastName + "', '" + phone + "')";

                        try
                        {
                            conn.Open();
                            SqlCommand sqlcom = new SqlCommand(insertQuery, conn);
                            sqlcom.ExecuteNonQuery();
                        }
                        catch(SqlException ex)
                        {
                            Label1.Text = ex.Message;
                        }
                        conn.Close();
                    }
                }
                catch(SqlException ex)
                {
                    Label1.Text = ex.Message;
                }
            }
            
            Refresh();
        }

        protected void btnDeleteID_Click(object sender, EventArgs e)
        {
           
            int delete = Convert.ToInt32(txtDeleteId.Text);
            

            string deleteType = "";

            dbcon = new KarateSchoolDataContext(connString);

            //Retrieve members and display
            var resultMembers = (from item in dbcon.NetUsers
                                 where item.UserID == delete
                                 select item.UserType).Single();
            deleteType = resultMembers.ToString();
            
            string temp = "";
            if (deleteType.Equals("Instructor"))
            {
                temp = "InstructorID";
            }
            else if (deleteType.Equals("Member"))
            {
                temp = "Member_UserID";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    string deleteQuery = "DELETE from " + deleteType + " WHERE " + temp + "='" + delete + "'";
                    string deleteQuery2 = "DELETE from NetUser WHERE UserID='" + delete + "'";
                    try
                    {
                        conn.Open();
                        SqlCommand sqlcom = new SqlCommand(deleteQuery, conn);
                        sqlcom.ExecuteNonQuery();
                        
                        SqlCommand sqlcom2 = new SqlCommand(deleteQuery2, conn);
                        sqlcom2.ExecuteNonQuery();
                        

                    }
                    catch (SqlException ex)
                    {
                        Label1.Text = ex.Message;
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Label1.Text = ex.Message;
            }

            
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            int memberId = Convert.ToInt32(txtAssign.Text);
            int sectionId = Convert.ToInt32(txtSection.Text);
            
            //Update
            try
            {
                using(SqlConnection conn = new SqlConnection(connString))
                {
                    string updateQuery = "UPDATE Section SET Member_ID = '" + memberId + "' WHERE SectionID = '" + sectionId + "'";
                    try
                    {
                        conn.Open();
                        SqlCommand sqlcom = new SqlCommand(updateQuery, conn);
                        sqlcom.ExecuteNonQuery();

                    }
                    catch(SqlException ex)
                    {
                        Label1.Text = ex.Message;
                    }
                    conn.Close();
                }
            }
            catch(SqlException ex)
            {
                Label1.Text = ex.Message;
            }
        }

        private void Refresh()
        {
            txtUserName.Text = "";
            txtUserPassword.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtDateJoined.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtDeleteId.Text = "";
            txtAssign.Text = "";
            txtSection.Text = "";
            lblID.Text = "";
            lblType.Text = "";
            InstructorGridView.DataBind();
            MemberGridView.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();

        }
    }
}