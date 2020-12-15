<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Masters/MasterPageMenu.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Pages_Masters_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style3
    {
    }
    .style4
    {
        width: 142px;
    }
    .style5
    {
        width: 284px;
    }
</style>
    <link href="Cook1.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
 
    <table class="style1" 
    style="border-top-style: solid; border-bottom-style: solid">
    <tr>
        <td class="style5">
            Upload the Products.XML Table?</td>
        <td class="style4" width="100px">
            <asp:CheckBox ID="chkGo" runat="server" />
        </td>
        <td>
            <asp:Label ID="lblSuccess" runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style5">
            <asp:Button ID="btnSub" runat="server" onclick="btnSub_Click" Text="Submit" CssClass="btn"/>
        </td>
        <td class="style4">
            <asp:Label ID="lblErr" runat="server" 
                style="color: #FF0000; font-size: medium; font-family: Arial, Helvetica, sans-serif" 
                Visible="False"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3" colspan="3">
            <textarea id="ta"  runat="server" cols="60" name="S1" rows="40"></textarea></td>
    </tr>
</table>
 
</asp:Content>

