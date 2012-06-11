<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-MyAchievement.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_MyAchievement" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/thanhtich.png" alt="" width="244" height="33" />
                    </td>
                    <td class="bgheaderright">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="content">
            <div class="mycontent">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr>
                        <td width="35%" align="right">
                            Xin chào:
                        </td>
                        <td>
                            <b>
                                <asp:Literal ID="litDisplayName" runat="server" /></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Điểm phân tích trận đấu:
                        </td>
                        <td>
                            <b>
                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().Point.ToString("N0") %>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Điểm dự đoán:
                        </td>
                        <td>
                            <b>
                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().PointPrediction.ToString("N0") %>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Điểm trò chơi kiến thức:
                        </td>
                        <td>
                            <b>
                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().PointQuestion.ToString("N0") %>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Tổng điểm:
                        </td>
                        <td>
                            <b>
                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().TotalPoint.ToString("N0") %>
                            </b>
                        </td>
                    </tr>
                     <tr>
                        <td align="right">
                            Hạng:
                        </td>
                        <td>
                            <b>
                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().Rank.ToString("N0") %>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            Các game đã tham gia:
                        </td>
                        <td>
                            <asp:Literal ID="litPlayGame" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlBetting" runat="server">
                    <h1 class="headerText">
                        Chi tiết Thử tài phân tích trận đấu
                    </h1>
                    <div class="containTable">
                        <asp:Repeater ID="rptBetting" runat="server" OnItemCommand="rptBetting_ItemCommand"
                            OnItemDataBound="rptBetting_ItemDataBound">
                            <HeaderTemplate>
                                <div class="containBG">
                                    <table cellspacing="1" cellpadding="5" border="0" width="100%">
                                        <tr class="headertable">
                                            <td width="90">
                                                Ngày chơi
                                            </td>
                                            <td height="30">
                                                Trận đấu
                                            </td>
                                            <td width="70">
                                                Tỷ số
                                            </td>
                                            <td width="160">
                                                Thông tin
                                            </td>
                                            <td width="100">
                                                Điểm thưởng
                                            </td>
                                        </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="Table">
                                    <td style="text-align: center;">
                                        <%# Convert.ToDateTime(Eval("BettingDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat)%>
                                    </td>
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
                    <h1 class="headerText">
                        Chi tiết Thử tài dự đoán
                    </h1>
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
                                            <td width="100">
                                              Thao tác
                                            </td>
                                        </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="Table">
                                    <td style="text-align: center;">
                                        <%# Convert.ToDateTime(Eval("PlayDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat) %>
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
                                        <asp:Literal ID="litPDetail" runat="server"/>                                       
                                        <asp:Panel ID="pnlPopupPDetail" runat="server"  class="invisible">
                                            <div id="p<%# Eval("Id") %>" class="pdetail">
                                                <h1 class="headerText">
                                                    Chi tiết thử tài dự đoán</h1>
                                                <asp:Repeater ID="rptPDetail" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="containBG">
                                                            <table cellspacing="1" cellpadding="5" border="0" width="100%">
                                                                <tr class="headertable">
                                                                    <td height="30">
                                                                        Câu hỏi
                                                                    </td>
                                                                    <td width="200">
                                                                        DS câu trả lời và đáp án
                                                                    </td>
                                                                    <td width="200">
                                                                        Đáp án bạn đã chọn
                                                                    </td>
                                                                    <td width="100">
                                                                        Điểm thưởng
                                                                    </td>
                                                                </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="Table">
                                                            <td>
                                                                <%# Eval("Prediction.PredictionName")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Repeater ID="rptPAnswer" runat="server">
                                                                    <HeaderTemplate>
                                                                        <ul>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <li <%# Eval("AnswerText").ToString().Length == 0 ? "style='display: none;'" : "" %>
                                                                            <%# Convert.ToBoolean(Eval("IsCorrectAnswer")) ? "class='rightAnswer'" : "" %>>
                                                                            <%# Eval("AnswerText") %>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </ul>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Repeater ID="rptPUserAnswer" runat="server">
                                                                    <HeaderTemplate>
                                                                        <ul>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <li <%# Eval("AnswerText").ToString().Length == 0 ? "style='display: none;'" : "" %> <asp:Literal ID="litAnswerCSS" runat="server" /> >
                                                                            <%# Eval("AnswerText") %>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </ul>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("Prediction.BonusPoint")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table></div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                         </asp:Panel>
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
                    <h1 class="headerText">
                        Chi tiết Thử tài kiến thức
                    </h1>
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
                                            <td width="100">
                                              Thao tác
                                            </td>
                                        </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="Table">
                                    <td style="text-align: center;">
                                        <%# Convert.ToDateTime(Eval("PlayDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat) %>
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
                                        <asp:Literal ID="litQDetail" runat="server"/>                                       
                                        <asp:Panel ID="pnlPopupQDetail" runat="server"  class="invisible">
                                            <div id="q<%# Eval("Id") %>" class="qdetail">
                                                <h1 class="headerText">
                                                    Chi tiết tử tài kiến thức</h1>
                                                <asp:Repeater ID="rptQDetail" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="containBG">
                                                            <table cellspacing="1" cellpadding="5" border="0" width="100%">
                                                                <tr class="headertable">
                                                                    <td height="30">
                                                                        Câu hỏi
                                                                    </td>
                                                                    <td width="200">
                                                                        DS câu trả lời và đáp án
                                                                    </td>
                                                                    <td width="200">
                                                                        Đáp án bạn đã chọn
                                                                    </td>
                                                                    <td width="100">
                                                                        Điểm thưởng
                                                                    </td>
                                                                </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="Table">
                                                            <td>
                                                                <%# Eval("Question.QuestionName")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Repeater ID="rptAnswer" runat="server">
                                                                    <HeaderTemplate>
                                                                        <ul>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <li <%# Eval("AnswerText").ToString().Length == 0 ? "style='display: none;'" : "" %>
                                                                            <%# Convert.ToBoolean(Eval("IsCorrectAnswer")) ? "class='rightAnswer'" : "" %>>
                                                                            <%# Eval("AnswerText") %>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </ul>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Repeater ID="rptUserAnswer" runat="server">
                                                                    <HeaderTemplate>
                                                                        <ul>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <li <%# Eval("AnswerText").ToString().Length == 0 ? "style='display: none;'" : "" %> <asp:Literal ID="litAnswerCSS" runat="server" /> >
                                                                            <%# Eval("AnswerText") %>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </ul>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("Question.BonusPoint")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table></div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                         </asp:Panel>
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
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript">
//<![CDATA[
    $('.fancybox').each(function () {
        $(this).fancybox({
        //  'hideOnContentClick': false
            onStart: hideFlash,
            onClosed: showFlash
        });
    })

    function hideFlash() {
        $('embed, object, iframe').css('visibility', 'hidden');
    }
    function showFlash() {
        $('embed, object, iframe').css({ 'visibility': 'visible' });
    }

    $(window).scroll(function () {
        $('#fancybox-overlay').height($(document).height());
    })

//]]>
</script>
