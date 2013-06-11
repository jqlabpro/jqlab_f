<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="YonetimPaneli.admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="http://fonts.googleapis.com/css?family=Cuprum" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/spinner/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="js/spinner/ui.spinner.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>

    <script type="text/javascript" src="js/fileManager/elfinder.min.js"></script>

    <script type="text/javascript" src="js/wysiwyg/jquery.wysiwyg.js"></script>
    <script type="text/javascript" src="js/wysiwyg/wysiwyg.image.js"></script>
    <script type="text/javascript" src="js/wysiwyg/wysiwyg.link.js"></script>
    <script type="text/javascript" src="js/wysiwyg/wysiwyg.table.js"></script>

    <script type="text/javascript" src="js/flot/jquery.flot.js"></script>
    <script type="text/javascript" src="js/flot/jquery.flot.pie.js"></script>
    <script type="text/javascript" src="js/flot/excanvas.min.js"></script>

    <script type="text/javascript" src="js/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/dataTables/colResizable.min.js"></script>

    <script type="text/javascript" src="js/forms/forms.js"></script>
    <script type="text/javascript" src="js/forms/autogrowtextarea.js"></script>
    <script type="text/javascript" src="js/forms/autotab.js"></script>
    <script type="text/javascript" src="js/forms/jquery.validationEngine-en.js"></script>
    <script type="text/javascript" src="js/forms/jquery.validationEngine.js"></script>

    <script type="text/javascript" src="js/colorPicker/colorpicker.js"></script>

    <script type="text/javascript" src="js/uploader/plupload.js"></script>
    <script type="text/javascript" src="js/uploader/plupload.html5.js"></script>
    <script type="text/javascript" src="js/uploader/plupload.html4.js"></script>
    <script type="text/javascript" src="js/uploader/jquery.plupload.queue.js"></script>

    <script type="text/javascript" src="js/ui/progress.js"></script>
    <script type="text/javascript" src="js/ui/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="js/ui/jquery.tipsy.js"></script>
    <script type="text/javascript" src="js/ui/jquery.alerts.js"></script>

    <script type="text/javascript" src="js/jBreadCrumb.1.1.js"></script>
    <script type="text/javascript" src="js/cal.min.js"></script>
    <script type="text/javascript" src="js/jquery.collapsible.min.js"></script>
    <script type="text/javascript" src="js/jquery.ToTop.js"></script>
    <script type="text/javascript" src="js/jquery.listnav.js"></script>
    <script type="text/javascript" src="js/jquery.sourcerer.js"></script>

    <script type="text/javascript" src="js/custom.js"></script>

</head>

<body>
    <form id="valid" class="mainForm" runat="server" clientidmode="Static">
        <!-- Top navigation bar -->
        <div id="topNav">
            <div class="fixed">
                <div class="wrapper">
                    <div class="userNav">
                        <ul>
                            <li><span></span></li>
                        </ul>
                    </div>
                    <div class="fix"></div>
                </div>
            </div>
        </div>
        <div class="nNote nFailure hideit" style="margin-top:-20px;" id="dv_not" runat="server">
            <p><strong>HATA: </strong>Kullanıcı Adı / Şifre Hatalı.</p>
        </div>
        <!-- Login form area -->
        <div class="loginWrapper">
            <div class="loginLogo">
                <img src="images/loginLogo.png" alt="" />
            </div>
            <div class="loginPanel">
                <div class="head">
                    <h5 class="iUser">Yönetim Paneli Girişi</h5>
                </div>
                <fieldset>
                    <div class="loginRow noborder">
                        <label for="req1">Kullanıcı Adı</label>
                        <div class="loginInput">
                            <asp:TextBox runat="server" CssClass="validate[required]" ClientIDMode="Static" ID="tx_kadi"></asp:TextBox>
                        </div>
                        <div class="fix"></div>
                    </div>

                    <div class="loginRow">
                        <label for="req2">Şifre</label>
                        <div class="loginInput">
                            <asp:TextBox runat="server" CssClass="validate[required]" TextMode="Password" ClientIDMode="Static" ID="tx_sifre"></asp:TextBox>
                        </div>
                        <div class="fix"></div>
                    </div>

                    <div class="loginRow">
                        <div class="rememberMe">
                            <asp:CheckBox ID="chk_hatirla" name="chbox" runat="server" Text="Beni Hatırla" />
                        </div>
                        <asp:Button ID="btn_giris" runat="server" Text="Giriş" OnClick="btn_login_Click" CssClass="greyishBtn submitForm" />
                        <div class="fix"></div>
                    </div>
                </fieldset>
            </div>
        </div>

        <!-- Footer -->
        <div id="footer">
            <div class="wrapper">
                <span></span>
            </div>
        </div>
    </form>
</body>
</html>
