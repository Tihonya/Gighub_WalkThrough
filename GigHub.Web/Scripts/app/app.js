var attendanceService = (function() {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);
    }

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            //The first one is for default value ONLY  "int id" - ! "int gigId" do not work!  I used my var with an object.
            //                      url: "/api/attendances/"  + button.attr("data-gig-id"),
            url: "/api/attendances",
            method: "DELETE",
            data: { gigId: gigId }
        })
                .done(done)
                .fail(fail);
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}());

var gigsController = (function () {

    var button;
   
    var init = function() {
        $(".js-toggle-attendance").click(toggleAttendance);
    }

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, done, fail);
        } else {
            attendanceService.deleteAttendance(gigId, done, fail);
        }
    };


    var done = function () {
        var text = (button.text() === "Going") ? "Going ?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

   var fail = function() {
        alert("Something failed");
    };


    return {
        init: init
    }

}());


