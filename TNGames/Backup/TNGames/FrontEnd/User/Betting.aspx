<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/BettingGame.Master"
    AutoEventWireup="true" CodeBehind="Betting.aspx.cs" Inherits="TNGames.FrontEnd.User.Betting" %>

<%@ Register Src="../../Controls/FrontEnd/Skin-BettingGame.ascx" TagName="Skin" TagPrefix="uc1" %>
<%@ Register Src="../../Controls/FrontEnd/Skin-BettingWinBoards.ascx" TagName="Skin"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/FrontEnd/Skin-GeneralNotice.ascx" TagName="Skin"
    TagPrefix="uc3" %>
<%@ Register Src="../../Controls/FrontEnd/Skin-News.ascx" TagName="Skin" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Trò chơi thử phân tích trận đấu - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc2:Skin ID="Skin2" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderCenter" runat="server">
    <uc1:Skin ID="Skin1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolderBottom" runat="server">
    <div class="clip">
        <uc3:Skin ID="Skin3" ContentTypeId="2" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
    <div class="bnotices">
        <uc3:Skin ID="Skin4" ContentTypeId="1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentPlaceHolderFooter" runat="server">
    <table style="width: 100%; margin-top: -10px;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 246px;">
                <uc3:Skin ID="Skin6" ContentTypeId="11" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td style="width: 742px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="bgcacuocBleft">
                            &nbsp;
                        </td>
                        <td class="bgcacuocBcenter" valign="top">
                            <div style="height: 110px; overflow: hidden;">
                                <div class="newsicon">
                                    <a href="/tin-tuc" title="Xem tất cả">
                                        <img alt="tintuc" src="/images/web/tintuc.png" />
                                    </a>
                                </div>
                                <uc4:Skin ID="Skin5" DisplayItem="4" IsNewsList="false" runat="server" />
                            </div>
                        </td>
                        <td class="bgcacuocBright">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
