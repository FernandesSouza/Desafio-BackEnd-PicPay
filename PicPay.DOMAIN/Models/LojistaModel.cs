using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Models
{
    public class LojistaModel
    {
        [Key]
        public Guid id_lojista { get; set; }
        public string? nomeCompleto { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public decimal? saldo { get; set; }
        public string? cnpj { get; set; }
    }
}
