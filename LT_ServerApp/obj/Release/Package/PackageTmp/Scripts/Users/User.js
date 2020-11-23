$(document).ready(function () {

    GetUserList();

    $('#userDatatable').DataTable({
        "filter": false,
        "info": false,
        "lengthMenu": [[5, 10], [5, 10]]
    });

    $('#btnUserSave').click(function () {
        var User = {};
        var Name = $('#txtName').val();
        var Email = $('#txtEmail').val();
        var Pass = $('#txtPassword').val();
        var Phone = $('#txtPhone').val();
        var RoleID = $('#txtDDRole').val();
        if (Name == "") {
            alert("UserName cannot be Empty!");
            return false;
        }
        if (Email == "") {
            alert("Email ID cannot be Empty!");
            return false;
        }
        if (Pass == "") {
            alert("Default Password cannot be Empty!");
            return false;
        }

        if (RoleID == "0") {
            alert("Please Select User Role!");
            return false;
        }

        User.Name = Name;
        User.Email = Email;
        User.Mobile = Phone;
        User.Password = Pass;
        User.RoleID = RoleID;

        $.ajax({
            type: "POST",
            url: "/User/InsertUser",
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(User),
            success: function (response) {
                alert("New User Added!");
                $('#userModal').modal('toggle');
                location.reload();
            },
            error: function (error) {
                alert("Something Went Wrong! Please try Again !");
            }
        });
    });
});

function GetUserList() {
    $.ajax({
        type: "GET",
        url: "/User/GetUserList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('.dataTables_empty').hide();
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td >" + item.UserID + "</td>" +
                    "<td>" + item.Name + "</td>" +
                    "<td >" + item.Mobile + "</td>" +
                    "<td>" + item.Email + "</td>" +
                    "<td>" + item.RoleName + "</td>" +
                    "<td >" +
                    "<button class='btn btn-xs btn-warning' id='" + item.UserID + "_UserEdit'><span class='glyphicon glyphicon-pencil'></span></button>" +
                     " <button class='btn btn-xs btn-danger' id='" + item.UserID + "_UserDelete'><span class='glyphicon glyphicon-trash'></span></button>" +
                     "</td>" +
                    "</tr>";
                $('#userTableBody').append(rows);
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