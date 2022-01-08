$(document).ready(function () {
    var TestBenchDetail = {};
    var BenchID = "";
    var tableList = "";

    $('.loader-img').show();
    GetTestBenchDetails();

    $('#drpTestBench1').on('change', function (e) {
        var id = $("#drpTestBench1").val();
        GetTableList(id);
    });

    $('#getBBTHVDetails').on('click', function () {
        
        var testBenchID = $("#drpTestBench1").val();
        var tableName = $("#drpTestBenchTable").val();
        var fromDate = $('#fromDate').val();
        var toDate = $('#toDate').val();

        if (tableName == "BBT_HV_Production") {
            $('#BBTHVtableDiv').css('display', 'inline');
            $('#BBTIRtableDiv').css('display', 'none');
            $('#BBTIRSettingtableDiv').css('display', 'none');
            GetBBTHVTableData(testBenchID, tableName, fromDate, toDate);
        }

        if (tableName == "BBT_IR_Production") {
            $('#BBTIRtableDiv').css('display', 'inline');
            $('#BBTHVtableDiv').css('display', 'none');
            $('#BBTIRSettingtableDiv').css('display', 'none');
            GetBBTIRTableData(testBenchID, tableName, fromDate, toDate);
        }
        if (tableName == "BBT_IR_Settings") {
            $('#BBTIRSettingtableDiv').css('display', 'inline');
            $('#BBTIRtableDiv').css('display', 'none');
            $('#BBTHVtableDiv').css('display', 'none');
            GetBBTIRSettingTableData(testBenchID, tableName, fromDate, toDate);
        }
    });
});

function GetTestBenchDetails() {

    var dropdown = "";
    dropdown = dropdown + "<option value='0'>Select Test Bench</option>";
    $.ajax({
        type: "GET",
        url: "/TestBench/GetTestBenchList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#drpTestBench1').empty();
            $.each(data, function (i, item) {
                dropdown = dropdown + "<option value=" + item.ID + ">" + item.TestBenchID + "</option>";
            });
            $('#drpTestBench1').append(dropdown);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

function GetTableList(id) {

    var dropdown = "";
    dropdown = dropdown + "<option value='0'>Select Table</option>";
    if (id != "0") {
        $.ajax({
            type: "GET",
            url: "/TestBench/GetTableList?rowId=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#drpTestBenchTable').empty();
                $.each(data, function (i, item) {
                    dropdown = dropdown + "<option value=" + item.TableName + ">" + item.TableName + "</option>";
                });
                $('#drpTestBenchTable').append(dropdown);
            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });
    }
}
function GetBBTHVTableData(id, tableName, fromDate, toDate) {

    $('.loader-img').show();
    $('#BBTHVDatatable').DataTable().destroy();
    $.ajax({
        type: "GET",
        url: "/Report/GetBBTHVTableData?testBenchID=" + id + "&tableName=" + tableName + "&fromDate=" + fromDate + "&toDate=" + toDate,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#BBTHVDatatable').DataTable({
                "data": data,
                "columns": [
                    { "data": "SrNo" },
                    { "data": "Barcode" },
                    { "data": "Date_Time" },
                    { "data": "ProjNo" },
                    { "data": "MOCode" },
                    { "data": "TagNo" },
                    { "data": "StackNo" },
                    { "data": "Voltage" },
                    { "data": "STP1" },
                    { "data": "STP2" },
                    { "data": "STP3" },
                    { "data": "STP4" },
                    { "data": "STP5" },
                    { "data": "STP6" },
                    { "data": "HV" },
                    { "data": "Date" },
                ],
                "paging": true,
                "filter": true,
                "info": false,
                "order": [[0, "desc"]],
                "dom": 'lBfrtip',
                "buttons": ['excel', 'csv', 'pdf', 'copy'],
                "lengthMenu":[[10,25,50,-1],[10,25,50,"All"]]
            });
            
            $('.loader-img').hide();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        } //End of AJAX error function  

    });
}

function GetBBTIRTableData(id, tableName, fromDate, toDate) {

    $('.loader-img').show();
    $('#BBTIRDatatable').DataTable().destroy();
    $.ajax({
        type: "GET",
        url: "/Report/GetBBTIRTableData?testBenchID=" + id + "&tableName=" + tableName + "&fromDate=" + fromDate + "&toDate=" + toDate,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#BBTIRDatatable').DataTable({
                "data": data,
                "columns": [
                    { "data": "SrNo" },
                    { "data": "Barcode" },
                    { "data": "Username" },
                    { "data": "Date_Time" },
                    { "data": "ProjNo" },
                    { "data": "MOCode" },
                    { "data": "TagNo" },
                    { "data": "StkNo" },
                    { "data": "LowLimit" },
                    { "data": "UppLimit" },
                    { "data": "STP1" },
                    { "data": "STP2" },
                    { "data": "STP3" },
                    { "data": "STP4" },
                    { "data": "STP5" },
                    { "data": "STP6" },
                    { "data": "STP7" },
                    { "data": "STP8" },
                    { "data": "STP9" },
                    { "data": "STP10" },
                    { "data": "STP11" },
                    { "data": "STP12" },
                    { "data": "STP13" },
                    { "data": "STP14" },
                    { "data": "STP15" },
                    { "data": "result" },
                    { "data": "Date" },
                ],
                "paging": true,
                "filter": true,
                "info": false,
                "order": [[0, "desc"]],
                "dom": 'lBfrtip',
                "buttons": ['excel', 'csv', 'pdf', 'copy'],
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
            });
            
            $('.loader-img').hide();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        } //End of AJAX error function  

    });
}

function GetBBTIRSettingTableData(id, tableName, fromDate, toDate) {

    $('.loader-img').show();
    $('#BBTIRSettingDatatable').DataTable().destroy();
    $.ajax({
        type: "GET",
        url: "/Report/GetBBT_IR_Settings?testBenchID=" + id + "&tableName=" + tableName + "&fromDate=" + fromDate + "&toDate=" + toDate,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#BBTIRSettingDatatable').DataTable({
                "data": data,
                "columns": [
                    { "data": "TestSelection" },
                    { "data": "TestTime" },
                    { "data": "RampTime" },
                    { "data": "TestVoltage" },
                    { "data": "HIResistanceValue" },
                    { "data": "LOResistanceValue" }
                    ],
                "paging": true,
                "filter": true,
                "info": false,
                "order": [[0, "desc"]],
                "dom": 'lBfrtip',
                "buttons": ['excel', 'csv', 'pdf', 'copy'],
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
            });
            $('.loader-img').hide();
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
            $('.loader-img').hide();
        } //End of AJAX error function  

    });
}

