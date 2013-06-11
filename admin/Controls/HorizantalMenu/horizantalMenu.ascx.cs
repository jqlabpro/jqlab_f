using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace icebear_v2.Controls.HorizantalMenu
{
    public partial class horizantalMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            a_logout.ServerClick += a_logout_ServerClick;
        }

        void a_logout_ServerClick(object sender, EventArgs e)
        {
            HttpCookie hc = Request.Cookies["user"];
            hc.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(hc);
            Response.Redirect("~/Admin/login.aspx");
        }
    }
}