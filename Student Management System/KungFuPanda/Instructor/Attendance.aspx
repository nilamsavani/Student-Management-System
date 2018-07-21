<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="KungFuPanda.Instructor.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <script src="assets/js/Jquery_1.12.js"></script>
    <script src="assets/js/jquery-ui.min.js"></script>
    <script>
        var $j = jQuery.noConflict();
        $j(document).ready(function () {

            $j("#txtDate").datepicker({

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="content">
        <div class="container-fluid">
            <!-- Basic Validation -->

            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12">

                    <div class="card">
                        <div class="header">
                            <h2><strong>Add Attendance</strong> </h2>

                        </div>
                        <div class="body">
                             <a id="aBack" href="AttendanceList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Attendance saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Student does not exist with this number.</strong>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtStudentNumber" CssClass="form-control" placeholder="Enter Student Number" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <asp:UpdatePanel runat="server" ID="up">
                                    <ContentTemplate>
                                        <div class="row clearfix">
                                            <div class="col-sm-12 noPaddig" style="padding:0px;">
                                                <asp:DropDownList runat="server" ID="ddlClassLevel" CssClass="ddlSet show-tick" AutoPostBack="true" OnSelectedIndexChanged="ddlClassLevel_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-12 noPadding">
                                                <asp:DropDownList runat="server" ID="ddlClassType" CssClass="ddlSet show-tick">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" placeholder="Enter Date Of Class" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsPresent" ClientIDMode="Static" Checked="true" Text="Is Present"></asp:CheckBox>
                                </div>
                                <asp:Button runat="server" ID="btnAddAttendance" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Submit" OnClick="btnAddAttendance_Click" />
                                 <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-raised btn-danger btn-round waves-effect" Text="Reset" OnClick="btnReset_Click" />
                                
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <!-- #END# Basic Validation -->
         <div>
                <asp:HiddenField ID="hdnPKId" ClientIDMode="Static"
                    Value="0"
                    runat="server" />
            </div>
    </section>


</asp:Content>
