using AutoMapper;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Interfaces;
using PicPay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Application.Services
{
    public class LojistaRepository : ILojistaRepository
    {
        private readonly IBaseRepository<LojistaModel> _repository;
        private readonly IMapper _mapper;

        public LojistaRepository(IBaseRepository<LojistaModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LojistaDTO> Register(LojistaDTO lojistaDTO)
        {
            bool emailOuCpfExistem = await _repository.Existe(u => u.email == lojistaDTO.email || u.cpf == lojistaDTO.cpf || u.cnpj == lojistaDTO.cnpj);
            if (emailOuCpfExistem == true)
            {
                throw new InvalidOperationException("Email,Cpf ou Cnpj já cadastrado");
            }
            else
            {
                var lojistaModel = _mapper.Map<LojistaModel>(lojistaDTO);
                await _repository.Cadastro(lojistaModel);

                var resultado = _mapper.Map<LojistaDTO>(lojistaModel);

                return resultado;

            }
        }
    }
}
