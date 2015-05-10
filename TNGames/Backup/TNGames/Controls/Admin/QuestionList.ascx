<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionList.ascx.cs"
    Inherits="TNGames.Controls.Admin.QuestionList" %>
<asp:Panel ID="pnlQuestionGame" runat="server">
    <h1 class="header">
        Danh sách bộ đề</h1>
    <asp:Label ID="lblMsgQG" runat="server" />
    <div class="containTable">
        <asp:Repeater ID="rptQG" runat="server" OnItemCommand="rptQG_ItemCommand" OnItemDataBound="rptQG_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Tên bộ đề
                            </td>
                            <td width="300">
                                Số câu hỏi
                            </td>
                            <td width="120">
                                Trạng thái
                            </td>
                            <td width="120">
                                Người chơi
                            </td>
                            <td width="120">
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table<%# Eval("Id").ToString() == TNGames.Core.Helper.TNHelper.GetQuestionGameSettings().QuestionGameID.ToString() ? " active" : "" %>">
                    <td>
                        <%# Eval("QuestionGameName")%>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("Questionses.Count")%>
                    </td>
                    <td style="text-align: center;">
                        <%#  Convert.ToBoolean(Eval("Active")) ? "Hiển thị" : "Ẩn" %>
                    </td>
                     <td style="text-align: center;">
                        <%# Eval("QuestionUserses.Count")%>
                    </td>
                    <td style="text-align: center;">
                        <a href="/admincp/question-edit/<%# Eval("Id") %>">Sửa</a> | <a href="/admincp/question-list/<%# Eval("Id") %>?isd=true">
                            Chi tiết</a> |
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                            Text="Xóa" OnClientClick="return confirm('Bạn thức sự muốn xóa bộ đề câu hỏi này không?')"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table> </div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerQG" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlList" runat="server">
    <h1 class="header">
        Chi tiết:
        <asp:Literal ID="litQGName" runat="server" />
    </h1>
    <b><a href="/admincp/question-list" style="float: right; margin-top: -20px;" title="Quay lại danh sách bộ đề game câu hỏi">
        Quay lại</a></b>
    <asp:Label ID="lblMsg" runat="server" />
    <div class="containTable">
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
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
                            <td width="120">
                                Trạng thái
                            </td>
                            <td width="120">
                                Số điểm thưởng
                            </td>
                            <td width="120" style="display: none;">
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Eval("QuestionName")%>
                    </td>
                    <td>
                        <asp:Repeater ID="rptAnswer" runat="server">
                            <HeaderTemplate>
                                <ul style="margin-left: 20px;">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li <%# Eval("AnswerText").ToString().Length == 0 ? "style='display: none;'" : "" %>>
                                    <asp:Literal ID="litAnswerText" runat="server" />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td style="text-align: center;">
                        <%#  Convert.ToBoolean(Eval("Active")) ? "Hiển thị" : "Ẩn" %>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("BonusPoint")%>
                    </td>
                    <td style="text-align: center; display: none;">
                        <a href="/admincp/question-edit/<%# Eval("Id") %>">Sửa</a> |
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                            Text="Xóa" OnClientClick="return confirm('Bạn thức sự muốn xóa câu hỏi này không?\nLưu ý: Tất cả thông tin người chơi liên quan đến bộ đề này sẽ bị xóa.')"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerList" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlRank" runat="server">
    <h1 class="header">
        Bảng xếp hạng game trả lời câu hỏi</h1>
    <asp:Label ID="lblMsg2" runat="server" />
    <div class="containTable">
        <asp:Repeater ID="rptRank" runat="server" OnItemCommand="rptRank_ItemCommand" OnItemDataBound="rptRank_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Người chơi
                            </td>
                            <td width="120">
                                Số điểm
                            </td>
                            <td width="120">
                                Hạng
                            </td>
                            <td width="120">
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Eval("ItemArray[1]") %>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("ItemArray[3]") %>
                    </td>
                    <td style="text-align: center;">
                        <%# Eval("ItemArray[4]") %>
                    </td>
                    <td style="text-align: center;">
                        <a href="/admincp/user-games/<%# Eval("ItemArray[0]") %>?type=2">Chi tiết</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerRank" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
