var checkbxes = $(".disable-boxes");
$("#formPartialViewEditor").on("change", "input.disable-boxes", function () {
    checkTheseBoxes($(this));
});

$(document).ready(function () {
    checkbxes.each(function (index) {
        checkTheseBoxes($(this));
    });
});


function checkTheseBoxes(checkbx) {
    var div = checkbx.attr("for");
    var shouldReverse = checkbx.hasClass("disabled-reverse");
    var inputs = $("#" + div + " :input");
    var divJ = $("#" + div);
    if (checkbx.prop("checked") && !shouldReverse) {
        inputs.prop("disabled", false);
        divJ.css("opacity", "1");
    }
    else {
        inputs.attr("disabled", true);
        divJ.css("opacity", ".6");
    }
}