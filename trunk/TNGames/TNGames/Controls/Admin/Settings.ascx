<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="TNGames.Controls.Admin.Settings" %>
<h1 class="header">
    Cấu hình</h1>
<asp:Label ID="lblMsg" runat="server" />
<table style="width: 100%;">
    <tr>
        <td class="contentbold" style="width: 150px;">
            Smtp server
        </td>
        <td>
            <asp:TextBox ID="txtSmtpServer" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Smtp port
        </td>
        <td>
            <asp:TextBox ID="txtSmtpPort" CssClass="text" runat="server" MaxLength="5" Text="80" />
            <act:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="txtSmtpPort" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Smtp authentication
        </td>
        <td>
            <asp:CheckBox ID="chkSmtpAuthentication" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Smtp username
        </td>
        <td>
            <asp:TextBox ID="txtSmtpUsername" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Smtp password
        </td>
        <td>
            <asp:TextBox ID="txtSmtpPassword" CssClass="text" runat="server" TextMode="Password"
                MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Xác nhận password
        </td>
        <td>
            <asp:TextBox ID="txtRetypeSmtpPassword" CssClass="text" runat="server" TextMode="Password"
                MaxLength="150" />
            <asp:CompareValidator ForeColor="Red" ID="CompareValidator1" runat="server" ErrorMessage="Mật khẩu không trùng khớp"
                ControlToValidate="txtRetypeSmtpPassword" ControlToCompare="txtSmtpPassword"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Site Url
        </td>
        <td>
            <asp:TextBox ID="txtSiteUrl" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Người gởi mail (mặc định)
        </td>
        <td>
            <asp:TextBox ID="txtDefaultSender" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Subject email kích họat
        </td>
        <td>
            <asp:TextBox ID="txtAcitveSubject" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Body email kích hoạt<br />
            (Biến hỗ trợ: $SiteUrl$, $Username$, $Password$, $ActiveLink$, $AcitveCode$)
        </td>
        <td>
            <asp:TextBox ID="txtActiveEmailTemplate" CssClass="textarea" runat="server" TextMode="MultiLine"
                Height="100" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Reset Subject email
        </td>
        <td>
            <asp:TextBox ID="txtResetSubject" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Reset body email
            <br />
            (Biến hỗ trợ: $SiteUrl$, $Username$, $Password$, $ActiveLink$, $AcitveCode$)
        </td>
        <td>
            <asp:TextBox ID="txtRestBody" runat="server" CssClass="textarea" TextMode="MultiLine"
                Height="100" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Số điểm mặc định
        </td>
        <td>
            <asp:TextBox ID="txtDefaultPoint" CssClass="text" runat="server" MaxLength="10" />
            <act:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="txtDefaultPoint" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Số lượng xếp hạng tối đa
        </td>
        <td>
            <asp:TextBox ID="txtHomeDisplayItem"  CssClass="text" runat="server" MaxLength="5" />
            <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                TargetControlID="txtDefaultPoint" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
            <input type="button" value="Thoát" onclick="location.href='/'" />
        </td>
    </tr>
</table>
