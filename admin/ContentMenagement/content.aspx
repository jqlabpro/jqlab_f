<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masterpage/admin.Master" AutoEventWireup="true" CodeBehind="content.aspx.cs" Inherits="YonetimPaneli.ContentMenagement.Icerik" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dv_icerikler" runat="server" class="mainForm">
        <fieldset>
            <div class="breadCrumbHolder module">
                <div class="breadCrumb module">
                    <ul>
                        <li class="firstB"><a href="#">Home</a> </li>
                        <li><a href="#">İçerik Yönetimi</a></li>
                        <li>İçerikler</li>
                    </ul>
                </div>
            </div>
            <div id="dv_table" runat="server" clientidmode="Static">
                <asp:Literal ID="lt_contents" runat="server"></asp:Literal>
            </div>
        </fieldset>
        <a class='btn14 mr5' title="Excel Olarak Kaydet" href="?mode=excel">
            <img src="../Images/icons/middlenav/exceldoc.png" /></a>
    </div>
    <div id="dv_icerik_ekleduzenle" runat="server" class="mainForm">
        <div class="title">
            <h5>İçerik Detayları</h5>
        </div>
        <div class="widget first">
            <div class="head">
            </div>
            <table cellpadding="10" cellspacing="10" border="0" width="800">
                <tr>
                    <td>İçerik Başlığı : </td>
                    <td>
                        <asp:TextBox ID="tx_baslik" Width="400" ClientIDMode="Static" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>Link :</td>
                    <td>
                        <asp:TextBox ID="tx_link" Width="400" ClientIDMode="Static" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>Yer Aldığı Kategori : </td>
                    <td>
                        <asp:DropDownList ID="drp_ustkategori" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:CheckBox ID="ch_tamsayfa" runat="server" Text="Bu İçerik Özel Sayfadır" />
                    </td>
                </tr>
                <tr>
                    <td>İçerik </td>
                    <td>
                        <CKEditor:CKEditorControl ID="ck_icerik" Width="95%" runat="server"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chk_yayin" runat="server" Text="Bu İçerik Yayında Değil" /><br />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btn_kaydet" CssClass="greyishBtn bt14" runat="server" Text="Kaydet"
                            OnClick="btn_kaydet_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        j(function () {
            var secili = 0;

            j("#a_all").click(function () {
                if (secili == 0) {
                    j(this).html("İptal");
                    j("#dv_table input[type=checkbox]").attr("checked", "checked");
                    j(".jqTransformCheckbox").addClass("jqTransformChecked");
                    secili = 1;
                    j("#tumunu_sil").show();
                }
                else {
                    j(this).html("Tümü");
                    j("#dv_table input[type=checkbox]").attr("checked", "");
                    j(".jqTransformCheckbox").removeClass("jqTransformChecked");
                    secili = 0;
                    j("#tumunu_sil").hide();
                }
            });

            j("#tumunu_sil").hide();

            j("input[type=checkbox]").change(function () {
                var siz = j("input[type=checkbox]:checked").size();
                if (siz > 0) {
                    j("#tumunu_sil").show();
                }
                else {
                    j("#tumunu_sil").hide();
                }
            });

            j("#tumunu_sil").click(function () {
                var cid = 1;
                var sayi = j("#dv_table input[type=checkbox]:checked").size();
                for (var i = 0; i < sayi; i++) {
                    cid = j("#dv_table input[type=checkbox]:checked").eq(i).attr("cid");
                    var row = j("#dv_table input[type=checkbox]:checked").eq(i).closest('tr');
                    row.hide();
                    j.ajax({
                        type: "POST",
                        url: "../ContentMenagement/content.aspx/BatchDelete",
                        data: "{cid:" + cid + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (icerik) {
                            //alert("silindi");
                        },
                        error: function (hata) {
                            //alert(hata);
                        }
                    });
                    j("#tumunu_sil").hide();
                }
            });

        });
    </script>
</asp:Content>
