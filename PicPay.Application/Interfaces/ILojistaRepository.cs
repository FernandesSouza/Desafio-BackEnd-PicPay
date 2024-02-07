using PicPay.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Interfaces
{
    public interface ILojistaRepository
    {
        Task<LojistaDTO> Register(LojistaDTO lojistaDTO);

    }
}
