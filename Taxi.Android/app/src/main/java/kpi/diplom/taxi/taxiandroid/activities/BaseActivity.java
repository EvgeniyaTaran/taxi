package kpi.diplom.taxi.taxiandroid.activities;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;

import kpi.diplom.taxi.taxiandroid.RestClient;
import kpi.diplom.taxi.taxiandroid.Settings;

public class BaseActivity extends AppCompatActivity {

	protected RestClient restClient;
	protected Settings settings;
	ProgressDialog pd;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		settings = new Settings(this);
		restClient = new RestClient(this);
	}

	protected void toLogin() {
		startActivity(new Intent(BaseActivity.this, AuthActivity.class));
	}
}
