<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TeamBuilder._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .jumbotron .container-team {
            max-width: 50%;
            text-align: left;
            margin-left: 10px;
            vertical-align: text-top;
        }
    </style>

    <div class="jumbotron">
        <asp:Label runat="server" CssClass="label label-warning" ID="lblError" Visible="false"></asp:Label>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <h2>Select Division Above</h2>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>




</asp:Content>
