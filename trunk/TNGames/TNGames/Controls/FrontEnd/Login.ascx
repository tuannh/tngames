<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="TNGames.Controls.FrontEnd.Login" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/dangnhap.png" alt="" />
                    </td>
                    <td class="bgheaderright">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="content">
            <div class="mycontent">
                <asp:LoginView ID="loginView" runat="server">
                    <AnonymousTemplate>
                        <asp:Label ID="lblMsg" runat="server" />
                        <table width="100%" border="0" cellspacing="0" cellpadding="3">
                            <tr>
                                <td width="25%" align="right">
                                    Email (*):
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="150px" MaxLength="100" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                                        ErrorMessage="Bạn chưa nhập địa chỉ email" ControlToValidate="txtEmail" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                                        runat="server" ErrorMessage="Địa chỉ email chưa đúng" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" align="right">
                                    Mật khẩu (*):
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" MaxLength="100" />
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Bạn chưa nhập mật khẩu" ControlToValidate="txtPassword" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" align="right">
                                </td>
                                <td>
                                    <asp:Button ID="btnLogin" runat="server" CssClass="buttonL" Text="Đăng nhâp" OnClick="btnLogin_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <a href="/dang-ky">Đăng ký</a><br />
                                    <a href="/quen-mat-khau">Quên mật khẩu</a>
                                </td>
                            </tr>
                        </table>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        <p>
                            Bạn đang đăng nhập. Hệ thống sẽ tự động chuyển sang trang thông tin cá nhân của
                            bạn sau 5 giây
                        </p>
                        <script type="text/javascript">
                        //<![CDATA[
                            function redirect() {
                                location.href = "/tai-khoan";
                            }
                            setTimeout(redirect, 5000);
                        //]]>
                        </script>
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
        </td>
    </tr>
</table>
