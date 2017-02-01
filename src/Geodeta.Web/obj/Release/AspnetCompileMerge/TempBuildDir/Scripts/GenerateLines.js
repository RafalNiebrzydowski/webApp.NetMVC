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
                            var boxText = '<div class="titleInfoBox">' + note.Title + '<div style="float:right"></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                        }
                        else {
                            var boxText = '<div class="titleInfoBox"><div style="float:left"></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
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
                        var line = new google.maps.Polyline({
                            path: pathline,
                            geodesic: true,
                            strokeColor: '#FF0000',
                            strokeOpacity: 1,
                            strokeWeight: 2.2
                        });
                        if (noteid[i] != null) {
                            var note = getNote(noteid[i]);
                            var boxText = '<div class="titleInfoBox">' + note.Title + '<div style="float:right"></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                        }
                        else {
                            var boxText = '<div class="titleInfoBox"><div style="float:left"></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
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
                            line.infobox.open(map, midLatLng(this));

                        });
                        lines.push(line);
                    }


                }
            }
            for (var i = 0, I = lines.length; i < I; i++) {
                lines[i].setMap(map);
            }

        }

    });
}

