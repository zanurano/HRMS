model.init.employee = function () {
    //$.get(`/Admin/Employee/Gets`, function (result) {
    //    console.log(result);
    //    let res = result.data;
    //    $("#tblEmployee").DataTable({
    //        aaData: res,
    //        aoColumns: [
    //            { data: 'employeeID' },
    //            { data: 'fullName' },
    //            { data: 'religion' },
    //            { data: 'gender' },
    //            { data: 'birthDate' },
    //            { data: 'birthPlace' }
    //        ],
    //    });
    //});
    var dt = $("#tblEmployee").DataTable({
        destroy: true,
        search: true,
        "ajax": {
            "url": "/Admin/Employee/Gets",
            //"type": "GET",
            "dataSrc": "Data",
            //"contentType": 'application/json; charset=utf-8',
            //"dataType": 'json',
        },
        //columns: [
        //    { data: 'employeeID' },
        //    { data: 'fullName' },
        //    { data: 'religion' },
        //    { data: 'gender' },
        //    { data: 'birthDate' },
        //    { data: 'birthPlace' },
        //    { data: null }
        //],
        //columnDefs: [
        //    {
        //        data: null,
        //        defaultContent: '<button class="btn btn-success">Click!</button>',
        //        targets: -1
        //    }
        //]
        aoColumns: [
            { data: 'EmployeeID' },
            { data: 'FullName' },
            { data: 'Religion' },
            { data: 'Gender' },
            { data: 'BirthDate' },
            { data: 'BirthPlace' },
            { data: 'CurrentPosition' },
            {
                data: null,
                //defaultContent: '<a class="btn" href="/Admin/Employee/Get/'+ data[0] +'">Detail</a>',
                render: function (d, t, f) {
                    return `<a class="btn btn-primary" href="/Admin/Employee/Detail/${d.EmployeeID}">Detail</a>`;
                },
                targets: -1
            }
        ],        
    });
}

model.on.create = () => {
    window.location.href = urlAction;
}