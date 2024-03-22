using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Employee
{
    [Table("FamilyMember")]
    public class FamilyMember
    {
        [Key]
        public string Id { get; set; }
        public string? EmployeeID { get; set; }
        public string? FullName {  get; set; }
        private Religion _religion { get; set; }
        public Religion? Religion
        {
            get
            {
                return _religion;
            }

            set
            {
                _religion = value ?? 0;
                ReligionDescription = Enum.GetName(typeof(Religion), (Religion)_religion);
            }
        }
        [NotMapped]
        public string? ReligionDescription { get; set; } = string.Empty;
        private Gender _gender { get; set; }
        public Gender? Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value ?? 0;
                GenderDescription = Enum.GetName(typeof(Gender), (Gender)_gender);
            }
        }
        [NotMapped]
        public string? GenderDescription { get; set; } = string.Empty;
        public string? RelationShip { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
