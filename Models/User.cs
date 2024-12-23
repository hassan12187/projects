using System.ComponentModel.DataAnnotations;

namespace Hr_Vacancy_Managment.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }
        [Required]
        [StringLength(50,ErrorMessage = "Only 50 characters allowed")]
        public string User_Name { get; set; }
        [Required]
        [EmailAddress]
        public string User_Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30,ErrorMessage="only 30 maximum characters allowed")]
        public string User_Password { get; set; }
        public User_Status user_status { get; set; }
    }
    public enum User_Status{
        Active = 1, Inactive = 2
    }
}

