using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public int UserId { get; set; }
        public int HobbyId { get; set; }

        public User User { get; set; }
        public Hobby Hobby { get; set; }
    }
}