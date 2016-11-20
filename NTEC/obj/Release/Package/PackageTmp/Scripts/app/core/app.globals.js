/*** 
* Used for defining the CORR UI
* @module UI
* @namespace CORR
*/

SampleNS.namespace("Globals");

SampleNS.Globals = (function () {
    "use strict";
    var counter = 0;
    var webapiurlbase = window.ApplicationBasePath + "aapi/";
    var normalurlbase = window.ApplicationBasePath;

    $.ajaxSetup({
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        cache: false
    });

    function ShowSpinner() {
        var opts = {
            lines: 11, // The number of lines to draw
            length: 3, // The length of each line
            width: 3, // The line thickness
            radius: 24, // The radius of the inner circle
            corners: 0, // Corner roundness (0..1)
            rotate: 30, // The rotation offset
            color: '#FFF', // #rgb or #rrggbb
            speed: 0.6, // Rounds per second
            trail: 64, // Afterglow percentage
            shadow: true, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };
        $("#divProgressStatus").spin(opts);
    }
    function HideSpinner() {
        $("#divProgressStatus").spin(false);
    }

    return {
        baseURL: normalurlbase
        , ShowSpinner: function (bShowOverlay) {
            $('#divProgressStatus').show();
            if (bShowOverlay) {
                $('#divProgressOverlay').show();
            }
            ShowSpinner();
        }
        , HideSpinner: function () {
            $('#divProgressStatus').hide();
            $('#divProgressOverlay').hide();
            HideSpinner();
        }
        , MakeAjaxCall: function (httpmethod, URL, data, successCallback, failureCallback, aynch, showProgress) {

            
            if (typeof aynch == 'undefined')
                aynch = true;

            if (typeof showProgress == 'undefined')
                showProgress = true;

            if (showProgress) {
                counter++;
                if (counter > 0) {
                    $('#divProgressOverlay').show();
                    $('#divProgressStatus').show();
                    ShowSpinner();
                }
            }

            var urltocall = normalurlbase + URL;

            var defObj = $.ajax({
                type: httpmethod, //"POST"
                url: urltocall,
                data: data,
                async: aynch,
                beforeSend: function (jqXHR, settings) {

                },
                success: function (resp) {

                    try {
                        var result = JSON.parse(resp);
                        if (successCallback)
                            successCallback(result);
                    } catch (err) {
                    }
                },
                error: function (err, type, httpStatus) {
                    if (err.status == 406 || err.status == 401) {
                        var retPath = "";
                        if (window.location.pathname) {
                            retPath = "?ReturnURL=" + (location.pathname + location.search).substr(1);
                        }
                        
                        window.location.href = SampleNS.Resources.Views.LoginURL + retPath;

                        //if (retPath.indexOf("?docId=") > -1) {

                        //    window.location.href = SampleNS.Resources.Views.LoginURL + retPath.replace("CORRweb/", "");
                        //}
                        //else {
                        //    window.location.href = SampleNS.Resources.Views.LoginURL;
                        //}
             
                       
                       // window.location.href = SampleNS.Resources.Views.LoginURL;
                        return;
                    }
                    if (failureCallback)
                        failureCallback(err, type, httpStatus);
                    else {
                        //alert(err.status + " - " + err.responseText + " - " + httpStatus);
                    }

                },
                complete: function () {
                    if (showProgress) {
                        counter--;
                        if (counter <= 0) {
                            $('#divProgressOverlay').hide();
                            $('#divProgressStatus').hide();
                            HideSpinner();
                        }
                    }
                }
            });

            return defObj;
        } //End of MakeAjaxCall



        , GetControllerPath: function (actionName, controllerName) {
            return "".format("{0}{1}/{2}", SampleNS.Globals.baseURL, controllerName, actionName);
        }
    };
}());

