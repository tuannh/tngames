<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-GeneralRanking.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_GeneralRanking" %>
<div class="generalList">
    <asp:Label ID="lblMsg" runat="server" />
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table class="generalRanking" width="100%" border="0" cellspacing="6" cellpadding="0">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td width="48">
                    <img alt="icon" src="/images/web/QC/game_icon1.png" width="48" height="48" />
                </td>
                <td>
                    <span class="contentboldyellow">
                        <%# Container.ItemIndex + 1%>:
                        <%# Eval("DisplayName")%></span><br />
                    <span class="contentbold">Điểm:
                        <%# Eval("TotalPoint")%></span><br />
                    <span class="contentbold">Hạng:
                        <%# Eval("Rank") %></span>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td width="48">
                    <img alt="icon" src="/images/web/QC/game_icon2.png" width="48" height="48" />
                </td>
                <td>
                    <span class="contentboldyellow">
                        <%# Container.ItemIndex + 1%>:
                        <%# Eval("DisplayName")%></span><br />
                    <span class="contentbold">Điểm:
                        <%# Eval("TotalPoint")%></span><br />
                    <span class="contentbold">Hạng:
                        <%# Eval("Rank") %></span>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
