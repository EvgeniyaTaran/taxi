(function () {
	var ui = {

	}

	var geocoderFrom;
	var geocoderTo;
	var map;
	var autocompleteFrom;
	var autocompleteTo;
	function initialize() {
		var interval;
		try {
			if (google) {
				geocoderFrom = new google.maps.Geocoder();
				geocoderTo = new google.maps.Geocoder();
				var latlng = new google.maps.LatLng(50.27009, 30.31180);
				var mapOptions = {
					zoom: 8,
					center: latlng
				}
				map = new google.maps.Map(document.getElementById("map"), mapOptions);

				var inputFrom = document.getElementById("AddressFrom");
				var inputTo = document.getElementById("AddressTo");
				var options = { types: ["geocode"] }
				autocompleteFrom = new google.maps.places.Autocomplete(inputFrom, options);
				autocompleteTo = new google.maps.places.Autocomplete(inputTo, options);

				document.getElementById("AddressFrom").addEventListener("change", function () { codeAddressFrom() });
				document.getElementById("AddressTo").addEventListener("change", function () { codeAddressTo() });

				$("#map")
					.css({
						width: "100%",
						height: $(window).height() - 150
					});
			} else {
				interval = setTimeout(function () {
					clearTimeout(interval);
					initialize();
				}, 500);
			}
		} catch (error) {
			interval = setTimeout(function () {
				clearTimeout(interval);
				initialize();
			}, 500);
		}
	}

	function codeAddressFrom() {
		var time = setTimeout(function () {
			var addressFrom = document.getElementById("AddressFrom").value;
			geocoderFrom.geocode({ 'address': addressFrom }, function (results, status) {
				if (status == google.maps.GeocoderStatus.OK) {
					map.setCenter(results[0].geometry.location);
					var marker = new google.maps.Marker({
						map: map,
						position: results[0].geometry.location
					});
					var fromLat = marker.position.lat();
					var fromLng = marker.position.lng();
					$("#GeoCoordinatesFromLat").val(fromLat);
					$("#GeoCoordinatesFromLng").val(fromLng);
				} else {
					console.log("Geocode for start point was not successful for the following reason: " + status);
				}
			});
			clearTimeout(time);
		}, 100);

	}
	function codeAddressTo() {
		var time2 = setTimeout(function () {
			var addressTo = document.getElementById("AddressTo").value;
			geocoderTo.geocode({ 'address': addressTo }, function (results, status) {
				if (status == google.maps.GeocoderStatus.OK) {
					var marker2 = new google.maps.Marker({
						map: map,
						position: results[0].geometry.location
					});
					var toLat = marker2.position.lat();
					var toLng = marker2.position.lng();
					$("#GeoCoordinatesToLat").val(toLat);
					$("#GeoCoordinatesToLng").val(toLng);
				} else {
					console.log("Geocode for end point was not successful for the following reason: " + status);
				}
			});
			clearTimeout((time2));
		});
	}


	$(document).ready(function () {
		initialize();
		//$("#AddressFrom").on("keypress", function(e) {
		//    var item = $(e.currentTarget);
		//    //var press = 0;
		//    if (item.val().length > 1) {
		//        codeAddress();
		//    }
		//})
		//$(".pac-container .pac-item:selected").addEventListener("change", function () { codeAddressFrom() });

		//document.getElementById("submit").addEventListener("mousedown", function () { codeAddressTo() });
	});

})();