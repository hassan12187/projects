using System.ComponentModel.DataAnnotations;

namespace Hr_Vacancy_Managment.Models
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 Characters are allowed")]
        public string Department_Name { get; set; }
        public Status Department_Status { get; set; }
        public DateTime Created_At { get; set; }
        public List<Employee> employees { get; set; }
        public List<Vacancy> Vacancy { get; set; }
    }
    public enum Status
    {
        Active=1,
        Deactive=0
    }
}
