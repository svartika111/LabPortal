$(document).ready(function () {
    var apiBaseUrl = "http://localhost:62647/";
    $('#btnGetData').click(function () {
        $.ajax({
            url: apiBaseUrl + 'api/StudentRecords',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var $table = $('<table/>').addClass('dataTable table table-bordered table-striped');
                var $header = $('<thead/>').html('<tr><th>UserName</th><th>Password</th><th>Email</th><th>CreatedDate</th><th>LastLoginDate</th></tr>');
                $table.append($header);
                $.each(data, function (i, val) {
                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(val.Username));
                    $row.append($('<td/>').html(val.Password));
                    $row.append($('<td/>').html(val.Email));
                    $row.append($('<td/>').html(val.CreatedDate));
                    $row.append($('<td/>').html(val.LastLoginDate));
                    $table.append($row);
                });
                $('#updatePanel').html($table);
            },
            error: function () {
                alert('Error!');
            }
        });
    });
});  