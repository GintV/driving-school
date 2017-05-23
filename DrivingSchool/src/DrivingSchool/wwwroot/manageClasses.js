$(function () {
    function moveItems(origin, dest) {
        $(origin).find(':selected').appendTo(dest);
    }
   
    $('#left').click(function () {
        moveItems('#AvailableClasses', '#SelectedClasses');
    });

    $('#right').on('click', function () {
        moveItems('#SelectedClasses', '#AvailableClasses');
    });
});


$(document).ready(function () {
    $('select').removeAttr('multiple');
});