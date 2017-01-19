var followingsController = (function () {
    var button;
    var init = function () {
        $(".js-toggle-follow").click(toggleFollow);
    };

    var toggleFollow = function (e) {
        button = $(e.target);
        var artistId = button.attr("data-user-id");
        if (button.hasClass("btn-default")) {
            followingService.following(artistId, done, fail);
        } else {
            followingService.unFollow(artistId, done, fail);
        }

    };

    var done = function () {
        var text = (button.text() === "Following") ? "Follow ?" : "Following";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    };

}());