<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="TNGames.Controls.Admin.Banner" %>
<h1 class="header">
    <asp:Literal ID="litType" runat="server" />
</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <asp:Panel ID="pnlList" runat="server">
        <asp:Repeater ID="rptList" runat="server" ClientIDMode="Predictable" OnItemCommand="rptList_ItemCommand"
            OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Banner
                            </td>
                            <td width="100">
                                Hiển thị
                            </td>
                            <td width="100">
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <tr class="Table">
                        <td>
                            <asp:HiddenField ID="hfContentID" runat="server" Value='<%# Eval("Id") %>' />
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" Height="50px" ValidationGroup="edit"
                                Width="96%" CssClass="myeditor" runat="server" Text='<%# Eval("ContentText") %>' />
                            <asp:RequiredFieldValidator ID="rfvedit" ForeColor="red" runat="server" ErrorMessage="*"
                                ControlToValidate="txtContent" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: center;">
                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lnkDelete" CommandArgument='<%# Eval("Id") %>' runat="server"
                                CommandName="delete" Text="Xóa" OnClientClick="return confirm('Bạn thực sự muốn xóa banner này không?');"></asp:LinkButton>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <div style="float: right;">
            <pager:Pager ID="pagerList" runat="server" CompactModePageCount="10" NormalModePageCount="10"
                OnCommand="pager_Command" />
        </div>
        <asp:Button ID="btnSave" runat="server" ValidationGroup="edit" Text="Lưu sửa đổi"
            OnClick="btnSave_Click" />
    </asp:Panel>
    <br />
    <input type="button" value="Thêm banner" id="btnAddBanner" />
    <asp:Panel ID="pnlAdd" runat="server" CssClass="invisible">
        <fieldset class="addtable">
            <legend>Thêm banner</legend>
            <table width="100%">
                <tr>
                    <td class="contentbold" style="width: 150px; vertical-align: text-top;">
                        Banner
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent" ValidationGroup="add" TextMode="MultiLine" Height="50px"
                            CssClass="text myeditor txtContent" runat="server" MaxLength="250" />
                        <asp:RequiredFieldValidator EnableClientScript="true" ValidationGroup="add" ForeColor="Red"
                            ID="rfvadd" runat="server" ErrorMessage="Bạn chưa nhập nội dung thông báo" ControlToValidate="txtContent"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="contentbold">
                        Trang thái
                    </td>
                    <td>
                        <label>
                            <asp:RadioButton ID="radYes" Checked="true" ValidationGroup="add" runat="server"
                                GroupName="active" />Hiển thị</label>
                        <label>
                            <asp:RadioButton ID="radNo" runat="server" ValidationGroup="add" GroupName="active" />Ẩn</label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input id="btnAddTemp" type="button" value="Thêm" />
                        <asp:Button ID="btnAdd" runat="server" style="display: none;" ValidationGroup="add" Text="Thêm" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</div>
<script type="text/javascript">
//<![CDATA[
    function loadEditor(objId) {
        var instance = CKEDITOR.instances[objId];
        if (instance != null) {
            CKEDITOR.remove(instance);
            instance.destroy();
            instance = null;
        }
        CKEDITOR.replace(objId, { toolbar: 'Basic', width: '96%', height: '100px' });
    }

    $(document).ready(function () {
        $('.myeditor').each(function (i, e) {
            loadEditor(e.id);
        })
        $('#btnAddBanner').click(function () {
            $(this).hide();
            $('#pnlAdd').slideDown();
        })
    })

    $('#btnAddTemp').click(function () {
        updateEditorValue();
        $('#btnAdd').click();
    })

    function updateEditorValue() {
        txt = $('.txtContent');
        if (txt.val().replace(/\s/g, '') == '') {
            id = txt.attr('id');
            editor = loadEditor(id);
            if (editor) {
                editor.updateElement();
            }
        }
    }
//]]>
</script>
