App.Views.ItemPhoto = Marionette.ItemView.extend({
    template: ".jPhotoTmpl",
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
                return that.options.isSelected || (that.model.attributes.fileName !== Utils.uuid4.empty() && !that.options.isDeleted);

            },
            hasServerFile: function () {
                return that.model.attributes.fileName !== Utils.uuid4.empty();
            }
        };
    },
    ui: {
        photo: "[type=file]",
        desc: "[name=desc]",
        "delete": "[name=delete]",
        cancelDelete: "[name=cancelDelete]",
        cancelSelected: "[name=cancelSelected]",
        clientPreview: ".jClientPreview",
        save: ".jSavePhoto"
    },
    initialize: function () {
        _.extend(this.options, {
            isDeleted: false,
            isSelected: false,
            selectedFile: null
        });
        if (this.model.attributes == null) {
            this.model.attributes = {
                fileName: Utils.uuid4.empty()
            };
        }
    },
    events: {
        "change @ui.photo": function () {
            var that = this;
            var reader = new FileReader();
            reader.onload = function (e) {
                that.ui.clientPreview.attr("src", e.target.result);
            };
            reader.readAsDataURL(this.ui.photo[0].files[0]);
            this.model.attributes.name = this.ui.photo[0].files[0].name;
            this.options.isSelected = true;
            this.options.selectedFile = this.ui.photo[0].files[0];
            this.render();

            App.needConfirmToLeave = true;
        },
        "click @ui.cancelSelected": function () {
            this.ui.photo.val("");
            this.options.isSelected = false;
            this.options.selectedFile = null;
            this.render();

            return false;
        },
        "click @ui.delete": function () {
            this.options.isDeleted = true;
            this.render();

            App.needConfirmToLeave = true;

            return false;
        },
        "click @ui.cancelDelete": function () {
            this.ui.photo.val("");
            this.options.isSelected = false;
            this.options.selectedFile = null;
            this.options.isDeleted = false;
            this.render();
            return false;
        },
        "click @ui.save": "save"
    },
    save: function () {
        var that = this;
        var data = that.getFile();
        if (data && data.file) {
        	var fd = new FormData();
        	fd.append(data.id, data.file);
        	fd.append("modelId", data.modelId);
        	fd.append("productId", data.productId);
        	fd.append("fileName", data.fileName);
        	fd.append("isMain", data.isMain);
        	fd.append("description", data.description);
        	var xhr = new XMLHttpRequest();
        	var uploadComplete = function () {
        		if (xhr.status === 200) {
        			var obj = JSON.parse(xhr.response);
        			App.Collections.photos.reset(obj.photos);
        			that.trigger("new_mainPhoto");
        		}
        	};
        	xhr.addEventListener("load", uploadComplete, false);
        	if (!data.productId && data.modelId) {
        		xhr.open("POST", "/api/productphotos/savemodelphoto/");
        	} else if (data.productId && !data.modelId) {
        		xhr.open("POST", "/api/productphotos/saveproductphoto/");
        	}
        	xhr.send(fd);
        	//$.post("/api/productmodel/savephoto", JSON.stringify({file: this.options.selectedFile})).done(function (res) {
        	//    console.log("saved");
        	//}).fail(function (res) {
        	//    console.log(res);
        	//});
        } else {
        	var fd = new FormData();
        	fd.append("modelId", data.modelId);
        	fd.append("productId", data.productId);
        	fd.append("fileName", data.fileName);
        	fd.append("isMain", data.isMain);
        	var xhr = new XMLHttpRequest();
        	var uploadComplete = function () {
        		if (xhr.status === 200) {
        			var obj = JSON.parse(xhr.response);
        			App.Collections.photos.reset(obj.photos);
        			that.trigger("new_mainPhoto");
        		}
        	};
        	xhr.addEventListener("load", uploadComplete, false);
        	if (!data.productId && data.modelId) {
        		xhr.open("POST", "/api/productphotos/deletemodelphoto/");
        	} else if (data.productId && !data.modelId) {
        		xhr.open("POST", "/api/productphotos/deleteproductphoto/");
        	}
        	xhr.send(fd);
        }
    },

    //serializeData: function () {
    //    return _.extend({}, this.model.attributes, this.model.data, {
    //        rootUrl: this.model.rootUrl,
    //        cid: this.model.cid,
    //        structureId: this.model.structureId,
    //        isDisabled: this.model.isDisabled
    //    });
    //},
    getData: function () {
        if (this.options.isSelected) {
            this.model.attributes.fileName = Utils.uuid4.new();
        } else if (this.options.isDeleted) {
            this.model.attributes.fileName = Utils.uuid4.empty();
        }
        return _.extend(this.model.attributes, {
            description: this.ui.desc.val()
        });
    },
    getFile: function () {
    	if (this.options.selectedFile !== null) {
    		return {
    			modelId: this.model.attributes.productModelId,
    			productId: this.model.attributes.productId,
    			id: this.model.attributes.id,
    			fileName: this.model.attributes.fileName,
    			isMain: this.model.attributes.isMain,
    			file: this.options.selectedFile,
    			description: this.ui.desc.val()
    		};
    	} else {
    		return {
    			modelId: this.model.attributes.productModelId,
    			productId: this.model.attributes.productId,
    			id: this.model.attributes.id,
    			fileName: this.model.attributes.fileName,
    			isMain: this.model.attributes.isMain
    		};
    	}
    }
});

App.Views.ItemNoPhoto = Marionette.ItemView.extend({
	template: ".jNoPhotoTmpl",
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
				return that.options.isSelected || (that.model.attributes.fileName !== Utils.uuid4.empty() && !that.options.isDeleted);

			},
			hasServerFile: function () {
				return that.model.attributes.fileName !== Utils.uuid4.empty();
			}
		};
	},
	ui: {
		photo: "[type=file]",
		desc: "[name=desc]",
		"delete": "[name=delete]",
		cancelDelete: "[name=cancelDelete]",
		cancelSelected: "[name=cancelSelected]",
		clientPreview: ".jClientPreview",
		save: ".jSavePhoto"
	},
	initialize: function () {
		_.extend(this.options, {
			isDeleted: false,
			isSelected: false,
			selectedFile: null
		});
		if (this.model.attributes == null) {
			this.model.attributes = {
				fileName: Utils.uuid4.empty()
			};
		}
	},
	events: {
		"change @ui.photo": function () {
			var that = this;
			var reader = new FileReader();
			reader.onload = function (e) {
				that.ui.clientPreview.attr("src", e.target.result);
			};
			reader.readAsDataURL(this.ui.photo[0].files[0]);
			this.model.attributes.name = this.ui.photo[0].files[0].name;
			this.options.isSelected = true;
			this.options.selectedFile = this.ui.photo[0].files[0];
			this.render();

			App.needConfirmToLeave = true;
		},
		"click @ui.cancelSelected": function () {
			this.ui.photo.val("");
			this.options.isSelected = false;
			this.options.selectedFile = null;
			this.render();

			return false;
		},
		"click @ui.delete": function () {
			this.options.isDeleted = true;
			this.render();

			App.needConfirmToLeave = true;

			return false;
		},
		"click @ui.cancelDelete": function () {
			this.ui.photo.val("");
			this.options.isSelected = false;
			this.options.selectedFile = null;
			this.options.isDeleted = false;
			this.render();
			return false;
		},
		"click @ui.save": "save"
	},
	save: function () {
		var that = this;
		var data = that.getFile();
		if (data && data.file) {
			var fd = new FormData();
			fd.append(data.id, data.file);
			fd.append("modelId", data.modelId);
			fd.append("productId", data.productId);
			fd.append("fileName", data.fileName);
			fd.append("isMain", data.isMain);
			fd.append("description", data.description);
			var xhr = new XMLHttpRequest();
			var uploadComplete = function () {
				if (xhr.status === 200) {
					var obj = JSON.parse(xhr.response);
					App.Collections.photos.reset(obj.photos);
					that.trigger("new_mainPhoto");
				}
			};
			xhr.addEventListener("load", uploadComplete, false);
			if (!data.productId && data.modelId) {
				xhr.open("POST", "/api/productphotos/savemodelphoto/");
			} else if (data.productId && !data.modelId) {
				xhr.open("POST", "/api/productphotos/saveproductphoto/");
			}
			xhr.send(fd);


			//$.post("/api/productmodel/savephoto", JSON.stringify({file: this.options.selectedFile})).done(function (res) {
			//    console.log("saved");
			//}).fail(function (res) {
			//    console.log(res);
			//});
		}
	},

	//serializeData: function () {
	//    return _.extend({}, this.model.attributes, this.model.data, {
	//        rootUrl: this.model.rootUrl,
	//        cid: this.model.cid,
	//        structureId: this.model.structureId,
	//        isDisabled: this.model.isDisabled
	//    });
	//},
	getData: function () {
		if (this.options.isSelected) {
			this.model.attributes.fileName = Utils.uuid4.new();
		} else if (this.options.isDeleted) {
			this.model.attributes.fileName = Utils.uuid4.empty();
		}
		return _.extend(this.model.attributes, {
			description: this.ui.desc.val()
		});
	},
	getFile: function () {
		if (this.options.selectedFile !== null) {
			return {
				modelId: this.model.attributes.productModelId,
				productId: this.model.attributes.productId,
				id: this.model.attributes.id,
				fileName: this.model.attributes.fileName,
				isMain: this.model.attributes.isMain,
				file: this.options.selectedFile,
				description: this.ui.desc.val()
			};
		} else {
			return {
				modelId: this.model.attributes.productModelId,
				productId: this.model.attributes.productId,
				id: this.model.attributes.id,
				fileName: this.model.attributes.fileName,
				isMain: this.model.attributes.isMain
			};
		}
	}
});

App.Views.ProductPhotos = Marionette.CollectionView.extend({
	childView: App.Views.ItemPhoto,
	emptyView: App.Views.ItemNoPhoto
});