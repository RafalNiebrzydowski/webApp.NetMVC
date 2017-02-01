function midLatLng(line) {

    var marker = new google.maps.Marker({
        position: new google.maps.LatLng((line.getPath().getAt(0).lat() + line.getPath().getAt(1).lat()) / 2, (line.getPath().getAt(0).lng() + line.getPath().getAt(1).lng()) / 2)
    });
    return marker;
}
function getNote(noteId) {
    var content;
    $.ajax({
        url: '/Obszar/GetNote',
        type: 'GET',
        data: { id: noteId },
        dataType: 'json',
        async: false,
        success: function (data) {
            content = data;

        }

    });
    return content;
}
