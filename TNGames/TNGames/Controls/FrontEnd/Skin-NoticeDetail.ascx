<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-NoticeDetail.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_NoticeDetail" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/thongbao.png" alt="" />
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
                <asp:Label ID="lblMsg" runat="server" />
                <asp:Panel ID="pnlDetail" runat="server">
                    <h2>
                        <asp:Literal ID="litTitle" runat="server" />
                    </h2>
                    <div class="noticeContent">
                        <asp:Literal ID="litContent" runat="server" />
                    </div>
                    <br />
                    <a style="float: right;" href="javascript:void(0);" onclick="history.back()">Quay lại</a>
                </asp:Panel>
                <asp:Repeater ID="rptList" runat="server">
                    <HeaderTemplate>
                        <h4>
                            Các thông báo khác</h4>
                        <ul class="noticelist">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href="/thong-bao/<%# Eval("Id") %>/<%# Eval("ContentAlias")  %>">
                            <%# Eval("ContentTitle")%></a> </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </td>
    </tr>
</table>
