<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Home.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TNGames.Default" %>

<%@ Register Src="Controls/FrontEnd/Skin-GeneralRanking.ascx" TagName="Skin" TagPrefix="uc1" %>
<%@ Register Src="Controls/FrontEnd/Skin-GameList.ascx" TagName="Skin" TagPrefix="uc2" %>
<%@ Register Src="Controls/FrontEnd/Skin-GeneralNotice.ascx" TagName="Skin" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Trang trò chơi - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="bgheaderClefttop">
                            &nbsp;
                        </td>
                        <td class="bgheaderCcentertop">
                            <img src="/images/web/sephang_tongthe.png" width="210" height="34" />
                        </td>
                        <td class="bgheaderCrighttop">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bgheaderCleftcenter">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" bgcolor="#4F97BE">
                            <uc1:Skin ID="Skin1" runat="server" />
                        </td>
                        <td class="bgheaderCrightcenter">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bgheaderCleftcenter">
                            &nbsp;
                        </td>
                        <td bgcolor="#4F97BE">
                        </td>
                        <td class="bgheaderCrightcenter">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="bgheaderAlefttop">
                            &nbsp;
                        </td>
                        <td class="bgheaderAcentertop">
                            <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                <tr>
                                    <td width="32">
                                        <img src="/images/web/user_icon.png" width="33" height="27" />
                                    </td>
                                    <td class="accountred">
                                        <asp:LoginView ID="LoginView3" runat="server">
                                            <AnonymousTemplate>
                                                Xin chào: Guest
                                            </AnonymousTemplate>
                                            <LoggedInTemplate>
                                                Xin chào:
                                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().DisplayName %><br />
                                                Điểm:
                                                <%= TNGames.Core.Helper.Utils.GetCurrentUser().Point.ToString("N0") %>
                                                | <a href="/dang-nhap?logout=true">Thoát</a>
                                                <% if (TNGames.Core.Helper.Utils.GetCurrentUser().IsAdmin)
                                                   { %>
                                                <a href="/admincp" title="Admin control panel">AdminCP</a>
                                                <%} %>
                                            </LoggedInTemplate>
                                        </asp:LoginView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="contentdisable">
                                        <asp:LoginView ID="LoginView2" runat="server">
                                            <AnonymousTemplate>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="/dang-nhap">Đăng nhập</a>
                                            </AnonymousTemplate>
                                            <LoggedInTemplate>
                                                <img src="/images/web/profile_icon.png" width="16" height="16" />
                                                <a href="/tai-khoan">Hồ sơ cá nhân</a>
                                            </LoggedInTemplate>
                                        </asp:LoginView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="contentdisable">
                                        <asp:LoginView ID="LoginView1" runat="server">
                                            <AnonymousTemplate>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="/dang-ky">Đăng ký</a>
                                            </AnonymousTemplate>
                                            <LoggedInTemplate>
                                                <img src="/images/web/core_icon.png" width="16" height="16" />
                                                <a href="/thanh-tich">Thông tin thành tích</a>
                                            </LoggedInTemplate>
                                        </asp:LoginView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="bgheaderArighttop">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bgheaderAleftbottom">
                            &nbsp;
                        </td>
                        <td class="bgheaderAcenterbottom">
                            &nbsp;
                        </td>
                        <td class="bgheaderArightbottom">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderCenter" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="bgheaderBlefttop">
                &nbsp;
            </td>
            <td valign="bottom" class="bgheaderBcentertop">
                <img src="/images/web/ds_game.png" width="267" height="39" />
            </td>
            <td class="bgheaderBrighttop">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="bgheaderBleftcenter">
                &nbsp;
            </td>
            <td valign="middle" bgcolor="#FFFFFF">
                <uc2:Skin ID="Skin2" runat="server" />
            </td>
            <td class="bgheaderBrightcenter">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="bgheaderBleftbottom">
                &nbsp;
            </td>
            <td valign="middle" class="bgheaderBcenterbottom">
                &nbsp;
            </td>
            <td class="bgheaderBrightbottom">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="bgheaderClefttop">
                            &nbsp;
                        </td>
                        <td class="bgheaderCcentertop">
                            <img src="/images/web/thongbaobantochuc.png" width="239" height="32" />
                        </td>
                        <td class="bgheaderCrighttop">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bgheaderCleftcenter">
                            &nbsp;
                        </td>
                        <td bgcolor="#4F97BE">
                            <div class="hnotice">
                                <uc3:Skin ID="Skin3" ContentTypeId="7" runat="server" />
                            </div>
                        </td>
                        <td class="bgheaderCrightcenter">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bgheaderCleftcenter">
                            &nbsp;
                        </td>
                        <td bgcolor="#4F97BE">
                            &nbsp;
                        </td>
                        <td class="bgheaderCrightcenter">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc3:Skin ID="Skin4" ContentTypeId="8" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
