using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Name: ")]

        public string HobbyName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        [Display(Name = "Description")]

        public string HobbyDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set;}

        public List<Interest> Hobbists { get; set; }
    }
}