function onFormSubmit() {
    $.ajax({
        url: '/Obszar/GetPoints',
        type: 'GET',
        data: { id: id2 },
        dataType: 'json',
        success: function (data) {

            $.each(data, function (index, Point) {
                var x;
                if (index < 9) {
                    x = 2;
                }
                if (index >= 9 && index < 99) {
                    x = 4;
                }
                if (index >= 99) {
                    x = 6;
                }
                var postion2 = new google.maps.LatLng(Point.CoordinateX, Point.CoordinateY);
                path.insertAt(path.length, new google.maps.LatLng(Point.CoordinateX, Point.CoordinateY));
                var marker = new MarkerWithLabel({
                    position: postion2,
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
                    labelAnchor: new google.maps.Point(x, 6),
                    labelClass: "labels"
                });
                
                if (Point.NoteId == null) {
                    var boxText = '<div class="titleInfoBox"><div style="float:left"></div></div>' + '<div class="textInfoBox">' + "Brak notatki" + '</div>';
                    marker.infobox = new InfoBox({
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
                }
                else {

                    var note = getNote(Point.NoteId);
                    var boxText = '<div class="titleInfoBox">' + note.Title + '<div style="float:right"></div></div>' + '<div class="textInfoBox">' + note.ContentNote + '</div>';
                    marker.infobox = new InfoBox({
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

                }
                markers.push(marker);

                marker.setTitle("#" + path.length);
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