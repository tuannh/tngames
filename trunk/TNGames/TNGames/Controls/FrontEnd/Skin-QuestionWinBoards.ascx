<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-QuestionWinBoards.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_QuestionWinBoards" %>
<asp:Repeater ID="rptList" runat="server">
    <HeaderTemplate>
        <div class="gameRankList">
        <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td width="48">
                <img src="/images/web/QC/game_icon1.png" width="48" height="48" />
            </td>
            <td>
                <span class="contentboldyellow">
                    <%# Container.ItemIndex + 1 %>.
                    <%# Eval("ItemArray[1]") %></span><br />
                <span class="contentbold">Điểm:
                    <%# Eval("ItemArray[3]") %></span><br />
                <span class="contentbold">Hạng:
                    <%# Eval("ItemArray[4]") %></span>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr>
            <td width="48">
                <img src="/images/web/QC/game_icon2.png" width="48" height="48" />
            </td>
            <td>
                <span class="contentboldyellow">
                    <%# Container.ItemIndex + 1 %>.
                    <%# Eval("ItemArray[1]") %></span><br />
                <span class="contentbold">Điểm:
                    <%# Eval("ItemArray[3]") %></span><br />
                <span class="contentbold">Hạng:
                    <%# Eval("ItemArray[4]") %></span>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </table></div>
    </FooterTemplate>
</asp:Repeater>
<h3>
    Tra cứu theo điểm</h3>
Điểm:
<asp:TextBox ID="txtFrom" runat="server" MaxLength="5" Width="40px" />
<act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="Từ"
    TargetControlID="txtFrom" />
<act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
    TargetControlID="txtFrom" />
<asp:TextBox ID="txtTo" runat="server" MaxLength="5" Width="40px" />
<act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="Đến"
    TargetControlID="txtTo" />
<act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
    TargetControlID="txtTo" />
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFrom"
    ControlToCompare="txtTo" Type="Integer" />
<asp:Button ID="btnSearch" runat="server" CssClass="buttonS" Text="Tìm" CausesValidation="False"
    OnClick="btnSearch_Click" />
<br />
<asp:Literal ID="litResult" runat="server" />
<asp:Repeater ID="rptSearch" runat="server">
    <HeaderTemplate>
        <div class="searchRankList">
        <table>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td width="48">
                <img src="/images/web/QC/game_icon1.png" width="48" height="48" />
            </td>
            <td>
                <span class="contentboldyellow">
                    <%# Container.ItemIndex + 1 %>.
                    <%# Eval("ItemArray[1]") %></span><br />
                <span class="contentbold">Điểm:
                    <%# Eval("ItemArray[3]") %></span><br />
                <span class="contentbold">Hạng:
                    <%# Eval("ItemArray[4]") %></span>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr>
            <td width="48">
                <img src="/images/web/QC/game_icon2.png" width="48" height="48" />
            </td>
            <td>
                <span class="contentboldyellow">
                    <%# Container.ItemIndex + 1 %>.
                    <%# Eval("ItemArray[1]") %></span><br />
                <span class="contentbold">Điểm:
                    <%# Eval("ItemArray[3]") %></span><br />
                <span class="contentbold">Hạng:
                    <%# Eval("ItemArray[4]") %></span>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </table></div>
    </FooterTemplate>
</asp:Repeater>
<pager:Pager ID="pager" runat="server" PageSize="2" CompactModePageCount="5" NormalModePageCount="5"
    OnCommand="pager_Command" />
