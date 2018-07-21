<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="ClassType.aspx.cs" Inherits="KungFuPanda.Instructor.ClassType" %>
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
                            <h2><strong>Add Class Level</strong> </h2>

                        </div>
                        <div class="body">
                            <a id="aBack" href="ClassTypeList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Class Level saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Class Level already exist.</strong>
                                </div>
                                <div class="form-group form-float">
                                        <asp:TextBox runat="server" ID="txtClassLevel" CssClass="form-control" placeholder="Enter Class Level" ClientIDMode="Static" required></asp:TextBox>
                                    </div>
                               
                                   <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsApprove" ClientIDMode="Static" Checked="true" Text="Is Approved"></asp:CheckBox>
                                </div>
                                    <asp:Button runat="server" ID="btnAddClassType" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Submit" OnClick="btnAddClassType_Click" />
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
