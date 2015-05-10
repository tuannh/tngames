<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BettingGameSettings.ascx.cs"
    Inherits="TNGames.Controls.Admin.BettingGameSettings" %>
<h1 class="header">
    Cấu hình</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <table width="100%">
        <tr>
            <td class="contentbold" style="width: 250px;">
                Tạm dừng game này
            </td>
            <td>
                <label>
                    <asp:RadioButton ID="radPauseYes" runat="server" GroupName="Pause" />Đồng ý</label>
                <label>
                    <asp:RadioButton ID="radPauseNo" runat="server" Checked="true" GroupName="Pause" />Bỏ
                    qua</label>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Cho phép xóa trận đấu khi có người chơi
            </td>
            <td>
                <label>
                    <asp:RadioButton ID="radAllowDeleteYes" runat="server" GroupName="AllowDelete" />Đồng
                    ý</label>
                <label>
                    <asp:RadioButton ID="radAllowDeleteNo" runat="server" Checked="true" GroupName="AllowDelete" />Bỏ
                    qua</label>
            </td>
        </tr>      
        <tr>
            <td class="contentbold">
                Số lượng xếp hạng tối đa
            </td>
            <td>
                <asp:TextBox ID="txtMaxDisplayItem" Width="100" CssClass="text" runat="server" MaxLength="5" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMaxDisplayItem"
                    ForeColor="Red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                    TargetControlID="txtMaxDisplayItem" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
                <input type="button" value="Thoát" onclick="location.href='/admincp/question-list'" />
            </td>
        </tr>
    </table>
</div>
