package kpi.diplom.taxi.taxiandroid;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.UUID;

public class Settings {

	private SharedPreferences _prefs;
	private String tokenKey = "token";
	public boolean IsDebug = false;

	public Settings(Context ctx) {

		_prefs = ctx.getSharedPreferences("CapiOffline", Context.MODE_PRIVATE);
	}

	public boolean isAuthenticated() {
		return _prefs.contains(tokenKey);
	}

	public UUID getToken() {
		return UUID.fromString(_prefs.getString(tokenKey, null));
	}

	public void setToken(UUID token) {
		SharedPreferences.Editor editor = _prefs.edit();
		if (token != null) {
			editor.putString(tokenKey, String.valueOf(token));
		} else {
			editor.remove(tokenKey);
		}

		editor.apply();
	}
}
