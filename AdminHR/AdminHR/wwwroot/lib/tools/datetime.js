var getDaysArray = function (year, month) {
    var monthIndex = month - 1; // 0..11 instead of 1..12
    var names = ['sun', 'mon', 'tue', 'wed', 'thu', 'fri', 'sat'];
    var date = new Date(year, monthIndex, 1);
    var result = [];
    while (date.getMonth() == monthIndex) {
        result.push(date.getDate() + '-' + names[date.getDay()]);
        date.setDate(date.getDate() + 1);
    }
    return result;
}

var getDaysArrayByMonth = function () {
    var daysInMonth = moment().daysInMonth();
    var arrDays = [];

    while (daysInMonth) {
        var current = moment().date(daysInMonth);
        arrDays.push(current);
        daysInMonth--;
    }

    return arrDays.sort((a, b) => a.diff(b));
}