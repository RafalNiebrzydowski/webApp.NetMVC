$(document).ready(function () {
    $("#button1").click(function () {
        $("#PoleIObwod").toggle('slide', {
            duration: 100,
            easing: 'easeOutBounce',
            direction: 'up'
        });
      /*  var polytmp = new google.maps.MVCArray;
        for (var i = 0, I = markers.length; i < I; i++) {
            polytmp.push(new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000) / 100000), (Math.round(markers[i].getPosition().lng() * 100000) / 100000)));
        }*/
        var field = fieldarea(markers);
        var circuit = 0;

        for (var i = 0, I = markers.length; i < I; i++) {

            if (i < markers.length - 1) {
               // var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000) / 100000), (Math.round(markers[i].getPosition().lng() * 100000) / 100000));
                //var point2 = new google.maps.LatLng((Math.round(markers[i + 1].getPosition().lat() * 100000) / 100000), (Math.round(markers[i + 1].getPosition().lng() * 100000) / 100000));
                circuit = circuit + getDistanceFromLatLonInM((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0), (Math.round(markers[i + 1].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i + 1].getPosition().lng() * 100000.0) / 100000.0));

            }
            else {
                //var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000) / 100000), (Math.round(markers[i].getPosition().lng() * 100000) / 100000));
                //var point2 = new google.maps.LatLng((Math.round(markers[0].getPosition().lat() * 100000) / 100000), (Math.round(markers[0].getPosition().lng() * 100000) / 100000));
                circuit = circuit + getDistanceFromLatLonInM((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0), (Math.round(markers[0].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[0].getPosition().lng() * 100000.0) / 100000.0));
            }

        }
        
        if (field < 10000) {
            $("#fieldArea").val( field + " m" + "\u00B2");
        } else {
            $("#fieldArea").val((Math.round((field/ 1000.0) * 100000.0) / 100000.0) + " km" + "\u00B2");
        }
       // $("#fieldArea").val(field);
        if (circuit < 10000) {
            $("#circuitArea").val((Math.round(circuit * 100000.0) / 100000.0) + " m");
        } else {
            $("#circuitArea").val((Math.round(((Math.round(circuit * 100000.0) / 100000.0) / 1000.0) * 100000.0) / 100000.0) + " km");
        }

    });
});

function UpdateFieldAndCircuit() {
    for (var i = 0, I = poly.length; i < I; i++) {
        poly[i].getPosition.lat() = (Math.round(poly[i].getPosition.lat() * 100000.0) / 100000.0);
        poly[i].getPosition.lng() = (Math.round(poly[i].getPosition.lng() * 100000.0) / 100000.0);
    }
    var field = google.maps.geometry.spherical.computeArea(poly.getPath());
    var circuit = 0;
    if (markers.length > 2) {
        for (var i = 0, I = markers.length; i < I; i++) {

            if (i < markers.length - 1) {
                var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0));
                var point2 = new google.maps.LatLng((Math.round(markers[i + 1].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i + 1].getPosition().lng() * 100000.0) / 100000.0));
                circuit = circuit + getDistanceFromLatLonInM((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0), (Math.round(markers[i + 1].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i + 1].getPosition().lng() * 100000.0) / 100000.0));


            }
            else {
                var point1 = new google.maps.LatLng((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0));
                var point2 = new google.maps.LatLng((Math.round(markers[0].getPosition().lat() * 100000.0) / 100000), (Math.round(markers[0].getPosition().lng() * 100000.0) / 100000.0));
                circuit = circuit + getDistanceFromLatLonInM((Math.round(markers[i].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[i].getPosition().lng() * 100000.0) / 100000.0), (Math.round(markers[0].getPosition().lat() * 100000.0) / 100000.0), (Math.round(markers[0].getPosition().lng() * 100000.0) / 100000.0));

            }

        }
    }
    var tmpfield = fieldarea(markers);
    if (tmpfield < 10000) {
        document.getElementById("fieldArea").value = tmpfield + " m" + "\u00B2";
    } else {
        document.getElementById("fieldArea").value = (Math.round((tmpfield / 1000.0) * 100000.0) / 100000.0) + " km" + "\u00B2";
    }
    //document.getElementById("fieldArea").value = fieldarea(markers);
    if (circuit < 10000) {
        document.getElementById("circuitArea").value = ((Math.round(circuit * 100000.0) / 100000.0) + " m");
    } else {
        document.getElementById("circuitArea").value = ((Math.round(((Math.round(circuit * 100000.0) / 100000.0) / 1000.0) * 100000.0) / 100000.0) + " km");
    }
    //document.getElementById("circuitArea").value = (Math.round(circuit* 100000.0) / 100000.0);


}

function getDistanceFromLatLonInM(lat1,
			lon1, lat2, lon2) {
		var R = 6378137;
		var dLat = toRadians(lat2 - lat1);
var dLon = toRadians(lon2 - lon1);
var a = Math.sin(dLat / 2) * Math.sin(dLat / 2)
        + Math.cos(toRadians(lat1)) * Math.cos(toRadians(lat2))
        * Math.sin(dLon / 2) * Math.sin(dLon / 2);
var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
var d = R * c;
return (Math.round(d * 100000.0) / 100000.0);

}

var EARTH_RADIUS = 6378137;
function fieldarea(locations) {
    return calculateAreaOfGPSPolygonOnSphereInSquareMeters(locations,
            6378137);
}

function calculateAreaOfGPSPolygonOnSphereInSquareMeters(
        locations, radius) {
    if (locations.length < 3) {
        return 0;
    }

    var diameter = radius * 2;
    var circumference = diameter * Math.PI;
    var listY = [];
    var listX = [];
    var listArea = [];
    
    var latitudeRef = (Math.round(locations[0].getPosition().lat() * 100000.0) / 100000.0);
    var longitudeRef = (Math.round(locations[0].getPosition().lng() * 100000.0) / 100000.0);
    for (var i = 1; i < locations.length; i++) {
        var latitude = (Math.round(locations[i].getPosition().lat() * 100000.0) / 100000.0);
        var longitude = (Math.round(locations[i].getPosition().lng() * 100000.0) / 100000.0);
        var segmentX = (Math.round(((longitude - longitudeRef) * circumference * Math.cos(toRadians(latitude)) / 360.0) * 100000.0) / 100000.0);
        var segmentY = (Math.round(((latitude - latitudeRef) * circumference / 360.0) * 100000.0) / 100000.0);
        listY.push(segmentY);
        listX.push(segmentX);
    }

    for (var i = 1; i < listX.length; i++) {
    var x1 = listX[i - 1];
    var y1 = listY[i - 1];
    var x2 = listX[i];
    var y2 = listY[i];
    var areaInSquare = (Math.round(((y1 * x2 - x1 * y2) / 2) * 100000.0) / 100000.0);
    listArea.push(areaInSquare);
}

var areasSum = 0;
for (var i =0; i<listArea.length; i++) {
    areasSum = areasSum + listArea[i];
}
return (Math.round(Math.abs(areasSum) * 100000.0) / 100000.0); 
}

function toRadians(Value) {
    return Value * Math.PI / 180.0;
}