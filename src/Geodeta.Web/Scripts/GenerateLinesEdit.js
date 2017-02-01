function generateLines() {
    $.ajax({
        url: '/Obszar/GetLines',
        type: 'GET',
        data: { id: id2 },
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, Line) {
                noteid.push(Line.NoteId)
            }
        );
            for (var i = 0; i < markers.length; i++) {

                if (markers.length > 1) {

                    if (i < markers.length - 1) {
                        var pathline = [];
                        pathline.push(markers[i].getPosition());
                        pathline.push(markers[i + 1].getPosition());
                        var line = new google.maps.Polyline({
                            path: pathline,
                            geodesic: true,
                            strokeColor: '#FF0000',
                            strokeOpacity: 1,
                            strokeWeight: 2.2
                        });
                        if (noteid[i] != null) {
                            var note = getNote(noteid[i]);
                            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + note.Title + '</div><div style="float:right;"><button type="button" onClick="openModalLine(\'' + note.Title + '\',\'' + note.ContentNote + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
           '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNoteLine()" style="margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                            noteTitleLine.push(note.Title);
                            noteContentLine.push(note.ContentNote);
                        }
                        else {
                            noteTitleLine.push("");
                            noteContentLine.push("Brak notatki");
                            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                        }
                        line.infobox = new InfoBox({
                            content: boxText,

                            boxStyle: {
                                background: "#fff",
                                color: "#fff",
                                radius: "3px",
                                minWidth: "160px",
                                maxWidth: "300px",
                                WebkitBorderRadius: "3px"
                  , MozBorderRadius: "3px"
                  , borderRadius: "3px"
                            }

                        });

                        google.maps.event.addListener(line, 'click', function () {
                            for (h = 0; h < lines.length; h++) {
                                lines[h].infobox.close();
                            }
                            for (h = 0; h < markers.length; h++) {
                                markers[h].infobox.close();
                            }
                            this.infobox.open(map, midLatLng(this));

                        });
                        lines.push(line);
                    }
                    if (i == markers.length - 1) {

                        var pathline = [];
                        pathline.push(markers[i].getPosition());
                        pathline.push(markers[0].getPosition());
                        var line2 = new google.maps.Polyline({
                            path: pathline,
                            geodesic: true,
                            strokeColor: '#FF0000',
                            strokeOpacity: 1,
                            strokeWeight: 2.2
                        });
                        if (noteid[i] != null) {
                            var note = getNote(noteid[i]);
                            noteTitleLine.push(note.Title);
                            noteContentLine.push(note.ContentNote);
                            var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + note.Title + '</div><div style="float:right;"><button type="button" onClick="openModalLine(\'' + note.Title + '\',\'' + note.ContentNote + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
         '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNoteLine()" style="margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                        }
                        else {
                            noteTitleLine.push("");
                            noteContentLine.push("Brak notatki");
                            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                        }
                        line2.infobox = new InfoBox({
                            content: boxText,

                            boxStyle: {
                                background: "#fff",
                                color: "#fff",
                                radius: "3px",
                                minWidth: "160px",
                                maxWidth: "300px",
                                WebkitBorderRadius: "3px"
                  , MozBorderRadius: "3px"
                  , borderRadius: "3px"
                            }

                        });
                        google.maps.event.addListener(line2, 'click', function () {
                            for (h = 0; h < lines.length; h++) {
                                lines[h].infobox.close();
                            }
                            for (h = 0; h < markers.length; h++) {
                                markers[h].infobox.close();
                            }
                            line2.infobox.open(map, midLatLng(this));

                        });
                        lines.push(line2);
                    }


                }
            }
            for (var i = 0, I = lines.length; i < I; i++) {
                lines[i].setMap(map);
            }
            existline = true;
        }

    });
}