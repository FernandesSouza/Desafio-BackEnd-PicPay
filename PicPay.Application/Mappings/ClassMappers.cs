using AutoMapper;
using PicPay.Application.DTOs;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Mappings
{
   public class ClassMappers: Profile
   {

        public ClassMappers()
        {
            CreateMap<UsuarioModel, UsuarioDTO>().ReverseMap();
            CreateMap<LojistaModel, LojistaDTO>().ReverseMap();
            CreateMap<TransferenciaModel, TransferenciaDTO>().ReverseMap();
        }
    }
}
