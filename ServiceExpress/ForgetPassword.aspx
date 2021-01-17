<%@ Page Title="" Language="C#" MasterPageFile="~/SE_BE.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="ServiceExpress.ForgetPassword" %>

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

        .email {
            background: url(images/email-icon-md.png) no-repeat;
            padding-left: 33px;
            border: 1px solid #ccc;
            border-radius: 10px;
            margin: 2%;
        }

        .watermarkedemail {
            height: 20px;
            width: 150px;
            padding-left: 33px;
            border-radius: 10px;
            border: 1px solid #BEBEBE;
            background-position-x: 33px;
            color: Gray;
            background: url(images/email-icon-md.png) no-repeat;
            margin: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="margin: 7%">
        <tr>
            <td>

                <asp:LinkButton ID="LinkButton1" CssClass="btn pull-right btn-danger" runat="server" Text="[X]"></asp:LinkButton>
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <strong>Forget Password</strong></td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:TextBox ID="txtForgetPWUuserName" runat="server" CssClass="username unwatermarked" Height="30" Width="200"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="txtForgetPWUuserNameTextBoxWatermarkExtender" runat="server" TargetControlID="txtForgetPWUuserName" WatermarkCssClass="watermarkedusername" WatermarkText="User Name">
                </asp:TextBoxWatermarkExtender>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:TextBox ID="txtForgetPWEmail" runat="server" CssClass="email unwatermarked" Height="30" Width="200"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="txtForgetPWEmailTextBoxWatermarkExtender" runat="server" TargetControlID="txtForgetPWEmail" WatermarkCssClass="watermarkedemail" WatermarkText="Email">
                </asp:TextBoxWatermarkExtender>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" ValidationGroup="ForgetPW" />
                <br />

                <asp:RequiredFieldValidator ID="RequiredtxtForgetPWUuserName" runat="server" ControlToValidate="txtForgetPWUuserName" Display="None" ErrorMessage="RequiredFieldValidator" ValidationGroup="ForgetPW"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CloseImageUrl="~/images/Actions-window-close-icon.png" CssClass="customCalloutStyle" WarningIconImageUrl="~/images/asbestos.png" TargetControlID="RequiredtxtForgetPWUuserName">
                </asp:ValidatorCalloutExtender>
                <br />

                <asp:RequiredFieldValidator ID="RequiredtxtForgetPWEmail" runat="server" ControlToValidate="txtForgetPWEmail" Display="None" ErrorMessage="RequiredFieldValidator" ValidationGroup="ForgetPW"></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CloseImageUrl="~/images/Actions-window-close-icon.png" CssClass="customCalloutStyle" WarningIconImageUrl="~/images/asbestos.png" TargetControlID="RequiredtxtForgetPWEmail">
                </asp:ValidatorCalloutExtender>
                <br />
                <asp:RegularExpressionValidator ID="RegulartxtForgetPWEmail" runat="server" ControlToValidate="txtForgetPWEmail" Display="None" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ForgetPW"></asp:RegularExpressionValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CloseImageUrl="~/images/Actions-window-close-icon.png" CssClass="customCalloutStyle" WarningIconImageUrl="~/images/asbestos.png" TargetControlID="RegulartxtForgetPWEmail">
                </asp:ValidatorCalloutExtender>

                <br />
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="btnForgetPW" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnForgetPW"
        DropShadow="false" PopupControlID="pnlLogin"
        BackgroundCssClass="modalBackgroundLHZ" CancelControlID="Button5">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlLogin" runat="server" CssClass="modalPopupLHZ" Style="display: none">
        <table style="margin: 13%">
            <tr>
                <td>
                    <br />
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="Ok" />
                    &nbsp;&nbsp;
                                    <asp:Label ID="lblForgetPWResult" runat="server"></asp:Label>

                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
