using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.Services;
using jqLab.datamodel;
using icebear_v2.Class;

namespace icebear_v2.EC
{
    public partial class banner : System.Web.UI.Page
    {
        static JqlabEntities idc = new JqlabEntities();

        [WebMethod]
        public static void BatchDelete(int cid)
        {
            var will_delete = (from wi in idc.Banner
                               where wi.BannerAlanID == cid
                               select wi).SingleOrDefault();
            idc.Banner.Remove(will_delete);
            idc.SaveChanges();
        }

        [WebMethod]
        public static void BatchDelete2(int cid)
        {
            var will_delete = (from wi in idc.BannerReklam
                               where wi.BannerReklamID == cid
                               select wi).SingleOrDefault();
            idc.BannerReklam.Remove(will_delete);
            idc.SaveChanges();
        }

        public string mode;
        public int? id, sid;
        public int p, herSayfadaKacItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            CloseAll();
            mode = Convert.ToString(Request.QueryString["mode"]);
            id = Convert.ToInt32(Request.QueryString["id"]);
            sid = Convert.ToInt32(Request.QueryString["sid"]);

            p = Convert.ToInt32(Request.QueryString["p"]);
            herSayfadaKacItem = 10;
            if (p == 0)
                p = 0;
            else
            {
                p--;
            }

            switch (mode)
            {
                case "add":
                    {
                        dv_banner_detay.Visible = true;
                        break;
                    }
                case "edit":
                    {
                        GetDetails();
                        dv_banner_detay.Visible = true;
                        break;
                    }
                case "delete":
                    {
                        var silinecek = (from ki in idc.Banner
                                         where ki.BannerAlanID == id
                                         select ki).FirstOrDefault();
                        idc.Banner.Remove(silinecek);
                        idc.SaveChanges();
                        Response.Redirect("banner.aspx");
                        break;
                    }
                case "sub-delete":
                    {
                        int? s_id = (from ki in idc.BannerReklam
                                     where ki.BannerReklamID == id
                                     select ki.BannerReklamBannerID).FirstOrDefault();

                        var silinecek = (from ki in idc.BannerReklam
                                         where ki.BannerReklamID == id
                                         select ki).FirstOrDefault();
                        idc.BannerReklam.Remove(silinecek);
                        idc.SaveChanges();

                        Response.Redirect("banner.aspx?mode=sub&id=" + s_id);
                        break;
                    }
                case "sub":
                    {
                        GetAllBanner();
                        dv_bannerlar.Visible = true;
                        break;
                    }
                case "sub-add":
                    {
                        MainBanners();
                        dv_banner_detay_sub.Visible = true;
                        break;
                    }
                case "sub-edit":
                    {
                        MainBanners();
                        GetDetailsSub();
                        dv_banner_detay_sub.Visible = true;
                        break;
                    }
                case null:
                    {
                        GetAllBanner();
                        dv_bannerlar.Visible = true;
                        break;
                    }
            }
        }

        private void CloseAll()
        {
            dv_banner_detay.Visible = false;
            dv_bannerlar.Visible = false;
            dv_banner_detay_sub.Visible = false;
        }

        private void MainBanners()
        {
            if (!IsPostBack)
            {
                var maink = (from mk in idc.Banner
                             select mk);
                int say = 0;
                foreach (var item in maink)
                {
                    drp_banners.Items.Insert(say, new ListItem(item.BannerAdi, item.BannerAlanID.ToString()));
                    say++;
                }
            }
            if (sid != 0)
                drp_banners.SelectedValue = sid.ToString();
        }

        private void GetAllBanner()
        {
            StringBuilder sb = new StringBuilder();

            if (mode == "sub")
            {
                var Bannerlar = (from ki in idc.BannerReklam
                                 orderby ki.BannerReklamID descending
                                 where ki.BannerReklamBannerID == id
                                 select ki).Skip(p * herSayfadaKacItem).Take(herSayfadaKacItem);
                sb.Append("<div class='widget first'> <div class='head'><h5 class='iFrames'>Banner Reklam Listesi</h5><div class='num'> <a href='?mode=sub-add&sid=" + Request.QueryString["id"].ToString() + "' class='blueNum' style='padding: 4px; margin: 5px;'><span>Banner Reklam Ekle</span></a></div></div>");
                sb.Append("<table class='tableStatic' cellpadding='0' cellspacing='0' border='0' width='100%'><thead>");
                sb.Append("<tr><td><a href='#' id='a_all2'>Tümü</a></td><td>Id</td>");
                sb.Append(" <td>Banner Alanı</td>");
                sb.Append(" <td>Değer</td>");
                sb.Append(" <td style='width:200px;'>Url</td>");
                sb.Append(" <td>Başlangıç</td>");
                sb.Append(" <td>Bitiş</td>");
                sb.Append(" <td>Aktif Mi ?</td>");
                sb.Append(" <td>İşlemler</td> </tr> </thead><tbody>");
                foreach (var item in Bannerlar)
                {
                    sb.Append("<tr><td><input id=\"ch_id\" cid='" + item.BannerReklamID + "' type=\"checkbox\" /></td><td>" + item.BannerReklamID + "</td>");
                    sb.Append("<td>" + GetMainBannerName(item.BannerReklamBannerID) + "</td>");
                    sb.Append("<td><a target='_blank' href='/files/images/banner/" + item.BannerReklamDeger + "'>" + item.BannerReklamDeger + "</a></td>");
                    sb.Append("<td>" + item.BannerReklamUrl + "</td>");
                    if (item.BannerReklamBaslangic.ToString().Split(' ').Length > 0)
                        sb.Append("<td>" + item.BannerReklamBaslangic.ToString().Split(' ')[0] + "</td>");
                    else
                        sb.Append("<td></td>");
                    if (item.BannerReklamBitis.ToString().Split(' ').Length > 0)
                        sb.Append("<td>" + item.BannerReklamBitis.ToString().Split(' ')[0] + "</td>");
                    else
                        sb.Append("<td></td>");
                    sb.Append("<td>" + AktifPasif(item.BannerReklamDurum.ToString()) + "</td>");
                    sb.Append("<td><a class='btn14 topDir mr5' original-title='Düzenle' href=\"?mode=sub-edit&id=" + item.BannerReklamID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a>");
                    sb.Append("<a class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Banner Reklamı Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=sub-delete&id=" + item.BannerReklamID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a></td></tr>");
                }
                sb.Append("</tbody></table></div>");
                lt_banner.Text = sb.ToString();
            }
            else
            {
                var Bannerlar = (from ki in idc.Banner
                                 orderby ki.BannerAlanID descending
                                 select ki).Skip(p * herSayfadaKacItem).Take(herSayfadaKacItem);
                sb.Append("<div class='widget first'> <div class='head'><h5 class='iFrames'>Banner Listesi</h5><div class='num'> <a href='?mode=add' class='blueNum' style='padding: 4px; margin: 5px;'><span>Banner Alanı Ekle</span></a></div></div>");
                sb.Append("<table class='tableStatic' cellpadding='0' cellspacing='0' border='0' width='100%'> <thead>");
                sb.Append("<tr><td><a href='#' id='a_all'>Tümü</a></td><td>Id</th> ");
                sb.Append("<td>Banner Adı</th> ");
                sb.Append("<td>Genişlik (px)</th>");
                sb.Append("<td>Yükseklik (px)</th>");
                sb.Append("<td>Max. Tıklama </th>");
                sb.Append("<td>İşlemler</th> </tr> </thead><tbody>");
                foreach (var item in Bannerlar)
                {
                    sb.Append("<tr><td><input id=\"ch_id\" cid='" + item.BannerAlanID + "' type=\"checkbox\" /></td><td>" + item.BannerAlanID + "</td>");
                    sb.Append("<td><input id='" + item.BannerAlanID + "' class='tx_cont' style='width:100px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.BannerAdi + "' /></td>");
                    sb.Append("<td>" + item.BannerW + "</td>");
                    sb.Append("<td>" + item.BannerY + "</td>");
                    sb.Append("<td>" + item.BannerMaxTik + "</td>");
                    sb.Append("<td><a class='btn14 topDir mr5' original-title='Düzenle'  href=\"?mode=edit&id=" + item.BannerAlanID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a>");
                    sb.Append("<a class='btn14 topDir mr5' original-title='Banner Reklamları' href=\"?mode=sub&id=" + item.BannerAlanID + "\"><img src=\"../Images/icons/dark/flag.png\" /></a>");
                    sb.Append("<a class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Banner Alanı ve (Varsa) Bannera Ait Alt Reklamlar Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete&id=" + item.BannerAlanID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a></td></tr>");
                }
                sb.Append("</tbody></table></div>");
                lt_banner.Text = sb.ToString();
            }
        }

        private string AktifPasif(string deger)
        {
            switch (deger)
            {
                case "True":
                    {
                        deger = "Aktif";
                        break;
                    }
                case "False":
                    {
                        deger = "Pasif";
                        break;
                    }
            }
            return deger;
        }

        private string GetMainBannerName(int? idk)
        {
            var Banner = (from ki in idc.Banner
                          where ki.BannerAlanID == idk
                          select ki.BannerAdi).SingleOrDefault();
            return Banner;
        }

        private void GetDetails()
        {
            if (!IsPostBack)
            {
                var detaylar = (from ki in idc.Banner
                                where ki.BannerAlanID == id
                                select ki).SingleOrDefault();
                tx_banneradi.Text = detaylar.BannerAdi;
                tx_max.Text = detaylar.BannerMaxTik.ToString();
                tx_genis.Text = detaylar.BannerW.ToString();
                tx_yuksek.Text = detaylar.BannerY.ToString();
            }
        }

        private void GetDetailsSub()
        {
            if (!IsPostBack)
            {
                var detaylar = (from ki in idc.BannerReklam
                                where ki.BannerReklamID == id
                                select ki).SingleOrDefault();
                tx_baslan.Text = detaylar.BannerReklamBaslangic.ToString();
                tx_bitis.Text = detaylar.BannerReklamBitis.ToString();
                tx_gosterim.Text = detaylar.BannerReklamGosterim.ToString();
                tx_max_tik.Text = detaylar.BannerReklamMaxTik.ToString();
                tx_reklam_baslik.Text = detaylar.BannerReklamBaslik;
                tx_reklam_detay.Text = detaylar.BannerReklamDetay;
                tx_url.Text = detaylar.BannerReklamUrl;
                tx_uyeno.Text = detaylar.BannerReklamUyeID.ToString();
                drp_banners.SelectedValue = detaylar.BannerReklamBannerID.ToString();
                chk_reklam_aktif.SelectedValue = detaylar.BannerReklamDurum.ToString();
                hd_Resim.Value = detaylar.BannerReklamDeger;
            }
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (fu_resim.HasFile)
            {
                try
                {
                    File.Delete(Server.MapPath("/files/images/banner/" + hd_Resim.Value));
                }
                catch (Exception)
                {

                }

                if (Path.GetExtension(fu_resim.FileName) == ".jpg" ||
                    Path.GetExtension(fu_resim.FileName) == ".jpeg" ||
                    Path.GetExtension(fu_resim.FileName) == ".png" ||
                    Path.GetExtension(fu_resim.FileName) == ".gif" ||
                    Path.GetExtension(fu_resim.FileName) == ".bmp")
                {
                    hd_Resim.Value = Tools.make_FileName(fu_resim.FileName, Path.GetExtension(fu_resim.FileName));
                    fu_resim.SaveAs(Server.MapPath("/files/images/banner/" + hd_Resim.Value));
                }
            }

            switch (mode)
            {
                case "add":
                    {
                        Banner banners = new Banner();
                        banners.BannerAdi = tx_banneradi.Text;
                        banners.BannerMaxTik = int.Parse(tx_max.Text);
                        banners.BannerW = int.Parse(tx_genis.Text);
                        banners.BannerY = int.Parse(tx_yuksek.Text);
                        idc.Banner.Add(banners);
                        idc.SaveChanges();
                        Response.Redirect("banner.aspx");
                        break;
                    }
                case "edit":
                    {
                        Banner banners = idc.Banner.First(k => k.BannerAlanID == id);
                        banners.BannerAdi = tx_banneradi.Text;
                        banners.BannerMaxTik = int.Parse(tx_max.Text);
                        banners.BannerW = int.Parse(tx_genis.Text);
                        banners.BannerY = int.Parse(tx_yuksek.Text);
                        idc.SaveChanges();
                        Response.Redirect("banner.aspx");
                        break;
                    }
                case "sub-add":
                    {
                        BannerReklam banner_reklam = new BannerReklam();
                        banner_reklam.BannerReklamBaslangic = Convert.ToDateTime(tx_baslan.Text);
                        banner_reklam.BannerReklamBaslik = tx_reklam_baslik.Text;
                        banner_reklam.BannerReklamBitis = Convert.ToDateTime(tx_bitis.Text);
                        banner_reklam.BannerReklamDeger = hd_Resim.Value;
                        if (tx_reklam_detay.Text != "")
                            banner_reklam.BannerReklamDetay = tx_reklam_detay.Text;
                        banner_reklam.BannerReklamDurum = bool.Parse(chk_reklam_aktif.SelectedValue);
                        if (tx_gosterim.Text != "")
                            banner_reklam.BannerReklamGosterim = int.Parse(tx_gosterim.Text);
                        if (tx_max_tik.Text != "")
                            banner_reklam.BannerReklamMaxTik = int.Parse(tx_max_tik.Text);
                        if (tx_tiklanma.Text != "")
                            banner_reklam.BannerReklamTiklanma = int.Parse(tx_tiklanma.Text);
                        banner_reklam.BannerReklamUrl = tx_url.Text;
                        if (tx_uyeno.Text != "")
                            banner_reklam.BannerReklamUyeID = int.Parse(tx_uyeno.Text);
                        banner_reklam.BannerReklamBannerID = int.Parse(drp_banners.SelectedValue);

                        if (tx_sira.Text != "")
                        {
                            banner_reklam.BannerReklamSiralama = int.Parse(tx_sira.Text);
                        }

                        idc.BannerReklam.Add(banner_reklam);
                        idc.SaveChanges();
                        Response.Redirect("banner.aspx?mode=sub&id=" + drp_banners.SelectedValue);
                        break;
                    }
                case "sub-edit":
                    {
                        BannerReklam banner_reklam = idc.BannerReklam.First(k => k.BannerReklamID == id);
                        banner_reklam.BannerReklamBaslangic = Convert.ToDateTime(tx_baslan.Text);
                        banner_reklam.BannerReklamBaslik = tx_reklam_baslik.Text;
                        banner_reklam.BannerReklamBitis = Convert.ToDateTime(tx_bitis.Text);
                        banner_reklam.BannerReklamDeger = hd_Resim.Value;
                        if (tx_reklam_detay.Text != "")
                            banner_reklam.BannerReklamDetay = tx_reklam_detay.Text;
                        banner_reklam.BannerReklamDurum = bool.Parse(chk_reklam_aktif.SelectedValue);
                        if (tx_gosterim.Text != "")
                            banner_reklam.BannerReklamGosterim = int.Parse(tx_gosterim.Text);
                        if (tx_max_tik.Text != "")
                            banner_reklam.BannerReklamMaxTik = int.Parse(tx_max_tik.Text);
                        if (tx_tiklanma.Text != "")
                            banner_reklam.BannerReklamTiklanma = int.Parse(tx_tiklanma.Text);
                        banner_reklam.BannerReklamUrl = tx_url.Text;
                        if (tx_uyeno.Text != "")
                            banner_reklam.BannerReklamUyeID = int.Parse(tx_uyeno.Text);
                        banner_reklam.BannerReklamBannerID = int.Parse(drp_banners.SelectedValue);
                        if (tx_sira.Text != "")
                        {
                            banner_reklam.BannerReklamSiralama = int.Parse(tx_sira.Text);
                        }
                        idc.SaveChanges();
                        Response.Redirect("banner.aspx?mode=sub&id=" + drp_banners.SelectedValue);
                        break;
                    }
            }
        }
    }
}