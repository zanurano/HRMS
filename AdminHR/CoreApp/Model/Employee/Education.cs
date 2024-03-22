using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Employee
{
    [Table("Education")]
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string EmployeeID { get; set; }
        public string SchoolName { get; set; }
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
        public string Title { get; set; }
        public string Major { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
