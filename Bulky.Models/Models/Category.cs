using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")] // data annotation for client side validation
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")] //custom error with range
        public int DisplayOrder { get; set; }
    }
}
