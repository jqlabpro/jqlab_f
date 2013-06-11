j(function () {

    j("a[rel='delete']").click(function () {
        var title = j(this).attr("title");
        var mesaj = j(this).attr("mesaj");
        var target = j(this).attr("href");

        var html = j("body").html();
        var cbox = "<div class='corfirm-bg'></div><div class='corfirm-box'><h5 style='file-size:12px; background-color:#720202; padding:2px; padding-left:5px; color:#FFF;'>Onay</h5><div class='mesaj'>" + mesaj + "</div><div class='ok btn14 mr5'>Tamam</div><div class='btn14 mr5 can'>İptal</div></div>";
        j("body").append(cbox);

        j(".can").click(function () {
            j(".corfirm-box").remove();
            j(".corfirm-bg").remove();
            return false;
        });
        j(".ok").click(function () {
            window.location = target;
        });

        try {
            var ekran_genis = j(window).width() / 2;
            var box_genis = j(".corfirm-box").width() / 2;
            ekran_genis = ekran_genis - box_genis;
            j(".corfirm-box").css("left", ekran_genis);

            var ekran_yuksek = j(window).height() / 2;
            var box_yuksek = j(".corfirm-box").height() / 2;
            ekran_yuksek = ekran_yuksek - box_yuksek;
            j(".corfirm-box").css("top", ekran_yuksek);
        } catch (e) {

        }

        return false;
    });


});