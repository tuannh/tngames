<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionGameSettings.ascx.cs"
    Inherits="TNGames.Controls.Admin.QuestionGameSettings" %>
<h1 class="header">
    Cấu hình</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <table width="100%">
        <tr>
            <td class="contentbold" style="width: 150px;">
                Bộ đề câu hỏi
            </td>
            <td>
                <asp:DropDownList ID="ddlQG" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="Bạn chưa chọn bộ đề câu hỏi" ControlToValidate="ddlQG"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Thời gian
            </td>
            <td>
                <asp:TextBox ID="txtTime" ToolTip="Tổng thời gian cho phép để trả lời các câu hỏi"
                    runat="server" MaxLength="5" />
                (giây)
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator4" runat="server"
                    ErrorMessage="Bạn chưa nhập thời gian dự đoán" ControlToValidate="txtTime"></asp:RequiredFieldValidator>
                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                    TargetControlID="txtTime" />
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Số lần chơi trong ngày
            </td>
            <td>
                <asp:TextBox ID="txtPlayNum" runat="server" MaxLength="3" />
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator3" runat="server"
                    ErrorMessage="Bạn chưa nhập thời gian dự đoán" ControlToValidate="txtPlayNum"></asp:RequiredFieldValidator>
                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                    TargetControlID="txtPlayNum" />
            </td>
        </tr>
        <tr>
            <td class="contentbold">
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
        <tr>
            <td colspan="2">
                <br />
                <b>Ghi chú: Nếu thời gian = 0 sẽ không hiển thị đồng hồ đếm ngược </b>
            </td>
        </tr>
    </table>
</div>
