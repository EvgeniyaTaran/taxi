(function () {
	var order = {
		from: {
			lat: null,
			long: null
		},
		to: {
			lat: null,
			long: null
		}
	};

	var page = {
		markerFrom: null,
		markerTo: null
	}

	var ui = {
		collapse: $(".jCollapse"),
		orderBlock: $(".jOrder"),
		additional: {
			toggle: $(".jAdditionalToggle"),
			items: $(".jAdditional")
		},
		create: $(".jCreateOrder")
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
				var latlng = new google.maps.LatLng(50.45, 30.51);
				var mapOptions = {
					zoom: 12,
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
						height: $(window).height() - 300
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

	function clearMarkers() {
		setMapOnAll(null);
	}

	function setMapOnAll(map) {
		if (page.markerFrom) {
			page.markerFrom.setMap(map);
		}
		if (page.markerTo) {
			page.markerTo.setMap(map);
		}
	}

	function showMarkers() {
		setMapOnAll(map);
	}

	function codeAddressFrom() {
		var time = setTimeout(function () {
			var addressFrom = document.getElementById("AddressFrom").value;
			geocoderFrom.geocode({ 'address': addressFrom }, function (results, status) {
				if (status == google.maps.GeocoderStatus.OK) {
					clearMarkers();
					map.setCenter(results[0].geometry.location);
					page.markerFrom = new google.maps.Marker({
						map: map,
						position: results[0].geometry.location
					});
					showMarkers();
					var fromLat = page.markerFrom.position.lat();
					var fromLng = page.markerFrom.position.lng();
					$("#GeoCoordinatesFromLat").val(fromLat);
					$("#GeoCoordinatesFromLng").val(fromLng);
					order.from.lat = fromLat;
					order.from.long = fromLng;
					if (order.to.lat && order.to.long) {
						ui.create.slideDown();
					}
				} else {
					console.log("Geocode for start point was not successful for the following reason: " + status);
					order.from.lat = null;
					order.from.long = null;
					ui.create.slideUp();
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
					clearMarkers();
					page.markerTo = new google.maps.Marker({
						map: map,
						position: results[0].geometry.location
					});
					showMarkers();
					var toLat = page.markerTo.position.lat();
					var toLng = page.markerTo.position.lng();
					$("#GeoCoordinatesToLat").val(toLat);
					$("#GeoCoordinatesToLng").val(toLng);
					order.to.lat = toLat;
					order.to.long = toLng;
					if (order.from.lat && order.from.long) {
						ui.create.slideDown();
					}
				} else {
					console.log("Geocode for end point was not successful for the following reason: " + status);
					order.to.lat = null;
					order.to.long = null;
					ui.create.slideUp();
				}
			});
			clearTimeout(time2);
		});
	}

	function bindEvents() {
		ui.collapse.click(function (e) {
			ui.collapse.toggleClass("hide");
			if (ui.orderBlock.is(":visible")) {
				ui.orderBlock.slideUp();
			} else {
				ui.orderBlock.slideDown();
			}
		});

		ui.additional.toggle.click(function () {
			$(this).toggleClass("selected");
			if (ui.additional.items.is(":visible")) {
				ui.additional.items.slideUp();
			} else {
				ui.additional.items.slideDown();
			}
		});
	}

	$(document).ready(function () {
		initialize();
	});

	bindEvents();

})();