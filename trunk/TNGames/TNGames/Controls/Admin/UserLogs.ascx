<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogs.ascx.cs" Inherits="TNGames.Controls.Admin.UserLogs" %>
<h1 class="header">
    Nhật ký người chơi</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <asp:Repeater ID="rptList" runat="server" ClientIDMode="Predictable" OnItemDataBound="rptList_ItemDataBound">
        <HeaderTemplate>
            <div class="containBG">
                <table cellspacing="1" cellpadding="5" border="0" width="100%">
                    <tr class="headertable">
                        <td height="30" width="60">
                            ID
                        </td>
                        <td width="150">
                            Thao tác
                        </td>
                        <td>
                            Nội dung
                        </td>
                        <td width="150">
                            Ngày ghi nhật ký
                        </td>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="Table">
                <td style="text-align: center;">
                    <%# Eval("Id")%>
                </td>
                <td>
                    <asp:Literal ID="litLogType" runat="server" />
                </td>
                <td>
                    <%# Eval("LogAction")%>
                </td>
                <td style="text-align: center;">
                    <%# ((DateTime)Eval("LogDate")).ToString(TNGames.Core.Helper.TNHelper.DateTimeFormat)%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></div>
        </FooterTemplate>
    </asp:Repeater>
    <pager:Pager ID="pager" runat="server" CompactModePageCount="10" NormalModePageCount="10"
        OnCommand="pager_Command" />
    <br />
    <a href="/admincp/user-list/">Danh sách người chơi</a> | <a href="/admincp/user-edit/<%= litUserId.Text %>">
        Thông tin người chơi</a>
    <asp:Literal ID="litUserId" runat="server" Visible="false" />
</div>
