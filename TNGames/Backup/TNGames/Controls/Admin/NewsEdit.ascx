<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsEdit.ascx.cs" Inherits="TNGames.Controls.Admin.NewsEdit" %>
<h1 class="header">
    Thêm mới/cập nhật tin tức</h1>
<table width="100%">
    <tr>
        <td class="contentbold" style="width: 150px;">
            Tiêu đề
        </td>
        <td>
            <asp:TextBox ID="txtTitle" CssClass="text" runat="server" MaxLength="150" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                ForeColor="Red" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Alias
        </td>
        <td>
            <asp:TextBox ID="txtAlias" CssClass="text" runat="server" MaxLength="150" />
        </td>
    </tr>
    <tr>
        <td class="contentbold">
            Photo
        </td>
        <td>
            <asp:FileUpload ID="fuPhoto" runat="server" />
            <asp:Image ID="imgPhoto" Style="max-width: 300px; max-height: 300px;" runat="server" Visible="false" />
        </td>
    </tr>
    <tr style="display: none;">
        <td class="contentbold">
            Loại tin tức
        </td>
        <td>
            <asp:DropDownList ID="ddlCategories" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="contentbold" valign="top">
            Tóm tắt
        </td>
        <td>
            <asp:TextBox ID="txtSummary" CssClass="textarea" runat="server" TextMode="MultiLine"
                Height="80px" />
        </td>
    </tr>
    <tr>
        <td class="contentbold" valign="top">
            Nội dung
        </td>
        <td>
            <asp:TextBox ID="txtContent" runat="server" CssClass="ckeditor" TextMode="MultiLine"
                Height="120px" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Xóa" Visible="false" OnClientClick="return confirm('Bạn thực sự muốn xóa tin này không?')"
                OnClick="btnDelete_Click" />
            <input type="button" onclick="location.href='/admincp/news-list'" value="Thoát" />
        </td>
    </tr>
</table>
