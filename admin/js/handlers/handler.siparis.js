j(function () {

    var time = 3000;
    var ids, ttutar, tkargo,durum;

    j(".change_val").click(function () {
        ids = j(this).attr("id");

        ttutar = j("#tt" + ids).val();
        tkargo = j("#tk" + ids).val();
        durum = j("#se" + ids + " :selected").val();

        jQuery.jGrowl("İşlem Yapılıyor...");

        j.ajax({
            type: "POST",
            url: "../EC/siparis.aspx/ChangeValue",
            data: "{ids:" + ids + ", tt:" + ttutar + ", kt:" + tkargo + ",sip:"+ durum +"}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //j(this).val(msg.d);
                var b_tt = String(msg.d).split("-")[0];
                var b_kt = String(msg.d).split("-")[1];

                j("#b_tt").html(" " + b_tt + " TL");
                j("#b_kt").html(" " + b_kt + " TL");

                jQuery.jGrowl("Güncelleme Başarılı...", { life: 3000 });
            },
            error: function (msgx) {
                //alert("olmadı");
            }
        });

        return false;
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
});