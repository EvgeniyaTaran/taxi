package kpi.bakalavrat.taxi.taxiandroid;

        import android.content.Context;

        import com.loopj.android.http.AsyncHttpClient;
        import com.loopj.android.http.AsyncHttpResponseHandler;
        import com.loopj.android.http.FileAsyncHttpResponseHandler;
        import com.loopj.android.http.JsonHttpResponseHandler;
        import com.loopj.android.http.RequestParams;

        import ua.of.markiza.visualization3d.models.MarkizaModel;

public class RestClient {
    Context context;
    Settings settings;


    public RestClient(Context context, Settings settings) {
        this.context = context;
        this.settings = settings;
    }

    public void getSunblindPhotos(MarkizaModel model, FileAsyncHttpResponseHandler responseHandler) {
        RequestParams params = new RequestParams();
        params.put("id", model.Id);
        Client.get("getsunblindphotos", params, responseHandler);
    }

    public void getInitPhotos(FileAsyncHttpResponseHandler responseHandler) {
        Client.get("getinitphotos", null, responseHandler);
    }

    public void getInitData(FileAsyncHttpResponseHandler responseHandler) {
        //RequestParams params = new RequestParams();
        Client.get("getinitdata", null, responseHandler);
    }

    public void getModelPhotos(final MarkizaModel model, FileAsyncHttpResponseHandler responseHandler) {
        RequestParams params = new RequestParams();
        params.put("id", model.Id);
        Client.get("getmodelphotos", params, responseHandler);
    }

    public void getModelTechPhotos(MarkizaModel model, FileAsyncHttpResponseHandler responseHandler) {
        RequestParams params = new RequestParams();
        params.put("id", model.Id);
        Client.get("getmodeltechphotos", params, responseHandler);
    }

    public void checkForUpdates(String json, FileAsyncHttpResponseHandler responseHandler) {
        RequestParams params = new RequestParams();
        params.put("json", json);
        Client.post("checkforupdates", params, responseHandler);
    }

    public void getContacts(FileAsyncHttpResponseHandler responseHandler) {
        RequestParams params = new RequestParams();
        Client.get("getcontacts", params, responseHandler);
    }
}

class Client {

    //private static final String BASE_URL = "http://markiza.of.ua/androidapi/";
    //private static final String BASE_URL = BuildConfig.DEBUG ? "http://10.0.2.2:3043/androidapi/" : "http://markiza.of.ua/androidapi/";
    private static final String BASE_URL = "http://10.0.2.2:3043/androidapi/";
    private static final int TIMEOUT = 180000;

    private static AsyncHttpClient client = new AsyncHttpClient();

    public static void get(String url, RequestParams params, AsyncHttpResponseHandler responseHandler) {
        client.setTimeout(TIMEOUT);
        client.get(getAbsoluteUrl(url), params, responseHandler);
    }

    public static void post(String url, RequestParams params, AsyncHttpResponseHandler responseHandler) {
        client.post(getAbsoluteUrl(url), params, responseHandler);
    }

    private static String getAbsoluteUrl(String relativeUrl) {
        return BASE_URL + relativeUrl;
    }
}