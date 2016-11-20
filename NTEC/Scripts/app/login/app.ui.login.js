/// <reference path="../../_references.js" />

/*** 
* Used for defining the CORR UI Home
* @module Home
* @namespace SampleNS.UI
*/

SampleNS.namespace("UI.Login");

SampleNS.UI.Login = (function () {
    "use strict";

    var _isInitialized = false;
    var _LangId = 0;

    function initialiseControls() {


        if (_isInitialized == false) {
            _isInitialized = true;
         

            bindEvents();
            
        }

    }

    function bindEvents() {

        $("#btnLogin").click(function () {
            loginUser();
        });
        
        // Keyboard
        $('#txtPass').bind('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                ///SampleNS.UI.Logon.hideMessages();
                loginUser();
                e.preventDefault();
                return false;
            }
        });
        $('#txtUser').bind('keypress', function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                // SampleNS.UI.Logon.hideMessages();
                loginUser();
                e.preventDefault();
                return false;
            }
            
        });

        $('#btnArabic').click(function () {
           
            if (_LangId != 1 || _LangId == 1) {
                setLanguage(1);
            }
                        
            return false;
        });

        $('#btnEnglish').click(function () {

            //alert(window.CORRLanguageId);
            if (_LangId != 2 || _LangId == 2) {
                setLanguage(2);
            }
                        

            return false;
        });

        loadExistingInfo();

    } // End of BindEvents()


  


    function setLanguage(langId) {
        
        //=============== Setting Language ============

        var url = "Login/SetLanguage?landId=" + langId;

        SampleNS.Globals.MakeAjaxCall("GET", url, [], function (result) {
            if (result.success === true) {
              
                window.location.href = SampleNS.Resources.Views.SetCultureAtLogin ;

            } else {
                SampleNS.UI.showRoasterMessage(result.message, Enums.MessageType.Warning, 5000);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Prob_Authen_Cred, Enums.MessageType.Error);
        });
    }

    function loginUser() {
        
        var logonCredentials = getInputLogonCredentials();

        if (logonCredentials === undefined) {
            return false;
        }

        if ($("#chkkeepmeloggedin").is(":checked") == true) {
            $.cookie("UserName", logonCredentials.UserName, { expires: 5 });
            $.cookie("Password", logonCredentials.Password, { expires: 5 });
        }
        else {
            $.cookie("UserName", null);
            $.cookie("Password", null);
        }

        var login = {
            UserName: logonCredentials.UserName,
            Password: logonCredentials.Password,
        }


        var UsrObj = {};
        UsrObj.UserName = $("#txtUser").val();
        UsrObj.Password = $("#txtPass").val();
        UsrObj.RememberMe = $("#chkkeepmeloggedin").is(":checked");


        var data = JSON.stringify(UsrObj);

        var url = "Login/LoginUser";
        debugger;
        SampleNS.Globals.MakeAjaxCall("POST", url, data, function (result) {
            
            if (result.success === true) {

                SampleNS.UI.showRoasterMessage("Successfully Logged In..", Enums.MessageType.Success);

                var returnUrl = Utilities.getURLParameterByName("ReturnUrl");
                
                if (returnUrl != "")
                    window.location.href = returnUrl;
                else
                    window.location.href = SampleNS.Globals.baseURL + result.urlData;

            } else {
                SampleNS.UI.showRoasterMessage(result.message, Enums.MessageType.Warning, 5000);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Prob_Authen_Cred, Enums.MessageType.Error);
        });






    }// End of LoginUser

    function loadExistingInfo() {

        var uName = $.cookie("UserName");
        var pasd = $.cookie("Password");
       
        if (uName != null && uName != "") {

            $('#txtUser').val(uName);
            $('#txtPass').val(pasd);
            $("#chkkeepmeloggedin").attr("checked", true);
        }
    }

    function getInputLogonCredentials() {

        var userName = $('#txtUser').val(),
            password = $('#txtPass').val(),
            logonCredentials = {};

        if ($.trim(userName) === '') {

            //if (_LangId == 1) {
                
            //    SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Record_Not_Delete, Enums.MessageType.Warning, 5000);
            //}
            //else {
            //    SampleNS.UI.showRoasterMessage("You must enter a user name.", Enums.MessageType.Warning, 5000);
            //}

            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_User_Name, Enums.MessageType.Warning, 5000);
            
            
            $('#txtUser').focus();
            return undefined;
        }
        if ($.trim(password) === '') {
            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Password, Enums.MessageType.Warning, 5000);
            //if (_LangId == 1) {
            //    SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Password, Enums.MessageType.Warning, 5000);
            //}
            //else
            //{
            //    SampleNS.UI.showRoasterMessage("You must enter a password.", Enums.MessageType.Warning);
            //}
            
           
            $('#txtPass').focus();
            return undefined;
        }


        logonCredentials['UserName'] = userName;
        logonCredentials['Password'] = password;

        //console.log(logonCredentials);
        return logonCredentials;
    }
   
    function resetPage() {


        initialiseControls();

    }

    return {

        initialisePage: function () {
            initialiseControls();
        },

        readyMain: function () {
            $(function () {

                SampleNS.UI.Login.initialisePage();

            });//End of Ready Function
        },
        LangId: function (langId) {
            $(function () {
                
                _LangId = langId;

            });
        },
        resetPage: function () {
            resetPage();
        }
    };
}());
