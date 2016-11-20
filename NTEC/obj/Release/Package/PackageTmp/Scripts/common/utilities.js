Utilities = (function () {
    "use strict";
    return {

        LoadDropDown: function (dropdown, dataToBind, valueField, textField, defaultOptText, dataAttrField1, dataAttrField2) {
            if (defaultOptText)
            {
                if (window.appLanguageId == 1) {
                    defaultOptText = "--اختيار--";

                }
                else {

                    defaultOptText = "--Select--";
                }
            }
            else
            {
                if (window.appLanguageId == 1) {
                    defaultOptText = "--اختيار--";

                }
                else {

                    defaultOptText = "--Select--";
                }
               
            }
            var $cmb = dropdown.html('');
            $("<option value='0'>" + defaultOptText + "</option>").appendTo($cmb);

            if (dataToBind == null)
                return false;

            for (var t = 0; t < dataToBind.length; t++) {
                var val = dataToBind[t][valueField];
                var txt = dataToBind[t][textField];
                if (dataAttrField1) {
                    var extra = dataToBind[t][dataAttrField1];
                    var field2 = null;

                    if (dataAttrField2) {
                        field2 = dataToBind[t][dataAttrField2];
                    }
                    if (field2) {
                        $("<option value='" + val + "'>" + txt + "</option>").data(dataAttrField1, extra).data(dataAttrField2, field2).appendTo($cmb);
                    }
                    else {
                        $("<option value='" + val + "' dataAttrField1='" + extra + "'>" + txt + "</option>").data(dataAttrField1, extra).appendTo($cmb);
                    }

                }
                else {
                    $("<option value='" + val + "'>" + txt + "</option>").appendTo($cmb);
                }



            }
        }, //End of LoadDropDown

        IsValidDate: function (dateToValidate) {
            var result = Date.parse(dateToValidate);
            if (result) {
                return Date.parse(new Date(result).toString("M/d/yyyy"));
            }
            else
                return null;
        },
        DiffofDatesInMin: function (date1, date2) {
            var diffMs = (date1 - date2); // milliseconds
            var diff = Math.round(diffMs / 60000); //Get diff in Minutes
            return diff;
        },

        IsValidTime: function (time) {
            var IsMatch = time.match(/^(0?[1-9]|1[012])(:[0-5]\d) [APap][mM]$/);
            if (IsMatch == null)
                return false;
            else
                return true;

        },
        ValidateEmailAddress: function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
            return pattern.test(emailAddress);
        },
        setHandlebarTemplate: function (templateSelector, selectorToApply, data) {
            $(selectorToApply).html('');
            var source = $(templateSelector).html();
            var template = Handlebars.compile(source);

            var html = template(data);
            $(selectorToApply).html(html);
        },
        DateTimeDifferenceFloor: function (enddate, startdate, tovalue) {
            return DateTimeDifferenceInPST(startdate, enddate, tovalue, false);
        },
        DateTimeDifferenceCeil: function (enddate, startdate, tovalue) {
            return DateTimeDifferenceInPST(startdate, enddate, tovalue, true);
        },
        DateTimeDifferenceInPST: function (startdate, enddate, tovalue, IsCiel) {
            var d1 = Date.UTC(startdate.getFullYear(), startdate.getMonth(), startdate.getDate(), startdate.getHours(), startdate.getMinutes(), startdate.getSeconds());
            var d2 = Date.UTC(enddate.getFullYear(), enddate.getMonth(), enddate.getDate(), enddate.getHours(), enddate.getMinutes(), enddate.getSeconds());

            if (IsCiel) {
                return Math.ceil((d2 - d1) / tovalue);
            }
            else {
                return Math.floor((d2 - d1) / tovalue);
            }
        },
        getURLParameterByName: function (name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    };
}());