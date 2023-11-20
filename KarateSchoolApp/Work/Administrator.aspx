<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="KarateSchoolApp.Work.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Administration</h1>
    <p>
        &nbsp;<br />
        Members:</p>
    <p>
        <asp:GridView ID="MemberGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
        Instructors:</p>
    <p>
        <asp:GridView ID="InstructorGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
    </p>
    <p>
        Add new user:</p>
    <p>
        Username:
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
&nbsp;</p>
    <p>
        Password:
        <asp:TextBox ID="txtUserPassword" runat="server"></asp:TextBox>
&nbsp;</p>
    <p>
        User type:
        <asp:DropDownList ID="DropDownListType" runat="server" AutoPostBack="True">
            <asp:ListItem>Member</asp:ListItem>
            <asp:ListItem>Instructor</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="btnAddUser" runat="server" Height="32px" OnClick="btnAddUser_Click" Text="Add User" Width="120px" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        Type: <asp:Label ID="lblType" runat="server"></asp:Label>
    </p>
    <p>
        ID:
        <asp:Label ID="lblID" runat="server"></asp:Label>
    </p>
    <p>
        First Name:
        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
&nbsp;</p>
    <p>
        Last Name:
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
&nbsp;</p>
    <p>
        Date Joined:
        <asp:TextBox ID="txtDateJoined" runat="server"></asp:TextBox>
        &nbsp;Format: M/D/YYYY 12:00:00 AM</p>
    <p>
        Phone:
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
&nbsp; Format: 000-000-0000</p>
    <p>
        Email:
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
&nbsp;Format: username@server.domain</p>
    <p>
        <asp:Button ID="btnAddInfo" runat="server" OnClick="btnAddInfo_Click" Text="Add Info" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
<p>
        &nbsp;</p>
<p>
        Delete User:</p>
<p>
        User ID:
        <asp:TextBox ID="txtDeleteId" runat="server"></asp:TextBox>
    </p>
<p>
        <asp:Button ID="btnDeleteID" runat="server" OnClick="btnDeleteID_Click" Text="Delete" />
    </p>
<p>
        &nbsp;</p>
<p>
        Assign Member:</p>
<p>
        Member ID:
        <asp:TextBox ID="txtAssign" runat="server"></asp:TextBox>
    </p>
<p>
        Section ID:
        <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
    </p>
<p>
        <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" />
    </p>
<p>
        &nbsp;</p>
<p>
        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <p>
    </p>
</asp:Content>

