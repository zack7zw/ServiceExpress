<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="ServiceExpress.Invoice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
     
        .auto-style13 {
            height: 24px;
        }
     
        .auto-style14 {
            text-align: center;
            color: #0066FF;
        }
        .newStyle1 {
            font-family: "Comic Sans MS";
        }
        .auto-style15 {
            height: 42px;
        }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <br />
    <h1 class="auto-style14">
        <span class="newStyle1">Invoice
        </span>
        </h1>
        <table class=" table-borderless-left center-table">
            <tr>
                <td>
        <table class="">
            <tr>
                <td class="auto-style13">
                    Company Name:
                </td>
                <td class="auto-style13">
                    <asp:TextBox ID="tbComName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style13"></td>
                <td class="auto-style13">
                    </td>
                <td class="auto-style13">
                    </td>
                <td class="auto-style13">
                    </td>
                <td class="auto-style13"></td>
            </tr>
            <tr>
                <td class="style17">
                    <asp:Label ID="lblFilter" runat="server" Text="Filter by:"></asp:Label>
                    &nbsp;
                    </td>
                <td class="style17">
                    <asp:TextBox ID="tbFromDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="tbFromDate_CalendarExtender" runat="server" DaysModeTitleFormat="MM/dd/yyyy" Enabled="True" Format="MM/dd/yyyy" TargetControlID="tbFromDate">
                    </asp:CalendarExtender>
                </td>
                <td class="style21">To:
                    <asp:TextBox ID="tbToDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="tbToDate_CalendarExtender" runat="server" DaysModeTitleFormat="MM/dd/yyyy" Enabled="True" Format="MM/dd/yyyy" TargetControlID="tbToDate">
                    </asp:CalendarExtender>
                    &nbsp;</td>
                <td class="style21">
                    <asp:RadioButtonList ID="rblSort" runat="server">
                        <asp:ListItem Selected="True" Value="asc">Ascending</asp:ListItem>
                        <asp:ListItem Value="desc">Descending</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style19">
                    <asp:Button ID="btnFilter" runat="server" onclick="btnFilter_Click" CssClass="btn btn-success" Text="Filter" />
                </td>
                <td class="style8">
                    <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                </td>
                <td class="style8">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">
                    <asp:Label ID="lblvonly" runat="server" Text="Invoice Type:"></asp:Label>
                    &nbsp;</td>
                <td class="auto-style15">
                    <asp:DropDownList ID="ddlViewOnly" runat="server" Width="106px">
                        <asp:ListItem Value="All">All</asp:ListItem>
                        <asp:ListItem Value="Unpaid">Unpaid</asp:ListItem>
                        <asp:ListItem Value="Outstanding">Outstanding</asp:ListItem>
                        <asp:ListItem Value="Due">Due</asp:ListItem>
                        <asp:ListItem Value="BadDebt">Bad Debt</asp:ListItem>
                        <asp:ListItem Value="Paid">Paid</asp:ListItem>
                        <asp:ListItem Value="Warning">Warning sent</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style15" colspan="3">
                    <asp:Button ID="btnSet" runat="server" CssClass="btn btn-success"  onclick="btnSet_Click" Text="Set"  />
                </td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="style17">
                    Generate chart:</td>
                <td class="style17">
                    <asp:Button ID="btnGen" runat="server" CssClass="btn btn-success"   Text="Go" OnClick="btnGen_Click"  />
                </td>
                <td class="style16" colspan="3">
                    &nbsp;</td>
                <td class="style8">&nbsp;</td>
                <td class="style8">&nbsp;</td>
            </tr>
        </table>
                </td>
            </tr>
            <tr>
                <td>
 <asp:GridView ID="gvInvoice" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="InvoiceNo" OnSelectedIndexChanged="GVinvoice_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="gvInvoice_PageIndexChanging" PageSize="5">
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
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"  CommandName="Select" Text="View Details"><img src="Images\\View.png" alt="View Details" /></asp:LinkButton>
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
                </td>
            </tr>
        </table>
 <asp:GridView ID="gvInvoiceDue" Visible="False" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="InvoiceNo" OnSelectedIndexChanged="gvInvoiceDue_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="gvInvoiceDue_PageIndexChanging" PageSize="5">
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
                       <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/View.png" OnClick="LinkButton1_Click" Width="38px" Height="35px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"  CommandName="Select" Text="View Details"><img src="Images\\mail_warning.png" alt="View Details" style="height: 40px; width:40px;" /></asp:LinkButton>
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
  
    <p class="style5">
        &nbsp;</p>
    <p class="style5">
        &nbsp;</p>
    <p class="style5">
        &nbsp;</p>
    <p class="style5">
        &nbsp;</p>
</asp:Content>
