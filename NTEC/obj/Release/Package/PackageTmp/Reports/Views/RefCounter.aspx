<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RefCounter.aspx.cs" Inherits="NTEC.Reports.Views.TestingReport" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Number Of References Report</title>
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
       
                  <div runat="server" dir="<%$ Resources: Resource, Direction %>" style="margin-bottom: 15px; margin-top: 15px">
                <div class="row">
                   <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" ID="countrLbl" Text="Counter By :" meta:resourcekey="countrLblResource1"></asp:Label>
                        <asp:DropDownList ID="ReferenceDrpLst" CssClass="form-control" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ReferenceDrpLst_SelectedIndexChanged" meta:resourcekey="ReferenceDrpLstResource1">
                          
                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">Users</asp:ListItem>
                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource3">Departments</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                   </div>
                   
                </div>
            </div>
     
            <div class="row">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ZoomMode="PageWidth"
                    SizeToReportContent="True" meta:resourcekey="ReportViewer1Resource1">
                    <LocalReport ReportPath="Reports\RefCounterByUser.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
    </form>
</body>
</html>
