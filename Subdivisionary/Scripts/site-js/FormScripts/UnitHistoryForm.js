
var selectTxt = "";
$(document).ready(function () {
    var vm = $("#usablenames").data("nameslist");
    console.log(vm);

    for (var i = -1; i < vm.length; i++) {
        if (i < 0)
            selectTxt += "<option>{Select one of the following names}</option>";
        else {
            selectTxt += "<option value=\"" +
                vm[i].Name +
                "\"" +
                "data-istenant=\"" +
                vm[i].IsTenant +
                "\">" +
                vm[i].Name +
                " - " +
                (vm[i].IsTenant ? "Tenant" : "Owner") +
                "</option>";
        }
    }
    updateAllLists();
});

var container = $("#occupantId");
container.on("entry:add", updateAllLists);
container.on("change",
    ".js-nameoptions",
    function(e) {
        var me = $(this);
        var selected = me.find(":selected").data("istenant");
        selected = (selected) ? selected : "False";
        $(me.parent("div").parent("div")).children(".isTenant").prop("value", selected);
    });

function updateAllLists() {
    $(".js-nameoptions")
        .each(function() {
            updateList($(this));
        });
}

function updateList(me) {
    if (me.children().length === 0) {
        me.append(selectTxt);
        var prop = me.attr("defaultval");
        if(prop)
            me.val(prop);
    }
}