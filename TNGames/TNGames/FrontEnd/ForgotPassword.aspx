<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Register.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="TNGames.FrontEnd.ForgotPassword" %>
<%@ Register src="../Controls/FrontEnd/ForgotPassword.ascx" tagname="ForgotPassword" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quên mật khẩu - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc1:ForgotPassword ID="ForgotPassword1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
</asp:Content>
