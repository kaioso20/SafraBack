using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroService.Model
{
    [Table("Safra")]
    public class Safra
    {
        [Key]
        [Column(Order = 1)]
        public long IdSafra { get; set; }
        [Column(Order = 2)]
        public string Nome { get; set; }
        [Column(Order = 3)]
        public int? Ano { get; set; }
    }
}
