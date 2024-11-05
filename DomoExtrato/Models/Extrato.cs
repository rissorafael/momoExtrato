using System.ComponentModel.DataAnnotations;

namespace DomoExtrato.Models
{
    public class Extrato
    {
        public int Id { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string TipoTransacao { get; set; }
        public decimal ValorMonetario { get; set; }
    }
}
