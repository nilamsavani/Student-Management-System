<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="KungFuPanda.Instructor.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <script src="assets/js/Jquery_1.12.js"></script>
    <script src="assets/js/jquery-ui.min.js"></script>
    <script>
        var $j = jQuery.noConflict();
        $j(document).ready(function () {

            $j("#txtDOP").datepicker({

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
                            <h2><strong>Add Fees</strong> </h2>

                        </div>
                        <div class="body">
                            <a id="aBack" href="PaymentList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Fee saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                     <strong>Student with this number already exist.</strong>
                                </div>
                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtStudentNumber" cssclass="form-control" placeholder="Enter Student Number" clientidmode="Static" required></asp:textbox>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-12 noPadding">
                                        <asp:dropdownlist runat="server" id="ddlPaymentType" cssclass=" show-tick ddlSet">
                                        </asp:dropdownlist>
                                    </div>
                                </div>


                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtAmount" cssclass="form-control" placeholder="Enter Amount" clientidmode="Static" required></asp:textbox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtDOP" cssclass="form-control" placeholder="Enter Date Of Payment" clientidmode="Static" required></asp:textbox>
                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsApprove" ClientIDMode="Static" Checked="true" Text="Is Approved"></asp:CheckBox>
                                </div>
                                <asp:button runat="server" id="btnAddPayment" cssclass="btn btn-raised btn-primary btn-round waves-effect" text="Submit" onclick="btnAddPayment_Click" />
                                <asp:linkbutton runat="server" id="btnReset" cssclass="btn btn-raised btn-danger btn-round waves-effect" text="Reset" onclick="btnReset_Click" />

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
