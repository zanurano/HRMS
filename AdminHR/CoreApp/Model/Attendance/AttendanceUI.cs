namespace CoreApp.Model.Attendance
{
    public class AttendanceUI
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
    }
}
