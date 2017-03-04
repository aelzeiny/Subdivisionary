var primaryContactBoxes = [
    $("#PrimaryContactInfo_Name"),
    $("#PrimaryContactInfo_Email"),
    $("#PrimaryContactInfo_Phone"),
    $("#PrimaryContactInfo_AddressLine1"),
    $("#PrimaryContactInfo_AddressLine2"),
    $("#PrimaryContactInfo_City"),
    $("#PrimaryContactInfo_State"),
    $("#PrimaryContactInfo_Zip")
];
var ownerContactBoxes = [
    $("#OwnerContactInfo_Name"),
    $("#OwnerContactInfo_Email"),
    $("#OwnerContactInfo_Phone"),
    $("#OwnerContactInfo_AddressLine1"),
    $("#OwnerContactInfo_AddressLine2"),
    $("#OwnerContactInfo_City"),
    $("#OwnerContactInfo_State"),
    $("#OwnerContactInfo_Zip")
];

var checkbx = $("#OwnerAndPrimaryContactAreSame");
checkbx.change(checkTheseBoxes);

$(document).ready(checkTheseBoxes);

function checkTheseBoxes() {
    if (checkbx.prop("checked")) {
        for (var i = 0; i < primaryContactBoxes.length; i++) {
            ownerContactBoxes[i].addClass("disabled");
            ownerContactBoxes[i].prop("disabled", true);
        }
    }
    else {
        for (var i = 0; i < primaryContactBoxes.length; i++) {
            ownerContactBoxes[i].removeClass("disabled");
            ownerContactBoxes[i].prop("disabled", false);
        }
    }
}