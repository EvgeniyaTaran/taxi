package kpi.diplom.taxi.taxiandroid.activities;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;

import com.loopj.android.http.AsyncHttpResponseHandler;

import java.util.UUID;

import kpi.diplom.taxi.taxiandroid.R;

public class AuthActivity extends BaseActivity {

	Spinner spinner;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_auth);
	}

	public void auth(View view) {
		String login = String.valueOf(((EditText) findViewById(R.id.login)).getText());
		String password = String.valueOf(((EditText) findViewById(R.id.password)).getText());
		int carId = 40; //((Spinner) findViewById(R.id.carSpinner)).;

		final ProgressDialog pDialog = new ProgressDialog(this);
		pDialog.show();

		restClient.login(login, password, carId, new AsyncHttpResponseHandler() {
			@Override
			public void onSuccess(int statusCode, cz.msebera.android.httpclient.Header[] headers, byte[] responseBody) {
				pDialog.dismiss();

				try {
					UUID uuid = UUID.fromString(new String(responseBody));

					settings.setToken(uuid);

                    /*Intent i = new Intent(AuthActivity.this, MainActivity.class);
                    startActivity(i);*/

					//типа автоматом должна открыться главная активити
					finish();

				} catch (Exception ex) {

				}
			}

			@Override
			public void onFailure(int statusCode, cz.msebera.android.httpclient.Header[] headers, byte[] responseBody, Throwable error) {
				pDialog.dismiss();
			}

		});
	}
}
