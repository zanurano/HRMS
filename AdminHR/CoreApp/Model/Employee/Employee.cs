using CoreApp.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoreApp.Model.Employee
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public string EmployeeID { get; set; }
        public string? FullName { get; set; }
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
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
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
        public string? NIK { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        private MaritalStatus _maritalStatus { get; set; }
        public MaritalStatus? MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                _maritalStatus = value ?? 0;
                MaritalStatusDescription = Enum.GetName(typeof(MaritalStatus), (MaritalStatus)_maritalStatus);
            }
        }
        [NotMapped]
        public string? MaritalStatusDescription { get; set; } = string.Empty;
        public string? CurrentPosition { get; set; } = string.Empty;
        public DateTime UpdateDate { get; set; }
    }

    public enum Gender : int
    {
        None = 0,
        Male = 1,
        Female = 2,
    }

    public enum Religion : int
    {
        Islam = 0,
        Katolik = 1,
        Kristen = 2,
        Budha = 3,
        Hindu = 4,
    }

    public enum MaritalStatus : int
    {
        None = 0,
        Married = 1,
        Single = 2,
        Widowed = 3,
        Divorced = 4,
        Cohabiting = 5,
        RegisteredPartnership = 6,
    }
}
