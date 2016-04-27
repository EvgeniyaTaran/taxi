package kpi.bakalavrat.taxi.taxiandroid;

import android.net.Uri;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.koushikdutta.async.future.Future;
import com.koushikdutta.async.http.AsyncHttpClient;
import com.koushikdutta.async.http.AsyncHttpRequest;
import com.koushikdutta.async.http.WebSocket;


public class MainActivity extends ActionBarActivity {

    private TextView _tvData;
    private String _name = "TEST";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        _tvData = (TextView) findViewById(R.id.textView);
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
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    public void SendRequest(View view){
        Future<WebSocket> ws = AsyncHttpClient.getDefaultInstance().websocket("ws://10.0.2.2:3030/api/request/get?username=Reee", null, new AsyncHttpClient.WebSocketConnectCallback() {
            @Override
            public void onCompleted(Exception ex, WebSocket webSocket) {
                webSocket.send("hello");
                webSocket.setStringCallback(new WebSocket.StringCallback() {
                    @Override
                    public void onStringAvailable(String s) {
                        _name = s;
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                showName();
                            }
                        });
                    }
                });
            }
        });

//
//        try{
//            _tvData.setText(AsyncHttpClient.getDefaultInstance().executeString(new AsyncHttpRequest(Uri.parse("http://10.0.2.2:3030/api/car/GetByNumber?number=11111"), "GET"), null).get());
//        }
//        catch (Exception e){}

    }

    private void showName() {
        _tvData.setText(_name);
    }
}
