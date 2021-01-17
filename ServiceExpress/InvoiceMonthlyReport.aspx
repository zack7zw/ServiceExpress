<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="InvoiceMonthlyReport.aspx.cs" Inherits="ServiceExpress.InvoiceMonthlyReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
           .calendar {
            background: url(images/calendar.png) no-repeat;
            padding-left: 33px;
            border: 1px solid #ccc;
            border-radius: 10px;
            margin: 2%;
        }

        .unwatermarked {
            height: 18px;
            width: 148px;
        }

        .watermarkedcalendar {
            height: 20px;
            width: 150px;
            padding-left: 33px;
            border-radius: 10px;
            border: 1px solid #BEBEBE;
            background-position-x: 33px;
            color: Gray;
            background: url(images/calendar.png) no-repeat;
            margin: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <br />
       <br />
       <table class="auto-style2">
             <tr>
                    <td>
                    <asp:TextBox ID="txtDOB" runat="server" CssClass="calendar" Height="30" Width="250"></asp:TextBox>
                    <asp:CalendarExtender ID="txtDOBCalendarExtender" runat="server" TargetControlID="txtDOB" DaysModeTitleFormat="MM/dd/yyyy">
                    </asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDOB" WatermarkCssClass="watermarkedcalendar" WatermarkText="Start Date">
                    </asp:TextBoxWatermarkExtender>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="calendar" Height="30" Width="250"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1" DaysModeTitleFormat="MM/dd/yyyy">
                    </asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TextBox1" WatermarkCssClass="watermarkedcalendar" WatermarkText="End Date">
                    </asp:TextBoxWatermarkExtender>
                    <br />
                </td>
             </tr>
             <tr>
                 <td>&nbsp;</td>
                 <td>
                     <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ServiceExpressConnectionString %>"></asp:SqlDataSource>
                 </td>
             </tr>
         </table>
     <h1 style="text-align: center; background-color: limegreen;">Net Sales </h1>
       <asp:LineChart ID="LineChart2" runat="server" Style="display: none" ChartHeight="300" ChartWidth="450"
            ChartTitle="Daily Sale of Purchase Order"
            ChartType="Stacked" ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9"
            ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" AreaDataLabel=" dollar">
      
        </asp:LineChart>
         <asp:Chart ID="Chart1" runat="server" Style="display: none" DataSourceID="SqlDataSource1">
             <series>
                 <asp:Series ChartType="Line" Name="Series1" XValueMember="Invoice Date" YValueMembers="Total Price">
                 </asp:Series>
             </series>
             <chartareas>
                 <asp:ChartArea Name="ChartArea1">
                 </asp:ChartArea>
             </chartareas>
         </asp:Chart>
    <br/>
       <asp:Button ID="btnBack" CssClass="btn btn-default" runat="server" Text="Back" OnClick="btnBack_Click" />
       <asp:Button ID="Button2" CssClass="btn btn-default" runat="server" Text="PDF" OnClick="btnPDF_Click" />
</asp:Content>
