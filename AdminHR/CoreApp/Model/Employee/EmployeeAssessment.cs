using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Employee
{
    [Table("EmployeeAssessment")]
    public class EmployeeAssessment
    {
        [Key]
        public string Id { get; set; }
        public string? AssessmentID { get; set; }
        public string? EmployeeID { get; set; }
        public string? Score { get; set; }
    }
}
