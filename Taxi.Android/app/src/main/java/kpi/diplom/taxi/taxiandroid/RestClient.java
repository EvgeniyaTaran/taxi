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
	//private static final String BASE_URL1 = "http://opinion.com.ua/interviewersApi/";

	//public static final String BASE_URL = BuildConfig.DEBUG ? "http://10.0.2.2:53938/interviewersApi/" : "http://opinion.com.ua/interviewersApi/";
	public static final String BASE_URL = "http://10.0.2.2:60606/api/";
	//public static final String BASE_URL = "http://opinion.com.ua/interviewersApi/";

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

	public void login(AsyncHttpResponseHandler responseHandler) {
		RequestParams params = new RequestParams();
		params.put("DriverId", UUID.randomUUID().toString());
		params.put("CarId", UUID.randomUUID().toString());
		params.put("Longitude", UUID.randomUUID().toString());
		params.put("Latitude", UUID.randomUUID().toString());
		Client.post("cab/login/", params, responseHandler);
	}

	public void getProjects(JsonHttpResponseHandler responseHandler) {
		RequestParams params = new RequestParams();
		Client.get("getProjects.ashx", params, responseHandler);
	}

	public void auth(String name, AsyncHttpResponseHandler responseHandler) {
		RequestParams params = new RequestParams();
		params.put("name", name);
		Client.get("auth.ashx", params, responseHandler);
	}
}