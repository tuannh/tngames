<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="TNGames.Controls.FrontEnd.Register" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/account_header.png" alt="" width="244" height="33" />
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
                <asp:Label ID="lblMsg" ForeColor="#0000FF" runat="server" />
                <asp:Panel ID="pnlRegister" runat="server">
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td width="25%" align="right">
                                Họ và Tên (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullName" runat="server" MaxLength="150" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa nhập họ tên" ControlToValidate="txtFullName" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Tên hiển thị (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa nhập tên hiển thị" ControlToValidate="txtDisplayName" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="right">
                                Giới tính (*):
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem Value="" Text="Chọn" />
                                    <asp:ListItem Selected="True" Value="1" Text="Nam" />
                                    <asp:ListItem Value="0" Text="Nữ" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa chọn giới tính" ControlToValidate="ddlGender" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong>Account và mật khẩu:</strong>
                            </td>
                            <td>
                                <hr width="100%" size="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Email (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa nhập địa chỉ email" Display="Dynamic" ControlToValidate="txtEmail" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Đia chỉ email cung cấp không hợp lệ"
                                    ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mật khẩu (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa nhập mật khẩu" ControlToValidate="txtPassword" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Xác nhận mật khẩu (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtRetypePass" runat="server" TextMode="Password" MaxLength="150"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" ForeColor="Red" runat="server" ErrorMessage="Mật khẩu không trùng khớp"
                                    ControlToValidate="txtRetypePass" ControlToCompare="txtPassword" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mã xác nhận (*):
                            </td>
                            <td>
                                <div class="captcha">
                                    <asp:TextBox ID="txtCaptcha" runat="server" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="captcha">
                                    <img title="Tải lại mã xách nhận" class="imgCaptcha" src="/captcha.ashx?w=90&h=20"
                                        alt="captcha" />
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server"
                                    ErrorMessage="Bạn chưa nhập mã xác nhận" ControlToValidate="txtCaptcha" Display="Static" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnRegister" CssClass="buttonL" runat="server" Text="Đăng ký" OnClick="btnRegister_Click" />
                                <input type="reset" class="buttonS" value="Thoát" onclick="location.href='/'" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">
    $('.captcha img').click(function () {
        $('.imgCaptcha').attr('src', '/captcha.ashx?w=90&h=20&v=' + new Date().getTime());
    })
</script>
