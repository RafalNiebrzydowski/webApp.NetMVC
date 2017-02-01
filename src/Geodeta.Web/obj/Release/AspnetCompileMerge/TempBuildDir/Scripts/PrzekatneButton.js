function HomeControl(controlDiv, map) {
    var press = false;

    controlDiv.style.padding = '5px';

    var controlUI = document.createElement('div');
    controlUI.style.backgroundColor = 'white';
    controlUI.style.cursor = 'pointer';
    controlUI.style.textAlign = 'center';
    controlUI.title = 'Przyciśnij aby wyświetlić na mapie przekątne';
    controlDiv.appendChild(controlUI);

    var controlText = document.createElement('div');
    controlText.style.fontFamily = 'Arial,sans-serif';
    controlText.style.fontSize = '12px';
    controlText.style.paddingLeft = '4px';
    controlText.style.paddingRight = '4px';
    controlText.innerHTML = '<b>Pokaż przekątne</b>';
    controlUI.appendChild(controlText);

    google.maps.event.addDomListener(controlUI, 'click', function () {
        var lineSymbol = {
            path: 'M 0,-1 0,1',
            strokeOpacity: 1,
            strokeColor: '#FF0000',
            scale: 2
        };

        delete diagonal;
        if (markers.length > 2) {
            for (var i = 0; i < markers.length - 2; i++) {
                if (i == 0) {
                    for (var k = i + 2; k < markers.length - 1; k++) {
                        var flightPlanCoordinates = [markers[i].getPosition(), markers[k].getPosition()];
                        var flightPath = new google.maps.Polyline({
                            path: flightPlanCoordinates,
                            strokeOpacity: 0,
                            icons: [{
                                icon: lineSymbol,
                                offset: '0',
                                repeat: '12px'
                            }],

                        });
                        diagonal.push(flightPath);
                    }
                }
                if (i > 0) {
                    for (var k = i + 2; k < markers.length; k++) {
                        var flightPlanCoordinates = [markers[i].getPosition(), markers[k].getPosition()];
                        var flightPath = new google.maps.Polyline({
                            path: flightPlanCoordinates,
                            strokeOpacity: 0,
                            icons: [{
                                icon: lineSymbol,
                                offset: '0',
                                repeat: '12px'
                            }],

                        });
                        diagonal.push(flightPath);

                    }
                }
            }
        }
        if (press == false) {
            for (var i = 0; i < diagonal.length; i++)
                diagonal[i].setMap(map);
            press = true;
        }
        else {
            for (var i = 0; i < diagonal.length; i++)
                diagonal[i].setMap(null);
            press = false;
            diagonal = [];
        }
    });
}
    function getDiagonal() {
        if (press == true) {
            var lineSymbol = {
                path: 'M 0,-1 0,1',
                strokeOpacity: 1,
                strokeColor: '#FF0000',
                scale: 2
            };
            for (var a = 0; a < diagonal.length; a++)
            { diagonal[a].setMap(null); }
            diagonal = [];
            if (markers.length > 2) {
                for (var i = 0; i < markers.length - 2; i++) {
                    if (i == 0) {
                        for (var k = i + 2; k < markers.length - 1; k++) {
                            var flightPlanCoordinates = [markers[i].getPosition(), markers[k].getPosition()];
                            var flightPath = new google.maps.Polyline({
                                path: flightPlanCoordinates,
                                strokeOpacity: 0,
                                icons: [{
                                    icon: lineSymbol,
                                    offset: '0',
                                    repeat: '12px'
                                }],

                            });
                            diagonal.push(flightPath);
                        }
                    }
                    if (i > 0) {
                        for (var k = i + 2; k < markers.length; k++) {
                            var flightPlanCoordinates = [markers[i].getPosition(), markers[k].getPosition()];
                            var flightPath = new google.maps.Polyline({
                                path: flightPlanCoordinates,
                                strokeOpacity: 0,
                                icons: [{
                                    icon: lineSymbol,
                                    offset: '0',
                                    repeat: '12px'
                                }],

                            });
                            diagonal.push(flightPath);

                        }
                    }
                }
            }

            for (var i = 0; i < diagonal.length; i++)
            { diagonal[i].setMap(map); }

        }
    }
