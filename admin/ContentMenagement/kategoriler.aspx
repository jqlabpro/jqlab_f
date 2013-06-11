<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masterpage/admin.Master" AutoEventWireup="True" CodeBehind="kategoriler.aspx.cs" Inherits="YonetimPaneli.ContentMenagement.kategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tbl tr td {
            width: 150px auto;
            padding: 5px;
        }
    </style>
    <div id="dv_kategoriler" runat="server">
        <div class="breadCrumbHolder module">
            <div class="breadCrumb module">
                <ul>
                    <li class="firstB"><a href="#">Home</a> </li>
                    <li><a href="#">İçerik Yönetimi</a></li>
                    <%--<%= sayfa %>
                    <%= yol %>--%>
                </ul>
            </div>
        </div>
        <div class="mainForm" runat="server" id="dv_arama">
            <div class="widget first">
                <div class="head closed">
                    <h5 class="iFrames">Kategori Arama</h5>
                </div>
                <div class="body">
                    <table class="tbl">
                        <tr>
                            <td><b>Kategori Adı</b><br />
                                <asp:TextBox ID="tx_a_adi" Width="150" runat="server"></asp:TextBox>
                            </td>
                            <td><b>Alt Kategorilerde Ara</b><br />
                                <asp:DropDownList ID="drp_a_kategori" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="Arama" runat="server" CssClass="redBtn" Text="Ara" OnClick="Arama_Click" />
                                <asp:Button ID="btn_reset" runat="server" CssClass="redBtn" Text="Temizle" OnClick="btn_reset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dv_table" runat="server" clientidmode="Static">
            <asp:Literal ID="lt_category" runat="server"></asp:Literal>
        </div>
        <br />
        <a href="#" class='redBtn' style="padding: 4px; margin: 5px;" id="tumunu_sil">
            <span>Seçilenleri Sil</span>
        </a>

        <br />
        <%--<a class='btn14 mr5' title="Excel Olarak Kaydet" href="?mode=excel<%= "-" + mode %>&cid=<%=cid %>">
            <img src="../Images/icons/middlenav/exceldoc.png" /></a>--%>
    </div>
    <div id="dv_kategori_edit" runat="server" class="mainForm">
        <fieldset>
            <div class="title">
                <h5>Kategori Detayları</h5>
            </div>
            <div class="widget first">
                <div class="head">
                </div>
                <table>
                    <tr>
                        <td>Kategori Adı : </td>
                        <td>
                            <asp:TextBox ID="tx_adi" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Üst Kategori : </td>
                        <td>
                            <asp:DropDownList ID="drp_ustkategori" Width="200px" runat="server">
                            </asp:DropDownList><br />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:CheckBox ID="chk_altkategori" Text="Bunu Alt Kategori Olarak Ayarla" runat="server" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chk_yayin" Text="Bu Kategori ve Alt Kategorileri Yayında Değil" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Kategori Resmi </td>
                        <td>
                            <asp:FileUpload ID="fu_resim" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Kategori Simgesi </td>
                        <td>
                            <asp:FileUpload ID="fu_icon" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Özel Filtre </td>
                        <td>
                            <asp:RadioButtonList ID="rd_ozel_filtre" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Var" Value="True" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yok" Value="False"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td>Sıra</td>
                        <td>
                            <asp:TextBox ID="tx_sira" Width="40px" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hd_sira" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_kaydet" runat="server" Text="Kaydet" CssClass="greyishBtn bt14" OnClick="btn_kaydet_Click" />
                            <asp:HiddenField ID="hd_icon" runat="server" />
                            <asp:HiddenField ID="hd_resim" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div id="dv_kategori_ozellik_secenekleri_detay" runat="server" class="mainForm">
        <fieldset>
            <div class="title">
                <h5>Kategori Özellik Seçenekleri Detayları</h5>
            </div>
            <div class="widget first">
                <div class="head">
                </div>
                <table cellpadding="10" cellspacing="10" border="0" width="800">
                    <tr>
                        <td>Özellik Adı</td>
                        <td>
                            <asp:DropDownList ID="drp_settingayar" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Değer</td>
                        <td>
                            <asp:TextBox ID="tx_deger" runat="server" ValidationGroup="vld3"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="tx_deger" ValidationGroup="vld3" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_kaydet2" runat="server" CssClass="greyishBtn" Text="Kaydet" ValidationGroup="vld3" OnClick="btn_kaydet_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>

    <div id="dv_kategori_ozellik_edit" runat="server" class="mainForm">
        <fieldset>
            <div class="title">
                <h5>Kategori Özelliği Detayları</h5>
            </div>
            <div class="widget first">
                <div class="head">
                </div>
                <table cellpadding="10" cellspacing="10" border="0" width="800">
                    <tr>
                        <td>Kategori Özelliği Adı : </td>
                        <td>
                            <asp:TextBox ID="tx_ozellik_adi" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Kategori : </td>
                        <td>
                            <asp:DropDownList ID="drp_kategori" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Açıklama : </td>
                        <td>
                            <asp:TextBox ID="tx_aciklama" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Aktif Mi?</td>
                        <td>
                            <asp:RadioButtonList ID="rd_ozellik_aktif" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Aktif" Value="True" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Pasif" Value="False"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td>Görüntülenme Modu </td>
                        <td>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chk_yesno" runat="server" Text="Evet / Hayır Seçeneği Yerleştir" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chk_text" runat="server" Text="Text Kutusu Yerleştir" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chk_drop" runat="server" Text="Açılır Menü Yerleştir" /></td>
                            </tr>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_kaydet1" runat="server" Text="Kaydet" CssClass="greyishBtn bt14" OnClick="btn_kaydet_Click" /></td>
                    </tr>
                </table>
            </div>
        </fieldset>
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


            j("#tumunu_sil").click(function () {
                var mode;
                var cid = 1;
                var sayi = j("#dv_table input[type=checkbox]:checked").size();
                for (var i = 0; i < sayi; i++) {
                    cid = j("#dv_table input[type=checkbox]:checked").eq(i).attr("cid");
                    mode = j("#dv_table input[type=checkbox]:checked").eq(i).attr("mode");
                    var row = j("#dv_table input[type=checkbox]:checked").eq(i).closest('tr');
                    row.hide();

                    if (mode == "sub" || mode == "0") {
                        j.ajax({
                            type: "POST",
                            url: "../ContentMenagement/kategoriler.aspx/BatchDelete",
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
                    }
                    else if(mode == "setting"){
                        j.ajax({
                            type: "POST",
                            url: "../ContentMenagement/kategoriler.aspx/BatchDelete2",
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
                    }
                    else if (mode == "setting-ayar") {
                        j.ajax({
                            type: "POST",
                            url: "../ContentMenagement/kategoriler.aspx/BatchDelete3",
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
                    }
                }
                return false;
            });

            j("#a_all2").click(function () {
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

        });
    </script>
</asp:Content>
