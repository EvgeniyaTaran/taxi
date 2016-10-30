package kpi.diplom.taxi.taxiandroid;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.UUID;

public class Settings {

	private SharedPreferences _prefs;
	private String tokenKey = "token";
	public boolean IsDebug = false;

	public Settings(Context ctx) {

		_prefs = ctx.getSharedPreferences("Taxi", Context.MODE_PRIVATE);
	}

	public boolean isAuthenticated() {
		return _prefs.contains(tokenKey);
	}

	public UUID getToken() {
		String uuid = _prefs.getString(tokenKey, null);
		if (uuid == null) {
			return null;
		}
		return UUID.fromString(uuid);
	}

	public void setToken(UUID token) {
		SharedPreferences.Editor editor = _prefs.edit();
		if (token != null) {
			editor.putString(tokenKey, String.valueOf(token));
		}

		editor.apply();
	}
}
