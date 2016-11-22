package kpi.diplom.taxi.taxiandroid.models;

import android.os.Parcel;
import android.os.Parcelable;

/**
 * Created by alexroverandom on 04-Sep-16.
 */
public class CarModel implements Parcelable {

	public int Id;
	public String Name;
	public int CarBrandId;
	public CarBrand Brand;

	public CarModel(Parcel parcel) {
		Id = parcel.readInt();
		Name = parcel.readString();
		Brand = parcel.readParcelable(CarBrand.class.getClassLoader());
	}

	@Override
	public int describeContents() {
		return 0;
	}

	@Override
	public void writeToParcel(Parcel dest, int flags) {
		dest.writeInt(Id);
		dest.writeString(Name);
		dest.writeParcelable(Brand, flags);
	}

	public static final Parcelable.Creator<CarModel> CREATOR = new Parcelable.Creator<CarModel>() {
		@Override
		public CarModel createFromParcel(Parcel parcel) {
			return new CarModel(parcel);
		}

		@Override
		public CarModel[] newArray(int i) {
			return new CarModel[0];
		}
	};

}
