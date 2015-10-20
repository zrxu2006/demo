//-------------------tools---------------------
//(function () {
$.messageBox = {

    showInfo: function (content) {
        $.messager.alert('信息提示 ', content, 'info');
    },
    showError: function (content) {
        $.messager.alert('错误提示 ', content, 'error');
    },
    showWarning: function (content) {
        $.messager.alert('警告提示 ', content, 'warning');
    },
    showQuestion: function (content, yes, no) {
        $.messager.confirm('提示', content, function (r) {
            if (r) {
                if (yes != undefined)
                    yes();
            }
            else {
                if (no != undefined)
                    no();
            }
        });

    },
    tips: function (config) {
        $.messager.show({
            title: config.title ? config.title : "提示",
            msg: config.msg,
            timeout: 1200,
            icon: config.icon ? config.icon : "info",
            showType: config.showType ? config.showType : 'slide',
            style: {
                right: '',
                top: document.body.scrollTop + document.documentElement.scrollTop,
                bottom: ''
            }
        });

    },
    showSuccess: function (content) {
        this.tips({
            msg: "成功！<br/>" + content
        });
    },
    showFail: function (content) {
        this.tips({
            msg: "异常！<br/>" + content,
            icon: "warning"
        });
    },
    showSysError: function (content) {
        this.tips({
            msg: "系统异常！<br/>" + content,
            icon: "error"
        });
    },
    showAjaxError: function (error) {
        this.showSysError(error, responseText);
    }
}
//function create
var buttonConfig = [
{
    name: "查看",
    click: "ASC.TEST,GetList",
    params: "'aaa',2,r"
}
]
function createDgButton(buttonConfig) {
    if (buttonConfig == undefined || buttonConfig == null || buttonConfig.length == 0)
        return "";

    var curCount = buttonConfig.length;
    var res = "<div class=\"dgbutton\">";
    for (var i = 0; i < buttonConfig.length; i++) {
        if (i != buttonConfig.length - 1) {
            if (buttonConfig[i].href == undefined)
                res += "<span><a href=\"###\"  onclick=\"" + buttonConfig[i].click + "(" + buttonConfig[i].params + ")\">" + buttonConfig[i].name + "</a></span><span>|</span>";
            else
                res += "<span><a href=\"" + buttonConfig[i].href + "\">" + buttonConfig[i].name + "</a></span><span>|</span>";
        }

        else {
            if (buttonConfig[i].href == undefined)
                res += "<span><a href=\"###\"  onclick=\"" + buttonConfig[i].click + "(" + buttonConfig[i].params + ")\">" + buttonConfig[i].name + "</a></span>";
            else
                res += "<span><a href=\"" + buttonConfig[i].href + "\">" + buttonConfig[i].name + "</a></span>";
        }
    }
    res += "</div>";
    return res;
}
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
//})


