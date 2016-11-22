package kpi.diplom.taxi.taxiandroid.models;

import android.os.Parcel;
import android.os.Parcelable;

public class Car implements Parcelable  {
	public int Id;
	public String Name;
	public CarModel Model;

	public Car(Parcel parcel) {
		Id = parcel.readInt();
		Name = parcel.readString();
		Model = parcel.readParcelable(CarModel.class.getClassLoader());
	}

	@Override
	public int describeContents() {
		return 0;
	}

	@Override
	public void writeToParcel(Parcel dest, int flags) {
		dest.writeInt(Id);
		dest.writeString(Name);
		dest.writeParcelable(Model, flags);
	}

	public static final Parcelable.Creator<Car> CREATOR = new Parcelable.Creator<Car>() {
		@Override
		public Car createFromParcel(Parcel parcel) {
			return new Car(parcel);
		}

		@Override
		public Car[] newArray(int i) {
			return new Car[0];
		}
	};
}
