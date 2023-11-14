using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateSchoolApp.Work
{
    public partial class Administrator : System.Web.UI.Page
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\elias\\Documents\\CSCI-213\\Assignment4\\KarateSchoolApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext dataContext;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}