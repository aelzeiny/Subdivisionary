function disableSignature(mSig, mUpBtn) {
    mSig.addClass("disabled");
    var vm = mSig.data("vm");
    mSig.children(".datestamp")
        .html("<strong>Signed: </strong>" + vm.DateStamp + "<strong>Username: </strong>" + vm.UserStamp);
    mUpBtn.css("visibility", "hidden");
}
function enableSignature(mSig, mUpBtn) {
    mSig.removeClass("disabled");
    mUpBtn.css("visibility", "visible");
}

function disableButton(mBtn) {
    mBtn.addClass("disabled");
    mBtn.prop("disabled", "true");
}

function enableButton(mBtn) {
    mBtn.removeAttr("disabled");
    mBtn.removeClass("disabled");
}

var allSigs = $(".bsmSignature");
var mForm = $("#formPartialViewEditor");
function allSignaturesDisabled() {
    allSigs.each(function() {
        if (!$(this).hasClass("disabled")) {
            return false;
        }
    });
    return true;
}

function saveSignature(sig, mCallback) {
    var dta = sig.jSignature("getData", "base30");
    if (!dta[1]) {
        toastr.error("Please sign document before uploading.");
        return false;
    }
    var vm = sig.data("vm");

    var url = sig.data("url");
    var upBtn = $(sig.data("uploadbtn"));
    disableButton(upBtn);

    vm.SerializationType = dta[0];
    vm.SignatureData = dta[1];

    bootbox.confirm({
        title: "Signature Confirmation",
        size: "medium",
        message: "By clicking the button labeled 'Accept' below, you agree to the terms and conditions of this Agreement" +
            " and acknowledge that you have read and understand the disclosures provided above." +
            " Paul, this is your area of expertice. I've only read stuff about UETA & E-SIGN act briefly. Thank you!",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Decline'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Accept'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: vm
                })
                    .done(function (data) {
                        disableSignature(sig, upBtn);
                        sig.prop("data-vm", data);
                        toastr.success("Document Signature Completed");
                        rslt = true;
                    })
                    .fail(function (xhr, textStatus, errorThrown) {
                        toastr.error("Something went wrong! Try refreshing the page & re-signing.");
                    });
            } else {
                toastr.error("Electronic signatures are not valid per the E-sign and Uniform Electronic Transactions Act" +
                    " unless the user accepts the stated agreement. Signature not saved.");
            }
            enableButton(upBtn);
            mCallback(result);
        }
    });
}

$(document).ready(function () {
    allSigs.each(function() {
        var self = $(this);
        self.jSignature();
        var vm = self.data("vm");
        if (vm.SignatureData) {
            var list = [vm.SerializationType, vm.SignatureData];
            self.jSignature("setData", "data:" + list.join(","));
            disableSignature(self, $(self.data("uploadbtn")));
        }
    });

    mForm.on("click", ".js-clear", function () {
        var sig = $($(this).data("for"));
        if (sig.hasClass("disabled")) {
            var deleteUrl = sig.data("deleteurl");
            var self = $(this);
            disableButton(self);
            bootbox.confirm({
                title: "Delete Signature Confirmation",
                size: "small",
                message:
                    "Are you sure you want to clear this signature box?",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Decline'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Accept'
                    }
                },
                callback: function(result) {
                    if (result) {
                        $.ajax({
                                url: deleteUrl,
                                type: "POST",
                                data: sig.data("vm")
                            })
                            .done(function(data) {
                                enableSignature(sig, $(sig.data("uploadbtn")));
                                sig.jSignature("clear");
                                toastr
                                    .success("Document Signature Removed. Please resign document before submitting application");
                            })
                            .fail(function(xhr, textStatus, errorThrown) {
                                toastr
                                    .error("Something went wrong! Try refreshing the page.");
                            });
                    }
                    enableButton(self);
                }
            });
        } else {
            sig.jSignature("clear");
        }
    });

    mForm.on("click", ".js-upload", function () {
        var sig = $($(this).data("for"));
        saveSignature(sig, function(rslt) {});
    });

    $("#formSubmitButton").click(function (e) {
        e.preventDefault();

        if (allSignaturesDisabled()) {
            mForm.submit();
        } else {
            // Validate all signatures submitted before saving
            allSigs.each(function () {
                var self = $(this);
                if (!self.hasClass("disabled")) {
                    // Attempt to save signatures
                    saveSignature(self, function (rslt) {
                        if (allSignaturesDisabled()) {
                            mForm.submit();
                        }
                    });
                }
            });
        }
    });
});