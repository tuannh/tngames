<%@ Page Title="" Language="C#" MasterPageFile="~/MaterPages/QuestionGame.Master"
    AutoEventWireup="true" CodeBehind="Prediction.aspx.cs" Inherits="TNGames.FrontEnd.User.Prediction" %>

<%@ Register Src="../../Controls/FrontEnd/Skin-PredictionWinBoards.ascx" TagName="Skin"
    TagPrefix="uc1" %>
<%@ Register Src="../../Controls/FrontEnd/Skin-PredictionGame.ascx" TagName="Skin"
    TagPrefix="uc3" %>
<%@ Register Src="../../Controls/FrontEnd/Skin-GeneralNotice.ascx" TagName="Skin"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Trò chơi thử tài dự đoán - .: Thanh Niên Online :.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolderLeft" runat="server">
    <uc1:Skin ID="Skin1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolderCenter" runat="server">
    <uc3:Skin ID="Skin3" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolderRight" runat="server">
    <uc2:Skin ID="Skin4" ContentTypeId="3" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentPlaceHolderBottom" runat="server">
    <uc2:Skin ID="Skin2" ContentTypeId="4" runat="server" />
</asp:Content>
