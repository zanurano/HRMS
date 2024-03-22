using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Attendance
{
    [Table("Holiday")]
    public class Holiday
    {
        [Key]
        public string Id { get; set; }
        public string? Name { get; set; }
        public DateTime OnDate { get; set; }
    }
}
