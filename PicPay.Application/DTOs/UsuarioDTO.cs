﻿

namespace PicPay.Application.DTOs
{
   public class UsuarioDTO
   {

        public Guid id_usuario { get; set; }
        public string? nomeCompleto { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public decimal? saldo { get; set; }
        public string? telefone { get; set; }

    }
}
