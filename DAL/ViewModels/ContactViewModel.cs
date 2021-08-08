using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.ViewModels
{
    public class ContactViewModel
    {
        [Key]
        public int ContactId { get; set; }
        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "Please enter full name")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required(ErrorMessage = "Please enter designation")]
        public string Designation { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Please enter mobile number")]
        public string Mobile { get; set; }

        
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Please choose gender")]
        public string Gender { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "Please select country")]
        public string Country { get; set; }
        public string ExistingImage { get; set; }
    }
}
