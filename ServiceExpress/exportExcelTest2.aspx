<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exportExcelTest2.aspx.cs" Inherits="ServiceExpress.exportExcelTest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Multiple GridViews To Excel</title>
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
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"  CommandName="Select" Text="View Details"><img src="Images\View.png" alt="View Details" /></asp:LinkButton>
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
        </asp:GridView><br /><br />
    <asp:GridView ID="GridView1" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="InvoiceNo" OnSelectedIndexChanged="GVinvoice_SelectedIndexChanged">
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
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"  CommandName="Select" Text="View Details"><img src="Images\View.png" alt="View Details" /></asp:LinkButton>
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
    </div>
        <br />Paging Enabled?
        <asp:RadioButtonList ID="rbPaging" runat="server">
            <asp:ListItem  Text = "Yes" Value = "True" Selected = "True"  ></asp:ListItem>
            <asp:ListItem  Text = "No" Value = "False"></asp:ListItem>
        </asp:RadioButtonList>
        <br />
        Export Preference
        <asp:RadioButtonList ID="rbPreference" runat="server">
            <asp:ListItem  Text = "Vertical" Value = "1" Selected = "True"  ></asp:ListItem>
            <asp:ListItem  Text = "Horizontal" Value = "2"></asp:ListItem>
        </asp:RadioButtonList>

        <asp:Button ID="btnExportExcel" runat="server" Text="ExportToExcel" OnClick="btnExportExcel_Click" />       
    </form>
</body>
</html>
