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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IBaseRepository<UsuarioModel> _repository;
        private readonly IMapper _mapper;

        public UsuarioRepository(IBaseRepository<UsuarioModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAll()
        {
            var usuarioModel = await _repository.VerTodos();
            var usuarioDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarioModel);
            return usuarioDTO;

        }

        public async Task<UsuarioDTO> Register(UsuarioDTO usuarioDTO)
        {
            bool emailOuCpfExistem = await _repository.Existe(u => u.email == usuarioDTO.email && u.cpf == usuarioDTO.cpf);
            if(emailOuCpfExistem == true) {

                throw new InvalidOperationException("Email ou Cpf já cadastrado");
            }
            else
            {
                var tarefaModel = _mapper.Map<UsuarioModel>(usuarioDTO);
                await _repository.Cadastro(tarefaModel);

                var resultado = _mapper.Map<UsuarioDTO>(tarefaModel);

                return resultado;
            }

            
        }

       
    }
}
