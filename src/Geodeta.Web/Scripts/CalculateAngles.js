var angle_button = false;
$(document).ready(function () {
    $("#button3").click(function () {
        $("#katy").toggle('slide', {
            duration: 100,
            easing: 'easeOutBounce',
            direction: 'up'
        });
        if (angle_button == false)
            addInputAngle();

    });
});

function getDifference(a1, a2) {
    al = (a1 > 0) ? a1 : 360 + a1;
    a2 = (a2 > 0) ? a2 : 360 + a2;
    var angle = Math.abs(a1 - a2) + 180;
    if (angle > 180) {
        angle = 360 - angle;
    }
    return Math.abs((Math.round(angle * 100000.0) / 100000.0));
}

function addInputAngle() {

    var ml = markers.length - 1;
    var angle = [];
    var second;
    if (markers.length > 2) {
        for (var i = 0, I = markers.length; i < I; i++) {
            if (i == 0) {
                var bearing1 = google.maps.geometry.spherical.computeHeading(markers[i].getPosition(), markers[i + 1].getPosition());
                var bearing2 = google.maps.geometry.spherical.computeHeading(markers[i + 1].getPosition(), markers[i + 2].getPosition());
                second = getDifference(bearing1, bearing2);

            }

            if (i == 1) {
                var bearing1 = google.maps.geometry.spherical.computeHeading(markers[i].getPosition(), markers[i - 1].getPosition());
                var bearing2 = google.maps.geometry.spherical.computeHeading(markers[i - 1].getPosition(), markers[i + 1].getPosition());
                bearing1 = (Math.round(bearing1 * 100000.0) / 100000.0);
                bearing2 = (Math.round(bearing2 * 100000.0) / 100000.0);
                angle.push(getDifference(bearing1, bearing2));
                angle.push(second);

            }

            if (i < ml && i > 1) {
                var bearing1 = google.maps.geometry.spherical.computeHeading(markers[i].getPosition(), markers[i - 1].getPosition());
                var bearing2 = google.maps.geometry.spherical.computeHeading(markers[i - 1].getPosition(), markers[i + 1].getPosition());
                bearing1 = (Math.round(bearing1 * 100000.0) / 100000.0);
                bearing2 = (Math.round(bearing2 * 100000.0) / 100000.0);
                angle.push(getDifference(bearing1, bearing2));


            }
            if (i == ml) {
                var bearing1 = google.maps.geometry.spherical.computeHeading(markers[0].getPosition(), markers[i].getPosition());
                var bearing2 = google.maps.geometry.spherical.computeHeading(markers[i].getPosition(), markers[i - 1].getPosition());
                bearing1 = (Math.round(bearing1 * 100000.0) / 100000.0);
                bearing2 = (Math.round(bearing2 * 100000.0) / 100000.0);
                angle.push(getDifference(bearing1, bearing2));

            }

        }
        var addList = document.getElementById('katy');

        angle_button = true;
        for (var i = 0, I = angle.length; i < I; i++) {

            var text = document.createElement('div');

            text.id = 'Kąt przy punkcie ' + (i + 1);
            text.innerHTML = "<label class='col-sm-13 control-label'  for='textinput'>" + text.id + "</label><div class='col-sm-3' ><input type='text' id='fd'" + "value=" + Math.round(angle[i]) + "\u00B0" + "  class='form-control' disabled='disabled'></div>";

            addList.appendChild(text);
        }
    }
    else {
        var addList2 = document.getElementById('katy');

        angle_button = true;
        for (var i = 0, I = markers.length; i < I; i++) {

            var text2 = document.createElement('div');

            text2.id = 'Kąt przy punkcie ' + (i + 1);
            text2.innerHTML = "<label class='col-sm-13 control-label' for='textinput'>" + text2.id + "</label><div class='col-sm-3' ><input type='text' id='fd'" + "value=" + 0 + "\u00B0" + "  class='form-control' disabled='disabled'></div>";

            addList2.appendChild(text2);
        }
    }
}

function updateLoadAngle() {
    if (angle_button == true) {
        for (var i = 0, I = markers.length + 1; i < I; i++) {
            var kat = document.getElementById("Kąt przy punkcie " + (i + 1));
            kat.parentNode.removeChild(kat);
        }
    }
    addInputAngle();

}
function updateAngle() {
    if (angle_button == true) {
        for (var i = 0, I = markers.length - 1; i < I; i++) {
            var kat = document.getElementById("Kąt przy punkcie " + (i + 1));
            kat.parentNode.removeChild(kat);
        }
    }
    addInputAngle();
}
function updateDragedAngle() {
    if (angle_button == true) {
        for (var i = 0, I = markers.length ; i < I; i++) {
            var kat = document.getElementById("Kąt przy punkcie " + (i + 1));
            kat.parentNode.removeChild(kat);
        }
    }
    addInputAngle();
}