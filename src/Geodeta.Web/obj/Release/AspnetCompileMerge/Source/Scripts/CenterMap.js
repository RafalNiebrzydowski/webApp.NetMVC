function centermap() {
    var x = 0;
    var y = 0;
    for (i = 0; i < markers.length; i++) {
        x += markers[i].getPosition().lat();

        y += markers[i].getPosition().lng();

    }
    x = x / markers.length;
    y = y / markers.length;
    var center = new google.maps.LatLng(x, y);
    map.setCenter(center);
}