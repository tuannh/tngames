<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="TNGames.Controls.FrontEnd.UserInfo" %>
<asp:LoginView runat="server">
    <LoggedInTemplate>
        <div class="accountred accountredTop">
            Xin chào:
            <%= TNGames.Core.Helper.Utils.GetCurrentUser().DisplayName %>&nbsp;|&nbsp;<a href="/dang-nhap?logout=true"
                title="Đăng xuất">Thoát</a>&nbsp; |&nbsp;<a href="/tai-khoan" title="Thông tin tài khoản"><img
                    src="/images/web/profile_icon.png" width="16" height="16" /></a>&nbsp;|&nbsp;<a href="/thanh-tich"
                        title="Thông tin các game đã chơi và số điểm"><img src="/images/web/core_icon.png"
                            width="16" height="16" /></a>
            <div style="text-align: left;">
                Tổng điểm: <span id="totalPoint">
                    <%= TNGames.Core.Helper.Utils.GetCurrentUser().TotalPoint.ToString("N0") %></span>
                <span id="cpoint" class="invisible">
                    <%= TNGames.Core.Helper.Utils.GetCurrentUser().Point.ToString("N0") %></span>
                <% if (TNGames.Core.Helper.Utils.GetCurrentUser().IsAdmin)
                   { %>
                <br />
                <a href="/admincp" title="Admin control panel">AdminCP</a>
                <%} %>
            </div>
        </div>
    </LoggedInTemplate>
</asp:LoginView>
