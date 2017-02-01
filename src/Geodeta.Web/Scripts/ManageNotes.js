function EditNote() {

    for (var i = 0, I = markers.length; i < I; ++i) {
        if (markers[i].infobox.getMap() != null) {
            noteTitlePoint[i] = document.getElementById("tytul").value;
            noteContentPoint[i] = document.getElementById("tekst").value;
            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + document.getElementById("tytul").value + '</div><div style="float:right;"><button type="button" onClick="openModal(\'' + document.getElementById("tytul").value + '\',\'' + document.getElementById("tekst").value + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
           '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNote()" margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + document.getElementById("tekst").value + '</div>';
            markers[i].infobox.setContent(boxText);
        }
    }

    $('#EdycjaNotatki').modal('hide');


}
function DeleteNote() {

    for (var i = 0, I = markers.length; i < I; ++i) {
        if (markers[i].infobox.getMap() != null) {
            noteTitlePoint[i] = "";
            noteContentPoint[i] = "Brak notatki";
            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNote()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
            markers[i].infobox.setContent(boxText);
        }
    }
}
function CreateNote() {

    for (var i = 0, I = markers.length; i < I; ++i) {
        if (markers[i].infobox.getMap() != null) {

            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + document.getElementById("nowytytul").value + '</div><div style="float:right;"><button type="button" onClick="openModal(\'' + document.getElementById("nowytytul").value + '\',\'' + document.getElementById("nowytekst").value + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
          '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNote()" margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + document.getElementById("nowytekst").value + '</div>';
            noteTitlePoint[i] = document.getElementById("nowytytul").value;
            noteContentPoint[i] = document.getElementById("nowytekst").value;
            markers[i].infobox.setContent(boxText);
        }
    }
    $('#DodawanieNotatki').modal('hide');
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
function EditNoteLine() {

    for (var i = 0, I = lines.length; i < I; ++i) {
        if (lines[i].infobox.getMap() != null) {

            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + document.getElementById("tytulLine").value + '</div><div style="float:right;"><button type="button" onClick="openModalLine(\'' + document.getElementById("tytulLine").value + '\',\'' + document.getElementById("tekstLine").value + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
           '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNoteLine()" margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + document.getElementById("tekstLine").value + '</div>';
            lines[i].infobox.setContent(boxText);
            noteTitleLine[i] = document.getElementById("tytulLine").value;
            noteContentLine[i] = document.getElementById("tekstLine").value;

        }
    }

    $('#EdycjaNotatkiLinii').modal('hide');


}
function DeleteNoteLine() {


    for (var i = 0, I = lines.length; i < I; ++i) {
        if (lines[i].infobox.getMap() != null) {
            noteTitleLine[i] = "";
            noteContentLine[i] = "Brak notatki";
            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
            lines[i].infobox.setContent(boxText);
        }
    }
}
function CreateNoteLine() {

    for (var i = 0, I = lines.length; i < I; ++i) {
        if (lines[i].infobox.getMap() != null) {
            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + document.getElementById("nowytytulLine").value + '</div><div style="float:right;"><button type="button" onClick="openModalLine(\'' + document.getElementById("nowytytulLine").value + '\',\'' + document.getElementById("nowytekstLine").value + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
          '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNoteLine()" margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + document.getElementById("nowytekstLine").value + '</div>';

            lines[i].infobox.setContent(boxText);
            noteTitleLine[i] = document.getElementById("nowytytulLine").value;
            noteContentLine[i] = document.getElementById("nowytekstLine").value;
        }
    }
    $('#DodawanieNotatkiLinii').modal('hide');
}