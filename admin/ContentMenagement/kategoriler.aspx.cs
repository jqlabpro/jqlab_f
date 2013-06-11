using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using jqLab.datamodel;
using icebear_v2.Class;
using System.Text;
using System.Web.Services;

namespace YonetimPaneli.ContentMenagement
{
    public partial class kategoriler : System.Web.UI.Page
    {
        static JqlabEntities idc = new JqlabEntities();

        [WebMethod]
        public static string ChangeName(int ids, string deger)
        {
            Kategoriler catego = idc.Kategoriler.First(ca => ca.KategoriID == ids);
            catego.KategoriAdi = deger;
            catego.KategoriAdi_Kucuk = Tools.makelink(deger);
            idc.SaveChanges();

            return deger;
        }

        [WebMethod]
        public static void BatchDelete(int cid)
        {
            var will_delete = (from wi in idc.Kategoriler
                               where wi.KategoriID == cid
                               select wi).SingleOrDefault();
            idc.Kategoriler.Remove(will_delete);
            idc.SaveChanges();
        }
        [WebMethod]
        public static void BatchDelete2(int cid)
        {
            var will_delete = (from wi in idc.KategoriOzellikleri
                               where wi.KatagoriOzellikID == cid
                               select wi).SingleOrDefault();
            idc.KategoriOzellikleri.Remove(will_delete);
            idc.SaveChanges();
        }

        [WebMethod]
        public static void BatchDelete3(int cid)
        {
            var will_delete = (from wi in idc.KategoriOzellikSecenekleri
                               where wi.KatagoriOzellikSecenekleriID == cid
                               select wi).SingleOrDefault();
            idc.KategoriOzellikSecenekleri.Remove(will_delete);
            idc.SaveChanges();
        }

        public string mode, id;
        public int? cid, langId, catId;
        public string yol, sayfa;

        protected void Page_Load(object sender, EventArgs e)
        {
            CloseAll();

            langId = GetLangId();
            mode = Convert.ToString(Request.QueryString["mode"]);
            id = Convert.ToString(Request.QueryString["id"]);
            cid = Convert.ToInt32(Request.QueryString["cid"]);
            sayfa = "<li>Kategoriler</li>";

            switch (mode)
            {
                case "add":
                    {
                        FillCategories();
                        dv_kategori_edit.Visible = true;
                        break;
                    }
                case "edit":
                    {
                        FillCategories();
                        GetDetails();
                        dv_kategori_edit.Visible = true;
                        break;
                    }
                case "sub":
                    {
                        AllCategories();
                        dv_kategoriler.Visible = true;
                        FillSearchCategories();
                        MakeRoad();
                        break;
                    }
                case "delete":
                    {
                        int tid = int.Parse(id);
                        dv_kategoriler.Visible = true;
                        var silinecek = (from ctg in idc.Kategoriler
                                         where ctg.KategoriID == tid
                                         select ctg).Single();
                        idc.Kategoriler.Remove(silinecek);
                        idc.SaveChanges();
                        DeleteAllSubCategories(int.Parse(id));

                        Response.Redirect("kategoriler.aspx");
                        break;
                    }
                case "setting":
                    {
                        AllCategories();
                        dv_kategoriler.Visible = true;
                        MakeRoad();
                        break;
                    }
                case "delete-setting":
                    {
                        int tid = int.Parse(id);
                        dv_kategoriler.Visible = true;
                        var silinecek = (from ctg in idc.KategoriOzellikleri
                                         where ctg.KatagoriOzellikID == tid
                                         select ctg).Single();
                        idc.KategoriOzellikleri.Remove(silinecek);
                        idc.SaveChanges();
                        DeleteAllSubCategories(int.Parse(id));

                        Response.Redirect("kategoriler.aspx");
                        break;
                    }
                case "edit-setting":
                    {
                        FillCategories();
                        GetDetails();
                        dv_kategori_ozellik_edit.Visible = true;
                        break;
                    }
                case "add-setting":
                    {
                        FillCategories();
                        dv_kategori_ozellik_edit.Visible = true;
                        drp_kategori.SelectedValue = Request.QueryString["cid"].ToString();
                        break;
                    }
                case "excel-":
                    {
                        dv_kategoriler.Visible = true;
                        AllCategories();
                        Save2Excel();
                        break;
                    }
                case "excel-setting":
                    {
                        dv_kategoriler.Visible = true;
                        AllCategories();
                        Save2Excel();
                        break;
                    }
                case "setting-ayar":
                    {
                        dv_kategoriler.Visible = true;
                        AllCategories();
                        MakeRoad();
                        break;
                    }
                case "edit-setting-ayar":
                    {
                        AllSettings();
                        GetDetails();
                        dv_kategori_ozellik_secenekleri_detay.Visible = true;
                        break;
                    }
                case "add-setting-ayar":
                    {
                        AllSettings();
                        dv_kategori_ozellik_secenekleri_detay.Visible = true;
                        break;
                    }
                case "delete-setting-ayar":
                    {
                        int tid = int.Parse(id);
                        dv_kategoriler.Visible = true;
                        var silinecek = (from ctg in idc.KategoriOzellikSecenekleri
                                         where ctg.KatagoriOzellikSecenekleriID == tid
                                         select ctg).Single();
                        idc.KategoriOzellikSecenekleri.Remove(silinecek);
                        idc.SaveChanges();
                        DeleteAllSubCategories(int.Parse(id));

                        Response.Redirect("kategoriler.aspx?mode=setting-ayar");
                        break;
                    }
                case "up":
                    {
                        try
                        {
                            Move("up");
                        }
                        catch (Exception)
                        {

                        }

                        Response.Redirect("kategoriler.aspx");
                        break;
                    }
                case "down":
                    {
                        try
                        {
                            Move("down");
                        }
                        catch (Exception)
                        {

                        }
                        Response.Redirect("kategoriler.aspx");
                        break;
                    }

                case null:
                    {
                        FillSearchCategories();
                        AllCategories();
                        dv_kategoriler.Visible = true;
                        break;
                    }
            }
        }

        private void MakeRoad()
        {
            switch (mode)
            {
                case "sub":
                    {
                        sayfa = "<li>Kategoriler</li>";
                        MakeSubRoad(cid);
                        break;
                    }
                case "setting":
                    {
                        dv_arama.Visible = false;
                        string kategori_ismi = (from ka in idc.Kategoriler where ka.KategoriID == cid select ka.KategoriAdi).SingleOrDefault();
                        sayfa = "<li>" + kategori_ismi + "</li><li>Kategori Özellikleri</li>";
                        break;
                    }
                case "setting-ayar":
                    {
                        dv_arama.Visible = false;
                        string kategori_ozellik_ismi = (from ka in idc.KategoriOzellikleri where ka.KatagoriOzellikID == cid select ka.KatagoriOzellikAdi).FirstOrDefault();
                        int? kategori_id = (from ka in idc.KategoriOzellikleri where ka.KatagoriOzellikID == cid select ka.KatagoriOzellikKategoriID).SingleOrDefault();
                        string kategori_ismi = (from ka in idc.Kategoriler where ka.KategoriID == kategori_id select ka.KategoriAdi).SingleOrDefault();
                        sayfa += "<li>" + kategori_ismi + "</li>";
                        sayfa += "<li>" + kategori_ozellik_ismi + "</li>";
                        sayfa += "<li>Kategori Özellik Seçenekleri</li>";
                        break;
                    }
            }
        }

        private void MakeSubRoad(int? id)
        {
            var temp = (from yo in idc.Kategoriler
                        where yo.KategoriID == id
                        select yo).ToArray();
            if (temp.Length > 0)
            {
                yol = "<li>" + temp[0].KategoriAdi + "</li>" + yol;
                MakeSubRoad(temp[0].KategoriUstKat);
            }
        }

        private void Save2Excel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=kategoriler.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            this.EnableViewState = false;
            dv_table.InnerHtml = lt_category.Text;
            Response.Write(dv_table.InnerHtml);
            Response.End();
        }

        private void Move(string where)
        {
            int tid = int.Parse(id);
            switch (where)
            {
                case "down":
                    {
                        int? sıra = (from q in idc.Kategoriler
                                     where q.KategoriID == tid
                                     select q.KategoriSira).SingleOrDefault();

                        int? ownerId = (from q in idc.Kategoriler
                                        where q.KategoriID == tid
                                        select q.OwnerID).SingleOrDefault();

                        int? yeni_sıra = (from q in idc.Kategoriler
                                          where q.KategoriSira > sıra && q.OwnerID == ownerId
                                          select q.KategoriSira).Min();

                        int degisecek_id = (from q in idc.Kategoriler
                                            where q.KategoriSira == yeni_sıra && q.OwnerID == ownerId
                                            select q.KategoriID).FirstOrDefault();



                        Kategoriler dcat = idc.Kategoriler.Where(c => c.KategoriID == degisecek_id).SingleOrDefault();
                        dcat.KategoriSira = sıra;
                        idc.SaveChanges();

                        tid = int.Parse(id);
                        Kategoriler cat = idc.Kategoriler.Where(c => c.KategoriID == tid).SingleOrDefault();
                        cat.KategoriSira = yeni_sıra;
                        idc.SaveChanges();

                        break;
                    }
                case "up":
                    {
                        tid = int.Parse(id);
                        int? sıra = (from q in idc.Kategoriler
                                     where q.KategoriID == tid
                                     select q.KategoriSira).SingleOrDefault();

                        int? ownerId = (from q in idc.Kategoriler
                                        where q.KategoriID == tid
                                        select q.OwnerID).SingleOrDefault();

                        int? yeni_sıra = (from q in idc.Kategoriler
                                          where q.KategoriSira < sıra && q.OwnerID == ownerId
                                          select q.KategoriSira).Max();

                        int degisecek_id = (from q in idc.Kategoriler
                                            where q.KategoriSira == yeni_sıra && q.OwnerID == ownerId
                                            select q.KategoriID).FirstOrDefault();

                        Kategoriler dcat = idc.Kategoriler.Where(c => c.KategoriID == degisecek_id).SingleOrDefault();
                        dcat.KategoriSira = sıra;
                        idc.SaveChanges();

                        Kategoriler cat = idc.Kategoriler.Where(c => c.KategoriID == tid).SingleOrDefault();
                        cat.KategoriSira = yeni_sıra;
                        idc.SaveChanges();
                        break;
                    }
            }
        }

        private void GetDetails()
        {
            int tid = int.Parse(id);
            if (!IsPostBack)
            {
                if (mode == "edit")
                {
                    var detaylar = (from ca in idc.Kategoriler
                                    where ca.KategoriID == tid
                                    select ca).ToList();
                    tx_adi.Text = detaylar[0].KategoriAdi;
                    drp_ustkategori.SelectedValue = detaylar[0].OwnerID.ToString();

                    bool? yayin = detaylar[0].KategoriDurum;
                    if (yayin == false)
                        chk_yayin.Checked = true;
                    else
                    {
                        chk_yayin.Checked = false;
                    }

                    rd_ozel_filtre.SelectedValue = detaylar[0].OzelFiltre.ToString();
                    hd_icon.Value = detaylar[0].KategoriIcon;
                    hd_resim.Value = detaylar[0].KategoriResim;
                    tx_sira.Text = detaylar[0].KategoriSira.ToString();
                    hd_sira.Value = detaylar[0].KategoriSira.ToString();
                }
                else if (mode == "edit-setting")
                {
                    tid = int.Parse(id);
                    var detaylar = (from ca in idc.KategoriOzellikleri
                                    where ca.KatagoriOzellikID == tid
                                    select ca).ToList();
                    tx_ozellik_adi.Text = detaylar[0].KatagoriOzellikAdi;
                    tx_aciklama.Text = detaylar[0].KatagoriOzellikAciklama;
                    drp_kategori.SelectedValue = detaylar[0].KatagoriOzellikKategoriID.ToString();

                    rd_ozellik_aktif.SelectedValue = detaylar[0].KatagoriOzellikDurum.ToString();
                    chk_drop.Checked = bool.Parse(detaylar[0].KategoriOzellikleriSuzmedeKullan.ToString());
                    chk_yesno.Checked = bool.Parse(detaylar[0].KategoriOzellikleriTekSecim.ToString());
                    chk_text.Checked = bool.Parse(detaylar[0].KategoriOzellikleriIsTextBox.ToString());
                }
                else if (mode == "edit-setting-ayar")
                {
                    tid = int.Parse(id);
                    var detaylar = (from ca in idc.KategoriOzellikSecenekleri
                                    where ca.KatagoriOzellikSecenekleriID == tid
                                    select ca).ToList();
                    tx_deger.Text = detaylar[0].KatagoriOzellikSecenekleriDeger;
                    drp_settingayar.SelectedValue = detaylar[0].KatagoriOzellikSecenekleriKatagoriOzellikID.ToString();
                }
            }
        }

        private void DeleteAllSubCategories(int id)
        {
            try
            {
                var silinecekler = (from si in idc.Kategoriler
                                    where si.KategoriID == id
                                    select si).FirstOrDefault();
                int idd = silinecekler.KategoriID;
                idc.Kategoriler.Remove(silinecekler);
                idc.SaveChanges();
                DeleteAllSubCategories(idd);
            }
            catch (Exception)
            {

            }
        }

        List<Kategoriler> lst_category = null;
        private void AllCategories()
        {
            StringBuilder sb = new StringBuilder();
            if (mode == null || mode == "sub")
            {
                if (mode == null)
                    mode = "0";

                List<Kategoriler> lst_category = null;
                if (cid != 0)
                {
                    lst_category = (from ca in idc.Kategoriler
                                    where ca.OwnerID == cid //&& ca.LangId == langId
                                    orderby ca.KategoriSira ascending
                                    select ca).ToList();
                }
                else
                {
                    lst_category = (from ca in idc.Kategoriler
                                    where ca.OwnerID == 0 || ca.OwnerID == null
                                    orderby ca.KategoriSira ascending
                                    select ca).ToList();
                }
                sb.Append("<div class=\"widget first\"><div class='head'><h5 class='iFrames'>Kategoriler</h5><div class='num'> <a href='?mode=add' class='blueNum'><span>Kategori Ekle</span></a></div></div>");
                sb.Append("<table class='tableStatic' cellpadding='0' cellspacing='0' border='0' width='100%'> <thead> <tr><td><a href='#' id='a_all'>Tümü</a></td> <td>Kategori No</th> <td>Kategori Adı </th> <td> Üst Kategori </th> <td>Yayinda Mı?</th> <td>Islemler</th> </tr> </thead><tbody>");
                foreach (var item in lst_category)
                {
                    sb.Append("<tr><td><input mode='" + mode + "' id=\"ch_id\" cid='" + item.KategoriID + "' type=\"checkbox\" /></td><td>" + item.KategoriID + "</td>");
                    sb.Append("<td><input id='" + item.KategoriID + "' class='tx_cat' style='width:300px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.KategoriAdi + "' /></td>");
                    sb.Append("<td>" + GetParentCategory(item.OwnerID) + "</td>");
                    sb.Append("<td>" + YesNo(item.KategoriDurum.ToString()) + "</td>");
                    sb.Append("<td><a class='btn14 topDir mr5' original-title='Düzenle' href=\"?mode=edit&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a><a class='btn14 topDir mr5'  original-title='Alt Kategoriler'  href=\"?mode=sub&cid=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/imageList.png\" /></a><a class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Kategori ve (Varsa) Alt Kategorileri Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a>");
                    sb.Append("<a class='btn14 topDir mr5' original-title='Yukarı Taşı' href=\"?mode=up&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/arrowup.png\" /></a><a class='btn14 topDir mr5' original-title='Aşağı Taşı' href=\"?mode=down&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/arrowdown.png\" /></a> <a class='btn14 topDir mr5'  original-title='Kategori Ayarları'  href=\"?mode=setting&cid=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/settings2.png\" /></a></td></tr>");
                }
                sb.Append("</tbody></table></div>");
            }
            else if (mode == "excel-")
            {
                if (cid == null)
                    cid = 0;
                List<Kategoriler> lst_category = (from ca in idc.Kategoriler
                                                  where ca.KategoriUstKat == cid //&& ca.LangId == langId
                                                  orderby ca.KategoriSira ascending
                                                  select ca).ToList();

                sb.Append("<table cellpadding='2' cellspacing='2' border='0' width='100%'> <thead> <tr> <td> <tr><td>Id</b> </td> <td> <tr><td>Kategori Adi</b> </td> <td> <tr><td>Üst Kategori</b> </td> <td> <tr><td>Yayinda Mi?</b> </td></tr> </thead> <tr>");
                foreach (var item in lst_category)
                {
                    sb.Append("<tr><td align='left'>" + item.KategoriID + "</td>");
                    sb.Append("<td>" + item.KategoriAdi + "</td>");
                    sb.Append("<td>" + GetParentCategory(item.OwnerID) + "</td>");
                    sb.Append("<td>" + YesNo(item.KategoriDurum.ToString()) + "</td>");
                }
                sb.Append("</tr></table>");
            }
            else if (mode == "setting")
            {
                sb.Append("<div class=\"widget first\"><div class='head'><h5 class='iFrames'>Kategoriler</h5><div class='num'> <a href='?mode=add-setting&cid=" + Request.QueryString["cid"].ToString() + "' class='blueNum' style='padding: 4px; margin: 5px;'><span>Kategori Özelliği Ekle</span></a></div></div>");
                sb.Append("<table class='tableStatic' cellpadding='2' cellspacing='2' border='0' width='100%'> <thead> <tr><td><a href='#' id='a_all2'>Tümü</a></td> <td> Id </th> <td> Kategori Özellik Adı </th> <td> Kategori Adı </th> <td> Aktif Mi? </th> <td> İşlemler </th> </tr> </thead>");
                if (cid == null)
                    cid = 0;
                var lst_category_setting = (from ca in idc.KategoriOzellikleri
                                            where ca.KatagoriOzellikKategoriID == cid
                                            select ca).ToList();
                foreach (var item in lst_category_setting)
                {
                    string display = "visibility:hidden;";
                    if (item.KategoriOzellikleriSuzmedeKullan == true)
                        display = DisplayMode("combobox");

                    sb.Append("<tr><td><input mode='" + mode + "'  id=\"ch_id\" cid='" + item.KatagoriOzellikID + "' type=\"checkbox\" /></td><td>" + item.KatagoriOzellikID + "</td><td>" + item.KatagoriOzellikID + "</td>");
                    sb.Append("<td><input id='" + item.KatagoriOzellikID + "' class='tx_cat' style='width:300px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.KatagoriOzellikAdi + "' /></td>");
                    sb.Append("<td>" + GetParentCategory(item.KatagoriOzellikKategoriID) + "</td>");
                    sb.Append("<td>" + YesNo(item.KatagoriOzellikDurum.ToString()) + "</td>");
                    sb.Append("<td><a class='btn14 topDir mr5'  original-title='Düzenle' href=\"?mode=edit-setting&cid=" + item.KatagoriOzellikKategoriID + "&id=" + item.KatagoriOzellikID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a><a  class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Kategori Özelliği Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete-setting&cid=" + item.KatagoriOzellikKategoriID + "&id=" + item.KatagoriOzellikID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a><a style='" + display + "' class='btn14 topDir mr5'  original-title='Kategori Özellik Ayarları' href=\"?mode=setting-ayar&cid=" + item.KatagoriOzellikID + "\"><img src=\"../Images/icons/dark/settings2.png\" /></a>");
                }
                sb.Append("</table></div>");
            }
            else if (mode == "setting-ayar")
            {
                sb.Append("<div class=\"widget first\"><div class='head'><h5 class='iFrames'>Kategoriler Özellik Seçenekleri</h5><div class='num'> <a href='?mode=add-setting-ayar' class='blueNum'><span>Kategori Özellik Seçeneği Ekle</span></a></div></div>");
                sb.Append("<table class='tableStatic' cellpadding='2' cellspacing='2' border='0' width='100%'> <thead> <tr><td><a href='#' id='a_all3'>Tümü</a></td> <td> Id </td> <td> Özellik Adı </td> <td> Değer </td> <td> İşlemler </td> </tr> </thead>");
                if (cid == null)
                    cid = 0;
                var lst_category_setting = (from ca in idc.KategoriOzellikSecenekleri
                                            where ca.KatagoriOzellikSecenekleriKatagoriOzellikID == cid
                                            select ca).ToList();
                foreach (var item in lst_category_setting)
                {
                    sb.Append("<tr><td><input  mode='" + mode + "' id=\"ch_id\" cid='" + item.KatagoriOzellikSecenekleriID + "' type=\"checkbox\" /></td><td>" + item.KatagoriOzellikSecenekleriID + "</td><td>" + GetOzellikAdi(item.KatagoriOzellikSecenekleriKatagoriOzellikID) + "</td>");
                    sb.Append("<td>" + item.KatagoriOzellikSecenekleriDeger + "</td>");
                    sb.Append("<td><a class='btn14 topDir mr5'  original-title='Düzenle' href=\"?mode=edit-setting-ayar&cid=" + item.KatagoriOzellikSecenekleriID + "&id=" + item.KatagoriOzellikSecenekleriID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a><a  class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Kategori Özelliği Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete-setting-ayar&cid=" + item.KatagoriOzellikSecenekleriID + "&id=" + item.KatagoriOzellikSecenekleriID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a>");
                }
                sb.Append("</table></div>");
            }
            else if (mode == "excel-setting")
            {
                sb.Append("<table cellpadding='2' cellspacing='2' border='0' width='100%'> <thead> <tr> <td> <tr><td>Id</b> </td> <td> <tr><td>Kategori Özellik Adı</b> </td> <td> <tr><td>Kategori Adı</b> </td> <td> <tr><td>Aktif Mi?</b> </td> </tr> </thead> <tr>");
                if (cid == null)
                    cid = 0;
                var lst_category_setting = (from ca in idc.KategoriOzellikleri
                                            where ca.KatagoriOzellikKategoriID == cid
                                            select ca).ToList();
                foreach (var item in lst_category_setting)
                {
                    sb.Append("<tr><td align='left'>" + item.KatagoriOzellikID + "</td>");
                    sb.Append("<td>" + item.KatagoriOzellikAdi + "</td>");
                    sb.Append("<td>" + GetParentCategory(item.KatagoriOzellikKategoriID) + "</td>");
                    sb.Append("<td>" + YesNo(item.KatagoriOzellikDurum.ToString()) + "</td>");
                }

                sb.Append("</tr></table>");
            }
            lt_category.Text = sb.ToString();
        }

        private string DisplayMode(string mode)
        {
            if (mode == "combobox")
            {
                mode = "visibility:visible;";
            }
            else
            {
                mode = "visibility:hidden;";
            }
            return mode;
        }

        private void CloseAll()
        {
            dv_kategori_edit.Visible = false;
            dv_kategoriler.Visible = false;
            dv_kategori_ozellik_edit.Visible = false;
            dv_kategori_ozellik_secenekleri_detay.Visible = false;
        }

        public string GetOzellikAdi(int? val)
        {
            string ozellik = (from oz in idc.KategoriOzellikleri
                              where oz.KatagoriOzellikID == val
                              select oz.KatagoriOzellikAdi).SingleOrDefault();
            return ozellik;
        }

        public int i = 1;
        List<ListItem> list_souce = new List<ListItem>();
        private void FillCategories()
        {
            if (!IsPostBack)
            {
                drp_ustkategori.Items.Insert(0, new ListItem("Ana Kategori", "0"));
                drp_kategori.Items.Insert(0, new ListItem("Seçiniz", "0"));
                foreach (var item in idc.Kategoriler)
                {
                    if (item.OwnerID == 0 || item.OwnerID == null)
                    {
                        bosluk = 1;

                        drp_ustkategori.Items.Insert(i, new ListItem(item.KategoriAdi, item.KategoriID.ToString()));
                        drp_kategori.Items.Insert(i, new ListItem(item.KategoriAdi, item.KategoriID.ToString()));
                        i++;
                        SubCategories(item.KategoriID);

                    }
                }
            }
        }

        private void FillSearchCategories()
        {
            if (!IsPostBack)
            {
                drp_a_kategori.Items.Insert(0, new ListItem("Seçiniz", "-1"));
                foreach (var item in idc.Kategoriler)
                {
                    if (item.OwnerID == 0 || item.OwnerID == null || item.OwnerID == cid)
                    {
                        drp_a_kategori.Items.Insert(i, new ListItem(item.KategoriAdi, item.KategoriID.ToString()));
                        i++;
                    }
                }
            }
        }

        private void AllSettings()
        {
            var settings = (from se in idc.KategoriOzellikleri
                            select se).ToArray();
            drp_settingayar.Items.Insert(0, new ListItem("Seçiniz", "0"));
            int say = 1;
            foreach (var item in settings)
            {
                drp_settingayar.Items.Insert(say, new ListItem(item.KatagoriOzellikAdi, item.KatagoriOzellikID.ToString()));
            }
        }

        int bosluk = 1;
        private void SubCategories(int? catId)
        {
            string girinti = "";
            for (int i = 0; i < bosluk; i++)
            {
                girinti += "─";
            }

            var li_sub = (from ca in idc.Kategoriler
                          where ca.OwnerID == catId
                          select ca).ToList();

            foreach (var item in li_sub)
            {
                drp_ustkategori.Items.Insert(i, new ListItem(girinti + " " + item.KategoriAdi, item.KategoriID.ToString()));
                drp_kategori.Items.Insert(i - 1, new ListItem(girinti + " " + item.KategoriAdi, item.KategoriID.ToString()));
                i++;

                int? top_id = item.KategoriID;

                var sub_c = (from sub in idc.Kategoriler
                             where sub.KategoriID == top_id
                             select sub).ToList();

                if (sub_c.Count > 0)
                {
                    bosluk++;
                    SubCategories(sub_c[0].KategoriID);
                }
                else
                {
                    bosluk--;
                }
            }
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (tx_sira.Text != hd_sira.Value && hd_sira.Value != "")
            {
                int? sıra = int.Parse(hd_sira.Value);
                int tid = int.Parse(id);

                int? ownerId = (from q in idc.Kategoriler
                                where q.KategoriID == tid
                                select q.OwnerID).SingleOrDefault();

                int? yeni_sıra = int.Parse(tx_sira.Text);

                int degisecek_id = (from q in idc.Kategoriler
                                    where q.KategoriSira == yeni_sıra && q.OwnerID == ownerId
                                    select q.KategoriID).FirstOrDefault();

                Kategoriler dcat = idc.Kategoriler.Where(c => c.KategoriID == degisecek_id).SingleOrDefault();
                dcat.KategoriSira = sıra;
                idc.SaveChanges();

                tid = int.Parse(id);
                Kategoriler cat = idc.Kategoriler.Where(c => c.KategoriID == tid).SingleOrDefault();
                cat.KategoriSira = yeni_sıra;
                idc.SaveChanges();
            }
            else if (hd_sira.Value == "")
            {

            }

            if (fu_resim.HasFile)
            {
                if (Path.GetExtension(fu_resim.FileName) == ".jpg" ||
                    Path.GetExtension(fu_resim.FileName) == ".jpeg" ||
                    Path.GetExtension(fu_resim.FileName) == ".png" ||
                    Path.GetExtension(fu_resim.FileName) == ".gif" ||
                    Path.GetExtension(fu_resim.FileName) == ".bmp")
                {
                    hd_resim.Value = Tools.make_FileName(fu_resim.FileName, Path.GetExtension(fu_resim.FileName));
                    fu_resim.SaveAs(Server.MapPath("~/Files/Images/" + hd_resim.Value));
                }
            }
            if (fu_icon.HasFile)
            {
                if (Path.GetExtension(fu_icon.FileName) == ".jpg" ||
                    Path.GetExtension(fu_icon.FileName) == ".jpeg" ||
                    Path.GetExtension(fu_icon.FileName) == ".png" ||
                    Path.GetExtension(fu_icon.FileName) == ".gif" ||
                    Path.GetExtension(fu_icon.FileName) == ".bmp")
                {
                    hd_icon.Value = Tools.make_FileName(fu_icon.FileName, Path.GetExtension(fu_icon.FileName));
                    fu_icon.SaveAs(Server.MapPath("~/Files/Images/" + hd_icon.Value));
                }
            }

            switch (mode)
            {
                case "add":
                    {
                        //if (!chk_altkategori.Checked)
                        //    catId = 0;
                        //else
                        catId = int.Parse(drp_ustkategori.SelectedValue);

                        Kategoriler cate = new Kategoriler();
                        //cate.LangId = GetLangId();
                        cate.KategoriAdi = tx_adi.Text;
                        cate.KategoriAdi_Kucuk = Tools.makelink(tx_adi.Text);
                        cate.KategoriSira = GetQueue();
                        cate.KategoriDurum = IsOnline();
                        cate.KategoriIcon = hd_icon.Value;
                        cate.KategoriResim = hd_resim.Value;
                        cate.OzelFiltre = bool.Parse(rd_ozel_filtre.SelectedValue);

                        //if (chk_altkategori.Checked)
                        cate.OwnerID = catId;
                        //else
                        //    cate.KategoriUstKat = 0;

                        idc.Kategoriler.Add(cate);
                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx");
                        break;
                    }
                case "edit":
                    {
                        //if (!chk_altkategori.Checked)
                        //    catId = 0;
                        //else
                        catId = int.Parse(drp_ustkategori.SelectedValue);
                       int tid = int.Parse(id);
                        Kategoriler cate = idc.Kategoriler.First(k => k.KategoriID == tid);
                        //cate.LangId = GetLangId();
                        cate.KategoriAdi = tx_adi.Text;
                        cate.KategoriAdi_Kucuk = Tools.makelink(tx_adi.Text);
                        cate.KategoriSira = GetQueue();
                        cate.KategoriDurum = IsOnline();
                        cate.KategoriIcon = hd_icon.Value;
                        cate.KategoriResim = hd_resim.Value;
                        cate.OzelFiltre = bool.Parse(rd_ozel_filtre.SelectedValue);

                        //if (chk_altkategori.Checked)
                        cate.OwnerID = catId;
                        //else
                        //    cate.KategoriUstKat = 0;

                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx");
                        break;
                    }
                case "add-setting":
                    {
                        bool? is_text = false;
                        is_text = chk_text.Checked;
                        bool? is_drop = false;
                        is_drop = chk_drop.Checked;
                        bool? is_yes = false;
                        is_yes = chk_yesno.Checked;
                        bool? durum = false;
                        durum = bool.Parse(rd_ozellik_aktif.SelectedValue);

                        KategoriOzellikleri k_ozellik = new KategoriOzellikleri();
                        k_ozellik.KatagoriOzellikAdi = tx_ozellik_adi.Text;
                        k_ozellik.KatagoriOzellikAciklama = tx_aciklama.Text;
                        k_ozellik.KatagoriOzellikKategoriID = int.Parse(drp_kategori.SelectedValue);
                        k_ozellik.KategoriOzellikleriIsTextBox = is_text;
                        k_ozellik.KatagoriOzellikDurum = durum;
                        k_ozellik.KategoriOzellikleriTekSecim = is_yes;
                        k_ozellik.KategoriOzellikleriSuzmedeKullan = is_drop;
                        idc.KategoriOzellikleri.Add(k_ozellik);
                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx?mode=setting&cid=" + drp_kategori.SelectedValue);
                        break;
                    }
                case "edit-setting":
                    {
                        bool? is_text = false;
                        is_text = chk_text.Checked;
                        bool? is_drop = false;
                        is_drop = chk_drop.Checked;
                        bool? is_yes = false;
                        is_yes = chk_yesno.Checked;
                        bool? durum = false;
                        durum = bool.Parse(rd_ozellik_aktif.SelectedValue);

                        //if (!chk_altkategori.Checked)
                        //    catId = 0;
                        //else
                        catId = int.Parse(drp_ustkategori.SelectedValue);
                        int tid = int.Parse(id);
                        KategoriOzellikleri k_ozellik = idc.KategoriOzellikleri.First(k => k.KatagoriOzellikID == tid);
                        k_ozellik.KatagoriOzellikAdi = tx_ozellik_adi.Text;
                        k_ozellik.KatagoriOzellikAciklama = tx_aciklama.Text;
                        k_ozellik.KatagoriOzellikKategoriID = int.Parse(drp_kategori.SelectedValue);
                        k_ozellik.KategoriOzellikleriIsTextBox = is_text;
                        k_ozellik.KatagoriOzellikDurum = durum;
                        k_ozellik.KategoriOzellikleriTekSecim = is_yes;
                        k_ozellik.KategoriOzellikleriSuzmedeKullan = is_drop;
                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx?mode=setting&cid=" + drp_kategori.SelectedValue);
                        break;
                    }
                case "add-setting-ayar":
                    {
                        KategoriOzellikSecenekleri cate = new KategoriOzellikSecenekleri();
                        cate.KatagoriOzellikSecenekleriKatagoriOzellikID = int.Parse(drp_settingayar.SelectedValue);
                        cate.KatagoriOzellikSecenekleriDeger = tx_deger.Text;
                        idc.KategoriOzellikSecenekleri.Add(cate);
                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx?mode=setting-ayar&cid=" + drp_settingayar.SelectedValue);
                        break;
                    }
                case "edit-setting-ayar":
                    {
                        int tid = int.Parse(id);
                        KategoriOzellikSecenekleri cate = idc.KategoriOzellikSecenekleri.First(k => k.KatagoriOzellikSecenekleriID == tid);
                        cate.KatagoriOzellikSecenekleriKatagoriOzellikID = int.Parse(drp_settingayar.SelectedValue);
                        cate.KatagoriOzellikSecenekleriDeger = tx_deger.Text;
                        idc.SaveChanges();

                        Response.Redirect("kategoriler.aspx?mode=setting-ayar&cid=" + drp_settingayar.SelectedValue);
                        break;
                    }
            }
        }

        private int? GetLangId()
        {
            string temp = Convert.ToString(Session["DIL"]);
            if (temp == "")
                temp = "tr";

            languages lang = new languages();
            var lang_id = (from lng in idc.languages
                           where lng.Exp == temp
                           select lng.Id).ToList();
            int? l_id = lang_id[0];
            return l_id;
        }

        private int? GetQueue()
        {
            var queue = (from qu in idc.Kategoriler
                         where qu.KategoriUstKat == catId
                         select qu.KategoriSira).Max();
            if (queue == null)
                queue = 1;
            else
                queue++;
            return queue;
        }

        private bool? IsOnline()
        {
            bool? secili = true;
            if (chk_yayin.Checked)
                secili = false;
            return secili;
        }

        public string YesNo(string value)
        {
            switch (value)
            {
                case "False":
                    {
                        value = "Hayır";
                        break;
                    }
                case "True":
                    {
                        value = "Evet";
                        break;
                    }
            }
            return value;
        }

        public string GetParentCategory(int? id)
        {
            int? temp_id = id;
            string categoryName = null;
            try
            {
                List<Kategoriler> ct_kat = (from ca in idc.Kategoriler
                                            where ca.KategoriID == temp_id
                                            select ca).ToList();
                categoryName = ct_kat[0].KategoriAdi;
            }
            catch { categoryName = " - "; }
            return categoryName;
        }

        protected void Arama_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            List<Kategoriler> kategoriler = new List<Kategoriler>();
            lt_category.Text = "";

            if (drp_a_kategori.SelectedIndex > 0)
            {
                kategoriler = (from ca in idc.Kategoriler
                               where ca.KategoriUstKat == int.Parse(drp_a_kategori.SelectedValue) //&& ca.LangId == langId
                               orderby ca.KategoriSira ascending
                               select ca).Where(ka => ka.KategoriAdi.Contains(tx_a_adi.Text.Trim())).ToList();
            }
            else
            {
                kategoriler = (from ca in idc.Kategoriler
                               where ca.KategoriUstKat == 0 || ca.KategoriUstKat == null
                               orderby ca.KategoriSira ascending
                               select ca).Where(ka => ka.KategoriAdi.Contains(tx_a_adi.Text.Trim())).ToList();
            }

            sb.Append("<div class='table'> <div class='head'><h5 class='iFrames'>Kategoriler</h5></div>");
            sb.Append("<table class='tableStatic' cellpadding='0' cellspacing='0' border='0' width='100%'> <thead> <tr><td><a href='#' id='a_all'>Tümü</a></td> <td>Kategori No</th> <td>Kategori Adı </th> <td> Üst Kategori </th> <td>Yayinda Mı?</th> <td>Islemler</th> </tr> </thead><tbody>");
            foreach (var item in kategoriler)
            {
                sb.Append("<tr><td><input id=\"ch_id\" cid='" + item.KategoriID + "' type=\"checkbox\" /></td><td>" + item.KategoriID + "</td>");
                sb.Append("<td><input id='" + item.KategoriID + "' class='tx_cat' style='width:300px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.KategoriAdi + "' /></td>");
                sb.Append("<td>" + GetParentCategory(item.KategoriUstKat) + "</td>");
                sb.Append("<td>" + YesNo(item.KategoriDurum.ToString()) + "</td>");
                sb.Append("<td><a class='btn14 topDir mr5' original-title='Düzenle' href=\"?mode=edit&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a><a class='btn14 topDir mr5'  original-title='Alt Kategoriler'  href=\"?mode=sub&cid=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/imageList.png\" /></a><a class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu Kategori ve (Varsa) Alt Kategorileri Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete&id=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/trash.png\" /></a>");
                sb.Append("<a class='btn14 topDir mr5'  original-title='Kategori Ayarları'  href=\"?mode=setting&cid=" + item.KategoriID + "\"><img src=\"../Images/icons/dark/settings2.png\" /></a></td></tr>");
            }
            sb.Append("</tbody></table></div>");
            lt_category.Text = sb.ToString();

        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("kategoriler.aspx");
        }
    }
}