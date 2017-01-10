<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ICA12-ADO.aspx.cs" Inherits="ICA12_ADO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_Header" Runat="Server">
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <h2>ICA12 - ADO - Basic Queries</h2>
    <div class="row">
        <div class="col-sm-2">
            Pick a Supplier:
        </div>
        <div class="col-sm-6">
            <asp:DropDownList ID="_DDL_Comapnies" runat="server" OnSelectedIndexChanged="_DDL_Comapnies_Index"></asp:DropDownList>
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="_TB_Filter" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <asp:Button ID="_Bu_Filter" runat="server" Text="Filter" OnClick="_Bu_Filter_Click" />
        </div>
    </div>

    <asp:Table ID="Table1" runat="server" BorderStyle="Solid" CellSpacing="10000"></asp:Table>
</asp:Content>

