<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NumberOFReference.aspx.cs" Inherits="NTEC.Reports.Views.NumberOFReference" culture="auto" meta:resourcekey="PageResource1" uiculture="auto"  %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Number Of References Report</title>
   
</head>
<body>
     <form id="form1" runat="server">

        <div  class="container">
            <div runat="server" dir="<%$ Resources: Resource, Direction %>" style="margin-bottom: 15px; margin-top: 15px">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label runat="server" ID="UserLbl" Text="User Name:" meta:resourcekey="UserLblResource1"></asp:Label>
                           
                            <asp:DropDownList ID="UsersDrpLst" CssClass="form-control" runat="server" meta:resourcekey="UsersDrpLstResource1"  ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label runat="server" ID="DepartLbl" Text="Department :" meta:resourcekey="DepartLblResource1"></asp:Label>
                           
                            <asp:DropDownList ID="DepartDplst" CssClass="form-control" runat="server" meta:resourcekey="DepartDplstResource1" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            
                             <asp:Label runat="server" ID="TypeLbl" Text="Type :" meta:resourcekey="TypeLblResource1"></asp:Label>
                            <asp:DropDownList ID="TypeDrpLst" CssClass="form-control" runat="server" meta:resourcekey="TypeDrpLstResource1"></asp:DropDownList>
                        </div>
                    </div>





                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                       
                             <asp:Label runat="server" ID="FromLbl" Text="From :" meta:resourcekey="FromLblResource1"></asp:Label>


                            <div class="input-group">
                                <asp:TextBox ID="FromDate" CssClass="form-control" runat="server" meta:resourcekey="FromDateResource1"  />
                                <span class="input-group-addon" id="FrmButton" style="font-size: 12px;">
                                    <button><span class="glyphicon glyphicon-calendar"></span></button>
                                </span>
                            </div>

                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="FromDate" PopupButtonID="FrmButton" Format="MM-dd-yyyy" BehaviorID="CalendarExtender1"></ajaxToolkit:CalendarExtender>

                        </div>

                    </div>
                    <div class="col-md-4">
                         <div class="form-group">
                            
                               <asp:Label runat="server" ID="ToLbl" Text="To :" meta:resourcekey="ToLblResource1"></asp:Label>
                             <div class="input-group">
                                <asp:TextBox ID="ToDate" CssClass="form-control" runat="server" meta:resourcekey="ToDateResource1" />
                                <span class="input-group-addon" id="ToButton" style=" font-size: 12px;">
                                    <button  ><span class="glyphicon glyphicon-calendar"></span></button>
                                </span>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                TargetControlID="ToDate" PopupButtonID="ToButton" Format="MM-dd-yyyy" BehaviorID="CalendarExtender2"></ajaxToolkit:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                     
                              <asp:Label runat="server" ID="CompanyLbl" Text="Company :" meta:resourcekey="CompanyLblResource1"></asp:Label>
                            <asp:TextBox ID="CompanyTxt" CssClass="form-control" runat="server" meta:resourcekey="CompanyTxtResource1"  />


                        </div>
                    </div>



                </div>
                <div class="row">
                    <div class="text-center">
                        <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Show Report" OnClick="Button1_Click" meta:resourcekey="Button1Resource1"  />
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ZoomMode="PageWidth"
                    SizeToReportContent="True" meta:resourcekey="ReportViewer1Resource1" >
                    <LocalReport ReportPath="Reports\NumberOfGeneratedReferences.rdlc">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
    </form>
</body>
</html>
