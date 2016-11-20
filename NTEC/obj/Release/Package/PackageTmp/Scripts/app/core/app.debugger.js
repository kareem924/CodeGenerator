/*** 
* Used for defining the CORR debugger
* @module Debugger
* @namespace CORR
*/


SampleNS.namespace("Debugger");

SampleNS.Debugger = (function () {
    "use strict";
    return {
        log: function (message) {
            try {
                if(console)
                    console.log(message);
            } catch (exception) {
                return;
            }
        }
    };
} ());

