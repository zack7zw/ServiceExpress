<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="CreateDeliveryOrder.aspx.cs" Inherits="ServiceExpress.CreateDeliveryOrder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

      <script type="text/javascript">
          function AllowAlphabet(e) {
              isIE = document.all ? 1 : 0
              keyEntry = !isIE ? e.which : event.keyCode;
              if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                  return true;
              else {
                  alert('Please Enter Only Character values!');
                  return false;
              }
          }

</script>


    <script language="javascript" type="text/javascript">
        function PrintDivContent(divId) {
            var printContent = document.getElementById(divId);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>
     <script type="text/javascript">

         function checkDate(sender, args) {
             if (sender._selectedDate < new Date()) {
                 alert("You cannot select a day earlier than today!");
                 sender._selectedDate = new Date();
                 // set the date back to the current date
                 sender._textbox.set_Value(sender._selectedDate.format(sender._format))
             }
         }
    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    &nbsp;<style type="text/css">
        #tbCompAdd {
            height: 70px;
            width: 259px;
        }
        .auto-style18 {
            height: 30px;
            width: 140px;
        }
        .auto-style19 {
            width: 140px;
            height: 27px;
        }
        .auto-style22 {
            height: 30px;
        }
        .auto-style23 {
            height: 27px;
        }
                             .auto-style24 {
                                 width: 100%;
                             }
                             .auto-style25 {
                         width: 273px;
                         text-align: right;
                     }
                             .auto-style27 {
                                 width: 377px;
                             }
                             .auto-style28 {
                                 width: 406px;
                                 text-align: left;
                             }
               .auto-style30 {
                   width: 311px;
               }
               .auto-style31 {
                   width: 274px;
               }
               .auto-style32 {
                   text-decoration: underline;
               }
               .auto-style33 {
                   width: 205px;
               }
        </style>
     <br />

     <div id="divToPrint">
     <table class="auto-style24">
         <tr>
             <td class="auto-style25">
                 &nbsp;</td>
             <td colspan="2" class="auto-style32">
                 <h1><strong>DELIVERY ORDER</strong></h1>
             </td>
         </tr>
         <tr>
             <td class="auto-style25">
                 &nbsp;</td>
             <td class="auto-style33">
                 <asp:Image ID="Image2" runat="server" Class="img-responsive" Height="123px" ImageUrl="~/Images/LogoSvcExp.gif" Width="197px" />
                 <br />
                 <asp:Label ID="lblComAdd" runat="server" Text="&lt;center&gt;Service Express &lt;br/&gt;18, Changi Business Park Central 1 &lt;br/&gt; Singapore 486097 &lt;br/&gt;6782-6690 &lt;center/&gt;"></asp:Label>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
     </table>
     <br />
    <br />
         <span class="auto-style32">SERVICE EXPRESS PTE.LTD</span><br />
         <br />
  
    <asp:Label ID="lblDeliverID" runat="server" Text="Deliver ID"></asp:Label>
         <br />
         <asp:Label ID="lblDeliveryid" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblPersonnel" runat="server" Text="Personnel(Customer's Name) :"></asp:Label>
    <br />
    <asp:Label ID="lblPerson" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCustAdd" runat="server" Text="Customer's Address (To Deliver) :"></asp:Label>
    &nbsp;<br />
    <asp:Label ID="lblAdd" runat="server"></asp:Label>
    <br />
         <br />
         Customer&#39;s Contact No:<br />
         <asp:Label ID="lblContact" runat="server"></asp:Label>
         <br />
    <br />
    <asp:Label ID="lblDeliveryDate" runat="server" Text="Delivery Date :"></asp:Label>
    <br />
    <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
         <asp:CalendarExtender ID="tbDate_CalendarExtender" runat="server" OnClientDateSelectionChanged="checkDate" Enabled="True" TargetControlID="tbDate" PopupButtonID="ImgCalendar">
         </asp:CalendarExtender>
         <asp:TextBoxWatermarkExtender ID="tbProductName_TextBoxWatermarkExtender" runat="server" TargetControlID="tbDate" WatermarkCssClass="graystyle" WatermarkText="mm/dd/yyyy">
                </asp:TextBoxWatermarkExtender>
    &nbsp;<asp:ImageButton ID="ImgCalendar" runat="server" Height="24px" ImageUrl="~/Images/calendar.png" Width="28px" />
    <br />
    <br />
    <br />
    <table style="width:34%;">
        <tr>
            <td bgcolor="#6699FF" class="auto-style18">Purchase Order ID</td>
            <td bgcolor="#6699FF" class="auto-style22">Order Date</td>
        </tr>
        <tr>
            <td class="auto-style19">
                <asp:Label ID="lblOrderID" runat="server"></asp:Label>
            </td>
            <td class="auto-style23">
                <asp:Label ID="lblOrderDate" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GvDelivery" runat="server" Align="Center" CellPadding="4" Height="287px" Width="831px" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AllowPaging="True" OnPageIndexChanging="GvDelivery_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="prodName" HeaderText="Product Items" />
            <asp:BoundField DataField="prodDescription" HeaderText="Product Descriptions" />
            <asp:BoundField DataField="quantity" HeaderText="Quantity" />
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
      <br />
      <br />
  <table class="auto-style24">
         <tr>
             <td class="auto-style31">&nbsp;</td>
             <td class="auto-style30">&nbsp;Items received are in good conditions.</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">Receiver&#39;s Name:<br />
                 <br />
&nbsp;_____________________</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">Receiver&#39;s Signature:<br />
                 <br />
&nbsp;_____________________&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">Date Received:<br />
                 <br />
&nbsp;_____________________&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td class="auto-style31">&nbsp;</td>
             <td class="auto-style30">&nbsp;</td>
             <td>DeliveryMan:
                 <asp:TextBox ID="tbPersonnel" onkeypress = "return AllowAlphabet(event)" runat="server"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="tbPersonnel" WatermarkCssClass="graystyle" WatermarkText="Jane">
                </asp:TextBoxWatermarkExtender>
             </td>
         </tr>
     </table>
         </div>
     <br />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <br />
     <table class="auto-style24">
         <tr>
             <td class="auto-style28">
                 &nbsp;</td>
             <td class="auto-style27">
    <asp:Button ID="BtnCreate" runat="server" BackColor="White" Text="CREATE" Width="80px" style="margin-left: 74px" Height="32px" OnClick="BtnCreate_Click" />
             &nbsp;&nbsp;
                 <asp:Button ID="btnPdf" runat="server" BackColor="White"  Height="32px" Text="PDF" Width="91px" OnClick="btnPdf_Click" PostBackUrl="~/CreateDeliveryOrder.aspx" />
             &nbsp;&nbsp;
    <asp:Button ID="btnPrint" runat="server" BackColor="White" Text="PRINT" Width="91px" OnClientClick="javascript:PrintDivContent('divToPrint');" Height="32px" />
             </td>
             <td>
                 &nbsp;</td>
         </tr>
     </table>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
</asp:Content>