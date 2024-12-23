using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hr_Vacancy_Managment.Models
{
    public class Applicant_Vacancy
    {
        [Key]
        public int Applicant_Vacancy_ID { get; set; }
        [Required]
        public int Applicant_ID { get; set; }
        [ForeignKey("Applicant_ID")]
        public Applicant applicant { get; set; }
        [Required]
        public int Vacancy_ID { get; set; }
        [ForeignKey("Vacancy_ID")]
        public Vacancy vacancy {  get; set; }
        public DateTime Applied_Date {  get; set; }
        public string? Interviewer_Number {  get; set; }
        public string? Interviewer_Name { get; set; }
        public DateTime? Scheduled_Interview_Date {  get; set; }
        public Applicant_Vacancy_Status? App_Vac_Status { get; set; }
    }
    public enum Applicant_Vacancy_Status{
        Interview_Scheduled,
        Selected,
        Rejected,
        Not_Required
    }
}
