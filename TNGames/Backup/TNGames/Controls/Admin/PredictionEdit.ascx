<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PredictionEdit.ascx.cs"
    Inherits="TNGames.Controls.Admin.PredictionEdit" %>
<h1 class="header">
    Thêm/sửa
</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <table width="100%">
        <tr>
            <td class="contentbold" style="width: 150px;">
                Tên bộ đề
            </td>
            <td>
                <asp:TextBox ID="txtQGName" CssClass="text" runat="server" MaxLength="250" />
                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="Tên bộ đề câu hỏi không thể rỗng" ValidationGroup="QG" ControlToValidate="txtQGName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="contentbold">
                Trạng thái
            </td>
            <td>
                <label>
                    <asp:RadioButton ID="radYes" Checked="true" runat="server" GroupName="status" />
                    Hiển thị
                </label>
                <label>
                    <asp:RadioButton ID="radNo" runat="server" GroupName="status" />
                    Ẩn
                </label>
            </td>
        </tr>
        <tr>
            <td class="contentbold" valign="top">
                Danh sách câu hỏi
            </td>
            <td>
                <asp:Repeater ID="rptQuestion" runat="server" ClientIDMode="Predictable" OnItemCommand="rptQuestion_ItemCommand"
                    OnItemDataBound="rptQuestion_ItemDataBound">
                    <HeaderTemplate>
                        <div class="containBG">
                            <table cellspacing="1" cellpadding="5" border="0" width="100%">
                                <tr class="headertable">
                                    <td height="30">
                                        Câu hỏi
                                    </td>
                                    <td width="300">
                                        Đáp án
                                    </td>
                                    <td width="76">
                                        Điểm thưởng
                                    </td>
                                    <td width="50">
                                        Thao tác
                                    </td>
                                </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="Table">
                            <td valign="middle">
                                <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("Id") %>' />
                                <asp:TextBox ID="txtQuestionName" TextMode="MultiLine" Height="90px" Width="350px"
                                    MaxLength="150" runat="server" Text='<%# Eval("PredictionName")%>' />
                                <asp:RequiredFieldValidator ValidationGroup="QG" ForeColor="Red" ID="rfv" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtQuestionName" />
                            </td>
                            <td>
                                <asp:Repeater ID="rptAnswer" runat="server" ClientIDMode="Predictable">
                                    <HeaderTemplate>
                                        <ul style="list-style: none;">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li <%# Eval("AnswerText").ToString() == "" ? "class='invisible'" : "" %>>
                                            <label class="editradio">
                                                <asp:RadioButton ID="radAnswer" runat="server" Checked='<%# Eval("IsCorrectAnswer") %>'
                                                    GroupName="Answer" /></label>
                                            <asp:TextBox ID="txtAnswer" ValidationGroup="QG" runat="server" MaxLength="260" Width="200px"
                                                Text='<%# Eval("AnswerText") %>' />
                                            <asp:RequiredFieldValidator ValidationGroup="norequire" ForeColor="Red" ID="rfv"
                                                runat="server" ErrorMessage="*" ControlToValidate="txtAnswer" />
                                            <asp:HiddenField ID="hfAnswerID" runat="server" Value='<%# Eval("Id") %>' />
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <input type="button" disabled="disabled" class="clearEditSelected" value="Xóa chọn" />
                            </td>
                            <td style="text-align: center;">
                                <asp:TextBox ID="txtBonus" CssClass="number" Width="60px" runat="server" Text='<%# Eval("BonusPoint")%>' />
                                <act:FilteredTextBoxExtender ID="ftx" runat="server" FilterType="Numbers" TargetControlID="txtBonus" />
                            </td>
                            <td style="text-align: center;">
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Container.ItemIndex %>'
                                    Text="Xóa" OnClientClick="return confirm('Bạn thức sự muốn xóa câu hỏi này không?')"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></div>
                    </FooterTemplate>
                </asp:Repeater>
                <% if (rptQuestion.Items.Count == 0)
                   { %>
                <span class="warning"><b>Bạn chưa nhập câu hỏi cho bộ đề</b></span>
                <%} %>
            </td>
        </tr>
        <tr id="trAdd" runat="server" class="tradd">
            <td>
            </td>
            <td>
                <input type="button" class="btnAddQ" value="Thêm câu hỏi" />
            </td>
        </tr>
    </table>
    <fieldset class="addtable" style="display: none;">
        <legend>Thêm câu hỏi</legend>
        <table width="100%">
            <tr>
                <td class="contentbold" style="width: 150px;">
                    Câu hỏi
                </td>
                <td>
                    <asp:TextBox ID="txtQuestion" ValidationGroup="new" TextMode="MultiLine" Height="50px"
                        CssClass="text" runat="server" MaxLength="250" />
                    <asp:RequiredFieldValidator ValidationGroup="new" ForeColor="Red" ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="Tên dự đoán không thể rỗng" ControlToValidate="txtQuestion"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentbold">
                    Danh sách đáp án
                </td>
                <td>
                    <span class="answerRequire warning">Bạn phải chọn một đáp án</span><br />
                    <asp:Repeater ID="rptAnswers" runat="server" ClientIDMode="Predictable" OnItemDataBound="rptAnswers_ItemDataBound">
                        <ItemTemplate>
                            <label class="radio">
                                <asp:RadioButton ID="radAnswer" runat="server" GroupName="Answer" /></label>
                            <asp:TextBox ID="txtAnswer" ValidationGroup="new" runat="server" MaxLength="150"
                                Width="300px" />
                            <asp:RequiredFieldValidator ValidationGroup="norequire" ForeColor="Red" ID="rfv"
                                runat="server" ErrorMessage="Bạn chưa nhập đáp án" ControlToValidate="txtAnswer" />
                            <asp:HiddenField ID="hfAnswerID" runat="server" />
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <br />
                        </SeparatorTemplate>
                    </asp:Repeater>
                    <br />
                    <input type="button" disabled="disabled" class="clearSelected" value="Xóa chọn" />
                </td>
            </tr>
            <tr>
                <td class="contentbold">
                    Điểm thưởng
                </td>
                <td>
                    <asp:TextBox ID="txtBonus" CssClass="number" ValidationGroup="new" runat="server"
                        MaxLength="10" Width="100" />
                    <asp:RequiredFieldValidator ValidationGroup="new" ForeColor="Red" ID="RequiredFieldValidator3"
                        runat="server" ErrorMessage="Bạn chưa nhập điểm thưởng. Điểm thưởng phải là một số dương"
                        ControlToValidate="txtBonus"></asp:RequiredFieldValidator>
                    <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                        TargetControlID="txtBonus" />
                </td>
            </tr>
            <tr style="display: none;">
                <td class="contentbold">
                    Thứ tự hiển thị
                </td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" Text="0" MaxLength="3" Width="100" />
                    <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                        TargetControlID="txtOrder" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="new" Text="Thêm" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <table width="100%">
        <tr>
            <td class="contentbold" style="width: 150px;">
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" ValidationGroup="QG" Text="Lưu" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Xóa" OnClientClick="return confirm('Bạn thực sự muốn xóa bộ đề câu hỏi này không?')"
                    OnClick="btnDelete_Click" />
                <input type="button" value="Thoát" onclick="location.href='/admincp/prediction-list'" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
//<![CDATA[
    $('.answerRequire').hide();
    <% if (!(Page.RouteData.Values["id"] is string)){ %>
    $('.editradio :radio, .radio :radio, .clearEditSelected, .clearSelected').hide();
    <%} %>
    $(document).ready(function () {
        $('.radio>input').change(function () {
            if ($(this)[0].checked) {
                $('.radio>input').attr('checked', false);
                $(this)[0].checked = true;
            }

            $('.clearSelected').attr('disabled', false);
            //IsValidAnswers();
        })

        $('.editradio>input').change(function () {
            if ($(this)[0].checked) {
                $(this).parent().parent().parent().find(':radio').attr('checked', false);
                $(this)[0].checked = true;
            }

            $(this).parent().parent().parent().parent().find('.clearEditSelected').attr('disabled', false);
        })
    })

    //    $('form').submit(function () {
    //        if ($('#txtQuestion').val() == '')
    //            return true;

    //        return IsValidAnswers();
    //    })

    function IsValidAnswers() {

        $('.answerRequire').hide();
        var ischecked = false;

        $('.radio>input').each(function (i, e) {
            if (!ischecked)
                ischecked = e.checked
        })

        if (!ischecked)
            $('.answerRequire').show();

        return ischecked;
    }

    $('.tradd').click(function () {
        $('.addtable').slideDown();
        $(this).hide();
    })

    $('.clearSelected').click(function () {
        $('.addtable .radio input').attr('checked', false);
        this.disabled = true;
    })

    $('.clearEditSelected').click(function () {
        $(this).parent().find(':radio').attr('checked', false);
        this.disabled = true;
    })

//]]>
</script>
