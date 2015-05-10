<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Register.Master" AutoEventWireup="true"
    CodeBehind="News.aspx.cs" Inherits="TNGames.FrontEnd.News" %>

<%@ Register Src="../Controls/Admin/NewsList.ascx" TagName="NewsList" TagPrefix="uc1" %>
<%@ Register Src="../Controls/FrontEnd/Skin-News.ascx" TagName="Skin" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Tin tức - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
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
                    <uc2:Skin ID="Skin1" DisplayItem="10" IsNewsList="true" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
</asp:Content>
