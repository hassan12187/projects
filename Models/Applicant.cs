using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace Hr_Vacancy_Managment.Models
{
    public class Applicant
    {
        [Key]
        public int Applicant_ID { get; set; }   
        [Required]
        [StringLength(50,ErrorMessage = "Only 50 characters are allowed")]
        public string Applicant_Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 characters are allowed")]
        [EmailAddress]
        public string Applicant_Email { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "Only 12 characters are allowed")]
        public string Applicant_Phone { get; set; }
        [Required]
        public string Applicant_CV { get; set; }
        public string? Applied_For {  get; set; }
        [Required]
        public string Applicant_Number { get; set; }
        public DateTime Creation_Date { get; set; }
        public Applicant_Status Applicant_Status  { get; set; }
    }
    public enum Applicant_Status
    {
        Not_in_proccess=0,
        In_Proccess=1,
        Hired=2,
        Banned=3
    }
}
