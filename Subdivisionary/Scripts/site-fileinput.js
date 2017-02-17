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