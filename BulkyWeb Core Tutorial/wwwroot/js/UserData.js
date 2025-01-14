var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/User/GetAll' },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/User/RoleManagement?userId=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Update</a>               
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}