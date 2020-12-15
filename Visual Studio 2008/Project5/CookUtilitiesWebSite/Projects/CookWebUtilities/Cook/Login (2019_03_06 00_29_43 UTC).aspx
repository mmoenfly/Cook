<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Masters/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Masters_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <asp:Login ID="lgon" runat="server" onauthenticate="lgon_Authenticate">
</asp:Login>
    <asp:Label ID="Lblerr" runat="server" Visible="False"></asp:Label>
</asp:Content>

