/// <reference path="../../_references.js" />

/*** 
* Used for defining the CORR UI Home
* @module Home
* @namespace SampleNS.UI
*/

SampleNS.namespace("UI.Template");

SampleNS.UI.Template = (function () {
    "use strict";

    var _isInitialized = false;
   


    function initialiseControls() {


        if (_isInitialized == false) {
            _isInitialized = true;
            setContentEditor();
            GetAllTemplates();
            bindEvents();

        }

    }

    function bindEvents() {
        $('#btnOpenTemplate').click(function (event) {

            clearTemplate();
            $("#divTemplate_Popup").modal({
                show: true,
                backdrop: 'static'
            });
            event.preventDefault();
            return false;
        });

        $('#btnSaveTemplate').click(function () {
            SaveTemplate();
            //
            clearTemplate();
        });

        $('#btnCancelTemplate').click(function () {
            $("#divTemplate_Popup").modal('hide');
            return false;
        });

        $("#btnDeleteTemplate").click(function () {
            var selectedRowId = $('#divTemplateGrid').getGridParam('selrow');
            if (selectedRowId == null)
            {
                SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Record_Selection_Delete, Enums.MessageType.Warning, 5000);
            
                return false;
            }
            
            bootbox.confirm(SampleNS.Resources.Msg_Sure_Cnfrm_Delete, function (uResp) {
                if (uResp) {                    
                    if (selectedRowId != null) {
                        deleteDataById(selectedRowId);
                    }                   
                }
            });

           
            return false;
        });

    } // End of BindEvents()
    function setContentEditor() {

        //$('#corrEditor').ckeditor();


        CKEDITOR.replace('corrEditorTemplate',
            {
                // Define the toolbar groups as it is a more accessible solution.
                toolbarGroups: [
	                { name: 'document', groups: ['mode', 'document', 'doctools'] },
	                { name: 'clipboard', groups: ['clipboard', 'undo'] },
	                { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
                    { name: 'tools' },
	                { name: 'others' },
	                '/',
	                { name: 'styles' },
	                { name: 'colors' },
                    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
                    { name: 'insert' }
                ],
                height: ($(document).height() - 500),
                // Remove the redundant buttons from toolbar groups defined above.
                removeButtons: 'Strike,Subscript,Superscript,Anchor,Styles,Save,NewPage,Templates,' +
                    'Find,Replace,selection,spellchecker,Spellchecker,Flash,Image,Table,Iframe,SetLanguage,Language',

            });

    }

    function SaveTemplate() {
        
        //var url = "Template/SaveTemplate?intTemplateId=" + $('#txtTemplateIdPop').val() + "&TemplateTitle=" + $('#txtTemplateTitle').val() + "&TemplateData=" + CKEDITOR.instances.corrEditorTemplate.getData() + "";
        var url = "Template/SaveTemplate";                
        var data = JSON.stringify({ TemplateId: $('#txtTemplateIdPop').val(), TemplateTitle: $('#txtTemplateTitle').val(), TemplateData: CKEDITOR.instances.corrEditorTemplate.getData() });

        SampleNS.Globals.MakeAjaxCall("POST", url, data, function (result) {
            if (result.IsSaveTemplate === true) {

                SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Template_Saved, Enums.MessageType.Success);
                GetAllTemplates();
            }
            else {
                SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Record_Not_Delete, Enums.MessageType.Error, 5000);
            }
            
            $("#divTemplate_Popup").modal('hide');
        }, function (xhr, ajaxOptions, thrownError) {

        },false);
    }

    function clearTemplate() {
        $("#txtTemplateTitle").val("");
        $("#txtTemplateIdPop").val("-1");
    }

    function GetAllTemplates() {
        var url = "Template/GetAllTemplates";

        SampleNS.Globals.MakeAjaxCall("GET", url, "{}", function (result) {
            if (result.error === true) {

                SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Data_Not_Load, Enums.MessageType.Error, 5000);
            }
            else {
                $("#divTemplateGrid").GridUnload(result);
                LoadTemplateGrid(result);
                var RecordCount = $('#divTemplateGrid').getGridParam("reccount");
                $('#divTemplateTitle').html(FolderName + " [" + RecordCount + "]");


            }

        }, function (xhr, ajaxOptions, thrownError) {

            //debugger;
            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Prob_Authen_Cred, Enums.MessageType.Error);
        });
    }

    function LoadTemplateGrid(result) {
        debugger;
       
        $("#divTemplateGrid").jqGrid({
            datatype: 'jsonstring',
            datastr: result,
            jsonReader: {
                root: "response", //array containing actual data
                repeatitems: false,
                id: "TemplateId" //index of the column with the PK in it
            },
            // colNames: ['Template Id', 'Template Name', 'Template View', 'Created Date'],
            colNames: [SampleNS.Resources.Messages.TemplateId, SampleNS.Resources.Messages.TemplateName, SampleNS.Resources.Messages.TemplateView, SampleNS.Resources.Messages.CreatedDate],
            colModel: [
               { name: 'TemplateId', index: 'TemplateId', width: 80, sorttype: 'int', align: SampleNS.Resources.Messages.AlignColumn },
                { name: 'TemplateTitle', index: 'TemplateTitle', width: 90, align: SampleNS.Resources.Messages.AlignColumn },
                { name: 'TemplateData', index: 'TemplateData', width: 90, hidden: true },
                { name: 'CreateDate', index: 'CreateDate', hidden: false, width: 100, formatter: "date", formatoptions: { srcformat: "ISO8601Long", newformat: "d M, Y" }, align: SampleNS.Resources.Messages.AlignColumn }


            ],
            rowNum: 10,
            autowidth: true,
            height: '100%',
            rowList: [10, 20, 30, 40, 50],
            pager: jQuery('#pagerdivTemplateGrid'),
            sortname: 'TemplateId',
            viewrecords: true,    // disable current view record text like 'View 1-10 of 100'                                                                                 
            sortorder: "desc",
            caption: "Template",
            ondblClickRow: function (rowid, iRow, iCol, e) {
                var data = $('#divTemplateGrid').getRowData(rowid);

                editRowData(data);

            },            
                onSelectRow: function (rowid, iRow, iCol, e) {
                
                    var data = $('#divTemplateGrid').getRowData(rowid);
                  
                
                },
            shrinkToFit: true
        }).navGrid('#pagerdivTemplateGrid', { edit: false, add: false, del: false, search: false });
        $("#divTemplateGrid").setGridParam({ sortname: 'TemplateId', sortorder: 'desc' })
.trigger('reloadGrid');


        var ViewPage = $("#pagerdivTemplateGrid").find(".ui-paging-info").html().replace("View", SampleNS.Resources.Messages.View).replace("of", SampleNS.Resources.Messages.Of);


        var height = $(window).height() - 290;
        $('.ui-jqgrid-bdiv').height(height);
        $('.ui-jqgrid .ui-jqgrid-bdiv').css({ 'overflow-y': 'auto' });

        $(window).on("resize", function () {
            var newWidth = $(".ui-jqgrid-htable").closest(".ui-jqgrid").parent().width();
            $("#divTemplateGrid").jqGrid("setGridWidth", newWidth, true);
        });


        $("#pagerdivTemplateGrid").find(".ui-paging-info").empty();
        $("#pagerdivTemplateGrid").find(".ui-paging-info").append(ViewPage);
    }

    function deleteDataById(RowID) {

        
        var data = $('#divTemplateGrid').getRowData(RowID);
        var url = "Template/DeleteTemplate?intTemplateId=" + data.TemplateId + "";


        SampleNS.Globals.MakeAjaxCall("POST", url, "{}", function (result) {
            if (result.success === true) {

                if (result.IsDeleteTemplate == true) {
                    SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Record_Delete, Enums.MessageType.Success)
                }

                GetAllTemplates();
            } else {
                SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Record_Not_Delete, Enums.MessageType.Error, 5000);
            }
        }, function (xhr, ajaxOptions, thrownError) {
            //debugger;
            SampleNS.UI.showRoasterMessage(SampleNS.Resources.Msg_Prob_Authen_Cred + ' ' + xhr.responseText, Enums.MessageType.Error);
        });

        //bootbox.confirm("Are you sure to delete '" + data.TemplateName + "'?", function (uResp) {

        //    if (uResp == true) {


        //    }


        //});


    }

    function editRowData(data) {
       
        $("#txtTemplateIdPop").attr('readonly', true);
        $("#txtTemplateIdPop").val(data.TemplateId);
        $("#txtTemplateTitle").val(data.TemplateTitle);
        CKEDITOR.instances.corrEditorTemplate.setData(data.TemplateData);

        $("#divTemplate_Popup").modal({
            show: true,
            backdrop: 'static'
        });

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
                SampleNS.UI.Template.initialisePage();


            });//End of Ready Function
        },
        resetPage: function () {
            resetPage();
        }
    };
}());
