
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
        text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='fd'" + "value=" + (Math.round(markers[i].getPosition().lat() * 100000) / 100000) + "  class='form-control' disabled='disabled'><input type='text' id='fd'" + "value=" + (Math.round(markers[i].getPosition().lng() * 100000) / 100000) + "  class='form-control' disabled='disabled'></div>";

        addList.appendChild(text);
    }

}

function addSingleInputLatLng() {
    var addList = document.getElementById('wspolrzedne');
    var text = document.createElement('div');
    text.id = 'Punkt ' + markers.length;
    text.innerHTML = "<label class='col-sm-2 control-label' for='textinput'>" + text.id + "</label><div class='col-sm-4'><input type='text' id='" + text.id + "x'" + "value=" + (Math.round(markers[markers.length - 1].getPosition().lat() * 100000) / 100000) + "  class='form-control' disabled='disabled'><input type='text' id='" + text.id + "y'" + "value=" + (Math.round(markers[markers.length - 1].getPosition().lng() * 100000) / 100000) + "  class='form-control' disabled='disabled'></div>";

    addList.appendChild(text);


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