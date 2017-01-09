package kpi.diplom.taxi.taxiandroid.adapters;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;

import java.util.List;

import kpi.diplom.taxi.taxiandroid.R;
import kpi.diplom.taxi.taxiandroid.models.Car;

public class CarAdapter extends ArrayAdapter<Car> {

	public CarAdapter(Context context, List<Car> items) {
		super(context, R.layout.fragment_car, items);
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder viewHolder;

		if(convertView == null) {
			LayoutInflater inflater = LayoutInflater.from(getContext());
			convertView = inflater.inflate(R.layout.fragment_car, parent, false);

			viewHolder = new ViewHolder();
			viewHolder.ivIcon = (ImageView) convertView.findViewById(R.id.ivIcon);
			//           viewHolder.tvTitle = (TextView) convertView.findViewById(R.id.tvTitle);
//            viewHolder.tvDescription = (TextView) convertView.findViewById(R.id.tvDescription);
			convertView.setTag(viewHolder);
		} else {
			// recycle the already inflated view
			viewHolder = (ViewHolder) convertView.getTag();
		}

		Car item = getItem(position);
		Drawable icon = Drawable.createFromPath("");
		viewHolder.ivIcon.setImageDrawable(icon);
//        viewHolder.tvTitle.setText(item.Name);
//        viewHolder.tvDescription.setText(item.description);

		return convertView;
	}

	private static class ViewHolder {
		ImageView ivIcon;
//        TextView tvTitle;
//        TextView tvDescription;
	}
}

