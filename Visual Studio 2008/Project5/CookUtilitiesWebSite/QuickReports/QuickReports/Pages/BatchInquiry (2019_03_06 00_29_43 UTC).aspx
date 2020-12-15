<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Masters/MasterPage.master" AutoEventWireup="true" CodeFile="BatchInquiry.aspx.cs" Inherits="Pages_BatchInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Cook1.css" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div   >
                  Customer Unid : <asp:TextBox ID="txtUnid" runat="server" Width="339px"></asp:TextBox>
                   
                   <br />
                         Batch Id:<asp:TextBox ID="txtBatch" runat="server"></asp:TextBox>
&nbsp;<asp:SqlDataSource ID="sqlServer2" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:Godaddy %>" SelectCommand="select distinct batchid from control.customerlog
where unid = @p1 order by batchid desc ">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="txtUnid" Name="p1" PropertyName="Text" />
                      </SelectParameters>
                  </asp:SqlDataSource>
                         <br />
                    
                </div>

    <table style="width: 800px"  >
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlServer" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" AllowPaging="True" AllowSorting="True" 
                    HorizontalAlign="Left" DataKeyNames="id" 
                    onrowdatabound="gvData_RowDataBound">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Id" 
                            SortExpression="id" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="unid" HeaderText="CustomerNumber" 
                            SortExpression="unid" Visible="False" />
                        <asp:BoundField DataField="LogMsg" HeaderText="Msg" 
                            SortExpression="LogMsg" />
                        <asp:BoundField DataField="status" HeaderText="Status" 
                            SortExpression="status" />
                        <asp:BoundField DataField="BatchId" HeaderText="Batch" 
                            SortExpression="BatchId" Visible="False" />
                        <asp:BoundField DataField="createdate" HeaderText="Time" 
                            SortExpression="createdate" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        &nbsp; No Data Available&nbsp;&nbsp;
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlServer" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Godaddy %>" 
                    SelectCommand="select * from Control.Customerlog
where unid = @p1 and batchid = @batchid">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="p1" QueryStringField="unid" />
                        <asp:QueryStringParameter Name="batchid" QueryStringField="batch" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td   style="width:60%">
                <asp:Label ID="lblerr" runat="server" CssClass="err" Visible="False"></asp:Label>
            </td>
            <td>
            </td>
            <td style="width:40%; ">
               </td>
        </tr>
    </table>
</asp:Content>

