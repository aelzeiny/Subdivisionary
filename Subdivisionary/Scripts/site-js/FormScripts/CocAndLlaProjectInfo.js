$(document).ready(checkAllBoxes);

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

var landDevContactBoxes = [
    $("#DeveloperContactInfo_Name"),
    $("#DeveloperContactInfo_Email"),
    $("#DeveloperContactInfo_Phone"),
    $("#DeveloperContactInfo_AddressLine1"),
    $("#DeveloperContactInfo_AddressLine2"),
    $("#DeveloperContactInfo_City"),
    $("#DeveloperContactInfo_State"),
    $("#DeveloperContactInfo_Zip")
];

var checkbx = $("#OwnerAndLandDevContactAreSame");
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