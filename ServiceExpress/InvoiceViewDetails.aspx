<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="InvoiceViewDetails.aspx.cs" Inherits="ServiceExpress.InvoiceViewDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
       
 
       
       
        .auto-style13 {
            width: 200px;
        }   
       
        .auto-style14 {
            height: 27px;
        }
       
       
 
       
       
        .auto-style15 {
            width: 200px;
            height: 34px;
        }
        .auto-style16 {
            width: 265px;
        }
       
       
 
       
       
        .auto-style17 {
            height: 22px;
        }
       
       
 
       
       
        .auto-style18 {
            width: 200px;
            height: 27px;
        }

       
            .auto-style19 {
                width: 425px;
            }

       
            .auto-style20 {
                width: 277px;
                height: 34px;
            }

       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br /><br />
    <asp:Button ID="btnDetailView" CssClass="btn btn-success" runat="server" Text="Invoice Details" OnClick="btnDetailView_Click" />
    <asp:Button ID="btnActualIV" CssClass="btn btn-primary" runat="server" Text="Actual Invoice" OnClick="btnActualIV_Click" />
     
     <div id="divIdPrint">
         
        <table class=" table-borderless center-table" style="background-color:white;">
            <tr>
                <td class="auto-style20">
                    &nbsp;</td>
                <td colspan="3" class="auto-style14">
                    <h1 class="text-center">Invoice</h1>
                </td>
                <td class="auto-style13">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20">
                    &nbsp;</td>
                <td colspan="3" class="auto-style14"><asp:Panel runat="server" ID="Panel2" HorizontalAlign="Center">
                    <asp:Image ID="Image2" runat="server" Class="img-responsive" Height="123px" ImageUrl="~/Images/LogoSvcExp.gif" Width="197px" />
                    <asp:Label ID="lblComAdd" runat="server" Text="&lt;center&gt;Service Express &lt;br/&gt;18, Changi Business Park Central 1 &lt;br/&gt; Singapore 486097 &lt;br/&gt;6782-6690 &lt;center/&gt;"></asp:Label>
                    <br />
                </asp:Panel> 
                </td>
                <td class="auto-style13">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20" rowspan="3">
                    <asp:Label ID="lblCustComName" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblPersonnel" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                    <br />
                &nbsp;</td>
                <td colspan="2" class="auto-style14" rowspan="3"></td>
                <td class="auto-style16" rowspan="3">
                    </td>
                <td class="auto-style13">
                    Invoice No.:
                    <asp:Label ID="lblInvoiceNo" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    Invoice Date:
                    <asp:Label ID="lblInvoiceDate" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style18">
                    Ref
                    Deliver No.:
                    <asp:Label ID="lblDONo" runat="server" ></asp:Label>
                &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20" rowspan="2">
                    &nbsp;</td>
                <td class="auto-style11" rowspan="2">
                    </td>
                <td colspan="2" class="auto-style8" rowspan="2">
                    &nbsp;</td>
                <td class="auto-style14">
                    <asp:Label ID="lblCP" runat="server" Text="Credit Period: "></asp:Label>
                    <asp:Label ID="lblCreditPeriod" runat="server" Text=""></asp:Label>
                    &nbsp;days</td>
            </tr>
            <tr>
                <td class="auto-style17">
                    <asp:Label ID="lblct" runat="server" Text="Credit Terms: "></asp:Label>
                    &nbsp;<br />
                    <asp:Label ID="lblCreditTerm" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style20">
                    &nbsp;</td>
                <td class="auto-style12">
                    &nbsp;</td>
                <td  colspan="2">
                    &nbsp;</td>
                <td class="auto-style13">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6" colspan="5">
        <asp:GridView ID="gvInvoiceDetails" runat="server" align="center" AutoGenerateColumns="False" CellPadding="3" Width="729px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:BoundField HeaderText="No." DataField="prodID" />
                <asp:BoundField HeaderText="Product Item" DataField="prodName" />
                <asp:BoundField HeaderText="Product Description" DataField="prodDescription" />
                <asp:BoundField HeaderText="Quantity" DataField="quantity" />
                <asp:BoundField HeaderText="Unit Price" DataField="price" />
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
                <td class="auto-style20">
                    </td>
                <td class="auto-style12">
                    </td>
                <td  colspan="2" class="auto-style8">
                    </td>
                <td class="auto-style15">
                    <table class="auto-style1">
                        <tr>
                            <td>Sub total: </td>
                            <td>
                    
                                $<asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style17">GST:</td>
                            <td class="auto-style17">
                    
                                $<asp:Label ID="lblGST" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style17">Total Amount:</td>
                            <td class="auto-style17">
                    
                                $<asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </td>
            </tr>
            <tr>
                <td class="auto-style20">
                    &nbsp;</td>
                <td class="auto-style12">
                    &nbsp;</td>
                <td  colspan="2">
                    &nbsp;</td>
                <td class="auto-style13">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style20">
                    <asp:Label ID="lblResults" runat="server" ></asp:Label>
                </td>
                <td class="auto-style12">
                    &nbsp;</td>
                <td  colspan="2">
                    &nbsp;</td>
                <td class="auto-style13">&nbsp;</td>
            </tr>
        </table>
         </div>
    <p>
        &nbsp;</p>
    <table class="style33">
        <tr>
            <td class="auto-style5">
                <asp:Button ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-default" OnClick="btnBack_Click" />
            </td>
            <td class="auto-style19">
                &nbsp;
                &nbsp;&nbsp;
            </td>
            <td> 
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info" Text="Print" OnClientClick="javascript: PrintDivContent('divIdPrint');" Width="58px" />
            </td>
            <td> 
                <asp:Button ID="btnExportEx" CssClass="btn btn-success" runat="server" Text="Export to Excel" OnClick="btnExportEx_Click"  />
            </td>
        </tr>
        </table>
         
     <br />
     <br />
     <br />
    <br />
</asp:Content>
