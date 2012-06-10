<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BettingList.ascx.cs"
    Inherits="TNGames.Controls.Admin.BettingList" %>
<asp:Panel ID="pnlList" runat="server">
    <h1 class="header">
        Danh sách</h1>
    <asp:Label ID="lblMsg" runat="server" />
    <div class="containTable">
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Trận đấu
                            </td>
                            <td width="100">
                                Đội A
                            </td>
                            <td width="100">
                                Đội B
                            </td>
                            <td width="80">
                                Ngày thi đấu
                            </td>
                            <td width="60">
                                Tỷ số
                            </td>
                            <td width="80">
                                Trạng thái
                            </td>
                            <td width="80">
                                <span title="Số lượng người chơi">Người chơi</span>
                            </td>
                            <td width="150">
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Eval("BettingName")%>
                    </td>
                    <td>
                        <%# Eval("HomeTeam")%>
                    </td>
                    <td>
                        <%# Eval("VisitingTeam")%>
                    </td>
                    <td style="text-align: center;">
                        <%#  (Eval("PlayDate") as DateTime?).HasValue ? (Eval("PlayDate") as DateTime?).Value.ToString(TNGames.Core.Helper.TNHelper.DateTimeFormat) : "" %>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litScoreRate" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:LinkButton ID="lnkActive" runat="server" CommandName="active" CommandArgument='<%# Eval("Id") %>'
                            Text="Hiển thị"></asp:LinkButton>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litUserCount" runat="server" />
                    </td>
                    <td class="action" style="text-align: center;">
                        <asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# string.Format("/admincp/betting-edit/{0}", Eval("Id")) %>'>Sửa</asp:HyperLink>
                        <asp:HyperLink ID="lnkScore" runat="server" NavigateUrl='<%# string.Format("/admincp/betting-edit/{0}?scoreupdate=true", Eval("Id")) %>'>Cập nhật tỷ số</asp:HyperLink>
                        <asp:LinkButton ID="lnkPointCal" runat="server" CommandName="calculate" CommandArgument='<%# Eval("Id") %>'
                            Text="Tính điểm" OnClientClick="return confirm('Bạn thực sự muốn tính điểm cho người chơi không?')"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                            Text="Xóa" OnClientClick="return confirm('Bạn thực sự muốn xóa trận đấu này không?\nLưu ý: Tất cả thông tin người chơi liên quan sẽ bị xóa.')"></asp:LinkButton>
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
    <script type="text/javascript">
//<![CDATA[
        $('.action').each(function () {
            $(this).find('a').each(function (i, e) {
                if (i > 0)
                    $(e).before('&nbsp;|&nbsp;');
            })
        })
//]]>
    </script>
</asp:Panel>
<asp:Panel ID="pnlRank" runat="server">
    <h1 class="header">
        Bảng xếp hạng</h1>
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
                        <a href="/admincp/user-games/<%# Eval("ItemArray[0]") %>?type=0">Chi tiết</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table> </div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerRank" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
