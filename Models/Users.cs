using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "First Name: ")]
        public string FirstName {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name: ")]
        public string LastName {get;set;}

        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        [Display(Name = "Username: ")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Passwords must be at least 5 characters")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Passwords must be at least 5 characters")]
        [Display(Name = "Confirm Password: ")]
        [Compare("Password", ErrorMessage = "The Passwords do not match.")]
        public string PassConfirm { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Interest> Interests { get; set; }
    }
}