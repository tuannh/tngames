<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.ascx.cs" Inherits="TNGames.Controls.Admin.UserEdit" %>
<h1 class="header">
    Cập nhật thông tin người chơi</h1>
<asp:Label ID="lblMsg" runat="server" />
<table width="100%">
    <tr>
        <td class="contentbold" style="width: 150px;">
            Họ tên
        </td>
        <td>
            <asp:TextBox ID="txtFullName" CssClass="text" runat="server" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Tên hiển thị
        </td>
        <td>
            <asp:TextBox ID="txtDisplayName" CssClass="text" runat="server" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Mật khẩu
        </td>
        <td>
            <asp:TextBox ID="txtPassword" CssClass="text" runat="server" TextMode="Password"
                MaxLength="150"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Mật khẩu không trùng khớp."
                ControlToValidate="txtPassword" ControlToCompare="txtRetypePass"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Xác nhận
        </td>
        <td>
            <asp:TextBox ID="txtRetypePass" CssClass="text" runat="server" TextMode="Password"
                MaxLength="150">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Ngày sinh
        </td>
        <td>
            <asp:DropDownList ID="ddlDay" runat="server" Width="50px" />
            <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" />
            <asp:TextBox ID="txtYear" ValidationGroup="info" runat="server" Width="50" Style="text-align: center;"
                MaxLength="4" />
            <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="yyyy"
                TargetControlID="txtYear" />
            <asp:RequiredFieldValidator ValidationGroup="info" ID="RequiredFieldValidator3" runat="server"
                ErrorMessage="Bạn chưa nhập năm sinh" ControlToValidate="txtYear" />
            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" runat="server"
                TargetControlID="txtYear" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            CMND
        </td>
        <td>
            <asp:TextBox ID="txtIDNumber" CssClass="text" runat="server" MaxLength="9"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Email
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="text" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Địa chỉ
        </td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="text" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Điện thoại
        </td>
        <td>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="text" MaxLength="15"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Thành phố
        </td>
        <td>
            <asp:DropDownList ID="ddlProvince" runat="server">
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
        <td class="contentbold">
            Trạng thái
        </td>
        <td>
            <label>
                <asp:RadioButton ID="radYes" runat="server" GroupName="active" />Kích hoạt</label>
            <label>
                <asp:RadioButton ID="radNo" runat="server" Checked="true" GroupName="active" />Chưa
                kích hoạt</label>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Điểm số
        </td>
        <td>
            <asp:TextBox ID="txtPoint" CssClass="text" runat="server" MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <label>
                <asp:RadioButton ID="radIsAdmin" runat="server" GroupName="admin" />Admin</label>
            <label>
                <asp:RadioButton ID="radIsUser" runat="server" GroupName="admin" />User</label>
            </label>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Xóa" OnClientClick="return confirm('Bạn muốn xóa người chơi này không?');"
                OnClick="btnDelete_Click" />
            <input type="button" value="Thoát" onclick="location.href='/admincp/user-list/'" />
        </td>
    </tr>
</table>
