using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecomm.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Catid { get; set; }
        [Required]
        public string Catname { get; set; }
    }
}
