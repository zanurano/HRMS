var today = new Date();
year = today.getFullYear();
month = today.getMonth();
day = today.getDate();
var calendar = null;

model.data.holiday = ko.observable(ko.mapping.toJS(model.prototype.holiday));
model.list.events = ko.observableArray([]);

model.is.addNewHoliday = ko.observable();

model.on.addNewHoliday = () => {
    model.is.addNewHoliday(true);
    //model.data.holiday({ Id: "", Name: "", OnDate: "" });
    $('#modalHoliday').modal('show');
}

model.init.holiday = function () {
    $.get("/Admin/Administrator/Holiday/Gets", function (res) {
        res.Data.forEach((x) => {
            model.list.events.push({
                id: x.Id,
                title: x.Name,
                start: new Date(x.OnDate),
                end: new Date(x.OnDate),
                backgroundColor: "#fe9701",
            });
        });
        console.log("res:", res);
        calendar = $('#holidayCalendar').fullCalendar({
            //defaultView: 'month',
            //lazyFetching: true,
            height: 'auto',
            editable: true,
            selectable: true,
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month'
            },
            events: model.list.events(),
            dayClick: function (date, jsEvent, view) {
                model.is.addNewHoliday(false);
                var events = $('#holidayCalendar').fullCalendar('clientEvents', function (ev) {
                    return moment(ev.start).format("YYYYMMDD") == moment(date).format("YYYYMMDD");
                });
                console.log("events", events)
                model.data.holiday({ Id: events[0].id, Name: events[0].title, OnDate: moment(events[0].start).format("YYYY-MM-DD") })
                $('#modalHoliday').modal('show');
            },
            //eventClick: function (event, jsEvent, view) {
            //    console.log("event:",event)
            //    $('#modalHoliday').modal('show');
            //    model.data.holiday({Id: event.id, Name: event.title, OnDate: new Date(event.start)})
            //},
        });
    });
}

model.on.saveHoliday = function () {
    let param = model.data.holiday();
    let event = {
        title: model.data.holiday().Name,
        start: new Date(model.data.holiday().OnDate),
        end: new Date(model.data.holiday().OnDate),
        //backgroundColor: "#fe9701",
        className: "bg-danger",
    }

    $("#btn-save").addClass("btn-progress");
    if (model.is.addNewHoliday()) {
        console.log("param:", param);
        $.post("/Admin/Administrator/Holiday/Insert", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.list.events.push(param);
            event.id = res.Data.Id;
            $("#holidayCalendar").fullCalendar('renderEvent', event, true);
            $('#modalHoliday').modal('hide')
        });
    } else {
        console.log("param:", param);
        $.post("/Admin/Administrator/Holiday/Update", param, function (res) {
            swal('Good Job', res.Message, 'success');
            $("#btn-save").removeClass("btn-progress");
            model.list.events.push(param);
            $("#holidayCalendar").fullCalendar('removeEvents', [param.Id], true);
            $("#holidayCalendar").fullCalendar('renderEvent', event, true);
            $('#modalHoliday').modal('hide');
        });
    }
}

model.on.deleteHoliday = function () {
    let param = model.data.holiday();
    $("#btn-delete").addClass("btn-progress");
    $.post("/Admin/Administrator/Holiday/Delete", param, function (res) {
        swal('Good Job', res.Message, 'success');
        $("#btn-delete").removeClass("btn-progress");
        model.list.events().splice(model.list.events().findIndex(item => item.Id === param.Id), 1);
        $("#holidayCalendar").fullCalendar('removeEvents', [param.Id] , true);
        $('#modalHoliday').modal('hide')
    });
}
