$.fn.hideOnClickExcept = function (except, effect) {
    var that = this;
    var what = that.selector;

    if (!$.isArray(except)) {
        except = $.makeArray(except);
    }

    $(document).click(function (e) {
        var target = $(e.target);

        var escape = false;
        if (target.parents(what).length || target.is(what)) {
            return;
        }
        
        $.each(except, function (index, selector) {
            if(escape){return;}
            if (target.parents(selector).length || target.is(selector)) {
                escape = true;
            }
        });
        
        if (escape) {
            return;
        }
        
        if(effect !== undefined)
        {
            effect(that);
        }
        else {
            that.hide();
        }
    });
};