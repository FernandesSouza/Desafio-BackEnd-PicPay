using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Models
{
    public class TransferenciaModel
    {
        [Key]
        public Guid dd { get; set; }
        public string? info_remetente { get; set; }
        public string? info_destinatario { get; set; }
        public decimal valor { get; set; }
        public bool autorizacaoExterna { get; set; }
        public bool sucesso { get; set; }
        public DateTime dataTransferencia { get; set; } = DateTime.UtcNow;

       

    }
}
