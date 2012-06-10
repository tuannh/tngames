<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="TNGames.Controls.Admin.ChangePassword" %>
<asp:Panel ID="pnlPassword" runat="server">
    <h1>
        Đổi mật khẩu</h1>
    <asp:Label ID="lblMsgPassword" runat="server" />
    <table style="width: 100%;">
        <tr>
            <td style="width: 100px;">
                Mật khẩu cũ:
            </td>
            <td>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" MaxLength="150"
                    ValidationGroup="password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtOldPassword" ID="RequiredFieldValidator2"
                    runat="server" ValidationGroup="password" ErrorMessage="Bạn chưa nhập mật khẩu cũ"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Mật khẩu:
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="150"
                    ValidationGroup="password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="password"
                    ErrorMessage="Mật khẩu mới không thể rỗng" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Xác nhận:
            </td>
            <td>
                <asp:TextBox ID="txtRetypePass" runat="server" TextMode="Password" MaxLength="150"
                    ValidationGroup="password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Mật khẩu không trùng khớp"
                    ControlToValidate="txtRetypePass" ControlToCompare="txtPassword" ValidationGroup="password" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" ValidationGroup="password"
                    OnClick="btnUpdate_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
