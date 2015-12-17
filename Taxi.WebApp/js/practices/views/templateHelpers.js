
(function () {
	var formatDate = function (dt, dtFormat) {
		if (dtFormat == null) {
			dtFormat = App.Common.DATEFORMAT;
		}
		if (!dt) {
			return "";
		}
		return moment(dt).format(dtFormat);
	}
	//return
	$.views.helpers({

		/**
			   * Provides underscore/lodash object in templates
			   * @type {Underscore/Lo-Dash}
		 */
		_: _,
		log: function (data) {
			if (data == null) {
				data = this;
			}
			return console.dir(data);
		},
		lower: function (input) {
			if (input == null) {
				input = "";
			}
			return input.toLowerCase();
		},
		join: function (array, separator) {
			if (array == null) {
				array = [];
			}
			if (separator == null) {
				separator = ", ";
			}
			return array.join(separator);
		},
		formatDate: formatDate,
		formatDateTime: function (dt) {
			return formatDate(dt, App.Common.DATETIMEFORMAT);
		},
		today: new Date(),
		daysLeft: function (dt) {
			//var today;
			var today = moment();
			return moment(dt).diff(today, "days");
		},

		/**
			   * Formats tel number
			   * @param  {Number} telephone number
			   * @return {String} formatted number in Ukr-specific format 
			   * +380 75 254-15-86
		 */
		formatPhone: function (number) {
			//var str;
			var str = number.toString();
			return str.replace(/(\d\d\d)(\d\d)(\d\d\d)(\d\d)(\d\d)/, "+$1 $2 $3-$4-$5");
		},
		isInRole: function(role) {
			var res = false;
			App.Identity.Roles.forEach(function(el) {
				if (el === role) {
					res = true;
				}
			});
			return res;
		}
	});
})();
