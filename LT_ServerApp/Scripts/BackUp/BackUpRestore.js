$(document).ready(function () {
    GetDatabaseList();
    GetBackUpList();
    $('input:radio[name=radios]').change(function () {
        if (this.value == '1') {
            $('#divBackUp').css('display', 'inline');
            $('#divRestore').css('display', 'none');
        }
        else if (this.value == '2') {
            $('#divBackUp').css('display', 'none');
            $('#divRestore').css('display', 'inline');
        }
    });

    $('#btnBackUpDatabase').click(function () {
        var database = $('#drpDatabaseList').val();
        $.ajax({
            type: "GET",
            url: "/Restore/BackUpDatabase?dbName=" + database,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "Success") {
                    alert("Database BackUp Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database BackUp Failed! Please try again!");
                }
                
            }, //End of AJAX Success function  

            failure: function (data) {
                if (data == "Success") {
                    alert("Database BackUp Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database BackUp Failed! Please try again!");
                }
            }, //End of AJAX failure function  
            error: function (data) {
                if (data == "Success") {
                    alert("Database BackUp Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database BackUp Failed! Please try again!");
                }
            } //End of AJAX error function  

        });
    });

    $('#btnRestore').click(function () {
        var testBenchID = $('#drpTestBenchL').val();
        var backupfile = $('#drpBackupDbList').val();
        $.ajax({
            type: "GET",
            url: "/Restore/RestoreDatabase?testBenchID=" + testBenchID + "&backUpFile=" + backupfile,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "Success") {
                    alert("Database Restored Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database Restore Failed! Please try again!");
                }

            }, //End of AJAX Success function  

            failure: function (data) {
                if (data == "Success") {
                    alert("Database Restored Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database Restored Failed! Please try again!");
                }
            }, //End of AJAX failure function  
            error: function (data) {
                if (data == "Success") {
                    alert("Database Restored Successfully Completed!!");
                    GetBackUpList();
                }
                else {
                    alert("Database Restored Failed! Please try again!");
                }
            } //End of AJAX error function  

        });
    });
});
function GetDatabaseList() {

    var dropdown = "";
    dropdown = dropdown + "<option value='0'>Select Test Bench</option>";
    var dbList = "";
    dbList = dbList + "<option value='0'>Select Database</option>";

    $.ajax({
        type: "GET",
        url: "/TestBench/GetTestBenchList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#drpDatabaseList').empty();
            $('#drpTestBenchL').empty();

            $.each(data, function (i, item) {
                dropdown = dropdown + "<option value=" + item.ID + ">" + item.TestBenchID + "</option>";
                dbList = dbList + "<option value=" + item.DBName + ">" + item.DBName + "</option>";
            });
            $('#drpDatabaseList').append(dbList);
            $('#drpTestBenchL').append(dropdown);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

function GetBackUpList() {

    var dropdown = "";
    dropdown = dropdown + "<option value='0'>Select Database File to Restore</option>";
    $.ajax({
        type: "GET",
        url: "/Restore/GetBackUpLog",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#drpBackupDbList').empty();
            $.each(data, function (i, item) {
                dropdown = dropdown + "<option value=" + item.BackUpName + ">" + item.BackUpName + "</option>";
            });
            $('#drpBackupDbList').append(dropdown);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}