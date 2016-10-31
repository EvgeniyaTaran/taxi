package kpi.diplom.taxi.taxiandroid.activities;

import android.Manifest;
import android.annotation.TargetApi;
import android.app.ProgressDialog;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Build;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import com.loopj.android.http.AsyncHttpResponseHandler;

import java.util.UUID;

import kpi.diplom.taxi.taxiandroid.GpsTracker;
import kpi.diplom.taxi.taxiandroid.R;
import kpi.diplom.taxi.taxiandroid.RestClient;

public class MainActivity extends BaseActivity {

	private GpsTracker gps;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
		setSupportActionBar(toolbar);

		FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
		fab.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View view) {
				Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
						.setAction("Action", null).show();
			}
		});

		gps = new GpsTracker(this);
	}

	@Override
	public void onResume() {
		super.onResume();

		if (getIntent().getBooleanExtra("EXIT", false)) {
			finish();
		}

		if (settings.isAuthenticated()) {

		} else {
			toLogin();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu_main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();

		//noinspection SimplifiableIfStatement
		if (id == R.id.action_logout) {
			logout();
		}

		return super.onOptionsItemSelected(item);
	}

	public void startWork(View view) {
		UUID driverId = settings.getToken();
		// TODO: select car from the list
		int carId = 40;

		if (driverId != null) {
			final ProgressDialog pDialog = new ProgressDialog(this);
			pDialog.show();

			double lat = 0;
			double lng = 0;
			Boolean withGeo = false;

			if (canAccessGps()) {
				Location location = gps.getCurrentLocation();
				lat = location.getLatitude();
				lng = location.getLongitude();
				withGeo = true;
			}

			restClient.startWork(driverId, carId, lat, lng, withGeo, new AsyncHttpResponseHandler() {
				@Override
				public void onSuccess(int statusCode, cz.msebera.android.httpclient.Header[] headers, byte[] responseBody) {
					pDialog.dismiss();


					Toast.makeText(MainActivity.this, "Started to work", Toast.LENGTH_SHORT).show();
				}

				@Override
				public void onFailure(int statusCode, cz.msebera.android.httpclient.Header[] headers, byte[] responseBody, Throwable error) {
					pDialog.dismiss();
				}

			});
		}
	}

	private boolean canAccessGps() {
		return (hasPermission(Manifest.permission.ACCESS_FINE_LOCATION));
	}

	@TargetApi(Build.VERSION_CODES.M)
	private boolean hasPermission(String perm) {
		return (PackageManager.PERMISSION_GRANTED == ContextCompat.checkSelfPermission(this, perm));
	}
}
