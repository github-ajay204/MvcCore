using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTutorial.Models
{
    [Table("Category")]
    public class Category
    {
        //[Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName( "Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
