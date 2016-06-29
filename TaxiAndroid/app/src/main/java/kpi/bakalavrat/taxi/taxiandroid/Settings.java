package kpi.bakalavrat.taxi.taxiandroid;

import android.content.Context;
import android.content.SharedPreferences;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;


public class Settings {

    private SharedPreferences _prefs;
    private String wallsKey = "walls";
    private String logsKey = "errors";

    public Settings(Context context){
        _prefs = context.getSharedPreferences("TaxiAndroid", Context.MODE_PRIVATE);
    }

    public ArrayList<CustomError> getErrors() {

        String json = _prefs.getString(logsKey, null);

        if (json == null || json.equals("")) {
            return new ArrayList<>();
        }

        Gson gson = new Gson();

        Type collectionType = new TypeToken<ArrayList<CustomError>>() {
        }.getType();

        ArrayList<CustomError> errors = gson.fromJson(json, collectionType);

        return errors;
    }

    public void setErrors(ArrayList<CustomError> errors) {
        Gson gson = new Gson();

        String json = gson.toJson(errors);

        SharedPreferences.Editor editor = _prefs.edit();
        editor.putString(logsKey, json);
        editor.apply();
    }

    public ArrayList<Wall> getWalls() {

        String json = _prefs.getString(wallsKey, null);

        if (json == null || json.equals("")) {
            return new ArrayList<>();
        }

        Gson gson = new Gson();

        Type collectionType = new TypeToken<ArrayList<Wall>>() {
        }.getType();

        ArrayList<Wall> walls = gson.fromJson(json, collectionType);

        return walls;
    }

    public void setWalls(ArrayList<Wall> walls) {
        Gson gson = new Gson();

        String json = gson.toJson(walls);

        SharedPreferences.Editor editor = _prefs.edit();
        editor.putString(wallsKey, json);
        editor.apply();
    }
}
