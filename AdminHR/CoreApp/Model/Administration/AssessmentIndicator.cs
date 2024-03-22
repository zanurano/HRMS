using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Administration
{
    [Table("AssessmentIndicator")]
    public class AssessmentIndicator
    {
        [Key]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int? QualityValue { get; set; }
    }
}
