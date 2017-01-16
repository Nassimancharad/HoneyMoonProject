$(document).ready(function () {
    $(".dressItem").each(function (i, obj) {
        var $obj = $(obj);
        var $clone = $obj.clone();
        var position = $obj.position();
        $clone.addClass("clonedItem");

        var text = $('.extra', $clone);

        var heightGrowing = 25;
        var widthGrowing = 50;
        var height = $obj.height();
        var width = $obj.width();
        console.log("height: " + height);
        console.log("width: " + width);

        $(obj).bind("mouseenter", function (e) {
            $(".dressItem").css("z-index", 1);
            $clone
                .css("top", position.top)
                .css("left", position.left)
                .css("z-index", 1000)
                .css("border", "1px solid #EAEAEA")
                .css("background-color", "white")
                .css("box-shadow", "0px 0px 5px #EAEAEA");
                
            text.css('display', 'inherit');
            $clone.stop();
            $clone.appendTo(".dresses").css("position", "absolute").animate({
                    height: "+=" + heightGrowing + "px",
                    width: "+=" + widthGrowing + "px",
                    top: "-=" + (heightGrowing + 1) + "px",
                    left: "-=" + (widthGrowing / 2 - 5) + "px",
                    paddingTop: heightGrowing,
                    paddingLeft: widthGrowing / 2,
                    paddingRight: widthGrowing / 2
                },
                0);

        }); // end mouseover
        $clone.mouseleave(function (e) {
            text.css('display', 'none');
            $clone.stop();
            $clone.animate({
                height: "-=" + heightGrowing + "px",
                width: "-=" + widthGrowing + "px",
                top: "+=" + (heightGrowing - 1) + "px",
                left: "+=" + (widthGrowing / 2 + 5) + "px",
                padding: 0

            }, 0, function () { $clone.detach(); });

        });
    }); // end each
});