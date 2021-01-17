<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="ServiceExpress.Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>.GirdView{
              margin-left: 10%;
            margin-right: 10%;
            margin-top:5%;
            margin-bottom: 8%;
       }</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table style="margin-left: 25%">
           <tr>
               <td style='font-family: "Courier New", Courier, monospace; color: #FF9999; font-size: xx-large; font-style: normal; text-decoration: underline; background-color: #CCFFFF; text-align: center;'>FeedBack</td>
           
           </tr>
         
       </table><br />
    <asp:GridView ID="GridView1" runat="server" CssClass="GirdView" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
        <Columns>
            <asp:BoundField DataField="feedbackID" HeaderText="feedbackID" />
            <asp:BoundField DataField="nric" HeaderText="nric" />
            <asp:BoundField DataField="fbemail" HeaderText="fbemail" />
            <asp:BoundField DataField="fbcomment" HeaderText="fbcomment" />
            <asp:BoundField DataField="fbRating" HeaderText="fbRating" />
            <asp:BoundField DataField="date" HeaderText="date" />
            <asp:BoundField DataField="orderID" HeaderText="orderID" />
            <asp:BoundField DataField="fbQnsID" HeaderText="fbQnsID" />
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
</asp:Content>
