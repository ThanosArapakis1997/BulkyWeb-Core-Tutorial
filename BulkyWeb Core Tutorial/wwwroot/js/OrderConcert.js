var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Cashier/Order/GetAll' },
        "columns": [
            { data: 'ConcertName', "width": "20%" },
            { data: 'ArtistName', "width": "20%" },
            { data: 'ConcertDate', "width": "25%" },
            { data: 'MusicVenue.Name', "width": "10%" },
            { data: 'Price', "width": "10%" },
            {
                data: 'Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/cashier/order/index?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Παρουσίες</a>               
                    </div>`
                },
                "width": "15%"
            }
        ]
    }); d
}