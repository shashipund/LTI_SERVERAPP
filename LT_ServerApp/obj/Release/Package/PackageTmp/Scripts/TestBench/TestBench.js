$(document).ready(function () {
    var TestBenchDetail = {};
    var BenchID = "";
    var tableList = "";
    $('.loader-img').show();
    GetTestBenchList();

    $('#testBenchDatatable').DataTable({
        "filter": false,
        "info": false,
        "ordering": false,
        "lengthMenu": [[5, 10], [5, 10]]
    });

    $('#btnSaveTestBench').click(function () {
        $('.loader-img').show();
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
                
                alert("Test Bench Details Added!");
                $('.loader-img').hide();
                clearFields();
                $('#testBenchModal').modal('toggle');
                GetTestBenchList();

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
            $('#benchtableBody').empty();
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
                $('#benchtableBody').append(rows);
            }); //End of foreach Loop   
            //console.log(data);
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
