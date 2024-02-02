using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicPay.Application.Interfaces;
using PicPay.Domain.Models;
using System.Security.Claims;

namespace PicPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciaController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TransferenciaModel>> Pix(string? remetente, string destinatario, decimal valor, TransferenciaModel transferenciaModel)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            remetente = userEmail!;
            var condicional = await _transferenciaService.GetByIdentificador(remetente);

            try
            {
                if(remetente != null && destinatario  != null && valor > 0) 
                {
                    if (condicional.email.Equals(destinatario, StringComparison.OrdinalIgnoreCase) ||
                        condicional.cpf.Equals(destinatario, StringComparison.OrdinalIgnoreCase) || condicional.telefone.Equals(destinatario, StringComparison.OrdinalIgnoreCase))
                    {
                        return BadRequest("Remetente e destinatário não podem ser iguais.");
                    }
                    var pix = await _transferenciaService.Pix(remetente, destinatario, valor, transferenciaModel);
                    if (pix == null)
                    {
                        return BadRequest("HOUVE UM PROBLEMA NO SERVIÇO DE PIX!");
                    }
                    else if(condicional.saldo < valor) {

                        return BadRequest("SALDO INSUFICIENTE");
                    }
                    else
                    {
                        return Ok(transferenciaModel);
                    }
                } else
                {
                    return BadRequest("PREENCHA OS CAMPOS NECESSARIOS");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Houve um problema interno" + ex);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferenciaModel>>> GetAllTransferencias(string? usuario)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            usuario = userEmail!;
            var user = await _transferenciaService.GetAllTransferencias(usuario);

            if (user is null)
            {
                return NotFound();
            } else
            {
                return Ok(user);
            }
        }
    }
}
