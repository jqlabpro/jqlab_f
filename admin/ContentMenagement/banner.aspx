<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masterpage/admin.Master" AutoEventWireup="true" CodeBehind="banner.aspx.cs" Inherits="icebear_v2.EC.banner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/admin/Controls/Pager/pager.ascx" TagPrefix="uc1" TagName="pager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div runat="server" id="dv_bannerlar" clientidmode="Static">
        <div class="breadCrumbHolder module">
            <div class="breadCrumb module">
                <ul>
                    <li class="firstB"><a href="#">Home</a> </li>
                    <li><a href="#">E-Ticaret</a></li>
                    <li>Bannerlar</li>
                </ul>
            </div>
        </div>
        <asp:Literal ID="lt_banner" runat="server"></asp:Literal>
        <br />
        <a href="#" class='redBtn' style="padding: 4px; margin: 5px;" id="tumunu_sil">
            <span>Seçilenleri Sil</span>
        </a>
        <br />
        <uc1:pager runat="server" ID="pager" _count="10" _sayfa="banner" />
    </div>
    <div runat="server" id="dv_banner_detay" clientidmode="Static" class="mainForm">
        <fieldset>
            <div class="title">
                <h5>Banner Alanı Ayarları</h5>
            </div>
            <div class="widget first">
                <div class="head">
                </div>
                <table width="100%">
                    <tr>
                        <td>Banner Adı</td>
                        <td>
                            <asp:TextBox ID="tx_banneradi" runat="server" ValidationGroup="vld_1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_banneradi" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Genişlik</td>
                        <td>
                            <asp:TextBox ID="tx_genis" runat="server" ValidationGroup="vld_1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_genis" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tx_genis" Display="Dynamic" ErrorMessage="Sadece Rakam Girilmeli" ForeColor="#CC0000" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Yükseklik</td>
                        <td>
                            <asp:TextBox ID="tx_yuksek" runat="server" ValidationGroup="vld_1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_yuksek" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tx_yuksek" Display="Dynamic" ErrorMessage="Sadece Rakam Girilmeli" ForeColor="#CC0000" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Max. Tıklama</td>
                        <td>
                            <asp:TextBox ID="tx_max" runat="server" ValidationGroup="vld_1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_max" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tx_max" Display="Dynamic" ErrorMessage="Sadece Rakam Girilmeli" ForeColor="#CC0000" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_kaydet" runat="server" Text="Kaydet" CssClass="greyishBtn submitForm" OnClick="btn_kaydet_Click" ValidationGroup="vld_1" /></td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div runat="server" id="dv_banner_detay_sub" class="mainForm">
        <fieldset>
            <div class="title">
                <h5>Banner Alanı Ayarları</h5>
            </div>
            <div class="widget first">
                <div class="head">
                </div>
                <table cellpadding="10" cellspacing="10" border="0" width="800">
                    <tr>
                        <td>Banner Alanı</td>
                        <td>
                            <asp:DropDownList ID="drp_banners" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Resim</td>
                        <td>
                            <asp:FileUpload ID="fu_resim" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Url</td>
                        <td>
                            <asp:TextBox ID="tx_url" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_url" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Sıra</td>
                        <td><asp:TextBox ID="tx_sira" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Başlangıç Tarihi</td>
                        <td>
                            <asp:TextBox ID="tx_baslan" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                            <asp:CalendarExtender TargetControlID="tx_baslan" ID="CalendarExtender1" runat="server"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_baslan" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Bitiş Tarihi</td>
                        <td>
                            <asp:TextBox ID="tx_bitis" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                            <asp:CalendarExtender TargetControlID="tx_bitis" ID="CalendarExtender2" runat="server"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tx_bitis" Display="Dynamic" ErrorMessage="*" ForeColor="#CC0000" ValidationGroup="vld_2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Gösterim</td>
                        <td>
                            <asp:TextBox ID="tx_gosterim" runat="server" ValidationGroup="vld_1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Tıklanma</td>
                        <td>
                            <asp:TextBox ID="tx_tiklanma" runat="server" ValidationGroup="vld_1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Akfit Mi ?</td>
                        <td>
                            <asp:RadioButtonList ID="chk_reklam_aktif" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Evet" Value="True" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Hayır" Value="False"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Üye No</td>
                        <td>
                            <asp:TextBox ID="tx_uyeno" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="tx_uyeno" Display="Dynamic" ErrorMessage="Sadece Rakam Girilmeli" ForeColor="#CC0000" ValidationExpression="^[0-9]+$" ValidationGroup="vld_2"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Max Tıklanma</td>
                        <td>
                            <asp:TextBox ID="tx_max_tik" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tx_max_tik" Display="Dynamic" ErrorMessage="Sadece Rakam Girilmeli" ForeColor="#CC0000" ValidationExpression="^[0-9]+$" ValidationGroup="vld_2"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Banner Reklam Başlık</td>
                        <td>
                            <asp:TextBox ID="tx_reklam_baslik" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Banner Reklam Detay</td>
                        <td>
                            <asp:TextBox ID="tx_reklam_detay" runat="server" ValidationGroup="vld_2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Kaydet" CssClass="greyishBtn submitForm" OnClick="btn_kaydet_Click" ValidationGroup="vld_2" />
                            <asp:HiddenField ID="hd_Resim" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <script type="text/javascript">
        j(function () {
            var secili = 0;

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

            j("#a_all").click(function () {
                if (secili == 0) {
                    j(this).html("İptal");
                    j("#dv_bannerlar input[type=checkbox]").attr("checked", "checked");
                    j(".jqTransformCheckbox").addClass("jqTransformChecked");
                    secili = 1;
                    j("#tumunu_sil").show();
                }
                else {
                    j(this).html("Tümü");
                    j("#dv_bannerlar input[type=checkbox]").attr("checked", "");
                    j(".jqTransformCheckbox").removeClass("jqTransformChecked");
                    secili = 0;
                    j("#tumunu_sil").hide();
                }
                return false;
            });

            j("#tumunu_sil").click(function () {
                var cid = 1;
                var sayi = j("#dv_bannerlar input[type=checkbox]:checked").size();
                for (var i = 0; i < sayi; i++) {
                    cid = j("#dv_bannerlar input[type=checkbox]:checked").eq(i).attr("cid");
                    var row = j("#dv_bannerlar input[type=checkbox]:checked").eq(i).closest('tr');
                    row.hide();
                    j.ajax({
                        type: "POST",
                        url: "../EC/banner.aspx/BatchDelete",
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
                window.location = window.location.href;
            });

            j("#a_all2").click(function () {
                if (secili == 0) {
                    j(this).html("İptal");
                    j("#dv_bannerlar input[type=checkbox]").attr("checked", "checked");
                    j(".jqTransformCheckbox").addClass("jqTransformChecked");
                    secili = 1;
                    j("#tumunu_sil").show();
                }
                else {
                    j(this).html("Tümü");
                    j("#dv_bannerlar input[type=checkbox]").attr("checked", "");
                    j(".jqTransformCheckbox").removeClass("jqTransformChecked");
                    secili = 0;
                    j("#tumunu_sil").hide();
                }
            });

            j("#tumunu_sil2").click(function () {
                var cid = 1;
                var sayi = j("#dv_bannerlar input[type=checkbox]:checked").size();
                for (var i = 0; i < sayi; i++) {
                    cid = j("#dv_bannerlar input[type=checkbox]:checked").eq(i).attr("cid");
                    var row = j("#dv_bannerlar input[type=checkbox]:checked").eq(i).closest('tr');
                    row.hide();
                    j.ajax({
                        type: "POST",
                        url: "../EC/banner.aspx/BatchDelete2",
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
