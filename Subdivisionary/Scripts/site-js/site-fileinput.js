$(document).ready(function () {
    $(".file-loading").each(function () {
        var up = $(this);
        up.fileinput({
            theme: "fa",
            showUpload: true,
            uploadAsync: true,
            overwriteInitial: false,
            uploadUrl: up.data("callback"),
            maxFileSize: up.data("maxfilesize"),
            maxFileCount: up.data("maxfilecount"),
            allowedFileExtensions: up.data("extensions"),
            initialPreview: up.data("files"),
            initialPreviewAsData: true,
            initialPreviewFileType: 'image',
            initialPreviewConfig: up.data("initialconfigs")
        });
    });
});

$("#formSubmitButton").click(function(e) {
    e.preventDefault();
    console.log($(".fileinput-upload-button").length);
    if ($(".fileinput-upload-button").length === 0)
        $("#formPartialViewEditor").submit();
    else
        toastr.error("Please upload or delete all pending files prior to saving this form");
});