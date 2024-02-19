var dataTable;

$(document).ready(function () {
    var concertid = $("#concertId").val();
    loadDataTable(concertid);

    $('#AddPresentsbutton').click(function () {       

        $(document).ready(function () {
            $('#AddPresentsbutton').click(function (e) {
                e.preventDefault();
                addPresents();
            });
        });
        alert('Submit button clicked!');
    });
});

function loadDataTable(concertid) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Cashier/Order/GetAll/' + concertid },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'surName', "width": "25%" },
            { data: 'phone', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'concert.concertName', "width": "10%" },
            { data: 'concert.concertDate', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                return `<div class="w-75 btn-group" role="group">
                    <input asp-for="@obj.Present" type="checkbox" id="present-checkbox" class="present-checkbox" data-order-id="@obj.Id" />
                    <input type="hidden" name="order-id" value="@obj.Id" />
                    <input type="hidden" name="order-name" value="@obj.Name" />
                    <input type="hidden" name="order-surname" value="@obj.SurName" />
                    <input type="hidden" name="order-phone" value="@obj.Phone" />
                    <input type="hidden" name="order-email" value="@obj.Email" />
                    <input type="hidden" name="order-concertId" value="@obj.ConcertId" />

                </div>`
                },
                "width": "15%"
            }
        ],
        "submit": `<button type="submit" onclick="addPresents()" id="AddPresentsbutton" style="background-color: #04AA6D;border: none;color: white;padding: 15px 32px;text-align: center;text-decoration: none;display: inline-block;font-size: 16px;margin: 4px 2px;cursor: pointer;">
            Προσθήκη Παρουσιών
        </button>`
    });
}

function addPresents() {
    var orders = [];
    $('.present-checkbox:checked').each(function () {
        var orderId = $(this).data('order-id');
        var Name = $(this).data('order-name');
        var SurName = $(this).data('order-surname');
        var Phone = $(this).data('order-phone');
        var Email = $(this).data('order-email');
        var ConcertId = $(this).data('order-concertId');
        var present = $(this).prop('checked');
        orders.push({ Id: orderId, Name: Name, SurName: SurName, Phone: Phone, Email: Email, ConcertId=ConcertId, Present: present});
    });

    $.ajax({
        type: 'POST',
        url: '/Order/AddPresents',
        data: JSON.stringify(orders),
        contentType: 'application/json',      
    });
}