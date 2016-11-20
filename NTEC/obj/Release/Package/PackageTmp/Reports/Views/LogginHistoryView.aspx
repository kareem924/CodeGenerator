<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogginHistoryView.aspx.cs" Inherits="NTEC.Reports.Views.WebForm1" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logging History Report</title>
   
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div runat="server" dir="<%$ Resources: Resource, Direction %>" style="margin-bottom: 15px; margin-top: 15px">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                       
                            <asp:Label runat="server" id="usrNameLbl" Text="User Name :" meta:resourcekey="usrNameLblResource1"></asp:Label>
                            <asp:DropDownList ID="UsersDrpLst" CssClass="form-control" runat="server" meta:resourcekey="UsersDrpLstResource1" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                           
                            <asp:Label runat="server" id="LogginTypeLbl" Text="Logging Type :" meta:resourcekey="LogginTypeLblResource1"></asp:Label>
                            <asp:DropDownList ID="TypesUsrsDplst" CssClass="form-control" runat="server" meta:resourcekey="TypesUsrsDplstResource1" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                    <div class="form-group">
                          
                           <asp:Label runat="server" id="FromLbl" Text="From :" meta:resourcekey="FromLblResource1"></asp:Label>


                            <div class="input-group">
                                <asp:TextBox ID="FromDate" CssClass="form-control" runat="server" meta:resourcekey="FromDateResource1" />
                                <span class="input-group-addon" id="FrmButton" style=" font-size: 12px;">
                                    <button ><span class="glyphicon glyphicon-calendar"></span></button>
                                </span>
                            </div>

                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="FromDate" PopupButtonID="FrmButton" Format="MM-dd-yyyy" BehaviorID="CalendarExtender1"></ajaxToolkit:CalendarExtender>

                        </div>
                    </div>
                    <div class="col-md-3">
                            <div class="form-group">
                            
                               <asp:Label runat="server" id="ToLbl" Text="To :" meta:resourcekey="ToLblResource1"></asp:Label>
                             <div class="input-group">
                                <asp:TextBox ID="ToDate" CssClass="form-control" runat="server" meta:resourcekey="ToDateResource1"  />
                                <span class="input-group-addon" id="ToButton" style=" font-size: 12px;">
                                    <button ><span class="glyphicon glyphicon-calendar"></span></button>
                                </span>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                TargetControlID="ToDate" PopupButtonID="ToButton" Format="MM-dd-yyyy" BehaviorID="CalendarExtender2"></ajaxToolkit:CalendarExtender>
                        </div>
                    </div>


             

                    <div class="row">
                        <div class="text-center" >
                            <asp:Button ID="Button1" CssClass="btn btn-success" OnClientClick="return checkForm();" runat="server" Text="Show Report" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
            
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="#FFFBF7" ZoomMode="PageWidth"
                    SizeToReportContent="True"
                    Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" meta:resourcekey="ReportViewer1Resource1" >
                    <LocalReport ReportPath="Reports\LogginHistoryView.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
    </form>

</body>

</html>

<script src="../../Scripts/jquery-2.2.3.min.js" type="text/javascript"></script>
<script>

    function checkForm() {
        var enddate = $('#ToDate').val();
        var splitEndDate = enddate.split('-');
        var endDate = new Date(splitEndDate[2], splitEndDate[0] - 1, splitEndDate[1]);
        var startdate = $('#FromDate').val();
        var splitStartDate = startdate.split('-');
        var startDate = new Date(splitStartDate[2], splitStartDate[0] - 1, splitStartDate[1]);
        var message = "<%=Resources.Resource.invalidPeriod%>";
        if (startDate > endDate) {
            alert(message);
            return false;
        } else {
            return true;
        }
    };
</script>