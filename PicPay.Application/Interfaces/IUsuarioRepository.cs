using PicPay.Application.DTOs;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Interfaces
{
   public interface IUsuarioRepository
    {
        Task<UsuarioDTO> Register(UsuarioDTO usuarioDTO);

        Task<IEnumerable<UsuarioDTO>> GetAll(); 

    }
}
