<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="KungFuPanda.Instructor.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/style.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            <!-- Basic Validation -->

            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12">

                    <div class="card">
                        <div class="header">
                            <h2><strong>Add Class</strong> </h2>

                        </div>
                        <div class="body">
                            <a id="aBack" href="ClassList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Class saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Class with this level, day and time already exist.</strong>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-12 noPadding">
                                        <asp:dropdownlist runat="server" id="ddlClassType" cssclass="ddlSet show-tick">
                                        </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtDay" cssclass="form-control" placeholder="Enter Day" clientidmode="Static" required></asp:textbox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:textbox runat="server" id="txtTime" cssclass="form-control" placeholder="Enter Time" clientidmode="Static" required></asp:textbox>
                                </div>


                                <div class="checkbox">
                                    <asp:checkbox runat="server" id="chkIsApprove" clientidmode="Static" checked="true" text="Is Approved"></asp:checkbox>
                                </div>
                                <asp:button runat="server" id="btnAddClass" cssclass="btn btn-raised btn-primary btn-round waves-effect" text="Submit" onclick="btnAddClass_Click" />
                                <asp:linkbutton runat="server" id="btnReset" cssclass="btn btn-raised btn-danger btn-round waves-effect" text="Reset" onclick="btnReset_Click" />

                            </div>
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
    </section>
</asp:Content>
