var items = Array(50, 60, 70, 80, 90), bg_color = Array('bg-danger', 'bg-warning', 'bg-purple', 'bg-primary', 'bg-success');
model.init.employee = function () {
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
        aoColumns: [
            { data: 'EmployeeID' },
            { data: 'FullName' },
            { data: 'Religion' },
            {
                data: null,
                render: function () {
                    //return items[Math.floor(Math.random() * items.length)];
                    let rand = Math.floor(Math.random() * items.length);
                    let score = items[rand];
                    //let bg = bg_color[Math.floor(Math.random() * 5)];
                    let bg = bg_color[rand];
                    console.log(rand, bg_color, bg)
                    return `<div class="progress-text">${score}pt</div>
                              <div class="progress progress-xs" data-height="6">
                                <div class="progress-bar ${bg} width-per-${score}"></div>
                              </div>`;
                    //return `<div class="progress-text">${score}%</div>
                    //      <div class="progress progress-xs" data-height="6">
                    //        <div class="progress-bar bg-success" data-width="${score}%"></div>
                    //      </div>`;
                }
            },
            {
                data: null,
                //defaultContent: '<a class="btn" href="/Admin/Employee/Get/'+ data[0] +'">Detail</a>',
                render: function (d, t, f) {
                    return `<a class="btn btn-primary" href="/Admin/EmployeeAssessment/Detail/${d.EmployeeID}">Detail</a>`;
                },
                targets: -1
            }
        ],
    });
}