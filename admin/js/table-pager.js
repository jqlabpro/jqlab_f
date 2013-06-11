j(function () {

    var dv_ileri = j("#dv_ileri");
    var dv_geri = j("#dv_geri");
    var dv_inner = j(".dv_inner");
    var page = j(".page");

    var say = 0;
    var genis = (page.size() - 2) / 10;
    genis = Math.round(genis);
    //if (page.size() % 10 != 0)
    //    genis++;

    dv_geri.click(function () {
        if (say > 0) {
            say--;
            slide("+=370px");
        } return false;
    });

    dv_ileri.click(function () {
        if (say < genis - 1) {
            say++;
            slide("-=370px");
        }
        return false;
    });

    function slide(yon) {
        dv_inner.animate({
            "margin-left": yon
        }, 1000, function () {
            //
        });
    }

    function calculate_genis() {

    }

});