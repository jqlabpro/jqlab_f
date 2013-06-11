using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using jqLab.datamodel;
using icebear_v2.Class;
using System.Web.Services;

namespace YonetimPaneli.ContentMenagement
{
    public partial class users : System.Web.UI.Page
    {
        static JqlabEntities idc = new JqlabEntities();
        [WebMethod]
        public static string ChangeName(int ids, string deger)
        {
            jqLab.datamodel.users user_t = idc.users.First(co => co.Id == ids);
            user_t.Name = deger;
            idc.SaveChanges();

            return deger;
        }
        [WebMethod]
        public static string ChangeMail(int ids, string deger)
        {
            jqLab.datamodel.users user_t = idc.users.First(co => co.Id == ids);
            user_t.EMail = deger;
            idc.SaveChanges();

            return deger;
        }
        [WebMethod]
        public static string ChangeUserName(int ids, string deger)
        {
            jqLab.datamodel.users user_t = idc.users.First(co => co.Id == ids);
            user_t.Username = deger;
            idc.SaveChanges();

            return deger;
        }
        [WebMethod]
        public static void BatchDelete(int cid)
        {
            var will_delete = (from wi in idc.contents
                               where wi.Id == cid
                               select wi).SingleOrDefault();
            idc.contents.Remove(will_delete);
            idc.SaveChanges();
        }

        public string mode, id;
        protected void Page_Load(object sender, EventArgs e)
        {
            CloseAll();
            mode = Convert.ToString(Request.QueryString["mode"]);
            id = Convert.ToString(Request.QueryString["id"]);

            switch (mode)
            {
                case "add":
                    {
                        dv_kullanici_duzenle.Visible = true;
                        break;
                    }
                case "edit":
                    {
                        GetDetails();
                        spn_uyari.Visible = true;
                        dv_kullanici_duzenle.Visible = true;
                        break;
                    }
                case "delete":
                    {
                        dv_kullanicilar.Visible = true;
                        var silinecek = (from ctg in idc.users
                                         where ctg.Id == int.Parse(id)
                                         select ctg).Single();
                        idc.users.Remove(silinecek);
                        idc.SaveChanges();

                        Response.Redirect("users.aspx");
                        break;
                    }
                case null:
                    {
                        AllUsers();
                        dv_kullanicilar.Visible = true;
                        break;
                    }
            }
        }

        private void CloseAll()
        {
            dv_kullanici_duzenle.Visible = false;
            dv_kullanicilar.Visible = false;
            spn_uyari.Visible = false;
        }

        private void AllUsers()
        {
            List<jqLab.datamodel.users> lst_users = (from co in idc.users
                                         select co).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in lst_users)
            {
                sb.Append("<tr><td><input id=\"ch_id\" cid='" + item.Id + "' type=\"checkbox\" /></td><td>" + item.Id + "</td>");
                sb.Append("<td><input id='n" + item.Id + "' class='tx_u_name' style='width:100px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.Name + "' /></td>");
                sb.Append("<td><input id='m" + item.Id + "' class='tx_u_mail' style='width:100px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.EMail + "' /></td>");
                sb.Append("<td><input id='u" + item.Id + "' class='tx_u_uname' style='width:100px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.Username + "' /></td>");
                sb.Append("<td><a class='btn14 topDir mr5' title='Düzenle' href=\"?mode=edit&id=" + item.Id + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a>");
                sb.Append("<a class='btn14 topDir mr5' rel='delete' title='Silme Onayı' mesaj='Bu Kullanıcı Kalıcı Olarak Silinecek<br>Devam Edilsin Mi ?' href=\"?mode=delete&id=" + item.Id + "\"><img src=\"../Images/icons/dark/trash.png\" /></a></td></tr>");
            }
            lt_users.Text = sb.ToString();
        }

        private void GetDetails()
        {
            if (!IsPostBack)
            {
                List<jqLab.datamodel.users> lst_user = (from co in idc.users
                                            where co.Id == int.Parse(id)
                                            select co).ToList();
                tx_isim.Text = lst_user[0].Name;
                tx_kadi.Text = lst_user[0].Username;
                tx_eposta.Text = lst_user[0].EMail;
                tx_pass.Text = lst_user[0].Password;
                hd_sifre.Value = lst_user[0].Password;
            }
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                case "add":
                    {
                        jqLab.datamodel.users us = new jqLab.datamodel.users();
                        us.Name = tx_isim.Text;
                        us.EMail = tx_eposta.Text;
                        us.Username = tx_kadi.Text;
                        us.Password = tx_pass.Text;

                        idc.users.Add(us);
                        idc.SaveChanges();

                        Response.Redirect("users.aspx");
                        break;
                    }
                case "edit":
                    {
                        jqLab.datamodel.users us = idc.users.First(k => k.Id == Convert.ToInt32(id));
                        us.Name = tx_isim.Text;
                        us.EMail = tx_eposta.Text;
                        us.Username = tx_kadi.Text;

                        if (tx_pass.Text != "")
                            us.Password = tx_pass.Text;
                        else
                            us.Password = hd_sifre.Value;

                        idc.SaveChanges();

                        Response.Redirect("users.aspx");
                        break;
                    }
            }
        }
    }
}