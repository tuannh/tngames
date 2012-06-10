<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Register.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="TNGames.FrontEnd.User.Account" %>
<%@ Register src="../../Controls/FrontEnd/Profile.ascx" tagname="Profile" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Thông tin tài khoản - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc1:Profile ID="Profile1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
</asp:Content>
