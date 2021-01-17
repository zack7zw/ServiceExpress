<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="ViewProductsDO.aspx.cs" Inherits="ServiceExpress.ViewProductsDO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            font-size: xx-large;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br />
     <br />
     <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style13">VIEW DELIVERY ORDER DETAILS</span><br />
     <asp:GridView ID="gvPurchaseOrders" runat="server" Height="340px" Width="826px" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" PageSize="5" Style="margin-bottom: 0px; margin-top: 1px;">
        <Columns>
            <asp:BoundField DataField="prodID" HeaderText="Product No." />
            <asp:BoundField DataField="prodName" HeaderText="Product Name" />
            <asp:BoundField DataField="prodDescription" HeaderText="Product Description" />
            <asp:BoundField HeaderText="Purchase Quantity" DataField="quantity" />
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    <table>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: center">
                <asp:Button ID="btnBack" runat="server" BackColor="White"  Height="32px" Text="BACK" Width="96px" Style="margin-left: 576px; text-align: center;" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
