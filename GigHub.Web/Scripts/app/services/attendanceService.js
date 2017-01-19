var attendanceService = (function () {
    var createAttendance = function(gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function(gigId, done, fail) {
        $.ajax({
                //The first one is for default value ONLY  "int id" - ! "int gigId" do not work!  I used my var with an object.
                //                      url: "/api/attendances/"  + button.attr("data-gig-id"),
                url: "/api/attendances",
                method: "DELETE",
                data: { gigId: gigId }
            })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    };
}());