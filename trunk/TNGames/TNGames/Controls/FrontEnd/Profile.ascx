<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="TNGames.Controls.FrontEnd.Profile" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/profile_header.png" alt="" width="194" height="39" />
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
                <asp:Panel ID="pnlInfo" runat="server">
                    <asp:Label ID="lblMsg" runat="server" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td width="25%" align="right">
                                Họ và Tên (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullName" ValidationGroup="info" runat="server" MaxLength="150" />
                                <asp:RequiredFieldValidator ValidationGroup="info" ForeColor="Red" ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="Bạn chưa nhập họ và tên" ControlToValidate="txtFullName" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Tên hiển thị (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtDisplayName" ValidationGroup="info" runat="server" MaxLength="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Ngày sinh (*):
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDay" runat="server" Width="50px" />
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" />
                                <asp:TextBox ID="txtYear" ValidationGroup="info" runat="server" Width="50" Style="text-align: center;"
                                    MaxLength="4" />
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy"
                                    TargetControlID="txtYear" />
                                <asp:RequiredFieldValidator ValidationGroup="info" ForeColor="Red" ID="RequiredFieldValidator3"
                                    runat="server" ErrorMessage="Bạn chưa nhập năm sinh" ControlToValidate="txtYear" />
                                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" runat="server"
                                    TargetControlID="txtYear" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                CMND (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtIDNumber" ValidationGroup="info" runat="server" MaxLength="9"></asp:TextBox>
                                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIDNumber" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Điện thoại (*):
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone" ValidationGroup="info" runat="server" MaxLength="15"></asp:TextBox>
                                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                    TargetControlID="txtPhone" />
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Địa chỉ:
                            </td>
                            <td>
                                <asp:TextBox ValidationGroup="info" ID="txtAddress" runat="server" MaxLength="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                                Tỉnh/Thành phố:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProvince" runat="server" ValidationGroup="info">
                                    <asp:ListItem Value="82" Text="An Giang"></asp:ListItem>
                                    <asp:ListItem Value="102" Text="Bà Rịa - Vũng Tàu" />
                                    <asp:ListItem Value="109" Text="Bình Dương" />
                                    <asp:ListItem Value="110" Text="Bình Phước" />
                                    <asp:ListItem Value="111" Text="Bình Thuận" />
                                    <asp:ListItem Value="161" Text="Bình Trị Thiên" />
                                    <asp:ListItem Value="108" Text="Bình Định" />
                                    <asp:ListItem Value="105" Text="Bạc Liêu" />
                                    <asp:ListItem Value="103" Text="Bắc Giang" />
                                    <asp:ListItem Value="104" Text="Bắc Kạn" />
                                    <asp:ListItem Value="106" Text="Bắc Ninh" />
                                    <asp:ListItem Value="107" Text="Bến Tre" />
                                    <asp:ListItem Value="112" Text="Cao Bằng" />
                                    <asp:ListItem Value="81" Text="Cà Mau" />
                                    <asp:ListItem Value="7" Text="Cần Thơ" />
                                    <asp:ListItem Value="162" Text="Cửu Long" />
                                    <asp:ListItem Value="116" Text="Gia Lai" />
                                    <asp:ListItem Value="123" Text="Hoà Bình" />
                                    <asp:ListItem Value="201" Text="Hà Bắc" />
                                    <asp:ListItem Value="117" Text="Hà Giang" />
                                    <asp:ListItem Value="118" Text="Hà Nam" />
                                    <asp:ListItem Value="159" Text="Hà Nam Ninh" />
                                    <asp:ListItem Value="5" Text="Hà Nội" />
                                    <asp:ListItem Value="119" Text="Hà Tây" />
                                    <asp:ListItem Value="120" Text="Hà Tĩnh" />
                                    <asp:ListItem Value="124" Text="Hưng Yên" />
                                    <asp:ListItem Value="121" Text="Hải Dương" />
                                    <asp:ListItem Value="158" Text="Hải Hưng" />
                                    <asp:ListItem Value="101" Text="Hải Phòng" />
                                    <asp:ListItem Value="122" Text="Hậu Giang" />
                                    <asp:ListItem Value="157" Text="Khác" />
                                    <asp:ListItem Value="125" Text="Khánh Hoà" />
                                    <asp:ListItem Value="126" Text="Kiên Giang" />
                                    <asp:ListItem Value="127" Text="Kon Tum" />
                                    <asp:ListItem Value="128" Text="Lai Châu" />
                                    <asp:ListItem Value="132" Text="Long An" />
                                    <asp:ListItem Value="131" Text="Lào Cai" />
                                    <asp:ListItem Value="129" Text="Lâm Đồng" />
                                    <asp:ListItem Value="130" Text="Lạng Sơn" />
                                    <asp:ListItem Value="133" Text="Nam Định" />
                                    <asp:ListItem Value="160" Text="Nghĩa Bình" />
                                    <asp:ListItem Value="134" Text="Nghệ An" />
                                    <asp:ListItem Value="135" Text="Ninh Bình" />
                                    <asp:ListItem Value="136" Text="Ninh Thuận" />
                                    <asp:ListItem Value="137" Text="Phú Thọ" />
                                    <asp:ListItem Value="138" Text="Phú Yên" />
                                    <asp:ListItem Value="139" Text="Quảng Bình" />
                                    <asp:ListItem Value="140" Text="Quảng Nam" />
                                    <asp:ListItem Value="141" Text="Quảng Ngãi" />
                                    <asp:ListItem Value="142" Text="Quảng Ninh" />
                                    <asp:ListItem Value="143" Text="Quảng Trị" />
                                    <asp:ListItem Value="144" Text="Sóc Trăng" />
                                    <asp:ListItem Value="145" Text="Sơn La" />
                                    <asp:ListItem Value="3" Text="TP.Hồ Chí Minh" />
                                    <asp:ListItem Value="149" Text="Thanh Hoá" />
                                    <asp:ListItem Value="147" Text="Thái Bình" />
                                    <asp:ListItem Value="148" Text="Thái Nguyên" />
                                    <asp:ListItem Value="150" Text="Thừa Thiên Huế" />
                                    <asp:ListItem Value="151" Text="Tiền Giang" />
                                    <asp:ListItem Value="152" Text="Trà Vinh" />
                                    <asp:ListItem Value="153" Text="Tuyên Quang" />
                                    <asp:ListItem Value="146" Text="Tây Ninh" />
                                    <asp:ListItem Value="154" Text="Vĩnh Long" />
                                    <asp:ListItem Value="155" Text="Vĩnh Phúc" />
                                    <asp:ListItem Value="156" Text="Yên Bái" />
                                    <asp:ListItem Value="114" Text="Điện Biên" />
                                    <asp:ListItem Value="9" Text="Đà Nẵng" />
                                    <asp:ListItem Value="6" Text="Đắc Lắk" />
                                    <asp:ListItem Value="113" Text="Đắc Nông" />
                                    <asp:ListItem Value="8" Text="Đồng Nai" />
                                    <asp:ListItem Value="115" Text="Đồng Tháp" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" align="right">
                            </td>
                            <td>
                                <asp:Button ID="btnSave" CssClass="buttonL" runat="server" Text="Cập nhật" OnClick="btnSave_Click" ValidationGroup="info" />
                            </td>
                        </tr>
                    </table>
                    <p class="warning">
                        <b>Lưu ý:</b> Thông tin CMND và ngày sinh chỉ được cập nhật 1 lần. Bạn hãy cập nhập
                        chính xác thông tin này vì thông tin này được dùng để kiếm chứng khi trao thưởng.
                    </p>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlPassword" runat="server">
                    <asp:Label ID="lblMsgPassword" runat="server" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td style="width: 25%" align="right">
                                <strong>Account và mật khẩu:</strong>
                            </td>
                            <td>
                                <hr width="100%" size="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Email:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="150" ValidationGroup="info"></asp:TextBox>
                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator4" ValidationGroup="info"
                                    runat="server" ErrorMessage="Bạn chưa nhập địa chỉ email" Display="Dynamic" ControlToValidate="txtEmail" />
                                <asp:RegularExpressionValidator ForeColor="Red" ValidationGroup="info" ID="RegularExpressionValidator1"
                                    runat="server" ErrorMessage="Đia chỉ email cung cấp không hợp lệ" ControlToValidate="txtEmail"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mật khẩu cũ:
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" MaxLength="150"
                                    ValidationGroup="password"></asp:TextBox>
                                <asp:RequiredFieldValidator ForeColor="Red" ControlToValidate="txtOldPassword" ID="RequiredFieldValidator2"
                                    runat="server" ValidationGroup="password" ErrorMessage="Bạn chưa nhập mật khẩu cũ"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mật khẩu mới:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="150"
                                    ValidationGroup="password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" runat="server"
                                    ValidationGroup="password" ErrorMessage="Mật khẩu mới không thể rỗng" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Xác nhận mật khẩu mới:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRetypePass" runat="server" TextMode="Password" MaxLength="150"
                                    ValidationGroup="password"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" ForeColor="Red" runat="server" ErrorMessage="Mật khẩu không trùng khớp"
                                    ControlToValidate="txtRetypePass" ControlToCompare="txtPassword" ValidationGroup="password" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="buttonL" Text="Cập nhật" ValidationGroup="password"
                                    OnClick="btnUpdate_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlPoint" runat="server">
                    <asp:Label ID="lblMsgPoint" runat="server" />
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                            <td style="width: 25%" align="right">
                                <b>Số điểm hiển tại của bạn:</b>
                            </td>
                            <td>
                                <asp:Literal ID="litPoint" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" align="right">
                            </td>
                            <td>
                                <asp:Button ID="btnResetAccount" CssClass="buttonS" runat="server" Text="Reset" OnClientClick="return confirm('Bạn muốn khởi tạo lại số điểm mặc định?\nLưu ý: Tài khoản điểm của bạn sẽ trở lại số điểm được tặng lúc mở tài khoản và các game bạn đang chơi chưa được tính điểm sẽ bị xóa'); "
                                    CausesValidation="false" OnClick="btnResetAccount_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
