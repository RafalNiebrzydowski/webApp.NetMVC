var diagonal_button = false;
$(document).ready(function () {
    $("#button5").click(function () {
        $("#przekatne").toggle('slide', {
            duration: 100,
            easing: 'easeOutBounce',
            direction: 'up'
        });
        if (diagonal_button == false)
            calculateDiagonals();

    });
});





function calculateDiagonals() {
    var length;
    var addList = document.getElementById('przekatne');
    diagonal_button = true;
    if (markers.length > 2) {
        for (var i = 0; i < markers.length - 2; i++) {
            if (i == 0) {
                for (var k = i + 2; k < markers.length - 1; k++) {
                    var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000) / 100000), (Math.round(markers[i].getPosition().lng() * 100000) / 100000));
                    var point2 = new google.maps.LatLng((Math.round(markers[k].getPosition().lat() * 100000) / 100000), (Math.round(markers[k].getPosition().lng() * 100000) / 100000));
                    length = google.maps.geometry.spherical.computeDistanceBetween(point1, point2);

                    var text = document.createElement('div');

                    text.id = 'Przekątna ' + (i + 1) + "-" + (k + 1);
                    var lengthline = (Math.round(length * 100000) / 100000);
                    if (lengthline < 10000) {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + lengthline + "&#160" + 'm' + "  class='form-control' disabled='disabled'></div>";
                    }
                    else {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + (Math.round((lengthline / 1000.0) * 100000.0) / 100000.0) + "&#160" + 'km' + "  class='form-control' disabled='disabled'></div>";
                    }
                    addList.appendChild(text);
                }
            }
            if (i > 0) {
                for (var k = i + 2; k < markers.length; k++) {
                    var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000) / 100000), (Math.round(markers[i].getPosition().lng() * 100000) / 100000));
                    var point2 = new google.maps.LatLng((Math.round(markers[k].getPosition().lat() * 100000) / 100000), (Math.round(markers[k].getPosition().lng() * 100000) / 100000));
                    length = google.maps.geometry.spherical.computeDistanceBetween(point1, point2);
                    var text = document.createElement('div');

                    text.id = 'Przekątna ' + (i + 1) + "-" + (k + 1);
                    var lengthline = (Math.round(length * 100000) / 100000);
                    if (lengthline < 10000) {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + lengthline + "&#160" + 'm' + "  class='form-control' disabled='disabled'></div>";
                    }
                    else {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + (Math.round((lengthline / 1000.0) * 100000.0) / 100000.0) + "&#160" + 'km' + "  class='form-control' disabled='disabled'></div>";
                    }
                    addList.appendChild(text);
                }
            }
        }
    }

}

function UpdateDragedDiagonal() {
    if (diagonal_button == true) {
        if (markers.length > 2) {
            for (var i = 0; i < markers.length - 2; i++) {
                if (i == 0) {
                    for (var k = i + 2; k < markers.length - 1; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
                if (i > 0) {
                    for (var k = i + 2; k < markers.length; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
            }
        }
    }
    calculateDiagonals();
}
function UpdateNewDiagonal() {
    if (diagonal_button == true) {

        if (markers.length - 1 > 2) {
            for (var i = 0; i < markers.length - 3; i++) {
                if (i == 0) {
                    for (var k = i + 2; k < markers.length - 2; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
                if (i > 0) {
                    for (var k = i + 2; k < markers.length - 1; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
            }
        }

    }

    calculateDiagonals();
}
function UpdateDiagonal() {
    if (diagonal_button == true) {
        if (markers.length > 1) {
            for (var i = 0; i < markers.length - 1; i++) {
                if (i == 0) {
                    for (var k = i + 2; k < markers.length; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
                if (i > 0) {
                    for (var k = i + 2; k < markers.length + 1; k++) {
                        var linie = document.getElementById("Przekątna " + (i + 1) + "-" + (k + 1));
                        linie.parentNode.removeChild(linie);
                    }
                }
            }
        }
    }
    calculateDiagonals();
}