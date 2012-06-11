<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.ForgotPassword" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img alt="qmk" src="/images/web/quenmatkhau.png" alt="" width="201" height="39" />
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
                <asp:Label ID="lblMsg" runat="server" />
                <asp:Panel ID="pnlReset" runat="server">
                    <asp:Label ID="Label1" runat="server" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td width="25%" align="right" valign="top">
                                Email đăng ký (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="150"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Bạn chưa nhập địa chỉ email"
                                    Display="Dynamic" ControlToValidate="txtEmail" />
                                <asp:RegularExpressionValidator ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Đia chỉ email cung cấp không hợp lệ"
                                    ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSend" CssClass="buttonS" runat="server" Text="Gởi" OnClick="btnSend_Click" />
                                <input type="reset" class="buttonS" value="Reset" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
