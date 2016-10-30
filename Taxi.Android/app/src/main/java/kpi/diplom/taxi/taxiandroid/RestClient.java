package kpi.diplom.taxi.taxiandroid;

import android.content.Context;

import com.loopj.android.http.AsyncHttpClient;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.FileAsyncHttpResponseHandler;
import com.loopj.android.http.JsonHttpResponseHandler;
import com.loopj.android.http.RequestHandle;
import com.loopj.android.http.RequestParams;
import com.loopj.android.http.SyncHttpClient;

import java.util.UUID;

class Client {

	public static final String BASE_URL = "http://10.0.2.2:60606/";

	private static AsyncHttpClient client = new AsyncHttpClient();
	private static SyncHttpClient syncClient = new SyncHttpClient();

	public static void get(String url, RequestParams params, AsyncHttpResponseHandler responseHandler) {
		client.get(getAbsoluteUrl(url), params, responseHandler);
	}

	public static void post(String url, RequestParams params, AsyncHttpResponseHandler responseHandler) {
		client.setTimeout(30000);
		client.post(getAbsoluteUrl(url), params, responseHandler);
	}

	public static RequestHandle postSync(String url, RequestParams params, AsyncHttpResponseHandler responseHandler) {
		return syncClient.post(getAbsoluteUrl(url), params, responseHandler);
	}

	private static String getAbsoluteUrl(String relativeUrl) {
		return BASE_URL + relativeUrl;
	}
}

public class RestClient {
	Context context;

	public RestClient(Context context) {
		this.context = context;
	}

	public void login(String login, String password, int carId, AsyncHttpResponseHandler responseHandler) {
		RequestParams params = new RequestParams();
		params.put("login", login);
		params.put("password", password);
		params.put("carId", carId);
		Client.post("auth/androidlogin", params, responseHandler);
	}

	public void startWork(UUID driverId, int carId, double lat, double lng, Boolean withGeo, AsyncHttpResponseHandler responseHandler) {
		RequestParams params = new RequestParams();
		params.put("driverId", driverId.toString());
		params.put("carId", carId);
		if (withGeo)
		{
			params.put("latitude", lat);
			params.put("longitude", lng);
		}
		Client.post("api/cab/startwork", params, responseHandler);
	}
}