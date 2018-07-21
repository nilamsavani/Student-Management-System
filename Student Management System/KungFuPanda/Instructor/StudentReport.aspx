<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="StudentReport.aspx.cs" Inherits="KungFuPanda.Instructor.StudentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <!-- Custom Css -->
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <link rel="stylesheet" href="assets/css/main.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <script src="assets/js/Jquery_1.12.js"></script>
    <%--<script src="assets/js/jquery-ui.min.js"></script>--%>

    <script>    
        var $j = jQuery.noConflict();
        $j(document).ready(function () {
            //$j("#txtFromDate").datepicker({

            //});
            //$j("#txtToDate").datepicker({

            //});
            //$j("#btnSearch").click(function () {
            //    getstudentdata("tbodystudentlist", $("#txtFromDate").val(), $("#txtToDate").val());
            //    return false;
            //});
            getstudentdata("tbodystudentlist");

        });
        //function getstudentdata(strbodyid, strFromDate, strToDate) {
        function getstudentdata(strbodyid) {
            // alert(strFromDate+"--"+strToDate);
            try {
                //var param = {strFromDate:strFromDate,strToDate:strToDate};
                var param = {};
                $j.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(param),
                    url: "StudentReport.aspx/getstudentdata",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                        //  console.log(response.d);
                        // alert(response.d);
                        if (response.d != "") {
                            var vjson = JSON.parse(response.d);
                            try {
                                var innerHTML = "";
                                $j("#" + strbodyid).html("");
                                for (var i = 0; i < vjson.length; i++) {
                                    var dob = new Date(vjson[i].STU_DOB);
                                    var fdob = (dob.getMonth() + 1) + '/' + dob.getDate() + '/' + dob.getFullYear();
                                    var doj = new Date(vjson[i].STU_DOJ);
                                    var fdoj = (doj.getMonth() + 1) + '/' + doj.getDate() + '/' + doj.getFullYear();
                                    innerHTML += "<tr>";
                                    innerHTML += "<td>" + vjson[i].STU_NUM + "</td>";
                                    if (vjson[i].STU_STATUS) {
                                        innerHTML += "<td><span class=\"btn btn-round btn-simple btn-sm btn-success btn-filter\">Approved</span></td>";
                                    }
                                    else {
                                        innerHTML += "<td><span class=\"btn btn-round btn-simple btn-sm btn-danger btn-filter\">Blocked</span></td>";
                                    }
                                    innerHTML += "<td>" + vjson[i].STU_FNAME + "</td>";
                                    innerHTML += "<td>" + vjson[i].STU_MNAME + "</td>";
                                    innerHTML += "<td>" + vjson[i].STU_LNAME + "</td>";
                                    //alert(getStudentParentData(vjson[i].STU_ID));
                                    innerHTML += getStudentParentData(vjson[i].STU_ID);
                                    // innerHTML += "<td>-</td>";
                                    innerHTML += "<td>" + fdob + "</td>";
                                    innerHTML += "<td>" + fdoj + "</td>";
                                    innerHTML += "<td>" + vjson[i].STU_MNUM + "</td>";
                                    innerHTML += "<td>" + vjson[i].STU_EMAIL + "</td>";
                                    innerHTML += "<td>" + vjson[i].STU_STREET + ", " + vjson[i].STU_CITY + ", " + vjson[i].STU_STATE + ", " + vjson[i].STU_ZIP + "</td>";
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
        function getStudentParentData(strStudentID) {
            var innerHTML = "";
            try {
                var param = { strStudentID: strStudentID };
                $j.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(param),
                    url: "StudentReport.aspx/getStudentParentData",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                        //alert(response.d);
                        if (response.d != "") {
                            var vjson = JSON.parse(response.d);
                            try {
                                if (vjson.length == 0) {
                                    innerHTML += "<td>-</td>";
                                }
                                else {
                                    var cnt = 0;
                                    for (var i = 0; i < vjson.length; i++) {
                                        if (vjson.length > 1) {
                                            //alert(2);
                                            if (cnt == 0) {
                                                innerHTML += "<td>";
                                            }
                                            if (vjson[i].PARENT_NAME.toString().trim() != "") {
                                                innerHTML += "<div><b>Name:</b> " + vjson[i].PARENT_NAME +"</div>";
                                                if (vjson[i].PARENT_GENDER) {
                                                    innerHTML += "<div><b>Relation:</b> Father</div>";
                                                }
                                                else {
                                                    innerHTML += "<div><b>Relation:</b> Mother</div>";
                                                }
                                                innerHTML += "<div><b>Email:</b> " + vjson[i].PARENT_EMAIL +"</div>";
                                                innerHTML += "<div><b>Mobile No.:</b> " + vjson[i].PARENT_MNUM +"</div>";
                                                if (vjson[i].PARENT_IS_STUDENT) {
                                                    innerHTML += "<div><b>Is Student:</b> Yes</div>";
                                                }
                                                else {
                                                    innerHTML += "<div><b>Is Student:</b> No</div>";
                                                }
                                            }
                                            if (cnt != 0) {
                                                innerHTML += "</td>";

                                            }
                                            else {
                                                innerHTML += "<hr>";
                                            }
                                            cnt++;
                                        }
                                        else if (vjson.length == 1) {
                                            //alert(1);
                                            if (vjson[i].PARENT_NAME.toString().trim() != "") {
                                                innerHTML += "<td>"
                                                innerHTML += "<div><b>Name:</b> " + vjson[i].PARENT_NAME+"</div>";
                                                if (vjson[i].PARENT_GENDER) {
                                                    innerHTML += "<div><b>Relation:</b> Father</div>";
                                                }
                                                else {
                                                    innerHTML += "<div><b>Relation:</b> Mother</div>";
                                                }
                                                 innerHTML += "<div><b>Email:</b> " + vjson[i].PARENT_EMAIL +"</div>";
                                                innerHTML += "<div><b>Mobile No.:</b> " + vjson[i].PARENT_MNUM +"</div>";
                                                if (vjson[i].PARENT_IS_STUDENT) {
                                                    innerHTML += "<div><b>Is Student:</b> Yes</div>";
                                                }
                                                else {
                                                    innerHTML += "<div><b>Is Student:</b> No</div>";
                                                }
                                                innerHTML += "</td>";
                                            }
                                        }


                                    }
                                }
                                //alert(innerHTML);
                                return innerHTML;
                            }
                            catch (ex) {
                                console.log(" error messge from getStudentParentData function : " + ex.message + " at " + ex.stack);
                            }
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
            return innerHTML;
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
                            <h2><strong>Students With Parent Information</strong> </h2>

                        </div>
                        <div class="body">
                            <div class="table-responsive">
                                <%--<div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" placeholder="Enter From Date" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <div class="form-group form-float">
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" placeholder="Enter To Date" ClientIDMode="Static" required></asp:TextBox>
                                </div>
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-raised btn-primary btn-round waves-effect" Text="Submit" ClientIDMode="Static" />--%>
                                <table class="table table-bordered table-striped table-hover js-basic-example dataTable">
                                    <thead>
                                        <tr>
                                            <th>Student ID</th>
                                            <th>Status</th>
                                            <th>First Name</th>
                                            <th>Middle Name</th>
                                            <th>Last Name</th>
                                            <th>Parents Detail</th>
                                            <th>DOB</th>
                                            <th>DOJ</th>
                                            <th>Mobile No</th>
                                            <th>Email</th>
                                            <th>Address</th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th>Student ID</th>
                                            <th>Status</th>
                                            <th>First Name</th>
                                            <th>Middle Name</th>
                                            <th>Last Name</th>
                                            <th>Parents Detail</th>
                                            <th>DOB</th>
                                            <th>DOJ</th>
                                            <th>Mobile No</th>
                                            <th>Email</th>
                                            <th>Address</th>
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

    <%--<script src="assets/bundles/mainscripts.bundle.js"></script>--%>
    <script src="assets/js/pages/tables/jquery-datatable.js"></script>
    <!-- Custom Js -->


    <%--    <script>
        var $j = jquery.noconflict();
        $j(document).ready(function () {
            getstudentdata("tmplstudentdata", "tbodystudentlist");
        });
        function getstudentdata(strtmplid, strbodyid) {
            try {
                var param = {};
                $j.ajax({
                    type: "post",
                    async: true,
                    data: JSON.stringify(param),
                    url: "old_studentreport.aspx/getstudentdata",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                      //  console.log(response.d);
                        if (response.d != "") {
                            var vjson = json.parse(response.d);
                            try {
                                $j("#" + strbodyid).html("");


                                $j("#" + strtmplid).tmpl(vjson).appendto("#" + strbodyid);
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
    <script type="text/x-jquery-tmpl" id="tmplstudentdata">
        <tr>
            <td>$j{stu_id}</td>
            <td>$j{stu_fname}</td>
            <td>$j{stu_mname}</td>
            <td>$j{stu_lname}</td>
            <td>$j{stu_num}</td>
        </tr>

    </script>--%>
</asp:Content>
