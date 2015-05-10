<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserGames.ascx.cs" Inherits="TNGames.Controls.Admin.UserGames" %>
<asp:Label ID="lblMsg" runat="server" Visible="false" />
<asp:Literal ID="litUser" runat="server" Visible="false" />
<asp:Panel ID="pnlBetting" runat="server">
    <h1 class="header">
        Thông tin trò chơi thử tài phân tích trận đấu của
        <%= litUser.Text %>
    </h1>
    <%= lblMsg.Text %>
    <div class="containTable">
        <asp:Repeater ID="rptBetting" runat="server" OnItemCommand="rptBetting_ItemCommand"
            OnItemDataBound="rptBetting_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Trận đấu
                            </td>
                            <td width="70">
                                Tỷ số
                            </td>
                            <td width="200">
                                Chi tiết
                            </td>
                            <td width="150">
                                Số điểm thắng
                            </td>
                            <td width="150">
                                Ngày chơi
                            </td>
                            <td width="150">
                                Thời gian on site
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Eval("Betting.BettingName")%><br />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litScoreRate" runat="server" />
                    </td>
                    <td>
                        <asp:Repeater ID="rptBettingDetail" runat="server">
                            <ItemTemplate>
                                <div>
                                    Đội chọn:
                                    <%# Eval("SelectedTeam") %>
                                    <br />
                                    Tỷ lệ:
                                    <%# Eval("BettingRate.HomeRateText")%>
                                    :
                                    <%# Eval("BettingRate.VisitingRateText")%>
                                    <br />
                                    Số điểm:
                                    <%# Eval("BettingPoint") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litWinPoint" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <%# Convert.ToDateTime(Eval("BettingDate")).ToString(TNGames.Core.Helper.TNHelper.DateTimeFormat)%>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litOnSiteTime" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerBetting" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlPrediction" runat="server">
    <h1 class="header">
        Thông tin trò chơi thử tài dự đoán của
        <%= litUser.Text %>
    </h1>
    <%= lblMsg.Text %>
    <div class="containTable">
        <asp:Repeater ID="rptPrediction" runat="server" OnItemCommand="rptPrediction_ItemCommand"
            OnItemDataBound="rptPrediction_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Ngày chơi
                            </td>
                            <td width="150">
                                Số câu hỏi
                            </td>
                            <td width="150">
                                Số câu trả lời đúng
                            </td>
                            <td width="150">
                                Điểm thưởng
                            </td>
                            <td width="150">
                                Thời gian on site
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Convert.ToDateTime(Eval("PlayDate")).ToString(TNGames.Core.Helper.TNHelper.DateTimeFormat) %>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litTotalQuestion" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litRightAnswer" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litBonusPoint" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litOnSiteTime" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerPrediction" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlQuestion" runat="server">
    <h1 class="header">
        Thông tin trò chơi thử tài kiến thức của
        <%= litUser.Text %>
    </h1>
    <%= lblMsg.Text %>
    <div class="containTable">
        <asp:Repeater ID="rptQuestion" runat="server" OnItemCommand="rptQuestion_ItemCommand"
            OnItemDataBound="rptQuestion_ItemDataBound">
            <HeaderTemplate>
                <div class="containBG">
                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                        <tr class="headertable">
                            <td height="30">
                                Ngày chơi
                            </td>
                            <td width="150">
                                Số câu hỏi
                            </td>
                            <td width="150">
                                Số câu trả lời đúng
                            </td>
                            <td width="150">
                                Điểm thưởng
                            </td>
                            <td width="150">
                                Thời gian on site
                            </td>
                            <td>
                                Thao tác
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Table">
                    <td>
                        <%# Convert.ToDateTime(Eval("PlayDate")).ToString(TNGames.Core.Helper.TNHelper.DateTimeFormat) %>
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litTotalQuestion" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litRightAnswer" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litBonusPoint" runat="server" />
                    </td>
                    <td style="text-align: center;">
                        <asp:Literal ID="litOnSiteTime" runat="server" />
                    </td>
                    <td style="text-align: center; ">
                        <asp:LinkButton ID="linkDelete" runat="server" OnClientClick="return confirm('Bạn thực sự muốn xóa thông tin game thử tài kiến thức của người chơi không? ');"
                            Text="Xóa" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></div>
            </FooterTemplate>
        </asp:Repeater>
        <pager:Pager ID="pagerQuestion" runat="server" CompactModePageCount="10" NormalModePageCount="10"
            OnCommand="pager_Command" />
    </div>
</asp:Panel>
