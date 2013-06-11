j(function () {

    var rl_urun_item = j("#h-menu .h_item[rel=rl_urun]");
    var position = rl_urun_item.position();
    var left_x = position.left;

    var rl_urun_subitem = j(".sub-items[rel=rl_urun]");
    rl_urun_subitem.css("margin-left", left_x - 14 + "px");
    rl_urun_subitem.css("margin-top", "-10px");
    rl_urun_subitem.hide();

    rl_urun_item.mouseenter(function () {
        j(this).removeClass("h_item");
        j(this).addClass("h_item_hover");
        rl_urun_subitem.show();
    });
    rl_urun_subitem.mouseenter(function () {
        rl_urun_item.removeClass("h_item");
        rl_urun_item.addClass("h_item_hover");
        rl_urun_subitem.show();
    });
    rl_urun_subitem.mouseleave(function () {
        rl_urun_item.addClass("h_item");
        rl_urun_item.removeClass("h_item_hover");
        rl_urun_subitem.hide();
    });
    rl_urun_item.mouseleave(function () {
        rl_urun_item.addClass("h_item");
        rl_urun_item.removeClass("h_item_hover");
        rl_urun_subitem.hide();
    });

    var rl_tanim_item = j("#h-menu .h_item[rel=rl_tanim]");
    position = rl_tanim_item.position();
    left_x = position.left;

    var rl_tanim_subitem = j(".sub-items[rel=rl_tanim]");
    rl_tanim_subitem.css("margin-left", left_x - 14 + "px");
    rl_tanim_subitem.css("margin-top", "-10px");
    rl_tanim_subitem.hide();

    rl_tanim_item.mouseenter(function () {
        j(this).removeClass("h_item");
        j(this).addClass("h_item_hover");
        rl_tanim_subitem.show();
    });
    rl_tanim_subitem.mouseenter(function () {
        rl_tanim_item.removeClass("h_item");
        rl_tanim_item.addClass("h_item_hover");
        rl_tanim_subitem.show();
    });
    rl_tanim_subitem.mouseleave(function () {
        rl_tanim_item.addClass("h_item");
        rl_tanim_item.removeClass("h_item_hover");
        rl_tanim_subitem.hide();
    });
    rl_tanim_item.mouseleave(function () {
        rl_tanim_item.addClass("h_item");
        rl_tanim_item.removeClass("h_item_hover");
        rl_tanim_subitem.hide();
    });

    var rl_icerik_item = j("#h-menu .h_item[rel=rl_icerik]");
    position = rl_icerik_item.position();
    left_x = position.left;

    var rl_icerik_subitem = j(".sub-items[rel=rl_icerik]");
    rl_icerik_subitem.css("margin-left", left_x - 14 + "px");
    rl_icerik_subitem.css("margin-top", "-10px");
    rl_icerik_subitem.hide();

    rl_icerik_item.mouseenter(function () {
        j(this).removeClass("h_item");
        j(this).addClass("h_item_hover");
        rl_icerik_subitem.show();
    });
    rl_icerik_subitem.mouseenter(function () {
        rl_icerik_item.removeClass("h_item");
        rl_icerik_item.addClass("h_item_hover");
        rl_icerik_subitem.show();
    });
    rl_icerik_subitem.mouseleave(function () {
        rl_icerik_item.addClass("h_item");
        rl_icerik_item.removeClass("h_item_hover");
        rl_icerik_subitem.hide();
    });
    rl_icerik_item.mouseleave(function () {
        rl_icerik_item.addClass("h_item");
        rl_icerik_item.removeClass("h_item_hover");
        rl_icerik_subitem.hide();
    });

});