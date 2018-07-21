<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KungFuPanda.Instructor.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        aside {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="authentication">
        <div class="container">
            <div class="col-md-12 content-center">
                <div class="row">

                    <div class="col-lg-12 col-md-12 ">
                        <div class="card-plain">
                            <div class="header">
                                <h5>Log in</h5>
                            </div>
                            <div class="body">

                               
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Invalid username or password</strong>
                                </div>
                                <div class="form form_validation">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="User Name" required></asp:TextBox>
                                        <%--<input type="text" class="form-control" placeholder="User Name">--%>
                                        <span class="input-group-addon"><i class="zmdi zmdi-account-circle"></i></span>
                                    </div>
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="Password" required></asp:TextBox>
                                        <%--<input type="password" placeholder="Password" class="form-control" />--%>
                                        <span class="input-group-addon"><i class="zmdi zmdi-lock"></i></span>
                                    </div>
                                </div>
                                <div class="footer">
                                    <asp:LinkButton runat="server" ID="lnkSignIn" CssClass="btn btn-primary btn-round btn-block" OnClick="lnkSignIn_Click">SIGN IN</asp:LinkButton>
                                    <%--<a href="Dashboard.aspx" class="btn btn-primary btn-round btn-block">SIGN IN</a>--%>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
