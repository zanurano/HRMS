model.data.assessmentIndicator = ko.observable(ko.mapping.toJS(model.prototype.assessmentIndicator));

model.is.newAssessment = ko.observable();

model.init.users = function () {
        $('#dtTable').DataTable();
    }

model.init.assessmentIndicator = function () {
    //if ($.fn.DataTable.isDataTable("#dtTable")) {
    //    $('#dtTable').DataTable().clear().destroy();
    //}

    $('#dtTable').DataTable({
        destroy: true,
        search: true,
        "ajax": {
            "url": "/Admin/Administrator/AssessmentIndicator/Gets",
            //"type": "GET",
            "dataSrc": "Data",
            //"contentType": 'application/json; charset=utf-8',
            //"dataType": 'json',
        },
        aoColumns: [
            { data: 'Name' },
            { data: 'Position' },
            { data: 'QualityValue' },
            {
                data: null,
                //defaultContent: '<a class="btn" href="/Admin/Employee/Get/'+ data[0] +'">Detail</a>',
                render: function (d, t, f) {
                    return `<a class="btn btn-primary m-1" href="#" data-toggle="modal" data-target="#modalAssessment""><i class="far fa-edit"></i></a>
                    <a class="btn btn-primary m-1" href="#"><i class="fas fa-trash"> Delete</a>`;
                },
                targets: -1
            }
        ],
    });
}

model.on.saveAssessment = () => {
    let param = model.data.assessmentIndicator();
    $("#btn-save").addClass("btn-progress");

    if (model.is.newAssessment()) {
        console.log("param AssessmentIndicator:", param);
        $.post("/Admin/Administrator/AssessmentIndicator/Insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.init.assessmentIndicator();
        });
    } else {
        console.log("param AssessmentIndicator:", param);
        $.post("/Admin/Administrator/AssessmentIndicator/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.init.assessmentIndicator();
        });
    }
}