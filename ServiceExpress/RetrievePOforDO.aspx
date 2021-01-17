<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="RetrievePOforDO.aspx.cs" Inherits="ServiceExpress.RetrievePOforDO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .auto-style2 {
            text-decoration: underline;
        }
    .auto-style3 {
        width: 108%;
    }
    .auto-style4 {
        width: 480px;
        text-align: right;
    }
    .auto-style5 {
        width: 721px;
        text-align: right;
        height: 26px;
    }
    .auto-style6 {
        height: 26px;
    }
    .auto-style13 {
        width: 480px;
        text-align: right;
        height: 34px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <h1 align="center" style="width: 938px"> &nbsp;</h1>
    <h1 align="center" style="width: 938px"> &nbsp;<span class="auto-style2">DELIVERY ORDER</span> </h1>

    <table class="auto-style3">
        <tr>
            <td class="auto-style13"></td>
            <td class="auto-style6">
                </td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp; Search By Status:&nbsp;&nbsp;</td>
            <td><asp:DropDownList ID="DropDownList1" runat="server" DataTextField="deliveryStatus" DataValueField="deliveryStatus">
                <asp:ListItem>-</asp:ListItem>
       <asp:ListItem>Processing</asp:ListItem>
       <asp:ListItem>Shipped</asp:ListItem>
       <asp:ListItem>Received</asp:ListItem>
       </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Width="80px" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvPurchaseOrders" runat="server" Height="347px" Width="826px" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" PageSize="5" style="margin-bottom: 0px" DataKeyNames="purOrderNo" OnSelectedIndexChanged="gvPurchaseOrders_SelectedIndexChanged" OnPageIndexChanging="gvPurchaseOrders_PageIndexChanging" OnRowDataBound="gvPurchaseOrders_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Delivery No.">
                <EditItemTemplate>
                    <asp:Label ID="deliveryNo" runat="server" Text='<%# Eval("deliveryNo") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="deliveryNo" runat="server" Text='<%# Bind("deliveryNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Purchase No.">
                <EditItemTemplate>
                    <asp:TextBox ID="purOrderNo" runat="server" Text='<%# Bind("purOrderNo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="purOrderNo" runat="server" Text='<%# Bind("purOrderNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Company's Personnel" ReadOnly="True" DataField="deliverySuppName" />
            <asp:BoundField DataField="orderDate" HeaderText="Purchase Date" ReadOnly="True" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Delivery Date" DataField="deliveryDate" ReadOnly="True" HtmlEncode="false" DataFormatString="{0:d}"  />
          
            <asp:BoundField DataField="deliveryStatus" HeaderText="Status" />
            <asp:TemplateField HeaderText="View" ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/view.png" OnClick="ImageButton2_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/parcel1.jpg" OnClick="LinkButton1_Click" Width="76px" Height="47px" />
                </ItemTemplate>
            </asp:TemplateField>
          
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
  

   
        <table class="auto-style3">
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        </table>
</ContentTemplate></asp:UpdatePanel>   
</asp:Content>
