<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Skin-GeneralNotice.ascx.cs"
    Inherits="TNGames.Controls.FrontEnd.Skin_GeneralNotice" %>
<asp:Label ID="lblMsg" runat="server" />
<asp:Repeater ID="rptList" runat="server" 
    onitemdatabound="rptList_ItemDataBound">
    <HeaderTemplate>
        <ul class="notices">
    </HeaderTemplate>
    <ItemTemplate>
        <li <%# Convert.ToBoolean(Eval("ContentType.IsBanner")) ? "" : "class='notice'" %>>
            <asp:Literal ID="litContent" runat="server" />
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
