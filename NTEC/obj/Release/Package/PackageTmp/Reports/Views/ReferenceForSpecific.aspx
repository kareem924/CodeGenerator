<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReferenceForSpecific.aspx.cs" Inherits="NTEC.Reports.Views.ReferenceForSpecific" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NTEC/Reports</title>
   
</head>
<body>
     <form id="form1" class="form-inline" runat="server">
        <div class="container">
            <div runat="server" dir="<%$ Resources: Resource, Direction %>" style="margin-bottom: 15px; margin-top: 15px">
                <div class="row text-center">
                   
                    <div class="form-group">
                        <label>Reference Name : </label>
                        <asp:DropDownList ID="ReferenceDrpLst" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                   
                    <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Show Report" OnClick="Button1_Click" />
                </div>
            </div>
            <div class="row">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="#FFFBF7" ZoomMode="PageWidth"
                    SizeToReportContent="True"
                    Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    <LocalReport ReportPath="Reports\ReferenceForSpecific.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
    </form>
</body>
</html>
