<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ServiceExpress.LogIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackgroundLHZ {
            background-color: Black;
            filter: alpha(opacity=80);
            opacity: 0.7;
        }

        .modalPopupLHZ {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-bottom: 10px;
            width: 300px;
            height: 325px;
        }

        .username {
            background: url(images/USERICON.jpg) no-repeat;
            padding-left: 33px;
            border: 1px solid #ccc;
            border-radius: 10px;
            margin: 2%;
        }

        .password {
            background: url(images/Lock_Icon.png) no-repeat;
            padding-left: 33px;
            border: 1px solid #ccc;
            border-radius: 10px;
            margin: 2%;
        }

        .unwatermarked {
            height: 18px;
            width: 148px;
        }

        .watermarkedusername {
            height: 20px;
            width: 150px;
            padding-left: 33px;
            border-radius: 10px;
            border: 1px solid #BEBEBE;
            background-position-x: 33px;
            color: Gray;
            background: url(images/USERICON.jpg) no-repeat;
            margin: 2%;
        }

        .watermarkedpassword {
            height: 20px;
            width: 150px;
            padding-left: 33px;
            border-radius: 10px;
            border: 1px solid #BEBEBE;
            background-position-x: 33px;
            color: Gray;
            background: url(images/Lock_Icon.png) no-repeat;
            margin: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <table>
        <tr>
            <td style='font-family: "Courier New", Courier, monospace; color: #FF9999; font-size: xx-large; font-style: normal; text-decoration: underline;  text-align: center;'>Login page</td>

        </tr>

    </table>   <table style="margin: 13%">
        <tr>
            <td>
                <br />
                <asp:TextBox ID="txtLogInUserName" runat="server" CssClass="username unwatermarked" Height="30" Width="200"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatertxtLogInUserName" runat="server" TargetControlID="txtLogInUserName" WatermarkCssClass="watermarkedusername" WatermarkText="User Name">
                </asp:TextBoxWatermarkExtender>
                <br />
            </td>
        </tr>
        <tr>

            <td>
                <br />
                <asp:TextBox ID="txtLogInPassword" runat="server" CssClass="password unwatermarked" Height="30" Width="200" TextMode="Password"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="txtLogInPasswordTextBoxWatermarkExtender1" runat="server" TargetControlID="txtLogInPassword" WatermarkCssClass="watermarkedpassword" WatermarkText="Password">
                </asp:TextBoxWatermarkExtender>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Rember me next time." />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="Button3" runat="server" OnClick="btnLoginclick" CssClass="btn btn-danger" Text="Login" ValidationGroup="LogIn" />
                &nbsp;&nbsp;
                  
                <asp:RequiredFieldValidator ID="RequiredtxtLogInUserName" runat="server" ControlToValidate="txtLogInUserName" Display="None" ErrorMessage="RequiredFieldValidator" ValidationGroup="LogIn"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CloseImageUrl="~/images/Actions-window-close-icon.png" CssClass="customCalloutStyle" WarningIconImageUrl="~/images/asbestos.png" TargetControlID="RequiredtxtLogInUserName">
                </asp:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RequiredtxtLogInPassword" runat="server" ControlToValidate="txtLogInPassword" Display="None" ErrorMessage="RequiredFieldValidator" ValidationGroup="LogIn"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CloseImageUrl="~/images/Actions-window-close-icon.png" CssClass="customCalloutStyle" WarningIconImageUrl="~/images/asbestos.png" TargetControlID="RequiredtxtLogInPassword">
                </asp:ValidatorCalloutExtender>
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="btnForgetPW" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnForgetPW"
        DropShadow="false" PopupControlID="pnlLogin"
        BackgroundCssClass="modalBackgroundLHZ" CancelControlID="Button5">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlLogin" runat="server" CssClass="modalPopupLHZ" Style="display: none">
        <table style="margin: 13%">
            <tr>
                <td>
                    <br />
                    <asp:Button ID="Button5" runat="server" CssClass="btn btn-danger" Text="Ok" />
                    &nbsp;&nbsp;
                         <asp:Label ID="lblLogInResult" runat="server"></asp:Label>

                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
