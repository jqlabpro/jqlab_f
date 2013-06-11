<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masterpage/admin.Master" AutoEventWireup="true" CodeBehind="summary.aspx.cs" Inherits="icebear_v2.ContentMenagement.summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="girisalani">
        <div class="welcome1">
            <h5>Hoşgeldiniz, mağazanızı yönetmek için bir menü seçin </h5>
        </div>
        <a href="../EC/Siparis.aspx">
            <div class="boxes">
                <img src="../images/icons/siparisler.png" width="120" height="120" />
            </div>
        </a><a href="#">
            <div class="boxes">
                <img src="../images/icons/kampanyalar.png" width="120" height="120" />
            </div>
        </a><a href="../ContentMenagement/kategoriler.aspx">
            <div class="boxes">
                <img src="../images/icons/kategoriler.png" width="120" height="120" />
            </div>
        </a><a href="../EC/marka.aspx">
            <div class="boxes">
                <img src="../images/icons/markalar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/urunler.aspx">
            <div class="boxes">
                <img src="../images/icons/urunler.png" width="120" height="120" />
            </div>
        </a><a href="../EC/uyeler.aspx">
            <div class="boxes">
                <img src="../images/icons/uyeler.png" width="120" height="120" />
            </div>
        </a><a href="../EC/sayfalar.aspx">
            <div class="boxes">
                <img src="../images/icons/sayfalar.png" width="120" height="120" />
            </div>
        </a>
        <div style="clear: left"></div>
        <a href="../EC/kirilimlar.aspx">
            <div class="boxes">
                <img src="../images/icons/kirilimlar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/kargolar.aspx">
            <div class="boxes">
                <img src="../images/icons/kargolar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/kurlar.aspx">
            <div class="boxes">
                <img src="../images/icons/kurlar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/bankalar.aspx">
            <div class="boxes">
                <img src="../images/icons/bankalar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/ayarlar.aspx">
            <div class="boxes">
                <img src="../images/icons/ayarlar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/yorumlar.aspx">
            <div class="boxes">
                <img src="../images/icons/yorumlar.png" width="120" height="120" />
            </div>
        </a><a href="../EC/excel_yukleme.aspx">
            <div class="boxes">
                <img src="../images/icons/excel.png" width="120" height="120" />
            </div>
        </a>
        <div style="clear: left"></div>
    </div>
</asp:Content>
