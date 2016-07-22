<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TeamBuilder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="container">
            <h2>Players</h2>
            <p>Available players</p>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th></th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>DOB</th>
                        <th>Age</th>
                        <th>Age at Season</th>
                        <th>Gender</th>
                        <th>Notes</th>
                        <th>Seasons Played</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPlayer" runat="server" OnItemDataBound="rptPlayer_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                
                                <td><asp:HiddenField runat="server" ID="hdnID"  Value='<%# Eval("ID") %>' /> </td>
                                <td><%# Eval("FNAME") %></td>
                                <td><%# Eval("LNAME") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "DOB", "{0:MM/dd/yyyy}") %></td>
                                <td><%# Eval("AGE","{0:F2}") %></td>
                                <td><%# Eval("AGESTART","{0:F2}") %></td>
                                <td><%# (bool.Parse(Eval("GENDER").ToString()) == true) ? "M" : "F" %></td>
                                <td><%# Eval("NOTES")%></td>
                                <td><%# Eval("SEASONSPLAYED") %></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlteam" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlteam_SelectedIndexChanged">
                                        <asp:ListItem Text="Select a Team" Value="0"></asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>

    <%--<div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>--%>
</asp:Content>
