<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="DailySalesReport.aspx.cs" Inherits="ServiceExpress.DailySalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
     
        .title {
            text-align: center;
            text-decoration: underline;
            background-color: #9999FF;
        }
        .auto-style2 {
            text-decoration: underline;
            font-size: x-large;
        }
        .auto-style13 {
            text-decoration: underline;
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <h1 class="title">Daily Sales Report</h1>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <span class="auto-style2"><strong>Product Sales</strong></span></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView ID="GVDailySales" runat="server" AutoGenerateColumns="False" style="text-align: center" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:BoundField DataField="prodID" HeaderText="Product #" />
                        <asp:BoundField DataField="prodName" HeaderText="Product Name" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="price" HeaderText="Unit Price" />
                        <asp:BoundField DataField="orderDate" HeaderText="Order Date" />
                        <asp:BoundField DataField="PurOrderNo" HeaderText="Purchase Order #" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                The total price for the sale :
                <asp:Label ID="lblTotalPrice" runat="server" style="font-size: xx-large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <span class="auto-style13"><strong>Product Sales Chart</strong></span></td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <asp:Chart ID="chartDailySales" runat="server" DataSourceID="SqlDataSource1">
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Product Name" YValueMembers="Unit Price">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ServiceExpressConnectionString %>"></asp:SqlDataSource>
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="PDF" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
