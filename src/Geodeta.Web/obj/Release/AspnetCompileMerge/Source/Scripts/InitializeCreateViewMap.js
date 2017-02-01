
//Inicjalizacja mapy
function Initialize() {

    var postion = new google.maps.LatLng(51.476706, 0);

    map = new google.maps.Map(document.getElementById("map_canvas"), {
        zoom: 14,
        center: postion,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    var homeControlDiv = document.createElement('div');
    var homeControl = new HomeControl(homeControlDiv, map);

    homeControlDiv.index = 1;
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(homeControlDiv);
    poly = new google.maps.Polygon({
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#FF0000",
        fillOpacity: 0.35,
        map: map
    });

    poly.setMap(map);
    poly.setPaths(new google.maps.MVCArray([path]));

    google.maps.event.addListener(map, 'click', addPoint);
}



