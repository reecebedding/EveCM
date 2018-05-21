$(document).ready(function () {
    $(window).outerWidth() > 992 && $("nav.side-navbar").mCustomScrollbar({
        scrollInertia: 200
    });
    $("#toggle-btn").on("click", function (e) {
        e.preventDefault(), $(window).outerWidth() > 1194 ? ($("nav.side-navbar").toggleClass("shrink"), $(".page").toggleClass("active")) : ($("nav.side-navbar").toggleClass("show-sm"), $(".page").toggleClass("active-sm"))
    }), $(".form-validate").each(function () {
        $(this).validate({
            errorElement: "div",
            errorClass: "is-invalid",
            validClass: "is-valid",
            ignore: ":hidden:not(.summernote),.note-editable.card-block",
            errorPlacement: function (e, t) {
                e.addClass("invalid-feedback"), "checkbox" === t.prop("type") ? e.insertAfter(t.siblings("label")) : e.insertAfter(t)
            }
        })
    });
    var e = $("input.input-material");
    e.filter(function () {
        return "" !== $(this).val()
    }).siblings(".label-material").addClass("active"), e.on("focus", function () {
        $(this).siblings(".label-material").addClass("active")
    }), e.on("blur", function () {
        $(this).siblings(".label-material").removeClass("active"), "" !== $(this).val() ? $(this).siblings(".label-material").addClass("active") : $(this).siblings(".label-material").removeClass("active")
    });
    var t = $("link#theme-stylesheet");
    $("<link id='new-stylesheet' rel='stylesheet'>").insertAfter(t);
    var a = $("link#new-stylesheet");
});