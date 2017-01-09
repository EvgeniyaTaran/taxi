package kpi.diplom.taxi.taxiandroid.fragments;

import android.app.ListFragment;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

import kpi.diplom.taxi.taxiandroid.adapters.CarAdapter;
import kpi.diplom.taxi.taxiandroid.models.Car;

/**
 * Created by alexroverandom on 01-Nov-16.
 */

public class CarFragment extends ListFragment {

	public static final String CARS_KEY = "CarsKey";

	private List<Car> _cars;

	public static CarFragment NewInstance(ArrayList<Car> grounds) {
		CarFragment fragment = new CarFragment();
		Bundle arguments = new Bundle();
		arguments.putParcelableArrayList(CARS_KEY, grounds);
		fragment.setArguments(arguments);

		return fragment;
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		Bundle arguments = getArguments();
		if(arguments != null)
		{
			_cars = arguments.getParcelableArrayList(CARS_KEY);
			setListAdapter(new CarAdapter(getActivity(), _cars));
		}

		// restore saved state
		if(savedInstanceState != null) {}
	}

	@Override
	public void onViewCreated(View view, Bundle savedInstanceState) {
		super.onViewCreated(view, savedInstanceState);
		getListView().setDivider(null);
	}

	@Override
	public void onListItemClick(ListView l, View v, int position, long id) {
		Car item = _cars.get(position);
		OnSelectedGroundListener listener = (OnSelectedGroundListener) getActivity();
		listener.onGroundSelected(item);
	}

	public interface OnSelectedGroundListener {
		void onGroundSelected(Car item);
	}
}