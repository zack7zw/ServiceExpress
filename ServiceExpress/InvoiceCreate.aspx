<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="InvoiceCreate.aspx.cs" Inherits="ServiceExpress.InvoiceCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            width: 347px;
        }
        .auto-style5 {
            width: 347px;
            height: 30px;
        }
        .auto-style6 {
            height: 30px;
        }
        .auto-style7 {
            width: 159px;
            height: 30px;
        }
        .auto-style8 {
            width: 159px;
        }
        
        .calendar {
            background: url(Images/calendar.png) no-repeat;
            padding-left: 33px;
            border: 1px solid #ccc;
            border-radius: 10px;
            margin: 2%;
        }
    .auto-style9 {
        width: 317px;
        height: 30px;
    }
    .auto-style10 {
        width: 208px;
    }
        .auto-style13 {
            width: 317px;
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center">
        Create Invoice</h1>
    <table class="auto-style2">
        <tr>
            <td class="auto-style7">Search by Company:</td>
            <td class="auto-style9"><asp:TextBox ID="tbSearch"  runat="server"></asp:TextBox>
            </td>
            <td class="auto-style5" colspan="2">&nbsp;</td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style8">Filter by Date:</td>
            <td class="auto-style13"><asp:TextBox ID="tbByDate" class="calendar form-control" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style3">To:
                <asp:TextBox ID="tbToDate" runat="server" class="calendar form-control"   ></asp:TextBox>
            </td>
            <td class="auto-style3">
                <asp:Button ID="btnSearch" runat="server" Text="Filter" CssClass="btn btn-primary" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
        <asp:GridView ID="gvDO" align="center" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="deliveryNo" OnSelectedIndexChanged="gvDO_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="gvDO_PageIndexChanging" PageSize="5" >
            <Columns>
                <asp:BoundField HeaderText="DO ID" DataField="deliveryNo" />
                <asp:BoundField HeaderText="Company" DataField="companyName" />
                <asp:BoundField HeaderText="Personnel" DataField="orderPersonnel" />
                <asp:BoundField HeaderText="Delivery Date" DataField="doDate" />
                <asp:BoundField HeaderText="Total Amount" DataField="totalPrice" />
                <asp:CommandField ShowSelectButton="True" SelectText="Create Invoice" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </p>
</asp:Content>
