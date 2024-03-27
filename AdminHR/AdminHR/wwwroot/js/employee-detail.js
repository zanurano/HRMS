model.data.employeeResume = ko.observable(ko.mapping.toJS(model.prototype.resume));
model.data.employeeFamily = ko.observable(ko.mapping.toJS(model.prototype.family));
model.data.employeeEducation = ko.observable(ko.mapping.toJS(model.prototype.education));
model.data.employeeWorkExpreience = ko.observable(ko.mapping.toJS(model.prototype.workExperience));

model.is.loading = ko.observable();
model.is.newFamilyMember = ko.observable(true);
model.is.newEducation = ko.observable(true);
model.is.newWorkExperience = ko.observable(true);

model.init.form = function (id) {
    console.log("edit:", id);
    model.data.employeeWorkExpreience().StartDate = new Date();
    model.data.employeeWorkExpreience().EndDate = new Date();

    if (isEdit == "True") {
        console.log("update data");
        model.is.loading(true);
        model.data.employeeResume().EmployeeID = id;
        model.data.employeeFamily().EmployeeID = id;
        model.data.employeeEducation().EmployeeID = id;
        model.data.employeeWorkExpreience().EmployeeID = id;
        

        model.get.resume(id);
        model.get.family(id);
        model.get.education(id);
        model.get.workExperience(id);
    }    
}

model.get.resume = (id) => {
    console.log("fetch data");
    $.get(`/Admin/Employee/Get/${id}`, function (result) {
        model.is.loading(false);
        model.data.employeeResume(result.Data);
    });
}

model.get.family = (id) => {
    if ($.fn.DataTable.isDataTable("#tblFamily")) {
        $('#tblFamily').DataTable().clear().destroy();
    }

    $.get(`/Admin/Employee/Family/Get/${id}`, function (result) {
        console.log("family:", result);
        $("#tblFamily").DataTable({
            aaData: result.Data,
            aoColumns: [
                { data: 'FullName' },
                { data: 'Religion' },
                { data: 'Gender' },
                { data: 'RelationShip' },
                { data: 'BirthDate' },
                { data: 'BirthPlace' },
                {
                    data: null,
                    render: function (d, t, f) {
                        console.log("data f:", f)
                        return `<a class="btn btn-primary m-1" href="#" data-toggle="modal" data-target="#modalFamily" onClick="model.on.editFamily"><i class="far fa-edit"></i></a>` +
                            `<a class="btn btn-danger m-1" href="#"><i class="far fa-trash-alt"></i></a>`;
                    },
                    targets: -1
                }
            ],
        });
    });
}

model.get.education = (id) => {
    if ($.fn.DataTable.isDataTable("#tblEducation")) {
        $('#tblEducation').DataTable().clear().destroy();
    }

    $.get(`/Admin/Employee/GetEducation/${id}`, function (result) {
        console.log("education:", result);

        $("#tblEducation").DataTable({
            "oLanguage": {
                "sEmptyTable": "Empty Data"
            },
            "language": {
                "emptyTable": "Nooo data",
                "infoEmpty": "No entries to show"
            },
            aaData: result.Data,
            aoColumns: [
                { data: 'SchoolName' },
                { data: 'YearStart' },
                { data: 'YearEnd' },
                { data: 'Title' },
                { data: 'Major' },
                {
                    data: null,
                    //defaultContent: '<a class="btn" href="/Admin/Employee/Get/'+ data[0] +'">Detail</a>',
                    render: function (d, t, f) {
                        return `<a class="btn btn-primary m-1" href="#" data-toggle="modal" data-target="#modalEducation" data-bind="click: model.on.editEducation"><i class="far fa-edit"></i></a>` +
                            `<a class="btn btn-danger m-1" href="#"><i class="far fa-trash-alt"></i></a>`;
                    },
                    targets: -1
                }
            ],
        });
    });
}

model.get.workExperience = (id) => {
    if ($.fn.DataTable.isDataTable("#tblWorkExperience")) {
        $('#tblWorkExperience').DataTable().clear().destroy();
    }

    $.get(`/Admin/Employee/GetWorkExperience/${id}`, function (result) {
        console.log("workExperience:", result);

        $("#tblWorkExperience").DataTable({
            aaData: result.Data,
            aoColumns: [
                { data: 'CompanyName' },
                { data: 'Position' },
                { data: 'StartDate' },
                { data: 'EndDate' },
                {
                    data: null,
                    //defaultContent: '<a class="btn" href="/Admin/Employee/Get/'+ data[0] +'">Detail</a>',
                    render: function (d, t, f) {
                        return `<a class="btn btn-primary m-1" href="#" data-toggle="modal" data-target="#modalWorkExperience" data-bind="click: model.on.editWorkExperience"><i class="far fa-edit"></i></a>` +
                            `<a class="btn btn-danger m-1" href="#"><i class="far fa-trash-alt"></i></a>`;
                    },
                    targets: -1
                }
            ],
        });
    });
}

model.on.back = () => {
    //history.go(-1);
    window.history.back();
}

model.on.saveResume = function () {
    let param = model.data.employeeResume();
    //$.ajax({
    //    type: "POST",
    //    url: `/Admin/Employee/Save`,
    //    data: param,
    //    contentType: "application/json; charset=utf-8",
    //    success: function (r) {
    //        swal('Save Data', r, 'success');
    //    },
    //    dataType: "json"
    //});
    $("#btn-save-resume").addClass("btn-progress");
    if (isEdit == "True") {
        $.post("/Admin/Employee/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-resume").removeClass("btn-progress");
        });
    } else {
        $.post("/Admin/Employee/Insert", param, function (res) {
            swal('Good Job', 'You has been create new', 'success');
            $("#btn-save-resume").removeClass("btn-progress");
        });
    }
}

model.on.deleteResume = () => {
    swal({
        title: 'Are you sure?',
        text: 'You will delete employee data and everything related to it, and you will not be able to recover it!',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
        if (willDelete) {
            swal('Data has been deleted!', {
                icon: 'success',
            });
        }
    });
}

model.on.addNewFamily = () => {
    model.is.newFamilyMember(true);
}

model.on.editFamily = function (f) {
    console.log("edit:", f)
    model.is.newFamilyMember(false);
    model.data.employeeFamily(f);
}

model.on.saveFamily = () => {
    let param = model.data.employeeFamily();
    $("#btn-save-family").addClass("btn-progress");

    if (model.is.newFamilyMember()) {
        console.log("param family:", param);
        $.post("/Admin/Employee/Family/Insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-family").removeClass("btn-progress");
            model.get.family();
        });
    } else {
        console.log("param family:", param);
        $.post("/Admin/Employee/Family/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-family").removeClass("btn-progress");
            model.get.family();
        });
    }    
}

model.on.addNewEducation = () => {
    model.is.newEducation(true);
}

model.on.editEducation = () => {
    model.is.newEducation(false);
}

model.on.saveEducation = () => {
    let param = model.data.employeeEducation();
    $("#btn-save-education").addClass("btn-progress");

    if (model.is.newEducation()) {
        console.log("insert param education:", param);
        $.post("/Admin/Employee/Education/Insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-education").removeClass("btn-progress");
        });
    } else {
        console.log("edit param education:", param);
        $.post("/Admin/Employee/Education/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-education").removeClass("btn-progress");
        });
    }
}

model.on.addNewWorkExperience = () => {
    model.is.newWorkExperience(true);
}

model.on.editWorkExperience = () => {
    model.is.newWorkExperience(false);
}

model.on.saveWorkExperience = () => {
    $("#btn-save-workexperience").addClass("btn-progress");
    let param = model.data.employeeWorkExpreience();
    if (model.is.newWorkExperience()) {
        console.log("insert param workexperience:", param);
        $.post("/Admin/Employee/WorkExperience/insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save-workexperience").removeClass("btn-progress");
        });
    } else {
        console.log("edit param workexperience:", param);
        $.post("/Admin/Employee/WorkExperience/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btnbtn-save-workexperience").removeClass("btn-progress");
        });
    }
}
