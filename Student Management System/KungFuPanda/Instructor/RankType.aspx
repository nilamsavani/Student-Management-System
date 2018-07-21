<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="RankType.aspx.cs" Inherits="KungFuPanda.Instructor.RankType" %>
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
                            <h2><strong>Add Belt Type</strong> </h2>

                        </div>
                        <div class="body">
                             <a id="aBack" href="RankTypeList.aspx" class="btn btn-raised btn-simple btn-round waves-effect">Back</a>
                            <div class="form_validation">
                                <div class="alert alert-success" id="divMessage" runat="server" visible="false">
                                    <strong>Belt type saved successfully.</strong>
                                </div>
                                <div class="alert alert-danger" id="divErrorMsg" runat="server" visible="false">
                                    <strong>Belt type already exist.</strong>
                                </div>
                                
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtBeltColor" CssClass="form-control" placeholder="Enter Belt Color" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtRequirements" CssClass="form-control" placeholder="Enter Requirements" TextMode="MultiLine" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                           
                                <div class="checkbox">
                                <asp:checkbox runat="server" ID="chkIsDefault"  ClientIDMode="Static" Checked="false" Text="Is Default"></asp:checkbox>
                                    </div>
                            <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkIsApprove" ClientIDMode="Static" Checked="true" Text="Is Approved"></asp:CheckBox>
                                </div>
                            <asp:Button runat="server" ID="btnAddBeltType" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Submit" OnClick="btnAddBeltType_Click" />
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
