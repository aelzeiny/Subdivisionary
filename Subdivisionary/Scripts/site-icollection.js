function replaceAll(str, find, replace) {
    return str.replace(new RegExp(escapeRegExp(find), "g"), replace);
}

function escapeRegExp(str) {
    return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

$("#formPartialViewEditor").on("click", ".add-entry", function () {
    var btn = $(this); // Clicked Button
    var key = btn.data("for"); // Property Key
    var container = $("#" + key); // Collection Container

    var entry = btn.data("entry"); // Partial View 
    var currentNum = container.children().length;

    // We replace the class & name properties w/i the entry before adding to the container for Model binding purposes    
    var replaceC = btn.data("replaceclass");
    var replaceN = btn.data("replacename");
    entry = replaceAll(entry, replaceC, replaceC + key + currentNum);
    entry = replaceAll(entry, replaceN, replaceN + key + currentNum);

    // now we append it to the container
    container.append(entry);
});