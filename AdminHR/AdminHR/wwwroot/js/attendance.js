model.data.attendance = ko.observable(ko.mapping.toJS(model.prototype.attendance));

model.is.addNewAttendance = ko.observable(true);
model.is.loading = ko.observable(true);

model.text.Name = ko.observable();
model.text.DateClock = ko.observable();

model.init.attendance = function () {
    if ($.fn.DataTable.isDataTable("#dtTable")) {
        $('#dtTable').DataTable().clear().destroy();
    }

    model.is.loading(true);
    //$("#clockIn").datetimepicker({
    //    "allowInputToggle": true,
    //    "showClose": true,
    //    "showClear": true,
    //    "showTodayButton": true,
    //    "format": "YYYY-MM-DD HH:mm:ss",
    //});
    //$("#clockOut").datetimepicker({
    //    "allowInputToggle": true,
    //    "showClose": true,
    //    "showClear": true,
    //    "showTodayButton": true,
    //    "format": "YYYY-MM-DD HH:mm:ss",
    //});

    let table = $('#dtTable');
    let dateOfMonth = getDaysArrayByMonth();

    let columns = [{
            title: "Employee ID",
            data: "EmployeeID",
        }, {
            title: "Employee Name",
            data: "EmployeeName"
        }];

    dateOfMonth.forEach((x) => {
        columns.push({
            title: moment(x).format("DD"),
            data: moment(x).format("YYYYMMDD")
        });
    });

    $.get("/Admin/Attendance/Gets/Combine/202403", function (res) {
        let dtSource = [];
        res.Data.Attendances.forEach((x) => {
            let obj1 = {
                EmployeeID: x.EmployeeID,
                EmployeeName: x.FullName
            }, obj2 = {}, absence = {};

            for (key in x.Attendances) {
                obj2[moment(key).format("YYYYMMDD")] = x.Attendances[key];
            }

            let assign = Object.assign(obj1, obj2);

            dtSource.push(assign);
        });

        console.log("dtSource:", dtSource);

        table.DataTable({
            data: dtSource,
            aoColumns: columns,
            createdRow: function (row, data, index, obj) {
                let i = 2;
                for (key in data) {
                    if (data[key] == 'P') {
                        $(`td:eq(${i})`, row).html('<i class="fas fa-check" />').addClass('text-success');
                    } else if (data[key] == 'H') {
                        $(`td:eq(${i})`, row).addClass('text-secondary');
                    } else if (data[key] == 'A') {
                        $(`td:eq(${i})`, row).addClass('text-danger');
                    }
                    i++;
                }
            },
            initComplete: function (settings, json) {
                
            }
        });
        model.is.loading(false);
    });

    
}

model.on.saveAttendance = function () {
    let param = model.data.attendance();
    $("#btn-save").addClass("btn-progress");
    if (model.is.addNewAttendance()) {
        console.log("param insert:", param);
        $.post("/Admin/Attendance/Insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.init.attendance();
        });
    } else {
        console.log("param update:", param);
        $.post("/Admin/Attendance/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.init.attendance();
        });
    }
}