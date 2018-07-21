<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="PaymentTransaction.aspx.cs" Inherits="KungFuPanda.Instructor.PaymentTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <!-- Custom Css -->
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <link rel="stylesheet" href="assets/css/main.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <script src="assets/js/Jquery_1.12.js"></script>

    <script>    
        var $j = jQuery.noConflict();
        $j(document).ready(function () {

            getstudentdata("tbodystudentlist");
        });
        function getstudentdata(strbodyid) {
            try {
                var param = {};
                $j.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(param),
                    url: "PaymentTransaction.aspx/getstudentdata",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                        if (response.d != "") {
                            var vjson = JSON.parse(response.d);
                            try {
                                var innerHTML = "";
                                $j("#" + strbodyid).html("");
                                for (var i = 0; i < vjson.length; i++) {
                                    
                                    innerHTML += "<tr>";
                                    innerHTML += "<td>" + vjson[i].STU_NUM + "</td>";
                                    innerHTML += "<td><span style='color:blue'>" + vjson[i].TotalAmount + "</span></td>";
                                    innerHTML += "<td><span style='color:green'>" + vjson[i].TotalPaidAmount + "</span></td>";
                                    innerHTML += "<td><span style='color:red'>" + vjson[i].TotalAmountToBePaid + "</span></td>";
                                    innerHTML += "</tr>";
                                }
                                $j("#" + strbodyid).html(innerHTML);
                            }
                            catch (ex) {
                                console.log(" error messge from getstudentdata function : " + ex.message + " at " + ex.stack);
                            }
                        }
                        else {
                        }
                    },
                    error: function (error) {
                        console.dir("error : " + error);
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    complete: function (response) {
                    }
                });
            }
            catch (ex) {
                console.log("error:" + ex.message)
            }
            return false
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content">
        <div class="container-fluid">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="header">
                            <h2><strong>Students Progress Report</strong> </h2>

                        </div>
                        <div class="body">
                            <div class="table-responsive">
                                <table class="table table-bordered table-striped table-hover js-basic-example dataTable">
                                    <thead>
                                        <tr>
                                            <th>Student ID</th>
                                            <th>Total Amount</th>
                                            <th>Total Paid Amount</th>
                                            <th>Total Amount To Pay</th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th>Student ID</th>
                                            <th>Total Amount</th>
                                            <th>Total Paid Amount</th>
                                            <th>Total Amount To Pay</th>
                                        </tr>
                                    </tfoot>
                                    <tbody id="tbodystudentlist">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>

    <!-- Jquery DataTable Plugin Js -->
    <script src="assets/bundles/datatablescripts.bundle.js"></script>
    <script src="assets/plugins/jquery-datatable/buttons/dataTables.buttons.min.js"></script>
    <script src="assets/plugins/jquery-datatable/buttons/buttons.bootstrap4.min.js"></script>
    <script src="assets/plugins/jquery-datatable/buttons/buttons.colVis.min.js"></script>
    <script src="assets/plugins/jquery-datatable/buttons/buttons.html5.min.js"></script>
    <script src="assets/plugins/jquery-datatable/buttons/buttons.print.min.js"></script>

    <script src="assets/js/pages/tables/jquery-datatable.js"></script>

</asp:Content>
