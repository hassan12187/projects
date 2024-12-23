using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hr_Vacancy_Managment.Models
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }
        [Required]
        public string Employee_Name { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Employee Email")]
        public string Employee_Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Employee Password")]
        public string Employee_Password { get; set; }

        [Required]
        public string Employee_Number { get; set; }
        public Employee_Status Employee_Status { get; set; }
        public DateTime Created_At { get; set; }
        [Required]
        public int Depart_ID { get; set; }
        public Department department { get; set; }
    }
    public enum Employee_Status
    {
        Deactive=0,
        Active=1
    }
}
