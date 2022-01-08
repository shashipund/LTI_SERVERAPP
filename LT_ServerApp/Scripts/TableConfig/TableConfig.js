var BenchID = "";
$(document).ready(function () {
    
    GetTestBenchdata();
    GetTableConfig();
    $('#tablePriorityDetails').DataTable({
        "filter": false,
        "info": false,
        "ordering": false,
        "lengthMenu": [[5, 10], [5, 10]]
    });
    $('#btnGetTestBenchDetails').click(function () {
        $('.loader-img').show();
        BenchID = $('#drpTestBench').val();
        $.ajax({
            type: "GET",
            url: "/TestBench/GetTables?rowId="+BenchID,
            contentType: 'application/json',
            dataType: "json",
            success: function (response) {
                tableList = response;
                
                $('#tableList').empty();
                $('#dropDownList').empty();
                if (response != null || response != undefined) {
                    $.each(response, function (i, item) {
                        var rows = "<li style='margin-bottom:25px;'>" +
                            "<label>" + item.TABLE_NAME + "</label> <br /></li>";
                        //location.reload();
                        $('#tableList').append(rows);


                    });
                    var length = response.length;
                    var priorityList = "";

                    $.ajax({
                        type: "GET",
                        url: "/TestBench/GetPriorityList",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            $('.loader-img').hide();
                            $.each(data, function (i, item) {
                                priorityList = priorityList + "<option value=" + item.ID + ">" + item.PriorityName + "</option>";
                            });
                            var select = "";
                            for (var i = 0; i < length; i++) {
                                select = select + "<select id='drpFrequency_" + i + "' name='SelectFrequency' class='form-control' style='margin-bottom:15px;'>" +
                                    "<option value='0'>Select Frequency</option>" +
                                    priorityList +
                                    "</select>";

                            }
                            $('#dropDownList').append(select);
                        }, //End of AJAX Success function  
                        error: function (data) {
                            alert("Something went wrong ! try Again !!");
                            $('.loader-img').hide();
                        } //End of AJAX error function  
                        

                    });
                    $('#btnTableSave').removeAttr('disabled');

                }
                else {
                    $('.loader-img').hide();
                    alert("Something Went Wrong! Please try Again !");
                }
            },
            error: function (error) {
                $('.loader-img').hide();
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });

    $('#btnTableSave').click(function () {
        var len = $("#tableList").find("li").length;
        var testBenchTable = [];
        var priorityID = "";
        for (var i = 0; i < len; i++) {
            var tableObject = {};
            priorityID = $('#drpFrequency_' + i).val();
            if (priorityID != "0") {
                tableObject.TestBenchID = BenchID;
                tableObject.TableName = tableList[i]["TABLE_NAME"];
                tableObject.PriorityID = priorityID;
                testBenchTable.push(tableObject);
            }
        }

        $.ajax({
            type: "POST",
            url: "/TestBench/InsertTablePriority",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(testBenchTable),
            success: function (response) {
                if (response.StatusCode == "200") {
                    //$('#testBenchModal').modal('toggle');
                    alert("Table Configuration Data Saved!");
                    //$('#dropDownList').empty();
                    //$('#tableList').empty();
                    location.reload();
                    GetTableConfig();
                    
                }
                else {
                    alert("Something went wrong! Please try again!");
                }
            },
            error: function (error) {
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });
});

function GetTestBenchdata() {

    var dropdown = "";
    dropdown = dropdown + "<option value='0'>Select Test Bench</option>";
    $.ajax({
        type: "GET",
        url: "/TestBench/GetTestBenchList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#drpTestBench').empty();
            $.each(data, function (i, item) {
                dropdown = dropdown + "<option value=" + item.ID + ">" + item.TestBenchID + "</option>";
            });
            $('#drpTestBench').append(dropdown);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

function GetTableConfig() {

    $('.loader-img').show();
    $.ajax({
        type: "GET",
        url: "/TableConfig/GetTableConfigList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.loader-img').hide();
            $('#tableBdy').empty();
            var count = 0;
            $.each(data, function (i, item) {
                count++;
                var rows = "<tr>" +
                    "<td >" + count + "</td>" +
                    "<td>" + item.TestBenchID + "</td>" +
                    "<td >" + item.TestBenchName + "</td>" +
                    "<td >" + item.TableName + "</td>" +
                    "<td>" + item.PriorityName + "</td>" +
                    "<td >" + item.Frequency + "</td>" +
                    //"<td >" +
                    //"<button class='btn btn-xs btn-warning' id='" + item.TestBenchID + "_testEdit'><span class='glyphicon glyphicon-pencil'> CreateTable</span></button>" +
                    // "</td>" +
                    "</tr>";
                $('#tableBdy').append(rows);
            }); //End of foreach Loop   
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