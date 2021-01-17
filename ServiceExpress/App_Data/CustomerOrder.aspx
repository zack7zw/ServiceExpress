<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="CustomerOrder.aspx.cs" Inherits="ServiceExpress.CustomerOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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

        .customCalloutStyle div, .customCalloutStyle td {
            border: solid 1px Black;
            background-color: #9C2052;
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            color: White;
        }
         .modalBackgroundLHZ {
            background-color: Black;
            filter: alpha(opacity=80);
            opacity: 0.7;
        }

        .modalPopupLHZ {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-bottom: 10px;
            width: 322px;
            height: 262px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center; background-color: #9966FF;">Customer Order</h1>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    Search Option:&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Show Fliter" OnClick="Button1_Click" />
    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
    <br />
    <asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvPO_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="purOrderNo" HeaderText="Order ID" />
            <asp:BoundField DataField="orderCompany" HeaderText="Customer ID" />
            <asp:BoundField DataField="orderDate" HeaderText="Order Date" />
            <asp:BoundField DataField="totalPrice" HeaderText="TotalPrice" />
            <asp:BoundField DataField="orderPersonnel" HeaderText="Admintration" />
            <asp:BoundField DataField="statusPO" HeaderText="Status" />
            <asp:CommandField ShowSelectButton="True" HeaderText="Acknowledge" SelectText="Acknowledge" />
        </Columns>
    </asp:GridView>
    <asp:ModalPopupExtender ID="gvPO_ModalPopupExtender" runat="server" TargetControlID="LinkButton1"
          DropShadow="false" PopupControlID="Panel1"
                                        BackgroundCssClass="modalBackgroundLHZ" CancelControlID="lbClose">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblResultPO" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="322px" Height="262px" CssClass="modalPopupLHZ" Style="display: none">
        <asp:LinkButton ID="lbClose" runat="server">Close</asp:LinkButton>
        <asp:DropDownList ID="ddlStatus" Height="30" Width="250" runat="server">
            <asp:ListItem>Please Select Status</asp:ListItem>
            <asp:ListItem>Create</asp:ListItem>
            <asp:ListItem>Update</asp:ListItem>
            <asp:ListItem>Delete</asp:ListItem>
        </asp:DropDownList>
        <asp:DropShadowExtender ID="ddlStatus_DropShadowExtender" runat="server" TargetControlID="ddlStatus">
        </asp:DropShadowExtender>
 
        <br />
        <br />
        <asp:TextBox ID="txtPOFromDate" runat="server" Height="30" Width="250" CssClass="unwatermarked calendar"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="txtPOFromDate_TextBoxWatermarkExtender" runat="server" TargetControlID="txtPOFromDate" WatermarkText="From Date" WatermarkCssClass="watermarkedcalendar">
        </asp:TextBoxWatermarkExtender>
        <asp:CalendarExtender ID="txtPOFromDate_CalendarExtender" runat="server" TargetControlID="txtPOFromDate" Format="MM/dd/yyyy">
        </asp:CalendarExtender>
        <br />
        <br />
        <asp:TextBox ID="txtPOEndDate" runat="server" Height="30" Width="250" CssClass="unwatermarked calendar"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="txtPOEndDateTextBoxWatermarkExtender1" runat="server" TargetControlID="txtPOEndDate" WatermarkText="End Date" WatermarkCssClass="watermarkedcalendar">
        </asp:TextBoxWatermarkExtender>
        <asp:CalendarExtender ID="txtPOEndDateCalendarExtender1" runat="server" TargetControlID="txtPOEndDate" Format="MM/dd/yyyy">
        </asp:CalendarExtender>
        <br />
        <br />
        <asp:DropDownList ID="ddlUsers" Height="30" Width="250" runat="server">
            <asp:ListItem>Please Select Users</asp:ListItem>
            <asp:ListItem>User1</asp:ListItem>
            <asp:ListItem>User2</asp:ListItem>
        </asp:DropDownList>
        <asp:DropShadowExtender ID="ddlUsers_DropShadowExtender" runat="server" TargetControlID="ddlUsers">
        </asp:DropShadowExtender>

        <br />
        <br />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </asp:Panel>
</asp:Content>
