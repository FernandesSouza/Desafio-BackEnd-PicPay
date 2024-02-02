using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.DTOs
{
    public class TransferenciaDTO
    {
        public Guid dd { get; set; }
        public string? info_remetente { get; set; }
        public string? info_destinatario { get; set; }
        public decimal valor { get; set; }
        public bool autorizacaoExterna { get; set; }
        public bool sucesso { get; set; }
        public DateTime dataTransferencia { get; set; } = DateTime.UtcNow;

    }
}
