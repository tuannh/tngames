<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-Login-Home.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_Login_Home" %>
<div id="divLogin" runat="server" style="background: #B4D6DC;">
    <asp:Label ID="lblMsg" ForeColor="Red" Style="float: left;" runat="server" />
    <div class="clear">
    </div>
    <table width="100%" border="0" cellpadding="5" cellspacing="0" class="dangnhap">
        <tr>
            <td width="80" height="35" align="right">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                    ErrorMessage="*" ControlToValidate="txtEmail" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                    runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                Email(*):
            </td>
            <td width="150">
                <asp:TextBox ID="txtEmail" CssClass="textfield" runat="server" Width="150px" MaxLength="100" />
            </td>
            <td width="80" align="right">
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="*" ControlToValidate="txtPassword" />
                Mật khẩu(*):
            </td>
            <td width="150">
                <asp:TextBox ID="txtPassword" CssClass="textfield" runat="server" Width="150px" TextMode="Password"
                    MaxLength="100" />
            </td>
            <td width="100" class="button">
                <asp:Button ID="btnLogin" runat="server" CssClass="buttonL" Text="Đăng nhập" OnClick="btnLogin_Click" />
            </td>
            <td align="left">
                <label>
                    <asp:CheckBox ID="chkSave" runat="server" />
                    Lưu mật khẩu
                </label>
            </td>
            <td width="160">
                <a href="/dang-ky">Đăng ký</a> | <a href="/quen-mat-khau">Quên mật khẩu</a>
            </td>
        </tr>
    </table>
</div>
<div id="divLoggedIn" runat="server" visible="false">
    <table width="100%" border="0" cellpadding="5" cellspacing="0" class="dangnhap" style="text-align: center;">
        <tr>
            <td width="20%" height="35" style="padding-left: 10px;">
                Xin chào:
                <%= TNGames.Core.Helper.Utils.GetCurrentUser().DisplayName %>
            </td>
            <td width="20%">
               Tổng điểm:
                <%= TNGames.Core.Helper.Utils.GetCurrentUser().TotalPoint.ToString("N0") %>
            </td>
            <td width="20%">
                <img src="/images/web/profile_icon.png" width="16" height="16" />
                <a href="/tai-khoan">Hồ sơ cá nhân</a>
            </td>
            <td width="20%">
                <img src="/images/web/core_icon.png" width="16" height="16" />
                <a href="/thanh-tich">Thông tin thành tích</a>
            </td>
            <td width="20%">
                <% if (TNGames.Core.Helper.Utils.GetCurrentUser().IsAdmin)
                   {  %>
                <a href="/admincp">AdminCP</a> |
                <%} %><a href="/dang-nhap?logout=true" title="Đăng xuất">Thoát</a>
            </td>
        </tr>
    </table>
</div>
