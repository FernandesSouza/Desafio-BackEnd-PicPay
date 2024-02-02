using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Interfaces;

namespace PicPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(UsuarioDTO usuarioDTO)
        {
            try
            {
                usuarioDTO.id_usuario = Guid.NewGuid();
                await _usuarioRepository.Register(usuarioDTO);
                if (usuarioDTO is null)
                {
                    return BadRequest("Ops! Parece que aconteceu um problema");
                }
                else
                {
                    return Ok(usuarioDTO);
                }
            } catch(Exception)
            {
                return BadRequest("Já existe um usuário cadastrado com esse email ou CPF.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsers()
        {
            var users = await _usuarioRepository.GetAll();

            if(users != null)
            {
                return Ok(users);
            } else
            {
                return BadRequest();
            }
        }

    }
}
