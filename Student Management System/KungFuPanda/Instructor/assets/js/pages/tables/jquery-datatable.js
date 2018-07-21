$(function () {
    $('.js-basic-example').DataTable({
        "order": [[1, "desc"]]
    });

    //Exportable table
    $('.js-exportable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
});