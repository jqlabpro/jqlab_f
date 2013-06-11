using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jqLab.datamodel;
using System.Globalization;

namespace YonetimPaneli.admin.masterpage
{
    public partial class admin : System.Web.UI.MasterPage
    {
        JqlabEntities idc = new JqlabEntities();

        public string isim, login_time, siparis_Sayi, uye_sayi;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["URL"] = Request.Url.ToString();
            checkCookie();

            dv_not.Visible = false;
            string dil = Convert.ToString(Session["DIL"]);
            if (!IsPostBack)
            {
                if (dil != "" && dil != null)
                    drp_dil.SelectedValue = dil;
            }

            bool? aktif = (from si in idc.SiteDurum
                           select si.Durum).FirstOrDefault();
            if (aktif == true)
            {
                dv_not.Visible = false;
            }
            else
            {
                dv_not.Visible = true;
            }
            

            if (!IsPostBack)
            {
                List<languages> langId = (from lang in idc.languages
                                              orderby lang.Id ascending
                                              select lang).ToList();
                int say = 0;
                foreach (var item in langId)
                {
                    drp_dil.Items.Insert(say,new ListItem(item.Name,item.Id.ToString()));
                }

            }

            //siparis_Sayi = GetSiparisCount().ToString();
            //GetLastUye();
        }

        void a_cikis_ServerClick(object sender, EventArgs e)
        {
            //Session.Abandon();

            HttpCookie hc = Request.Cookies["user"];
            hc.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(hc);
            Response.Redirect("~/admin/login.aspx");
        }

        protected void drp_dil_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<languages> langId = (from lang in idc.languages
                                          where lang.Exp == drp_dil.SelectedValue
                                          select lang).ToList();

            Session["DIL"] = langId[0].Exp;
            Response.Redirect(Request.Url.ToString());
        }

        protected int GetSiparisCount()
        {
            int siparis_sayi = 0;
            return siparis_sayi;
        }

        private void GetLastUye()
        {
            //uye_sayi = (from u in idc.Uye
            //                where u.UyeEklenme >= DateTime.Now.Date && u.UyeEklenme <= DateTime.Now.AddDays(-1)
            //                select u).Count().ToString();
            
        }

        private void checkCookie()
        {
            string gecerli = string.Empty;

            HttpCookie hc = Request.Cookies["user"];
            try
            {
                gecerli = Convert.ToString(hc["user"]);
                var kullanici = (from us in idc.users
                                 where us.Username == gecerli
                                 select us).SingleOrDefault();
                isim = kullanici.Name;
                login_time = kullanici.LoginDate.ToString();
            }
            catch
            {
                Response.Redirect("~/admin/login.aspx");
            }

            if (gecerli == "")
            {
                Response.Redirect("~/admin/login.aspx");
            }
        }
    }
}