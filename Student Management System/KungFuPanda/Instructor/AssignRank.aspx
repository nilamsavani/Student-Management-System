<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="AssignRank.aspx.cs" Inherits="KungFuPanda.AssignRank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <script src="assets/js/Jquery_1.12.js"></script>
    <script src="assets/js/jquery-ui.min.js"></script>
    <script>
        var $j = jQuery.noConflict();
        $j(document).ready(function () {
            $j("#txtRankDate").datepicker({
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
                            <h2><strong>Assign Belt To Student</strong> </h2>
                        </div>
                        <div class="body">
                             <a id="aBack" href="AssignRankList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Rankassigned to student successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Student does not exist with this number.</strong>
                                </div>
                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtStudentNumber" cssclass="form-control" placeholder="Enter Student Number" clientidmode="Static" required></asp:textbox>
                                </div>
                               
                                <div class="row clearfix">
                                    <div class="col-sm-12 noPadding">
                                        <asp:dropdownlist runat="server" id="ddlRankType" cssclass="show-tick ddlSet">
                                        </asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtRankDate" cssclass="form-control" placeholder="Enter Date Of Rank" clientidmode="Static" required></asp:textbox>
                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsApprove" ClientIDMode="Static" Checked="true" Text="Is Approved"></asp:CheckBox>
                                </div>
                                <asp:button runat="server" id="btnAssignRank" cssclass="btn btn-raised btn-primary btn-round waves-effect" text="Submit" onclick="btnAssignRank_Click" />
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
