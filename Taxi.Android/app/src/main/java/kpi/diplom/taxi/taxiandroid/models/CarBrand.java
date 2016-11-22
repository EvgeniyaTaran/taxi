package kpi.diplom.taxi.taxiandroid.models;

import android.os.Parcel;
import android.os.Parcelable;

/**
 * Created by alexroverandom on 04-Sep-16.
 */
public class CarBrand implements Parcelable {
	public int Id;
	public String Name;

	public CarBrand(Parcel parcel) {
		Id = parcel.readInt();
		Name = parcel.readString();
	}

	@Override
	public int describeContents() {
		return 0;
	}

	@Override
	public void writeToParcel(Parcel dest, int flags) {
		dest.writeInt(Id);
		dest.writeString(Name);
	}

	public static final Parcelable.Creator<CarBrand> CREATOR = new Parcelable.Creator<CarBrand>() {
		@Override
		public CarBrand createFromParcel(Parcel parcel) {
			return new CarBrand(parcel);
		}

		@Override
		public CarBrand[] newArray(int i) {
			return new CarBrand[0];
		}
	};
}
