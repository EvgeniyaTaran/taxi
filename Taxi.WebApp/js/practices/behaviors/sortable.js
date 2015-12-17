Behaviors.Sortable = Marionette.Behavior.extend({
	onRender: function () {

		this._sortable();
	},
	onAddChild:function() {
		this._sortable();
	},
	onChildRemove:function() {
		this._sortable();
	},
	_sortable:function() {
		var collection = this.view.collection // Замыкаем коллекцию
			, items = this.view.children._views // Получаем список дочерних элементов
			, view
		;

		for (var v in items) {
			view = items[v];
			view.$el.attr('data-backbone-cid', view.model.cid); // Привязываем элемент к модели по cid
		}
		this.$el.sortable("destroy");
		this.$el.sortable({ // Делаем список сортируемым
			axis: this.options.axis || false,
			grid: this.options.grid || false,
			containment: this.options.containment || false,
			forcePlaceholderSize: this.options.forcePlaceholderSize || true,
			cursor: "move",
			handle: this.options.handle || false,
			revert: this.options.revert || false
		}).bind('sortupdate', function (event, ui) {
			var model = collection.get(ui.item.data('backbone-cid'));
			// Получаем привязанную модель
			collection.remove(model, { silent: true });
			// По-тихому удаляем её из коллекции
			collection.add(model, { at: ui.item.index(), silent: true });
			//И также втихаря добавляем её по нужному индексу

			for (var i = 0; i < collection.length; i++) {
				if (collection.models[i].data) {
					collection.models[i].data.num = i;
				} else {
					collection.models[i].num = i;
				}
			}

			//console.log("sortable: new order", collection.models.map(function (m) { return m.data.num; }));

			//model.data.num = ui.item.index();
		});;
	}
});

Behaviors.MenuControl = Marionette.Behavior.extend({
	defaults: {
		name: "Group"
	},
	getTemplate: function () {
		var tmpl = ".j" + this.options.name;

		if (this.view.options.isMenu) {
			tmpl += "Menu";
		}

		tmpl += "Tmpl";

		return ".jControlTmpls " + tmpl;
	},
});

var MenuControlGetTemplate = function (view, isCustomMenu, customTmpl) {
	var tmpl = ".j";

	if (view.options.isMenu) {
		tmpl += "Menu";

		if (isCustomMenu) {
			if (customTmpl) {
				tmpl += customTmpl;
			} else {
				tmpl += view.model.control;
			}
		} else {
			tmpl += "Default";
		}

	} else {
		if (customTmpl) {
			tmpl += customTmpl;
		} else {
			tmpl += view.model.control;
		}
	}
	
	tmpl += "Tmpl";

	return ".jControlTmpls " + tmpl;
}