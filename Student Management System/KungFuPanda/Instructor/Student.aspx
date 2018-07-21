<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="KungFuPanda.Instructor.Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <script src="assets/js/Jquery_1.12.js"></script>
    <script src="assets/js/jquery-ui.min.js"></script>
    <script>
        var $j = jQuery.noConflict();
        $j(document).ready(function () {

            $j("#txtDOB").datepicker({

            });
            $j("#txtDOJ").datepicker({

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

                            <h2><strong>Add Student</strong> </h2>

                        </div>
                        <div class="body">
                            <a id="aBack" href="StudentList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Student data saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Student with this number already exist.</strong>
                                </div>
                                <div class="col-md-6 float-left">
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtFName" cssclass="form-control" placeholder="Enter First Name" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtMName" cssclass="form-control" placeholder="Enter Middle Name" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtLName" cssclass="form-control" placeholder="Enter Last Name" clientidmode="Static" required></asp:textbox>

                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtEmail" cssclass="form-control" placeholder="Enter Email" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtMobileNumber" cssclass="form-control" placeholder="Enter Mobile Number" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtDOB" cssclass="form-control" placeholder="Enter DOB" clientidmode="Static" required></asp:textbox>
                                    </div>
                                   
                                    <div class="form-group">
                                        <asp:radiobutton runat="server" id="rbtnMale" groupname="gender" checked="true" cssclass="radio inlineblock m-r-20" text="Male" />
                                        <asp:radiobutton runat="server" id="rbtnFemale" checked="false" groupname="gender" cssclass="radio inlineblock" text="Female" />

                                    </div>
                                    <asp:button runat="server" id="btnAddStudent" cssclass="btn btn-raised btn-primary btn-round waves-effect" text="Submit" onclick="btnAddStudent_Click" />
                                    <asp:linkbutton runat="server" id="btnReset" cssclass="btn btn-raised btn-danger btn-round waves-effect" text="Reset" onclick="btnReset_Click" />

                                </div>

                                <div class="col-md-6 float-left">



                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtDOJ" cssclass="form-control" placeholder="Enter DOJ" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtStreet" cssclass="form-control" placeholder="Enter Address" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtCity" cssclass="form-control" placeholder="Enter City" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtState" cssclass="form-control" placeholder="Enter State" clientidmode="Static" required></asp:textbox>
                                    </div>
                                    <div class="form-group form-float">
                                        <asp:textbox runat="server" id="txtZIP" cssclass="form-control" placeholder="Enter Postal Code" clientidmode="Static" required></asp:textbox>
                                    </div>
                                     <div class="checkbox">
                                        <asp:checkbox runat="server" id="chkIsApprove" clientidmode="Static" checked="true" text="Is Approved"></asp:checkbox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- #END# Basic Validation -->
            <div>
                <asp:hiddenfield id="hdnPKId" clientidmode="Static"
                    value="0"
                    runat="server" />
            </div>
        </div>
    </section>
</asp:Content>
