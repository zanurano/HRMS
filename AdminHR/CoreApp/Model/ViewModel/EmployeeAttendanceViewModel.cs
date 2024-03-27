namespace CoreApp.Model.ViewModel
{
    public class EmployeeAttendanceViewModel
    {
        public string? EmployeeID { get; set; }
        public string? FullName { get; set; }
        public Dictionary<DateTime, string>? Attendances { get; set; }
    }
}
