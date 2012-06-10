<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserList.ascx.cs" Inherits="TNGames.Controls.Admin.UserList" %>
<h1 class="header">
    Danh sách người chơi</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
        <HeaderTemplate>
            <div class="containBG">
                <table cellspacing="1" cellpadding="5" border="0" width="100%">
                    <tr class="headertable">
                        <td height="30">
                            Họ tên
                        </td>
                        <td width="120">
                            Tên hiển thị
                        </td>
                        <td width="160">
                            Email
                        </td>
                        <td width="100">
                            Số điểm
                        </td>
                        <td width="60">
                            Hạng
                        </td>
                        <td width="100">
                            Trò chơi tham gia
                        </td>
                        <td width="80">
                            Trang thái
                        </td>
                        <td width="100">
                            Thao tác
                        </td>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="Table">
                <td>
                    <%# Eval("FullName")%>
                </td>
                <td>
                    <%# Eval("DisplayName")%>
                </td>
                <td>
                    <%# Eval("Email")%>
                </td>
                <td style="text-align: left;">
                    Phân tích: <%# Eval("Point")%><br />
                    Dự đoán: <%# Eval("PointPrediction")%><br />
                    Kiến thức: <%# Eval("PointQuestion")%><br />
                    Tổng điểm: <%# Eval("TotalPoint")%>
                </td>
                <td style="text-align: center;">
                    <%# Eval("Rank") %>
                </td>
                <td>
                    <asp:Literal ID="litGameBetting" runat="server" />
                    <asp:Literal ID="litGamePrediction" runat="server" />
                    <asp:Literal ID="litGameQuestion" runat="server" />
                </td>
                <td style="text-align: center;">
                    <%#  Convert.ToBoolean(Eval("Active")) ? "Đã kích hoạt" : "Chưa kích hoạt" %>
                </td>
                <td style="text-align: center;">
                    <a href="/admincp/user-edit/<%# Eval("Id") %>">Sửa</a> | <a href="/admincp/user-logs/<%# Eval("Id") %>">
                        Logs</a> |
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                        Text="Xóa" OnClientClick="return confirm('Bạn muốn xóa người chơi này không?')"></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></div>
        </FooterTemplate>
    </asp:Repeater>
    <pager:Pager ID="pager" runat="server" CompactModePageCount="10" NormalModePageCount="10"
        OnCommand="pager_Command" />
    <p>
        Từ khóa:
        <asp:TextBox ID="txtSearch" runat="server" MaxLength="100"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Tìm" OnClick="btnSearch_Click" />
    </p>
</div>
