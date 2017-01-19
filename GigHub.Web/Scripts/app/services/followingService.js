var followingService = (function () {

    var following = function (artistId, done, fail) {
        $.post("/api/followings", { followeeId: artistId })
            .done(done)
            .fail(fail);

    };

    var unFollow = function (artistId, done, fail) {
        $.ajax({
            url: "/api/followings",
            method: "DELETE",
            data: { followeeId: artistId }
        })
            .done(done)
            .fail(fail);
    };

    return {
        following: following,
        unFollow: unFollow
    };

}());
