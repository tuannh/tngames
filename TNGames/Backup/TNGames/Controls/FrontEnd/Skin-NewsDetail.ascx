<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-NewsDetail.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_NewsDetail" %>
<table width="100%" border="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bgheaderleft">
                        &nbsp;
                    </td>
                    <td valign="middle" class="header">
                        <img src="/images/web/tintuc.png" alt="" />
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
                <asp:Panel ID="pnlDetail" runat="server">
                    <h2>
                        <asp:Literal ID="litTitle" runat="server" />
                    </h2>
                    <span class="postDate" style="font-style: italic;">
                        <asp:Literal ID="litPostDate" runat="server" /></span>
                    <div class="summary">
                        <asp:Image ID="imgPhoto" Style="max-width: 250px; float: left; padding-right: 10px;"
                            runat="server" />
                        <asp:Literal ID="litSummary" runat="server" /></div>
                    <div class="newContent">
                        <asp:Literal ID="litNewContent" runat="server" />
                    </div>
                    <br />
                    <a style="float: right;" href="javascript:void(0);" onclick="history.back()">Quay lại</a>
                </asp:Panel>
                <asp:Repeater ID="rptNews" runat="server">
                    <HeaderTemplate>
                        <h4>
                            Các tin khác</h4>
                        <ul class="newslist">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><a href="/tin-tuc/<%# Eval("Id") %>/<%# Eval("NewsAlias") %>" title="<%# Eval("NewsTitle") %>">
                            <%# Eval("NewsTitle")%></a> <span>
                                <%# Convert.ToDateTime( Eval("CreatedDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat)%>
                            </span></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </td>
    </tr>
</table>
