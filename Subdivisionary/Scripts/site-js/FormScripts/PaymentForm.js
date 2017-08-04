$("#PaidWithChecks").change(paidChecked);
$(document).ready(paidChecked);

function paidChecked() {
    if (document.getElementById("PaidWithChecks").checked) {
        $('#uncheckDivId :input').attr('disabled', true);
        $('#uncheckDivId').css("opacity", "0.4");
        $('#checkDivId :input').removeAttr('disabled');
        $('#checkDivId').css("opacity", "1");
        $("#formSubmitButton").removeAttr('disabled');
    } else {
        $('#uncheckDivId :input').removeAttr('disabled');
        $('#uncheckDivId').css("opacity", "1.00");
        $('#checkDivId :input').attr('disabled', true);
        $('#checkDivId').css("opacity", "0.4");
        $("#formSubmitButton").attr('disabled', true);
    }
}

$("#verifyBtn").click(function () {
    var me = $(this);
    $("#spinIcon").addClass("fa-spin");
    me.prop("disabled", true);
    var url = me.data("url");
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (!data.success) {
                me.prop("disabled", false);
                toastr.error("Online Payment could not be verified. If you have already paid the invoice online, then please try again in a little while.");
            }
            else if (data.paid) {
                toastr.success("Online Payment Verification Complete! Click the save button below.");
                $("#onlineVerified").css("visibility", "visible");
                $("#formSubmitButton").removeAttr('disabled');
            }
            else if (data.voided)
                toastr.warning("This invoice has been voided.");
            $("#spinIcon").removeClass("fa-spin");
        },
        error: function () {
            $("#spinIcon").removeClass("fa-spin");
            me.prop("disabled", false);
            toastr.error("Online Payment could not be verified. Please try again in a little while");
        }
    });
});