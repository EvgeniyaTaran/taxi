package kpi.diplom.taxi.taxiandroid;

import android.Manifest;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.Locale;

public class GpsTracker {
	private static final int intervalSec = 30;
	private static final int fastestIntervalSec = 10;
	private static final long MIN_DISTANCE_CHANGE_FOR_UPDATES = 10;// 10 meters
	private static final long MIN_TIME_BW_UPDATES = 1000 * 60 * 1;// 1 minute
	protected LocationManager locationManager;
	Activity context;
	boolean isGPSEnabled = false;
	boolean isNetworkEnabled = false;
	private PassageLocation locationGps;
	private PassageLocation locationNetwork;
	private boolean isFirstChange = true;
	LocationListener locationListenerGps = new LocationListener() {
		public void onLocationChanged(Location location) {
			if (isFirstChange) {
				locationGps.setStart(location);
			} else {
				locationGps.setFinish(location);
			}
			//lm.removeUpdates(this);
			//lm.removeUpdates(locationListenerNetwork);
		}

		public void onProviderDisabled(String provider) {
		}

		public void onProviderEnabled(String provider) {
		}

		public void onStatusChanged(String provider, int status, Bundle extras) {
		}
	};
	LocationListener locationListenerNetwork = new LocationListener() {
		public void onLocationChanged(Location loc) {
			if (isFirstChange) {
				locationNetwork.setStart(loc);
			} else {
				locationNetwork.setFinish(loc);
			}
			//lm.removeUpdates(this);
			//lm.removeUpdates(locationListenerNetwork);
		}

		public void onProviderDisabled(String provider) {
		}

		public void onProviderEnabled(String provider) {
		}

		public void onStatusChanged(String provider, int status, Bundle extras) {
		}
	};

	public GpsTracker(Activity context) {
		this.context = context;
		locationManager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);

		locationGps = new PassageLocation();
		locationNetwork = new PassageLocation();
	}

	public PassageLocation getLocationGps() {
		return locationGps;
	}

	public Location getCurrentLocation() {
		Location location = null;
		try {
			location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
			locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, MIN_TIME_BW_UPDATES, MIN_DISTANCE_CHANGE_FOR_UPDATES, locationListenerGps);
		}
		catch(SecurityException e) {

		}
		return location;
	}

	public PassageLocation getLocationNetwork() {
		return locationNetwork;
	}

	public void start() {
		try {
			isFirstChange = true;
			isGPSEnabled = locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER);
			isNetworkEnabled = locationManager.isProviderEnabled(LocationManager.NETWORK_PROVIDER);
			if (isGPSEnabled) {
				locationGps.setPreStart(locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER));
				locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, MIN_TIME_BW_UPDATES, MIN_DISTANCE_CHANGE_FOR_UPDATES, locationListenerGps);
			}
			if (isNetworkEnabled) {
				locationNetwork.setPreStart(locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER));
				locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, MIN_TIME_BW_UPDATES, MIN_DISTANCE_CHANGE_FOR_UPDATES, locationListenerNetwork);
			}
		}
		catch(SecurityException e) {

		}
	}

	public void stop() {
		try {
			locationManager.removeUpdates(locationListenerNetwork);
			locationManager.removeUpdates(locationListenerGps);
		} catch(SecurityException e) {

		}
	}

	public class PassageLocation {
		private final DecimalFormat locationFormat = new DecimalFormat("#0.000000", new DecimalFormatSymbols(Locale.US));
		private Location preStartLocation = null;
		private Location startLocation = null;
		private Location finishLocation = null;

		public String getPreStart() {
			return format(preStartLocation);
		}

		public void setPreStart(Location location) {
			preStartLocation = location;
		}

		public String getStart() {
			return format(startLocation);
		}

		public void setStart(Location location) {
			startLocation = location;
		}

		public String getFinish() {
			return format(finishLocation);
		}

		public void setFinish(Location location) {
			finishLocation = location;
		}

		private String format(Location location) {
			if (location != null) {
				return locationFormat.format(location.getLatitude()) + "," + locationFormat.format(location.getLongitude());
			} else {
				return "";
			}
		}
	}
}