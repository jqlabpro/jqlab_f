j(function () {
    j("#dv_ara").hide();
    j("#tx_arama").focus(function () {
        j("#dv_secenek").fadeIn("slow");
    });

    j("#tx_arama").click(function () {
        j("#dv_secenek").fadeIn("slow");
    });

    j("#dv_secenek").mouseleave(function () {
        j("#dv_secenek").hide();
    });

    j("#chk_secenek").change(function () {
        var secili_sayi = j("#chk_secenek :checked").size();
        if (secili_sayi > 0)
            j("#dv_ara").show();
        else
            j("#dv_ara").hide();
    });

    j("#tx_baslik").keyup(function () {
        var deger = j(this).val();

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/content.aspx/makeLink",
            data: "{deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                j("#tx_link").val(msg.d);
            },
            error: function (msgx) {
                j("#tx_link").val("hata");
            }
        });

    });

    var cat_value, cont_value, stok_adet;

    j(".txt_stok_adet").focus(function () {
        stok_adet = j(this).val();
    });
    j(".txt_stok_adet").blur(function () {
        var deger = j(this).val();
        var ids = j(this).attr("id");
        ids = String(ids).replace("txt_stok_adet", "");

        if (deger == stok_adet)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/EC/stok_hareket.aspx/ChangeAdet",
            data: "{ids:" + ids + ", deger:" + deger + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                j(this).val(msg.d);
            },
            error: function (msgx) {
                //j("#tx_link").val("hata");
            }
        });

    });

    j(".tx_cat").focus(function () {
        cat_value = j(this).val();
    });

    j(".tx_cat").blur(function () {
        var deger = j(this).val();
        var id = j(this).attr("id");

        if (deger == cat_value)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/category.aspx/ChangeName",
            data: "{ids:" + id + ", deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert("Güncelleme Başarılı.");
            },
            error: function (msgx) {
                //alert("Bir Hata Oluştu.");
                j(this).val(cat_value);
            }
        });

    });

    j(".tx_cont").focus(function () {
        cont_value = j(this).val();
    });

    j(".tx_cont").blur(function () {
        var deger = j(this).val();
        var id = j(this).attr("id");

        if (deger == cont_value)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/content.aspx/ChangeName",
            data: "{ids:" + id + ", deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert("Güncelleme Başarılı.");
            },
            error: function (msgx) {
                //alert("Bir Hata Oluştu.");
                j(this).val(cont_value);
            }
        });

    });

    j(".tx_u_name").focus(function () {
        cont_value = j(this).val();
    });

    j(".tx_u_name").blur(function () {
        var deger = j(this).val();
        var id = j(this).attr("id");
        id = String(id).replace("n", "");

        if (deger == cont_value)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/users.aspx/ChangeName",
            data: "{ids:" + id + ", deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert("Güncelleme Başarılı.");
            },
            error: function (msgx) {
                //alert("Bir Hata Oluştu.");
                j(this).val(cont_value);
            }
        });

    });

    j(".tx_u_mail").focus(function () {
        cont_value = j(this).val();
    });

    j(".tx_u_mail").blur(function () {
        var deger = j(this).val();
        var id = j(this).attr("id");
        id = String(id).replace("m", "");

        if (deger == cont_value)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/users.aspx/ChangeMail",
            data: "{ids:" + id + ", deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert("Güncelleme Başarılı.");
            },
            error: function (msgx) {
                //alert("Bir Hata Oluştu.");
                j(this).val(cont_value);
            }
        });

    });

    j(".tx_u_uname").focus(function () {
        cont_value = j(this).val();
    });

    j(".tx_u_uname").blur(function () {
        var deger = j(this).val();
        var id = j(this).attr("id");
        id = String(id).replace("u", "");

        if (deger == cont_value)
            return false;

        WaitBox("Güncelleniyor...", time);

        j.ajax({
            type: "POST",
            url: "/ContentMenagement/users.aspx/ChangeUserName",
            data: "{ids:" + id + ", deger:'" + deger + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //alert("Güncelleme Başarılı.");
            },
            error: function (msgx) {
                //alert("Bir Hata Oluştu.");
                j(this).val(cont_value);
            }
        });

    });

    function WaitBox(mesaj, time) {
        var ekran_genis = j(window).width() / 2;
        var box_genis = j(".waitbox").width() / 2;
        var ekran_genis = ekran_genis - box_genis;
        j(".waitbox").css("left", ekran_genis);

        var ekran_yuksek = j(window).height() / 2;
        var box_yuksek = j(".waitbox").height() / 2;
        var ekran_yuksek = ekran_yuksek - box_yuksek;
        j(".waitbox").css("top", ekran_yuksek);

        j("body").append("<div class='corfirm-bg'></div><div class='waitbox'>" + mesaj + "</div>");
        setInterval("j('.corfirm-bg').remove();j('.waitbox').remove();", time);
    }

    j(".content-images").mouseenter(function () {
        var id = j(this).attr("id");
        j("#" + id + " .delete-image").show();
    });
    j(".content-images").mouseleave(function () {
        var id = j(this).attr("id");
        j("#" + id + " .delete-image").hide();
    });

    j(".content-images").mouseenter(function () {
        var id = j(this).attr("id");
        j("#" + id + " .edit-image").show();
    });
    j(".content-images").mouseleave(function () {
        var id = j(this).attr("id");
        j("#" + id + " .edit-image").hide();
    });


});