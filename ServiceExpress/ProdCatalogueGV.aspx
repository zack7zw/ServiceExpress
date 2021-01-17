<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="ProdCatalogueGV.aspx.cs" Inherits="ServiceExpress.ProdCatalogueGV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
        .auto-style2 {
            width: 100%;
        }

        .auto-style4 {
            height: 48px;
            width: 71%;
            font-size: x-large;
        }

        .auto-style5 {
            width: 71%;
        }

        .auto-style6 {
            margin-right: 0px;
        }

        .auto-style7 {
            width: 141px;
        }

        .popupWindow {
            position: absolute;
            left: 300px;
            top: 50px;
            width: 350px;
            border: solid 1px black;
            padding: 10px;
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style2">
        <tr>
            <td class="auto-style4"><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Product Catalogue Administration</em></td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                <span class="auto-style1"><span class="auto-style2"><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New Record" />

                </em></span></span></td>
            <td class="auto-style7">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">
                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="auto-style6" ForeColor="#333333" GridLines="None" Height="243px" OnPageIndexChanging="gvProduct_PageIndexChanging" OnRowCancelingEdit="gvProduct_RowCancelingEdit" OnRowDeleting="gvProduct_RowDeleting" OnRowEditing="gvProduct_RowEditing" OnRowUpdating="gvProduct_RowUpdating" OnSelectedIndexChanged="gvProduct_SelectedIndexChanged" PageSize="5" Width="1001px" DataKeyNames="prodID" AllowPaging="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="prodID" HeaderText="Product ID" />
                        <asp:TemplateField HeaderText="Product Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbname" runat="server" Text='<%# Bind("prodName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblprodName" runat="server" Text='<%# Bind("prodName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Images">
                            <EditItemTemplate>
                            
                                <asp:TextBox ID="FileUpload1" runat="server" Text='<%# Eval("image") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image ID="FileUpload1" runat="server" ImageUrl='<%# Eval("image", "~/Images/{0}") %>' />
                            </ItemTemplate>
                            <ControlStyle Height="100px" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbprice" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblprice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbproddesc" runat="server" Text='<%# Bind("prodDescription") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblproddesc" runat="server" Text='<%# Bind("prodDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Category">
                            <EditItemTemplate>
                                <asp:TextBox ID="tbcategory" runat="server" Text='<%# Bind("prodCategory") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("prodCategory") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
        </tr>
    </table>
    <asp:Panel ID="pnlPopUp" Visible="false" CssClass="popupWindow" runat="server">
        <!-- category -->
        <div class="catLabel">
            <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
        </div>
        <div class="catText">
            <asp:TextBox ID="tbname" runat="server"></asp:TextBox>
        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Product name is a required field." ControlToValidate="tbname" ForeColor="#336699"></asp:RequiredFieldValidator>
        <div class="clear" />

        <!-- Title -->
        <div class="catLabel">
            <asp:Label ID="Label2" runat="server" Text="Image"></asp:Label>
        </div>
        <div class="catText">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Image is a required field." ControlToValidate="FileUpload1" ForeColor="#336699"></asp:RequiredFieldValidator>
        <div class="clear" />

        <!-- price -->
        <div class="catLabel">
            <asp:Label ID="Label4" runat="server" Text="Price"></asp:Label>
        </div>
        <div class="catText">
            <asp:TextBox ID="tbprice" runat="server"></asp:TextBox>
        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Price is a required field." ControlToValidate="tbprice" ForeColor="#336699"></asp:RequiredFieldValidator>
        <div class="clear" />

        <!-- Filename -->
        <div class="catLabel">
            <asp:Label ID="Label3" runat="server" Text="Product Description"></asp:Label>
        </div>
        <div class="catText">
            <asp:TextBox ID="tbproddesc" runat="server"></asp:TextBox>
        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Product description is a required field." ControlToValidate="tbproddesc" ForeColor="#336699"></asp:RequiredFieldValidator>
        <div class="catLabel">
            <asp:Label ID="Label5" runat="server" Text="Product Category"></asp:Label>
        </div>
        <div class="catText">
            <asp:TextBox ID="tbcategory" runat="server"></asp:TextBox>
        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Product category is a required field." ControlToValidate="tbcategory" ForeColor="#336699"></asp:RequiredFieldValidator>
        <br />
        <div class="clear" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />

    </asp:Panel>
</asp:Content>