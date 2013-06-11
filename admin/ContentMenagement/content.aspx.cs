using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using jqLab.datamodel;
using icebear_v2.Class;
using System.Text;
using System.Web.Services;

namespace YonetimPaneli.ContentMenagement
{
    public partial class Icerik : System.Web.UI.Page
    {
        static JqlabEntities idc = new JqlabEntities();

        [WebMethod]
        public static string ChangeName(int ids, string deger)
        {
            contents conte = idc.contents.First(co => co.Id == ids);
            conte.Title = deger;
            conte.Link = Tools.makelink(deger);
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
        public int? cid, langId, catId;

        protected void Page_Load(object sender, EventArgs e)
        {
            CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
            _FileBrowser.BasePath = "/ckfinder/";
            _FileBrowser.SetupCKEditor(ck_icerik);

            CloseAll();

            langId = GetLangId();
            mode = Convert.ToString(Request.QueryString["mode"]);
            id = Convert.ToString(Request.QueryString["id"]);

            switch (mode)
            {
                case "add":
                    {
                        FillCategories();
                        dv_icerik_ekleduzenle.Visible = true;
                        break;
                    }
                case "edit":
                    {
                        FillCategories();
                        GetDetails();
                        dv_icerik_ekleduzenle.Visible = true;
                        break;
                    }
                case "delete":
                    {
                        int tid = int.Parse(id);
                        dv_icerikler.Visible = true;
                        var silinecek = (from ctg in idc.contents
                                         where ctg.Id == tid
                                         select ctg).Single();
                        idc.contents.Remove(silinecek);
                        idc.SaveChanges();

                        Response.Redirect("content.aspx");
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

                        Response.Redirect("content.aspx");
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
                        Response.Redirect("content.aspx");
                        break;
                    }
                case "excel":
                    {
                        AllContents();
                        Save2Excel();
                        break;
                    }
                case null:
                    {
                        AllContents();
                        dv_icerikler.Visible = true;
                        break;
                    }
            }
        }

        [WebMethod]
        public static string makeLink(string deger)
        {
            return Tools.makelink(deger);
        }

        public int i = 0;
        List<ListItem> list_souce = new List<ListItem>();
        private void FillCategories()
        {
            if (!IsPostBack)
            {

                foreach (var item in idc.Kategoriler)
                {
                    if (item.OwnerID == 0)
                    {
                        bosluk = 1;
                        drp_ustkategori.Items.Insert(i, new ListItem(item.KategoriAdi, item.KategoriID.ToString()));
                        i++;
                        SubCategories(item.KategoriID);

                    }
                }
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
                i++;

                int? top_id = item.KategoriID;

                var sub_c = (from sub in idc.categories
                             where sub.Id == top_id
                             select sub).ToList();

                if (sub_c.Count > 0)
                {
                    bosluk++;
                    SubCategories(sub_c[0].Id);
                }
                else
                {
                    bosluk--;
                }
            }
        }

        private void Move(string where)
        {
            switch (where)
            {
                case "down":
                    {
                        int? sıra = (from q in idc.Kategoriler
                                     where q.KategoriID == int.Parse(id)
                                     select q.KategoriSira).SingleOrDefault();

                        int? yeni_sıra = (from q in idc.Kategoriler
                                          where q.KategoriSira > sıra
                                          select q.KategoriSira).Min();

                        int degisecek_id = (from q in idc.Kategoriler
                                            where q.KategoriSira == yeni_sıra
                                            select q.KategoriID).FirstOrDefault();

                        contents dcat = idc.contents.Where(c => c.Id == degisecek_id).Single();
                        dcat.Queue = sıra;
                        idc.SaveChanges();

                        contents cat = idc.contents.Where(c => c.Id == int.Parse(id)).Single();
                        cat.Queue = sıra + 1;
                        idc.SaveChanges();

                        break;
                    }
                case "up":
                    {
                        int? sıra = (from q in idc.Kategoriler
                                     where q.KategoriID == int.Parse(id)
                                     select q.KategoriSira).SingleOrDefault();

                        int? yeni_sıra = (from q in idc.Kategoriler
                                          where q.KategoriSira < sıra
                                          select q.KategoriSira).Max();

                        int degisecek_id = (from q in idc.Kategoriler
                                            where q.KategoriSira == yeni_sıra
                                            select q.KategoriID).FirstOrDefault();

                        contents dcat = idc.contents.Where(c => c.Id == degisecek_id).Single();
                        dcat.Queue = sıra;
                        idc.SaveChanges();

                        contents cat = idc.contents.Where(c => c.Id == int.Parse(id)).Single();
                        cat.Queue = sıra - 1;
                        idc.SaveChanges();
                        break;
                    }
            }
        }

        private void Save2Excel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=icerikler.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            this.EnableViewState = false;
            dv_table.InnerHtml = lt_contents.Text;
            Response.Write(dv_table.InnerHtml);
            Response.End();
        }

        private void AllContents()
        {
            StringBuilder sb = new StringBuilder();
            if (mode == "excel")
            {
                List<contents> lst_contents = (from co in idc.contents
                                               orderby co.Queue ascending
                                               select co).ToList();
                sb.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'> <thead> <tr><td> Id</td> <td> Baslik</td> <td>Kategorisi</td> <td>Yayinda Mi?<td>Link</td> </tr> </thead> <tr>");
                foreach (var item in lst_contents)
                {
                    sb.Append("<tr><td align='left'>" + item.Id + "</td>");
                    sb.Append("<td>" + item.Title + "</td>");
                    sb.Append("<td>" + GetParentCategory(item.CatId) + "</td>");
                    sb.Append("<td>" + YesNo(item.Yayin.ToString()) + "</td>");
                    sb.Append("<td>" + item.Link + "</td>");
                }
                sb.Append("</tr></table>");
            }
            else
            {
                List<contents> lst_contents = (from co in idc.contents
                                               orderby co.Queue ascending
                                               select co).ToList();
                sb.Append("<div class=\"widget first\"><div class='head'><h5 class='iFrames'>İçerikler</h5><div class='num'> <a href='?mode=add' class='blueNum' style='padding: 4px; margin: 5px;'><span>İçerik Ekle</span></a></div></div>");
                sb.Append("<table cellpadding='0' class='tableStatic' cellspacing='0' border='0' width='100%'> <thead> <tr><td><a href='#' id='a_all'>Tümü</a></td>  <td>Id</td> <td>Baslik</td> <td>Kategorisi</td> <td>Yayinda Mi?</td><td>Link</td><td>İşlemler</td> </tr> </thead> <tr>");
                foreach (var item in lst_contents)
                {
                    sb.Append("<tr><td><input id=\"ch_id\" cid='" + item.Id + "' type=\"checkbox\" /></td><td>" + item.Id + "</td>");
                    sb.Append("<td><input id='" + item.Id + "' class='tx_cont' style='width:300px; border:none; background-image:none; background-color:transparent;' type='text' value='" + item.Title + "' /></td>");
                    sb.Append("<td>" + GetParentCategory(item.CatId) + "</td>");
                    sb.Append("<td>" + YesNo(item.Yayin.ToString()) + "</td>");
                    sb.Append("<td>" + item.Link + "</td>");
                    //sb.Append("<td>" + item.Queue + "</td>");
                    sb.Append("<td><a  class='btn14 topDir mr5' original-title='Düzenle' href=\"?mode=edit&id=" + item.Id + "\"><img src=\"../Images/icons/dark/pencil.png\" /></a><a class='btn14 topDir mr5' original-title='İçerik Resimleri' href=\"pictures.aspx?mode=pic&cid=" + item.Id + "\"><img src=\"../Images/icons/dark/photos.png\" /></a> <a class='btn14 topDir mr5' original-title='Yukarı Taşı' rel='delete' href=\"?mode=up&id=" + item.Id + "\"><img src=\"../Images/icons/dark/arrowup.png\" /></a> <a class='btn14 topDir mr5' original-title='Aşağı Taşı' href=\"?mode=down&id=" + item.Id + "\"><img src=\"../Images/icons/dark/arrowdown.png\" /></a> <a class='btn14 topDir mr5' rel='delete' original-title='Silme Onayı' mesaj='Bu İçerik ve (Varsa) İçeriğe Ait Resimler Kalıcı Olarak Silinecek.Devam Edilsin Mi ?' href=\"?mode=delete&id=" + item.Id + "\"><img src=\"../Images/icons/dark/trash.png\" /></a></td></tr>");
                }
                sb.Append("</tbody></table></div>");
            }
            lt_contents.Text = sb.ToString();
        }

        private void GetDetails()
        {
            int tid = int.Parse(id);
            if (!IsPostBack)
            {
                List<contents> lst_content = (from co in idc.contents
                                              where co.Id == tid
                                              select co).ToList();
                tx_baslik.Text = lst_content[0].Title;
                tx_link.Text = lst_content[0].Link;
                drp_ustkategori.SelectedValue = lst_content[0].CatId.ToString();
                ck_icerik.Text = HttpUtility.HtmlDecode(lst_content[0].Contents1);
                chk_yayin.Checked = bool.Parse(lst_content[0].TamSayfa.ToString());
                int? yayin = lst_content[0].Yayin;
                if (yayin == 0)
                    chk_yayin.Checked = false;
                else
                    chk_yayin.Checked = true;

                catId = lst_content[0].CatId;
            }
        }

        private void CloseAll()
        {
            dv_icerik_ekleduzenle.Visible = false;
            dv_icerikler.Visible = false;
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
            if (catId != int.Parse(drp_ustkategori.SelectedValue))
            {
                catId = int.Parse(drp_ustkategori.SelectedValue);
            }

            var queue = (from qu in idc.contents
                         where qu.CatId == catId
                         select qu.Queue).Max();
            if (queue == null)
                queue = 1;
            else
                queue++;

            return queue;
        }

        private int? IsOnline()
        {
            int? secili = 1;
            if (!chk_yayin.Checked)
                secili = 0;
            return secili;
        }

        public string YesNo(string value)
        {
            switch (value)
            {
                case "0":
                    {
                        value = "Evet";
                        break;
                    }
                case "1":
                    {
                        value = "Hayır";
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
                List<categories> ct_kat = (from ca in idc.categories
                                           where ca.Id == temp_id
                                           select ca).ToList();
                categoryName = ct_kat[0].Name;
            }
            catch { categoryName = " - "; }
            return categoryName;
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                case "add":
                    {
                        contents cont = new contents();
                        catId = int.Parse(drp_ustkategori.SelectedValue);
                        cont.Title = tx_baslik.Text;
                        cont.LangId = GetLangId();

                        if (tx_link.Text != "")
                            cont.Link = tx_link.Text;
                        else
                        {
                            cont.Link = Tools.makelink(tx_baslik.Text);
                        }

                        cont.Queue = GetQueue();
                        cont.Yayin = IsOnline();
                        cont.Contents1 = HttpUtility.HtmlEncode(ck_icerik.Text);
                        cont.CatId = int.Parse(drp_ustkategori.SelectedValue);
                        cont.TamSayfa = chk_yayin.Checked;
                        idc.contents.Add(cont);
                        idc.SaveChanges();

                        Response.Redirect("content.aspx");
                        break;
                    }
                case "edit":
                    {
                        int tid = int.Parse(id);
                        contents cont = idc.contents.First(k => k.Id == tid);
                        cont.Title = tx_baslik.Text;
                        cont.LangId = GetLangId();

                        if (tx_link.Text != "")
                            cont.Link = tx_link.Text;
                        else
                        {
                            cont.Link = Tools.makelink(tx_baslik.Text);
                        }

                        cont.TamSayfa = chk_yayin.Checked;
                        cont.Queue = GetQueue();
                        cont.Yayin = IsOnline();
                        cont.Contents1 = HttpUtility.HtmlEncode(ck_icerik.Text);
                        cont.CatId = int.Parse(drp_ustkategori.SelectedValue);

                        idc.SaveChanges();

                        Response.Redirect("content.aspx");
                        break;
                    }
            }
        }
    }
}