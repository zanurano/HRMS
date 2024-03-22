namespace CoreApp.Model.Attendance
{
    public class Attendance
    {
        public string Id { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
