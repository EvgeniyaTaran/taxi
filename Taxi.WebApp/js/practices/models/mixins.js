Backbone.Models = { Mixins: {} };

Backbone.Models.Mixins.Idable = {
	idAttribute: "id"
};
Backbone.Models.Mixins.Sel2Member = {
	_getSel2TextField: function () {
		if (_.isFunction(this.sel2TextField)) {
			return this.sel2TextField();
		} else {
			return this.get(this.sel2TextField);
		}
	},
	sel2IdField: null,
	sel2TextField: "text",
	toSel2Member: function () {
		return {
			id: this.sel2IdField ? this.get(this.sel2IdField) : this.id,
			text: this._getSel2TextField()
		};
	}
};

Backbone.Models.Mixins.Partiable = (function () {
	//var _normalizeUrl;

	function Partiable() { }

	Partiable.prototype._isFetchedFull = false;

	var normalizeUrl = function (url) {
		if (url[0] !== "/") {
			url = "/" + url;
		}
		if (url.slice(-1) !== "/") {
			return url += "/";
		}
	};

	Partiable.prototype.isFetched = function () {
		return this._isFetchedFull;
	};

	Partiable.prototype.fetchFullFn = function () {
		//var url;
		var url = normalizeUrl(this.urlRoot);
		return $.get(this.urlRoot + this.id);
	};

	Partiable.prototype.fetchFull = function () {
		if (this._isFetchedFull) {
			return $.when(this);
		} else {
			return $.when(this.fetchFullFn()).done(_.bind(this.onFetchFull, this)).done((function (_this) {
				return function () {
					return _this._isFetchedFull = true;
				};
			})(this)).then((function (_this) {
				return function () {
					return _this;
				};
			})(this));
		}
	};

	return Partiable;

})();