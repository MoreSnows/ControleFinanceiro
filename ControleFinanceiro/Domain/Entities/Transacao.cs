using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ControleFinanceiro.Domain.Entities
{
    [Table("Transacoes")]
    public class Transacao
    {
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int Categoria { get; set; }

        public Transacao() => Id = Guid.NewGuid();
    }
}
