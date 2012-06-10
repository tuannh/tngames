<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/Register.Master" AutoEventWireup="true"
    CodeBehind="Active.aspx.cs" Inherits="TNGames.FrontEnd.Active" %>

<%@ Register Src="../Controls/FrontEnd/ActiveAccount.ascx" TagName="ActiveAccount"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Kích hoạt tài khoản - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc1:ActiveAccount ID="ActiveAccount1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
</asp:Content>
