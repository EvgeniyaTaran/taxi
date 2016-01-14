App.Views.Photo = Marionette.ItemView.extend({
	template: ".jPhotoGalleryItemTmpl",
	templateHelpers: function () {
		var that = this;
		return {
			isDeleted: function () {
				return that.options.isDeleted;
			},
			isSelected: function () {
				return that.options.isSelected;
			},
			hasFile: function () {
				return that.options.isSelected || (that.model.fileName !== Utils.uuid4.empty() && !that.options.isDeleted);
			},
			getRootUrl: function () {
				return that.options.rootUrl;
			}
		}
	},
	_isDeleted: null,
	ui: {
		desc: "[name=desc]",
		"delete": "[name=delete]",
		cancelDelete: "[name=cancelDelete]",
		cancelSelected: "[name=cancelSelected]"
	},
	events: {
		"click @ui.cancelSelected": function () {
			//this.ui.photo.val("");
			//this.options.isSelected = false;
			//this.options.selectedFile = null;
			this.model.collection.remove(this.model);
			//return false;
		},
		"click @ui.delete": function () {
			this.options.isDeleted = true;
			this.render();
			this.triggerMethod("view:change");
			App.needConfirmToLeave = true;
			this._isDeleted = true;
			return false;
		},
		"click @ui.cancelDelete": function () {
			//this.ui.photo.val("");
			this.options.isSelected = false;
			this.options.selectedFile = null;
			this.options.isDeleted = false;
			this._isDeleted = false;
			this.render();
			this.triggerMethod("view:change");
			return false;
		},
	},
	initialize: function () {
		_.extend(this.options, {
			isDeleted: false,
			isSelected: false,
			isFileLoaded: false
		});
		//_.extend(this.model, { isDisabled: this.options.isDisabled });
	},
	onRender: function () {

		var that = this;
		var file = that.model.selectedFile;

		if (file && !this.options.isFileLoaded) {
			var reader = new FileReader();
			reader.onload = function (e) {
				that.options.isFileLoaded = true;
				that.options.isSelected = true;
				that.model.selectedFileUrl = e.target.result;
				//that.model.id = Utils.uuid4.empty();
				that.render();
				that.triggerMethod("view:change");
			};

			reader.readAsDataURL(file);
		}

		//console.log(this.model);

	},
	getData: function () {

		if (this.options.isSelected) {
			//this.model.id = Utils.uuid4.new();
			//data.name = this.options.selectedFile.name;
		} else if (this.options.isDeleted) {
			this.model.id = Utils.uuid4.empty();
		}
		this.model.description = this.ui.desc.val();

		return {
			description: this.model.description,
			id: this.model.id,
			name: this.model.name,
			num: this.model.num
		};
	},
	getFile: function () {
		if (this.model.selectedFile) {
			return {
				id: this.model.id,
				fileName: this.model.fileName,
				file: this.model.selectedFile
			};
		} else if (this._isDeleted) {
			return {
				id: this.model.id,
				fileName: this.model.fileName,
				toDelete: true
			};
		}
	}

});

App.Views.Photos = Marionette.CollectionView.extend({
	childView: App.Views.Photo,
	//behaviors: {
	//	Sortable: {
	//		containment: 'parent',
	//		handle: ".jHandle"
	//	}
	//},
	childViewOptions: function () {
		return this.options;
	},
	//childEvents: {
	//    "view:change": function () {
	//        this._sortable();
	//    }
	//},
	//onRender: function () {
	//    this._sortable();
	//},
	//_sortable: function () {
	//    var collection = this.collection // Замыкаем коллекцию
	//    , items = this.children._views // Получаем список дочерних элементов
	//    , view
	//    ;

	//    for (var v in items) {
	//        view = items[v];
	//        view.$el.attr('data-backbone-cid', view.model.cid); // Привязываем элемент к модели по cid
	//    }
	//    this.$el.sortable("destroy");

	//    this.$el.sortable({ // Делаем список сортируемым
	//        containment: "parent",
	//        forcePlaceholderSize: true,
	//        cursor: "move",
	//        handle: ".jHandle",
	//    }).bind('sortupdate', function (event, ui) {
	//        var model = collection.get(ui.item.data('backbone-cid'));
	//        // Получаем привязанную модель
	//        collection.remove(model, { silent: true });
	//        // По-тихому удаляем её из коллекции
	//        collection.add(model, { at: ui.item.index(), silent: true });
	//        //И также втихаря добавляем её по нужному индексу

	//        for (var i = 0; i < collection.length; i++) {
	//            collection.models[i].num = i;
	//        }
	//    });
	//}
});

App.Views.PhotoGallery = Marionette.LayoutView.extend({
	template: ".jPhotoGalleryTmpl",
	regions: {
		photosList: ".jPhotosList"
	},
	ui: {
		photoFile: ".jPhotoFileDialog",
		sortableList: ".jPhotoSortable",
		save: ".jSavePhotos"
	},
	events: {
		"change @ui.photoFile": "addFiles",
		"click @ui.save": "save"
	},
	initialize: function () {
		_.extend(this.options,
			{
				collection: new Backbone.Collection()
			});
	},

	addFiles: function () {
		var that = this;
		var fileList = this.ui.photoFile[0].files;

		$.each(fileList, function (i, item) {
			var object = {
				fileName: Utils.uuid4.new(),
				name: item.name,
				description: "",
				selectedFileUrl: "",
				selectedFile: item
			};

			var m = new Backbone.ExtModel(object);

			that.options.collection.add(m);
		});

		this.ui.photoFile.val("");
		App.needConfirmToLeave = true;
	},

	onRender: function () {
		var that = this;
		var photos = App.Collections.photos.filter({ isMain: false });
		that.options.collection = new Backbone.Collection(photos);

		this.photosList.show(new App.Views.Photos({
			collection: this.options.collection
		}));
		//if (this.model.photos.models.length > 0) {

		//    $.each(this.model.photos.models, function (i, item) {
		//        if (!item.isMain) {
		//            that.options.collection.add(new Backbone.ExtModel(item));
		//        }
		//    });

		//    //this.options.collection.reset(this.options.collection.sortBy("num"));
		//}

		////console.log(this.model);
		//this.photosList.show(new App.Views.Photos({
		//    collection: this.options.collection
		//}));
	},
	getData: function () {
		var data = [];
		var views = this.photosList.currentView.children.toArray();
		for (var i = 0; i < views.length; i++) {
			var model = views[i].getData();
			if (model.id !== Utils.uuid4.empty()) {
				data.push(model);
			}
		}

		return data;
	},
	getFile: function () {
		var data;
		if (this.model.productModelId) {
			data = {
				files: [],
				modelId: this.model.productModelId,
				productId: this.model.id,
				deletedFiles: []
			};
		}
		else {
			data = {
				files: [],
				modelId: this.model.id,
				productId: null,
				deletedFiles: []
			};
		}
		var views = this.photosList.currentView.children.toArray();
		for (var i = 0; i < views.length; i++) {
			var file = views[i].getFile();
			if (file) {
				if (file.toDelete) {
					data.deletedFiles.push(file);
				} else {
					data.files.push(file);
				}
			}
		}

		return data;
	},
	save: function () {
		var that = this;
		var data = that.getFile();
		if (data && data.files) {
			var fd = new FormData();
			$.each(data.files, function (i, item) {
				fd.append(item.fileName, item.file);
			});
			var deletedNames = "";
			$.each(data.deletedFiles, function (i, item) {
				deletedNames += ";" + item.fileName;
			});
			fd.append("deletedFiles", deletedNames);
			fd.append("modelId", data.modelId);
			fd.append("productId", data.productId);
			var xhr = new XMLHttpRequest();
			var uploadComplete = function () {
				if (xhr.status === 200) {
					var obj = JSON.parse(xhr.response);
					App.Collections.photos.reset(obj.photos);
					that.render();
				}
			};
			xhr.addEventListener("load", uploadComplete, false);
			if (!data.productId) {
				xhr.open("POST", "/api/productphotos/saveotherphotos/");
			} else {
				xhr.open("POST", "/api/productphotos/saveotherproductphotos/");
			}
			xhr.send(fd);
		}
	}
});

