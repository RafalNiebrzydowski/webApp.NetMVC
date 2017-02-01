var dragended;
function LoadPoints() {
    $.ajax({
        url: '/Obszar/GetPoints',
        type: 'GET',
        data: { id: id2 },
        dataType: 'json',
        success: function (data) {

            $.each(data, function (index, Point) {
                var coorX;
                if (index < 9) {
                    coorX = 2;
                }
                if (index >= 9 && index < 99) {
                    coorX = 4;
                }
                if (index >= 99) {
                    coorX = 6;
                }
                var postion2 = new google.maps.LatLng(Point.CoordinateX, Point.CoordinateY);
                path.insertAt(path.length, new google.maps.LatLng(Point.CoordinateX, Point.CoordinateY));
                var marker = new MarkerWithLabel({
                    position: postion2,
                    draggable: true,
                    raiseOnDrag: false,
                    map: map,
                    icon: {
                        path: google.maps.SymbolPath.CIRCLE,
                        scale: 7,
                        strokeColor: "#FF0000",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#FFFFFF",
                        fillOpacity: 1
                    },
                    labelContent: "" + (index + 1),
                    labelAnchor: new google.maps.Point(coorX, 6),
                    labelClass: "labels"
                });



                markers.push(marker);
                marker.setTitle("#" + path.length);
                if (Point.NoteId == null) {
                    noteTitlePoint.push("");
                    noteContentPoint.push("Brak notatki");
                    var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNote(\'' + Point.Id + '\')" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                    marker.infobox = new InfoBox({
                        content: boxText,

                        boxStyle: {
                            background: "#fff",
                            color: "#fff",
                            radius: "3px",
                            maxWidth: "300px",
                            WebkitBorderRadius: "3px"
              , MozBorderRadius: "3px"
              , borderRadius: "3px"
                        }

                    });
                }
                else {

                    var note = getNote(Point.NoteId);
                    noteTitlePoint.push(note.Title);
                    noteContentPoint.push(note.ContentNote);
                    var boxText = '<div class="titleInfoBox"><div style="float:left;max-width:180px;">' + note.Title + '</div><div style="float:right;"><button type="button" onClick="openModal(\'' + note.Title + '\',\'' + note.ContentNote + '\')" class="btn btn-default btn-circle" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-edit"></i></button>' +
            '<button type="button" class="btn btn-default btn-circle" onClick="DeleteNote()" style="margin-right:5px"><i class="glyphicon glyphicon-remove"></i></button></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                    marker.infobox = new InfoBox({
                        content: boxText,

                        boxStyle: {
                            background: "#fff",
                            color: "#fff",
                            radius: "3px",
                            minWidth: "200px",
                            maxWidth: "300px",
                            WebkitBorderRadius: "3px"
              , MozBorderRadius: "3px"
              , borderRadius: "3px"
                        }

                    });

                }
                var boxTextLine = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';

                notes.push(markers.length - 1);
                notes.push(Point.NoteId);
                google.maps.event.addListener(marker, 'rightclick', function () {
                    marker.setMap(null);
                    for (var i = 0, I = markers.length; i < I && markers[i] != marker; ++i);
                    markers.splice(i, 1);
                    for (var k = 0; k < markers.length; k++) {
                        markers[k].labelContent = "" + (k + 1);
                        markers[k].label.setContent();
                    }
                    path.removeAt(i);
                    UpdateFieldAndCircuit();
                    updatePoint();
                    getDiagonal();
                    updateLoadAngle();
                    UpdateLengthLine();
                    UpdateDiagonal();
                    noteTitlePoint.splice(i, 1);
                    noteContentPoint.splice(i, 1);
                    if (lines.length > 1) {
                        if (i > 0) {
                            lines[i].setMap(null);
                            lines[i - 1].setMap(null);
                            lines.splice(i - 1, 2);
                            noteTitleLine.splice(i - 1, 2);
                            noteContentLine.splice(i - 1, 2);
                            var l = lines.length + 1;
                            if (i == (l)) {
                                var p = [markers[markers.length - 1].getPosition(),
                                                        markers[0].getPosition()
                                ];
                                var line = new google.maps.Polyline({
                                    path: p,
                                    geodesic: true,
                                    strokeColor: '#FF0000',
                                    strokeOpacity: 1,
                                    strokeWeight: 2.2
                                });
                                line.infobox = new InfoBox({
                                    content: boxTextLine,

                                    boxStyle: {
                                        background: "#fff",
                                        color: "#fff",
                                        radius: "3px",
                                        minWidth: "150px",
                                        maxWidth: "300px",
                                        WebkitBorderRadius: "3px"
                          , MozBorderRadius: "3px"
                          , borderRadius: "3px"
                                    }

                                });
                                google.maps.event.addListener(line, 'click', function () {

                                    line.infobox.open(map, midLatLng(this));

                                });
                                lines.push(line);
                                lines[lines.length - 1].setMap(map);
                                noteTitleLine.push("");
                                noteContentLine.push("Brak notatki");
                            }
                            if (i < (l)) {

                                var p = [markers[i - 1].getPosition(),
                                                        markers[i].getPosition()
                                ];
                                var line = new google.maps.Polyline({
                                    path: p,
                                    geodesic: true,
                                    strokeColor: '#FF0000',
                                    strokeOpacity: 1,
                                    strokeWeight: 2.2
                                });

                                line.infobox = new InfoBox({
                                    content: boxTextLine,

                                    boxStyle: {
                                        background: "#fff",
                                        color: "#fff",
                                        radius: "3px",
                                        minWidth: "150px",
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
                                lines.splice(i - 1, 0, line);
                                lines[i - 1].setMap(map);

                                noteTitleLine.splice(i - 1, 0, "");
                                noteContentLine.splice(i - 1, 0, "Brak notatki");
                            }



                        }
                        if (i == 0) {
                            lines[i].setMap(null);
                            lines[lines.length - 1].setMap(null);
                            lines.splice(lines.length - 1, 1);
                            lines.splice(i, 1);
                            noteTitleLine.splice(lines.length - 1, 1);
                            noteTitleLine.splice(i, 1);
                            noteContentLine.splice(lines.length - 1, 1);
                            noteContentLine.splice(i, 1);
                            var p = [markers[0].getPosition(),
                                markers[markers.length - 1].getPosition()
                            ];
                            var line = new google.maps.Polyline({
                                path: p,
                                geodesic: true,
                                strokeColor: '#FF0000',
                                strokeOpacity: 1,
                                strokeWeight: 2.2
                            });
                            line.infobox = new InfoBox({
                                content: boxTextLine,

                                boxStyle: {
                                    background: "#fff",
                                    color: "#fff",
                                    radius: "3px",
                                    minWidth: "150px",
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
                            lines[lines.length - 1].setMap(map);
                            noteTitleLine.push("");
                            noteContentLine.push("Brak notatki");
                        }

                    }
                    if (lines.length == 1) {

                        lines[0].setMap(null);

                        lines.splice(0, 1);
                        noteTitleLine.splice(0, 1);
                        noteContentLine.splice(0, 1);
                        existline = false;
                    }

                }
        );

                google.maps.event.addListener(marker, 'dragend', function (e) {
                    var target = e.target || e.srcElement;
                    if (target && target.className == 'labels') {
                        dragended = true;
                    }
                    for (var i = 0, I = markers.length; i < I && markers[i] != marker; ++i);
                    path.setAt(i, marker.getPosition());
                    UpdateFieldAndCircuit();
                    if (wspol_button == true) {
                        document.getElementById("Punkt " + (i + 1) + "x").value = marker.getPosition().lat();
                        document.getElementById("Punkt " + (i + 1) + "y").value = marker.getPosition().lng();
                    }
                    updateDragedAngle();
                    UpdateDragedLengthLine();
                    getDiagonal();
                    UpdateDragedDiagonal();
                    if (lines.length > 1) {
                        if (i > 0) {
                            if (linesremove == true) { i = i - 1; }
                            var pathline;
                            var pathline2;
                            pathline = lines[i].getPath().getAt(1);
                            pathline2 = lines[i - 1].getPath().getAt(0);
                            var path2 = [marker.getPosition(), pathline];
                            var path3 = [pathline2, marker.getPosition()];
                            lines[i].setPath(path2);
                            lines[i - 1].setPath(path3);
                        }
                        if (i == 0) {
                            var pathline;
                            var pathline2;
                            pathline = lines[i].getPath().getAt(1);
                            pathline2 = lines[markers.length - 1].getPath().getAt(0);
                            var path2 = [marker.getPosition(), pathline];
                            var path3 = [pathline2, marker.getPosition()];
                            lines[i].setPath(path2);
                            lines[markers.length - 1].setPath(path3);
                        }
                    }
                    if (lines.length == 1) {
                        var pathline;
                        if (i == 0) {
                            pathline = lines[0].getPath().getAt(1);
                            var path2 = [marker.getPosition(), pathline];
                        }
                        if (i == 1) {
                            pathline = lines[0].getPath().getAt(0);
                            var path2 = [pathline, marker.getPosition()];
                        }
                        lines[0].setPath(path2);
                    }
                }
                );
                google.maps.event.addListener(marker, 'click', function () {
                    for (h = 0; h < lines.length; h++) {
                        lines[h].infobox.close();
                    }
                    for (h = 0; h < markers.length; h++) {
                        markers[h].infobox.close();
                    }
                    marker.infobox.open(map, this);
                });

            });
            centermap();
            generateLines();
        }
    });

}

function addPoint(event) {
    if (!dragended) {
        var coorX;
        if ((markers.length + 2) < 9) {
            coorX = 2;
        }
        if ((markers.length + 2) >= 9 && (markers.length + 2) < 99) {
            coorX = 4;
        }
        if ((markers.length + 2) >= 99) {
            coorX = 6;
        }
        path.insertAt(path.length, event.latLng);
        var marker = new MarkerWithLabel({
            position: event.latLng,
            map: map,
            draggable: true,
            raiseOnDrag: false,
            icon: {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 7,
                strokeColor: "#FF0000",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#FFFFFF",
                fillOpacity: 1
            },
            labelContent: "" + (markers.length + 1),
            labelAnchor: new google.maps.Point(coorX, 6),
            labelClass: "labels"
        });
        var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNote()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
        var boxTextLine = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';

        marker.infobox = new InfoBox({
            content: boxText,

            boxStyle: {
                background: "#60646D",
                color: "#fff",
                radius: "3px",
                maxWidth: "300px",
                WebkitBorderRadius: "3px"
    , MozBorderRadius: "3px"
    , borderRadius: "3px"
            }

        });
        markers.push(marker);
        marker.setTitle("#" + path.length);
        addSingleInputLatLng();
        UpdateFieldAndCircuit();
        updateAngle();
        UpdateNewLengthLine();
        UpdateNewDiagonal();
        getDiagonal();
        noteTitlePoint.push("");
        noteContentPoint.push("Brak notatki");
        google.maps.event.addListener(marker, 'rightclick', function () {
            marker.setMap(null);
            for (var i = 0, I = markers.length; i < I && markers[i] != marker; ++i);
            markers.splice(i, 1);
            for (var k = 0; k < markers.length; k++) {
                markers[k].labelContent = "" + (k + 1);
                markers[k].label.setContent();
            }
            path.removeAt(i);
            updatePoint();
            updateLoadAngle();
            UpdateFieldAndCircuit();
            UpdateLengthLine();
            UpdateDiagonal();
            getDiagonal();
            noteTitlePoint.splice(i, 1);
            noteContentPoint.splice(i, 1);
            if (lines.length > 1) {
                if (i > 0) {
                    lines[i].setMap(null);
                    lines[i - 1].setMap(null);
                    lines.splice(i - 1, 2);
                    noteTitleLine.splice(i - 1, 2);
                    noteContentLine.splice(i - 1, 2);
                    var l = lines.length + 1;
                    if (i == (l)) {
                        var p = [markers[markers.length - 1].getPosition(),
                                                markers[0].getPosition()
                        ];
                        var line = new google.maps.Polyline({
                            path: p,
                            geodesic: true,
                            strokeColor: '#FF0000',
                            strokeOpacity: 1,
                            strokeWeight: 2.2
                        });
                        line.infobox = new InfoBox({
                            content: boxTextLine,

                            boxStyle: {
                                background: "#fff",
                                color: "#fff",
                                radius: "3px",
                                minWidth: "150px",
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
                        lines[lines.length - 1].setMap(map);
                        noteTitleLine.push("");
                        noteContentLine.push("Brak notatki");
                    }
                    if (i < (l)) {

                        var p = [markers[i - 1].getPosition(),
                                                markers[i].getPosition()
                        ];
                        var line = new google.maps.Polyline({
                            path: p,
                            geodesic: true,
                            strokeColor: '#FF0000',
                            strokeOpacity: 1,
                            strokeWeight: 2.2
                        });

                        line.infobox = new InfoBox({
                            content: boxTextLine,

                            boxStyle: {
                                background: "#fff",
                                color: "#fff",
                                radius: "3px",
                                minWidth: "150px",
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
                        lines.splice(i - 1, 0, line);
                        lines[i - 1].setMap(map);

                        noteTitleLine.splice(i - 1, 0, "");
                        noteContentLine.splice(i - 1, 0, "Brak notatki");
                    }



                }
                if (i == 0) {
                    lines[i].setMap(null);
                    lines[lines.length - 1].setMap(null);
                    lines.splice(lines.length - 1, 1);
                    lines.splice(i, 1);
                    noteTitleLine.splice(lines.length - 1, 1);
                    noteTitleLine.splice(i, 1);
                    noteContentLine.splice(lines.length - 1, 1);
                    noteContentLine.splice(i, 1);
                    var p = [markers[0].getPosition(),
                        markers[markers.length - 1].getPosition()
                    ];
                    var line = new google.maps.Polyline({
                        path: p,
                        geodesic: true,
                        strokeColor: '#FF0000',
                        strokeOpacity: 1,
                        strokeWeight: 2.2
                    });
                    line.infobox = new InfoBox({
                        content: boxTextLine,

                        boxStyle: {
                            background: "#fff",
                            color: "#fff",
                            radius: "3px",
                            minWidth: "150px",
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
                    lines[lines.length - 1].setMap(map);
                    noteTitleLine.push("");
                    noteContentLine.push("Brak notatki");
                }

            }
            if (lines.length == 1) {

                lines[0].setMap(null);

                lines.splice(0, 1);
                noteTitleLine.splice(0, 1);
                noteContentLine.splice(0, 1);
                existline = false;
            }
        }
        );

        google.maps.event.addListener(marker, 'dragend', function (e) {
            var target = e.target || e.srcElement;
            if (target && target.className == 'labels') {
                dragended = true;
            }
            for (var i = 0, I = markers.length; i < I && markers[i] != marker; ++i);
            path.setAt(i, marker.getPosition());
            UpdateFieldAndCircuit();
            updateDragedAngle();
            document.getElementById("Punkt " + (i + 1) + "x").value = marker.getPosition().lat();
            document.getElementById("Punkt " + (i + 1) + "y").value = marker.getPosition().lng();
            UpdateDragedLengthLine();
            UpdateDragedDiagonal();
            getDiagonal();
            if (lines.length > 1) {
                if (i > 0) {
                    if (linesremove == true) { i = i - 1; }
                    var pathline;
                    var pathline2;
                    pathline = lines[i].getPath().getAt(1);
                    pathline2 = lines[i - 1].getPath().getAt(0);
                    var path2 = [marker.getPosition(), pathline];
                    var path3 = [pathline2, marker.getPosition()];
                    lines[i].setPath(path2);
                    lines[i - 1].setPath(path3);
                }
                if (i == 0) {
                    var pathline;
                    var pathline2;
                    pathline = lines[i].getPath().getAt(1);
                    pathline2 = lines[markers.length - 1].getPath().getAt(0);
                    var path2 = [marker.getPosition(), pathline];
                    var path3 = [pathline2, marker.getPosition()];
                    lines[i].setPath(path2);
                    lines[markers.length - 1].setPath(path3);
                }
            }
            if (lines.length == 1) {
                var pathline;
                if (i == 0) {
                    pathline = lines[0].getPath().getAt(1);
                    var path2 = [marker.getPosition(), pathline];
                }
                if (i == 1) {
                    pathline = lines[0].getPath().getAt(0);
                    var path2 = [pathline, marker.getPosition()];
                }
                lines[0].setPath(path2);
            }

        }
        );
        google.maps.event.addListener(marker, 'click', function () {
            for (h = 0; h < lines.length; h++) {
                lines[h].infobox.close();
            }
            for (h = 0; h < markers.length; h++) {
                markers[h].infobox.close();
            }
            marker.infobox.open(map, this);

        });
        //Dodawanie linii


        if (existline == false) {

            if (markers.length > 1) {
                for (var i = 0, I = markers.length; i < I && markers[i] != marker; i++) {
                    if (i < markers.length - 1) {
                        if (i > 0) {
                            var pathline = [];
                            pathline.push(markers[i - 1].getPosition());
                            pathline.push(markers[i].getPosition());

                            var line = new google.maps.Polyline({
                                path: pathline,
                                geodesic: true,
                                strokeColor: '#FF0000',
                                strokeOpacity: 1,
                                strokeWeight: 2.2
                            });
                            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                            line.infobox = new InfoBox({
                                content: boxText,

                                boxStyle: {
                                    background: "#fff",
                                    color: "#fff",
                                    radius: "3px",
                                    minWidth: "150px",
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
                            noteTitleLine.push("");
                            noteContentLine.push("Brak notatki");

                        }
                        if (i == 0) {
                            var pathline = [];
                            pathline.push(markers[0].getPosition());
                            pathline.push(markers[1].getPosition());

                            var line = new google.maps.Polyline({
                                path: pathline,
                                geodesic: true,
                                strokeColor: '#FF0000',
                                strokeOpacity: 1,
                                strokeWeight: 2.2
                            });
                            var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                            line.infobox = new InfoBox({
                                content: boxText,

                                boxStyle: {
                                    background: "#fff",
                                    color: "#fff",
                                    radius: "3px",
                                    minWidth: "150px",
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
                            noteTitleLine.push("");
                            noteContentLine.push("Brak notatki");

                        }
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
                        var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                        line.infobox = new InfoBox({
                            content: boxText,

                            boxStyle: {
                                background: "#fff",
                                color: "#fff",
                                radius: "3px",
                                minWidth: "150px",
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
                        noteTitleLine.push("");
                        noteContentLine.push("Brak notatki");


                    }

                }
                for (var i = 0, I = lines.length; i < I; i++) {
                    lines[i].setMap(map);
                }
                existline = true;
            }
        }
        else {
            if (markers.length > 1) {
                if (lines.length > 1) {
                    var pathline1 = [];
                    pathline1.push(markers[markers.length - 2].getPosition());
                    pathline1.push(markers[markers.length - 1].getPosition());
                    if (linesremove == false)
                        lines[lines.length - 1].setPath(pathline1);
                    else {
                        lines[lines.length - 2].setPath(pathline1);
                        linesremove = false;
                    }
                }
                if (lines.length == 1) {
                    var pathline1 = [];
                    pathline1.push(markers[markers.length - 2].getPosition());
                    pathline1.push(markers[markers.length - 1].getPosition());
                    var line = new google.maps.Polyline({
                        path: pathline1,
                        geodesic: true,
                        strokeColor: '#FF0000',
                        strokeOpacity: 1,
                        strokeWeight: 2.2
                    });
                    var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                    line.infobox = new InfoBox({
                        content: boxText,

                        boxStyle: {
                            background: "#fff",
                            color: "#fff",
                            radius: "3px",
                            minWidth: "150px",
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
                    lines[lines.length - 1].setMap(map);
                    noteTitleLine.push("");
                    noteContentLine.push("Brak notatki");
                }

                var pathline2 = [];
                pathline2.push(markers[markers.length - 1].getPosition());
                pathline2.push(markers[0].getPosition());

                var line = new google.maps.Polyline({
                    path: pathline2,
                    geodesic: true,
                    strokeColor: '#FF0000',
                    strokeOpacity: 1,
                    strokeWeight: 2.2
                });
                var boxText = '<div class="titleInfoBox"><div style="float:left"><button type="button" class="btn btn-default btn-circle" onClick="openModalAddNoteLine()" style="margin:left:5px;margin-right:5px"><i class="glyphicon glyphicon-plus"></i></button></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                line.infobox = new InfoBox({
                    content: boxText,

                    boxStyle: {
                        background: "#fff",
                        color: "#fff",
                        radius: "3px",
                        minWidth: "150px",
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
                lines[lines.length - 1].setMap(map);
                noteTitleLine.push("");
                noteContentLine.push("Brak notatki");

            }
        }
    }
    dragended = false;
}

function updatePoint() {
    if (wspol_button == true) {
        for (var i = 0, I = markers.length + 1; i < I; i++) {
            var wspolrzedna = document.getElementById("Punkt " + (i + 1));
            wspolrzedna.parentNode.removeChild(wspolrzedna);
        }
    }
    addInputLatLng();


}

function createPoint(id2) {


    var pointsX = new Array();
    var pointsY = new Array();
    for (var i = 0, I = markers.length; i < I; i++) {
        var x1 = markers[i].getPosition().lat();
        var y1 = markers[i].getPosition().lng();
        pointsX.push(x1);
        pointsY.push(y1);
    }

    $.ajax({
        url: '/Obszar/CreateListOfPoints',
        type: 'POST',
        dataType: 'json',
        traditional: true,
        data: { id3: id2, x: pointsX, y: pointsY, notetitlepoint: noteTitlePoint, notecontentpoint: noteContentPoint, notetitleline: noteTitleLine, notecontentline: noteContentLine }
    });
}

function RemoveListOfPoints(id2) {

    $.ajax({
        url: '/Obszar/RemoveListOfPoints',
        type: 'POST',
        data: { id3: id2 }
    });


}
function createListOfPoint() {
    var tmp = 1;
    var s = markers.length - 1;

    for (var i = markers.length - 1; i >= 0; i--) {


        if (i > 0) {
            $.ajax({
                url: '/Obszar/ListOfPointCreate',
                type: 'POST',
                data: { a: i, b: i - 1 }
            });

        }
        else if (i == 0) {
            $.ajax({
                url: '/Obszar/ListOfPointCreate',
                type: 'POST',
                data: { a: i, b: s }
            });
        }
        tmp++;
    }

}


var wspol_button = false;
$(document).ready(function () {
    $("#button2").click(function () {
        $("#wspolrzedne").toggle('slide', {
            duration: 100,
            easing: 'easeOutBounce',
            direction: 'up'
        });
        if (wspol_button == false)
            addInputLatLng();

    });
});


function addInputLatLng() {

    var addList = document.getElementById('wspolrzedne');

    wspol_button = true;
    for (var i = 0, I = markers.length; i < I; i++) {

        var text = document.createElement('div');

        text.id = 'Punkt ' + (i + 1);
        text.innerHTML = "<div id='wspol'><label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='" + text.id + "x'" + "value=" + markers[i].getPosition().lat() + "  class='form-control' disabled='disabled'><input type='text' id='" + text.id + "y'" + "value=" + markers[i].getPosition().lng() + "  class='form-control' disabled='disabled'></div></div>";

        addList.appendChild(text);
    }

}
function addSingleInputLatLng() {
    var addList = document.getElementById('wspolrzedne');
    var text = document.createElement('div');
    text.id = 'Punkt ' + markers.length;
    text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='" + text.id + "x'" + "value=" + markers[markers.length - 1].getPosition().lat() + "  class='form-control' disabled='disabled'><input type='text' id='" + text.id + "y'" + "value=" + markers[markers.length - 1].getPosition().lng() + "  class='form-control' disabled='disabled'></div>";

    addList.appendChild(text);


}
