function getUrl(propertyValue) {
    var url = propertyValue.substr(5, propertyValue.length - 7);
    return url;
}

var $selected = null;

function selectThumbnail($thumb) {
    if ($selected != null)
        $selected.fadeTo("slow", 0.5);
    $selected = $thumb;
    $selected.stop();
    $selected.fadeTo("slow", 1.0);
    var $fullImage = $("#fullDressImage");
    var thumbImg = getUrl($selected.css("background-image"));
    $fullImage.attr("src", thumbImg);
}

$(document).ready(function () {
    $thumbnails = $(".dressThumbnail");
    $thumbnails.fadeTo(0, 0.5);
    $thumbnails.mouseenter(function () {
        $(this).stop();
        $(this).fadeTo("slow", 1.0);
    });
    $thumbnails.mouseleave(function () {
        if (!$(this).is($selected)) {
            $(this).stop();
            $(this).fadeTo("slow", 0.5);
        }
    });
    $thumbnails.click(function () {
        selectThumbnail($(this));
    });
    if ($thumbnails.length !== 0)
        selectThumbnail($thumbnails.first());
});