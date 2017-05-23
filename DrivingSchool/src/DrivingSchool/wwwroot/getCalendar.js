/* Calendar script */
$.getScript("../calendar.js", function () {
    //console.error(eventsData);

    $("#calendar").fullCalendar({
        header: {
            left: "prev,next today",
            center: "title",
            right: "agendaWeek,agendaDay"
        },
        editable: false,
        events: eventsData
    });
})