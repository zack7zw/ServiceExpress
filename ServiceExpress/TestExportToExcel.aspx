<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestExportToExcel.aspx.cs" Inherits="ServiceExpress.TestExportToExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style3 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="gvInvoice" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="InvoiceNo" OnSelectedIndexChanged="GVinvoice_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Invoice No." DataField="InvoiceNo" />
                    <asp:BoundField HeaderText="Company" DataField="companyName" />
                    <asp:BoundField HeaderText="Contact Personnel" DataField="orderPersonnel" />
                    <asp:BoundField HeaderText="Credit Period" DataField="creditPeriod" />
                    <asp:BoundField HeaderText="Total Price" DataField="grandTotal" />
                    <asp:BoundField HeaderText="Date" DataField="invoiceDate" />
                    <asp:BoundField HeaderText="Status" DataField="invoiceStatus" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="View Details"><img src="Images\View.png" alt="View Details" /></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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

            <asp:Label ID="MessageBox" runat="server" Text="Label"></asp:Label>

            /<asp:Label ID="MessageBox0" runat="server" Text="Label"></asp:Label>

            <table class="">
                <tr>
                    <td class="auto-style2" colspan="2">
                        <h1 class="auto-style3">Payment details</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

            <asp:GridView ID="gvPayment" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="paymentNo" OnSelectedIndexChanged="GVinvoice_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Payment No." DataField="PaymentNo" />
                    <asp:BoundField HeaderText="Amount Paid" DataField="paidAmt" />
                    <asp:BoundField HeaderText="Transaction No/Verify" DataField="transactionNo" />
                    <asp:BoundField HeaderText="Type" DataField="paymentType" />
                    <asp:BoundField HeaderText="Paid By" DataField="paymentPersonnel" />
                    <asp:BoundField HeaderText="Date" DataField="paymentDate" />
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

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id="btnBack" class="btn-default" onclick="closeModalPayment();" type="button" value="Close" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
        <asp:Button ID="Button1" runat="server" OnClick="btnExport_Click" Text="Button" />
        <asp:Button ID="Button2" runat="server" OnClick="Buttonexcel_Click" Text="Button" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
    </form>
</body>
</html>
