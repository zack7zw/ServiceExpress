<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="InvoiceView.aspx.cs" Inherits="ServiceExpress.InvoiceView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 27px;
        }
        .auto-style4 {
            height: 27px;
    
        }

        .auto-style13 {
            height: 22px;
        }
        .auto-style14 {
            height: 22px;
            width: 98px;
        }
        .auto-style15 {
            width: 98px;
            height: 42px;
        }
        .auto-style16 {
            height: 27px;
            width: 98px;
        }
        .auto-style17 {
            width: 17px;
            height: 42px;
        }
        .auto-style18 {
            height: 42px;
        }
        .auto-style19 {
            text-align: center;
            color: #0066FF;
        }
        .newStyle1 {
            font-family: "comic Sans MS";
        }
    </style>
     <script type="text/javascript">
         function closeModalPayment() {
             $("#PaymentModal").modal("hide");
             //$('body').removeClass('modal-open');
             $('.modal-backdrop').remove();
         }
         function openModalPayment() {
             $('.modal-backdrop').remove();
             $('#PaymentModal').modal({ show: true });
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <h1 class="auto-style19">
            <span class="newStyle1">Invoice Details</span></h1>
        <table style="width: 934px">
            <tr>
                <td class="auto-style14">
                    <asp:Button ID="btnDetailView" CssClass="btn btn-success" runat="server" Text="Invoice Details" />
                </td>
                <td class="auto-style13">
                    &nbsp;</td>
                <td class="auto-style13">
                    <asp:Button ID="btnActualIV" CssClass="btn btn-primary" runat="server" Text="Actual View" OnClick="btnActualIV_Click" />
                </td>
                <td colspan="4" class="auto-style13"><a id="btnPaymentDetail" class="navbar-button btn pull-left btn-primary" data-toggle="modal" href="#PaymentModal">View Payment</a>

                </td>
                <td class="auto-style13">
                    &nbsp;</td>
                <td class="auto-style13">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style14">
                    Invoice No.: 
                    </td>
                <td class="auto-style13" colspan="2">
                    <asp:Label ID="lblInvoiceNo" runat="server" ></asp:Label>
                </td>
                <td colspan="4" class="auto-style13">&nbsp;</td>
                <td class="auto-style13">
                    &nbsp;</td>
                <td class="auto-style13">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style14">
                    <asp:Label ID="lblcust" runat="server" Text="Company:"></asp:Label>
                &nbsp;</td>
                <td class="auto-style13" colspan="2">
                    <asp:Label ID="lblcustName" runat="server" ></asp:Label>
                </td>
                <td colspan="4" class="auto-style13"></td>
                <td class="auto-style13">
                    </td>
                <td class="auto-style13">
                    Date:
                    <asp:Label ID="lblIVdate" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">
                    <asp:Label ID="lblcp" runat="server" Text="Credit Period:"></asp:Label>
                </td>
                <td colspan="2" class="auto-style18">
                    <asp:Label ID="lblCreditPeriod" runat="server"></asp:Label>
                </td>
                <td class="auto-style18">
                    No. Days left:&nbsp;<asp:Label ID="lblNoOfDays" runat="server" ></asp:Label>
                </td>
                <td class="auto-style18">
                    <asp:Label ID="lblNoOfDayDisc" runat="server" Text="No. Days left: <br/>(discount)"></asp:Label>
                    &nbsp;</td>
                <td class="auto-style17">
                    <asp:Label ID="lblNoOfDaysDisc" runat="server" ></asp:Label>
                </td>
                <td colspan="2" class="auto-style18">
                    </td>
                <td class="auto-style18">Status:
                    <asp:Label ID="lblStatus" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style16">
                    Total Amount: </td>
                <td class="auto-style2" colspan="2">
                    
                                $<asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                            </td>
                <td class="auto-style2">
                    Amount Paid: $<asp:Label ID="lblPaidAmt" runat="server" Text="0"></asp:Label>
                </td>
                <td class="auto-style2" colspan="2">
                    Outstanding:
                    $<asp:Label ID="lblOutstandAmt" runat="server" ></asp:Label>
                </td>
                <td  colspan="2" class="auto-style2">
                    </td>
                <td ></td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="9">
        <asp:GridView ID="gvInvoiceDetails" runat="server" align="center" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="729px">
            <Columns>
                <asp:BoundField DataField="prodID" HeaderText="No." />
                <asp:BoundField DataField="prodName" HeaderText="Product Item" />
                <asp:BoundField DataField="prodDescription" HeaderText="Product Description" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
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
                <td class="auto-style2" colspan="3">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2" colspan="4">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
        </table>
        
    <table class="style3">
        <tr>
            <td class="auto-style5">
                <asp:Button ID="btnBack" CssClass="btn-default" runat="server" Text="Back" Width="56px" OnClick="btnBack_Click" />
            </td>
            <td class="auto-style4">
                &nbsp;</td>
            <td> 
                <asp:Button ID="btnPrint" CssClass="btn-success" runat="server" Text="Print" Width="58px"  />
            </td>
            <td> 
                &nbsp;</td>
            <td> 
                <asp:Button ID="btnExportEx" CssClass="btn-info" runat="server" Text="Export to Excel" OnClick="btnExportEx_Click"  />
            </td>
        </tr>
        </table>
    <br />
        <div class="modal fade" id="PaymentModal"  role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">

                         <div id="divIdPrint">
                     <table class="">
                <tr>
                    <td class="auto-style2" colspan="2">
                       
                        <h1 class="auto-style3">Payment details</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        InvoiceNo.: <asp:Label ID="lblivnb" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvPayment" align="center" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" DataKeyNames="paymentNo" >
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
                        </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
                        
                        </div> 
                        <table >
            <tr>
                <td><input id="btnBackk" class="btn-default" onclick="closeModalPayment();" type="button" value="Close" /> &nbsp;&nbsp;<input id="btnPrintt" type="button" value="Print" class="btn-primary" onclick="    javascript: PrintDivContent('divIdPrint');" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>   
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
