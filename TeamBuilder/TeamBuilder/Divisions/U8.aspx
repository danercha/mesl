<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="U8.aspx.cs" Inherits="TeamBuilder.Divisions.U8" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .jumbotron .container-team {
            max-width: 50%;
            text-align: left;
            margin-left: 10px;
            vertical-align: text-top;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            (function ($) {

                $('#filter').keyup(function () {

                    var rex = new RegExp($(this).val(), 'i');
                    $('.searchable tr').hide();
                    $('.searchable tr').filter(function () {
                        return rex.test($(this).text());
                    }).show();

                })

            }(jQuery));

        });
    </script>
    <div class="jumbotron">

        <h2>Division <span class="label label-primary"> U8</span></h2><br />
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#players">Available Players</a></li>
            <li><a data-toggle="tab" href="#allplayers">All Players</a></li>
            <li><a data-toggle="tab" href="#team">Teams</a></li>
            <li><a data-toggle="tab" href="#coach">Coaches</a></li>
        </ul>
        <asp:Label runat="server" CssClass="label label-warning" ID="lblError" Visible="false"></asp:Label>
        <div class="tab-content">
            <div id="players" class="tab-pane fade in active">
                <h3>Available Players
                    <asp:Label runat="server" ID="lblPlayerCount"></asp:Label></h3>

                <div class="container">
                    <table class="table table-hover table-responsive">
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

                                        <td>
                                            <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                        </td>
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
            <div id="allplayers" class="tab-pane fade">
                <h3>All Players
                    <asp:Label runat="server" ID="lblAllPlayerCount"></asp:Label></h3>
                <div class="input-group">
                    <span class="input-group-addon">Filter</span>

                    <input id="filter" type="text" class="form-control" placeholder="Type here...">
                </div>
                <div class="container">
                    <table class="table table-hover table-responsive">
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
                                <th>Team</th>
                            </tr>
                        </thead>
                        <tbody class="searchable">
                            <asp:Repeater ID="rptAllPlayer" runat="server">
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:HiddenField runat="server" ID="hdnID" Value='<%# Eval("ID") %>' />
                                        </td>
                                        <td><%# Eval("FNAME") %></td>
                                        <td><%# Eval("LNAME") %></td>
                                        <td><%# DataBinder.Eval(Container.DataItem, "DOB", "{0:MM/dd/yyyy}") %></td>
                                        <td><%# Eval("AGE","{0:F2}") %></td>
                                        <td><%# Eval("AGESTART","{0:F2}") %></td>
                                        <td><%# (bool.Parse(Eval("GENDER").ToString()) == true) ? "M" : "F" %></td>
                                        <td><%# Eval("NOTES")%></td>
                                        <td><%# Eval("SEASONSPLAYED") %></td>
                                        <td><%# Eval("TEAMID") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="team" class="tab-pane fade">
                <div class="table-responsive">
                    <table class="table">
                        <asp:Repeater runat="server" ID="rptTeams" OnItemCommand="rptTeams_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <div class="container-team">
                                            <div class="row">
                                                <h3>
                                                    <h3><u><%# Eval("NAME") %></u> <span style="font-size: small;"><%# Eval("COACH") %></span></h3>
                                                </h3>
                                                <span class="col-sm-3" style="background-color: yellow; margin-right: 5px;">
                                                    <h4>Age: <%# Eval("AGE","{0:F2}") %></h4>
                                                </span>
                                                <div class="progress" style="width: 80px;">
                                                    <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar"
                                                        aria-valuenow='40' aria-valuemin="0" aria-valuemax="100" style='width: <%# Eval("GENDER","{0:F0}") + "%" %>'>
                                                        <%# Eval("GENDER","{0:F0}") %>%
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER1") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER1").ToString()) %>' runat="server" ID="lbRemove" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_1" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER2") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER2").ToString()) %>' runat="server" ID="LinkButton1" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_2" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER3") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER3").ToString()) %>' runat="server" ID="LinkButton2" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_3" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER4") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER4").ToString()) %>' runat="server" ID="LinkButton3" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_4" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER5") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER5").ToString()) %>' runat="server" ID="LinkButton4" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_5" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER6") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER6").ToString()) %>' runat="server" ID="LinkButton5" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER7") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER7").ToString()) %>' runat="server" ID="LinkButton11" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER8") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER8").ToString()) %>' runat="server" ID="LinkButton12" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER9") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER9").ToString()) %>' runat="server" ID="LinkButton13" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER10") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER10").ToString()) %>' runat="server" ID="LinkButton14" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER11") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER11").ToString()) %>' runat="server" ID="LinkButton15" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                            <div class="row">
                                                <h5><%# Eval("PLAYER12") %>
                                                    <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER12").ToString()) %>' runat="server" ID="LinkButton16" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                            </div>
                                        </div>
                                    </td>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <td>
                                    <div class="container-team">
                                        <div class="row">
                                            <h3>
                                                <h3><u><%# Eval("NAME") %></u> <span style="font-size: small;"><%# Eval("COACH") %></span></h3>
                                            </h3>
                                            <span class="col-sm-3" style="background-color: yellow; margin-right: 5px;">
                                                <h4>Age: <%# Eval("AGE","{0:F2}") %></h4>
                                            </span>
                                            <div class="progress" style="width: 80px;">
                                                <div class="progress-bar progress-bar-success progress-bar-striped" role="progressbar"
                                                    aria-valuenow='40' aria-valuemin="0" aria-valuemax="100" style='width: <%# Eval("GENDER","{0:F0}") + "%" %>'>
                                                    <%# Eval("GENDER","{0:F0}") %>%
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER1") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER1").ToString()) %>' runat="server" ID="lbRemove" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_1" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER2") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER2").ToString()) %>' runat="server" ID="LinkButton6" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_2" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER3") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER3").ToString()) %>' runat="server" ID="LinkButton7" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_3" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER4") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER4").ToString()) %>' runat="server" ID="LinkButton8" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_4" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER5") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER5").ToString()) %>' runat="server" ID="LinkButton9" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_5" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                        <div class="row">
                                            <h5><%# Eval("PLAYER6") %>
                                                <asp:LinkButton Visible='<%# !String.IsNullOrEmpty(Eval("PLAYER6").ToString()) %>' runat="server" ID="LinkButton10" CommandName="Delete" CommandArgument='<%# Eval("NAME") + "_6" %>' OnClientClick="if (!confirm('Are you sure do you want to delelte it?')) return false;"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton></h5>
                                        </div>
                                    </div>
                                </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div id="coach" class="tab-pane fade">
                <h3>Coaches</h3>
                <asp:Repeater runat="server" ID="rptCoaches">
                    <ItemTemplate>
                        <h5><%# Eval("FNAME") %> <%# Eval("LNAME") %></h5>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
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
