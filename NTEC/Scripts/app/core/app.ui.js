/*** 
* Used for defining the CORR UI
* @module UI
* @namespace CORR
*/

SampleNS.namespace("UI");

SampleNS.UI = (function () {
    "use strict";
    var msgsContainer = new Object();
    var stack = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
    var classStack = "stack-bottomright";
    if (window.CORRLanguageId === "1") {
        stack = { "dir1": "up", "dir2": "right", "spacing1": 0, "spacing2": 0, "firstpos1": 25 };
        classStack = "stack-bottomleft";
    }
        
    return {

        showRoasterMessage: function (message, type, _delay) {
            if (!_delay)
                _delay = 3000;

            var opts = {
                title: "Over Here",
                text: "",
                addclass: classStack,
                stack: stack,
                history:false,
                shadow: false,
                delay: _delay
            };
              
            switch (type) {
                case Enums.MessageType.Error:

                    opts.title = SampleNS.Resources.Msg_Error;
                    opts.text = message;
                    opts.type = "error";

                    break;
                case Enums.MessageType.Info:
                    opts.title = SampleNS.Resources.Msg_Information;
                    opts.text = message;
                    opts.type = "info";
                    break;
                case Enums.MessageType.Success:
                    opts.title = SampleNS.Resources.Messages.Success;
                    opts.text = message;
                    opts.type = "success";
                    break;
                case Enums.MessageType.Warning:
                    opts.title = SampleNS.Resources.Msg_Warning
                    opts.text = message;
                    opts.type = "notice";
                    break;
            }
            $.pnotify(opts);

        },
        showMessage: function (containerselector, message, type, _delay) {

            if (type == Enums.MessageType.Loading || message == "")
                return;

            this.showRoasterMessage(message, type, _delay);
            
            return;

            var $container = $(containerselector);
            var d = new Date();
            var n = d.getMilliseconds();

            if ($container != null && $container.length > 0) {
                if (message == "") {
                    $container.hide();
                    return false;
                }
            }

            if (type == Enums.MessageType.Error) {
                $container.text(message).attr("class", "msgError").show();
            }
            else if (type == Enums.MessageType.Success) {
                $container.text(message).attr("class", "msgSuccess").show();
            }
            else if (type == Enums.MessageType.Loading) {
                $container.text(message).attr("class", "msgLoading").show();
            }
            else if (type == Enums.MessageType.Warning) {
                $container.text(message).attr("class", "msgWarning").show();
            }
            if (type == Enums.MessageType.Error || type == Enums.MessageType.Success || type == Enums.MessageType.Warning) {
                if ($container != null && $container.length > 0) {
                    $container.click(function () {
                        $(this).fadeOut(500);
                    });
                    //debugger;
                    var t = msgsContainer[containerselector];
                    if (t) clearTimeout(t);

                    t = setTimeout(function () {
                        var t = n;
                        $container.fadeOut(500);
                    }, 5000);
                    msgsContainer[containerselector] = t;
                } //End of container != null
            } // End of type ==
        }, //End of ShwoMessage Function
        

    };
} ());

