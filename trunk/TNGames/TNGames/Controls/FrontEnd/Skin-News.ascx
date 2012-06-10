<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-News.ascx.cs" Inherits="TNGames.Controls.FrontEnd.Skin_News" %>
<asp:Literal ID="litMsg" runat="server" Visible="false" Text="Không tìm thấy dữ liệu tin tức." />
<asp:Panel ID="pnlNewsTop" runat="server">
    <asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
        <HeaderTemplate>
            <ul class="newslist">
        </HeaderTemplate>
        <ItemTemplate>
            <li><a href="/tin-tuc/<%# Eval("Id") %>/<%# Eval("NewsAlias") %>" title="<%# Eval("NewsTitle") %>">
                <%# Eval("NewsTitle") %></a> <span>
                    <%# Convert.ToDateTime( Eval("CreatedDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat)%>
                </span></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
<asp:Panel ID="pnlNewsList" runat="server">
    <asp:Repeater ID="rptNewsList" runat="server" OnItemDataBound="rptNews_ItemDataBound">
        <HeaderTemplate>
            <ul class="newslist">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div <%# string.IsNullOrEmpty(Convert.ToString(Eval("Photo"))) ? "" : "class='newscontent'" %>>
                    <a href="/tin-tuc/<%# Eval("Id") %>/<%# Eval("NewsAlias") %>" title='<%# Eval("NewsTitle")%>'>
                        <%# Eval("NewsTitle")%></a> <span>
                            <%# Convert.ToDateTime( Eval("CreatedDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat)%>
                        </span>
                    <div class="newsdesc">
                        <asp:Literal ID="litDesc" runat="server" />
                    </div>
                </div>
                <div <%# string.IsNullOrEmpty(Convert.ToString(Eval("Photo"))) ? "class='invisible'" : "class='divPhoto'" %>>
                    <a href="/tin-tuc/<%# Eval("Id") %>" title='<%# Eval("NewsTitle")%>'>
                        <asp:Image ID="imgPhoto" runat="server" CssClass="newsPhoto" />
                    </a>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
