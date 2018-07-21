<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="Parent.aspx.cs" Inherits="KungFuPanda.Instructor.Parent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            <!-- Basic Validation -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12">

                    <div class="card">
                        <div class="header">
                            <h2><strong>Add Parent</strong> </h2>

                        </div>
                        <div class="body">
                            <a id="aBack" href="ParentList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Parent data saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Student does not exist with this number.</strong>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtStudentNumber" CssClass="form-control" placeholder="Enter Student Number" ClientIDMode="Static" required></asp:TextBox>
                                </div>

                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtFName" CssClass="form-control" placeholder="Enter First Name" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtMName" CssClass="form-control" placeholder="Enter Middle Name" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtLName" CssClass="form-control" placeholder="Enter Last Name" ClientIDMode="Static" required></asp:TextBox>

                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter Email" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control" placeholder="Enter Mobile Number" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsStudent" ClientIDMode="Static" Checked="false" Text="Is Student"></asp:CheckBox>


                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsApprove" ClientIDMode="Static" Checked="true" Text="Is Approved"></asp:CheckBox>
                                </div>
                                <div class="form-group">
                                    <asp:RadioButton runat="server" ID="rbtnMale" GroupName="gender" Checked="true" CssClass="radio inlineblock m-r-20" Text="Male" />
                                    <asp:RadioButton runat="server" ID="rbtnFemale" Checked="false" GroupName="gender" CssClass="radio inlineblock" Text="Female" />

                                </div>
                                <asp:Button runat="server" ID="btnAddParent" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Submit" OnClick="btnAddParent_Click" />
                                 <asp:LinkButton runat="server" ID="btnReset" CssClass="btn btn-raised btn-danger btn-round waves-effect" Text="Reset" OnClick="btnReset_Click" />
                                
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
        </div>
    </section>
</asp:Content>
