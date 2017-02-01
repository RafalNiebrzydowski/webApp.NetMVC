var length_button = false;
$(document).ready(function () {
    $("#button4").click(function () {
        $("#linie").toggle('slide', {
            duration: 100,
            easing: 'easeOutBounce',
            direction: 'up'
        });
        if (length_button == false)
            LengthLine();

    });
});

function LengthLine() {
    var length;
    var addList = document.getElementById('linie');
    length_button = true;
    if (markers.length > 1) {
        if (markers.length > 2) {
            for (var i = 0, I = markers.length; i < I; i++) {

                if (i < markers.length - 1) {
                    var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat()*100000)/100000), (Math.round(markers[i].getPosition().lng()*100000)/100000));
                    var point2 = new google.maps.LatLng((Math.round(markers[i + 1].getPosition().lat()*100000)/100000), (Math.round(markers[i + 1].getPosition().lng()*100000)/100000));
                    length = google.maps.geometry.spherical.computeDistanceBetween(point1, point2);
                    var text = document.createElement('div');

                    text.id = 'Linia ' + (i + 1) + "-" + (i + 2);
                    var lengthline = (Math.round(length * 100000) / 100000);
                    if (lengthline < 10000) {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + lengthline + "&#160" + 'm' + "  class='form-control' disabled='disabled'></div>";
                    }
                    else {
                        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + (Math.round((lengthline / 1000.0) * 100000.0) / 100000.0) + "&#160" + 'km' + "  class='form-control' disabled='disabled'></div>";
                    }
                    addList.appendChild(text);
                }
                else {
                    var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat()*100000)/100000), (Math.round(markers[i].getPosition().lng()*100000)/100000));
                    var point2 = new google.maps.LatLng((Math.round(markers[0].getPosition().lat()*100000)/100000), (Math.round(markers[0].getPosition().lng()*100000)/100000));
                    length = google.maps.geometry.spherical.computeDistanceBetween(point1, point2);
                    var text = document.createElement('div');

                    text.id = 'Linia ' + (i + 1) + "-" + (1);
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
        else {
            var point1 = new google.maps.LatLng((Math.round(markers[0].getPosition().lat()*100000)/100000), (Math.round(markers[0].getPosition().lng()*100000)/100000));
            var point2 = new google.maps.LatLng((Math.round(markers[1].getPosition().lat()*100000)/100000), (Math.round(markers[1].getPosition().lng()*100000)/100000));
            var text = document.createElement('div');
            length = google.maps.geometry.spherical.computeDistanceBetween(point1, point2);
            text.id = 'Linia ' + (1) + "-" + (2);
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

function UpdateLengthLine() {
    if (length_button == true) {
        if (markers.length > 1) {
            for (var i = 0, I = markers.length + 1; i < I; i++) {
                if (i < markers.length) {
                    var linie = document.getElementById("Linia " + (i + 1) + "-" + (i + 2));
                    linie.parentNode.removeChild(linie);
                }
                else {
                    var linie = document.getElementById("Linia " + (i + 1) + "-" + (1));
                    linie.parentNode.removeChild(linie);
                }
            }
        }
        else if (markers.length <= 1 && markers.length > 0) {
            var linie = document.getElementById("Linia " + (1) + "-" + (2));
            linie.parentNode.removeChild(linie);
        }
    }
    LengthLine();
}
function UpdateDragedLengthLine() {
    if (length_button == true) {
        if (markers.length > 1) {
            for (var i = 0, I = markers.length; i < I; i++) {
                if (markers.length < 4) {
                    if (i < markers.length - 1) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (i + 2));
                        linie.parentNode.removeChild(linie);
                    }
                    else if (i == markers.length - 1) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (1));
                        linie.parentNode.removeChild(linie);
                    }
                }
                else {
                    if (i < markers.length - 1) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (i + 2));
                        linie.parentNode.removeChild(linie);
                    }
                    else {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (1));
                        linie.parentNode.removeChild(linie);
                    }
                }
            }
        }
    }
    LengthLine();
}
function UpdateNewLengthLine() {
    if (length_button == true) {

        if (markers.length - 1 > 1) {
            for (var i = 0, I = markers.length - 1; i < I; i++) {
                if (markers.length - 1 < 3) {
                    if (i < markers.length - 2) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (i + 2));
                        linie.parentNode.removeChild(linie);
                    }
                    else if (i > markers.length - 2) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (1));
                        linie.parentNode.removeChild(linie);
                    }
                }
                else {
                    if (i < markers.length - 2) {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (i + 2));
                        linie.parentNode.removeChild(linie);
                    }
                    else {
                        var linie = document.getElementById("Linia " + (i + 1) + "-" + (1));
                        linie.parentNode.removeChild(linie);
                    }
                }
            }
        }
        else if (markers.length - 2 == 1) {
            var linie = document.getElementById("Linia " + (1) + "-" + (2));
            linie.parentNode.removeChild(linie);
        }

    }

    LengthLine();
}