
SampleNS.namespace("UI.Common");

SampleNS.UI.Common = (function () {
    "use strict";
    //Private static Members

    var callbacks = {

    };

    //Public static Members
    return {

        initialisePage: function () {

        } // initialisePage
        , registerCallbacks: function (cbacks) {
            callbacks = $.extend(callbacks, cbacks);
            return this;
        } //registerCallbacks
    };
}());

