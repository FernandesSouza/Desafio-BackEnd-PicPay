using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;

namespace PicPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LojistaController : ControllerBase
    {
        private readonly ILojistaRepository _lojistaRepository;

        public LojistaController(ILojistaRepository lojistaRepository)
        {
            _lojistaRepository = lojistaRepository;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterNewLojista(LojistaDTO lojistaDTO)
        {
            try
            {
                lojistaDTO.id_lojista = Guid.NewGuid();
                await _lojistaRepository.Register(lojistaDTO);
                if(lojistaDTO is null)
                {
                    return BadRequest("Ops! Parece que aconteceu um problema");
                }
                else
                {
                    return Ok(lojistaDTO);
                }
            }
            catch (Exception)
            {
                return BadRequest("Já existe um lojista cadastrado com esse email, CPF ou CNPJ.");
            }

        }
    }
}
