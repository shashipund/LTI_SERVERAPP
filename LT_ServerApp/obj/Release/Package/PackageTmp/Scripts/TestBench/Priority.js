$(document).ready(function () {

    GetPriorityList();

    $('#priorityDatatable').DataTable({
        "filter": false,
        "info": false,
        "ordering": false,
        "lengthMenu": [[3, 6], [3, 6]]
    });

    $('#btnPrioritySave').click(function () {
        var Priority = {};
        var Name = $('#txtPriority').val();
        var Frequency = $('#drpFrequency').val();
        
        if (Name == "") {
            alert("Pririty Name cannot be Empty!");
            return false;
        }
        if (Frequency == "0") {
            alert("Please Select Frequency!");
            return false;
        }

        Priority.PriorityName = Name;
        Priority.Frequency = Frequency;

        $.ajax({
            type: "POST",
            url: "/TestBench/InsertPriority",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(Priority),
            success: function (response) {
                $('#priorityModal').modal('toggle');
                alert("Priority Added!");
                location.reload();
            },
            error: function (error) {
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });
});

function GetPriorityList() {
    $.ajax({
        type: "GET",
        url: "/TestBench/GetPriorityList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.dataTables_empty').hide();
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td >" + item.ID + "</td>" +
                    "<td>" + item.PriorityName + "</td>" +
                    "<td >" + item.Frequency + "</td>" +
                    "<td >" +
                    "<button class='btn btn-xs btn-warning' id='" + item.ID + "_priorityEdit'><span class='glyphicon glyphicon-pencil'></span></button>" +
                     " <button class='btn btn-xs btn-danger' id='" + item.ID + "_priorityDelete'><span class='glyphicon glyphicon-trash'></span></button>" +
                     "</td>" +
                    "</tr>";
                $('#priorityDatatable').append(rows);
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