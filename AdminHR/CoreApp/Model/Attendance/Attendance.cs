using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Attendance
{
    [Table("Attendance")]
    public class Attendance
    {
        public string Id { get; set; }
        public string EmployeeID { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
