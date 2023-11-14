<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="KarateSchoolApp.Work.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        Members:</p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="MemberFirstName" HeaderText="MemberFirstName" SortExpression="MemberFirstName" />
                <asp:BoundField DataField="MemberLastName" HeaderText="MemberLastName" SortExpression="MemberLastName" />
                <asp:BoundField DataField="MemberDateJoined" HeaderText="MemberDateJoined" SortExpression="MemberDateJoined" />
                <asp:BoundField DataField="MemberPhoneNumber" HeaderText="MemberPhoneNumber" SortExpression="MemberPhoneNumber" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KarateSchoolConnectionString3 %>" SelectCommand="SELECT [MemberFirstName], [MemberLastName], [MemberDateJoined], [MemberPhoneNumber] FROM [Member]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
