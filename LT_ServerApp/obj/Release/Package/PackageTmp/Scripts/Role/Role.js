var roleList = [];

$(document).ready(function () {
   
    GetRoles();

    $('#roleDatatable').DataTable({
        "data":roleList,
        "filter": false,
        "info": false,
        "lengthMenu": [[3, 6], [3, 6]]
    });
});

function GetRoles() {
    $.ajax({
        type: "GET",
        url: "/Role/GetRoles",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            
            $('.dataTables_empty').hide();
            $.each(data, function (i, item) {
                var option = "<option value=" + item.RoleID + ">" + item.RoleName + "</option>";
                $('#txtDDRole').append(option);
                //roleList.push(item);
                //roleList.push(item.RoleID, item.RoleName);
                var rows = "<tr>" +
                    "<td >" + item.RoleID + "</td>" +
                    "<td>" + item.RoleName + "</td>" +
                    "<td >"+
                    "<button class='btn btn-xs btn-warning' id='" + item.RoleID + "_RoleEdit'><span class='glyphicon glyphicon-pencil'></span></button>" +
                     " <button class='btn btn-xs btn-danger' id='" + item.RoleID + "_roleDelete'><span class='glyphicon glyphicon-trash'></span></button>" +
                     "</td>" +
                    "</tr>";
                $('#roleDatatable').append(rows);

            }); //End of foreach Loop   
            //console.log(data);
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

function saveRole() {
    var Role = {};
    var roleName = $('#txtRoleName').val();
    if (roleName == "") {
        alert("Role Name cannot be Empty!");
        return false;
    }

    Role.RoleName = roleName;

    $.ajax({
        type: "POST",
        url: "/Role/InsertRole",
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify(Role),
        success: function (response) {
                alert("New Role Added!");
                $('#myModal').modal('toggle');
                location.reload();
        },
        error: function (error) {
            alert("Something went wrong! Please try again.");
        }
    });
}