using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr_Vacancy_Managment.Models
{
    public class Vacancy
    {
        [Key]
        public int Vacancy_ID { get; set; }
        [Required]
        public string Vacancy_Title { get; set; }
        [Required]
        public string Vacancy_Description { get; set; }
        [Required]
        public string Vacancy_Number { get; set; }
        [Required]
        public int Number_Of_Jobs_Under_Vacancy { get; set; }
        [Required]
        public int Depart_ID { get; set; }
        [ForeignKey("Depart_ID")]
        public Department Departments { get; set; }
        public DateTime Last_Date { get; set; }
        public DateTime Date_Of_Creation { get; set; }
        [Required]
        public string Owned_By { get; set; }
        public Vacancy_Status Vacancy_Status { get; set; }
        public Timing Timing { get; set; } 
        public int salary { get; set; }
    }
    public enum  Vacancy_Status{ 
        Close=0,
        Open=1,
        Suspended=2
    }
    public enum Timing
    {
        Part_Time=0,
        Full_Time=1,
        Remote=2
    }
}
