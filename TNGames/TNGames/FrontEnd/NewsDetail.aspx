<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Register.Master" AutoEventWireup="true"
    CodeBehind="NewsDetail.aspx.cs" Inherits="TNGames.FrontEnd.NewsDetail" %>

<%@ Register Src="../Controls/FrontEnd/Skin-NewsDetail.ascx" TagName="Skin" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Tin tức - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc1:Skin ID="Skin1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
</asp:Content>
