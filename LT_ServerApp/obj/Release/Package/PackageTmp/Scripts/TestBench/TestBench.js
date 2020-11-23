$(document).ready(function () {
    var TestBenchDetail = {};
    var BenchID="";
    var tableList = "";
    $('#btnGetTestBenchDetails').hide();
    GetTestBenchList();

    $('#testBenchDatatable').DataTable({
        "filter": false,
        "info": false,
        "ordering": false,
        "lengthMenu": [[5, 10], [5, 10]]
    });

    $('#btnSaveTestBench').click(function () {

        var TestbenchID = $('#txtTestBenchID').val();
        var TestBenchName = $('#txtTestBenchName').val();
        var DBName = $('#txtDBName').val();
        var IPAddress = $('#txtIPAddress').val();
        var DBuser = $('#txtDBUser').val();
        var DBPassword = $('#txtDBPassword').val();
        var PortNo = $('#txtPortNo').val();
        BenchID = TestbenchID;
        if (TestbenchID == "") {
            alert("Test BenchID cannot be Empty!");
            return false;
        }
        if (TestBenchName == "") {
            alert("TestBenchName cannot be empty!");
            return false;
        }
        if (DBName == "") {
            alert("DBName cannot be Empty!");
            return false;
        }
        if (IPAddress == "") {
            alert("IP Address cannot be empty!");
            return false;
        }
        if (PortNo == "") {
            alert("Port No cannot be Empty!");
            return false;
        }
        if (DBuser == "") {
            alert("DB User cannot be Empty!");
            return false;
        }
        if (DBPassword == "") {
            alert("DB Password Cannot be empty!");
            return false;
        }

        TestBenchDetail.TestBenchID = TestbenchID;
        TestBenchDetail.TestBenchName = TestBenchName;
        TestBenchDetail.DBName = DBName;
        TestBenchDetail.IPAddress = IPAddress;
        TestBenchDetail.DBUser = DBuser;
        TestBenchDetail.DBPassword = DBPassword;
        TestBenchDetail.PortNo = PortNo;

        $.ajax({
            type: "POST",
            url: "/TestBench/InsertTestBench",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(TestBenchDetail),
            success: function (response) {
                //$('#priorityModal').modal('toggle');
                alert("Test Bench Details Added!");
                $('#btnSaveTestBench').hide();
                $('#btnGetTestBenchDetails').show();
            },
            error: function (error) {
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });


    $('#btnGetTestBenchDetails').click(function () {
        $.ajax({
            type: "POST",
            url: "/TestBench/GetTables",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(TestBenchDetail),
            success: function (response) {
                tableList = response;
                alert("Test Bench Tables Fetched!");
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
                            
                            $.each(data, function (i, item) {
                                priorityList =priorityList + "<option value="+item.ID+">"+item.PriorityName+"</option>";
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
                        } //End of AJAX error function  

                    });
                    
                }
            },
            error: function (error) {
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });

    $('#btnTableSave').click(function () {
        var len = $("#tableList").find("li").length;
        var testBenchTable = [];
        
        for (var i = 0; i < len; i++) {
            var tableObject = {};
                tableObject.TestBenchID = BenchID;
                tableObject.TableName = tableList[i]["TABLE_NAME"];
                if ($('#drpFrequency_' + i).val() == "0")
                {
                    alert("Select Frequency for Table !");
                    return;
                }
                tableObject.PriorityID = $('#drpFrequency_' + i).val();
            
                testBenchTable.push(tableObject);
        }

        $.ajax({
            type: "POST",
            url: "/TestBench/InsertTablePriority",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(testBenchTable),
            success: function (response) {
                if (response.StatusCode == "200") {
                    $('#testBenchModal').modal('toggle');
                    alert("Test Bench Details Added!");
                    location.reload();
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

function GetTestBenchList() {
    $.ajax({
        type: "GET",
        url: "/TestBench/GetTestBenchList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.dataTables_empty').hide();
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td >" + item.ID + "</td>" +
                    "<td>" + item.TestBenchID + "</td>" +
                    "<td >" + item.TestBenchName + "</td>" +
                    "<td >" + item.DBName + "</td>" +
                    "<td>" + item.IPAddress + "</td>" +
                    "<td >" + item.DBUser + "</td>" +
                    "<td >" + item.PortNo + "</td>" +
                    "<td >" +
                    "<button class='btn btn-xs btn-warning' id='" + item.ID + "_testEdit'><span class='glyphicon glyphicon-pencil'></span></button>" +
                     " <button class='btn btn-xs btn-danger' id='" + item.ID + "_testDelete'><span class='glyphicon glyphicon-trash'></span></button>" +
                     "</td>" +
                    "</tr>";
                $('#testBenchDatatable').append(rows);
            }); //End of foreach Loop   
            console.log(data);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

function clearFields() {
    $('#txtTestBenchID').val('');
    $('#txtTestBenchName').val('');
    $('#txtDBName').val('');
    $('#txtIPAddress').val('');
    $('#txtDBUser').val('');
    $('#txtDBPassword').val('');
    $('#txtPortNo').val('');
    $('#tableList').empty();
    $('#dropDownList').empty();
}
