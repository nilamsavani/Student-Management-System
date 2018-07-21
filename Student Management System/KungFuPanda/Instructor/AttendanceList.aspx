<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/master.Master" AutoEventWireup="true" CodeBehind="AttendanceList.aspx.cs" Inherits="KungFuPanda.Instructor.AttendanceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <!-- Custom Css -->
    <link rel="stylesheet" href="assets/css/jquery-ui.css">
    <link rel="stylesheet" href="assets/css/main.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <script src="assets/js/Jquery_1.12.js"></script>

    <script>    
        $(document).ready(function () {
            getAttendancedata("tbodylist");
        });
        function getAttendancedata(strbodyid) {
            try {
                var param = {};
                $.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(param),
                    url: "AttendanceList.aspx/getAttendancedata",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                        if (response.d != "") {
                            var vjson = JSON.parse(response.d);
                            try {
                                var innerHTML = "";
                                $("#" + strbodyid).html("");
                                for (var i = 0; i < vjson.length; i++) {
                                     var attendancedate = new Date(vjson[i].ATT_DATE);
                                    var fattendancedate = (attendancedate.getMonth() + 1) + '/' + attendancedate.getDate() + '/' + attendancedate.getFullYear();

                                    innerHTML += "<tr>";

                                    innerHTML += "<td><a class=\"button button-small edit\" title=\"Edit\" onClick=\"EditStudent(" + vjson[i].ATT_ID + ")\" style='cursor:pointer;'><i class=\"zmdi zmdi-edit\" title=\"Edit\" style=\"color:green;\"></i></a></td>";
                                    if (vjson[i].ATT_STATUS) {
                                        innerHTML += "<td><span class=\"btn btn-round btn-simple btn-sm btn-success btn-filter\" onclick=\"ChangeStatus(" + vjson[i].ATT_ID + ")\">Approved</span></td>";
                                    }
                                    else {
                                        innerHTML += "<td><span class=\"btn btn-round btn-simple btn-sm btn-danger btn-filter\"  onclick=\"ChangeStatus(" + vjson[i].ATT_ID + ")\">Blocked</span></td>";
                                    }
                                    innerHTML += "<td>" + vjson[i].STU_NUM + "</td>";
                                    innerHTML += "<td>" + fattendancedate + "</td>";
                                    innerHTML += "<td>" + vjson[i].CLASS_LEVEL_NAME + "</td>";
                                    innerHTML += "<td>" + vjson[i].CLASS_DAY + "</td>";
                                    innerHTML += "<td>" + vjson[i].CLASS_TIME + "</td>";

                                    innerHTML += "</tr>";
                                }
                                $("#" + strbodyid).html(innerHTML);
                            }
                            catch (ex) {
                                console.log(" error messge from getAttendancedata function : " + ex.message + " at " + ex.stack);
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
        function ChangeStatus(strAttendanceID) {
            try {
                var param = { strAttendanceID: strAttendanceID };
                $.ajax({
                    type: "POST",
                    async: false,
                    data: JSON.stringify(param),
                    url: "AttendanceList.aspx/ChangeStatus",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (response) {
                        if (response.d != "") {
                            try {
                                $("#divMessage").css("display", "block");
                                getAttendancedata("tbodylist");
                            }
                            catch (ex) {
                                console.log(" error messge from ChangeStatus function : " + ex.message + " at " + ex.stack);
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
            return false;
        }
        function EditStudent(strAttendanceID) {
            window.location.href = "Attendance.aspx?ID=" + strAttendanceID;
            return false;
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
                            <h2><strong>Attendance List</strong> </h2>

                        </div>
                        <div class="body">
                            <div class="table-responsive">
                                <div class="alert alert-success" id="divMessage" style="display: none;">
                                    <strong>Attendance data updated successfully.</strong>
                                </div>
                                <asp:button runat="server" id="btnAddParent" cssclass="btn btn-raised btn-primary btn-round waves-effect" text="Add Attendance" onclick="btnAddParent_Click" />

                                <table class="table table-bordered table-striped table-hover js-basic-example dataTable">
                                    <thead>
                                        <tr>
                                            <th>Edit</th>
                                            <th>Status</th>
                                            <th>Student ID</th>
                                            <th>Date</th>
                                            <th>Class Level</th>
                                            <th>Day</th>
                                            <th>Time</th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th>Edit</th>
                                            <th>Status</th>
                                            <th>Student ID</th>
                                            <th>Date</th>
                                            <th>Class Level</th>
                                            <th>Day</th>
                                            <th>Time</th>
                                        </tr>
                                    </tfoot>
                                    <tbody id="tbodylist">
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



</asp:Content>
