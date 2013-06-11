/// <reference path="jquery-1.8.3.min.js" />
/// <reference path="http://code.jquery.com/ui/1.9.2/jquery-ui.js" />

var j = new jQuery.noConflict();

j(function () {
    var secilen = "";
    var id = j("#hd_id").val();


    GetPositions();
    function GetPositions() {
        var id = j("#hd_id").val();

        j.ajax({
            type: "POST",
            url: "../EC/fatura.aspx/GetPositions",
            data: "{id:" + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                j("#fatura").append(msg.d);

                j("#tx_x").val("0");
                j("#tx_y").val("0");

                //j("#fatura").children().removeAttr("style");

                drag_resize();
            },
            error: function (msgx) {
                //
            }
        });

        j.ajax({
            type: "POST",
            url: "../EC/fatura.aspx/GetPositionTextBox",
            data: "{id:" + id + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                j("#fatura").append(msg.d);
                drag_resize();

                //var size = j("textarea").size();
                //for (var i = 0; i < size; i++) {
                //    var genis = j("textarea :eq(" + i + ")").parent().parent().css("width");
                //    var yuksek = j("textarea :eq(" + i + ")").parent().parent().css("height");
                //    j("textarea :eq(" + i + ")").width(genis);
                //    j("textarea :eq(" + i + ")").height(yuksek);
                //}
            },
            error: function (msgx) {
                //
            }
        });


    }


    j("#btn_del").hide();

    j("#btn_del").click(function () {
        j("#" + secilen).remove();
        j("#btn_del").hide();
        j("#topbar input[type='text']").val("");

        j.ajax({
            type: "POST",
            url: "../EC/fatura.aspx/DelItem",
            data: "{itemname:'" + secilen + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                j("#spn_item").html("Silindi!");
            },
            error: function (msgx) {
                //
            }
        });

        return false;
    });

    j("#btn_add").click(function () {

        var adi = j("#tx_adi").val();
        secilen = adi;

        if (adi == "") {
            alert("Lütfen Bir Ad Girin");
            return false;
        }
        else {
            var kactane = j("#" + adi).size();
            if (kactane > 0) {
                alert("Lütfen Başka Bir Ad Girin");
                return false;
            }
        }

        var x = j("#tx_x").val();
        var y = j("#tx_y").val();
        var w = j("#tx_w").val();
        var h = j("#tx_h").val();
        var satir = j("#tx_satir");

        var html = j("#fatura").html();

        var parca = j("select").val();

        switch (parca) {
            case "Yazı Kutusu":
                {
                    html = "<div class='draggable' id='" + secilen + "'><textarea class='item' type='text'></textarea></div>" + html;
                    j("#fatura").html(html);

                    var h = j(this).css("height");
                    h = String(h).replace("px", "");
                    var w = j(this).css("width");
                    w = String(w).replace("px", "");

                    j("#tx_h").val(h);
                    j("#tx_w").val(w);

                    j("#tx_x").val("0");
                    j("#tx_y").val("0");

                    drag_resize();
                    break;
                }
            case "Ürün Listesi":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddUrunList",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });

                    break;
                }
            case "Adet Listesi":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddAdets",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Stok Kodu Listesi":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddUrunKodus",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Fiyat Listesi":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddFiyatListesi",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Tutar Listesi":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddTutarListesi",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "İl Kodu":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddIlKodu",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Seri No":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddSeriNo",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Satıcı Adres":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddSaticiAdres",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });
                    break;
                }
            case "Toplam Tutar Kutusu":
                {
                    j.ajax({
                        type: "POST",
                        url: "../EC/fatura.aspx/AddToplamTutar",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            html = "<div class='draggable' style='padding:10px;' id='" + secilen + "'>" + msg.d + "</div>" + html;
                            j("#fatura").html(html);

                            j("#tx_x").val("0");
                            j("#tx_y").val("0");

                            drag_resize();
                        },
                        error: function (msgx) {
                            //
                        }
                    });

                    break;
                }
        }

        //if (w != "")
        //    j(".item").css("width", w + "px");

        //if (h != "")
        //    j(".item").css("height", h + "px");

        //if (x != "")
        //    j(".item").css("margin-left", x + "px");

        //if (y != "")
        //    j(".item").css("margin-top", y + "px");


        j("#btn_del").show();

        return false;
    });

    j("select").change(function () {
        var secili = j(this).val();
        if (secili != "Yazı Kutusu") {
            j(".whide").fadeOut();
        }
        else {
            j(".whide").fadeIn();
        }
    });

    function SavePosition() {

        var x = j("#" + secilen).position().top;
        var y = j("#" + secilen).position().left;
        var w;
        var h;

        var satir = j("#tx_satir").val();
        var mode = j("select option:selected").text();

        if (mode == "Yazı Kutusu") {
            w = j("#" + secilen + " textarea").css("width");
            h = j("#" + secilen + " textarea").css("height");
        }
        else {
            w = j("#" + secilen).css("width");
            h = j("#" + secilen).css("height");
        }

        w = String(w).replace("px", "");
        h = String(h).replace("px", "");

        j.ajax({
            type: "POST",
            url: "../EC/fatura.aspx/AddOrUpdate",
            data: "{adi:'" + secilen + "',x:'" + x + "',y:'" + y + "',w:'" + w + "',h:'" + h + "',satirarasi:'" + satir + "',mode:'" + mode + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                html = "<div class='draggable' style='padding:10px;' id='" + adi + "'>" + msg.d + "</div>" + html;
                j("#fatura").html(html);
                drag_resize();
            },
            error: function (msgx) {
                //
            }
        });
    }

    function drag_resize() {
        j(".draggable").draggable({
            start: function () {
                j(this).css("opacity", 0.6);
                j("#spn_item").html(secilen);
            },
            stop: function () {
                j(this).css("opacity", 1);

                var x = j(this).css("top");
                x = String(x).replace("px", "");
                var y = j(this).css("left");
                y = String(y).replace("px", "");

                j("#tx_x").val(x);
                j("#tx_y").val(y);

                //secilen = j(this).attr("id");
                //j("#spn_item").html(secilen);

                if (secilen != "" && secilen != undefined)
                    SavePosition();
            }
        });
        j(".item").resizable({
            helper: "resizable-helper",
            autoHide : true,
            start: function () {
                j(this).css("opacity", 0.6);
            },
            create: function () {
                var kactane = j("textarea").size();
                for (var i = 0; i < kactane; i++) {
                    var w = j(this).parent().css("width");
                    var h = j(this).parent().css("height");

                    j(this).width(w);
                    j(this).height(h);

                    j(this).find("textarea").width(w);
                    j(this).find("textarea").height(h);
                }
            },
            stop: function () {
                j(this).css("opacity", 1);
                var h = j(this).css("height");
                h = String(h).replace("px", "");
                var w = j(this).css("width");
                w = String(w).replace("px", "");

                j("#tx_h").val(h);
                j("#tx_w").val(w);

                //secilen = j(this).parent().attr("id");
                //j("#spn_item").html(secilen);
                j("#btn_del").show();

                if (secilen != "" && secilen != undefined)
                    SavePosition();
            }
        });

        j("#tx_x").keyup(function () {
            var deger = j(this).val();
            j("#" + secilen).css("margin-left", deger + "px");
            drag_resize();
        });

        j("#tx_y").keyup(function () {
            var deger = j(this).val();
            j("#" + secilen).css("margin-top", deger + "px");
            drag_resize();
        });

        j("#tx_h").keyup(function () {
            var deger = j(this).val();
            j("#" + secilen).css("height", deger + "px");
            drag_resize();
        });

        j("#tx_w").keyup(function () {
            var deger = j(this).val();
            j("#" + secilen).css("width", deger + "px");
            drag_resize();
        });

        j("#tx_satir").keyup(function () {
            var deger = j(this).val();
            j("#" + secilen).css("line-height", deger + "px");
            SavePosition();
            return false;
        });

        j(".draggable").mousedown(function (e) {
            secilen = j(this).attr("id");
            j("#btn_del").show();
            j("#spn_item").html(secilen);
        });

        j("#btn_yazdir").click(function () {
            j(".item").resizable("destroy");
            j(".item").css("opacity", 1.0);
            j(".item").css("background-color", "transparent");
            j(".item").css("border", "none");
            window.print();
            return false;
        });

    }

});