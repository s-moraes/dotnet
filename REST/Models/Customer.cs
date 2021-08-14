using System.ComponentModel.DataAnnotations;

namespace Module1.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(15)]
        public string Name { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage="Email Format Not Valid")]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}