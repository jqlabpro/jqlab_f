using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jqLab.datamodel;


namespace YonetimPaneli.admin
{
    public partial class login : System.Web.UI.Page
    {
        JqlabEntities idc = new JqlabEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                dv_not.Visible = false;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            bool OK = false;

            var logins = (from lgn in idc.users
                          where lgn.Username == tx_kadi.Text && lgn.Password == tx_sifre.Text
                          select lgn).SingleOrDefault();
            if (logins != null)
            {
                try
                {
                    //Data.user usr = idc.users.First(u => u.Username == tx_kadi.Text && u.Password == tx_sifre.Text);
                    jqLab.datamodel.users usr = idc.users.First(us => us.Username == tx_kadi.Text);
                    usr.LoginDate = DateTime.Now;
                    idc.SaveChanges();

                    if (!chk_hatirla.Checked)
                    {
                        HttpCookie hc = new HttpCookie("user");
                        hc["user"] = tx_kadi.Text;
                        hc.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(hc);
                    }
                    else
                    {
                        HttpCookie hc = new HttpCookie("user");
                        hc["user"] = tx_kadi.Text;
                        hc.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(hc);
                    }

                    string url = Convert.ToString(Session["URL"]);
                    if (url != "" && url != null)
                        Response.Redirect(url);

                    Response.Redirect("ContentMenagement/summary.aspx");
                }
                catch
                {
                    
                }
            }
            else
            {
                dv_not.Visible = true;
            }
        }
    }
}