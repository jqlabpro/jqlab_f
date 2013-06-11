<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masterpage/admin.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="YonetimPaneli.ContentMenagement.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dv_kullanicilar" runat="server" class="mainForm" clientidmode="Static">
        <fieldset>
            <div class="breadCrumbHolder module">
                <div class="breadCrumb module">
                    <ul>
                        <li class="firstB"><a href="#">Home</a> </li>
                        <li><a href="#">İçerik Yönetimi</a></li>
                        <li>Kullanıcılar</li>
                    </ul>
                </div>
            </div>
            <div class="widget first">
                <div class='head'>
                    <h5 class='iFrames'>Kullanıcı Listesi</h5>
                    <div class='num'> <a href='?mode=add' class='blueNum' style='padding: 4px; margin: 5px;'><span>Kullacını Ekle</span></a></div>
                </div>
                <table class="tableStatic" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <td><a href='#' id='a_all'>Tümü</a></td>
                            <td>Id</td>
                            <td>İsim</td>
                            <td>E-Posta</td>
                            <td>Kullanıcı Adı</td>
                            <td>İşlemler</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="lt_users" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <br />
            <a href="#" class='redBtn' style="padding: 4px; margin: 5px;" id="tumunu_sil">
                <span>Seçilenleri Sil</span>
            </a>
            <br />
        </fieldset>
    </div>
    <div id="dv_kullanici_duzenle" runat="server" class="mainForm">
        <div class="title">
            <h5>Kullanıcı Detayları</h5>
        </div>
        <fieldset>
            <div class="widget first">
                <div class="head">
                </div>
                <table cellpadding="10" cellspacing="10" border="0" width="800">
                    <tr>
                        <td>İsim Soyisim :</td>
                        <td>
                            <asp:TextBox ID="tx_isim" Width="300" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Kullanıcı Adı : </td>
                        <td>

                            <asp:TextBox ID="tx_kadi" Width="300" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>E-Posta : </td>
                        <td>

                            <asp:TextBox ID="tx_eposta" Width="300" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Şifre : </td>
                        <td>

                            <asp:TextBox ID="tx_pass" Width="300" TextMode="Password" runat="server"></asp:TextBox><span id="spn_uyari" runat="server"> ( Değiştirmek İstemiyorsanız Boş Bırakın )</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_kaydet" runat="server" Text="Kaydet" CssClass="greyishBtn bt14" OnClick="btn_kaydet_Click" />
                            <asp:HiddenField ID="hd_sifre" runat="server" />
                        </td>
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
                    j("#dv_kullanicilar input[type=checkbox]").attr("checked", "checked");
                    j(".jqTransformCheckbox").addClass("jqTransformChecked");
                    secili = 1;
                    j("#tumunu_sil").show();
                }
                else {
                    j(this).html("Tümü");
                    j("#dv_kullanicilar input[type=checkbox]").attr("checked", "");
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
                var sayi = j("#dv_kullanicilar input[type=checkbox]:checked").size();
                for (var i = 0; i < sayi; i++) {
                    cid = j("#dv_kullanicilar input[type=checkbox]:checked").eq(i).attr("cid");
                    var row = j("#dv_kullanicilar input[type=checkbox]:checked").eq(i).closest('tr');
                    row.hide();
                    j.ajax({
                        type: "POST",
                        url: "../ContentMenagement/users.aspx/BatchDelete",
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
