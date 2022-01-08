$(document).ready(function () {

    $('.datepicker').datepicker({
        "format": "mm-dd-yyyy"
    });
    $('.loader-img').hide();

    GetCount();
});
function GetWebLog() {
    $.ajax({
        type: "GET",
        url: "/Home/GetWebJobLog",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(JSON.stringify(data));
            //$('#weblogbody').empty();
            $('#tblWebLog').DataTable({
                "data": data,
                columns: [
                     { data: 'ID' },
                    { data: 'TestBenchID' },
                    { data: 'TableName' },
                    { data: 'StartDateTime' },
                    { data: 'EndDateTime' },
                    { data: 'Status' }
                ],
                "filter": true,
                "info": false,
                "order": [[0, "desc"]]
            });
            $('.loader-img').hide();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}
function GetCount() {
    $.ajax({
        type: "GET",
        url: "/Home/GetTestBenchCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("Count=" + data);
            $('#count').html(data);
            $('.loader-img').hide();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
    GetWebLog();
}