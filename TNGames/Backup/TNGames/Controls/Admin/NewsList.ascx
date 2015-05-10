<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsList.ascx.cs" Inherits="TNGames.Controls.Admin.NewsList" %>
<h1 class="header">
    Danh sách tin tức</h1>
<asp:Label ID="lblMsg" runat="server" />
<div class="containTable">
    <asp:Repeater ID="rptNews" runat="server" OnItemCommand="rptNews_ItemCommand">
        <HeaderTemplate>
            <div class="containBG">
                <table cellspacing="1" cellpadding="5" border="0" width="100%">
                    <tr class="headertable">
                        <td height="30">
                            Tiêu đề
                        </td>
                        <td style="display: none;">
                            Loại tin tức
                        </td>
                        <td style="width: 100px;">
                            Ngày tạo
                        </td>
                        <td style="width: 100px;">
                            Thao tác
                        </td>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="Table">
                <td>
                    <%# Eval("NewsTitle")%>
                </td>
                <td style="display: none;">
                    <%# Eval("Category.CategoryName")%>
                </td>
               <td style="text-align: center;">
                    <%# Convert.ToDateTime( Eval("CreatedDate")).ToString(TNGames.Core.Helper.TNHelper.DateFormat)%>
                </td>
                <td style="text-align: center;">
                    <a href="/admincp/news-edit/<%# Eval("Id") %>">Sửa</a>&nbsp;|&nbsp;<asp:LinkButton
                        ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                        Text="Xóa" OnClientClick="return confirm('Bạn thức sự muốn xóa tin này không?')"></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></div>
        </FooterTemplate>
    </asp:Repeater>
    <pager:Pager ID="pager" runat="server" CompactModePageCount="10" NormalModePageCount="10"
        OnCommand="pager_Command" />
</div>
