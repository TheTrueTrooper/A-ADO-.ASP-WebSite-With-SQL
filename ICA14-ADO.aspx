<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ICA14-ADO.aspx.cs" Inherits="ICA14_ADO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="_Header" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:AngeloSanches_NorthwindConnectionString %>" SelectCommand="SELECT [ProductID], [ProductName] FROM [Products]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AngeloSanches_NorthwindConnectionString %>" SelectCommand="SELECT [OrderID], d.[ProductID], [ProductName], d.[UnitPrice], [Quantity], [Discount] FROM [Order Details] as d inner join [Products] as p on d.[ProductID] = p.[ProductID] WHERE ([OrderID] = @OrderID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="_TB_Filter" Name="OrderID" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


    <div class="row">
        <div class="col-sm-2">
            Pick a Supplier:
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="_TB_Filter" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <asp:Button ID="_Bu_Filter" runat="server" Text="Get Order Details"/>
        </div>
    </div>
    <asp:GridView ID="_GW_" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="OrderID,ProductID" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
<asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName"></asp:BoundField>
            <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
            <asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <div>
    <asp:Button ID="_Bu_Delete" runat="server" Text="Delete" OnClick="_Bu_Delete_Click" Width="86px" />
    </div>
    <div>
    <asp:Label ID="_La_Status" runat="server" Text="Status: "></asp:Label>
    </div>
    <hr />
    <div>
        Enter OrderID<asp:TextBox ID="_TB_OrderID" runat="server"></asp:TextBox>
    </div>
    <div>
        Select Product<asp:DropDownList ID="_DDL_Pro" runat="server" DataSourceID="SqlDataSource2" DataTextField="ProductName" DataValueField="ProductID"></asp:DropDownList>
    </div>
    <div>
        Enter Quantity<asp:TextBox ID="_TB_Qty" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="_Bu_Insert" runat="server" Text="Insert Records" OnClick="_Bu_Insert_Click"/>
    </div>
    <div>
    <asp:Label ID="_La_Status2" runat="server" Text="Status: "></asp:Label>
    </div>
</asp:Content>

